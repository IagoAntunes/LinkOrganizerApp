using Sitemark.Domain.Dtos;

namespace Sitemark.Application.Services
{
    public interface ILinkService
    {
        Task<Result<CreateLinkResponseDto>> CreateLinkAsync(LinkCreateDto linkCreateDto, Guid userId);
        Task<Result<ICollection<LinkDto>>> GetLinksAsync();
    }
}
