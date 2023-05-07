using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.RelativeTypes;

namespace ManageContacts.Service.Services.RelativeTypes;

public interface IRelativeTypeService
{
    Task<OkResponseModel<RelativeTypeModel>> GetAllAsync(CancellationToken cancellationToken = default);
}