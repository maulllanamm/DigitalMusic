namespace DigitalMusic.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string[] Errors { get; set; }

        public BadRequestException(string[] errors) : base("Multiple errors occurred. See error details.")
        {
            Errors = errors;
        }

    }
}
