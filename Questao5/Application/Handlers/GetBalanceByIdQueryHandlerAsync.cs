using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Handlers
{
    public class GetBalanceByIdQueryHandlerAsync : IRequestHandler<GetBalanceByAccountIdQuery, Result<BalanceViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public GetBalanceByIdQueryHandlerAsync(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public async Task<Result<BalanceViewModel>> Handle(GetBalanceByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var clientEntity = await _accountRepository.GetAccountInformation(new GetAccountDto(request.Id));

            if (clientEntity is null)
            {
                return new Result<BalanceViewModel>(
                    success: false,
                    error: new Error(
                        description: Constants.BalanceErrors[AccountBalanceErrorsEnum.INVALID_ACCOUNT],
                        type: nameof(AccountBalanceErrorsEnum.INVALID_ACCOUNT)
                        )
                    );                    
            }

            if (!clientEntity.IsActive())
            {
                return new Result<BalanceViewModel>(
                    success: false,
                    error: new Error(
                        description: Constants.BalanceErrors[AccountBalanceErrorsEnum.INACTIVE_ACCOUNT],
                        type: nameof(AccountBalanceErrorsEnum.INACTIVE_ACCOUNT)
                        )
                    );
            }

            var transactions = await _transactionRepository.GetAllTransactions(new GetTransactionsDto(request.Id));

            if (!transactions.Any()) 
            {
                return new Result<BalanceViewModel>(
                    success: true,
                    data: new BalanceViewModel(clientEntity.Numero, clientEntity.Nome, Constants.SALDO_INICIAL)
                    );
            }

            return new Result<BalanceViewModel>(
                    success: true,
                    data: new BalanceViewModel(clientEntity.Numero, clientEntity.Nome, CalculateBalance(transactions))
                    );
        }

        private static double CalculateBalance(List<Transaction> transactions)
        {
            // calculo do saldo manual para garantir O(n)
            double debits = 0;
            double credit = 0;

            for (int i = 0; i < transactions.Count; i++)
            {
                switch (transactions[i].TipoMovimento)
                {
                    case Constants.DEBIT_TRANSACTION_IDENTIFIER:
                        debits += transactions[i].Valor;
                        break;
                    case Constants.CREDIT_TRANSACTION_IDENTIFIER:
                        credit += transactions[i].Valor;
                        break;
                }
            }

            return credit - debits;
        }
    }
}
