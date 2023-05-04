using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Models.Roles;

namespace ManageContacts.Service.Mapping.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEditModel, Role>();
        CreateMap<Role, RoleModel>();
        CreateMap<PagedList<Role>, PagedList<RoleModel>>();
    }
}