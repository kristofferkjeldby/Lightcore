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

        public void Slice(int x, int y, ImageTextureBuilderStartPosition startPosition = ImageTextureBuilderStartPosition.BottomLeft)
        {
            XSliceSize = (float)Image.Width / x;
            YSliceSize = (float)Image.Height / y;
            StartPosition = startPosition;
        }

        public RectangleF GetSlice(int x, int y)
        {
            var startX = x * XSliceSize;
            var startY = StartPosition == ImageTextureBuilderStartPosition.BottomLeft ? y * YSliceSize : Image.Height - ((y  + 1) * YSliceSize);

            var width = Math.Min(Image.Width - startX, XSliceSize);
            var height = Math.Min(Image.Height - startY, YSliceSize);

            return new RectangleF(startX, startY, width, height);
        }

        public float XSliceSize { get; set; }

        public float YSliceSize { get; set; }

        public ImageTextureBuilderStartPosition StartPosition { get; set; }

        public float Reflectance { get; set; }

        public float Transparency { get; set; }

        public float Metallicity { get; set; }

        public float Shiny { get; set; }

        public Texture GetTexture(Vector color, int x, int y)
        {
            return new ImageTexture(color, Image, GetSlice(x, y), Reflectance, Transparency, Metallicity, Shiny);
        }
    }
}
