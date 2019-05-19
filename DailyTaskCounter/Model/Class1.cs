
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DailyTaskCounter.Model
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase]
        public void TestMethod1()
        {
            //MainPage m = new MainPage();
            //string today = m.GetToday();

            Program p = new Program();
            decimal a = 10;
            decimal b = 20;

            decimal sum = p.AddNumber(a, b);
            Assert.AreEqual(sum, 30);
        }
    }
    public class Program
    {
        public decimal AddNumber(decimal a, decimal b)
        {
            decimal sum = a + b;
            return sum;
        }
    }
}

