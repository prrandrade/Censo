namespace Censo.API
{
    using Domain.Interfaces;
    using Domain.Interfaces.Data;
    using Infra.Data;
    using Infra.Data.Repositories;
    using Infra.Environment;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // infra services
            services.AddTransient<IMyEnvironment, MyEnvironment>();
            services.AddDbContext<DatabaseContext>();

            services.AddControllers();

            // repositories
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<ISchoolingRepository, SchoolingRepository>();
            services.AddTransient<IEthnicityRepository, EthnicityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
