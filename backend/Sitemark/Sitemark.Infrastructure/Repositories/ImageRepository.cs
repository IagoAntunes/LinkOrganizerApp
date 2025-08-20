using Microsoft.EntityFrameworkCore;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using Sitemark.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Infrastructure.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        private readonly SitemarkDbContext dbContext;

        public ImageRepository(SitemarkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<ImageEntity>> GetImage(Guid imageId)
        {
            var imageFinded = await dbContext.Images.FirstOrDefaultAsync(x => x.Id == imageId);
            if(imageFinded == null)
            {
                return Result<ImageEntity>.Failure(
                    new Error(
                        "error-get-image",
                        ""
                    )
                );
            }
            else
            {
                return Result.Success<ImageEntity>(imageFinded);
            }
        }

        public async Task<Result<ImageEntity>> UploadImageAsync(ImageDto imageDto)
        {
            var imageEntity = new ImageEntity
            {
                Id = imageDto.Id,
                OriginalFileName = imageDto.Name,
                FilePath = imageDto.FilePath,
                ContentType = imageDto.ContentType,
            };

            var image = await dbContext.Images.AddAsync(imageEntity);
            await dbContext.SaveChangesAsync();

            if (image == null)
            {
                return Result<ImageEntity>.Failure(
                    new Error(
                        "error-update-image",
                        ""
                    )
                );
            }

            return imageEntity;
        }
    }
}
