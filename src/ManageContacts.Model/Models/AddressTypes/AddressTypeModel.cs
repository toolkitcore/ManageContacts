namespace ManageContacts.Model.Models.AddressTypes;

public class AddressTypeModel
{
    public Guid Id { get; set; }
    public string TypeName { get; set; }
    public string UnaccentedName { get; set; }
    public string? Description { get; set; }
}