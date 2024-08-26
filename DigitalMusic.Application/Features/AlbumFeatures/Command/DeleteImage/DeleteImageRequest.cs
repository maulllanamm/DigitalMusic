using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteImage;
    public sealed record DeleteImageRequest
    (
        Guid Id
    ) : IRequest<bool>;
