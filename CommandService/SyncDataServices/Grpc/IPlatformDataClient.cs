namespace CommandService.SyncDataServices.Grpc
{
    using System.Collections.Generic;
    using CommandService.Models;

    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}