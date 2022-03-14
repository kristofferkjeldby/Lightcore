namespace Lightcore.Test.Texture.ImageUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Lightcore.Textures;
    using Lightcore.Common.Models;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Textures.Extensions;
    using System.Drawing;

    [TestClass]
    public class TransformationsTests
    {
        [TestMethod]
        public void SquareToQuadrilateralTransformation()
        {
            var identity = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1)
                }
            );

            Assert.IsTrue(identity.Matrix.IsIdentity);

            var scale1 = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                                new Vector(0, 0),
                                new Vector(2, 0),
                                new Vector(2, 2),
                                new Vector(0, 2)
                }
            );

            Assert.AreEqual(
                new Matrix(
                    new Vector(2, 0, 0),
                    new Vector(0, 2, 0),
                    new Vector(0, 0, 1)
                ),
                scale1.Matrix
            );

            var scale2 = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                    new Vector(0, 0),
                    new Vector(3, 0),
                    new Vector(3, 2),
                    new Vector(0, 2)
                }
            );

            Assert.AreEqual(
                new Matrix(
                    new Vector(3, 0, 0),
                    new Vector(0, 2, 0),
                    new Vector(0, 0, 1)
                ),
                scale2.Matrix
            );

            var translate = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                    new Vector(1, 0),
                    new Vector(2, 0),
                    new Vector(2, 1),
                    new Vector(1, 1)
                }
            );

            Assert.AreEqual(
                new Matrix(
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0),
                    new Vector(1, 0, 1)
                ),
                translate.Matrix
            );

            var shear = ImageUtils.SquareToQuadrilateralTransformation(
                    new Vector[] {
                        new Vector(0, 0),
                        new Vector(1, 0),
                        new Vector(2, 1),
                        new Vector(1, 1)
                    }
                );

            Assert.AreEqual(
                new Matrix(
                    new Vector(1, 0, 0),
                    new Vector(1, 1, 0),
                    new Vector(0, 0, 1)
                ),
                shear.Matrix
            );

            var nonAffine = ImageUtils.SquareToQuadrilateralTransformation(
                    new Vector[] {
                        new Vector(0, 0),
                        new Vector(1, 0),
                        new Vector(1.1f, 1),
                        new Vector(0, 1)
                    }
                );

            var xy = nonAffine.Transform(new Vector(1, 1)).ToPoint();

            Assert.AreEqual(new PointF(1.1f, 1), xy);
              
        }
    }
}
