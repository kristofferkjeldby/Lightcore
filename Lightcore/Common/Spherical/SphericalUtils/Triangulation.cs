namespace Lightcore.Common.Spherical
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class SphericalUtils
    {
        public static List<SphericalTriangle> Triangulation(Vector[] vectors)
        {
            var triangles =  new List<SphericalTriangle>();
            var vds = vectors.Distinct().ToList();

            if (vds.Count < 3)
                return triangles;

            if (vds.Count == 3)
            {
                triangles.Add(new SphericalTriangle(vectors.ToArray()));
                return triangles;
            }

            var origin = vds[0];

            var axises = new SortedDictionary<int, Orthodrome>();

            for (int i = 1; i < vds.Count(); i ++)
            {
                var axis = new Orthodrome(vds[0], vds[i]);
                var index = vds.Count(vector => (vector.ToCartesian() * axis.Normal) > Constants.Delta);
                if (axises.ContainsKey(index))
                    axises[index] = axis.Angle > axises[index].Angle ? axis : axises[index];
                else
                    axises.Add(index, axis);
            }

            for (int i = 0; i < axises.Count - 1; i++)
            {
                triangles.Add(new SphericalTriangle(origin, axises.ElementAt(i).Value[1], axises.ElementAt(i + 1).Value[1]));
            }

            return triangles;
        }
    }
}
