using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public class UpdateAlbumDTO
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
