using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAllTransactions(GetTransactionsDto accountId);
        public Task<bool> SaveTransaction(NewTransactionDto transaction);
    }
}
