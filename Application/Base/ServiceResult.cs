namespace Application.Base
{
    public class ServiceResult
    {
        public bool IsSuccess { get; protected set; }
        public string? Message { get; protected set; }
        public int StatusCode { get; protected set; }

        public static ServiceResult Success(string? message = null, int statusCode = 200) =>
            new() { IsSuccess = true, Message = message, StatusCode = statusCode };

        public static ServiceResult Failure(string message, int statusCode = 400) =>
            new() { IsSuccess = false, Message = message, StatusCode = statusCode };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; private set; }

        public static ServiceResult<T> Success(T data, string? message = null, int statusCode = 200) =>
            new() { IsSuccess = true, Data = data, Message = message, StatusCode = statusCode };

        public static new ServiceResult<T> Failure(string message, int statusCode = 400) =>
            new() { IsSuccess = false, Message = message, StatusCode = statusCode };
    }
}