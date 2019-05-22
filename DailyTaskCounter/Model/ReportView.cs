using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossPieCharts.UWP.PieCharts;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DailyTaskCounter.Model
{
    public class ReportView
    {
        public List<PieChartArgs> PieChartCollection { get; set; }
        private decimal totalCallcount=100;
        private decimal totalReached;
        private decimal totalAppointment;
        public static decimal totalProgress { get; set; }
        

        public ReportView()
        {
            totalProgress = Convert.ToDecimal(56.55);
            
            PieChartCollection = new List<PieChartArgs>
            {
                new PieChartArgs
                {
                    Percentage = Convert.ToInt16(totalCallcount),
                    ColorBrush = new SolidColorBrush(Colors.PowderBlue),
                },
                 new PieChartArgs
                {
                    Percentage = Convert.ToInt16(totalProgress),
                    ColorBrush = new SolidColorBrush(Colors.Blue),
                }
            };
        }

        

    }
}
