using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetAll
{
    public sealed record GetAllUserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
    }
}
