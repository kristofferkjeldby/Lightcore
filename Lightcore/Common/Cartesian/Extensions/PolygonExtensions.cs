namespace Lightcore.Common.Cartesian.Extensions
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using System.Linq;

    public static class PolygonExtensions
    {
        public static double Distance(this Polygon polygon)
        {
            return polygon.Elements.Min(vector => vector.Length());
        }

        public static Vector Midpoint(this Polygon polygon)
        {
            return polygon.Elements.Average();
        }

        public static Vector Normal(this Polygon polygon)
        {
            return (polygon[1] - polygon[0]) % (polygon[2] -polygon[0]);
        }
    }
}
