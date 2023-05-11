using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.PhoneTypes;

namespace ManageContacts.Service.Services.PhoneTypes;

public interface IPhoneTypeService
{
    Task<OkResponseModel<IEnumerable<PhoneTypeModel>>> GetAllAsync(CancellationToken cancellationToken = default);
}