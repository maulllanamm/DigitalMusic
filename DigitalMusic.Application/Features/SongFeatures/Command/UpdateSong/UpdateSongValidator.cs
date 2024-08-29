using FluentValidation;

namespace DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong
{
    public class UpdateSongValidator : AbstractValidator<UpdateSongRequest>
    {
        public UpdateSongValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Length(2, 50) // Panjang minimum dan maksimum
                .WithMessage("Title must be between 2 and 50 characters long.");
            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required.")
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("The year must be between 1900 and the current year.");
            RuleFor(x => x.Performer)
                .NotEmpty()
                .WithMessage("Performer is required");
            RuleFor(x => x.Genre)
                .NotEmpty()
                .WithMessage("Genre is required");
            RuleFor(x => x.Duration)
                .NotEmpty()
                .WithMessage("Duration is requred");
        }
    }
}
