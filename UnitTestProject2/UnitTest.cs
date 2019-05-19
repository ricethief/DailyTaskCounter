
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyTaskCounter;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        
        public void Testing_GetToday()
        {
            string today = MainPage.GetToday();
           Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_AddCount()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_SubtractCount()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_GetProgress()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_InsertOrReplace()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_Reset()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_getDataFromDate()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        public void Testing_dbAccess()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }

    }
}
