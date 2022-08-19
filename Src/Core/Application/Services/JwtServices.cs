using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{

    public static class JwtServices
    {
        /// <summary>
        /// Generates the JWT
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string JwtToken(string UserId, IConfiguration configuration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:KeySecret"])
                );

            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", UserId)
            };

            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(4)
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }


        /// <summary>
        /// Get the user what have send the request by the JWT
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string GetClaimUser(ClaimsPrincipal User)
        {
            try
            {
                var Response = User.Claims.Where(claim => claim.Type == "UserId").FirstOrDefault();
                if (Response == null) { return "Error"; }
                return Response.Value;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
