using AutoMapper;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Models.Users;
using ManageContacts.Shared.Helper;
using Microsoft.Extensions.Configuration;

namespace ManageContacts.Service.Mapping.Users;

public class UserProfile : Profile
{
    public UserProfile(IConfiguration configuration)
    {
        CreateMap<UserRegistrationModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.UserId = Guid.NewGuid();
                dest.PasswordSalt = CryptoHelper.GenerateKey();
                dest.PasswordHashed = CryptoHelper.Encrypt(src.Password, dest.PasswordSalt);
                dest.CreatedTime = DateTime.UtcNow;
            });

        CreateMap<User, UserProfileModel>();
    }
}