namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Enumerators;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ShadowWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }

        public ShadowWorld() : base()
        {
            Map = new Tuple<float, Vector>[20, 20];

            var surfaceXColor = new ColorEnumerator(Map.GetLength(0), new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();
            var surfaceYColor = new ColorEnumerator(Map.GetLength(1), new Vector(0, 0, 0), new Vector(0, 1, 0)).GetEnumerator();

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                var xColor = surfaceXColor.Get();
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    var color = surfaceYColor.Get() + xColor;
                    Map[x, y] = new Tuple<float, Vector>(CommonUtils.Sin(x/20f) * 50f, color);
                }
            }


        }

        public override void Create(List<Entity> entities, List<Light> lights, int animateStep = 0)
        {
            entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, -200), 700, 700, Map));
            entities.Add(WorldUtils.SimpleSphere(EntityType.Preview, Color.Red.ToVector(), new Vector(0, 0, 100), 50, 20));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(200, 100, 400f),
                    400
                )
            );
        }
    }
}
