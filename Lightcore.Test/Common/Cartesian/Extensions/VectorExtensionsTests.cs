namespace Lightcore.Test.Common.Cartesian.Cartesian.Extensions
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VectorExtensionsTests
    {
        [TestMethod]
        public void Length()
        {
            var f = new Vector(1, 2, 3, 4);
            Assert.AreNotEqual(f.Length(), 1);
            Assert.AreEqual(f.Unit().Length(), 1);
        }
    }
}
