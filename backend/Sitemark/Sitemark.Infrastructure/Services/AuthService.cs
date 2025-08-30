using AutoMapper;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Repositories;

namespace Sitemark.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
        }

        public async Task<Result<UserDto>> GetUserInfo(Guid uerId)
        {
            var result = await authRepository.GetUserInfo(uerId);
            if(result.IsSuccess && result.Value != null)
            {
                var userDto = mapper.Map<UserDto>(result.Value);
                return Result<UserDto>.Success(userDto);
            }
            return Result<UserDto>.Failure(
                new Error(
                    "error-user-not-found",
                    ""
                )
            );
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
