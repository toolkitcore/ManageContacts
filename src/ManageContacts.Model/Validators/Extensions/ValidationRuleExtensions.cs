using FluentValidation;
using System.Text.RegularExpressions;

namespace ManageContacts.Model.Validators.Extensions;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptionsConditions<T, string> NotPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((password, context) =>
        {
            if (string.IsNullOrEmpty(password))
                context.AddFailure(@"The password is required.");
            else if (password.Length < 8 || password.Length > 30)
                context.AddFailure(@"The password must be 8 - 30 characters long.");
            else if (!Regex.IsMatch(password, @"\d+", RegexOptions.Singleline))
                context.AddFailure("The password must contains at least 1 numeric [0-9].");
            else if (!Regex.IsMatch(password, @"[a-z]", RegexOptions.Singleline))
                context.AddFailure("The password must contains at least 1 lowercase character [a-z].");
            else if (!Regex.IsMatch(password, @"[A-Z]", RegexOptions.Singleline))
                context.AddFailure("The password must contains at least 1 uppercase character [A-Z].");
            else if (!Regex.IsMatch(password, @"[\*\.\!\@\#\$\%\^\&\(\)\{\}\[\]\:\;\<\>\,\.\?\/\~_\+\-\=\|\\]", RegexOptions.Singleline))
                context.AddFailure("The password must contains at least 1 special character.");
        });
    }
    
    public static IRuleBuilderOptionsConditions<T, string> NotPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((phone, context) =>
        {
            if (string.IsNullOrEmpty(phone))
                context.AddFailure(@"The phone is required.");
            else if (phone.Length < 10 || phone.Length > 10)
                context.AddFailure(@"The password must be 10 characters long.");
            else if (!Regex.IsMatch(phone, @"^(03|05|07|08|09)+([0-9]{8})$", RegexOptions.Singleline))
                context.AddFailure("Phone is not valid");
        });
    }
}