using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Globalization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common;

namespace FRCSPreparationPortal.API
{
    public static class Jwt
    {
        public const string tenantId = "http://schemas.microsoft.com/identity/claims/tenantid";
        /// <summary>
        /// Create new JWT token for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Create(Users user)
        {
            // creating claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            };

            claims.Add(new Claim("FirstName", user.FirstName));
            foreach (var roles in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, ((Common.Roles)roles.RoleId).ToString()));
            }
            claims.Add(new Claim("uid", user.Id.ToString()));

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.JwtSigningKey));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.UtcNow.AddHours(3),
                            signingCredentials: signingCredentials
                        );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JwtSecurityToken Read(string token)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.JwtSigningKey));

            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey
            };

            JwtSecurityToken validatedToken;

            try
            {
                validatedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            }
            catch (Exception)
            {
                return null;
            }

            return validatedToken;
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            int userId;
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            int.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return userId;
        }
    }
}
