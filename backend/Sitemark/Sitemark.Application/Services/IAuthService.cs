using Sitemark.Domain.Dtos;


namespace Sitemark.Application.Services
{
    public interface IAuthService
    {
        Task<Result<string>> Login(AuthLoginDto loginDto);
        Task<Result> Register(AuthRegisterDto registerDto);
    }
}
