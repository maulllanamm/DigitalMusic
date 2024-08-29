using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Command.DeleteSong
{
    public sealed record DeleteSongRequest
    (
        Guid Id
    ) : IRequest<bool>;
}
