using ManageContacts.Model.Abstractions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.Model.Models.Contacts;

public class ContactFilterRequestModel : FilterRequestModel
{
    [BindProperty(Name = "deleted")]
    public bool Deleted { get; set; } = false;
}