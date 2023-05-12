using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Roles;

public class RoleModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Deleted { get; set; }
}