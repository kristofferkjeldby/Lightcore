namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ImageWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }
        public object MapHelpers { get; }

        public ImageWorld(Image image)
        {
            var bitmap = new Bitmap(image);

            // As the zero coordinate is in the low bottom of the screen flipping the image is needed.
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

            Map = MapHelper.CreateMap(
                bitmap.Width,
                bitmap.Height,
                (x, y) => CommonUtils.Sin(x / 30f) * 70,
                (x, y) => bitmap.GetPixel(x, y).ToVector()
            );
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.Surface(renderMode, new Vector(0, 0, 0), 300, 300, Map, ColorTextureStore.ShinyTexture));

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
