namespace Lightcore.Test.Common.CommonUtils
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class NormalizeTests
    {
        [TestMethod]
        public void CartesianComplex()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(0, 0, -1),
                    new Vector(0, -1, 0),
                    new Vector(2, 0, 0)
                ),
                new Vector(1, 1, 1),
                ReferenceFrameType.Cartesian
            );

            Assert.AreEqual(new Vector(1, 1, 1), CommonUtils.Normalize(source, new Vector(0, 0, 0)));
            Assert.AreEqual(new Vector(1, 1, 0), CommonUtils.Normalize(source, new Vector(1, 0, 0)));
            Assert.AreEqual(new Vector(1, 0, 1), CommonUtils.Normalize(source, new Vector(0, 1, 0)));
            Assert.AreEqual(new Vector(3, 1, 1), CommonUtils.Normalize(source, new Vector(0, 0, 1)));
        }

        [TestMethod]
        public void SphericalSimple()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 0, 1),
                ReferenceFrameType.Spherical
            );

            Assert.AreEqual(new Vector(2, 0, 0), CommonUtils.Normalize(source, new Vector(1, 0, 0)));
            Assert.AreEqual(new Vector(Math.Sqrt(2), Math.PI/4, 0), CommonUtils.Normalize(source, new Vector(1, Math.PI/2, 0)));
        }

        [TestMethod]
        public void SphericalComplexR()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(2, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 0, 0),
                ReferenceFrameType.Spherical
            );

            Assert.AreEqual(new Vector(2, Math.PI / 2, 0), CommonUtils.Normalize(source, new Vector(1, Math.PI/2, 0)));
        }

        [TestMethod]
        public void SphericalFlipY()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, -1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 0, 0),
                ReferenceFrameType.Spherical
            );

            Assert.AreEqual(new Vector(1, Math.PI / 2, (3f/2*Math.PI)), CommonUtils.Normalize(source, new Vector(1, Math.PI/2, Math.PI/2)));
        }
    }
}
