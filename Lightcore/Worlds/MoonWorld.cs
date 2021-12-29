namespace Lightcore.Worlds
{
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

    public class MoonWorld : WorldBuilder
    {
        public int PreviewResolution { get; set; }

        public Tuple<float, Vector>[,] Map { get; set; }

        public Tuple<float, Vector>[,] PreviewMap { get; set; }

        public MoonWorld(int previewResolution = 40) : base()
        {
            PreviewResolution = previewResolution;

            var bitmap = ImageTextureStore.Get("Moon").ImageTextureType.Bitmap;

            Map = MapHelper.CreateMap(
                bitmap.Width,
                bitmap.Height,
                (x, y) => bitmap.GetPixel(x, y).GetBrightness() * 5,
                (x, y) => bitmap.GetPixel(x, y).ToVector()
            );

            PreviewMap = Map.Reduce(PreviewResolution);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.Sphere(renderMode, new Vector(0, 0, 0), 100, Map, ColorTextureStore.ColorTexture));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-500, -500, 500f),
                    900
                )
            );
        }
    }
}
