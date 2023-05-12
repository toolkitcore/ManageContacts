using ManageContacts.Model.Models.Addresses;
using ManageContacts.Model.Models.Companies;
using ManageContacts.Model.Models.EmailAddresses;
using ManageContacts.Model.Models.Groups;
using ManageContacts.Model.Models.PhoneNumbers;
using ManageContacts.Model.Models.Relatives;
using ManageContacts.Model.Models.Users;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Contacts;

public class ContactModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Note { get; set; }
    public Guid GroupId { get; set; }
    public UserModel User { get; set; }
    public GroupModel Group { get; set; }
    public CompanyModel Company { get; set; }
    public IEnumerable<PhoneNumberModel> PhoneNumbers { get; set; }
    public IEnumerable<EmailAddressModel> EmailAddresses { get; set; }
    public IEnumerable<AddressModel> Addresses { get; set; }
    public IEnumerable<RelativeModel> Relatives { get; set; }
}