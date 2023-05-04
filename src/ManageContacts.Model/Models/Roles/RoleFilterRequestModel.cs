using ManageContacts.Model.Abstractions.Audits;
using ManageContacts.Model.Abstractions.Requests;
using ManageContacts.Model.Abstractions.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Roles;

public class RoleFilterRequestModel : FilterRequestModel
{
    [BindProperty(Name = "deleted")]
    public bool Deleted { get; set; } = false;
}