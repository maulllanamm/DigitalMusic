using AutoMapper;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetAll
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest,List<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _userRepository.GetAll();
                return _mapper.Map<List<GetAllUserResponse>>(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
