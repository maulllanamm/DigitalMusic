using FluentValidation;

namespace DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        }
    }
}
