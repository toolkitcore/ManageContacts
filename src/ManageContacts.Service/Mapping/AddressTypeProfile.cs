using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.AddressTypes;

namespace ManageContacts.Service.Mapping;

public class AddressTypeProfile : Profile
{
    public AddressTypeProfile()
    {
        CreateMap<AddressType, AddressTypeModel>();
    }
}