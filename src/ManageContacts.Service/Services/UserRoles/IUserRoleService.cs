using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.UserRoles;

namespace ManageContacts.Service.Services.UserRoles;

public interface IUserRoleService
{
    Task<BaseResponseModel> AddOrUpdateUserRoleAsync(Guid userId, UserRoleEditModel userRoleEdit, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> RemoveUserRoleAsync(Guid userId, CancellationToken cancellationToken = default);
}