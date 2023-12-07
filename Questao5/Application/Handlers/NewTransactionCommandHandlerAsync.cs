using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories;
using System.Transactions;

namespace Questao5.Application.Handlers
{
    public class NewTransactionCommandHandlerAsync : IRequestHandler<NewTransactionCommand, Result<TransactionViewModel>>
    {
        private readonly IIdempotentRepository _idempotentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public NewTransactionCommandHandlerAsync(IIdempotentRepository idempotentRepository, 
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _idempotentRepository = idempotentRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<Result<TransactionViewModel>> Handle(NewTransactionCommand request, CancellationToken cancellationToken)
        {
            var idempodentRequest = await _idempotentRepository.CheckForIdempondentRequestResult(new GetIdempodentDto(request.RequestId));

            if (idempodentRequest is not null 
                && idempodentRequest.RequestAlreadyProcessed())
            {
                return new Result<TransactionViewModel>(
                success: true,
                    data: new TransactionViewModel(idempodentRequest.Response)
                );
            }

            var entity = request.ToEntity();

            if (!entity.IsValidAmount())
            {
                return new Result<TransactionViewModel>(
                    success: false,
                    error: new Error(
                        description: Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_VALUE],
                        type: nameof(AccountTransactionErrorsEnum.INVALID_VALUE)
                        )
                    );
            }

            if (!entity.IsValidTransactionType())
            {
                return new Result<TransactionViewModel>(
                   success: false,
                   error: new Error(
                       description: Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_TYPE],
                       type: nameof(AccountTransactionErrorsEnum.INVALID_TYPE)
                       )
                   );
            }

            var accountInformation = await _accountRepository.GetAccountInformation(new GetAccountDto(request.AccountId));

            if (accountInformation is null)
            {
                return new Result<TransactionViewModel>(
                  success: false,
                  error: new Error(
                      description: Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_ACCOUNT],
                      type: nameof(AccountTransactionErrorsEnum.INVALID_ACCOUNT)
                      )
                  );
            }
            if (!accountInformation.IsActive())
            {
                return new Result<TransactionViewModel>(
                  success: false,
                  error: new Error(
                      description: Constants.TransactionErrors[AccountTransactionErrorsEnum.INACTIVE_ACCOUNT],
                      type: nameof(AccountTransactionErrorsEnum.INACTIVE_ACCOUNT)
                      )
                  );
            }

            var transactionSaveResult = await _transactionRepository.SaveTransaction(new NewTransactionDto(entity.Id, entity.IdContaCorrente, entity.DataMovimento,
                entity.TipoMovimento, entity.Valor));

            if (transactionSaveResult is false)
            {
                return new Result<TransactionViewModel>(
                  success: false,
                  error: new Error(
                      description: Constants.TransactionErrors[AccountTransactionErrorsEnum.GENERIC_ERROR],
                      type: nameof(AccountTransactionErrorsEnum.GENERIC_ERROR)
                      )
                  );
            }

            await _idempotentRepository.SaveIdempondentRequest(new NewIdempodentDto(request.RequestId, JsonConvert.SerializeObject(request), entity.Id));

            return new Result<TransactionViewModel>(
                   success: true,
                   data: new TransactionViewModel(entity.Id)
                   );
        }        
    }
}
