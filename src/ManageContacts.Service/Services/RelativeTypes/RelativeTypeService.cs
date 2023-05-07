using AutoMapper;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Entities;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.RelativeTypes;
using ManageContacts.Shared.Extensions;

namespace ManageContacts.Service.Services.RelativeTypes;

public class RelativeTypeService : IRelativeTypeService
{
    private readonly IRepository<RelativeType> _addressTypeRepository;
    private readonly IMapper _mapper;
    
    public RelativeTypeService(IUnitOfWork<ContactsContext> uow, IMapper mapper)
    {
        _mapper = _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _addressTypeRepository = uow.GetRepository<RelativeType>();
    }
    
    public async Task<OkResponseModel<RelativeTypeModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var relativeTypes = await _addressTypeRepository.FindAllAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if(relativeTypes.NotNullOrEmpty())
            return new OkResponseModel<RelativeTypeModel>(_mapper.Map<RelativeTypeModel>(relativeTypes));

        return new OkResponseModel<RelativeTypeModel>();
        
    }
}