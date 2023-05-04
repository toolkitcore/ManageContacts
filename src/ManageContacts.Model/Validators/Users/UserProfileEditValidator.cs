using System.Text.RegularExpressions;
using FluentValidation;
using ManageContacts.Model.Models.Users;
using ManageContacts.Model.Validators.Extensions;

namespace ManageContacts.Model.Validators.Users;

public class UserProfileEditValidator : AbstractValidator<UserProfileEditModel>
{
    public UserProfileEditValidator()
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

        RuleFor(x => x.PhoneNumber).NotPhone();
        
        RuleFor(x => x.Avatar)
            .Must(path => string.IsNullOrEmpty(path) || File.Exists(path))
            .WithMessage("Url avatar is invalid.");
    }
}