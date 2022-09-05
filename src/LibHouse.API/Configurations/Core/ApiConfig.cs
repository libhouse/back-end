using LibHouse.API.Configurations.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace LibHouse.API.Configurations.Core
{
    public static class ApiConfig
    {
        public static IServiceCollection AddWebApiConfig(this IServiceCollection services)
        {
            services.AddCorsConfig();

            services.AddMvcCore(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddApiVersioningConfig();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors("Development");
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}