using System.ComponentModel.DataAnnotations;

namespace ManageContacts.Shared.AttributeExtensions;

public class EmailOrPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phoneAttribute = new PhoneAttribute();
        var emailAttribute = new EmailAddressAttribute();
        
        if (phoneAttribute.IsValid(value?.ToString()) || emailAttribute.IsValid(value?.ToString()))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("The field must be either a valid phone number or email address.");
        }
    }
}