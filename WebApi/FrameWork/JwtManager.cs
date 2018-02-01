using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmergencyAccount.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace WebApi.FrameWork
{
    public static class JwtManager
    {
        /// <summary>
        ///     Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken(EntityLoginModel loginInfo, int expireMinutes = 10)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("loginInfo", JsonConvert.SerializeObject(loginInfo))
                        }),

                Expires = now.AddMinutes(expireMinutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        public static EntityLoginModel GetUserLogin(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                if (jwtToken.ValidTo <= DateTime.UtcNow)
                    throw new UnauthorizedAccessException();

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                var userInfo = principal.Claims.FirstOrDefault(x => x.Type == "loginInfo")?.Value;

                return JsonConvert.DeserializeObject<EntityLoginModel>(userInfo);
            }

            catch (Exception)
            {
                throw new UnauthorizedAccessException("token校验未通过");
            }
        }
    }
}
