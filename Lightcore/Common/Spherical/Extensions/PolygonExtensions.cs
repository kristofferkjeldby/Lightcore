namespace Lightcore.Common.Spherical.Extensions
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Models;

    public static class PolygonExtensions
    {
        public static SphericalTriangle ToSphericalTriangle(this Polygon a)
        {
            return new SphericalTriangle(a.Elements);
        }
    }
}
