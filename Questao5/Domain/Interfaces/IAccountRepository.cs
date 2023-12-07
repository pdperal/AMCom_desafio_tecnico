using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Domain.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account?> GetAccountInformation(GetAccountDto dto);
    }
}
