using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace DigitalMusic.Application.Features.AuthFeatures.ResetPasswordFeatures
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly string _papper = "v81IKJ3ZBFgwc2AdnYeOLhUn9muUtIQ0AJKgfewu*!(24uyjfebweuy";
        private readonly int _iteration = 5;
        public ResetPasswordHandler(IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        public async Task<string> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPasswordResetToken(request.PasswordResetToken);
            if (user is null)
            {
                var errors = new string[] { "Invalid reset password token." };
                throw new BadRequestException(errors);
            }

            if (DateTime.UtcNow > user.password_reset_expires)
            {
                throw new UnauthorizedException("Token expired.");
            }

            var newPassword = _passwordHelper.ComputeHash(request.NewPassword, user.password_salt, _papper, _iteration);
            user.password_hash = newPassword;
            user.password_reset_token = null;
            user.password_reset_expires = null;

            await _userRepository.Update(user);

            return "Password successfully reset";
        }
    }
}
