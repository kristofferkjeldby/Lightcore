using System;

namespace Lightcore
{
    public struct Constants
    {
        public const int DeltaDecimals = 10;
        public static string DeltaFormat = $"n{DeltaDecimals}";
        public static double Delta = Math.Pow(0.1f, DeltaDecimals);
        public const double PI = Math.PI;
        public const double PI2 = PI * 2;
    }
}
