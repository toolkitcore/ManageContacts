using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageContacts.Entity.Entities;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Users;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageContacts.Service.AuthServices.AccessToken;

public static class AccessTokenService 
{
    public static AuthorizedResponseModel GenerateJwtToken(User user, JwtSetting jwtSetting)
    {
        var refreshToken = Guid.NewGuid().ToString();
        var issuedTime = DateTime.UtcNow;
        var expiredTime = issuedTime.AddMinutes(jwtSetting.ExpiredMinute);
        var claims = new ClaimsIdentity(new[]
        {
            new Claim("UserId", user.UserId.ToString()),
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email)
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
    
}