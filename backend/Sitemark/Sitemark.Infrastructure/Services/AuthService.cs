using Sitemark.Domain.Dtos;
using Sitemark.Domain.Repositories;

namespace Sitemark.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<Result<string>> Login(AuthLoginDto loginDto)
        {
            return await authRepository.Login(loginDto);
        }

        public async Task<Result> Register(AuthRegisterDto registerDto)
        {
            return await authRepository.Register(registerDto);
        }
    }
}
