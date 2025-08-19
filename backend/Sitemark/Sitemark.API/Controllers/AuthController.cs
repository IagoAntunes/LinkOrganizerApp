using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitemark.API.Dtos.Requests;
using Sitemark.Application.Services;
using Sitemark.Domain.Dtos;

namespace Sitemark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authSevice;
        private readonly IMapper mapper;

        public AuthController(
            IAuthService authSevice,
            IMapper mapper
        )
        {
            this.authSevice = authSevice;
            this.mapper = mapper;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            var registerDto = mapper.Map<AuthRegisterDto>(request);
            var result = await authSevice.Register(registerDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
        {
            var loginDto = mapper.Map<AuthLoginDto>(request);
            var result = await authSevice.Login(loginDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
