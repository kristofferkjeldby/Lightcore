namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;

    public partial class WorldUtils
    {
        public static Entity Square(Texture texture, EntityType entityType, Vector origin, Vector x, Vector y)
        {
            var diagonal = origin + x + y;

            return new Entity
            (
                entityType,
                new Polygon(texture, new Vector[] { origin, origin + x, origin + y }),
                new Polygon(texture, new Vector[] { diagonal, diagonal - x , diagonal - y })
            );
        }
    }
}
