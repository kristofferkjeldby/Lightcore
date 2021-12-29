namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleBox(Vector color, Vector origin, Vector x, Vector y, Vector z, int resolution, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            var polygons = new List<Polygon>();

            var diagonal = origin + x + y + z;

            polygons.AddRange(SimpleSurface(color, origin, y, x, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, origin, x, z, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, diagonal, -z, -x, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, diagonal, -x, -y, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, origin, z, y, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, diagonal, -y, -z, resolution, texture).Elements);

            var box = new Entity(EntityType.World, polygons.ToArray());
            return box;

        }
    }
}
