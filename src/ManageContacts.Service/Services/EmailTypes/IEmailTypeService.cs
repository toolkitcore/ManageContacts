using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.EmailTypes;

namespace ManageContacts.Service.Services.EmailTypes;

public interface IEmailTypeService
{
    Task<OkResponseModel<IEnumerable<EmailTypeModel>>> GetAllAsync(CancellationToken cancellationToken = default);
}