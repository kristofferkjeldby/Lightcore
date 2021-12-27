namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common;
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using System.Drawing;

    public static class ColorExtensions
    {
        public static Vector ToVector(this Color color)
        {
            return new Vector(color.R, color.G, color.B)/byte.MaxValue;
        }

        public static Color ToColor(this Vector vector, float transparency = 0)
        {
            var limited = byte.MaxValue * vector.Limit(0, 1);
            var alpha = byte.MaxValue - CommonUtils.Limit(transparency, 0, 1) * byte.MaxValue;

            return Color.FromArgb(
                //CommonUtils.ToInt(alpha),
                CommonUtils.ToInt(limited[0]),
                CommonUtils.ToInt(limited[1]),
                CommonUtils.ToInt(limited[2])
                );
        }
    }
}
