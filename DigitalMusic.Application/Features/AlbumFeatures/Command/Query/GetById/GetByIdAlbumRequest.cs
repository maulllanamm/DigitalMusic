using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public sealed record GetByIdAlbumRequest(Guid id) : IRequest<GetByIdAlbumResponse>;
}
