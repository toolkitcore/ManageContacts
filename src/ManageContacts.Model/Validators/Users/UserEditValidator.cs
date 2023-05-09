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
            .EmailAddress()
            .WithMessage("The email is invalid.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().When(x => !string.IsNullOrEmpty(x.Password)) // Kiểm tra password không được rỗng khi nó không phải là null hoặc rỗng
            .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Password)) // Kiểm tra độ dài password tối thiểu khi nó không phải là null hoặc rỗng
            .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Password)) // Kiểm tra độ dài password tối đa khi nó không phải là null hoặc rỗng
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$").When(x => !string.IsNullOrEmpty(x.Password)) // Kiểm tra password theo định dạng
            .WithMessage("Password is not valid.");
        
        RuleFor(x => x.PhoneNumber).NotPhone();
        
        RuleFor(x => x.Avatar)
            .Must(path => string.IsNullOrEmpty(path) || File.Exists(path))
            .WithMessage("Url avatar is invalid.");
    }
}