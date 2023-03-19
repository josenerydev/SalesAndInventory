namespace SalesAndInventory.Api.Utilities
{
    public class Result<T>
    {
        public bool Succeeded { get; private set; }
        public T Data { get; private set; }
        public string[] Errors { get; private set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static Result<T> Failure(params string[] errors)
        {
            return new Result<T> { Succeeded = false, Errors = errors };
        }
    }
}