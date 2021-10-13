namespace CommandService.Profiles
{
    using AutoMapper;
    using CommandService.Dtos;
    using CommandService.Models;

    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
        }
    }
}