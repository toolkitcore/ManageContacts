using AutoMapper;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.PhoneTypes;
using ManageContacts.Shared.Extensions;

namespace ManageContacts.Service.Services.PhoneTypes;

public class PhoneTypeService : IPhoneTypeService
{
    private readonly IRepository<PhoneType> _phoneTypeRepository;
    private readonly IMapper _mapper;
    
    public PhoneTypeService(IUnitOfWork<ContactsContext> uow, IMapper mapper)
    {
        _mapper = _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _phoneTypeRepository = uow.GetRepository<PhoneType>();
    }
    
    public async Task<OkResponseModel<IEnumerable<PhoneTypeModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var phoneTypes = await _phoneTypeRepository.FindAllAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if(phoneTypes.NotNullOrEmpty())
            return new OkResponseModel<IEnumerable<PhoneTypeModel>>(_mapper.Map<IEnumerable<PhoneTypeModel>>(phoneTypes));

        return new OkResponseModel<IEnumerable<PhoneTypeModel>>();
        
    }
}