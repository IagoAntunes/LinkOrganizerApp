using Microsoft.AspNetCore.Identity;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;

namespace Sitemark.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<string>> Login(AuthLoginDto loginDto);
        Task<Result> Register(AuthRegisterDto registerDto);
        Task<Result<IdentityUser>> GetUserInfo(Guid userId);
    }
}
