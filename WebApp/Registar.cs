using Location.City;
using Location.City.Abstractions;
using Location.Country;
using Location.Country.Abstractions;
using Location.UserLocation.Abstractions;
using Location.UserLocation;
using Location;
using Infrastructure;
using Install;
using Message;
using Message.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using User;
using User.Abstraction;
using User.Service;
using WebApp.Service;
using WebApp.Settings;
using WebApp.Service;

namespace WebApp
{
    /// <summary>
    /// Регистратор сервиса
    /// </summary>
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            applicationSettings.ConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";
            services.AddSingleton(applicationSettings);
            services.AddScoped(typeof(DbContext), typeof(DataContext));
            return services.AddSingleton((IConfigurationRoot)configuration)
                .InstallServices()
                .ConfigureContext(applicationSettings.ConnectionString)
                .InstallRepositories();
        }

        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IMessageService, MessageService>()
                .AddTransient<ICityService, CityService>()
                .AddTransient<ICountryService, CountryService>()
                .AddTransient<ILocationService, LocationService>()
                .AddTransient<IUserService, UserService>()
                .AddScoped<IRabbitMQService, RabbitMQService>()
                .AddTransient<IProfileService, ProfileService>();

            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IMessageRepository, MessageRepository>()
                .AddTransient<ICityRepository, CityRepository>()
                .AddTransient<ICountryRepository, CountryRepository>()
                .AddTransient<IUserLocationRepository, UserLocationRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IResetPasswordRepository, ResetPasswordRepository>();
            return serviceCollection;
        }
    }
}