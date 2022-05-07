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
    abstract class enemy : unit
    {
        static public event Func<(int, int, napravlenie)> get_coords_from_player;
        static protected (int, int, napravlenie) coords_from_player() => get_coords_from_player();

        [field: NonSerialized]
        protected Action proverka_povorota;
        protected int xkt, ykt;
        protected void going_to_target(bool ismax)
        {
            if (x == x_center_kletki && y == y_center_kletki)
            {
                choose_napravlenie(ismax);
                (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
            }
        }
        protected void choose_napravlenie(bool ismax)
        {
            bool[] walls = get_walls_around();
            if (walls[((int)to + 2) % 4] && !walls[((int)to + 1) % 4] && !walls[((int)to + 3) % 4] && !walls[(int)to])
                to = (napravlenie)(((int)to + 2) % 4);
            else
            {
                int oldprotivopnaprav = ((int)to + 2) % 4, length = (ismax) ? 0 : 1000000000;
                for (int i = ((int)oldprotivopnaprav + 1) % 4; i != oldprotivopnaprav; i = (i + 1) % 4)
                    if (walls[i] && (ismax && length < (xkt - (xk + (i - 1) % 2)) * (xkt - (xk + (i - 1) % 2)) + (ykt - (yk - (i - 2) % 2)) * (ykt - (yk - (i - 2) % 2)) || !ismax && length > (xkt - (xk + (i - 1) % 2)) * (xkt - (xk + (i - 1) % 2)) + (ykt - (yk - (i - 2) % 2)) * (ykt - (yk - (i - 2) % 2))))
                    {
                        to = (napravlenie)i;
                        length = (xkt - (xk + (i - 1) % 2)) * (xkt - (xk + (i - 1) % 2)) + (ykt - (yk - (i - 2) % 2)) * (ykt - (yk - (i - 2) % 2));
                    }
            }
        }
        protected override void moving()
        {
            if (this is blueghost)
                xk = xk;
            proverka_povorota();
            peredvizenie();
        }
        protected void choose_napravlenie_in_random()
        {
            bool[] walls = get_walls_around();
            if (walls[((int)to + 2) % 4] && !walls[((int)to + 1) % 4] && !walls[((int)to + 3) % 4] && !walls[(int)to])
                to = (napravlenie)(((int)to + 2) % 4);
            else if (walls[((int)to + 2) % 4] || walls[((int)to + 1) % 4] || walls[((int)to + 3) % 4] || walls[(int)to])
            {
                napravlenie oldprotivopnaprav = (napravlenie)(((int)to + 2) % 4);
                do to = (napravlenie)((new Random()).Next() % 4); while (!walls[(int)to] || to == oldprotivopnaprav);
            }
        }
        protected enemy(int interval, bool is_for_time, int number_of_unit, Bitmap[] images_for_animate, napravlenie napr, (int,int)[] coords, int time_for_dead_timer) : base(interval, number_of_unit, is_for_time, images_for_animate, napr, coords, time_for_dead_timer) { }
        protected void going_random()
        {
            if (x == x_center_kletki && y == y_center_kletki)
            {
                choose_napravlenie_in_random();
                (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
            }
        }
    }
    [Serializable]
    abstract class simple_enemy : enemy
    {
        public static event Action<int> get_score;
        void checking_kill(int xkp, int ykp)
        {
            if (xkp == xk && ykp == yk)
            {
                get_score(200);
                call_event_delete_me(number_of_unit);
            }
        }
        void check_kill_by_pacman(pacman.pacman_EventArgs data)
        {
            if (!data.am_i_dead)
                checking_kill(data.xk, data.yk);
        }
        protected override void handler_for_animate_timer(object sender, ElapsedEventArgs e) => index_for_animate = ++index_for_animate % 2;
        public simple_enemy(int number_of_unit, (int, int)[] coords, Bitmap[] images_for_animate, int interval, napravlenie napr, int time_for_dead_timer) : base(interval, true, number_of_unit, images_for_animate, napr, coords, time_for_dead_timer) => podpis_on_events();
        public override void activate()
        {
            base.activate();
            podpis_on_events();
        }
        void podpis_on_events()
        {
            bullet.check_killing_units += checking_kill;
            pacman.check_killing += check_kill_by_pacman;
            proverka_povorota = checking_povovorota;
        }
        abstract protected void checking_povovorota();
        protected override void show()
        {
            mutex_for_show.WaitOne();
            if (is_alive)
                graphics.DrawImage(images_for_show[index_for_animate + (int)to * 2], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        protected override void show(Graphics frame, int cordx, int cordy)
        {
            mutex_for_show.WaitOne();
            if (is_alive)
                frame.DrawImage(images_for_show[index_for_animate + (int)to * 2], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        public override void Dispose()
        {
            bullet.check_killing_units -= checking_kill;
            pacman.check_killing -= check_kill_by_pacman;
            base.Dispose();
        }
    }
}
