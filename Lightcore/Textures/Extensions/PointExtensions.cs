namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common.Models;
    using System.Drawing;

    public static class PointExtensions
    {
        public static Vector ToVector(this PointF point)
        {
            return new Vector(point.X, point.Y);
        }

        public static PointF ToPoint(this Vector vector)
        {
            return new PointF(vector[Axis.X], vector[Axis.Y]);
        }
    }
}
