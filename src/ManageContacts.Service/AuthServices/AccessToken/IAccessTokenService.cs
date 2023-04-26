using ManageContacts.Model.Abstractions;
using ManageContacts.Model.Users;

namespace ManageContacts.Service.AuthServices.AccessToken;

public interface IAccessTokenService
{
    AuthorizedResponseModel GenerateJwtToken(UserContextModel auth);
    
    UserContextModel ParseJwtToken(string token);
}