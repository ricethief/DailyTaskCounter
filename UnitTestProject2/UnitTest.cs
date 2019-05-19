using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyTaskCounter;
using DailyTaskCounter.Model;
using System.Collections.Generic;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]

        public void Testing1_GetToday()
        {
            string today = MainPage.GetToday();
            Assert.AreEqual(("5/19/2019").ToString(), today);
        }
        [TestMethod]
        public void Testing2_AddCount()
        {
            string today = MainPage.GetToday();
            DailyTaskCounter.Model.TaskCounter taskCounter = new DailyTaskCounter.Model.TaskCounter(today, 1, 1, 1, 1);
            decimal sum = taskCounter.reached;
            decimal result = MainPage.AddCount(sum);
            Assert.AreEqual(2, result);
        }
        [TestMethod]
        public void Testing3_SubtractCount()
        {
            string today = MainPage.GetToday();
            DailyTaskCounter.Model.TaskCounter taskCounter = new DailyTaskCounter.Model.TaskCounter(today, 1, 1, 1, 1);
            decimal sum = taskCounter.reached;
            decimal result = MainPage.SubtractCount(sum);
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Testing4_SubtractCount_From0()
        {
            string today = MainPage.GetToday();
            DailyTaskCounter.Model.TaskCounter taskCounter = new DailyTaskCounter.Model.TaskCounter(today, 1, 0, 1, 1);
            decimal sum = taskCounter.reached;
            decimal result = MainPage.SubtractCount(sum);
            Assert.AreNotEqual(-1, result);
        }
        [TestMethod]
        public void Testing5_GetProgress()
        {
            decimal result = MainPage.GetProgress(1, 1);
            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void Testing6_GetProgress_From_0_Call()
        {
            decimal result = MainPage.GetProgress(0, 1);
            Assert.AreEqual(0, result);
        }
        [TestMethod]

        public void Testing7_dbAccess()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDBDummy.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            string result = MainPage.dbAccess(path, conn); //creating table
            string expected = "Access success";

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Testing8_InsertOrReplace()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDBDummy.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            string access = MainPage.dbAccess(path, conn); //creating table
            string result = MainPage.InsertOrReplace("5/19/2019", 1, 1, 1, 1, conn);
            string expected = "Data Insert success";

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Testing9_Reset()
        {

            string result = MainPage.Reset(1, 1, 1, 1);
            string expected = "Reset success";

            Assert.AreEqual(expected, result);
        }
        public void Testing10_getDataFromDate()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDBDummy.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            string access = MainPage.dbAccess(path, conn); //creating table
            string Inserted_Data = MainPage.InsertOrReplace("5/19/2019", 1, 1, 1, 1, conn);

            List<TaskCounter> e = new List<TaskCounter>();
            e = MainPage.getDataFromDate("5/19/2019", conn, e);

            decimal result = e.Count;
            decimal expected =1;

            Assert.AreEqual(expected, result);
        }

    }
}
