namespace Lightcore.Common.Spherical.Extensions
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Models;
    using System.Collections.Generic;

    public static class OrthodromeExtensions
    {
        public static Vector[] Intersect(this Orthodrome a, Orthodrome b)
        {
            var result = new List<Vector>();

            if (a.Normal.Unit() == b.Normal.Unit() || a.Normal.Unit() == -b.Normal.Unit())
                return result.ToArray();

            var t = (a.Normal % b.Normal).Unit(); // Line of intersection between the two great arc planes 

            var t1 = t.ToSpherical();
            var t2 = (-t).ToSpherical();

            if ((a.Phi.Includes(t1[Axis.Phi]) && b.Phi.Includes(t1[Axis.Phi])))
                result.Add(t1);


            if ((a.Phi.Includes(t2[Axis.Phi]) && b.Phi.Includes(t2[Axis.Phi])))
                result.Add(t2);

            return result.ToArray();
        }
    }
}
