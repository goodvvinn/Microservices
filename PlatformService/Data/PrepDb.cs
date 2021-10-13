namespace PlatformService.Data
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using PlatformService.Model;

    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd) 
        {
            if (isProd)
            {
                System.Console.WriteLine("------> Attempting to apply migration");
                try
                {
                     context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"-----> Could not run migration: {ex}");
                }
            }

            if (!context.Platforms.Any())
            {
                System.Console.WriteLine("Seeding data....");
                context.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "DotNet",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "JavaScript",
                        Publisher = "Mozilla",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Ruby",
                        Publisher = "OpenSource",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "React",
                        Publisher = "Facebook",
                        Cost = "FreeTrial"
                    });

                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("-----> We already have data");
            }
        }
    }
}