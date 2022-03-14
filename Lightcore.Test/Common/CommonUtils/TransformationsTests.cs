namespace Lightcore.Test.Common.CommonUtils
{
    using Lightcore.Common.Models;
    using Lightcore.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransformationsTests
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

            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            Assert.AreEqual(new Vector(1, 2, 37), transformation.Transform(new Vector(1, 2, 3)));
            Assert.AreEqual(new Vector(3, 4, 40), transformation.Transform(new Vector(3, 4, 0)));
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

            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            Assert.AreEqual(new Vector(1, Constants.PI / 2, 0), transformation.Transform(new Vector(1, 0, 1)));
            Assert.AreEqual(new Vector(1, Constants.PI, 0), transformation.Transform(new Vector(0, 0, 0)));
        }

        [TestMethod]
        public void SpericalToCartesian()
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

            var destination = new ReferenceFrame
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

            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            Assert.AreEqual(new Vector(1, 0, 1), transformation.Transform(new Vector(1, Constants.PI / 2, 0)));
            Assert.AreEqual(new Vector(0, 0, 0), transformation.Transform(new Vector(1, Constants.PI, 0)));
        }

        [TestMethod]
        public void SpericalToSperical()
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

            var destination = new ReferenceFrame
            (
                new Matrix
                (
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(0, 0, 1)
                ),
                new Vector(0, 1, 0),
                ReferenceFrameType.Spherical
            );

            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            Assert.AreEqual(new Vector(1.73205f, 0.95532f, 5.49779f), transformation.Transform(new Vector(1, Constants.PI / 2, 0)));
            Assert.AreEqual(new Vector(1, Constants.PIhalf, 4.71239f), transformation.Transform(new Vector(1, Constants.PI, 0)));
        }
    }
}
