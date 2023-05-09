using FluentValidation;
using ManageContacts.Model.Models.Contacts;

namespace ManageContacts.Model.Validators.Contacts;

public class ContactEditValidator : AbstractValidator<ContactEditModel>
{
    public ContactEditValidator()
    {
        
    }
}