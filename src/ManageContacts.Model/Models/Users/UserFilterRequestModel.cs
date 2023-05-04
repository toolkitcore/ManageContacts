using ManageContacts.Model.Abstractions.Audits;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserFilterRequestModel : FilterRequestAuditModel
{
    [BindProperty(Name = "deleted")]
    public bool Deleted { get; set; } = false;
}