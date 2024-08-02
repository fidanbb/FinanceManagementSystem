using FinanceManagementSystem.Application.Dtos.User;
using FinanceManagementSystem.Application.Helpers.Responses;
using FinanceManagementSystem.Application.Services.Token;
using FinanceManagementSystem.Application.Tools;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Persistence.Services.Token
{
    public class TokenService : ITokenService
    {
        public JwtTokenResponse GenerateJwtToken(CheckAppUserDto result)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, result.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, result.Username)
            };

            result.Roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });


            if (!string.IsNullOrWhiteSpace(result.Username))
            {
                claims.Add(new Claim("NameSurname", result.Name + " " + result.Surname));
                claims.Add(new Claim("UserId", result.Id));

            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience,
                claims: claims, notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signinCredentials);


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new JwtTokenResponse(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
