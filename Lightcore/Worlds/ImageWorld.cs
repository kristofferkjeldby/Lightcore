namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
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
            entities.Add(Shapes.Surface(new Vector(-100, -100, 0), new Vector(200, 0, 0), new Vector(0, 200, 0), Map, ColorTextureStore.ShinyTexture, renderMode));

            lights.Add(
                new AngleLight
                (
                    Color.White.ToVector(),
                    new Vector(-50f, -50f, 600f),
                    new Vector(1, 1, -5),
                    500,
                    Constants.PI
                )
            );
        }
    }
}
