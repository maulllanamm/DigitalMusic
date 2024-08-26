using System.Data;
using FluentValidation;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteImage;

public class DeleteImageValidator : AbstractValidator<DeleteImageRequest>
{
    public DeleteImageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");

    }
}