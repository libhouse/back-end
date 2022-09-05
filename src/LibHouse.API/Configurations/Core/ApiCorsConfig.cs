using Microsoft.Extensions.DependencyInjection;

namespace LibHouse.API.Configurations.Core
{
    public static class ApiCorsConfig
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Development",
                    configurePolicy: builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });

            return services;
        }
    }
}