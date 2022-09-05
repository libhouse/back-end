using LibHouse.API.Filters.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace LibHouse.API.Configurations.Swagger
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.IncludeXmlComments(GetSwaggerXmlCommentsPath());
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(
            this IApplicationBuilder app, 
            IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                    options.RoutePrefix = "";
                    options.EnableDeepLinking();
                    options.DisplayOperationId();
                });

            return app;
        }

        private static string GetSwaggerXmlCommentsPath()
        {
            string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            string xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            return xmlCommentsFullPath;
        }
    }
}