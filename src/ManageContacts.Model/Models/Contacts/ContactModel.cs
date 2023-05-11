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
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty(PropertyName = "last_name")]
    public string LastName { get; set; }
        
    [JsonProperty(PropertyName = "nick_name")]
    public string NickName { get; set; }
        
    [JsonProperty(PropertyName = "birthday")]
    public DateTime? Birthday { get; set; }
        
    [JsonProperty(PropertyName = "note")]
    public string? Note { get; set; }
        
    [JsonProperty(PropertyName = "group_id")]
    public Guid GroupId { get; set; }
    
    [JsonProperty(PropertyName = "user")]
    public UserModel User { get; set; }
        
    [JsonProperty(PropertyName = "group")]
    public GroupModel Group { get; set; }
        
    [JsonProperty(PropertyName = "company")]
    public CompanyModel Company { get; set; }
        
    [JsonProperty(PropertyName = "phone_numbers")]
    public IEnumerable<PhoneNumberModel> PhoneNumbers { get; set; }
        
    [JsonProperty(PropertyName = "email_addresses")]
    public IEnumerable<EmailAddressModel> EmailAddresses { get; set; }
        
    [JsonProperty(PropertyName = "addresses")]
    public IEnumerable<AddressModel> Addresses { get; set; }
        
    [JsonProperty(PropertyName = "relatives")]
    public IEnumerable<RelativeModel> Relatives { get; set; }
}