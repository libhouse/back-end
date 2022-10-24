using LibHouse.API.Configurations.Authentication;
using LibHouse.API.Configurations.Cache;
using LibHouse.API.Configurations.Core;
using LibHouse.API.Configurations.Dependencies;
using LibHouse.API.Configurations.Email;
using LibHouse.API.Configurations.Logging;
using LibHouse.API.Configurations.Swagger;
using LibHouse.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibHouse.API
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration, 
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiResponseCompressionConfig();
            services.AddEmailConfig(Configuration, Environment);
            services.ResolveGeneralDependencies();
            services.AddIdentityConfiguration(Configuration);
            services.AddAuthenticationConfiguration(Configuration);
            services.ResolveRepositories(Configuration);
            services.ResolveValidators();
            services.ResolveGateways();
            services.ResolveSenders(Configuration);
            services.ResolveServices();
            services.AddApiCachingConfig(Configuration);
            services.AddWebApiConfig();
            services.AddLoggingConfiguration();
            services.AddSwaggerConfig();
        }

        public void Configure(
            IApplicationBuilder app, 
            IApiVersionDescriptionProvider provider)
        {
            app.UseResponseCompression();
            if (Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto,
            });
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseLoggingConfiguration(Configuration);
            app.UseMvcConfiguration(Environment);
            app.UseSwaggerConfig(provider);
        }
    }
}