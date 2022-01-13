namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleBox(Vector color, Vector origin, Vector axis1, Vector axis2, Vector axis3, int resolution, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            var polygons = new List<Polygon>();

            var frontButtonLeft = origin;
            var frontButtonRight = origin + axis1;
            var backButtonRight = origin + axis1 + axis3;
            var backButtonLeft = origin + axis3;
            var frontTopRight = origin + axis2;

            polygons.AddRange(SimpleSurface(color, frontButtonLeft, axis1, axis2, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, frontButtonRight, axis3, axis2, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, backButtonRight, -axis1, axis2, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, backButtonLeft, -axis3, axis2, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, frontTopRight, axis1, axis3, resolution, texture).Elements);
            polygons.AddRange(SimpleSurface(color, frontButtonRight, -axis1, axis3, resolution, texture).Elements);

            var box = new Entity(EntityType.World, polygons.ToArray());
            return box;
        }
    }
}
