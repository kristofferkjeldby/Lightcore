namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common;
    using Lightcore.Common.Models;

    public static class VectorExtensions
    {
        public static Angle Angle2D(this Vector vector)
        {
            return new Angle(CommonUtils.Atan2(vector[Axis.Y], vector[Axis.X]));
        }


        public static float Slope2D(this Vector vector)
        {
            // Avoid dividing by zero
            if (CommonUtils.Abs(vector[Axis.X]) < Constants.Delta)
            { 
                if (vector[Axis.Y] > 0)
                    return float.PositiveInfinity;
                if (vector[Axis.Y] < 0)
                    return float.NegativeInfinity;
                return float.NaN;
            }

            return vector[Axis.Y] / vector[Axis.X];
        }
    }
}
