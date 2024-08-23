using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UploadImage
{
    public class UploadImageValidator : AbstractValidator<UploadImageRequest>
    {
        public UploadImageValidator()
        {
            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("File is required.")
                .Must(BeValidImage).WithMessage("Invalid file type.")
                .Must(BeAllowedSize).WithMessage("File size exceeds the allowed limit.");
        }
        
        private bool BeValidImage(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }

        private bool BeAllowedSize(IFormFile file)
        {
            // Define your file size limit in bytes (e.g., 5MB)
            const long maxSizeInBytes = 2 * 1024 * 1024; 
        
            return file.Length <= maxSizeInBytes;
        }
    }
}
