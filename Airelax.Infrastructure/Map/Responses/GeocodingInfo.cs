namespace Airelax.Infrastructure.Map.Responses
{
    public class GeocodingInfo
    {
        public Coordinate Location { get; set; }
        public CoordinateRange Bounds { get; set; }
        public CoordinateRange Viewport { get; set; }
    }
}