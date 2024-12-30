using Application.Models.Login;
using Application.Services.Intefaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IJWTService _jwtService;
        private readonly IUserRepository _userRepository;

        public LoginService(IJWTService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<string?> Login(LoginRequestModel request)
        {
            var loginSuccess = await _userRepository.LoginAsync(request.UserName, request.Password);

            if (loginSuccess) { return await _jwtService.GenerateToken(request.UserName); }

            return null;

        }


    }
}
