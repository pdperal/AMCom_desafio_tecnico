namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class GetTransactionsDto
    {
        public string Id { get; set; }

        public GetTransactionsDto(string id)
        {
            Id = id;
        }
    }
}
