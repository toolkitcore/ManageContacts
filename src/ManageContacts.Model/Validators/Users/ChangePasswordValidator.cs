using FluentValidation;
using ManageContacts.Model.Models.Users;
using ManageContacts.Model.Validators.Extensions;

namespace ManageContacts.Model.Validators.Users;
 
public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordValidator()
    {
        RuleFor(u => u.OldPassword).NotPassword();
        RuleFor(u => u.NewPassword).NotPassword();
    }
}