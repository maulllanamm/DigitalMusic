using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed record UpdateAlbumRequest
    (
        Guid Id,
        string Name,
        int Year
    ) : IRequest<UpdateAlbumResponse>;
}
