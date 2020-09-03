namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Cartesian.Extensions;

    public static partial class CartesianUtils
    {
        public static Vector Reflect(Vector a, Vector normal)
        {
            var n = normal.Unit();
            return a - 2 * (a * n) * n;
        }
    }
}
