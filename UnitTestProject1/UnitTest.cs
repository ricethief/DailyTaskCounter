
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod2()
        {
          //  DailyTaskCounter.MainPage mainPage = new DailyTaskCounter.MainPage();

        }

        [TestMethod]
        public void TestMethod1()
        {
            ss p = new ss();
            decimal a = 10;
            decimal b = 20;

            decimal sum = p.AddNumber(a, b);
            Assert.AreEqual(sum, 31);
        }
    }
    public class ss
    {
        public decimal AddNumber(decimal a, decimal b)
        {
            decimal sum = a + b;
            return sum;
        }
    }
}

