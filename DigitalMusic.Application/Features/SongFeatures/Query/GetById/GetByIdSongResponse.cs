using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.SongFeatures.Query.GetById
{
    public sealed record GetByIdSongResponse
    {
        public string Title { get; init; }
        public int Year { get; init; }
        public string Performer { get; init; }
        public string Genre { get; init; }
        public int Duration { get; init; }
        public Guid? AlbumId { get; init; }
    }
}
