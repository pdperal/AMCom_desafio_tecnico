namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class GetAccountDto
    {
        public string Id { get; set; }

        public GetAccountDto(string id)
        {
            Id = id;
        }
    }
}
