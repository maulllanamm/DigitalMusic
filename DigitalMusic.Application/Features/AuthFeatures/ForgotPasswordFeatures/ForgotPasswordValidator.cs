using FluentValidation;

namespace DigitalMusic.Application.Features.AuthFeatures.ForgotPasswordFeatures
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
