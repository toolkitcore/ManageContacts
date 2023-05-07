using FluentValidation;
using ManageContacts.Model.Models.Users;
using ManageContacts.Model.Validators.Extensions;

namespace ManageContacts.Model.Validators.Users;

public class UserRegistrationValidator : AbstractValidator<UserRegistrationModel>
{
    public UserRegistrationValidator()
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
        
        
    }
}