using FluentValidation;
using ManageContacts.Model.Models.Roles;

namespace ManageContacts.Model.Validators.Roles;

public class RoleEditValidator : AbstractValidator<RoleEditModel>
{
    public RoleEditValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Role name must be at least 3 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(255)
            .WithMessage("Description cannot be longer than 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}