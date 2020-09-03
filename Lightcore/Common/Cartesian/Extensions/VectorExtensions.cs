namespace Lightcore.Common.Cartesian.Extensions
{
    using Lightcore.Common.Models;
    using System;
    using System.Linq;

    public static class VectorExtensions
    {
        public static double Length(this Vector vector)
        {
            return Math.Sqrt(vector.Elements.Sum(value => Math.Pow(value, 2)));
        }

        public static Vector Unit(this Vector vector)
        {
            return vector / vector.Length();
        }
    }
}
