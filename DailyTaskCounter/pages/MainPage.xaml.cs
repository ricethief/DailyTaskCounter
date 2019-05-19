using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DailyTaskCounter.Model;
using System.Linq;
using Windows.UI.Popups;
using DailyTaskCounter.pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DailyTaskCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //nasted variable
        string path;
        SQLite.Net.SQLiteConnection conn;
        private List<TaskCounter> taskCounters = new List<TaskCounter>();
        private string date;
        private decimal callcount;
        private decimal reached;
        private decimal appointment;
        private decimal progress;

        //MainPageFunction
        public MainPage()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDB.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            this.InitializeComponent();
            date = GetToday();
            dbAccess(path, conn);
            taskCounters = getDataFromDate(date, conn, taskCounters);
            dumpDBtoMemory();
        }
        private void hambergerButton_Click(object sender, RoutedEventArgs e)
        {
            Hb_menu.IsPaneOpen = true;
            hambergerButton.Visibility = Visibility.Collapsed;
            hambergerButtonClose.Visibility = Visibility.Visible;
        }
        private void hambergerButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hb_menu.IsPaneOpen = false;
            hambergerButton.Visibility = Visibility.Visible;
            hambergerButtonClose.Visibility = Visibility.Collapsed;
        }
        private void mainViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void reportViewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReportPage));
        }
        private void datetimePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            Reset(callcount, reached, appointment, progress, CallCountLable, ReachedCountLable, AppointmentCountLable, ProgressLable);
            date = GetDatePickerDate(datetimepicker);
            taskCounters = getDataFromDate(date, conn, taskCounters);
            dumpDBtoMemory();
        }
        //Add Call Event handler
        private void CallAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            callcount = AddCount(_callcount);
            CallCountLable.Text = callcount.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //subtract Call Event handler
        private void CallSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            callcount = SubtractCount(_callcount);
            CallCountLable.Text = callcount.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //Add Reached Event handler
        private void ReachedAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            reached = AddCount(_reached);
            ReachedCountLable.Text = reached.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //Subtract Reached Event handler
        private void ReachedSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            reached = SubtractCount(_reached);
            ReachedCountLable.Text = reached.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //Add Appointment Event handler
        private void AppointmentAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            appointment = AddCount(_appointment);
            AppointmentCountLable.Text = appointment.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //Subtract Appointment Event handler
        private void AppointmentSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            appointment = SubtractCount(_appointment);
            AppointmentCountLable.Text = appointment.ToString();
            GetProgress(callcount, reached, ProgressLable);
        }
        //Save Session Event handler
        private async void SaveSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            var message = new MessageDialog("Session Saved");
            message.Commands.Add(new UICommand("OK"));
            await message.ShowAsync();
            InsertOrReplace(date, callcount, reached, appointment, progress, conn);
        }
        //Reset Event handler
        private async void ResetBTN_Click(object sender, RoutedEventArgs e)
        {
            var message = new MessageDialog("Are you sure you want to Reset");
            message.Commands.Add(new UICommand("OK", null));
            message.Commands.Add(new UICommand("CANCEL", null));
            message.DefaultCommandIndex = 0;
            message.CancelCommandIndex = 1;
            var cmd = await message.ShowAsync();
            if (cmd.Label == "OK")
            {
                Reset(callcount, reached, appointment, progress, CallCountLable, ReachedCountLable, AppointmentCountLable, ProgressLable);
                this.callcount = Convert.ToDecimal(CallCountLable.Text);
                this.reached = Convert.ToDecimal(ReachedCountLable.Text);
                this.appointment = Convert.ToDecimal(ReachedCountLable.Text);
                this.progress = Convert.ToDecimal(ReachedCountLable.Text);
                InsertOrReplace(date, callcount, reached, appointment, progress, conn);
            }
        }

        //get current date
        public static string GetToday()
        {
            string today = DateTime.Now.Date.ToString("M/d/yyyy");
            return today;
        }
        //get datepicker's date
        public static string GetDatePickerDate(DatePicker _datePicker)
        {
            string today = _datePicker.Date.ToString("d");
            return today;
        }
        //add
        public static decimal AddCount(decimal value)
        {
            decimal result;
            value++;
            result = value;
            return result;
        }
        //subtract
        public static decimal SubtractCount(decimal value)
        {
            decimal result;
            if (0 < value)
            {
                value--;
                result = value;

            }
            else
            {
                result = 0;
            }
            return result;

        }
        //get progress
        public static decimal GetProgress(decimal call, decimal reached, TextBlock lableName)
        {
            decimal _progress = 0;
            if (call != 0)
            {
                _progress = (reached / call) * 100;
                lableName.Text = _progress.ToString("F");
                return _progress;
            }
            else lableName.Text = "0";

            return _progress;
        }
        //insertOrReplace date
        public static string InsertOrReplace(string _date, decimal _c, decimal reached, decimal appointment, decimal progress, SQLite.Net.SQLiteConnection conn)
        {
            try
            {
                var add = conn.InsertOrReplace(new TaskCounter()
                {
                    date = _date,
                    callcount = _c,
                    reached = reached,
                    appointment = appointment,
                    progress = progress
                });
                return "Data Insert success";
            }
            catch { return "Data Insert Fail"; }
        }
        //Reset
        public static string Reset(decimal _callcount, decimal _reached, decimal _appointment, decimal _progress, TextBlock CallCountLable, TextBlock ReachedCountLable, TextBlock AppointmentCountLable, TextBlock ProgressLable)
        {
            try
            {
                _callcount = 0;
                _reached = 0;
                _appointment = 0;
                _progress = 0;
                CallCountLable.Text = _callcount.ToString();
                ReachedCountLable.Text = _reached.ToString();
                AppointmentCountLable.Text = _appointment.ToString();
                ProgressLable.Text = _progress.ToString(("F"));
                return "Reset success";
            }
            catch { return "Reset fail"; }
        }
        //Get data from DB where selected date is 
        public static List<TaskCounter> getDataFromDate(string _date, SQLite.Net.SQLiteConnection _conn, List<TaskCounter> _taskCounters)
        {
            try
            {
                var query = from task in _conn.Table<TaskCounter>() select task;
                foreach (var task in query)
                {
                    _taskCounters.Add(task);
                }
                return _taskCounters;
            }
            catch { return _taskCounters; }
        }
        //SQLite Path and connection
        public static string dbAccess(string path, SQLite.Net.SQLiteConnection conn)
        {
            try
            {
                conn.CreateTable<TaskCounter>();
                return "Access success";
            }
            catch { return "Access Fail"; }
        }
        //Dumping data drom DB to nasted values
        public void dumpDBtoMemory()
        {
            foreach (var item in taskCounters.Where(d => d.date == date))
            {
                this.callcount = item.callcount;
                this.reached = item.reached;
                this.appointment = item.appointment;
                this.progress = item.progress;

                CallCountLable.Text = callcount.ToString();
                ReachedCountLable.Text = reached.ToString();
                AppointmentCountLable.Text = appointment.ToString();
                ProgressLable.Text = progress.ToString(("F"));
            }
        }

    }
}
