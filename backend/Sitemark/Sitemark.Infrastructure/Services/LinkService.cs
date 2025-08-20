using AutoMapper;
using Sitemark.Application.Services;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Infrastructure.Services
{
    internal class LinkService : ILinkService
    {
        private readonly IImageService imageService;
        private readonly ILinkRepository linkRepository;
        private readonly IMapper mapper;

        public LinkService(
            IImageService imageService,
            ILinkRepository linkRepository,
            IMapper mapper
            )
        {
            this.imageService = imageService;
            this.linkRepository = linkRepository;
            this.mapper = mapper;
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
                var linkDto = mapper.Map<CreateLinkResponseDto>(createLinkResult.Value);
                return Result.Success(linkDto);
            }
            else
            {
                return Result.Failure<CreateLinkResponseDto>(createLinkResult.Error);
            }
        }

        public async Task<Result<ICollection<LinkDto>>> GetLinksAsync()
        {
            var getLinksResult = await linkRepository.GetLinksAsync();
            if (getLinksResult.IsSuccess)
            {
                var linkDtos = mapper.Map<ICollection<LinkDto>>(getLinksResult.Value);
                return Result.Success(linkDtos);
            }
            else
            {
                return Result.Failure<ICollection<LinkDto>>(getLinksResult.Error);
            }
        }
    }
}
