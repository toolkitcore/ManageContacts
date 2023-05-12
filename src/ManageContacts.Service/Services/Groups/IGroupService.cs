using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Groups;

namespace ManageContacts.Service.Services.Groups;

public interface IGroupService
{
    Task<OkResponseModel<PaginationList<GroupModel>>> GetAllAsync(GroupFilterRequestModel filter, CancellationToken cancellationToken = default);
    Task<OkResponseModel<GroupModel>> GetAsync(Guid groupId,CancellationToken cancellationToken = default);
    Task<BaseResponseModel> CreateAsync(GroupEditModel groupEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> UpdateAsync(Guid groupId, GroupEditModel groupEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> DeleteAsync(Guid groupId, bool deleteGroupContacts = false, CancellationToken cancellationToken = default);
}