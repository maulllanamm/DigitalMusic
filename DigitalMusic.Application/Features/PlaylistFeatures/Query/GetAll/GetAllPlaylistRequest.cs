using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetAll
{
    public sealed record GetAllPlaylistRequest() : IRequest<List<GetAllPlaylistResponse>>;
}
