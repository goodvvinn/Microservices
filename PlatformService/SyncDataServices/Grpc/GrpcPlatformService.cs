namespace PlatformService.SyncDataServices.Grpc
{
    using System.Threading.Tasks;
    using AutoMapper;
    using global::Grpc.Core;
    using PlatformService.Data;

    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();
            foreach (var item in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(item));
            }
            
            return Task.FromResult(response);
        }
    }
}