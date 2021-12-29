namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;

    public partial class Shapes
    {
        public static Entity Square(Vector origin, Vector x, Vector y, Texture texture)
        {
            var diagonal = origin + x + y;

            return new Entity
            (
                EntityType.World,
                new Polygon(texture, new Vector[] { origin, origin + x, origin + y }),
                new Polygon(texture, new Vector[] { diagonal, diagonal - x , diagonal - y })
            );
        }
    }
}
