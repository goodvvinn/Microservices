namespace PlatformService.Profiles
{
    using AutoMapper;
    using PlatformService.Dto;
    using PlatformService.Model;

    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}