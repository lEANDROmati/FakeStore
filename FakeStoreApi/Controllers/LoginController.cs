using Application.Models.Login;
using Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequest)
        {
            var token = await _loginService.Login(loginRequest);

            if (token != null)
            {
                return Ok(new { Token = token });
            }

            return BadRequest(new { message = "El nombre de usuario o la contraseña son incorrectos." });
        }

    }
}
