using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;

namespace ManageContacts.Service.HangfireService.ContactCleanupJob;

public class ContactCleanupJob
{
    private readonly IUnitOfWork _uow;
    private readonly IRepository<Contact> _contactRepository;
    
    public ContactCleanupJob(IUnitOfWork<ContactsContext> uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _contactRepository = uow.GetRepository<Contact>();
    }
    
    public void Run()
    {
        var contacts = _contactRepository.FindAll(
            predicate: c => c.Deleted && (c.DeletedTime.HasValue && c.DeletedTime.Value < DateTime.UtcNow.AddDays(-30)));
        _contactRepository.BulkDelete(contacts.ToList());
    }
}