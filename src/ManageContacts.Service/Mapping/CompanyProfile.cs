using AutoMapper;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Models.Companies;
using ManageContacts.Model.Models.Contacts;

namespace ManageContacts.Service.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyEditModel, Company>();
        CreateMap<Company, CompanyModel>();
    }
}