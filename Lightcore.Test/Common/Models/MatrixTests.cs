namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixTest()
        {
            var a = new Matrix(
                new Vector(1, 2, 3),
                new Vector(4, 5, 6),
                new Vector(7, 8, 9)
            );

            var b = new Matrix(
                new Vector(10, 11, 12),
                new Vector(13, 14, 15),
                new Vector(16, 17, 18)
            );
            Assert.AreEqual((a * b).Transpose(), b.Transpose() * a.Transpose());
            Assert.AreEqual(a * b, (b.Transpose() * a.Transpose()).Transpose());

            var v = new Vector(19, 20, 21);

            Assert.AreEqual(a * v, v * a.Transpose());
        }
    }
}
