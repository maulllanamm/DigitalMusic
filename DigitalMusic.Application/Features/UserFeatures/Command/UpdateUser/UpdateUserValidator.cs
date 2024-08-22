using FluentValidation;

namespace DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Fullname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        }
    }
}
