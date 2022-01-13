using Lightcore.Common.Models;
using System;
using System.Drawing;

namespace Lightcore.Textures.Models
{
    public class ImageTextureBuilder
    {
        public Bitmap Image { get; set; }

        public ImageTextureBuilder(Bitmap image, float reflectance = 0.2f, float transparency = 0, float metallicity = 0, float shiny = 0.2f)
        {
            Image = image;
            Reflectance = reflectance;
            Transparency = transparency;
            Metallicity = metallicity;
            Shiny = shiny;
        }

        public void Slice(int x, int y)
        {
            XSliceSize = (float)Image.Width / x;
            YSliceSize = (float)Image.Height / y;
        }

        public RectangleF GetSlice(int x, int y)
        {
            var startX = x * XSliceSize;
            var startY = y * YSliceSize;

            var width = Math.Min(Image.Width - startX - 0.1f, XSliceSize);
            var height = Math.Min(Image.Height - startY - 0.1f, YSliceSize);

            return new RectangleF(startX, startY, width, height);
        }

        public float XSliceSize { get; set; }

        public float YSliceSize { get; set; }

        public float Reflectance { get; set; }

        public float Transparency { get; set; }

        public float Metallicity { get; set; }

        public float Shiny { get; set; }

        public Texture GetTexture(Vector color, RectangleF? crop)
        {
            return new ImageTexture(color, Image, crop, Reflectance, Transparency, Metallicity, Shiny);
        }

        public Texture GetTexture(Vector color, int x, int y)
        {
            return new ImageTexture(color, Image, GetSlice(x, y), Reflectance, Transparency, Metallicity, Shiny);
        }
    }
}
