using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;

namespace ManageContacts.Service.Services.Users;

public interface IUserService
{
    Task<OkResponseModel<IPagedList<UserModel>>> GetAllAsync(UserFilterRequestModel filter,
        CancellationToken cancellationToken = default);

    Task<OkResponseModel<UserModel>> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    
    Task<BaseResponseModel> DeleteAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UndeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UndeleteUsersAsync(IEnumerable<Guid> listOfUserIds, CancellationToken cancellationToken);
    
    Task<BaseResponseModel> SignUpAsync(UserRegistrationModel registerUser, CancellationToken cancellationToken = default);
    
    Task<AuthorizedResponseModel> SignInAsync(UserLoginModel loginUser, CancellationToken cancellationToken = default);
    
    Task<AuthorizedResponseModel> RefreshTokenAsync(CancellationToken cancellationToken = default);
    
    Task<BaseResponseModel> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    
    Task<BaseResponseModel> ConfirmEmailAsync(Guid userId, string code, CancellationToken cancellationToken = default);
    
    Task<OkResponseModel<UserProfileModel>> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel,
        CancellationToken cancellationToken = default);
    
    Task<BaseResponseModel> UpdateProfileAsync(UserProfileEditModel userProfile,CancellationToken cancellationToken = default);
}