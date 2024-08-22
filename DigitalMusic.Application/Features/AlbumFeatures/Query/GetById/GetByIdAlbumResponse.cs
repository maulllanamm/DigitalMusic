using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public sealed record GetByIdAlbumResponse
    {
        public string Name { get; init; }
        public int Year { get; init; }
        public string Cover { get; init; }
    }
}
