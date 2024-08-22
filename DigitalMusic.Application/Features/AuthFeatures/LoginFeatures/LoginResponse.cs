using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.AuthFeatures.LoginFeatures
{
    public sealed record LoginResponse
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public Role Role { get; set; }
    }
}
