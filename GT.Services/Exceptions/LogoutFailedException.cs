namespace GT.Application.Exceptions
{
    public class LogoutFailedException : Exception
    {
        public int StatusCode { get; }

        public LogoutFailedException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
