using Microsoft.AspNetCore.Http;
using Sitemark.Application.Services;
using System.Security.Claims;

namespace Sitemark.Infrastructure.Services
{
    /// <summary>
    /// Implementação do serviço que obtém o usuário atual a partir do HttpContext.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// O ID do usuário, obtido da claim "NameIdentifier".
        /// </summary>
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// O nome do usuário, obtido da claim "Name".
        /// </summary>
        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    }
}
