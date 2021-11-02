using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OngProject.Application.Interfaces.Identity;

namespace OngProject.DataAccess.Identity
{
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly JwtConfig _jwtConfig;
        
        public TokenHandlerService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        
        public string GenerateJwtToken(ITokenParameters parameters)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", parameters.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, parameters.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, parameters.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}