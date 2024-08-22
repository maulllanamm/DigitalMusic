using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.DeleteUser
{
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetById(request.id);
                if (user == null)
                {
                    throw new NotFoundException("User Not Found");
                }

                return await _userRepository.Delete(request.id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
