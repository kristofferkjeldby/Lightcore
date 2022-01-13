namespace Lightcore.Common
{
    using System;

    public static partial class CommonUtils
    {
        public static float Sin(float angle)
        {
            return (float)Math.Sin(angle);
        }

        public static float Cos(float angle)
        {
            return (float)Math.Cos(angle);
        }

        public static float Tan(float angle)
        {
            return (float)Math.Tan(angle);
        }

        public static float Acos(float x)
        {
            return (float)Math.Acos(x);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        public static float Abs(float x)
        {
            return Math.Abs(x);
        }

        public static int Sign(float x)
        {
            return Math.Sign(x);
        }
    }
}
