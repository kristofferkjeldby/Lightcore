namespace Lightcore.Test.Common.Spherical.Extensions
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical;
    using Lightcore.Common.Spherical.Extensions;
    using Lightcore.Common.Spherical.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrthodromeExtensionsTests
    {
        [TestMethod]
        public void IntersectTests()
        {
            var a = new Orthodrome(new Vector(1, 0, 0), new Vector(1, Constants.PI/2, 0));
            var b = new Orthodrome(new Vector(1, Constants.PI/2, 0), new Vector(1, Constants.PI/2, Constants.PI/2));
            var intersect = a.Intersect(b);
            Assert.IsTrue(intersect.Length == 1);
            Assert.IsTrue(intersect[0].Equals(new Vector(1, Constants.PI / 2, 0)));
        }
    }
}
