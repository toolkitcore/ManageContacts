namespace ManageContacts.Model.Models.EmailTypes;

public class EmailTypeModel
{
    public Guid Id { get; set; }
    public string TypeName { get; set; }
    public string UnaccentedName { get; set; }
    public string? Description { get; set; }
}