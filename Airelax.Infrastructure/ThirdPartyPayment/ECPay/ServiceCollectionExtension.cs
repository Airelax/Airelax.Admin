using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay
{
    public static class ServiceCollectionExtension
    {
        public static void AddECPayService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IECPayService, ECPayService>();
            services.AddHttpClient<ECPayService>();
            services.Configure<ECPaySetting>(configuration.GetSection("ECPay"));
        }
    }
}