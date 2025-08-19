

namespace Sitemark.Domain.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(string username, string email, string userId, List<string> roles);
    }
}
