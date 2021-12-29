namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;

    public partial class Shapes
    {
        public static Entity SimpleSquare(Vector color, Vector origin, Vector x, Vector y, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            var diagonal = origin + x + y;

            return new Entity
            (
                EntityType.World,
                new Polygon(texture(color), new Vector[] { origin, origin + x, origin + y }),
                new Polygon(texture(color), new Vector[] { diagonal, diagonal - x , diagonal - y })
            );
        }
    }
}
