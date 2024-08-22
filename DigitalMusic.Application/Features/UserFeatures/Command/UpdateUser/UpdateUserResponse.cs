namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public sealed record UpdateUserResponse
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
    }
}
