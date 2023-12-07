using NSubstitute;
using NSubstitute.Core;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories;
using Xunit;

namespace Questao5.Tests.Handlers
{
    public class GetBalanceByIdQueryHandlerTests
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetBalanceByIdQueryHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _transactionRepository = Substitute.For<ITransactionRepository>();
        }      

        [Fact]
        public void Handle_Get_Balance_Request_invalid_account_not_exist()
        {
            // arrange
            var query = new GetBalanceByAccountIdQuery("B6BAFC09 -6967-ED11-A567-055DFA4A16C9");

            _accountRepository
                .GetAccountInformation(new GetAccountDto(query.Id))
                .ReturnsForAnyArgs((Account)null);

            var handler = new GetBalanceByIdQueryHandlerAsync(_transactionRepository, _accountRepository);

            // act

            var result = handler.Handle(query, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.BalanceErrors[AccountBalanceErrorsEnum.INVALID_ACCOUNT], result.Error.Description);
        }

        [Fact]
        public void Handle_Get_Balance_Request_invalid_account_inactive()
        {
            // arrange
            var query = new GetBalanceByAccountIdQuery("B6BAFC09 -6967-ED11-A567-055DFA4A16C9");

            _accountRepository
                .GetAccountInformation(new GetAccountDto(query.Id))
                .ReturnsForAnyArgs(new Account(query.Id, 123, "Katherine Sanchez", 0));

            var handler = new GetBalanceByIdQueryHandlerAsync(_transactionRepository, _accountRepository);

            // act

            var result = handler.Handle(query, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.BalanceErrors[AccountBalanceErrorsEnum.INACTIVE_ACCOUNT], result.Error.Description);
        }

        [Fact]
        public void Handle_Get_Balance_Request_success_account_has_no_transactions()
        {
            // arrange
            var query = new GetBalanceByAccountIdQuery("B6BAFC09 -6967-ED11-A567-055DFA4A16C9");

            _accountRepository
                .GetAccountInformation(new GetAccountDto(query.Id))
                .ReturnsForAnyArgs(new Account(query.Id, 123, "Katherine Sanchez", 1));

            _transactionRepository
                .GetAllTransactions(new GetTransactionsDto(query.Id))
                .ReturnsForAnyArgs(new List<Transaction>());

            var handler = new GetBalanceByIdQueryHandlerAsync(_transactionRepository, _accountRepository);

            // act

            var result = handler.Handle(query, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.True(result.Success);
            Assert.Equal(Constants.SALDO_INICIAL, result.Data.Saldo);
        }

        [Fact]
        public void Handle_Get_Balance_Request_success()
        {
            // arrange
            var query = new GetBalanceByAccountIdQuery("B6BAFC09 -6967-ED11-A567-055DFA4A16C9");

            _accountRepository
                .GetAccountInformation(new GetAccountDto(query.Id))
                .ReturnsForAnyArgs(new Account(query.Id, 123, "Katherine Sanchez", 1));

            _transactionRepository
                .GetAllTransactions(new GetTransactionsDto(query.Id))
                .ReturnsForAnyArgs(new List<Transaction>() 
                { 
                    new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69", query.Id,
                    DateTime.Now.ToString(), Constants.CREDIT_TRANSACTION_IDENTIFIER, 10)
                });

            var handler = new GetBalanceByIdQueryHandlerAsync(_transactionRepository, _accountRepository);

            // act

            var result = handler.Handle(query, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.True(result.Success);
            Assert.Equal(10, result.Data.Saldo);
        }
    }
}
