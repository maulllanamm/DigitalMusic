using FluentValidation;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.CreatePlaylist
{
    public class CreatePlaylistValidator : AbstractValidator<CreatePlaylistRequest>
    {
        public CreatePlaylistValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 50) // Panjang minimum dan maksimum
                .WithMessage("Title must be between 2 and 50 characters long.");
        }

    }
}
