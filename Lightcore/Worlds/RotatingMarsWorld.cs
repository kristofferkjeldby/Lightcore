namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class RotatingMarsWorld : WorldBuilder
    {
        public int PreviewResolution { get; set; }

        public Tuple<float, Vector>[,] Map { get; set; }

        public RotatingMarsWorld(int previewResolution = 40, int step = 0) : base()
        {
            PreviewResolution = previewResolution;
            var colorBitmap = ImageTextureStore.Get("MarsColor").ImageTextureType.Bitmap;
            var heightBitmap = ImageTextureStore.Get("MarsHeight").ImageTextureType.Bitmap;

            Map = new Tuple<float, Vector>[colorBitmap.Width, colorBitmap.Height];

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    Map[x, y] =
                        new Tuple<float, Vector>
                        (
                            heightBitmap.GetPixel((x+130) % heightBitmap.Width, y).GetBrightness() * 5,
                            colorBitmap.GetPixel(x, y).ToVector()
                        );
                }
            }
        }

        public override void Create(List<Entity> entities, List<Light> lights, int animateStep = 0)
        {
            var angle = (Constants.PI2 / Settings.AnimateMaxSteps) * animateStep;

            var rotateLight = CartesianUtils.Rotate(new Vector(0, 1, 0.2f).Unit(), -angle * 2);
            var rotateMars = CartesianUtils.Rotate(new Vector(0, 1, 0).Unit(), angle);

            entities.Add(WorldUtils.Sphere(EntityType.Preview, new Vector(0, 0, 0), 150, Map.Reduce(PreviewResolution)).Transform(rotateMars));
            entities.Add(WorldUtils.Sphere(EntityType.World, new Vector(0, 0, 0), 150, Map).Transform(rotateMars));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-500, -500, 500f).Transform(rotateLight),
                    900
                )
            );
        }
    }
}
