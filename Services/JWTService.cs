using EmpSubbieWebAPI.Data.Contexts;
using EmpSubbieWebAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EmpSubbieWebAPI.Services
{
    public class JWTService : IJWTService
    {
        // Private Class Members
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;       

        // Constructor
        public JWTService(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;            
        }


        /// <summary>
        /// Generate a JWT Token Response Object for specified User Identity
        /// </summary>
        /// <returns>Response Object containing JWT Token and Expiry Time</returns>
        public async Task<AccessTokenResponse> GenerateJWTokenResponseAsync(ApplicationUser user)
        {
        
            // Set Token Timeouts
            var now = DateTime.UtcNow;
            var dateTimeOffset = new DateTimeOffset(now);
            var unixNow = dateTimeOffset.ToUnixTimeSeconds().ToString();

            // Get the Users Claims / Roles
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, unixNow, ClaimValueTypes.Integer64),
                // new Claim(ClaimTypes.Role, "TestRole"),                
            };

            // Add Roles to Claims
            var roles = await _userService.GetRolesForUserAsync(user);
            foreach (var role in roles) { claims.Add(new Claim(ClaimTypes.Role, role)); }

            var response = new AccessTokenResponse
            {
                AccessToken = GetToken(now, claims),
                ExpiresIn = (int)_appSettings.JWTConfiguration.Expiration,
                Id = user.Id,
                Name = user.Email
            };

            // Serialize and return the response
            return response;
        }

        
        private string GetToken(DateTime now, List<Claim> claims)
        {
            // Create Token Options
            //string secretKey = _appSettings.JWTConfiguration.SecretKey;
            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            //TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToInt32(_appSettings.JWTConfiguration.Expiration));

            // Create the JWT and write it to a string
            //var jwt = new JwtSecurityToken(
            //    issuer: _appSettings.JWTConfiguration.ValidIssuer,
            //    audience: _appSettings.JWTConfiguration.ValidAudience,
            //    claims: claims,
            //    notBefore: now,
            //    expires: now.Add(expiration),
            //    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTConfiguration.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.Add(TimeSpan.FromMinutes(Convert.ToInt32(_appSettings.JWTConfiguration.Expiration))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedJwt = tokenHandler.WriteToken(token);

            return encodedJwt;
        }
    }
}
