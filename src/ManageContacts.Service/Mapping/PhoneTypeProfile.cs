using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.PhoneTypes;

namespace ManageContacts.Service.Mapping;

public class PhoneTypeProfile : Profile
{
    public PhoneTypeProfile()
    {
        CreateMap<PhoneType, PhoneTypeModel>();
    }
}