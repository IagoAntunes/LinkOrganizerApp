using AutoMapper;
using Sitemark.API.Dtos.Requests;
using Sitemark.Domain.Dtos;

namespace Sitemark.API.Mapping
{
    public class RequestToDtoMapper : Profile
    {
        public RequestToDtoMapper()
        {
            CreateMap<AuthRegisterRequest, AuthRegisterDto>();
            CreateMap<AuthLoginRequest, AuthRegisterDto>();
        }
    }
}
