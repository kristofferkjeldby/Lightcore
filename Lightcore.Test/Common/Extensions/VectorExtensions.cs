namespace Lightcore.Test.Common.Extensions
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VectorExtensions
    {
        [TestMethod]
        public void LeadCoefficient()
        {
            var v1 = new Vector(10);
            var v2 = new Vector(0, 0, 7, 3);

            var leadCoefficient = v2.LeadCoefficient(out var index);

            Assert.AreEqual(leadCoefficient, 7);
            Assert.AreEqual(index, 2);
        }
    }
}
