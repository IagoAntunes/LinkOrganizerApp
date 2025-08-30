using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Sitemark.Application.Services;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sitemark.Infrastructure.Services
{
    internal class LinkService : ILinkService
    {
        private readonly IImageService imageService;
        private readonly ILinkRepository linkRepository;
        private readonly IMapper mapper;
        private readonly IDistributedCache _cache;

        public LinkService(
            IImageService imageService,
            ILinkRepository linkRepository,
            IMapper mapper,
            IDistributedCache cache
            )
        {
            this.imageService = imageService;
            this.linkRepository = linkRepository;
            this.mapper = mapper;
            this._cache = cache;
        }

        public async Task<Result<CreateLinkResponseDto>> CreateLinkAsync(LinkCreateDto linkCreateDto, Guid userId)
        {
            var image = await imageService.UploadImageAsync(linkCreateDto.File, Guid.NewGuid(), userId, linkCreateDto.File.FileName);

            var linkEntity = mapper.Map<LinkEntity>(linkCreateDto);
            linkEntity.UserId = userId.ToString();
            linkEntity.ImageId = image.Id;

            var createLinkResult = await linkRepository.CreateLinkAsync(linkEntity);

            if(createLinkResult.IsSuccess)
            {
                string cacheKey = GetUserLinksCacheKey(userId);
                await _cache.RemoveAsync(cacheKey);
                var linkDto = mapper.Map<CreateLinkResponseDto>(createLinkResult.Value);
                return Result.Success(linkDto);
            }
            else
            {
                return Result.Failure<CreateLinkResponseDto>(createLinkResult.Error);
            }
        }

        private string GetUserLinksCacheKey(Guid userId)
        {
            return $"links:user:{userId}";
        }


        public async Task<Result<ICollection<LinkDto>>> GetLinksAsync(Guid userId)
        {
            string cacheKey = GetUserLinksCacheKey(userId);
            string cachedLinksJson = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedLinksJson))
            {
                var linksCached = JsonSerializer.Deserialize<ICollection<LinkDto>>(cachedLinksJson);
                return Result.Success(linksCached);
            }

            var getLinksResult = await linkRepository.GetLinksAsync(userId);

            if (getLinksResult.IsSuccess)
            {
                var linkDtos = mapper.Map<ICollection<LinkDto>>(getLinksResult.Value);

                var options = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5)) 
                                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                var linksToCacheJson = JsonSerializer.Serialize(linkDtos);
                await _cache.SetStringAsync(cacheKey, linksToCacheJson, options);

                return Result.Success(linkDtos);
            }
            else
            {
                return Result.Failure<ICollection<LinkDto>>(getLinksResult.Error);
            }
        }

        public async Task<Result<LinkDto>> DeleteLinkAsync(Guid linkId)
        {
            var result = await linkRepository.DeleteLinkAsync(linkId);

            if (result.IsSuccess)
            {
                var linkDto = mapper.Map<LinkDto>(result.Value);
                string cacheKey = GetUserLinksCacheKey(Guid.Parse(result.Value.UserId));
                await _cache.RemoveAsync(cacheKey);
                return Result.Success(linkDto);

            }
            else
            {
                return Result.Failure<LinkDto>(result.Error);
            }

        }
    }
}
