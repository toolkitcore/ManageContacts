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
    private readonly IRepository<User> _userRepository;
    public ContactService(IUnitOfWork<ContactsContext> uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger, IWebHostEnvironment env)
        : base(uow, httpContextAccessor, mapper, logger, env)
    {
        _contactRepository = uow.GetRepository<Contact>();
        _groupRepository = uow.GetRepository<Group>();
        _userRepository = uow.GetRepository<User>();
    }
    
    public async Task<OkResponseModel<PaginationList<ContactModel>>> GetAllAsync(ContactFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var contacts = await _contactRepository.PagingAllAsync(
            predicate: c => (string.IsNullOrEmpty(filter.SearchString) || (!string.IsNullOrEmpty(filter.SearchString) && c.LastName.Contains(filter.SearchString))),
            orderBy: sortFields.GetSortType(filter.GetSortType()),
            pageIndex: filter.PageIndex,
            pageSize: filter.PageSize,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (contacts.NotNullOrEmpty())
            return new OkResponseModel<PaginationList<ContactModel>>(_mapper.Map<PaginationList<ContactModel>>(contacts));

        return new OkResponseModel<PaginationList<ContactModel>>(new PaginationList<ContactModel>());
    }

    public async Task<OkResponseModel<ContactModel>> GetAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        var contact = await _contactRepository.GetAsync(
            predicate: c => c.Id == contactId && !c.Deleted,
            include: c => c.Include(i => i.Group)
                .Include(i => i.Company)
                .Include(i => i.PhoneNumbers)
                .Include(i => i.EmailAddresses)
                .Include(i => i.Addresses)
                .Include(i => i.Relatives),
            cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (contact == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<ContactModel>(_mapper.Map<ContactModel>(contact));
    }

    public async Task<BaseResponseModel> CreateAsync(ContactEditModel contactEdit, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid contactId, ContactEditModel contactEdit, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> DeleteAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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