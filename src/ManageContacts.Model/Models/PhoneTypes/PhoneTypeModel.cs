namespace ManageContacts.Model.Models.PhoneTypes;

public class PhoneTypeModel
{
    public Guid Id { get; set; }
    public string TypeName { get; set; }
    public string UnaccentedName { get; set; }
    public string? Description { get; set; }
}