using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist
{
    public sealed record UpdatePlaylistResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public User User { get; init; }
    }
}
