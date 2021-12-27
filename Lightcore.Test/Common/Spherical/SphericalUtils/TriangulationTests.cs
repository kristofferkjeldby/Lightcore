namespace Lightcore.Test.Common.Spherical.Extensions
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical;
    using Lightcore.Common.Spherical.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class TriangulationTests
    {
        [TestMethod]
        public void TriangulationTest()
        {
            var vectors = new List<Vector>();
            vectors.Add(new Vector(1, 0, 0));
            vectors.Add(new Vector(1, 0.5f, 0));
            vectors.Add(new Vector(1, 0.5f, 0.5f));
            vectors.Add(new Vector(1, 0.5f, Angle.ToCyclicAngle(-0.5f)));
            vectors.Add(new Vector(1, 1f, 0));

            var triangles = SphericalUtils.Triangulation(vectors.ToArray());

            Assert.AreEqual(2, triangles.Count, Constants.Delta);
            Assert.AreEqual(triangles[0].Area(), triangles[1].Area(), Constants.Delta);



        }
    }
}
