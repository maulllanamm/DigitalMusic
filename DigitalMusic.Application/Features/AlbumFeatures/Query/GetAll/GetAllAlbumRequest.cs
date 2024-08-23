using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Query.GetById
{
    public sealed record GetAllAlbumRequest() : IRequest<List<GetAllAlbumResponse>>;
}
