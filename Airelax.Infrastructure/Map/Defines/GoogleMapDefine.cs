namespace Airelax.Infrastructure.Map.Defines
{
    public struct GoogleMapDefine
    {
        public struct Geocoding
        {
            public const string PATH = "/maps/api/geocode/json";
        }

        public struct QueryString
        {
            public const string ADDRESS = "address";
            public const string KEY = "key";
        }
    }
}