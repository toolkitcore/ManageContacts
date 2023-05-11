using AutoMapper;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.AddressTypes;
using ManageContacts.Shared.Extensions;


namespace ManageContacts.Service.Services.AddressTypes;

public class AddressTypeService : IAddressTypeService
{
    private readonly IRepository<AddressType> _addressTypeRepository;
    private readonly IMapper _mapper;
    
    public AddressTypeService(IUnitOfWork<ContactsContext> uow, IMapper mapper)
    {
        _mapper = _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _addressTypeRepository = uow.GetRepository<AddressType>();
    }
    
    public async Task<OkResponseModel<IEnumerable<AddressTypeModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var addressTypes = await _addressTypeRepository.FindAllAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if(addressTypes.NotNullOrEmpty())
            return new OkResponseModel<IEnumerable<AddressTypeModel>>(_mapper.Map<IEnumerable<AddressTypeModel>>(addressTypes));

        return new OkResponseModel<IEnumerable<AddressTypeModel>>();
        
    }
}