namespace Lightcore.Test.Common.Models.Spherical
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class OrthodromeTests
    {
        [TestMethod]
        public void NormalThetaTest()
        {
            var northPole = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, 0, 0));
            var equator = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, Constants.PI / 2, Constants.PI / 2));
            var north = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, 1f / 4 * Constants.PI, Constants.PI / 2));
            var south = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, 3f / 4 * Constants.PI, Constants.PI / 2));
            var southPole = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, Constants.PI, 0));

            Assert.AreEqual(0, CartesianUtils.Angle(northPole.Normal, northPole.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 2, CartesianUtils.Angle(northPole.Normal, equator.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 4, CartesianUtils.Angle(northPole.Normal, north.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 4, CartesianUtils.Angle(equator.Normal, south.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 4, CartesianUtils.Angle(south.Normal, southPole.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 2, CartesianUtils.Angle(equator.Normal, southPole.Normal), Constants.Delta);
            Assert.AreEqual(0, CartesianUtils.Angle(southPole.Normal, southPole.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI, CartesianUtils.Angle(northPole.Normal, southPole.Normal), Constants.Delta);
        }

        [TestMethod]
        public void NormalPhiTest()
        {
            var middle = new Orthodrome(new Vector(1, Constants.PI / 2, 0), new Vector(1, 0, 0));
            var east = new Orthodrome(new Vector(1, Constants.PI / 2, 1f / 4 * Constants.PI), new Vector(1, 0, 0));
            var west = new Orthodrome(new Vector(1, Constants.PI / 2, -1f / 4 * Constants.PI), new Vector(1, 0, 0));

            Assert.AreEqual(Math.PI / 4, CartesianUtils.Angle(middle.Normal, east.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 4, CartesianUtils.Angle(west.Normal, middle.Normal), Constants.Delta);
            Assert.AreEqual(Math.PI / 2, CartesianUtils.Angle(west.Normal, east.Normal), Constants.Delta);
        }
    }
}
