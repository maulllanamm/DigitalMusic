namespace DigitalMusic.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string Message { get; set; }
        public UnauthorizedException(string message) : base("Unauthorized.")
        {
            Message = message;
        }
    }
}
