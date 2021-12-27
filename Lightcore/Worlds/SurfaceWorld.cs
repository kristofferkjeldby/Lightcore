namespace Lightcore.Worlds
{
    using Lightcore.Common;
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

    public class SurfaceWorld : WorldBuilder
    {
        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public Tuple<float, Vector>[,] Map { get; set; }

        public SurfaceWorld(int resolution = 200, int previewResolution = 40, int animateStep = 0) : base()
        {
            Resolution = resolution;
            PreviewResolution = previewResolution;

            Map = new Tuple<float, Vector>[Resolution, Resolution];

            var surfaceXColor = new ColorEnumerator(Map.GetLength(0), new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();
            var surfaceYColor = new ColorEnumerator(Map.GetLength(1), new Vector(0, 0, 0), new Vector(0, 1, 0)).GetEnumerator();

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                var xColor = surfaceXColor.Get();
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    var color = new Vector(0, 0, 0);
                        color = surfaceYColor.Get() + xColor;

                    Map[x, y] = new Tuple<float, Vector>(CommonUtils.Sin(x/20f) * 50f + CommonUtils.Sin(y/70f) * 8f, color);
                }
            }


        }

        public override void Create(List<Entity> entities, List<Light> lights, int animateStep = 0)
        {
            entities.Add(WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), 400, 300, Map.Reduce(PreviewResolution)));
            entities.Add(WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), 400, 300, Map));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(200, 160, 170f),
                    400
                )
            );
        }
    }
}
