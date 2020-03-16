namespace Censo.API
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Domain.Interfaces.API;
    using Domain.Interfaces.Data;
    using Hubs;
    using Infra.Data;
    using Infra.Data.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // infra services
            if (CurrentEnvironment.IsEnvironment("Test"))
            {
                services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("database").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            }
            else
            {
                services.AddDbContext<DatabaseContext>(options =>
                {
                    var datasource = Environment.GetEnvironmentVariable("SQL_SERVER_HOST");
                    var database = Environment.GetEnvironmentVariable("SQL_SERVER_DATABASE");
                    var user = Environment.GetEnvironmentVariable("SQL_SERVER_USER");
                    var password = Environment.GetEnvironmentVariable("SQL_SERVER_PASSWORD");
                    options.UseSqlServer($"Data Source={datasource};Initial Catalog={database};Persist Security Info=True;User ID={user};Password={password}");
                });
            }

            // cors
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            }));

            // repositories
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<ISchoolingRepository, SchoolingRepository>();
            services.AddTransient<IEthnicityRepository, EthnicityRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();

            // controllers
            services.AddControllers();

            // signal r server for real time dashboard
            services.AddSignalR();

            // swagger doc generation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Census API" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // dashboard hub
            services.AddTransient<IHub, DashboardHub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Census API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<DashboardHub>("/hub");
            });

            
            if (Environment.GetCommandLineArgs().Contains("--migrate"))
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                    context.Database.Migrate();
                }
            }
        }
    }
}
