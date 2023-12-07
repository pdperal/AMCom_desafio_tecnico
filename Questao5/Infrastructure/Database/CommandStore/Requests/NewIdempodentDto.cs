namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class NewIdempodentDto
    {
        public string Id { get; private set; }
        public string Request { get; private set; }
        public string Response { get; private set; }

        public NewIdempodentDto(string id, string request, string response)
        {
            Id = id;
            Request = request;
            Response = response;
        }
    }
}
