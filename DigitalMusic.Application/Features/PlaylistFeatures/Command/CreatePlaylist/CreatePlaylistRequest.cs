using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.CreatePlaylist
{
    public sealed record CreatePlaylistRequest
    (
        string Name
    ) : IRequest<Guid>;
}
