using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public sealed record GetAllAlbumRequest() : IRequest<List<GetAllAlbumResponse>>;
}
