namespace Lightcore.Test.Common.Cartesian.CartesianUtils
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AngleTests
    {
        [TestMethod]
        public void AngleTest()
        {
            Assert.AreEqual(0d, CartesianUtils.Angle(new Vector(1, 0, 0), new Vector(1, 0, 0)));
            Assert.AreEqual(Constants.PI / 2, CartesianUtils.Angle(new Vector(1, 0, 0), new Vector(0, 1, 0)));
            Assert.AreEqual(Constants.PI / 2, CartesianUtils.Angle(new Vector(1, 0, 0), new Vector(0, -1, 0)));
            Assert.AreEqual(Constants.PI, CartesianUtils.Angle(new Vector(1, 0, 0), new Vector(-1, 0, 0)));
        }
    }
}
