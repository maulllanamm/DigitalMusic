using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.AlbumFeatures.Query.GetById
{
    public sealed record GetAllAlbumResponse
    {
        public string Name { get; init; }
        public int Year { get; init; }
        public string Cover { get; init; }
    }
}
