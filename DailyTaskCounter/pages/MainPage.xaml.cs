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
            this.InitializeComponent();
            date = GetToday();
            dbAccess();
            getDataFromDate(date);
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
            Reset();
            date = GetDatePickerDate(datetimepicker);
            getDataFromDate(date);
            dumpDBtoMemory();
        }
        //Add Call Event handler
        private void CallAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            callcount = AddCount(_callcount);
            CallCountLable.Text = callcount.ToString();
            GetProgress(callcount, reached);
        }
        //subtract Call Event handler
        private void CallSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _callcount = Convert.ToDecimal(CallCountLable.Text);
            callcount = SubtractCount(_callcount);
            CallCountLable.Text = callcount.ToString();
            GetProgress(callcount, reached);
        }
        //Add Reached Event handler
        private void ReachedAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            reached = AddCount(_reached);
            ReachedCountLable.Text = reached.ToString();
            GetProgress(callcount, reached);
        }
        //Subtract Reached Event handler
        private void ReachedSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _reached = Convert.ToDecimal(ReachedCountLable.Text);
            reached = SubtractCount(_reached);
            ReachedCountLable.Text = reached.ToString();
            GetProgress(callcount, reached);
        }
        //Add Appointment Event handler
        private void AppointmentAddBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            appointment = AddCount(_appointment);
            AppointmentCountLable.Text = appointment.ToString();
            GetProgress(callcount, reached);
        }
        //Subtract Appointment Event handler
        private void AppointmentSubtractBTN_Click(object sender, RoutedEventArgs e)
        {
            decimal _appointment = Convert.ToDecimal(AppointmentCountLable.Text);
            appointment = SubtractCount(_appointment);
            AppointmentCountLable.Text = appointment.ToString();
            GetProgress(callcount, reached);
        }
        //Save Session Event handler
        private async void SaveSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            var message = new MessageDialog("Session Saved");
            message.Commands.Add(new UICommand("OK"));
            await message.ShowAsync();
            InsertOrReplace(date, callcount, reached, appointment, progress);
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
                Reset();
                InsertOrReplace(date, callcount, reached, appointment, progress);
            }
        }

        //get current date
        public static string GetToday()
        {
            string today = DateTime.Now.Date.ToString("M/d/yyyy");
            return today;
        }
        //get datepicker's date
        public string GetDatePickerDate(DatePicker _datePicker)
        {
            string today = _datePicker.Date.ToString("d");
            return today;
        }
        //add
        public decimal AddCount(decimal value)
        {
            decimal result;
            value++;
            result = value;
            return result;
        }
        //subtract
        public decimal SubtractCount(decimal value)
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
        public void GetProgress(decimal call, decimal reached)
        {
            if (call != 0)
            {
                progress = (reached / call) * 100;
                ProgressLable.Text = progress.ToString("F");
            }
            else ProgressLable.Text = "0";
        }
        //insertOrReplace date
        public void InsertOrReplace(string date, decimal callcaount, decimal reached, decimal appointment, decimal progress)
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
        //Reset
        public void Reset()
        {
            callcount = 0;
            reached = 0;
            appointment = 0;
            progress = 0;
            CallCountLable.Text = callcount.ToString();
            ReachedCountLable.Text = reached.ToString();
            AppointmentCountLable.Text = appointment.ToString();
            ProgressLable.Text = progress.ToString(("F"));
        }
        //Get data from DB where selected date is 
        public void getDataFromDate(string _date)
        {

            var query = from task in conn.Table<TaskCounter>() where task.date == _date select task;
            foreach (var task in query)
            {
                taskCounters.Add(task);
            }
        }
        //SQLite Path and connection
        public void dbAccess()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDB.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<TaskCounter>();
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
