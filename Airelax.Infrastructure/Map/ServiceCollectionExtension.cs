using Lazcat.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airelax.Infrastructure.Map
{
    public static class ServiceCollectionExtension
    {
        public static void AddGoogleGeoService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<GoogleGeocodingService>();
            services.Configure<GoogleMapApiSetting>(configuration.GetSection("GoogleMapApiSetting"));
        }
    }
}