using FluentValidation;

namespace DigitalMusic.Application.Features.AuthFeatures.ResetPasswordFeatures
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator() 
        {
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(50);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(8).MaximumLength(50).Equal(x => x.NewPassword).WithMessage("Confirm password must match the new password.");
            RuleFor(x => x.PasswordResetToken).NotEmpty();
        }
    }
}
