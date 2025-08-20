using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Sitemark.Application.Services;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using System;

namespace Sitemark.Infrastructure.Services
{
    internal class ImageService : IImageService
    {
        private readonly IImageRepository repository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public ImageService(
            IImageRepository repository,
            IMapper mapper,
            IWebHostEnvironment environment
            )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.environment = environment;
        }

        public async Task<ImageDto?> GetImage(Guid idImage)
        {
            var resultGetImage = await repository.GetImage(idImage);
            if (resultGetImage.IsFailure)
            {
                return null;
            }
            var imageDto = mapper.Map<ImageDto>(resultGetImage.Value);
            return imageDto;
        }

        public async Task<ImageDto?> UploadImageAsync(IFormFile file, Guid id, Guid userId, string name)
        {
            var idImage = Guid.NewGuid();
            var filePath = await SaveFileToDiskAsync(file, idImage);
            if (filePath == null)
            {
                return null;
            }

            var imageDto = new ImageDto
            {
                Id = idImage,
                UserId = userId,
                Name = name,
                FilePath = filePath,
                ContentType = file.ContentType,
            };
            var resultUploadImage = await repository.UploadImageAsync(imageDto);
            if (resultUploadImage.IsFailure)
            {
                return null;
            }

            var createdImageDto = mapper.Map<ImageDto>(resultUploadImage.Value);
            return createdImageDto;
        }

        private async Task<string?> SaveFileToDiskAsync(IFormFile file, Guid idImage)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(environment.ContentRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var storedFileName = $"{idImage}{fileExtension}";

            var filePath = Path.Combine(uploadsFolder, storedFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("Images", storedFileName);
        }
    }
}
