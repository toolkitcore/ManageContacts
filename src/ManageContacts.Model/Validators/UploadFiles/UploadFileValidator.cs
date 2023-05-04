using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ManageContacts.Model.Validators.UploadFiles;

public class UploadFileValidator : AbstractValidator<IFormFile>
{
    public UploadFileValidator()
    {
        RuleFor(file => file)
            .NotEmpty()
            .NotNull()
            .WithMessage("Image is required")
            .Must(file => file.Length > 0 && file.Length <= 10485760)
            .WithMessage("Invalid image size. Image must be between 1MB and 10MB.")
            .Must(file => file.FileName.EndsWith(".png") 
                          || file.FileName.EndsWith(".jpg") 
                          || file.FileName.EndsWith(".jpeg") 
                          || file.FileName.EndsWith(".gif"))
            .WithMessage("Invalid image format. Only PNG, JPG, JPEG and GIF formats are allowed.");
    }
}