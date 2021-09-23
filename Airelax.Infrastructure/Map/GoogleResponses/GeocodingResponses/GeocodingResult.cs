using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Airelax.Infrastructure.Map.GoogleResponses.GeocodingResponses
{
    public class GeocodingResult
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }
}