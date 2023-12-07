namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class NewTransactionDto
    {
        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public string TransactionDate { get; private set; }
        public char TransactionType { get; private set; }
        public double Amount { get; private set; }

        public NewTransactionDto(string id, string accountId, string transactionDate, char transactionType, double amount)
        {
            Id = id;
            AccountId = accountId;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            Amount = amount;
        }
    }
}
