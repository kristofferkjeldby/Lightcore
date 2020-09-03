namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void VectorTest()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(1, 2, 3);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
            Assert.IsFalse(a == b);
        }
    }
}
