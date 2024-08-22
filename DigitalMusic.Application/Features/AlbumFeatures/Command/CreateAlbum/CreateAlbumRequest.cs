using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed record CreateAlbumRequest
    (
        string Name,
        int Year
    ) : IRequest<Guid>;
}
