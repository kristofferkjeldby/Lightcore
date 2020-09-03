namespace Lightcore.Common.Extensions
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class VectorExtensions
    {
        public static Vector Average(this IEnumerable<Vector> vectors)
        {
            return new Vector(
                vectors.Average(vector => vector[0]),
                vectors.Average(vector => vector[1]),
                vectors.Average(vector => vector[2])
                );
        }

        public static Vector Limit(this Vector vector, double min, double max)
        {
            return new Vector(
                CommonUtils.Limit(vector[0], min, max),
                CommonUtils.Limit(vector[1], min, max),
                CommonUtils.Limit(vector[2], min, max)
            );
        }

        public static double Average(this Vector vector)
        {
            return (vector[0] + vector[1] + vector[2]) / 3;
        }

        public static Vector ToCartesian(this Vector vector)
        {
            return new Vector(
                (vector[Axis.R] * Math.Sin(vector[Axis.Theta]) * Math.Cos(vector[Axis.Phi])),
                (vector[Axis.R] * Math.Sin(vector[Axis.Theta]) * Math.Sin(vector[Axis.Phi])),
                (vector[Axis.R] * Math.Cos(vector[Axis.Theta]))
            );
        }

        public static Vector ToSpherical(this Vector vector)
        {
            return new Vector(
                vector.Length(),
                CommonUtils.Limit(Angle.ToCyclicAngle(Math.Acos(vector[Axis.Z] / vector.Length())), 0, Math.PI),
                CommonUtils.Limit(Angle.ToCyclicAngle(Math.Atan2(vector[Axis.Y], vector[Axis.X])), 0, Math.PI * 2)
            );
        }
    }
}
