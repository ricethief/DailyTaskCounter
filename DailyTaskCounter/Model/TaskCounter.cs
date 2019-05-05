using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTaskCounter.Model
{
    class TaskCounter
    {
        private decimal callcount { get; set; }
        private decimal reached { get; set; }
        private decimal appointment { get; set; }
        private decimal progress { get; set; }
        private DateTime date { get; set; }
    }
}
