namespace CommandService.EventProcessing
{
    using System.Text.Json;
    using AutoMapper;
    using CommandService.Data;
    using CommandService.Dtos;
    using CommandService.Models;
    using Microsoft.Extensions.DependencyInjection;

    public enum EventType
    {
        PlatformPublished,
        Undetermined
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:

                break;
                default:
                break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            System.Console.WriteLine("----> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Platform_Published":
                System.Console.WriteLine("\n----->Platform Published Event Detected\n");
                return EventType.PlatformPublished;
                default:
                System.Console.WriteLine("\n----> Could not determine \n");
                return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage) 
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformExists(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        System.Console.WriteLine($"\n---> Platform already exists ");
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"\n---> Could not add platform to DB {ex.Message}\n");
                }
            }
        }
    }
}