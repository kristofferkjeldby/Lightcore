namespace Lightcore.Common
{
    using System;

    public static partial class CommonUtils
    {
        public static double Limit(double a, double min, double max)
        {
            if (double.IsPositiveInfinity(a)) return max;
            if (double.IsNaN(a)) return min;
            if (double.IsNegativeInfinity(a)) return min;

            if (a < min)
                return min;
            if (a > max)
                return max;
            return a;
        }

        public static int ToInt(double a)
        {
            return (int)Math.Round(a, 0);
        }
    }
}
