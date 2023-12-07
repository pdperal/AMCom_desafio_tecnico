namespace Questao5.Domain.Entities
{
    public class Transaction
    {
        public string Id { get; private set; }
        public string IdContaCorrente { get; private set; }
        public string DataMovimento { get; private set; }
        public char TipoMovimento { get; private set; }
        public double Valor { get; private set; }

        public Transaction(string id, string idContaCorrente, string dataMovimento, char tipoMovimento, double valor)
        {
            Id = id;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public bool IsValidTransactionType()
        {
            return char.ToLower(TipoMovimento).Equals('c')
                || char.ToLower(TipoMovimento).Equals('d');
        }

        public bool IsValidAmount()
        {
            return Valor > 0;
        }
    }
}
