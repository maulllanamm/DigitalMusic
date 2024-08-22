using System.Security.Claims;
using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserHandler(IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newUser = _mapper.Map<User>(request);

                var user = await _userRepository.GetById(request.Id);
                user.username = newUser.username;
                user.email = newUser.email;
                user.full_name = newUser.full_name;
                user.phone_number = newUser.phone_number;
                user.address = newUser.address;

                var res = await _userRepository.Update(user);

                return _mapper.Map<UpdateUserResponse>(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
