using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Entities
{
    public class AccountEntityTests
    {
        [Fact]
        public void Account_Should_Be_Inactive()
        {
            // arrange

            var account = new Account("F475F943-7067-ED11-A06B-7E5DFA4A16C9", 741, "Ameena Lynn", 0);
            // act

            var result = account.IsActive();
            // assert 

            Assert.False(result);
        }
        [Fact]
        public void Account_Should_Be_Active()
        {
            // arrange

            var account = new Account("B6BAFC09 -6967-ED11-A567-055DFA4A16C9", 123, "Katherine Sanchez", 1);
            // act

            var result = account.IsActive();
            // assert 

            Assert.True(result);
        }
    }
}
