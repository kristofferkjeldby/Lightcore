namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity MapBox(Vector origin, Vector axis1, Vector axis2, Vector axis3, Tuple<float, Vector>[,] map, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            var polygons = new List<Polygon>();

            var frontButtonLeft = origin;
            var frontButtonRight = origin + axis1;
            var backButtonRight = origin + axis1 + axis3;
            var backButtonLeft = origin + axis3;
            var frontTopRight = origin + axis2;

            polygons.AddRange(MapSurface(frontButtonLeft, axis1, axis2, map, texture).Elements);
            polygons.AddRange(MapSurface(frontButtonRight, axis3, axis2, map, texture).Elements);
            polygons.AddRange(MapSurface(backButtonRight, -axis1, axis2, map, texture).Elements);
            polygons.AddRange(MapSurface(backButtonLeft, -axis3, axis2, map, texture).Elements);
            polygons.AddRange(MapSurface(frontTopRight, axis1, axis3, map, texture).Elements);
            polygons.AddRange(MapSurface(frontButtonRight, -axis1, axis3, map, texture).Elements);

            var box = new Entity(EntityType.World, polygons.ToArray());
            return box;

        }
    }
}
