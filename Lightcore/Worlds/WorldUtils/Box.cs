namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;
    using System.Collections.Generic;

    public partial class WorldUtils
    {
        public static Entity Box(Texture texture, EntityType entityType, Vector origin, Vector x, Vector y, Vector z)
        {
            var polygons = new List<Polygon>();

            var diagonal = origin + x + y + z;

            polygons.AddRange(Square(texture, entityType, origin, y, x).Elements);
            polygons.AddRange(Square(texture, entityType, origin, x, z).Elements);
            polygons.AddRange(Square(texture, entityType, diagonal, -z, -x).Elements);
            polygons.AddRange(Square(texture, entityType, diagonal, -x, -y).Elements);
            polygons.AddRange(Square(texture, entityType, origin, z, y).Elements);
            polygons.AddRange(Square(texture, entityType, diagonal, -y, -z).Elements);

            var box = new Entity(entityType, polygons.ToArray());
            return box;

        }
    }
}
