using FluentValidation;
using ManageContacts.Model.Models.PhoneNumbers;
using ManageContacts.Model.Validators.Extensions;

namespace ManageContacts.Model.Validators.PhoneNumbers;

public class PhoneNumberEditValidator : AbstractValidator<PhoneNumberEditModel>
{
    public PhoneNumberEditValidator()
    {
        RuleFor(x => x.Phone).NotPhone();
    }
}