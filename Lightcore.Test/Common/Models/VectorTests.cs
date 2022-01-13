namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Cartesian.Extensions;
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

            Assert.AreEqual(a, b);
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
            Assert.IsFalse(a == b);

            var x = new Vector(1, 0, 0);
            var y = new Vector(0, 1, 0);
            var z = new Vector(0, 0, 1);

            Assert.AreEqual((x % y), z);
        }

        [TestMethod]
        public void DotProduct()
        {
            var a = new Vector(2, 0, 0);
            var b = new Vector(-3, -3, 0);

            Assert.AreEqual(-6, (a * b));
            Assert.AreEqual(-6, (b * a));
        }

        [TestMethod]
        public void CrossProduct()
        {
            var a = new Vector(2, 0, 0);
            var b = new Vector(-3, -3, 0);

            Assert.AreEqual(6, (a % b).Length());
            Assert.AreEqual(6, (b % a).Length());
        }
    }
}
