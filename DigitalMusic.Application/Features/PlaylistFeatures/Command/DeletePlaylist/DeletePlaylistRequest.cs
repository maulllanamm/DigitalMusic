using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.DeletePlaylist
{
    public sealed record DeletePlaylistRequest
    (
        Guid Id
    ) : IRequest<bool>;
}
