namespace Lightcore.Worlds
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Enumerators;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class FlatWorld : WorldBuilder
    {
        public FlatWorld(int width = 600, int height = 600, int resolution = 200, int previewResolution = 20) : base()
        {
            Lights = new List<Light>()
            {
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(0f, -0f, 400f),
                    400
                ),
            };

            var xColorEnumerator = new ColorEnumerator(resolution, new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();

            var map = new Tuple<double, Vector>[resolution, resolution];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                var xColor = xColorEnumerator.Get();
                var yColorEnumerator = new ColorEnumerator(resolution, xColor, new Vector(0, 1, 0)).GetEnumerator();

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    var yColor = yColorEnumerator.Get();
                    var distance = Math.Sqrt(Math.Pow(x - resolution / 2, 2) + Math.Pow(y - resolution / 2, 2)) / (resolution / 2);

                    map[x, y] =
                        new Tuple<double, Vector>
                        (
                            0,
                            yColor
                        );
                }
            }

            var reducedMap = map.Reduce(previewResolution);

            Entities.Add(WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), width, height, map));
            Entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), width, height, reducedMap));
        }
    }
}
