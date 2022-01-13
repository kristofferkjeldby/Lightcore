namespace Lightcore.Test.Texture.ImageUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Lightcore.Textures;
    using Lightcore.Common.Models;

    [TestClass]
    public class TransformationsTests
    {
        [TestMethod]
        public void SquareToQuadrilateralTransformation()
        {
            var transformation1 = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                    new Vector(50, 50),
                    new Vector(100, 50),
                    new Vector(100, 100),
                    new Vector(50, 100)
                }
            );

            Assert.AreEqual(new Vector(0, 0, 0), transformation1(new Vector(0, 0, 0)));
            Assert.AreEqual(new Vector(50, 50, 0), transformation1(new Vector(1, 1, 0)));

            var transformation2 = ImageUtils.SquareToQuadrilateralTransformation(
                new Vector[] {
                    new Vector(1, 1),
                    new Vector(3, 1),
                    new Vector(3, 3),
                    new Vector(1, 3)
                }
            );

            Assert.AreEqual(new Vector(2, 2, 0), transformation2(new Vector(1, 1, 0)));
        }
    }
}
