namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common.Models;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public static class PointExtensions
    {
        public static Vector ToVector(this PointF point, float? plane = null)
        {
            // Homogeneous 
            if (plane.HasValue)
                return new Vector(point.X, point.Y, plane.Value);

            return new Vector(point.X, point.Y);
        }

        public static PointF ToPoint(this Vector vector)
        {
            // Homogeneous 
            if (vector.N == 3)
                return new PointF(vector[Axis.X] / vector[Axis.Z], vector[Axis.Y] / vector[Axis.Z]);

            return new PointF(vector[Axis.X], vector[Axis.Y]);
        }

        public static PointF[] ApplyOverscan(this PointF[] points, int overscan)
        {
            var averageX = points.Average(p => p.X);
            var averageY = points.Average(p => p.Y);

            var result = new List<PointF>();

            foreach (var point in points)
            {
                var x = point.X < averageX ? point.X - overscan : point.X + overscan;
                var y = point.Y < averageY ? point.Y - overscan : point.Y + overscan;

                result.Add(new PointF(x, y));
            }

            return result.ToArray();
        }
    }
}
