using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetByUsername
{
    public class GetByUsernameHandler : IRequestHandler<GetByUsernameRequest, GetByUsernameResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetByUsernameHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetByUsernameResponse> Handle(GetByUsernameRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);
            if (user == null)
            {
                throw new NotFoundException($"User with username {request.Username} Not Found");
            }
            return _mapper.Map<GetByUsernameResponse>(user);
        }
    }
}
