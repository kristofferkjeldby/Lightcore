namespace Lightcore.Test.Common.Extensions
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MatrixExtensions
    {
        [TestMethod]
        public void EchelonForm()
        {
            var matrix = new Matrix(
                new Vector(1, -2),
                new Vector(5, -7),
                new Vector(7, -5)
            );

            var echolonForm = matrix.ToEchelonForm();

            Assert.AreEqual(
                new Matrix(
                    new Vector(1, 0),
                    new Vector(0, 1),
                    new Vector(-8, 3)
                ),
                echolonForm);
        }

        [TestMethod]
        public void JoinAndSplit()
        {
            var matrix1 = new Matrix(
                new Vector(1, 0),
                new Vector(0, 1)
            );

            var matrix2 = new Matrix(
                new Vector(2, 2),
                new Vector(2, 2)
            );

            var joined = matrix1.Join(matrix2);

            Assert.AreEqual(
                new Matrix(
                    new Vector(1, 0),
                    new Vector(0, 1),
                    new Vector(2, 2),
                    new Vector(2, 2)
                ),
                joined
            );

            var splited = joined.Split(2);

            Assert.AreEqual(matrix1, splited.Item1);
            Assert.AreEqual(matrix2, splited.Item2);
        }

        [TestMethod]
        public void Invert()
        {
            var matrix1 = new Matrix(
                new Vector(2, 2),
                new Vector(4, 4)
            );

            var inverted1 = matrix1.Invert();

            Assert.AreEqual(
                null,
                inverted1
            );

            var matrix2 = new Matrix(
                new Vector(-1, 1),
                new Vector(3f / 2f, -1)
            );

            var inverted2 = matrix2.Invert();

            Assert.AreEqual(
                new Matrix(
                    new Vector(2, 2),
                    new Vector(3, 2)
                ),
                inverted2
            );
        }

        [TestMethod]
        public void Determinant()
        {
            var matrix1 = new Matrix(
                new Vector(3, 1),
                new Vector(7, -4)
            );

            var determinant = matrix1.Determinant();

            Assert.AreEqual(
                -19,
                determinant
            );
        }
    }
}
