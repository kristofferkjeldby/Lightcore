namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AngleTests
    {
        [TestMethod]
        public void AngleTest()
        {
            Assert.AreEqual(Constants.PI, new Angle(Constants.PI), Constants.Delta);
            Assert.AreEqual(0f, new Angle(Constants.PI) + new Angle(-Constants.PI), Constants.Delta);
            Assert.AreEqual(0f, new Angle(Constants.PI) + new Angle(Constants.PI), Constants.Delta);
            Assert.AreEqual(0.5f * Constants.PI, new Angle(-3f/2f * Constants.PI), Constants.Delta);
            Assert.AreEqual(Constants.PI, new Angle(Constants.PI) + new Angle(Constants.PI) + new Angle(Constants.PI), Constants.Delta);
        }
    }
}
