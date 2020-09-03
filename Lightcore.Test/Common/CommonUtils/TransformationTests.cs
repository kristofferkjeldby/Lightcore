namespace Lightcore.Test.Common.CommonUtils
{
    using Lightcore.Common.Models;
    using Lightcore.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TransformationTests
    {
        [TestMethod]
        public void CartesianToCartesian()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 0, 0),
                ReferenceFrameType.Cartesian
            );

            var destination = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, -1)
                ),
                new Vector(0, 0, 40),
                ReferenceFrameType.Cartesian
            );

            var transformation = CommonUtils.Transformation(source, destination);

            Assert.AreEqual(new Vector(1, 2, 37), transformation(new Vector(1, 2, 3)));
        }

        [TestMethod]
        public void CartesianToSperical()
        {
            var source = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 0, 0),
                ReferenceFrameType.Cartesian
            );

            var destination = new ReferenceFrame
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

            var transformation = CommonUtils.Transformation(source, destination);

            Assert.AreEqual(new Vector(1, Math.PI / 2, 0), transformation(new Vector(1, 0, 1)));
            Assert.AreEqual(new Vector(1, Math.PI, 0), transformation(new Vector(0, 0, 0)));
        }
    }
}
