using AutoMapper;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Models.PhoneNumbers;

namespace ManageContacts.Service.Mapping;

public class PhoneNumberProfile : Profile
{
    public PhoneNumberProfile()
    {
        CreateMap<PhoneNumberEditModel, PhoneNumber>();
        CreateMap<PhoneNumber, PhoneNumberModel>();
    }
}