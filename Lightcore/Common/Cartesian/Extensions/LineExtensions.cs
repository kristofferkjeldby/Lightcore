namespace Lightcore.Common.Cartesian.Extensions
{
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;

    public static class LineExtensions
    {
        public static Polygon ToPolygon(this Line line, Texture texture)
        {
            return new Polygon(texture, line.Vectors);
        }
    }
}
