using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;


namespace Mp3GainLibTest
{
    [TestClass]
    public class HistoricalArrayShould
    {
        [TestMethod]
        public void GetDataForValidIndex()
        {
            var arr = new HistoricalArray(new[] { 1.1, 1.2, 1.3 }, 0);

            Assert.AreEqual(1.1, arr[0]);
            Assert.AreEqual(1.2, arr[1]);
            Assert.AreEqual(1.3, arr[2]);
        }


        [TestMethod]
        public void GetZeroForInvalidIndex()
        {
            var arr = new HistoricalArray(new[] { 1.1, 1.2, 1.3 }, 0);

            Assert.AreEqual(0.0, arr[-1]);
            Assert.AreEqual(0.0, arr[3]);
        }


        [TestMethod]
        public void GetHistoryWhenShiftedAndAssigned()
        {
            var arr = new HistoricalArray(new[] { 1.1, 1.2, 1.3 }, 2);
            arr.Shift(1);
            arr[0] = 2.1;

            Assert.AreEqual(2.1, arr[0]);
            Assert.AreEqual(1.3, arr[-1]);
            Assert.AreEqual(1.2, arr[-2]);
            Assert.AreEqual(0.0, arr[-3]);
        }


        [TestMethod]
        public void GetHistoryWhenShiftedAndSet()
        {
            var arr = new HistoricalArray(new[] { 1.1, 1.2, 1.3 }, 2);
            arr.Shift(1);
            arr.SetData(new[] { 2.1, 2.2 });

            Assert.AreEqual(2.1, arr[0]);
            Assert.AreEqual(1.3, arr[-1]);
            Assert.AreEqual(1.2, arr[-2]);
            Assert.AreEqual(0.0, arr[-3]);
        }


        [TestMethod]
        public void GetUnderlyingData()
        {
            var arr = new HistoricalArray(new[] { 1.1, 1.2, 1.3 }, 0);
            var u = arr.Underlying;
            Assert.AreEqual(1.1, u[0]);
            Assert.AreEqual(1.2, u[1]);
            Assert.AreEqual(1.3, u[2]);
        }
    }
}
