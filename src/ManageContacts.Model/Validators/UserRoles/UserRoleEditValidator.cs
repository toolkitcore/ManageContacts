using FluentValidation;
using ManageContacts.Model.Models.UserRoles;
using ManageContacts.Shared.Extensions;

namespace ManageContacts.Model.Validators.UserRoles;

public class UserRoleEditValidator : AbstractValidator<UserRoleEditModel>
{
    public UserRoleEditValidator()
    {
        RuleFor(x => x.ListRoleId)
            .Must(rid => (rid == null || !rid.NotNullOrEmpty()) || rid.HasDuplicated(i => i))
            .WithMessage("Duplicate role id list");
    }
}