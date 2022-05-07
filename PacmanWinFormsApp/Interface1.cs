using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanWinFormsApp
{
    interface ITemporary
    {
        public event Action my_timer_ended;
        protected System.Timers.Timer timer_for_dead { get; set; }
        protected void start_timer() => timer_for_dead.Start();
        protected void stop_timer() => timer_for_dead.Stop();
    }
}
