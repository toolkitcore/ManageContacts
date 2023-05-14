namespace ManageContacts.Model.Models.PhoneNumbers;

public class PhoneNumberEditModel
{
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string? Type { get; set; }
    public string? FormattedType { get; set; }
    public Guid? PhoneTypeId { get; set; }
}