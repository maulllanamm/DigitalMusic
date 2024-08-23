using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Query.GetById
{
    public sealed record GetByIdAlbumRequest(Guid id) : IRequest<GetByIdAlbumResponse>;
}
