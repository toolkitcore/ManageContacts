using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.Contacts;

namespace ManageContacts.Service.Mapping;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactEditModel, Contact>()
            .AfterMap((src, dest) =>
            {
 
            });
        CreateMap<Contact, ContactModel>();
        CreateMap<IPagedList<Contact>, PaginationList<ContactModel>>();
    }
}