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
    using System.Drawing;

    public class SurfaceWorld : WorldBuilder
    {
        public SurfaceWorld() : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(200, 160, 170f),
                    400
                )
            );

            var surface = new Tuple<double, Vector>[300, 300];

            var surfaceXColor = new ColorEnumerator(surface.GetLength(0), new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();
            var surfaceYColor = new ColorEnumerator(surface.GetLength(1), new Vector(0, 0, 0), new Vector(0, 1, 0)).GetEnumerator();

            for (int x = 0; x < surface.GetLength(0); x++)
            {
                var xColor = surfaceXColor.Get();
                for (int y = 0; y < surface.GetLength(1); y++)
                {
                    var color = new Vector(0, 0, 0);
                        color = surfaceYColor.Get() + xColor;

                    surface[x, y] = new Tuple<double, Vector>(Math.Sin(x/20d) * 50d + Math.Sin(y/70d) * 8d, color);
                }
            }

            Entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), 400, 300, surface.Reduce(40)));
            Entities.Add(WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), 400, 300, surface));
        }
    }
}
