using ManageContacts.Model.Models.Companies;
using ManageContacts.Model.Models.Groups;
using ManageContacts.Model.Models.PhoneNumbers;
using ManageContacts.Model.Models.Users;

namespace ManageContacts.Model.Models.Contacts;

public class ContactEditModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Note { get; set; }
    public Guid? GroupId { get; set; }
    public CompanyEditModel Company { get; set; }
    public IEnumerable<PhoneNumberEditModel> PhoneNumbers { get; set; }
}