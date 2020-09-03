namespace Lightcore.Test.Common.Spherical.Extensions
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Extensions;
    using Lightcore.Common.Spherical.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class SphericalTrianglesExtensionsTests 
    {
        [TestMethod]
        public void AreaTest()
        {
            var northPole = new Vector(1, 0, 0);
            var equator = new Vector(1, Math.PI / 2, 0);
            var north = new Vector(1, Math.PI / 4, Math.PI / 2);
            var west = new Vector(1, Math.PI / 2, Math.PI / 2);
            var middlewest = new Vector(1, Math.PI / 2, 1d / 4 * Math.PI);

            Assert.AreEqual(4d / 16 * Math.PI, new SphericalTriangle(northPole, equator, north).Area(), Constants.Delta);
            Assert.AreEqual(4d / 16 * Math.PI, new SphericalTriangle(equator, north, west).Area(), Constants.Delta);
            Assert.AreEqual(4d / 8 * Math.PI, new SphericalTriangle(northPole, equator, west).Area(), Constants.Delta);
            Assert.AreEqual(4d / 8 * Math.PI, new SphericalTriangle(equator, northPole, west).Area(), Constants.Delta);
            Assert.AreEqual(4d / 16 * Math.PI, new SphericalTriangle(equator, northPole, middlewest).Area(), Constants.Delta);
        }

        [TestMethod]
        public void IncludesTests()
        {
            var triangle = new SphericalTriangle
            (
                new[] {
                    new Vector(1, 0, 0),
                    new Vector(1, Constants.PI / 2, 0),
                    new Vector(1, Constants.PI / 2, Constants.PI / 2)
                }
            );

            Assert.IsTrue(triangle.Includes(new Vector(1, Constants.PI / 2, 0)));
            Assert.IsFalse(triangle.Includes(new Vector(1, Constants.PI, 0)));
            Assert.IsTrue(triangle.Includes(new Vector(1, (Constants.PI / 2) - 0.5d, (Constants.PI / 2) - 0.5d)));
        }

        [TestMethod]
        public void IntersectionTests()
        {
            var a = new SphericalTriangle
            (
                new[] {
                    new Vector(1, 0, 0),
                    new Vector(1, Constants.PI / 2, 0),
                    new Vector(1, Constants.PI / 2, Constants.PI / 2)
                }
            );

            var b = new SphericalTriangle
            (
                new[] {
                    new Vector(1, 0, 0),
                    new Vector(1, (Constants.PI / 2) + 0.5f, 0.5f),
                    new Vector(1, (Constants.PI / 2) + 0.5f, (Constants.PI / 2) + 0.5f)
                }
            );

            var intersections = a.Intersection(b).ToList();

            Assert.IsTrue(intersections.Count() == 3);
            Assert.IsTrue(intersections.Contains(new Vector(1, 0, 0)));
            Assert.IsTrue(intersections.Contains(new Vector(1, Constants.PI / 2, 0.5f)));
            Assert.IsTrue(intersections.Contains(new Vector(1, Constants.PI / 2, Constants.PI / 2)));

        }
    }
}
