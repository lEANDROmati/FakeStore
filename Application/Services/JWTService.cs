using Application.Services.Intefaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JWTService:IJWTService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public JWTService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public  async Task<string> GenerateToken(string userName)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string key = _configuration["Jwt:Key"];
            string issuer = _configuration["Jwt:Issuer"];
            string audiencia = _configuration["Jwt:Audience"];

            UserEntity user = await _userRepository.GetByUserNameAsync(userName);

            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                }),
                Expires = System.DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audiencia
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenResponse = tokenHandler.WriteToken(token);

            return tokenResponse;

        }
    }
}
