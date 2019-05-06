using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DailyTaskCounter.Model;
using System.Linq;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DailyTaskCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        private List<TaskCounter> taskCounters = new List<TaskCounter>();
        private string date;
        private decimal callcount;
        private decimal reached;
        private decimal appointment;
        private decimal progress;



        public MainPage()
        {

            this.InitializeComponent();
            date = DateTime.Now.Date.ToString();
            datePicker.Date = Convert.ToDateTime(date);
            dbAccess();
           // getDataFromDate(date);

        }
        private void datePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            //getDataFromDate(date);
            date = datePicker.Date.ToString();
        }
        private void CallAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            _callcount++;
            callcount = _callcount;
            CallCountLable.Text = callcount.ToString();
            GetProgress(callcount, reached);
        }
        private void CallSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            if (0 < _callcount)
            {
                _callcount--;
                callcount = _callcount;
                CallCountLable.Text = callcount.ToString();
            }
            else
            {
                callcount = _callcount;
                CallCountLable.Text = callcount.ToString();
            }
            GetProgress(callcount, reached);
        }
        private void ReachedAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            _reached++;
            reached = _reached;
            ReachedCountLable.Text = reached.ToString();
            GetProgress(callcount, reached);
        }
        private void ReachedSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            if (0 < _reached)
            {
                _reached--;
                reached = _reached;
                ReachedCountLable.Text = reached.ToString();
            }
            else
            {
                reached = _reached;
                ReachedCountLable.Text = reached.ToString();
            }
            GetProgress(callcount, reached);
        }
        private void AppointmentAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            _appointment++;
            appointment = _appointment;
            AppointmentCountLable.Text = appointment.ToString();
            GetProgress(callcount, reached);
        }
        private void AppointmentSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            if (0 < _appointment)
            {
                _appointment--;
                appointment = _appointment;
                AppointmentCountLable.Text = appointment.ToString();

            }
            else
            {
                appointment = _appointment;
                AppointmentCountLable.Text = appointment.ToString();
            }
            GetProgress(callcount, reached);
        }
        public void GetProgress(decimal call, decimal reached)
        {
            if (call != 0)
            {
                progress = (reached / call) * 100;
                ProgressLable.Text = progress.ToString("F");
            }
            else ProgressLable.Text = "0";
        }
        private void ResetBTN_Click(object sender, RoutedEventArgs e)
        {
            //Reset();
            getDataFromDate(date);
            foreach (var item in taskCounters)
            {
                AppointmentCountLable.Text = item.appointment.ToString();
                CallCountLable.Text = item.callcount.ToString();
                ReachedCountLable.Text = item.reached.ToString();
                ProgressLable.Text = item.progress.ToString();
            }
        }
        public void Reset()
        {
            callcount = 0;
            reached = 0;
            appointment = 0;
            progress = 0;
            CallCountLable.Text = callcount.ToString();
            ReachedCountLable.Text = reached.ToString();
            AppointmentCountLable.Text = appointment.ToString();
            ProgressLable.Text = progress.ToString();
        }
        private void SaveSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            var add = conn.InsertOrReplace(new TaskCounter()
            {
                date = date,
                callcount = callcount,
                reached = reached,
                appointment = appointment,
                progress = progress
            });
        }
        public void getDataFromDate (string _date)
        {
            // var data = from d in conn.Table<TaskCounter>() where d.date.Equals(_date) select d;
            //var query = conn.Table<TaskCounter>().Where(v => v.date.StartsWith(_date));
            //var query = conn.Table<TaskCounter>().Where(v => v.date.StartsWith(_date));
            var query = from task in conn.Table<TaskCounter>() where task.date == _date select task;
            foreach (var task in query)
            {
                taskCounters.Add(task);
            }

            //var query = conn.Table<TaskCounter>().Where(d =>d.date);
            //  //  Query<TaskCounter>("SELECT * FROM TaskCounter Where date = " + date, conn);
            //foreach (var taskCounter in query)
            //{
            //    TaskCounter t1 = new TaskCounter(taskCounter.date, taskCounter.callcount,
            //        taskCounter.reached, taskCounter.appointment, taskCounter.progress);
            //    callcount = t1.callcount;
            //}

        }
        public void dbAccess()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDB.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<TaskCounter>();
        }

    }
}
