using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AutoSync.API.Options
{
    public class TokenHelper
    {
        public string CreateToken(JwtIssuerOptions _jwtOptions, IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               notBefore: _jwtOptions.NotBefore,
               expires: _jwtOptions.Expiration,
               signingCredentials: _jwtOptions.SigningCredentials,
               claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public bool ValidateToken(JwtIssuerOptions _jwtOptions, string token, out long userId)
        {
            ClaimsPrincipal cPrinc;
            List<Claim> claims;
            userId = 0;
            try
            {
                var valparam = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,

                };
                cPrinc = new JwtSecurityTokenHandler().ValidateToken(token, valparam, out SecurityToken validatedToken);
                claims = cPrinc.Claims.ToList();
                if (!long.TryParse(claims[0].Value, out userId))
                {
                    return false;
                }
                return true;
            }

            catch (SecurityTokenException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
