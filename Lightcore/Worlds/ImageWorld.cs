namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ImageWorld : WorldBuilder
    {
        public ImageWorld(Image image)
        {

            var bitmap = new Bitmap(image);

            Lights = new List<Light>()
            {
                new AngleLight
                (
                    Color.White.ToVector(),
                    new Vector(-50f, -50f, 600f),
                    new Vector(1, 1, -5),
                    400,
                    Constants.PI
                ),
            };

            var resolution = Math.Min(image.Width, image.Height);

            var colorStepSize = (double)byte.MaxValue / resolution;

            var map = new Tuple<double, Vector>[resolution, resolution];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] =
                        new Tuple<double, Vector>
                        (
                            Math.Sin(x / 20) * 20,
                            bitmap.GetPixel(x, resolution - y - 1).ToVector()
                        );
                }
            }

            int previewResolution = 10;
            var stepSize = resolution / 10;
            var reducedMap = new Tuple<double, Vector>[previewResolution, previewResolution];

            for (int x = 0; x < reducedMap.GetLength(0); x++)
            {
                for (int y = 0; y < reducedMap.GetLength(1); y++)
                {
                    reducedMap[x, y] = map[x * stepSize, y * stepSize];
                }
            }

            Entities.Add(WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), 300, 300, map));
            Entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), 300, 300, reducedMap));
        }
    }
}
