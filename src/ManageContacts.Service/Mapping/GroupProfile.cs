using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.Groups;

namespace ManageContacts.Service.Mapping;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<GroupEditModel, Group>();
        CreateMap<Group, GroupModel>();
        CreateMap<IPagedList<Group>, PaginationList<GroupModel>>();
    }
}