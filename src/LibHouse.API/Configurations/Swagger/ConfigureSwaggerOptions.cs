using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace LibHouse.API.Configurations.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        readonly IConfiguration configuration;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider,
            IConfiguration configuration)
        {
            this.provider = provider;
            this.configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, configuration));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(
            ApiVersionDescription description,
            IConfiguration configuration)
        {
            string mitLicense = configuration.GetSection("GeneralSettings").GetSection("MITLicense").Value;

            var info = new OpenApiInfo()
            {
                Title = "LibHouse API",
                Version = description.ApiVersion.ToString(),
                Description = "This API acts as a backend service for the LibHouse website frontend",
                Contact = new OpenApiContact() { Name = "LibHouse Team", Email = "libhouseteam@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri(mitLicense) }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This version is deprecated.";
            }

            return info;
        }
    }
}