using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class NewTransactionCommand : IRequest<Result<TransactionViewModel>>
    {
        public string RequestId { get; set; }
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public char TransactionType { get; set; }

        public Transaction ToEntity()
        {
            return new Transaction(Guid.NewGuid().ToString(), AccountId, DateTime.Now.ToString(), char.ToUpper(TransactionType), Amount);
        }
    }
}
