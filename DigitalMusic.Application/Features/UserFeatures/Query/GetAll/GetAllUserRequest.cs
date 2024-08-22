using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetAll
{
    public sealed record GetAllUserRequest : IRequest<List<GetAllUserResponse>>
    {

    }
}
