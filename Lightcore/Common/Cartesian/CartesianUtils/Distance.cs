namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;

    public static partial class CartesianUtils
    {
        public static float Distance(Line a, Vector point)
        {
            return (a.Direction % (point - a.Origin)).Length() / a.Direction.Length();
        }
    }
}
