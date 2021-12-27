namespace Lightcore.Worlds
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Enumerators;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class FlatWorld : WorldBuilder
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public Tuple<float, Vector>[,] Map { get; set; }

        public Tuple<float, Vector>[,] PreviewMap { get; set; }

        public FlatWorld(int width = 600, int height = 600, int resolution = 200, int previewResolution = 20) : base()
        {
            Width = width;
            Height = height;
            Resolution = resolution;
            PreviewResolution = previewResolution;

            var xColorEnumerator = new ColorEnumerator(resolution, new Vector(1, 0, 0), new Vector(-1, 0, 1)).GetEnumerator();

            Map = new Tuple<float, Vector>[resolution, resolution];

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                var xColor = xColorEnumerator.Get();
                var yColorEnumerator = new ColorEnumerator(resolution, xColor, new Vector(0, 1, 0)).GetEnumerator();

                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    var yColor = yColorEnumerator.Get();
                    var distance = Math.Sqrt(Math.Pow(x - resolution / 2, 2) + Math.Pow(y - resolution / 2, 2)) / (resolution / 2);

                    Map[x, y] =
                        new Tuple<float, Vector>
                        (
                            0,
                            yColor
                        );
                }
            }

            PreviewMap = Map.Reduce(PreviewResolution);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            AddFiltered(entities, renderMode, EntityType.Preview, () => WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), Width, Height, PreviewMap, ColorTextureStore.ColorTexture));
            AddFiltered(entities, renderMode, EntityType.World, () => WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), Width, Height, Map, ColorTextureStore.ColorTexture));

            lights.Add(
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(0f, -0f, 400f),
                    400
                )
            );
        }
    }
}
