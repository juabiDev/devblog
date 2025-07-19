using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesContracts.DTOs;
using ServicesContracts.ServicesContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponse CreateJwtToken(LoginRequest user)
        {
           DateTime expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:EXPIRATION_HOURS"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email.ToString()), // Subject (user id)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique ID
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at (date time and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()), // Unique name identifier of the user (Email)

            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])    
            );

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                Token = token,
                Expiration = expiration,
                Email = user.Email
            };
        }

        public AuthenticationResponse CreateJwtToken(UserDTO user)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:EXPIRATION_HOURS"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email.ToString()), // Subject (user id)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique ID
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at (date time and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()), // Unique name identifier of the user (Email)

            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                Token = token,
                Expiration = expiration,
                Email = user.Email
            };
        }


    }
}
