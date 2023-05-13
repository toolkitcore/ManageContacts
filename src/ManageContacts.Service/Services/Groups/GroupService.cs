using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Groups;
using ManageContacts.Service.Abstractions.Core;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ManageContacts.Service.Services.Groups;

public class GroupService : BaseService, IGroupService
{
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<Contact> _contactRepository;
    public GroupService(IUnitOfWork<ContactsContext> uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<GroupService> logger, IWebHostEnvironment env) : base(uow, httpContextAccessor, mapper, logger, env)
    {
        _groupRepository = uow.GetRepository<Group>();
        _contactRepository = uow.GetRepository<Contact>();
    }

    public async Task<OkResponseModel<PaginationList<GroupModel>>> GetAllAsync(GroupFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var groups = await _groupRepository.PagingAllAsync(
            predicate: g => (string.IsNullOrEmpty(filter.SearchString) || (!string.IsNullOrEmpty(filter.SearchString) && g.Name.Contains(filter.SearchString))) 
                            && !g.Deleted
                            && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (groups.NotNullOrEmpty())
            return new OkResponseModel<PaginationList<GroupModel>>(_mapper.Map<PaginationList<GroupModel>>(groups));
        
        return new OkResponseModel<PaginationList<GroupModel>>(new PaginationList<GroupModel>());
    }

    public async Task<OkResponseModel<GroupModel>> GetAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await _groupRepository.GetAsync(
            predicate: g => g.Id == groupId && !g.Deleted && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (group == null)
            throw new NotFoundException("The group is not found.");

        return new OkResponseModel<GroupModel>(_mapper.Map<GroupModel>(group));
    }

    public async Task<BaseResponseModel> CreateAsync(GroupEditModel groupEdit, CancellationToken cancellationToken = default)
    {
        var existGroup = await _groupRepository.GetAsync(
            predicate: g => g.Name == groupEdit.Name && !g.Deleted && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (existGroup != null)
            throw new BadRequestException("Group with the same name already exists.");

        var newGroup = _mapper.Map<Group>(groupEdit);
        newGroup.CreatorId = _currentUserId;
        await _groupRepository.InsertAsync(newGroup, cancellationToken).ConfigureAwait(false);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Create group contact successful.");
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid groupId, GroupEditModel groupEdit, CancellationToken cancellationToken = default)
    {
        var group = await _groupRepository.GetAsync(
            predicate: g =>g.Id == groupId && !g.Deleted && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
            ).ConfigureAwait(false);

        if (group == null)
            throw new BadRequestException("The request is invalid.");
        
        var existGroup = await _groupRepository.GetAsync(
            predicate: g => g.Name == groupEdit.Name && !g.Deleted && g.Id != groupId && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (existGroup != null)
            throw new BadRequestException("Group with the same name already exists.");

        group.Name = groupEdit.Name;
        group.Description = groupEdit.Description;
        
        _groupRepository.Update(group);
        await _uow.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Update group contact successful.");
    }

    public async Task<BaseResponseModel> DeleteAsync(Guid groupId, bool deleteGroupContacts = false, CancellationToken cancellationToken = default)
    {
        var group = await _groupRepository.GetAsync(
            predicate: g =>g.Id == groupId && !g.Deleted && g.User.Id == _currentUserId,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (group == null)
            throw new BadRequestException("The request is invalid.");
        
        _uow.BeginTransaction();
        
        var contacts = await _contactRepository.FindAllAsync(
            predicate: c => c.GroupId == groupId && !c.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (contacts.NotNullOrEmpty())
        {
            foreach (var contact in contacts)
            {
                contact.GroupId = null;
            }
            
            if (deleteGroupContacts)
                await _contactRepository.BulkDeleteAsync(contacts.ToList(), cancellationToken).ConfigureAwait(false);
            else
                await _contactRepository.BulkUpdateAsync(contacts.ToList(), cancellationToken).ConfigureAwait(false);
        }
        
        _groupRepository.Delete(group);
        await _uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        
        return new BaseResponseModel("Delete group contact successful.");
    }
}