namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class GetIdempodentDto
    {
        public string Id { get; set; }

        public GetIdempodentDto(string id)
        {
            Id = id;
        }
    }
}
