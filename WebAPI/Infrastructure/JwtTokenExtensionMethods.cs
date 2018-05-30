using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Core.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Infrastructure
{
    public static class JwtTokenExtensionMethods
    {
        public static string GenerateJwtToken(this ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IocContainer.Configuration["JWTAuth:SecurityKey"])),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: IocContainer.Configuration["JWTAuth:Issuer"],
                audience: IocContainer.Configuration["JWTAuth:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
