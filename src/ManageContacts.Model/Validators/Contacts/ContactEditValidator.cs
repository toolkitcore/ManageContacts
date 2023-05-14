using FluentValidation;
using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Validators.Companies;
using ManageContacts.Model.Validators.PhoneNumbers;

namespace ManageContacts.Model.Validators.Contacts;

public class ContactEditValidator : AbstractValidator<ContactEditModel>
{
    public ContactEditValidator()
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
        
        RuleFor(x => x.NickName)
            .MinimumLength(3)
            .When(x => !string.IsNullOrEmpty(x.NickName))
            .WithMessage("Nickname must be at least 3 characters.");

        RuleFor(x => x.Birthday)
            .NotNull()
            .LessThan(DateTime.Now)
            .WithMessage("Birthday must be before today's date.");
        
        RuleFor(x => x.Note)
            .MaximumLength(1000)
            .WithMessage("Notes must be less than 1000 characters.");
        
        RuleFor(x => x.Company)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .SetValidator(new CompanyEditValidator());

        RuleForEach(x => x.PhoneNumbers)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .SetValidator(new PhoneNumberEditValidator());
    }
}