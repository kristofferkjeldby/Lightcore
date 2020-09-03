namespace Lightcore.Worlds
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Enumerators;
    using Lightcore.Worlds.Models;
    using System;
    using System.Drawing;

    public class ShadowWorld : WorldBuilder
    {
        public ShadowWorld() : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(200, 100, 400f),
                    400
                )
            );

            var surface = new Tuple<double, Vector>[20, 20];
            var sphere = new Tuple<double, Vector>[40, 40];

            var surfaceXColor = new ColorEnumerator(surface.GetLength(0), new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();
            var surfaceYColor = new ColorEnumerator(surface.GetLength(1), new Vector(0, 0, 0), new Vector(0, 1, 0)).GetEnumerator();

            for (int x = 0; x < surface.GetLength(0); x++)
            {
                var xColor = surfaceXColor.Get();
                for (int y = 0; y < surface.GetLength(1); y++)
                {
                    var color = surfaceYColor.Get() + xColor;
                    surface[x, y] = new Tuple<double, Vector>(Math.Sin(x/20f) * 50f, color);
                }
            }

            Entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, -200), 700, 700, surface));
            Entities.Add(WorldUtils.SimpleSphere(EntityType.Preview, Color.Red.ToVector(), new Vector(0, 0, 100), 50, 20));
        }
    }
}
