using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Query.GetAll
{
    public sealed record GetAllSongRequest() : IRequest<List<GetAllSongResponse>>;
}
