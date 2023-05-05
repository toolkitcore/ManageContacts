using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.Roles;

namespace ManageContacts.Service.Mapping;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEditModel, Role>();
        CreateMap<Role, RoleModel>();
        CreateMap<IPagedList<Role>, PaginationList<RoleModel>>();
    }
}