using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Entities
{
    public class IdempondentEntityTests
    {
        [Fact]
        public void Idempondent_request_already_processed()
        {
            // arrange

            var account = new Idempodent("F475F943-7067-ED11-A06B-7E5DFA4A16C9", "teste", "response");
            // act

            var result = account.RequestAlreadyProcessed();
            // assert 

            Assert.True(result);
        }
    }
}
