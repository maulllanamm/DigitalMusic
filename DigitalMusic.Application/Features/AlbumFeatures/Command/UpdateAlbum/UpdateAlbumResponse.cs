using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed record UpdateAlbumResponse
    {
        public string Name { get; init; }
        public int Year { get; init; }
    }
}
