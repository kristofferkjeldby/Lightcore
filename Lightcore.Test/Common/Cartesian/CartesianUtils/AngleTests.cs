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
            var w = new Vector(1, 0, 0);
            var nw = new Vector(1, 1, 0);
            var ne = new Vector(-1, 1, 0);
            var e = new Vector(-1, 0, 0);
            var se = new Vector(-1, -1, 0);
            var sw = new Vector(1, -1, 0);

            Assert.AreEqual(0f, CartesianUtils.Angle(w, w));
            Assert.AreEqual(0f, CartesianUtils.Angle(e, e));
            Assert.AreEqual(Constants.PIfouth, CartesianUtils.Angle(w, nw));
            Assert.AreEqual(Constants.PIfouth, CartesianUtils.Angle(w, sw));
            Assert.AreEqual(Constants.PIfouth * 3, CartesianUtils.Angle(w, ne));
            Assert.AreEqual(Constants.PIfouth * 3, CartesianUtils.Angle(w, se));
            Assert.AreEqual(Constants.PI, CartesianUtils.Angle(w, e));

            Assert.AreEqual(Constants.PIfouth, CartesianUtils.Angle(nw, w));
            Assert.AreEqual(Constants.PIfouth, CartesianUtils.Angle(sw, w));
            Assert.AreEqual(Constants.PIfouth * 3, CartesianUtils.Angle(ne, w));
            Assert.AreEqual(Constants.PIfouth * 3, CartesianUtils.Angle(se, w));
            Assert.AreEqual(Constants.PI, CartesianUtils.Angle(e, w));
        }
    }
}
