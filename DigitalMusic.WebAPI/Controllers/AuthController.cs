using System.Security.Claims;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.AuthFeatures.ForgotPasswordFeatures;
using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures;
using DigitalMusic.Application.Features.AuthFeatures.ResetPasswordFeatures;
using DigitalMusic.Application.Features.AuthFeatures.VerifyFeatures;
using DigitalMusic.Application.Helper.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMusic.WebAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAccessTokenHelper _accessTokenHelper;
        private readonly IRefreshTokenHelper _refreshTokenHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IMediator mediator, IAccessTokenHelper accessTokenHelper, IRefreshTokenHelper refreshTokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _accessTokenHelper = accessTokenHelper;
            _refreshTokenHelper = refreshTokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request,
           CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(request, cancellationToken);
            var accessToken = _accessTokenHelper.GenerateAccessToken(user.Id.ToString(), user.Email, user.Username, user.Role.name);
            var refreshToken = _refreshTokenHelper.GenerateRefreshToken(user.Id.ToString(), user.Email, user.Username, user.Role.name);
            _refreshTokenHelper.SetRefreshToken(refreshToken, user.Username);
            return Ok(accessToken);
        }

        [HttpPost("verify")]
        public async Task<ActionResult<string>> Verify(string verifyToken ,CancellationToken cancellationToken)
        {
            var verify = await _mediator.Send(new VerifyRequest(verifyToken), cancellationToken);
            return Ok(verify);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
            string[] errors;
            if (string.IsNullOrEmpty(refreshToken))
            {
                errors = new string[] { "Invalid token." };
                throw new BadRequestException(errors);
            }

            var principal = _accessTokenHelper.ValidateAccessToken(refreshToken);

            // Mendapatkan informasi user dari token
            var id = principal.FindFirstValue(ClaimTypes.Sid);
            var email = principal.FindFirstValue(ClaimTypes.Email);
            var username = principal.FindFirstValue(ClaimTypes.Name);
            var roleName = principal.FindFirstValue(ClaimTypes.Role);
            _refreshTokenHelper.ValidateRefreshToken(username, refreshToken);

            var newAccessToken = _accessTokenHelper.GenerateAccessToken(id,email, username, roleName);
            var newRefreshToken = _refreshTokenHelper.GenerateRefreshToken(id,email, username, roleName);
            _refreshTokenHelper.SetRefreshToken(newRefreshToken, username);
            return Ok(newAccessToken);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            var forgotPassword = await _mediator.Send(new ForgotPasswordRequest(email), cancellationToken);
            return Ok(forgotPassword);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var resetPassword = await _mediator.Send(request, cancellationToken);
            return Ok(resetPassword);
        }
    }
}