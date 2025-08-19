using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using System.Data;

namespace Sitemark.Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthRepository(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository
            )
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task<Result<string>> Login(AuthLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user != null)
            {
                var passwordIsCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordIsCorrect)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user.UserName, user.Email, user.Id, roles.ToList());
                        return Result.Success(jwtToken);
                    }
                }
            }

            return Result<string>.Failure(
                new Error(
                    "error-login",
                    ""
                )    
            );
        }

        public async Task<Result> Register(AuthRegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Result.Success();
                    }
                    else
                    {
                        return Result.Failure(
                            new Error(
                                "Auth.Register.RolesFailed",
                                string.Join("\n", identityResult.Errors.Select(e => e.Description))
                            )
                        );
                    }


                }
            }
            return Result.Failure(
                new Error(
                "error-register",
                ""
            ));
        }
    }
}
