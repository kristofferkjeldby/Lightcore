namespace Lightcore.Textures.Models
{
    using Lightcore.Common.Models;
    using System.Drawing;

    public abstract class Texture : IClonable<Texture>
    {
        public abstract Texture Clone();

        public virtual Brush GetBrush(Polygon polygon, PointF[] points) { return null; }
    }
}
