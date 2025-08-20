using Microsoft.AspNetCore.Http;
using Sitemark.Domain.Dtos;

namespace Sitemark.Application.Services
{
    public interface IImageService
    {
        Task<ImageDto?> UploadImageAsync(IFormFile file, Guid id, Guid userId, string name);
        Task<ImageDto?> GetImage(Guid idImage);
    }
}
