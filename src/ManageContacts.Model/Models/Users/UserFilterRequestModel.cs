using ManageContacts.Model.Abstractions.Audits;
using ManageContacts.Model.Abstractions.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserFilterRequestModel : FilterRequestModel
{
    [BindProperty(Name = "deleted")]
    public bool Deleted { get; set; } = false;
}