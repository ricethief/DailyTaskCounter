using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DailyTaskCounter.Model;

namespace DailyTaskCounter.Data
{
    class ApplicationDBContext : DbContext
    {
        public DbSet<TaskCounter> taskCounters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=text.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
