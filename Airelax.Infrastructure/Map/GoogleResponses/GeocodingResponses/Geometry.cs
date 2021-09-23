// ReSharper disable InconsistentNaming

namespace Airelax.Infrastructure.Map.GoogleResponses.GeocodingResponses
{
    public class Geometry
    {
        public GoogleCoordinateRange bounds { get; set; }
        public GoogleCoordinate location { get; set; }
        public string location_type { get; set; }
        public GoogleCoordinateRange viewport { get; set; }
    }
}