using FluentValidation;
using ManageContacts.Model.Models.Users;
using ManageContacts.Model.Validators.Extensions;

namespace ManageContacts.Model.Validators.Users;

public class UserLoginValidator : AbstractValidator<UserLoginModel>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("User name cannot be empty or null.")
            .MinimumLength(3)
            .WithMessage("User name must be at least 6 characters.");
        
        RuleFor(x => x.Password).NotPassword();
    }
}