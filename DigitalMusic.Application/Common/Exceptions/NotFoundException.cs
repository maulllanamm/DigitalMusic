namespace DigitalMusic.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Message { get; set; }
        public NotFoundException(string message) : base("Not Found.")
        {
            Message = message;
        }
    }
}
