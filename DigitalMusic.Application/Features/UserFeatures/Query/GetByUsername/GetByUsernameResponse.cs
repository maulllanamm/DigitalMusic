using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetByUsername
{
    public sealed record GetByUsernameResponse
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public Role Role { get; set; }
    }
}
