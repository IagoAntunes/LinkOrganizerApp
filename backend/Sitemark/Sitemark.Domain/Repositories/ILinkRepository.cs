using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;

namespace Sitemark.Domain.Repositories
{
    public interface ILinkRepository
    {
        Task<Result<LinkEntity>> CreateLinkAsync(LinkEntity link);
        Task<Result<List<LinkEntity>>> GetLinksAsync(Guid userId);
        Task<Result<LinkEntity>> DeleteLinkAsync(Guid linkId);

    }
}
