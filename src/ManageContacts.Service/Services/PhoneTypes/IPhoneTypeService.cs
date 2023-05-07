using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.PhoneTypes;

namespace ManageContacts.Service.Services.PhoneTypes;

public interface IPhoneTypeService
{
    Task<OkResponseModel<PhoneTypeModel>> GetAllAsync(CancellationToken cancellationToken = default);
}