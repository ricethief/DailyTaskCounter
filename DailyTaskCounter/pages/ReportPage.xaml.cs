using DailyTaskCounter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DailyTaskCounter.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportPage : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        private List<TaskCounter> taskCounters = new List<TaskCounter>();
        public ReportPage()
        {
            this.InitializeComponent();
            dbAccess();
            getDataFromDB();
            getList();
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
            Frame.Navigate(typeof(MainPage));
        }

        private void reportViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public void dbAccess()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "TaskConuterDB.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<TaskCounter>();
        }
        public void getDataFromDB()
        {
            var query = from task in conn.Table<TaskCounter>() select task;
            foreach (var task in query)
            {
                taskCounters.Add(task);
            }
        }
        public void getList()
        {
            foreach (var task in taskCounters)
            {
                listBox.Items.Add(task.ToString());
            }
        }
  
    }
}
