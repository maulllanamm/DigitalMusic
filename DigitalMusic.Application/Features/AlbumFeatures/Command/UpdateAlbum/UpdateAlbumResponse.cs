using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UpdateAlbum
{
    public sealed record UpdateAlbumResponse
    {
        public string Name { get; init; }
        public int Year { get; init; }
    }
}
