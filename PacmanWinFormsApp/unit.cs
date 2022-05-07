using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PacmanWinFormsApp
{
    [Serializable]
    abstract class unit : generall_class, IDisposable
    {
        [field: NonSerialized]
        protected event Func<int, int, bool[]> CHECK_WALLS;
        public virtual event Func<int, int, bool[]> check_walls { add { CHECK_WALLS += value; if (!game.game_is_stopped_on_pause) { start_timer(); if(is_for_time) timer_for_dead.Start(); timer_for_animate.Start(); } } remove => CHECK_WALLS -= value; }
        public static event Func<int, int, napravlenie, (int, int)[]> check_teleport;
        public static event Action<int> unit_is_deleting;
        public static event Action<FrameEventArgs> fill_background_in_frame;
        static event Action<FrameByUnitEventArgs> fill_frame_by_units;
        protected bool is_alive = true;
        [field: NonSerialized]
        protected Graphics graphics;
        public class FrameByUnitEventArgs : EventArgs
        {
            public Rectangle oblast;
            public Graphics frame;
        }
        [field: NonSerialized]
        protected Mutex mutex_for_show;
        public class FrameEventArgs : EventArgs
        {
            public int xk, yk, width, height;
            public Graphics frame;
        }
        [field: NonSerialized]
        protected Bitmap[] images_for_show;
        [NonSerialized]
        protected System.Timers.Timer timer_for_animate;
        protected int index_for_animate;
        [field: NonSerialized]
        protected Mutex mutex;
        protected bool is_for_time { get; init; }
        [NonSerialized]
        protected System.Timers.Timer timer_for_dead;
        private napravlenie TO;
        protected napravlenie to
        {
            get => TO;
            set
            {
                int xp = (value == napravlenie.left) ? -1 : (value == napravlenie.right) ? 1 : 0;
                int yp = (value == napravlenie.down) ? 1 : (value == napravlenie.up) ? -1 : 0;
                mutex.WaitOne();
                if (((int)value + 2) % 4 == (int)TO)
                    (xperekl, yperekl) = (xperekl + xp * size_of_kletki, yperekl + yp * size_of_kletki);
                else
                {
                    int xpTO = (TO == napravlenie.left) ? -1 : (TO == napravlenie.right) ? 1 : 0;
                    int ypTO = (TO == napravlenie.down) ? 1 : (TO == napravlenie.up) ? -1 : 0;
                    (xperekl, yperekl) = (xperekl + xp * size_of_kletki / 2 - xpTO * size_of_kletki / 2, yperekl + yp * size_of_kletki / 2 - ypTO * size_of_kletki / 2);
                }
                TO = value;
                mutex.ReleaseMutex();
            }
        }
        protected int x, y, xk, yk, xperekl, yperekl, x_center_kletki, y_center_kletki, number_of_unit;
        [NonSerialized]
        protected System.Timers.Timer timer_of_object;
        protected bool[] get_walls_around() => CHECK_WALLS(xk, yk);
        protected void set_coords((int, int)[] coords) => ((x, y), (xk, yk), (xperekl, yperekl), (x_center_kletki, y_center_kletki)) = (coords[0], coords[1], coords[2], coords[3]);
        static protected void show(int xkobnov, int ykobnov, int xper, int yper, napravlenie to, bool with_units = true)
        {
            Bitmap newframe = new(3 * size_of_kletki, 3 * size_of_kletki);
            fill_background_in_frame(new FrameEventArgs() { xk = xkobnov - 1, yk = ykobnov - 1, height = 3, width = 3, frame = Graphics.FromImage(newframe) });
            int xpicture = xper - (int)to % 2 * size_of_kletki * 3 / 2 - ((int)to + 1) % 2 * size_of_kletki - (int)to / 2 * ((int)to + 1) % 2 * size_of_kletki, ypicture = yper - ((int)to + 1) % 2 * size_of_kletki * 3 / 2 - (int)to % 2 * size_of_kletki - (int)to / 2 * (int)to % 2 * size_of_kletki;
            if (with_units) fill_frame_by_units?.Invoke(new FrameByUnitEventArgs() { frame = Graphics.FromImage(newframe), oblast = new Rectangle(xpicture, ypicture, newframe.Width, newframe.Height) });
            if (game.game_is_going) place_for_game.CreateGraphics().DrawImage(newframe, xpicture, ypicture);
        }
        void show(FrameByUnitEventArgs data)
        {
            if (data.oblast.IntersectsWith(new Rectangle(x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8, size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)))
                show(data.frame, x - data.oblast.Left, y - data.oblast.Top);
        }
        protected void peredvizenie()
        {
            mutex.WaitOne();
            int yp = (to == napravlenie.left || to == napravlenie.right) ? 0 : (to == napravlenie.up) ? -1 : 1;
            int xp = (to == napravlenie.up || to == napravlenie.down) ? 0 : (to == napravlenie.left) ? -1 : 1;
            x += xp;
            y += yp;
            (int oldxk, int oldyk, int xper, int yper, napravlenie napr) = (xk, yk, xperekl, yperekl, to);
            if (to == napravlenie.right && x >= xperekl || to == napravlenie.left && x <= xperekl || to == napravlenie.up && y <= yperekl || to == napravlenie.down && y >= yperekl)
            {
                xperekl += xp * size_of_kletki;
                yperekl += yp * size_of_kletki;
                yk -= yp;
                xk += xp;
                (int, int)[] coords = check_teleport(xk, yk, to);
                (oldxk, oldyk, xper, yper, napr) = (xk, yk, xperekl, yperekl, to);
                if (coords != null)
                    set_coords(coords);
            }
            mutex.ReleaseMutex();
            Task.Run(() => show(oldxk, oldyk, xper, yper, napr));

        }
        protected abstract void moving();
        protected virtual void set_other_in_restart(int lvl, int eat, int left_lives) { }
        void restart(int lvl, int eat, int left_lives)
        {
            set_other_in_restart(lvl, eat, left_lives);
            show();
        }
        protected unit(int interval, int number_of_unit, bool is_for_time, Bitmap[] images_for_animate, napravlenie napr, (int, int)[] coords, int time_for_dead_timer)
        {
            (this.number_of_unit, this.is_for_time, TO) = (number_of_unit, is_for_time, napr);
            set_coords(coords);
            podpis_on_events(interval, images_for_animate, time_for_dead_timer);
        }
        void change_number_of_unit(int number_of_deleting)
        {
            if (number_of_unit > number_of_deleting)
                number_of_unit--;
        }
        protected void start_timer_for_dead() => timer_for_dead.Enabled = true;
        protected void stop_timer_for_dead() => timer_for_dead.Enabled = false;
        protected static void call_event_delete_me(int number) => unit_is_deleting(number);
        abstract protected void show();
        void show(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(new Rectangle(x - size_of_kletki, y - size_of_kletki, 2 * size_of_kletki, 2 * size_of_kletki)) && game.game_is_going)
                show();
        }
        abstract protected void show(Graphics frame, int cordx, int cordy);
        protected void start_timer_for_animate() => timer_for_animate.Start();
        protected void stop_timer_for_animate() => timer_for_animate.Stop();
        protected void start_timer() => timer_of_object.Enabled = true;
        protected void stop_timer() => timer_of_object.Enabled = false;
        protected virtual void handler_for_dead_timer(object sender, System.Timers.ElapsedEventArgs e) { }
        protected virtual void handler_for_animate_timer(object sender, System.Timers.ElapsedEventArgs e) { }
        void podpis_on_events(int interval, Bitmap[] images_for_animate, int time_for_dead_timer)
        {
            graphics = place_for_game.CreateGraphics();
            mutex = new();
            mutex_for_show = new();
            timer_of_object = new() { Interval = interval, Enabled = false };
            timer_of_object.Elapsed += delegate { moving(); };
            unit_is_deleting += change_number_of_unit;
            game.start_game += start_timer;
            game.stop_game += stop_timer;
            place_for_game.Paint += show;
            if (is_for_time)
            {
                timer_for_dead = new(time_for_dead_timer) { AutoReset = false };
                timer_for_dead.Elapsed += handler_for_dead_timer;
                game.start_game += start_timer_for_dead;
                game.stop_game += stop_timer_for_dead;
            }
            game.restart_event += restart;
            fill_frame_by_units += show;
            images_for_show = images_for_animate;
            for (int i = 0; i < images_for_show.Length; i++)
                images_for_show[i].MakeTransparent(Color.Black);
            index_for_animate = 0;
            timer_for_animate = new(105);
            timer_for_animate.Elapsed += handler_for_animate_timer;
            game.start_game += start_timer_for_animate;
            game.stop_game += stop_timer_for_animate;
        }
        abstract protected (int, Bitmap[], int) get_data_for_activate();
        public virtual void activate()
        {
            (int interval, Bitmap[] bitmaps, int time_for_dead) = get_data_for_activate();
            podpis_on_events(interval, bitmaps, time_for_dead);
        }
        public virtual void Dispose()
        {
            mutex_for_show.WaitOne();
            is_alive = false;
            mutex_for_show.ReleaseMutex();
            timer_of_object.Stop();
            Task.Run(() => show(xk, yk, xperekl, yperekl, to));
            timer_of_object.Dispose();
            fill_frame_by_units -= show;
            game.start_game -= start_timer;
            game.stop_game -= stop_timer;
            game.restart_event -= restart;
            place_for_game.Paint -= show;
            if (is_for_time)
            {
                timer_for_dead.Enabled = false;
                timer_for_dead.Dispose();
                game.start_game -= start_timer_for_dead;
                game.stop_game -= stop_timer_for_dead;
            }
            timer_for_animate.Dispose();
            unit_is_deleting -= change_number_of_unit;
            CHECK_WALLS = null;
            game.start_game -= start_timer_for_animate;
            game.stop_game -= stop_timer_for_animate;
            mutex_for_show.Dispose();
            mutex.Dispose();
            for (int i = 0; i < images_for_show.Length; i++)
                images_for_show[i].Dispose();
            graphics = null;
        }
    }
}
