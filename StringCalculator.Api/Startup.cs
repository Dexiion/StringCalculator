using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StringCalculator.Application;
using StringCalculator.Application.Actions;
using StringCalculator.Infraestructure;
using Microsoft.OpenApi.Models;
using StringCalculator.Api.HealthChecks;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StringCalculator.Api
{
    public class Startup
    {
        private const string logFolderPath = "./logs/";
        private const string logFilePath = logFolderPath + "log.txt";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<GetStringCalculator>();
            services.AddHealthChecks().AddFileSystemHealthCheck(logFolderPath);
            services.AddSingleton<ICSharpLogger, CSharpLog>(_ => new CSharpLog(logFilePath));
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                })
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    
                })
                .UseHealthChecks("/status.json", new HealthCheckOptions
                    { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});

        }
    }

    public class ConfigureSwaggerOptions
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
            ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "String Calculator API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }


    public static class ServicesExtensions
    {
        public static IHealthChecksBuilder AddFileSystemHealthCheck(this IHealthChecksBuilder builder, string logFolderPath)
        {
            return builder.Add(new HealthCheckRegistration("Log Folder Health Check",
                _ => new LoggerHealthCheck(logFolderPath), null, null));
        }
    }
}
