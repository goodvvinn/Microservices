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
            if (command == null)
            {
                throw new System.ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                 throw new System.ArgumentNullException(nameof(platform));
            }
            else
            {
                _context.Platforms.Add(platform);
            }
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalID == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
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