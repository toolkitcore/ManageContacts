using System.Net;
using Newtonsoft.Json;

namespace ManageContacts.Model.Abstractions.Responses;

public class AuthorizedResponseModel : BaseResponseModel
{
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty(PropertyName = "expired_in")]
    public long? ExpiredIn { get; set; }
    
    [JsonProperty(PropertyName = "refresh_token")]
    public string RefreshToken { get; set; }
    
    public string TokenType => "bearer";

    public AuthorizedResponseModel() { }

    public AuthorizedResponseModel(string errorMessage)
    {
        ErrorMessage = errorMessage;
        StatusCode = HttpStatusCode.Unauthorized;
    }

    public AuthorizedResponseModel(string accessToken, string refreshToken, DateTime issuedTime, DateTime expiredTime)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiredIn = null; // handle
        StatusCode = HttpStatusCode.OK;
    }
}