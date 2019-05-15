using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.Diagnostics;
using SQLite.Net.Attributes;

namespace DailyTaskCounter.Model
{
   public class TaskCounter
    {

        [PrimaryKey]
        public string date { get; set; }
        public decimal callcount { get; set; }
        public decimal reached { get; set; }
        public decimal appointment { get; set; }
        public decimal progress { get; set; }

        public TaskCounter()
        {
        }

        public TaskCounter(string date,object call,decimal reached, decimal appointment,decimal progress)
        {
            this.date = date;
            this.reached = reached;
            this.appointment = appointment;
            this.progress = progress;
        }
        public override string ToString()
        {
            return "Date: " + date + "| Calls: " + callcount + "| Reched Phone Calls: " + reached;
        }

    }
    
}
