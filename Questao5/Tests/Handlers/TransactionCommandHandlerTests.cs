using NSubstitute;
using NSubstitute.Core;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
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
    public class TransactionCommandHandlerTests
    {
        private readonly IIdempotentRepository _idempondentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCommandHandlerTests()
        {
            _idempondentRepository = Substitute.For<IIdempotentRepository>();
            _accountRepository = Substitute.For<IAccountRepository>();
            _transactionRepository = Substitute.For<ITransactionRepository>();
        }

        [Fact]
        public void Handle_Transaction_Request_already_processed_idempondent()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };
            var idempondent = new Idempodent("B6BAFC09", string.Empty, "d51217a4-e98d-4db9-832d-2eda86099252");

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs(idempondent);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.True(result.Success);
            Assert.Equal(idempondent.Response, result.Data.Id);
        }

        [Fact]
        public void Handle_Transaction_Request_invalid_amount()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 0,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_VALUE], result.Error.Description);
        }

        [Fact]
        public void Handle_Transaction_Request_invalid_transaction_type()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'A'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_TYPE], result.Error.Description);
        }

        [Fact]
        public void Handle_Transaction_Request_invalid_transaction_account_not_exist()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            _accountRepository
                .GetAccountInformation(new GetAccountDto(command.AccountId))
                .ReturnsForAnyArgs((Account)null);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.TransactionErrors[AccountTransactionErrorsEnum.INVALID_ACCOUNT], result.Error.Description);
        }

        [Fact]
        public void Handle_Transaction_Request_invalid_transaction_error_while_saving_transaction()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            _accountRepository
                .GetAccountInformation(new GetAccountDto(command.AccountId))
                .ReturnsForAnyArgs(new Account(command.AccountId, 123, "Katherine Sanchez", 1));

            var entity = command.ToEntity();

            _transactionRepository
                .SaveTransaction(new NewTransactionDto(entity.Id, entity.IdContaCorrente,
                    entity.DataMovimento, entity.TipoMovimento, entity.Valor))
                .ReturnsForAnyArgs(false);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.TransactionErrors[AccountTransactionErrorsEnum.GENERIC_ERROR], result.Error.Description);
        }

        [Fact]
        public void Handle_Transaction_Request_invalid_transaction_inactive_account()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            _accountRepository
                .GetAccountInformation(new GetAccountDto(command.AccountId))
                .ReturnsForAnyArgs(new Account(command.AccountId, 123, "Katherine Sanchez", 0));

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.False(result.Success);
            Assert.Equal(Constants.TransactionErrors[AccountTransactionErrorsEnum.INACTIVE_ACCOUNT], result.Error.Description);
        }

        [Fact]
            public void Handle_Transaction_Request_save_success()
        {
            // arrange
            var command = new NewTransactionCommand()
            {
                AccountId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                Amount = 10,
                RequestId = "B6BAFC09",
                TransactionType = 'C'
            };

            _idempondentRepository
                .CheckForIdempondentRequestResult(new GetIdempodentDto(command.RequestId))
                .ReturnsForAnyArgs((Idempodent)null);

            _accountRepository
                .GetAccountInformation(new GetAccountDto(command.AccountId))
                .ReturnsForAnyArgs(new Account(command.AccountId, 123, "Katherine Sanchez", 1));

            var entity = command.ToEntity();

            _transactionRepository
                .SaveTransaction(new NewTransactionDto(entity.Id, entity.IdContaCorrente,
                    entity.DataMovimento, entity.TipoMovimento, entity.Valor))
                .ReturnsForAnyArgs(true);

            var handler = new NewTransactionCommandHandlerAsync(_idempondentRepository, _accountRepository, _transactionRepository);

            // act

            var result = handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // assert

            Assert.True(result.Success);
            Assert.NotNull(result.Data.Id);
        }
    }
}
