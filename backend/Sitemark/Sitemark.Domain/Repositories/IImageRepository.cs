using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;

namespace Sitemark.Domain.Repositories
{
    public interface IImageRepository
    {
        Task<Result<ImageEntity>> UploadImageAsync(ImageDto imageDto);
        Task<Result<ImageEntity>> GetImage(Guid imageId);
    }
}
