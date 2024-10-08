﻿using FluentValidation;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum
{
    public class CreateAlbumValidator : AbstractValidator<CreateAlbumRequest>
    {
        public CreateAlbumValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 50) // Panjang minimum dan maksimum
                .WithMessage("Name must be between 2 and 50 characters long.");
            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required.")
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("The year must be between 1900 and the current year.");
        }
    }
}
