using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace DigitalMusic.Application.Features.AuthFeatures.ForgotPasswordFeatures
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IResetPasswordTokenHelper _resetPasswordTokenHelper;
        public ForgotPasswordHandler(IUserRepository userRepository, IResetPasswordTokenHelper resetPasswordTokenHelper)
        {
            _userRepository = userRepository;
            _resetPasswordTokenHelper = resetPasswordTokenHelper;
        }


        public async Task<string> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var passwordResetToken = _resetPasswordTokenHelper.GeneratePasswordResetToken(user.username, user.email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(passwordResetToken) as JwtSecurityToken;

            user.password_reset_token = passwordResetToken;
            user.password_reset_expires = securityToken.ValidTo;

            await _userRepository.Update(user);

            return "You may now reset your password";
        }
    }
}
