using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetById
{
    public sealed record GetByIdPlaylistResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public User User { get; init; }
    }
}
