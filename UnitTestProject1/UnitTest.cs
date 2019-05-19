
using System;

using Xunit;

namespace UnitTestProject1
{
  
    public class UnitTest1
    {
        [Fact]
        public void TestMethod2()
        {
          //  DailyTaskCounter.MainPage mainPage = new DailyTaskCounter.MainPage();

        }
        [Fact]
        public void TestMethod1()
        {
            ss p = new ss();
            decimal a = 10;
            decimal b = 20;

            decimal sum = p.AddNumber(a, b);
            Assert.Equal(31, sum);
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

