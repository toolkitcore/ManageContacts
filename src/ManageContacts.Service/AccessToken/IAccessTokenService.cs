using ManageContacts.Model.Abstractions;
using ManageContacts.Model.Users;

namespace ManageContacts.Service.AccessToken;

public interface IAccessTokenService
{
    AuthorizedResponseModel GenerateJwtToken(UserContextModel auth);
    UserContextModel ParseJwtToken(string token);
}