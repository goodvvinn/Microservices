namespace PlatformService.Data
{
    using System.Collections.Generic;
    using PlatformService.Model;

    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);

        void CreatePlatform(Platform platform);
    }
}