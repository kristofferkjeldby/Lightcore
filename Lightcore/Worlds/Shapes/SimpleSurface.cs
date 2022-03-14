namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleSurface(Vector color, Vector origin, Vector axis1, Vector axis2, int resolution, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                resolution = Settings.PreviewResolution;

            var polygons = new List<Polygon>();

            var xStep = axis1 / resolution;
            var yStep = axis2 / resolution;

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    var guid = Guid.NewGuid();

                    var start = origin + (xStep * x) + (yStep * y);

                    polygons.Add(
                        new Polygon
                        (
                            texture(color),
                            new Vector[] {
                                start,
                                start + xStep,
                                start + yStep,
                            }
                            ,
                            guid
                        )
                    );

                    polygons.Add(
                        new Polygon
                        (
                            texture(color),
                            new Vector[]
                            {
                                start + xStep + yStep,
                                start + yStep,
                                start + xStep,
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
