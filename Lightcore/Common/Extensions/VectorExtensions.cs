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
            return new Vector(Enumerable.Range(0, vectors.First().N).Select(n => vectors.Average(vector => vector[n])));
        }

        public static Vector Limit(this Vector vector, float min, float max)
        {
            return new Vector(vector.Elements.Select(element => CommonUtils.Limit(element, min, max)));
        }

        public static float Average(this Vector vector)
        {
            return (vector[0] + vector[1] + vector[2]) / 3;
        }

        public static float LeadCoefficient(this Vector vector, out int? index)
        {
            for (index = 0; index < vector.Elements.Length; index++)
            {
                if (vector[index] != 0)
                    return vector[index];
            }

            index = null;
            return float.NaN;
        }

        public static Vector ToCartesian(this Vector vector)
        {
            return new Vector(
                (vector[Axis.R] * CommonUtils.Sin(vector[Axis.Theta]) * CommonUtils.Cos(vector[Axis.Phi])),
                (vector[Axis.R] * CommonUtils.Sin(vector[Axis.Theta]) * CommonUtils.Sin(vector[Axis.Phi])),
                (vector[Axis.R] * CommonUtils.Cos(vector[Axis.Theta]))
            );
        }

        public static Vector ToSpherical(this Vector vector)
        {
            return new Vector(
                vector.Length(),
                CommonUtils.Limit(Angle.ToCyclicAngle((float)Math.Acos(vector[Axis.Z] / vector.Length())), 0, Constants.PI),
                CommonUtils.Limit(Angle.ToCyclicAngle((float)Math.Atan2(vector[Axis.Y], vector[Axis.X])), 0, Constants.PI2)
            );
        }
    }
}
