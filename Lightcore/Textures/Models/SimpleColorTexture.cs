namespace Lightcore.Textures.Models
{
    using System.Drawing;
    using Lightcore.Common.Models;
    using Lightcore.Textures.Extensions;

    public class SimpleColorTexture : Texture
    {
        public SimpleColorTexture(Vector color, float transparency = 0)
        {
            Color = color;
            Transparency = transparency;
        }

        public Vector Color { get; set; }

        public override Texture Clone()
        {
            return new SimpleColorTexture(Color, Transparency);
        }

        public override Brush GetBrush(Polygon polygon, PointF[] points)
        {
            return new SolidBrush(Color.ToColor(Transparency));
        }

        public float Transparency { get; set; }
    }
}
