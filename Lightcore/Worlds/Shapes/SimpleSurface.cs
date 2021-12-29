namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleSurface(RenderMode renderMode, Vector color, Vector origin, float width, float height, int resolution, Func<Vector, Texture> texture)
        {
            if (renderMode.Preview)
                resolution = Settings.PreviewResolution;

            var polygons = new List<Polygon>();

            var xStepSize = width / resolution;
            var yStepSize = height / resolution;

            var xOffset = origin[0] - width / 2;
            var yOffset = origin[1] - height / 2;
            var zOffset = origin[2];

            for (int x = 0; x < resolution - 1; x++)
            {
                for (int y = 0; y < resolution - 1; y++)
                {
                    var guid = Guid.NewGuid();

                    polygons.Add(
                        new Polygon
                        (
                            texture(color),
                            new Vector[] {
                            new Vector(xOffset + xStepSize * x, yOffset + yStepSize * y, zOffset),
                            new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * y, zOffset),
                            new Vector(xOffset + xStepSize * x, yOffset + yStepSize * (y+1), zOffset)
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
                                new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * (y+1), zOffset),
                                new Vector(xOffset + xStepSize * x, yOffset + yStepSize * (y+1), zOffset),
                                new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * y, zOffset)
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
