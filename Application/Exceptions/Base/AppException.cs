namespace Application.Exceptions.Base
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; set; }
        protected AppException(string message, int statusCode = 500) 
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
