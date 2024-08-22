namespace DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures
{
    public sealed record RegisterResponse
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
    }
}
