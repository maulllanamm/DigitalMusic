using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public sealed record GetByIdUserRequest(Guid id) : IRequest<GetByIdUserResponse>;
}
