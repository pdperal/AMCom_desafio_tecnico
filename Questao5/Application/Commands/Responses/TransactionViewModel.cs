namespace Questao5.Application.Commands.Responses
{
    public class TransactionViewModel 
    {
        public string Id { get; set; }

        public TransactionViewModel(string id)
        {
            Id = id;
        }
    }
}
