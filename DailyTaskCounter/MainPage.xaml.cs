using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DailyTaskCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private decimal callcount;
        private decimal reached;
        private decimal appointment;
        private decimal progress;
        private DateTime date;

        public MainPage()
        {
            this.InitializeComponent();
            date= DateTime.Now.Date;
            datePicker.Date = date;
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
            Reset();
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

       
    }
}
