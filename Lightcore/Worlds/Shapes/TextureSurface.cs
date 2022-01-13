namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using Lightcore.Worlds.Extensions;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity TextureSurface(Vector color, Vector origin, Vector axis1, Vector axis2, int resolution, ImageTextureBuilder textureBuilder, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                resolution = Settings.PreviewResolution;

            textureBuilder.Slice(resolution, resolution);

            var polygons = new List<Polygon>();

            var xStep = axis1 / resolution;
            var yStep = axis2 / resolution;

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    var guid = Guid.NewGuid();

                    var start = origin + (xStep * x) + (yStep * y);

                    var texture = textureBuilder.GetTexture(color, x, y);

                    polygons.Add(
                        new Polygon
                        (
                            texture,
                            new Vector[] {
                                start,
                                start + xStep,
                                start + xStep + yStep,
                                start + yStep,
                            },
                            guid
                        )
                    );
                }
            }

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
