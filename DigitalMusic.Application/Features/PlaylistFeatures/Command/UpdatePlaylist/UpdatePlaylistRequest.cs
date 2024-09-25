using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist
{
    public sealed record UpdatePlaylistRequest
    (
        Guid Id,
        string Name
    ) : IRequest<UpdatePlaylistResponse>;
}
