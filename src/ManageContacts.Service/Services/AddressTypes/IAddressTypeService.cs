using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.AddressTypes;

namespace ManageContacts.Service.Services.AddressTypes;

public interface IAddressTypeService
{
    Task<OkResponseModel<AddressTypeModel>> GetAllAsync(CancellationToken cancellationToken = default);
}