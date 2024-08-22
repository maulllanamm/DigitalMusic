using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace DigitalMusic.Application.Features.AuthFeatures.VerifyFeatures
{
    public sealed class VerifyHandler : IRequestHandler<VerifyRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public VerifyHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(VerifyRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByVerifyToken(request.VerifyToken);
            if (user is null)
            {
                throw new NotFoundException("Invalid token");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(user.verify_token) as JwtSecurityToken;

            DateTime expires = securityToken.ValidTo;
            if (DateTime.Now > expires)
            {
                throw new UnauthorizedException("Token expired.");
            }

            user.verify_date = DateTime.UtcNow;
            await _userRepository.Update(user);
            return "User verified";
        }
    }
}
