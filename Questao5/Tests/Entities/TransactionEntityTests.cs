using Questao5.Domain;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Entities
{
    public class TransactionEntityTests
    {
        [Fact]
        public void Transaction_invalid_type()
        {
            // arrange

            var transaction = new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69",
                "F475F943-7067-ED11-A06B-7E5DFA4A16C9", DateTime.Now.ToString(), 
                'A', 10);
            // act

            var result = transaction.IsValidTransactionType();
            // assert 

            Assert.False(result);
        }
        [Fact]
        public void Transaction_valid_credit_type()
        {
            // arrange

            var transaction = new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69",
                "F475F943-7067-ED11-A06B-7E5DFA4A16C9", DateTime.Now.ToString(),
                Constants.CREDIT_TRANSACTION_IDENTIFIER, 10);
            // act

            var result = transaction.IsValidTransactionType();
            // assert 

            Assert.True(result);
        }
        [Fact]
        public void Transaction_valid_debit_type()
        {
            // arrange

            var transaction = new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69",
                "F475F943-7067-ED11-A06B-7E5DFA4A16C9", DateTime.Now.ToString(),
                Constants.DEBIT_TRANSACTION_IDENTIFIER, 10);
            // act

            var result = transaction.IsValidTransactionType();
            // assert 

            Assert.True(result);
        }

        [Fact]
        public void Transaction_invalid_amount()
        {
            // arrange

            var transaction = new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69",
                "F475F943-7067-ED11-A06B-7E5DFA4A16C9", DateTime.Now.ToString(),
                Constants.DEBIT_TRANSACTION_IDENTIFIER, 0);
            // act

            var result = transaction.IsValidAmount();
            // assert 

            Assert.False(result);
        }

        [Fact]
        public void Transaction_valid_amount()
        {
            // arrange

            var transaction = new Transaction("d4ca5147-c006-4277-bbe3-22424afcaa69",
                "F475F943-7067-ED11-A06B-7E5DFA4A16C9", DateTime.Now.ToString(),
                Constants.DEBIT_TRANSACTION_IDENTIFIER, 10);
            // act

            var result = transaction.IsValidAmount();
            // assert 

            Assert.True(result);
        }
    }
}
