using FluentValidation;
using ManageContacts.Model.Models.Companies;

namespace ManageContacts.Model.Validators.Companies;

public class CompanyEditValidator : AbstractValidator<CompanyEditModel>
{
    public CompanyEditValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Company name cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Company name must be at least 3 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(255)
            .WithMessage("Description company cannot be longer than 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}