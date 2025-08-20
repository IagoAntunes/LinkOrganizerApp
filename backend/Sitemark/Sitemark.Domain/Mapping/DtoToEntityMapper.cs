
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sitemark.Domain.Dtos;
using Sitemark.Domain.Entities;

namespace Sitemark.Domain.Mapping
{
    public class DtoToEntityMapper : Profile
    {

        public DtoToEntityMapper()
        {
            CreateMap<LinkCreateDto, LinkEntity>().ReverseMap();
            CreateMap<LinkDto, LinkEntity>().ReverseMap();
            CreateMap<LinkEntity, CreateLinkResponseDto>().ReverseMap();
            CreateMap<ImageDto, ImageEntity>().ReverseMap();
            CreateMap<IdentityUser, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }

    }
}
