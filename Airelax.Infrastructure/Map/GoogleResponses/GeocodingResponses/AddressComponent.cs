using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Airelax.Infrastructure.Map.GoogleResponses.GeocodingResponses
{
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
}