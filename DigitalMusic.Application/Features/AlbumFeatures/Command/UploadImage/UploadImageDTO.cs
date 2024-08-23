using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UploadImage
{
    public class UploadImageDTO
    {
        public IFormFile ImageFile { get; set; }
    }
}
