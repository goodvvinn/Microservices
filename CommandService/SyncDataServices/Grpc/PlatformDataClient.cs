namespace CommandService.SyncDataServices.Grpc
{
    using System.Collections.Generic;
    using AutoMapper;
    using CommandService.Models;
    using global::Grpc.Net.Client;
    using Microsoft.Extensions.Configuration;
    using PlatformService;

    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            this._configuration = configuration;
            this._mapper = mapper;
        }

        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            System.Console.WriteLine($" -----> Calling GRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"\n ----->Could not call GRPC Server {ex.Message}\n");
                return null;
            }
        }
    }
}