using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Airelax.Infrastructure.Map.GoogleResponses.GeocodingResponses
{
    public class GeocodingResponse : GoogleApiStatus
    {
        public List<GeocodingResult> results { get; set; }
        public string status { get; set; }
    }
}