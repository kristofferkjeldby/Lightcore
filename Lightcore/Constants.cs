using System;

namespace Lightcore
{
    public struct Constants
    {
        public const int DeltaDecimals = 10;
        public static string DeltaFormat = $"n{DeltaDecimals}";
        public static float Delta = (float)Math.Pow(0.1f, DeltaDecimals);
        public const float PI = (float)Math.PI;
        public const float PI2 = PI * 2;
    }
}
