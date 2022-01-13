namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Transpose()
        {
            var M = new Matrix(
                new Vector(1, 2),
                new Vector(4, 5)
            );

            var T = new Matrix(
                new Vector(1, 4),
                new Vector(2, 5)
            );

            Assert.AreEqual(T, M.Transpose());

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
        }

        [TestMethod]
        public void Product()
        {
            var M1 = new Matrix(
                new Vector(2, 1, 3, 0),
                new Vector(-1, 3, 0, 0),
                new Vector(3, 0, -1, 0),
                new Vector(5, 4, -2, 1)
            );

            var v1 = new Vector(2, 0, -1, 1);

            var p1 = M1 * v1;

            Assert.AreEqual(new Vector(6, 6, 5, 1), p1);

            var M2 = new Matrix(
                new Vector(13, 8, 6),
                new Vector(9, 7, 4),
                new Vector(7, 4, 0),
                new Vector(15, 6, 3)
            );

            var v2 = new Vector(3, 4, 2);

            var p2 = v2 * M2;

            Assert.AreEqual(new Vector(83, 63, 37, 75), p2);

            var M3 = new Matrix(
                new Vector(1, 4),
                new Vector(2, 5),
                new Vector(3, 6)
            );

            var M4 = new Matrix(
                new Vector(10, 20, 30),
                new Vector(11, 21, 31)
            );

            Assert.AreEqual(
            new Matrix(
                new Vector(140, 320),
                new Vector(146, 335)
            ),
            M3 * M4);
        }

        public void Identity()
        {
            var M1 = new Matrix(
                new Vector(10, 20),
                new Vector(11, 21)
            );

            Assert.IsFalse(M1.IsIdentity);
            Assert.IsTrue(M1.IsSquare);

            var M2 = new Matrix(
                new Vector(1, 0, 0),
                new Vector(0, 1, 0),
                new Vector(0, 0, 1)
            );

            Assert.IsTrue(M1.IsIdentity);
            Assert.IsTrue(M1.IsSquare);

            var M3 = Matrix.Identity(3);

            Assert.AreEqual(M2, M3);
        }

        [TestMethod]
        public void Dimensions()
        {
            var c = new Matrix(8, 7);
            Assert.AreEqual(c.N, 8);
            Assert.AreEqual(c.M, 7);
        }
    }
}
