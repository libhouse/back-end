﻿using LibHouse.API.Authentication;
using LibHouse.API.Configurations.Contexts;
using LibHouse.API.Configurations.Swagger;
using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Business.Validations.Users;
using LibHouse.Data.Repositories.Residents;
using LibHouse.Data.Repositories.Users;
using LibHouse.Data.Transactions;
using LibHouse.Infrastructure.Authentication.Login;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Authentication.Logout;
using LibHouse.Infrastructure.Authentication.Password;
using LibHouse.Infrastructure.Authentication.Register;
using LibHouse.Infrastructure.Email.Senders.Users;
using LibHouse.Infrastructure.Email.Settings.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibHouse.API.Configurations.Dependencies
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveGeneralDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoggedUser, AspNetLoggedUser>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }

        public static IServiceCollection ResolveBuilders(this IServiceCollection services)
        {
            services.AddScoped<IRoomPreferencesBuilder, RoomPreferencesBuilder>();
            return services;
        }

        public static IServiceCollection ResolveValidators(this IServiceCollection services)
        {
            services.AddScoped<UserRegistrationValidator>();
            return services;
        }

        public static IServiceCollection ResolveSenders(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<UserRegistrationSenderSettings>(options
                => configuration.GetSection("UserRegistrationSenderSettings").Bind(options));
            services.AddScoped<IUserRegistrationSender, MailKitUserRegistrationSender>();
            services.Configure<UserPasswordResetSenderSettings>(options
                => configuration.GetSection("UserPasswordResetSenderSettings").Bind(options));
            services.AddScoped<IUserPasswordResetSender, MailKitUserPasswordResetSender>();
            return services;
        }

        public static IServiceCollection ResolveGateways(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrationGateway, IdentityUserRegistrationGateway>();
            services.AddScoped<IConfirmUserRegistrationGateway, IdentityConfirmUserRegistrationGateway>();
            services.AddScoped<IUserLoginGateway, IdentityUserLoginGateway>();
            services.AddScoped<IUserLogoutGateway, IdentityUserLogoutGateway>();
            services.AddScoped<IUserLoginRenewalGateway, IdentityUserLoginRenewalGateway>();
            services.AddScoped<IUserPasswordResetGateway, IdentityUserPasswordResetGateway>();
            services.AddScoped<IConfirmUserPasswordResetGateway, IdentityConfirmUserPasswordResetGateway>();
            return services;
        }

        public static IServiceCollection ResolveUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistration, UserRegistration>();
            services.AddScoped<IConfirmUserRegistration, ConfirmUserRegistration>();
            services.AddScoped<IUserLogin, UserLogin>();
            services.AddScoped<IUserLogout, UserLogout>();
            services.AddScoped<IUserLoginRenewal, UserLoginRenewal>();
            services.AddScoped<IUserPasswordReset, UserPasswordReset>();
            services.AddScoped<IConfirmUserPasswordReset, ConfirmUserPasswordReset>();
            services.AddScoped<IResidentRoomPreferencesRegistration, ResidentRoomPreferencesRegistration>();
            return services;
        }

        public static IServiceCollection ResolveRepositories(
            this IServiceCollection services, 
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetValue<string>("LibHouseConnectionString");
            services.AddLibHouseContext(environment, connectionString);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResidentRepository, ResidentRepository>();
            return services;
        }
    }
}