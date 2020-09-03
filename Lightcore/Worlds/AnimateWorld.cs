namespace Lightcore.Worlds
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Enumerators;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Drawing;

    public class AnimateWorld : WorldBuilder
    {
        public AnimateWorld(string filename, int seed = 0, int maxseed = 256) : base()
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

            var image = Image.FromFile("./Textures/Images/" + filename + ".jpg");
            var bitmap = new Bitmap(image);

            //var yStepSize =  (Math.PI * 4) / image.Height;
            var ystart = ((Math.PI * 2) / maxseed) * seed ;
            var xstepSize = (Math.PI*12) / image.Width;
            var ystepSize = (Math.PI * 6) / image.Height;

            var map = new Tuple<double, Vector>[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var e1 = Math.Cos(x * xstepSize);
                    var e2 = Math.Sin((y * ystepSize) + ystart);

                    var color = bitmap.GetPixel(x, y).ToVector();
                    var brightness = bitmap.GetPixel(x, y).GetBrightness();


                    map[x, y] = new Tuple<double, Vector>((e1*20) + (e2*60) + brightness * 5, color);
                }
            }

            bitmap.Dispose();
            image.Dispose();

            bitmap = null;
            image = null;

            Entities.Add(WorldUtils.Sphere(EntityType.World, new Vector(0, 0, 0), 100, map));
        }
    }
}
