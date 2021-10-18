namespace PlatformService.AsyncDataServices
{
    using PlatformService.Dto;

    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}