namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;

    public partial class WorldUtils
    {
        public static Entity Line(Texture texture, EntityType entityType, Vector origin, Vector end)
        {
            return new Entity
            (
                entityType,
                new Line(origin, end).ToPolygon(texture)
            );
        }
    }
}
