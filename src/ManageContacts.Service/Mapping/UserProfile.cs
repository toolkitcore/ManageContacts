using AutoMapper;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Paginations;
using ManageContacts.Model.Models.Roles;
using ManageContacts.Model.Models.Users;
using ManageContacts.Shared.Helper;

namespace ManageContacts.Service.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.PasswordSalt = CryptoHelper.GenerateKey();
                dest.PasswordHashed = CryptoHelper.Encrypt(src.Password, dest.PasswordSalt);
                dest.CreatedTime = DateTime.UtcNow;
            });

        CreateMap<UserEditModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.PasswordSalt = CryptoHelper.GenerateKey();
                dest.PasswordHashed = CryptoHelper.Encrypt(src.Password, dest.PasswordSalt);
            });
        
        CreateMap<User, UserProfileModel>()
            .AfterMap((src, dest) =>
            {
                dest.Roles = src.UserRoles.Select(ur => new RoleModel()
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description,
                }).ToList();
            });
        
        CreateMap<User, UserModel>();
        CreateMap<IPagedList<User>, PaginationList<UserModel>>();

    }
}