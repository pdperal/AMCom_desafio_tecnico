namespace Questao5.Domain.Entities
{
    public class Idempodent
    {
        public string Id { get; private set; }
        public string Request { get; private set; }
        public string Response { get; private set; }

        public Idempodent(string id, string request, string response)
        {
            Id = id;
            Request = request;
            Response = response;
        }

        public bool RequestAlreadyProcessed()
            => Request != null && Id != null && Response != null;
    }
}
