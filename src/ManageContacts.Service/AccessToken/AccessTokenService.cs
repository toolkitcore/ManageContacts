using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageContacts.Model.Abstractions;
using ManageContacts.Model.Users;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageContacts.Service.AccessToken;

public class AccessTokenService : IAccessTokenService
{
    private readonly JwtSetting _jwtSetting;

    public AccessTokenService(IConfiguration configuration)
    {
        _jwtSetting = configuration.GetOptions<JwtSetting>() ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    public AuthorizedResponseModel GenerateJwtToken(UserContextModel auth)
    {
        var refreshToken = Guid.NewGuid().ToString();
        var issuedTime = DateTime.UtcNow;
        var expiredTime = issuedTime.AddMinutes(_jwtSetting.ExpiredMinute);
        
        var claims = new ClaimsIdentity(new[]
        {
            new Claim("UserId", auth.Id),
            new Claim("Email", auth.Email)
        });
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Audience = _jwtSetting.Audience,
            Issuer = _jwtSetting.Issuer,
            Expires = expiredTime,
            IssuedAt = issuedTime,
        };
        return default;
    }

    public UserContextModel ParseJwtToken(string token)
    {
        throw new NotImplementedException();
    }
}