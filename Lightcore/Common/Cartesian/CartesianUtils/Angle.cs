namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;
    using System;

    public static partial class CartesianUtils
    {
        public static Angle Angle(Vector a, Vector b)
        {
            return new Angle(Math.Atan2((a % b).Length(), a * b));
        }

        public static Angle Angle(Line a, Line b)
        {
            return Angle(a.Direction, b.Direction);
        }
    }
}
