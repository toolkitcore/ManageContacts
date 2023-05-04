using FluentValidation;
using ManageContacts.Model.Models.Users;
using ManageContacts.Model.Validators.Extensions;
using ManageContacts.Shared.Extensions;

namespace ManageContacts.Model.Validators.Users;

public class UserEditValidator : AbstractValidator<UserEditModel>
{
    public UserEditValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage("First name cannot be empty or null.")
            .MinimumLength(3)
            .WithMessage("First name must be at least 3 characters.");
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Last name cannot be empty or null.")
            .MinimumLength(3)
            .WithMessage("Last name must be at least 3 characters.");
        
        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("User name cannot be empty or null.")
            .MinimumLength(3)
            .WithMessage("User name must be at least 6 characters.");
        
        RuleFor(u => u.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("The email is invalid.");

        RuleFor(x => x.Password).NotPassword();
        
        RuleFor(x => x.PhoneNumber).NotPhone();
        
        RuleFor(x => x.Avatar)
            .Must(path => string.IsNullOrEmpty(path) || File.Exists(path))
            .WithMessage("Url avatar is invalid.");

        RuleFor(x => x.ListRoleId)
            .Must(rid => (rid == null || !rid.NotNullOrEmpty()) || rid.HasDuplicated(i => i))
            .WithMessage("Duplicate role id list");
    }
}