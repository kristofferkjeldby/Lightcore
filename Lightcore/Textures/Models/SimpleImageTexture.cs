namespace Lightcore.Textures.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Extensions;
    using System.Drawing;

    public class SimpleImageTexture : Texture, IImageTexture
    {
        public Bitmap Image { get; set; }

        public Vector Color { get; set; }

        public SimpleImageTexture(Bitmap image)
        {
            Image = image;
        }

        public override Brush GetBrush(Polygon polygon, PointF[] points)
        {
            if (points.Length != 4)
                return new TextureBrush(Image);

            var mappedImage = Image.ForwardMap(points);

            var brush = new TextureBrush(mappedImage);

            return brush;
        }

        public override Texture Clone()
        {
            return new SimpleImageTexture(Image);
        }
    }
}
