namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;

    public static partial class CartesianUtils
    {
        public static Angle Angle(Vector a, Vector b)
        {
            return new Angle(CommonUtils.Acos(a * b / (a.Length() * b.Length())));
        }
    }
}
