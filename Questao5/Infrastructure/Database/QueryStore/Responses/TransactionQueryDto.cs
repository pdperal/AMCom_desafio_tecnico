using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class TransactionQueryDto
    {
        public string Id { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public double Valor { get; set; }

        public Transaction ToEntity()
        {
            return new Transaction(Id, IdContaCorrente, DataMovimento, TipoMovimento, Valor);
        }
    }
}
