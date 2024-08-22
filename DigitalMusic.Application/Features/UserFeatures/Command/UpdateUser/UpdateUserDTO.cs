using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public class UpdateUserRequestDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
