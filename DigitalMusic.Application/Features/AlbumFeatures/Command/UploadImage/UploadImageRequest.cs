using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UploadImage
{
    public sealed record UploadImageRequest
    (
        Guid Id,
        IFormFile ImageFile
    ) : IRequest<string>;
}
