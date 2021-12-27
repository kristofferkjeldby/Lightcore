namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ImageWorld : WorldBuilder
    {
        public Bitmap Image { get; set; }

        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public Tuple<float, Vector>[,] Map { get; set; }

        public Tuple<float, Vector>[,] PreviewMap { get; set; }

        public ImageWorld(Image image, int previewResolution = 40)
        {
            Image = new Bitmap(image);
            Resolution = Math.Min(image.Width, image.Height);
            PreviewResolution = previewResolution;

            var colorStepSize = (float)byte.MaxValue / Resolution;

            Map = new Tuple<float, Vector>[Resolution, Resolution];

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    Map[x, y] =
                        new Tuple<float, Vector>
                        (
                            CommonUtils.Sin(x / 20) * 20,
                            Image.GetPixel(x, Resolution - y - 1).ToVector()
                        );
                }
            }

            PreviewMap = Map.Reduce(PreviewResolution);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            AddFiltered(entities, renderMode, EntityType.Preview, () => WorldUtils.Surface(EntityType.Preview, new Vector(0, 0, 0), 300, 300, PreviewMap, ColorTextureStore.ColorTexture));
            AddFiltered(entities, renderMode, EntityType.World, () => WorldUtils.Surface(EntityType.World, new Vector(0, 0, 0), 300, 300, Map, ColorTextureStore.ColorTexture));

            lights.Add(
                new AngleLight
                (
                    Color.White.ToVector(),
                    new Vector(-50f, -50f, 600f),
                    new Vector(1, 1, -5),
                    400,
                    Constants.PI
                )
            );
        }
    }
}
