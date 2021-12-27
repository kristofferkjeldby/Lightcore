namespace Lightcore.Common
{
    using System;

    public static partial class CommonUtils
    {
        public static float Limit(float a, float min, float max)
        {
            if (float.IsPositiveInfinity(a)) return max;
            if (float.IsNaN(a)) return min;
            if (float.IsNegativeInfinity(a)) return min;

            if (a < min)
                return min;
            if (a > max)
                return max;
            return a;
        }

        public static int ToInt(float a)
        {
            return (int)Math.Round(a, 0);
        }
    }
}
