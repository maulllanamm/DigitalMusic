using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetAll
{
    public sealed record GetAllPlaylistResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public User User { get; init; }
    }
}