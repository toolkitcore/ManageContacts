using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Roles;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace ManageContacts.Service.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork<ContactsContext> _unitOfWork;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;
    private readonly Guid _currentUserId;
    public RoleService(IUnitOfWork<ContactsContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = _unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _roleRepository = unitOfWork.GetRepository<Role>();
        _userRoleRepository = unitOfWork.GetRepository<UserRole>();
        _currentUserId = httpContextAccessor.GetCurrentUserId();
    }
    public async Task<OkResponseModel<IPagedList<RoleModel>>> GetAllAsync(RoleFilterRequestModel filter, CancellationToken cancellationToken = default)
    {
        var roles = await _roleRepository.PagingAllAsync(
            predicate: u => (string.IsNullOrEmpty(filter.SearchString) 
                            || (!string.IsNullOrEmpty(filter.SearchString) && u.Name.Contains(filter.SearchString))
                            || (string.IsNullOrEmpty(u.Description) || !string.IsNullOrEmpty(u.Description) && (u.Description.Contains(filter.SearchString))))
                            && u.Deleted == filter.Deleted,
            orderBy: u => u.OrderByDescending(x => x.CreatedTime),
            pageIndex: filter.PageIndex,
            pageSize: filter.PageSize,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        return new OkResponseModel<IPagedList<RoleModel>>(_mapper.Map<PagedList<RoleModel>>(roles));
    }

    public async Task<OkResponseModel<RoleModel>> GetAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetAsync(
            predicate: r => r.Id == roleId && !r.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (role == null)
            throw new BadRequestException("The request is invalid.");

        return new OkResponseModel<RoleModel>(_mapper.Map<RoleModel>(role));
    }

    public async Task<BaseResponseModel> CreateAsync(RoleEditModel roleEdit, CancellationToken cancellationToken = default)
    {
        var existRole = await _roleRepository.GetAsync(
            predicate: r => r.Name == roleEdit.Name && !r.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if(existRole != null)
            throw new BadRequestException("Role with the same name already exists.");
        
        var newRole = _mapper.Map<Role>(roleEdit);
        newRole.CreatorId = _currentUserId;
        await _roleRepository.InsertAsync(newRole, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Create role successful.");
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid roleId, RoleEditModel roleEdit, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetAsync(
            predicate: r => r.Id == roleId && !r.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (role == null)
            throw new BadRequestException("The request is invalid.");
        
        var existRole = await _roleRepository.GetAsync(
            predicate: r => r.Name == roleEdit.Name && !r.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if(existRole != null)
            throw new BadRequestException("Role with the same name already exists.");
        
        role.Name = roleEdit.Name;
        role.Description = roleEdit.Description;
        role.ModifierId = _currentUserId;
        
        _roleRepository.Update(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new BaseResponseModel("Updated role successfully");

    }

    public async Task<BaseResponseModel> DeleteAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetAsync(
            predicate: r => r.Id == roleId && !r.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        
        if (role == null)
            throw new BadRequestException("The request is invalid.");

        var urs = await _userRoleRepository.FindAllAsync(
            predicate: ur => ur.RoleId == roleId && !ur.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        try
        {
            _unitOfWork.BeginTransaction();
            _roleRepository.Delete(role);
            await _userRoleRepository.UpdateAsync(urs.ToList(), cancellationToken).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw new BadRequestException("Delete role failure.");
        }
        
        return new BaseResponseModel("Deleted role successful");
    }

    public async Task<BaseResponseModel> UndeleteAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetAsync(
            predicate: r => r.Id == roleId && r.Deleted,
            cancellationToken: cancellationToken,
            disableTracking: true
        ).ConfigureAwait(false);
        
        if (role == null)
            throw new BadRequestException("The request is invalid.");
        
        if(role.DeletedTime != null && (DateTime.UtcNow - role.DeletedTime.Value).Days > 30)
            throw new BadRequestException("Records deleted more than 30 days old cannot be recovered.");
        
        var urs = await _userRoleRepository.FindAllAsync(
            predicate: ur => ur.RoleId == roleId && ur.Deleted,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        role.Deleted = false;
        foreach (var ur in urs)
        {
            ur.Deleted = false;
        }
        
        _roleRepository.Update(role);
        await _userRoleRepository.UpdateAsync(urs.ToList(), cancellationToken).ConfigureAwait(false);
        await _roleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return new BaseResponseModel("Undeleted role successful");
    }
}