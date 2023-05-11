using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Contacts;

namespace ManageContacts.Service.Services.Contacts;

public interface IContactService
{
    Task<OkResponseModel<PaginationList<ContactModel>>> GetAllAsync(ContactFilterRequestModel filter,
        CancellationToken cancellationToken = default);
    Task<OkResponseModel<ContactModel>> GetAsync(Guid contactId, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> CreateAsync(ContactEditModel contactEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> UpdateAsync(Guid contactId, ContactEditModel contactEdit, CancellationToken cancellationToken = default);
    Task<BaseResponseModel> DeleteAsync(Guid contactId, CancellationToken cancellationToken = default);
    
}