using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Requests;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Contacts;
using ManageContacts.Service.Abstractions.Core;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ManageContacts.Service.Services.Contacts;

public class ContactService : BaseService, IContactService
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<PhoneNumber> _phoneNumberRepository;
    private readonly IRepository<Company> _companyRepository;
    public ContactService(IUnitOfWork<ContactsContext> uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<ContactService> logger, IWebHostEnvironment env)
        : base(uow, httpContextAccessor, mapper, logger, env)
    {
        _contactRepository = uow.GetRepository<Contact>();
        _groupRepository = uow.GetRepository<Group>();
        _phoneNumberRepository = uow.GetRepository<PhoneNumber>();
        _companyRepository = uow.GetRepository<Company>();
    }
    
    public async Task<OkResponseModel<PaginationList<ContactModel>>> GetAllAsync(ContactFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var contacts = await _contactRepository.PagingAllAsync(
            predicate: c => (string.IsNullOrEmpty(filter.SearchString) || (!string.IsNullOrEmpty(filter.SearchString) && (c.FirstName.Contains(filter.SearchString) || c.LastName.Contains(filter.SearchString) || c.NickName.Contains(filter.SearchString)))) 
                            && filter.Deleted
                            && c.User.Id == _currentUserId,
            orderBy: sortFields.GetSortType(filter.GetSortType()),
            pageIndex: filter.PageIndex,
            pageSize: filter.PageSize,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (contacts.NotNullOrEmpty())
            return new OkResponseModel<PaginationList<ContactModel>>(_mapper.Map<PaginationList<ContactModel>>(contacts));

        return new OkResponseModel<PaginationList<ContactModel>>(new PaginationList<ContactModel>());
    }
    
    public async Task<OkResponseModel<ContactModel>> GetAsync(Guid contactId, bool deleted = false, CancellationToken cancellationToken = default)
    {
        var contact = await _contactRepository.GetAsync(
            predicate: c => c.Id == contactId && c.Deleted == deleted && c.User.Id == _currentUserId,
            include: c => c.Include(i => i.Group)
                .Include(i => i.Company)
                .Include(i => i.PhoneNumbers),
            cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (contact == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<ContactModel>(_mapper.Map<ContactModel>(contact));
    }

    public async Task<OkResponseModel<IEnumerable<ContactModel>>> GetAllByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await _groupRepository.GetAsync(
            predicate: g => g.Id == groupId && !g.Deleted && g.User.Id == _currentUserId,
            include: g => g.Include(i => i.Contacts),
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (group == null)
            throw new BadRequestException("The request is invalid.");

        if(group.Contacts.NotNullOrEmpty())
            return new OkResponseModel<IEnumerable<ContactModel>>(_mapper.Map<IEnumerable<ContactModel>>(group.Contacts));
        
        return new OkResponseModel<IEnumerable<ContactModel>>(new List<ContactModel>());

    }

    public async Task<BaseResponseModel> CreateAsync(ContactEditModel contactEdit, CancellationToken cancellationToken = default)
    {
        var existContact = await _contactRepository.GetAsync(
            predicate: c => (c.FirstName == contactEdit.FirstName || c.LastName == contactEdit.LastName || c.NickName == contactEdit.NickName) 
                            && !c.Deleted
                            && c.User.Id == _currentUserId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (existContact != null)
            throw new BadRequestException("The contact is already exists.");

        if (contactEdit.GroupId != null)
        {
            var group = await _groupRepository.GetAsync(
                predicate: g => g.Id == contactEdit.GroupId && !g.Deleted,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);
            if(group == null)
                throw new BadRequestException("The group contact doesn't exist");
        }

        var phoneTypeModels = contactEdit.PhoneNumbers
            .Where(p => p.PhoneTypeId != null)
            .Select(p => new
            {
                Id = p.PhoneTypeId,
                Type = p.Type
            });

        var phoneTypes = await _phoneNumberRepository.FindAllAsync(
            predicate: p=> phoneTypeModels.Any(x => x.Id == p.PhoneTypeId && x.Type == p.Type),
            cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (phoneTypeModels.Count() != phoneTypes.Count())
            throw new BadRequestException("There are phone types that don't exist.");
        
        var newContact = _mapper.Map<Contact>(contactEdit);
        newContact.CreatorId = _currentUserId;
        await _contactRepository.InsertAsync(newContact, cancellationToken).ConfigureAwait(false);

        try
        {
            await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new InternalServerException("Create contact failure.");
        }

        return new BaseResponseModel("Create contact successful.");
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid contactId, ContactEditModel contactEdit, CancellationToken cancellationToken = default)
    {
        var contact = await _contactRepository.GetAsync(
            predicate: c => c.Id == contactId && !c.Deleted,
            include: c => c.Include(i => i.Group)
                .Include(i => i.Company)
                .Include(i => i.PhoneNumbers),
        cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (contact == null)
            throw new BadRequestException("The request is invalid.");
        
        var existContact = await _contactRepository.GetAsync(
            predicate: c => (c.FirstName == contactEdit.FirstName || c.LastName == contactEdit.LastName || c.NickName == contactEdit.NickName) 
                            && !c.Deleted 
                            && c.Id != contactId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (existContact != null)
            throw new BadRequestException("The contact is already exists.");
        
        
        _uow.BeginTransaction();
        
        contact.FirstName = contactEdit.FirstName;
        contact.LastName = contactEdit.LastName;
        contact.NickName = contactEdit.NickName;
        contact.Birthday = contactEdit.Birthday;
        contact.Note = contactEdit.Note;
        contact.GroupId = contactEdit.GroupId;
        contact.Group = null;

        if (contact.Company != null)
        {
            if (contactEdit.Company != null)
            {
                contact.Company.Name = contactEdit.Company.Name;
                contact.Company.Description = contactEdit.Company.Description;
            }
            else
            {
                _companyRepository.Delete(contact.Company);
            }
        }
        else
        {
            if (contactEdit.Company != null)
            {
                contact.Company = new Company()
                {
                    Name = contactEdit.Company.Name,
                    Description = contactEdit.Company.Description,
                };
            }
        }
        
        foreach (var phone in contactEdit.PhoneNumbers)
        {
            var phoneNumber = contact.PhoneNumbers.FirstOrDefault(p => p.Id == phone.Id);
            if (phoneNumber != null)
            {
                phoneNumber.Phone = phone.Phone;
                phoneNumber.Type = phone.Type;
                phoneNumber.FormattedType = phone.FormattedType;
                phoneNumber.PhoneTypeId = phone.PhoneTypeId.HasValue ? phone.PhoneTypeId.Value : null;
            }
            else
            {
                var newPhoneNumber = new PhoneNumber
                {
                    Phone = phone.Phone,
                    Type = phone.Type,
                    FormattedType = phone.FormattedType,
                    PhoneTypeId = phone.PhoneTypeId.HasValue ? phone.PhoneTypeId.Value : null,
                    ContactId = contact.Id
                };

                contact.PhoneNumbers.Add(newPhoneNumber);
            }
        }
        
        var phoneNumbersToDelete = contact.PhoneNumbers
            .Where(p => !contactEdit.PhoneNumbers.Any(pu => pu.Id == p.Id));
        if (phoneNumbersToDelete.NotNullOrEmpty())
            _phoneNumberRepository.Delete(phoneNumbersToDelete.ToList());

        _contactRepository.Update(contact);
        
        try
        {
            await _uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await _uow.RollbackAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogError(ex, ex.Message);
            throw new InternalServerException("Update contact failure.");
        }
        
        return new BaseResponseModel("Update contact successful.");

    }

    public async Task<BaseResponseModel> DeleteAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        var contact = await _contactRepository.GetAsync(
            predicate: c => c.Id == contactId && !c.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (contact == null)
            throw new BadRequestException("The request is invalid.");
        
        _contactRepository.Delete(contact);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Delete contact successful.");
    }

    public async Task<BaseResponseModel> RecoverAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        var contact = await _contactRepository.GetAsync(
            predicate: c => c.Id == contactId && c.Deleted && c.User.Id == _currentUserId,
        cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (contact == null)
            throw new BadRequestException("The request is invalid.");
        
        if(contact.DeletedTime.HasValue && (DateTime.UtcNow - contact.DeletedTime.Value).Days > 30)
            throw new BadRequestException("Records deleted more than 30 days old cannot be recovered.");
        
        contact.Deleted = false;
        
        _contactRepository.Update(contact);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return new BaseResponseModel("Recover contact successful");
    }

    #region [PRIVATE FIELDS]
    private static readonly Dictionary<string, Func<IQueryable<Contact>, IOrderedQueryable<Contact>>> sortFields = new Dictionary<string, Func<IQueryable<Contact>, IOrderedQueryable<Contact>>>(StringComparer.OrdinalIgnoreCase)
    {
        { "default", contact => contact.OrderBy(c => c.LastName) },
        { "last_name_asc", contact => contact.OrderBy(c => c.LastName) },
        { "last_name_desc", contact => contact.OrderByDescending(c => c.LastName) },
        { "create_time_asc", contact => contact.OrderBy(c => c.CreatedTime) },
        { "create_time_desc", contact => contact.OrderByDescending(c => c.CreatedTime) }
    };
    #endregion
    
}