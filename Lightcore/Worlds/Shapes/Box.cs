namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity Box(Vector origin, Vector x, Vector y, Vector z, Texture texture, RenderMode renderMode = null)
        {
            var polygons = new List<Polygon>();

            var diagonal = origin + x + y + z;

            polygons.AddRange(Square(origin, y, x, texture).Elements);
            polygons.AddRange(Square(origin, x, z, texture).Elements);
            polygons.AddRange(Square(diagonal, -z, -x, texture).Elements);
            polygons.AddRange(Square(diagonal, -x, -y, texture).Elements);
            polygons.AddRange(Square(origin, z, y, texture).Elements);
            polygons.AddRange(Square(diagonal, -y, -z, texture).Elements);

            var box = new Entity(EntityType.World, polygons.ToArray());
            return box;

        }
    }
}
