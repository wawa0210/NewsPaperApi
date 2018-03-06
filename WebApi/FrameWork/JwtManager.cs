using CommonLib;
using EmergencyAccount.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.FrameWork
{
    public class JwtManager
    {
        public string GenerateJwtToken(EntityAccountManager entityAccount)
        {
            var claims = new[]
            {
                  new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(entityAccount))
            };
            var issure = ConfigurationHelper.GetInstance().GetSection("Jwt").GetSection("issure").Value;
            var audience = ConfigurationHelper.GetInstance().GetSection("Jwt").GetSection("audience").Value;
            var securitykey = ConfigurationHelper.GetInstance().GetSection("Jwt").GetSection("securitykey").Value;
            var expireseconds = ConfigurationHelper.GetInstance().GetSection("Jwt").GetSection("expireseconds").Value.ToInt32(1800);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issure,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(expireseconds),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
