namespace Lightcore.Test.Common.Models
{
    using Lightcore.Common.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AngleSpanTests
    {
        [TestMethod]
        public void DirectionAndInclude()
        {
            var quaterPi = Constants.PI / 4;

            var case0 = new AngleSpan(1, 1);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, case0.AngleSpanDirection);
            Assert.AreEqual(Constants.PI2, case0.Length, Constants.Delta);
            Assert.IsTrue(case0.Includes(0));
            Assert.IsTrue(case0.Includes(Constants.PI));

            var case0f = new AngleSpan(2, 2, AngleSpanDirection.Clockwise);
            Assert.AreEqual(AngleSpanDirection.Clockwise, case0f.AngleSpanDirection);
            Assert.AreEqual(0, case0f.Length, Constants.Delta);
            Assert.IsTrue(case0f.Includes(2));
            Assert.IsTrue(!case0f.Includes(Constants.PI));

            var case1 = new AngleSpan(quaterPi, 3f * quaterPi);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, case1.AngleSpanDirection);
            Assert.AreEqual(2 * quaterPi, case1.Length, Constants.Delta);
            Assert.IsTrue(!case1.Includes(0));
            Assert.IsTrue(case1.Includes(2f * quaterPi));
            Assert.IsTrue(!case1.Includes(Constants.PI));

            var case1f = new AngleSpan(quaterPi, 3f * quaterPi, AngleSpanDirection.Clockwise);
            Assert.AreEqual(AngleSpanDirection.Clockwise, case1f.AngleSpanDirection);
            Assert.AreEqual(-6 * quaterPi, case1f.Length, Constants.Delta);
            Assert.IsTrue(case1f.Includes(0));
            Assert.IsTrue(!case1f.Includes(2f * quaterPi));
            Assert.IsTrue(case1f.Includes(Constants.PI));

            var case2 = new AngleSpan(3f * quaterPi, quaterPi);
            Assert.AreEqual(AngleSpanDirection.Clockwise, case2.AngleSpanDirection);
            Assert.AreEqual(-2 * quaterPi, case2.Length, Constants.Delta);
            Assert.IsTrue(!case2.Includes(0));
            Assert.IsTrue(case2.Includes(2f * quaterPi));
            Assert.IsTrue(!case2.Includes(Constants.PI));

            var case2f = new AngleSpan(3f * quaterPi, quaterPi, AngleSpanDirection.CounterClockwise);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, case2f.AngleSpanDirection);
            Assert.AreEqual(6 * quaterPi, case2f.Length, Constants.Delta);
            Assert.IsTrue(case2f.Includes(0));
            Assert.IsTrue(!case2f.Includes(2f * quaterPi));
            Assert.IsTrue(case2f.Includes(Constants.PI));

            var case3 = new AngleSpan(7f * quaterPi, quaterPi);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, case3.AngleSpanDirection);
            Assert.AreEqual(2 * quaterPi, case3.Length, Constants.Delta);
            Assert.IsTrue(!case3.Includes(6f * quaterPi));
            Assert.IsTrue(case3.Includes(0));
            Assert.IsTrue(!case3.Includes(2f * quaterPi));

            var case3f = new AngleSpan(7f * quaterPi, quaterPi, AngleSpanDirection.Clockwise);
            Assert.AreEqual(AngleSpanDirection.Clockwise, case3f.AngleSpanDirection);
            Assert.AreEqual(-6 * quaterPi, case3f.Length, Constants.Delta);
            Assert.IsTrue(case3f.Includes(6f * quaterPi));
            Assert.IsTrue(!case3f.Includes(0));
            Assert.IsTrue(case3f.Includes(2f * quaterPi));

            var case4 = new AngleSpan(quaterPi, 7f * quaterPi);
            Assert.AreEqual(AngleSpanDirection.Clockwise, case4.AngleSpanDirection);
            Assert.AreEqual(-2 * quaterPi, case4.Length, Constants.Delta);
            Assert.IsTrue(!case4.Includes(6f * quaterPi));
            Assert.IsTrue(case4.Includes(0));
            Assert.IsTrue(!case4.Includes(2f * quaterPi));

            var case4f = new AngleSpan(quaterPi, 7f * quaterPi, AngleSpanDirection.CounterClockwise);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, case4f.AngleSpanDirection);
            Assert.AreEqual(6 * quaterPi, case4f.Length, Constants.Delta);
            Assert.IsTrue(case4f.Includes(6f * quaterPi));
            Assert.IsTrue(!case4f.Includes(0));
            Assert.IsTrue(case4f.Includes(2f * quaterPi));
        }

        [TestMethod]
        public void Union()
        {
            var quaterPi = Constants.PI / 4;

            var middle1 = new AngleSpan(7f * quaterPi, quaterPi);
            var c1 = new AngleSpan(6f * quaterPi, 0);
            var cc1 = new AngleSpan(0, 2f * quaterPi);

            var middlecc = middle1.Union(cc1);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, middlecc.AngleSpanDirection);
            Assert.AreEqual(3 * quaterPi, middlecc.Length, Constants.Delta);

            var middlec = middle1.Union(c1);
            Assert.AreEqual(AngleSpanDirection.CounterClockwise, middlecc.AngleSpanDirection);
            Assert.AreEqual(3 * quaterPi, middlec.Length, Constants.Delta);

            Assert.IsNull(middle1.Union(new AngleSpan(2f * quaterPi, 6f * quaterPi, AngleSpanDirection.CounterClockwise)));

            var middle2= new AngleSpan(quaterPi, 7f * quaterPi);
            var c2 = new AngleSpan(0, 6f * quaterPi);
            var cc2 = new AngleSpan(2f * quaterPi, 0);

            var middlecc2 = middle2.Union(cc2);
            Assert.AreEqual(AngleSpanDirection.Clockwise, middlecc2.AngleSpanDirection);
            Assert.AreEqual(-3 * quaterPi, middlecc2.Length, Constants.Delta);

            var middlec2 = middle2.Union(c2);
            Assert.AreEqual(AngleSpanDirection.Clockwise, middlecc2.AngleSpanDirection);
            Assert.AreEqual(-3 * quaterPi, middlec2.Length, Constants.Delta);

            Assert.IsNull(middle2.Union(new AngleSpan(2f * quaterPi, 6f * quaterPi, AngleSpanDirection.CounterClockwise)));
        }
    }
}
