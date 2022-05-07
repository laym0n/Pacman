using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Timers;

namespace PacmanWinFormsApp
{

    [Serializable]
    class bullet : unit
    {
        public static event Func<int, int, bool> delete_wall_in_my_kletka;
        public static event Action<int, int> check_killing_units;
        bool find_wall = false, is_teleported = true;
        public override event Func<int, int, bool[]> check_walls { add { CHECK_WALLS += value; start_timer(); } remove => CHECK_WALLS -= value; }
        protected override void moving()
        {
            (int oldxk, int oldyk) = (xk, yk);
            peredvizenie();
            if (oldxk != xk || oldyk != yk)
            {
                if (Math.Abs(oldxk - xk) + Math.Abs(oldyk - yk) > 1)
                {
                    if (is_teleported = !is_teleported)
                        call_event_delete_me(number_of_unit);
                }
                if (find_wall)
                {
                    if (!delete_wall_in_my_kletka(xk, yk))
                        call_event_delete_me(number_of_unit);
                }
                else
                    find_wall = delete_wall_in_my_kletka(xk, yk);
            }
            check_killing_units(xk, yk);
        }
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.BulletLeft, new Size(size_of_kletki * 12 / 8, size_of_kletki * 6 / 8)), new Bitmap(Properties.Resources.BulletUp, new Size(size_of_kletki * 6 / 8, size_of_kletki * 12 / 8)), new Bitmap(Properties.Resources.BulletRight, new Size(size_of_kletki * 12 / 8, size_of_kletki * 6 / 8)), new Bitmap(Properties.Resources.BulletDown, new Size(size_of_kletki * 6 / 8, size_of_kletki * 12 / 8)) }, 1);
        protected override void show()
        {
            mutex_for_show.WaitOne();
            if (is_alive)
                graphics.DrawImage(images_for_show[(int)to], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        protected override void show(Graphics frame, int cordx, int cordy)
        {
            mutex_for_show.WaitOne();
            if (is_alive)
                frame.DrawImage(images_for_show[(int)to], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        public bullet(int number_of_unit, napravlenie to, (int, int)[] coords) : base(1, number_of_unit, false, new Bitmap[] { new Bitmap(Properties.Resources.BulletLeft, new Size(size_of_kletki * 12 / 8, size_of_kletki * 6 / 8)), new Bitmap(Properties.Resources.BulletUp, new Size(size_of_kletki * 6 / 8, size_of_kletki * 12 / 8)), new Bitmap(Properties.Resources.BulletRight, new Size(size_of_kletki * 12 / 8, size_of_kletki * 6 / 8)), new Bitmap(Properties.Resources.BulletDown, new Size(size_of_kletki * 6 / 8, size_of_kletki * 12 / 8)) }, to, coords, 1) { }
    }
    [Serializable]
    class creater_of_ghost : simple_enemy
    {
        static int time_for_living = 12000;
        public static event Action<int> get_ghost;
        public override void activate()
        {
            base.activate();
            timer_of_object.Interval = 25;
        }
        protected override void handler_for_dead_timer(object sender, ElapsedEventArgs e)
        {
            timer_for_dead.Dispose();
            get_ghost(new Random().Next() % 4);
            call_event_delete_me(this.number_of_unit);
        }
        public creater_of_ghost(int number_of_unit, (int, int)[] coords) : base(number_of_unit, coords, new Bitmap[] { new Bitmap(Properties.Resources.Purple1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, 25, napravlenie.left, time_for_living) { }
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Purple1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Purple2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_living);
        protected override void checking_povovorota()
        {
            (xkt, ykt, _) = coords_from_player();
            going_to_target(true);
        }
    }
    [Serializable]
    class creater_of_barrier : simple_enemy
    {
        static int time_for_living = 15000;
        public static event Action<int, int> barrier_created;
        [field: NonSerialized]
        System.Timers.Timer timer_for_creating_barrier;
        public override event Func<int, int, bool[]> check_walls { add { CHECK_WALLS += value; if (is_for_time) { start_timer(); timer_for_dead.Start(); timer_for_creating_barrier.Start(); timer_for_animate.Start(); } } remove => CHECK_WALLS -= value; }
        protected override void checking_povovorota() => going_random();
        public creater_of_barrier(int number_of_unit, (int, int)[] coords) : base(number_of_unit, coords, new Bitmap[] { new Bitmap(Properties.Resources.Green1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, 1, napravlenie.left, time_for_living) => create_timer_for_barrier();
        public override void activate()
        {
            base.activate();
            create_timer_for_barrier();
        }
        void create_timer_for_barrier()
        {
            timer_for_creating_barrier = new(3000);
            timer_for_creating_barrier.Elapsed += delegate { barrier_created(xk, yk); };
        }
        protected override void handler_for_dead_timer(object sender, ElapsedEventArgs e) => call_event_delete_me(this.number_of_unit);
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Green1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Green2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_living);
        public override void Dispose()
        {
            timer_for_creating_barrier.Stop();
            timer_for_creating_barrier.Dispose();
            base.Dispose();
        }
    }
}
