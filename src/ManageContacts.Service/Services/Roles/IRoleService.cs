using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Roles;

namespace ManageContacts.Service.Services.Roles;

public interface IRoleService
{
    Task<OkResponseModel<IPagedList<RoleModel>>> GetAllAsync(RoleFilterRequestModel filter, CancellationToken cancellationToken = default);
    Task<OkResponseModel<RoleModel>> GetAsync(Guid roleId,CancellationToken cancellationToken = default);
    Task<BaseResponseModel> CreateAsync(RoleEditModel roleEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> UpdateAsync(Guid roleId, RoleEditModel roleEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> DeleteAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> UndeleteAsync(Guid roleId, CancellationToken cancellationToken = default);
}