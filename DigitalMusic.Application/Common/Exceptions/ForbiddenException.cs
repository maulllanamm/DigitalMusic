namespace DigitalMusic.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public string Message { get; set; }
        public ForbiddenException(string message) : base("Forbidden.")
        {
            Message = message;
        }
    }
}
