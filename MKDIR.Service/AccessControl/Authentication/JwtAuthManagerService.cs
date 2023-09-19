using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Service.AccessControl.Authentication
{
    public class JwtAuthManagerService : IJwtAuthManagerService
    {
        private readonly GeneralConfiguration _configuration;

        public JwtAuthManagerService(IOptions<GeneralConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task<string> GetTokenAsync(BusinessUser user)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecret));
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //            //new Claim(ClaimTypes.Sid, user.BusinessUserId.ToString()),
            //            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.SureName}"),
            //            new Claim(ClaimTypes.Email, user.Email),
            //            //new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(user.Empresas))
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(_configuration.JwtTokenExpiration),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            //};

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.SureName}"),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(_configuration.JwtTokenExpiration);

            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,             
            expires: expiration,
            signingCredentials: creds
            );

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
