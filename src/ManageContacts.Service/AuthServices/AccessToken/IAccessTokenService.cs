using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;

namespace ManageContacts.Service.AuthServices.AccessToken;

public interface IAccessTokenService
{
    AuthorizedResponseModel GenerateJwtToken(UserContextModel auth);
    
    UserContextModel ParseJwtToken(string token);
}