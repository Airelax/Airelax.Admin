using System.Threading.Tasks;
using Airelax.Infrastructure.Map.Responses;

namespace Airelax.Infrastructure.Map.Abstractions
{
    public interface IGeocodingService
    {
        Task<GeocodingInfo> GetGeocodingInfo(string address);
    }
}