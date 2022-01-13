namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public partial class Shapes
    {
        public static Entity TextureBox(Vector color, Vector origin, Vector axis1, Vector axis2, Vector axis3, int resolution, ImageTextureBuilder textureBuilder, RenderMode renderMode = null)
        {
            var polygons = new List<Polygon>();

            var frontButtonLeft = origin;
            var frontButtonRight = origin + axis1;
            var backButtonRight = origin + axis1 + axis3;
            var backButtonLeft = origin + axis3;
            var frontTopRight = origin + axis2;

            polygons.AddRange(TextureSurface(color, frontButtonLeft, axis1, axis2, resolution, textureBuilder).Elements);
            polygons.AddRange(TextureSurface(color, frontButtonRight, axis3, axis2, resolution, textureBuilder).Elements);
            polygons.AddRange(TextureSurface(color, backButtonRight, -axis1, axis2, resolution, textureBuilder).Elements);
            polygons.AddRange(TextureSurface(color, backButtonLeft, -axis3, axis2, resolution, textureBuilder).Elements);
            polygons.AddRange(TextureSurface(color, frontTopRight, axis1, axis3, resolution, textureBuilder).Elements);
            polygons.AddRange(TextureSurface(color, frontButtonRight, -axis1, axis3, resolution, textureBuilder).Elements);

            var box = new Entity(EntityType.World, polygons.ToArray());
            return box;

        }
    }
}
