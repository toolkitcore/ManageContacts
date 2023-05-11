using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.EmailTypes;

namespace ManageContacts.Service.Mapping;

public class EmailTypeProfile : Profile
{
    public EmailTypeProfile()
    {
        CreateMap<EmailType, EmailTypeModel>();
    }
}