using ManageContacts.Service.Services.Contacts;

namespace ManageContacts.WebApi.Controllers;

public class ContactController : BaseController
{
    private readonly IContactService _contactService;
    public ContactController(ILogger<ContactController> logger, IContactService contactService) : base(logger)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
    }
}