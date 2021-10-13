namespace CommandService.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using CommandService.Models;

    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            this._context = context;
        }

        public void CreateCommand(int platformId, Command command)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePlatform(Platform platform)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            throw new System.NotImplementedException();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .Where(p => p.PlatformId == platformId && p.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(p => p.PlatformId == platformId)
                .OrderBy(p => p.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}