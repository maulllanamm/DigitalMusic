using FluentValidation;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist
{
    public class UpdatePlaylistValidator : AbstractValidator<UpdatePlaylistRequest>
    {
        public UpdatePlaylistValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 50) // Panjang minimum dan maksimum
                .WithMessage("Title must be between 2 and 50 characters long.");

        }
    }
}
