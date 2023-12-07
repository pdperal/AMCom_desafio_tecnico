namespace Questao5.Domain.Entities
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public Error Error { get; private set; }
        public T Data { get; private set; }

        public Result(bool success, Error error = default, T data = default)
        {
            Success = success;
            Error = error;
            Data = data;
        }
    }

    public class Error
    {
        public string Description { get; private set; }
        public string Type { get; private set; }

        public Error(string description, string type)
        {
            Description = description;
            Type = type;
        }
    }
}
