using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Query.GetById
{
    public sealed record GetByIdSongRequest(Guid id) : IRequest<GetByIdSongResponse>;
}
