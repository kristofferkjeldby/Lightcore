namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Drawing;

    public class TorusWorld : WorldBuilder
    {
        public TorusWorld(int seed = 0, int maxseed = 100) : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-450, -450, 300),
                    700
                )
            );

            var image = ImageTextureStore.Get("Doughnut").ImageTextureType.Image;
            var bitmap = new Bitmap(image);

            var xStepSize = (Math.PI * 2) / image.Width;
            var xStart = ((double)image.Width / maxseed) * seed;


            var map = new Tuple<double, Vector>[bitmap.Width, bitmap.Height];

            for (int x = 0; x < map.GetLength(0); x++)
            {

                var xAdd = Math.Sin(((xStart + x) * xStepSize));

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new Tuple<double, Vector>(bitmap.GetPixel(x, y).GetBrightness()*5 + xAdd*20, bitmap.GetPixel(x , y).ToVector());
                }
            }

            Entities.Add(WorldUtils.Torus(EntityType.World, new Vector(0, 0, 0), 100, 50, map));
        }
    }
}
