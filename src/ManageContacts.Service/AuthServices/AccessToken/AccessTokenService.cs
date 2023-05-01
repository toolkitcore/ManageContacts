using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageContacts.Service.AuthServices.AccessToken;

public class AccessTokenService : IAccessTokenService
{
    private readonly JwtSetting jwtSetting;
    public AccessTokenService(IConfiguration configuration)
    {
        jwtSetting = configuration.GetOptions<JwtSetting>() ?? throw new ArgumentNullException(nameof(configuration));
    }
    public AuthorizedResponseModel GenerateJwtToken(UserContextModel auth)
    {
        var refreshToken = Guid.NewGuid().ToString();
        var issuedTime = DateTime.UtcNow;
        var expiredTime = issuedTime.AddMinutes(jwtSetting.ExpiredMinute);
        var claims = new ClaimsIdentity(new[]
        {
            new Claim("UserId", auth.UserId),
            new Claim("UserName", auth.UserName),
            new Claim("Email", auth.Email)
        });
        
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Audience = jwtSetting.Audience,
            Issuer = jwtSetting.Issuer,
            Expires = expiredTime,
            IssuedAt = issuedTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Key)), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var accessToken = jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescriptor));

        return new AuthorizedResponseModel(accessToken, refreshToken, issuedTime, expiredTime);
        
    }

    public UserContextModel ParseJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSetting.Key);
        tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience
            
        }, out SecurityToken validatedToken);
        
        var jwtToken = (JwtSecurityToken)validatedToken;

        var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
        var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;

        return new UserContextModel()
        {
            UserId = userId,
            UserName = userName,
            Email = email
        };
        
    }
}