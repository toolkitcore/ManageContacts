using AutoMapper;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.EmailTypes;
using ManageContacts.Shared.Extensions;

namespace ManageContacts.Service.Services.EmailTypes;

public class EmailTypeService : IEmailTypeService
{
    private readonly IRepository<EmailType> _emailTypeRepository;
    private readonly IMapper _mapper;
    
    public EmailTypeService(IUnitOfWork<ContactsContext> uow, IMapper mapper)
    {
        _mapper = _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _emailTypeRepository = uow.GetRepository<EmailType>();
    }
    
    public async Task<OkResponseModel<IEnumerable<EmailTypeModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var addressTypes = await _emailTypeRepository.FindAllAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if(addressTypes.NotNullOrEmpty())
            return new OkResponseModel<IEnumerable<EmailTypeModel>>(_mapper.Map<IEnumerable<EmailTypeModel>>(addressTypes));

        return new OkResponseModel<IEnumerable<EmailTypeModel>>();
        
    }
}