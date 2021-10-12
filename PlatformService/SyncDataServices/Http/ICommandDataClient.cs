namespace PlatformService.SyncDataServices.Http
{
    using System.Threading.Tasks;
    using PlatformService.Dto;

    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platform);
    }
}