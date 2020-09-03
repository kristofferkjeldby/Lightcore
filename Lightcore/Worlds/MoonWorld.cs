namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Drawing;

    public class MoonWorld : WorldBuilder
    {
        public MoonWorld(int resolution = 40, int previewResolution = 60) : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-500, -500, 500f),
                    900
                )
            );

            var image = ImageTextureStore.Get("Moon").ImageTextureType.Image;
            var bitmap = new Bitmap(image);

            var map1 = new Tuple<double, Vector>[bitmap.Width, bitmap.Height];

            for (int x = 0; x < map1.GetLength(0); x++)
            {
                for (int y = 0; y < map1.GetLength(1); y++)
                {
                    map1[x, y] =
                        new Tuple<double, Vector>
                        (
                            bitmap.GetPixel(x, y).GetBrightness() * 5,
                            bitmap.GetPixel(x, y).ToVector() & new Vector(1.3, 1.3, 0.7)
                        );
                }
            }

            var map2 = new Tuple<double, Vector>[bitmap.Width, bitmap.Height];

            for (int x = 0; x < map2.GetLength(0); x++)
            {
                for (int y = 0; y < map2.GetLength(1); y++)
                {
                    map2[x, y] =
                        new Tuple<double, Vector>
                        (
                            bitmap.GetPixel(x, y).GetBrightness() * 30,
                            bitmap.GetPixel(x, y).ToVector()
                        );
                }
            }

            Entities.Add(WorldUtils.Sphere(EntityType.Preview, new Vector(-50, -50, 0), 50, map1.Reduce(40)));
            Entities.Add(WorldUtils.Sphere(EntityType.Preview, new Vector(150, 50, -860), 800, map2.Reduce(100)));
            Entities.Add(WorldUtils.Sphere(EntityType.World, new Vector(-50, -50, 0), 50, map1.Reduce(800)));
            Entities.Add(WorldUtils.Sphere(EntityType.World, new Vector(150, 50, -860), 800, map2));
        }
    }
}
