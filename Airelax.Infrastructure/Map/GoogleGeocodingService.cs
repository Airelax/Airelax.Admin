using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Airelax.Infrastructure.Map.Abstractions;
using Airelax.Infrastructure.Map.Defines;
using Airelax.Infrastructure.Map.GoogleResponses.GeocodingResponses;
using Airelax.Infrastructure.Map.Responses;
using Lazcat.Infrastructure.DependencyInjection;
using Lazcat.Infrastructure.ExceptionHandlers;
using Lazcat.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Airelax.Infrastructure.Map
{
    [DependencyInjection(typeof(IGeocodingService))]
    public class GoogleGeocodingService : IGeocodingService
    {
        private const int Timeout = 5000;
        private readonly GoogleMapApiSetting _googleMapApiSetting;
        private readonly ILogger<GoogleGeocodingService> _logger;
        private HttpClient _httpClient;

        public GoogleGeocodingService(HttpClient httpClient, IOptions<GoogleMapApiSetting> options, ILogger<GoogleGeocodingService> logger)
        {
            _logger = logger;
            _googleMapApiSetting = options.Value;
            SetHttpClient(httpClient);
        }


        public async Task<GeocodingInfo> GetGeocodingInfo(string address)
        {
            var requestUrl = $"{GoogleMapDefine.Geocoding.PATH}?{GoogleMapDefine.QueryString.ADDRESS}={address}&{GoogleMapDefine.QueryString.KEY}={_googleMapApiSetting.ApiKey}";
            var geocodingResponse = await SendRequest<GeocodingResponse>(requestUrl);
            return ConvertToGeocodingInfo(geocodingResponse);
        }

        private async Task<T> SendRequest<T>(string requestUrl) where T : GoogleApiStatus
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>(requestUrl);
                if (response == null) throw ExceptionBuilder.Build(HttpStatusCode.InternalServerError, "cannot not get map data");
                if (!string.Equals(response.status, "ok", StringComparison.CurrentCultureIgnoreCase)) throw ExceptionBuilder.Build(HttpStatusCode.InternalServerError, "cannot not get map data");
                return response;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.ToString());
                throw ExceptionBuilder.Build(HttpStatusCode.InternalServerError, "cannot not get map data");
            }
        }

        private static GeocodingInfo ConvertToGeocodingInfo(GeocodingResponse geocodingResponse)
        {
            var geometry = geocodingResponse.results.First()?.geometry;
            if (geometry == null) throw ExceptionBuilder.Build(HttpStatusCode.InternalServerError, "cannot not get map data");
            return new GeocodingInfo
            {
                Bounds = new CoordinateRange
                {
                    SouthWest = new Coordinate(geometry.bounds.southwest.lat, geometry.bounds.southwest.lng),
                    Northeast = new Coordinate(geometry.bounds.northeast.lat, geometry.bounds.northeast.lng)
                },
                Viewport = new CoordinateRange
                {
                    SouthWest = new Coordinate(geometry.viewport.southwest.lat, geometry.viewport.southwest.lng),
                    Northeast = new Coordinate(geometry.viewport.northeast.lat, geometry.viewport.northeast.lng)
                },
                Location = new Coordinate(geometry.location.lat, geometry.location.lng)
            };
        }

        private void SetHttpClient(HttpClient httpClient)
        {
            httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout);
            httpClient.BaseAddress = new Uri(_googleMapApiSetting.BaseUrl);
            _httpClient = httpClient;
        }
    }
}