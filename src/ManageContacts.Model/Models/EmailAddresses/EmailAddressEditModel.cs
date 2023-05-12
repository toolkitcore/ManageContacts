namespace ManageContacts.Model.Models.EmailAddresses;

public class EmailAddressEditModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string? Type { get; set; }
    public string? FormattedType { get; set; }
    public Guid EmailTypeId { get; set; }
}