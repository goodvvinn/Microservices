namespace PlatformService
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using PlatformService.Data;
    using PlatformService.SyncDataServices.Http;

    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this._env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                System.Console.WriteLine("Using SQLServer");
                services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                System.Console.WriteLine("Using InMemDb");
                services.AddDbContext<AppDbContext>(opt => 
                    opt.UseInMemoryDatabase("InMem"));
            }
            
            services.TryAddScoped<IPlatformRepo, PlatformRepo>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });

            System.Console.WriteLine($"----> CommandService Endpoint {Configuration["CommandService"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDb.PrepPopulation(app, env.IsProduction());
        }
    }
}
