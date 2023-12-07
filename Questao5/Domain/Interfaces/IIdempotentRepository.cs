using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Interfaces
{
    public interface IIdempotentRepository
    {
        public Task<Idempodent> CheckForIdempondentRequestResult(GetIdempodentDto dto);
        public Task SaveIdempondentRequest(NewIdempodentDto newIdempodentDto);
    }
}
