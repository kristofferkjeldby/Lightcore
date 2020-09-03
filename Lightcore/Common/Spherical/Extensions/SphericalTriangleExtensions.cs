namespace Lightcore.Common.Spherical.Extensions
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SphericalTriangleExtensions
    {
        public static bool Includes(this SphericalTriangle a, Vector b)
        {
            var bc = b.ToCartesian();

            var n01 = new GreatCircle(a[0], a[1]).Normal;

            if (Math.Sign(a[2].ToCartesian() * n01) != Math.Sign(bc * n01) && Math.Sign(bc * n01) != 0)
                return false;

            var n12 = new GreatCircle(a[1], a[2]).Normal;

            if (Math.Sign(a[0].ToCartesian() * n12) != Math.Sign(bc * n12) && Math.Sign(bc * n12) != 0)
                return false;

            var n20 = new GreatCircle(a[2], a[0]).Normal;

            if (Math.Sign(a[1].ToCartesian() * n20) != Math.Sign(bc * n20) && Math.Sign(bc * n20) != 0)
                return false;

            return true;
        }

        public static Vector[] Intersection(this SphericalTriangle a, SphericalTriangle b)
        {
            var vectors = new List<Vector>();

            for (int ai = 0; ai < a.Orthodromes.Length; ai ++)
            {
                for (int bi = 0; bi < b.Orthodromes.Length; bi++)
                {
                    vectors.AddRange(a.Orthodromes[ai].Intersect(b.Orthodromes[bi]));
                }
            }

            for (int ai = 0; ai < a.Count(); ai++)
            {
                if (b.Includes(a[ai]))
                    vectors.Add(a[ai]);
            }

            for (int bi = 0; bi < b.Count(); bi++)
            {
                if (a.Includes(b[bi]))
                    vectors.Add(b[bi]);
            }

            return vectors.Distinct().ToArray();
        }

        public static Orthodrome[] ToOrthodromes(this SphericalTriangle a)
        {
            return new[]
            {
                new Orthodrome(a[0], a[1]),
                new Orthodrome(a[1], a[2]),
                new Orthodrome(a[2], a[0])
            };
        }

        public static double Area(this SphericalTriangle a)
        {
            var orthodromes = a.ToOrthodromes();
            var angle0 = (Math.PI) - CartesianUtils.Angle(orthodromes[2].Normal, orthodromes[0].Normal);
            var angle1 = (Math.PI) - CartesianUtils.Angle(orthodromes[0].Normal, orthodromes[1].Normal);
            var angle2 = (Math.PI) - CartesianUtils.Angle(orthodromes[1].Normal, orthodromes[2].Normal);

            return (angle0 + angle1 + angle2 - Math.PI);
        }

    }
}
