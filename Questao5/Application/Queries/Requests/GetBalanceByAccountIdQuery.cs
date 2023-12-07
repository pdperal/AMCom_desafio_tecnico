using MediatR;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests
{
    public class GetBalanceByAccountIdQuery : IRequest<Result<BalanceViewModel>>
    {
        public string Id { get; set; }

        public GetBalanceByAccountIdQuery(string id)
        {
            Id = id;
        }
    }
}
