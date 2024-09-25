using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetById
{
    public sealed record GetByIdPlaylistRequest(Guid id) : IRequest<GetByIdPlaylistResponse>;
}
