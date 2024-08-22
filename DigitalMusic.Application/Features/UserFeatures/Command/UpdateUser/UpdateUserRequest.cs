using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public sealed record UpdateUserRequest
    (
        Guid Id,
        string Username,
        string Email,
        string Fullname,
        string PhoneNumber,
        string Address
    ) : IRequest<UpdateUserResponse>;
}
