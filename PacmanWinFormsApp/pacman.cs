using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace PacmanWinFormsApp
{
    [Serializable]
    class pacman : unit
    {
        static Keys[] codes_for_moving = { Keys.Left, Keys.Up, Keys.Right, Keys.Down };
        static public void change_codes(Keys[] new_codes) => codes_for_moving = new_codes;
        napravlenie naprav;
        public static event Action<pacman_EventArgs> check_killing;
        public static event Action<int, int> checking_eat_object_in_kletka;
        public static event Action pacman_dead;
        public class pacman_EventArgs : EventArgs
        {
            public int xk, yk;
            public bool am_i_dead;
            public pacman_EventArgs(int xk, int yk) => (this.xk, this.yk, this.am_i_dead) = (xk, yk, false);
        }
        protected override void moving()
        {
            (int oldxk, int oldyk) = (xk, yk);
            if (x == x_center_kletki && y == y_center_kletki)
            {
                bool[] walls = get_walls_around();
                if (to != naprav && walls[(int)naprav])
                    to = naprav;
                (int xp, int yp) = (((int)to - 1) % 2, ((int)to - 2) % 2);
                if (walls[(int)to])
                {
                    peredvizenie();
                    (x_center_kletki, y_center_kletki) = (x_center_kletki + xp * size_of_kletki, y_center_kletki + yp * size_of_kletki);
                }
            }
            else
                peredvizenie();
            if (oldxk != xk || oldyk != yk)
                checking_eat_object_in_kletka(xk, yk);
            pacman_EventArgs ret = new(xk, yk);
            check_killing?.Invoke(ret);
            if (ret.am_i_dead)
                pacman_dead();
        }
        void change_naprav_KeyPress(object sender, KeyEventArgs e)
        {
            naprav = (e.KeyCode == codes_for_moving[0]) ? napravlenie.left : (e.KeyCode == codes_for_moving[1]) ? napravlenie.up : (e.KeyCode == codes_for_moving[2]) ? napravlenie.right : (e.KeyCode == codes_for_moving[3]) ? napravlenie.down : naprav;
            if ((int)naprav == ((int)to + 2) % 4 && (x != x_center_kletki || y != y_center_kletki))
            {
                to = naprav;
                (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
            }
        }
        protected override void show()
        {
            mutex_for_show.WaitOne();
            if (is_alive)
            {
                if (index_for_animate != 2)
                    graphics.DrawImage(images_for_show[(int)to + index_for_animate * 4], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
                else
                    graphics.DrawImage(images_for_show[8], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            }
            mutex_for_show.ReleaseMutex();
        }
        protected override void show(Graphics frame, int cordx, int cordy)
        {
            mutex_for_show.WaitOne();
            if (is_alive)
            {
                if (index_for_animate != 2)
                    frame.DrawImage(images_for_show[(int)to + index_for_animate * 4], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
                else
                    frame.DrawImage(images_for_show[8], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            }
            mutex_for_show.ReleaseMutex();
        }
        protected override void set_other_in_restart(int lvl, int eat, int left_lives)
        {
            to = napravlenie.left;
            naprav = to;
        }
        (int, int, napravlenie) get_coords_from_me() => (xk, yk, to);
        public override void activate()
        {
            base.activate();
            podpis_on_events();
            show();
        }
        protected override void handler_for_animate_timer(object sender, ElapsedEventArgs e) => index_for_animate = ++index_for_animate % 3;
        void podpis_on_events()
        {
            place_for_game.KeyDown += change_naprav_KeyPress;
            enemy.get_coords_from_player += get_coords_from_me;
        }
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Pacman1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman3, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, 1);
        public void Deconstruct(out int xk, out int yk, out napravlenie to, out napravlenie naprav) => (xk, yk, to, naprav) = (this.xk, this.yk, this.to, this.naprav);
        public pacman(int number_of_unit, bool is_for_time, (int, int)[] coords) : base(1, number_of_unit, is_for_time, new Bitmap[] { new Bitmap(Properties.Resources.Pacman1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pacman3, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, napravlenie.left, coords, 1)
        {
            podpis_on_events();
            naprav = napravlenie.left;
        }
        public override void Dispose()
        {
            place_for_game.KeyDown -= change_naprav_KeyPress;
            enemy.get_coords_from_player -= get_coords_from_me;
            base.Dispose();
        }
    }
}
