using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Relatives;

public class RelativeModel
{
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string? Type { get; set; }
    public string? FormattedType { get; set; }
    public Guid PhoneTypeId { get; set; }
}