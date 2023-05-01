using ManageContacts.Model.Abstractions.Audits;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserFilterRequestModel : FilterRequestAuditModel
{
    [JsonProperty(PropertyName = "deleted")]
    public bool Deleted { get; set; } = false;
}