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
    abstract class ghost : enemy
    {
        public enum Condition { run_away, weakness, chase, dead, moving_in_house, entering_to_house, coming_to_free }
        int xt, yt, points_to_freedom, time_to_freedom, normal_number_of_points, normal_time_to_freedom;
        Condition condition;
        [field: NonSerialized]
        System.Timers.Timer timer_for_getting_free;
        static Mutex mutex_for_condition = new(), mutex_for_load_times = new();
        static int time_for_moving;
        static bool is_weakness = false, last_second_in_weakness = false;
        protected static int time_for_object_for_time = 12500;
        static int[] times_for_conditions;
        static int time_for_weakness, index_in_times, kol_vo_of_eaten_ghosts;
        static System.Timers.Timer timer_for_generall_condition, timer_for_last_second_in_weakness;
        static event Action generall_condition_changed;
        static event Action weakness_ended_event;
        public static event Func<(int, int)> get_coords_for_moving_in_house;
        public static event Func<napravlenie, (int, int)[]> get_coords_of_place_over_gates;
        public static event Action<int> ghost_eaten;
        public static event Func<(int, int)> get_absolute_coords_of_center_house;
        Bitmap[] my_animate_weakness, my_animate_end_weakness, my_animate_dead;
        static void delete_timer_for_last_second()
        {
            timer_for_last_second_in_weakness.Stop();
            timer_for_last_second_in_weakness.Dispose();
            game.start_game -= start_timer_for_last_second;
            game.stop_game -= stop_timer_for_last_second;
        }
        static void set_timer_in_weakness()
        {
            mutex_for_condition.WaitOne();
            stop_timer_for_condition();
            timer_for_generall_condition.Dispose();
            timer_for_generall_condition = new(time_for_weakness);
            if (timer_for_last_second_in_weakness != null)
                delete_timer_for_last_second();
            last_second_in_weakness = false;
            timer_for_last_second_in_weakness = new(time_for_weakness - 1000) { AutoReset = false };
            timer_for_last_second_in_weakness.Elapsed += delegate { last_second_in_weakness = true; };
            game.start_game += start_timer_for_last_second;
            game.stop_game += stop_timer_for_last_second;
            timer_for_generall_condition.Elapsed += delegate { weakness_ended_event(); };
            timer_for_last_second_in_weakness.Start();
            timer_for_generall_condition.Start();
            is_weakness = true;
            mutex_for_condition.ReleaseMutex();
        }
        static void start_timer_for_last_second() => timer_for_last_second_in_weakness.Start();
        static void stop_timer_for_last_second() { last_second_in_weakness = false; timer_for_last_second_in_weakness.Start(); }
        static void save_times(pole.type_of_game type)
        {
            mutex_for_load_times.WaitOne();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new();
            string file = (type == pole.type_of_game.saved_of_classic) ? "times_for_ghosts_classic_saved.dat" : "times_for_ghosts_new_saved.dat";
            using (System.IO.FileStream fs = new(file, System.IO.FileMode.Create))
            {
                formatter.Serialize(fs, new Object[] { times_for_conditions, time_for_weakness, index_in_times, kol_vo_of_eaten_ghosts, is_weakness, last_second_in_weakness, time_for_weakness, kol_vo_of_eaten_ghosts });
            }
            mutex_for_load_times.ReleaseMutex();
        }
        static void load_times(pole.type_of_game type)
        {
            if (type == pole.type_of_game.saved_of_classic || type == pole.type_of_game.saved_of_new)
            {
                mutex_for_load_times.WaitOne();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new();
                string file = (type == pole.type_of_game.saved_of_classic) ? "times_for_ghosts_classic_saved.dat" : "times_for_ghosts_new_saved.dat";
                using (System.IO.FileStream fs = new(file, System.IO.FileMode.Open))
                {
                    Object[] data = (Object[])formatter.Deserialize(fs);
                    (times_for_conditions, time_for_weakness, index_in_times, kol_vo_of_eaten_ghosts, is_weakness, last_second_in_weakness, time_for_weakness, kol_vo_of_eaten_ghosts) = ((int[])data[0], (int)data[1], (int)data[2], (int)data[3], (bool)data[4], (bool)data[5], (int)data[6], (int)data[7]);
                }
                if (is_weakness) set_timer_in_weakness(); else time_for_weakness_ended();
                mutex_for_load_times.ReleaseMutex();
            }
        }
        static void change_speed(pole.difficult dif) => time_for_moving = (dif == pole.difficult.eazy) ? 23 : (dif == pole.difficult.medium) ? 17 : 1;
        static ghost()
        {
            timer_for_generall_condition = new();
            game.game_is_loading += load_times;
            game.game_is_saving += save_times;
            timer_for_generall_condition.Elapsed += change_generall_timer;
            game.restart_event += set_data;
            game.units_is_loading += set_data_in_loading_units;
            pole.difficult_changed += change_speed;
            game.start_game += start_timer_for_condition;
            game.stop_game += stop_timer_for_condition;
            superpoint.superpoint_eaten += set_timer_in_weakness;
            weakness_ended_event += time_for_weakness_ended;
            game.game_ended += delete_data;
        }
        static void set_data_in_loading_units(pole.type_of_game type)
        {
            if (type == pole.type_of_game.begin_of_classic || type == pole.type_of_game.begin_of_new)
                index_in_times = 0;
        }
        static void delete_data()
        {
            (get_coords_for_moving_in_house, get_absolute_coords_of_center_house, get_coords_of_place_over_gates) = (null, null, null);
            if (timer_for_last_second_in_weakness != null)
                delete_timer_for_last_second();
        }
        static void set_data(int lvl, int eat, int left_lives)
        {
            if (timer_for_last_second_in_weakness != null)
                delete_timer_for_last_second();
            time_for_weakness = (lvl < 5) ? 7000 - (lvl - 1) * 500 : 4000;
            times_for_conditions = (lvl == 1) ? new int[] { 7000, 20000, 7000, 20000, 5000, 20000, 5000 } : (lvl >= 1 && lvl <= 3) ? new int[] { 7000, 20000, 7000, 20000, 5000 } : new int[] { 5000, 20000, 5000, 20000, 5000 };
            index_in_times = 0;
            (kol_vo_of_eaten_ghosts, timer_for_generall_condition.Interval) = (0, times_for_conditions[0]);
            last_second_in_weakness = false;
        }
        static void time_for_weakness_ended()
        {
            mutex_for_condition.WaitOne();
            (kol_vo_of_eaten_ghosts, last_second_in_weakness, is_weakness) = (0, false, false);
            timer_for_generall_condition.Stop();
            timer_for_generall_condition.Dispose();
            timer_for_generall_condition = new();
            timer_for_generall_condition.Elapsed += change_generall_timer;
            if (timer_for_last_second_in_weakness != null)
                delete_timer_for_last_second();
            if (index_in_times != times_for_conditions.Length)
            {
                timer_for_generall_condition.Interval = times_for_conditions[index_in_times];
                start_timer_for_condition();
            }
            mutex_for_condition.ReleaseMutex();
        }
        static void change_generall_timer(Object source, System.Timers.ElapsedEventArgs e)
        {
            mutex_for_condition.WaitOne();
            stop_timer_for_condition();
            ++index_in_times;
            generall_condition_changed();
            if (index_in_times != times_for_conditions.Length)
            {
                timer_for_generall_condition.Interval = times_for_conditions[index_in_times];
                start_timer_for_condition();
            }
            mutex_for_condition.ReleaseMutex();
        }
        static void start_timer_for_condition()
        {
            if (index_in_times != times_for_conditions.Length)
                timer_for_generall_condition.Start();
        }
        static void stop_timer_for_condition() => timer_for_generall_condition.Stop();
        protected override void show()
        {
            mutex_for_show.WaitOne();
            if (is_alive && (condition == Condition.dead || condition == Condition.entering_to_house))
                graphics.DrawImage(my_animate_dead[(int)to], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            else if (is_alive && condition == Condition.weakness)
            {
                if (last_second_in_weakness)
                    graphics.DrawImage(my_animate_end_weakness[index_for_animate], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
                else
                    graphics.DrawImage(my_animate_weakness[index_for_animate], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            }
            else if (is_alive)
                graphics.DrawImage(images_for_show[index_for_animate + (int)to * 2], x - size_of_kletki * 7 / 8, y - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        protected override void show(Graphics frame, int cordx, int cordy)
        {
            mutex_for_show.WaitOne();
            if (is_alive && (condition == Condition.dead || condition == Condition.entering_to_house))
                frame.DrawImage(my_animate_dead[(int)to], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            else if (is_alive && condition == Condition.weakness)
            {
                if (last_second_in_weakness)
                    frame.DrawImage(my_animate_end_weakness[index_for_animate], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
                else
                    frame.DrawImage(my_animate_weakness[index_for_animate], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            }
            else if (is_alive)
                frame.DrawImage(images_for_show[index_for_animate + (int)to * 2], cordx - size_of_kletki * 7 / 8, cordy - size_of_kletki * 7 / 8);
            mutex_for_show.ReleaseMutex();
        }
        void check_killing(pacman.pacman_EventArgs data)
        {
            if (!data.am_i_dead && xk == data.xk && yk == data.yk)
            {
                if (condition == Condition.weakness)
                {
                    ghost_eaten(200 * ++kol_vo_of_eaten_ghosts);
                    if (is_for_time) call_event_delete_me(number_of_unit); else set_condition_in_dead();
                }
                else if (condition != Condition.dead && condition != Condition.entering_to_house)
                    data.am_i_dead = true;
            }
        }
        void check_killing_by_bullet(int xkplayer, int ykplayer)
        {
            if (xk == xkplayer && yk == ykplayer && condition != Condition.coming_to_free && condition != Condition.dead && condition != Condition.entering_to_house && condition != Condition.moving_in_house)
            {
                ghost_eaten(200);
                if (is_for_time)
                    call_event_delete_me(number_of_unit);
                else
                    set_condition_in_dead();
            }
        }
        protected abstract (int, int) coords_for_run_away();
        abstract protected void set_coords_of_target(int xkp, int ykp, napravlenie top);
        void going_in_house()
        {
            if (to == napravlenie.up && y <= yt)
                to = napravlenie.down;
            else if (to == napravlenie.down && y >= xt)
                to = napravlenie.up;
        }
        void going_in_entering_to_house()
        {
            if (yt == y)
                set_condition_in_going_to_freedom();
        }
        void going_to_absolute_target_in_kletka()
        {
            going_to_target(false);
            if (x == xt && y == yt)
                set_condition_in_entering_to_house();
        }
        void going_to_freedom()
        {
            if (x > xt) { if (to != napravlenie.left) to = napravlenie.left; }
            else if (x < xt) { if (to != napravlenie.right) to = napravlenie.right; }
            else if (to != napravlenie.up) to = napravlenie.up;
            else if (x == xt && y == yt)
            {
                to = napravlenie.left;
                set_coords(get_coords_of_place_over_gates(to));
                set_condition_in_general_condition(false);
            }
        }
        void set_condition_in_dead()
        {
            timer_of_object.Interval = time_for_moving/*1*/;
            (int, int)[] coords_of_target = get_coords_of_place_over_gates(napravlenie.left);
            ((xt, yt), (xkt, ykt)) = (coords_of_target[0], coords_of_target[1]);
            condition = Condition.dead;
            proverka_povorota = going_to_absolute_target_in_kletka;
        }
        void set_condition_in_weakness(bool change_to = true)
        {
            timer_of_object.Interval = 30;
            condition = Condition.weakness;
            proverka_povorota = going_random;
            if (change_to) to = (napravlenie)(((int)to + 2) % 4);
            (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
        }
        void set_condition_in_entering_to_house()
        {
            if (get_absolute_coords_of_center_house != null) (xt, yt) = get_absolute_coords_of_center_house();
            condition = Condition.entering_to_house;
            proverka_povorota = going_in_entering_to_house;
            to = napravlenie.down;
        }
        void start_timer_to_freedom() => timer_for_getting_free?.Start();
        void stop_timer_to_freedom() => timer_for_getting_free?.Stop();
        void set_condition_in_moving_in_house()
        {
            point.eaten_point += point_eaten;
            condition = Condition.moving_in_house;
            proverka_povorota = going_in_house;
            game.start_game += start_timer_to_freedom;
            game.stop_game += stop_timer_to_freedom;
            if (get_coords_for_moving_in_house != null)(xt, yt) = get_coords_for_moving_in_house();
            timer_for_getting_free = (time_to_freedom > 0) ? new(time_to_freedom) { AutoReset = false } : new(1) { AutoReset = false };
            timer_for_getting_free.Elapsed += delegate
            {
                after_moving_in_house();
                set_condition_in_going_to_freedom();
            };
        }
        void after_moving_in_house()
        {
            point.eaten_point -= point_eaten;
            timer_for_getting_free?.Stop();
            timer_for_getting_free?.Dispose();
            game.start_game -= start_timer_to_freedom;
            game.stop_game -= stop_timer_to_freedom;
        }
        void set_condition_in_going_to_freedom()
        {
            if (get_coords_of_place_over_gates != null) 
                (xt, yt) = get_coords_of_place_over_gates(napravlenie.left)[0];
            condition = Condition.coming_to_free;
            proverka_povorota = going_to_freedom;
        }
        void set_condition_in_general_condition(bool change_to)
        {
            if (index_in_times % 2 == 0) 
                set_condition_in_run_away(change_to); 
            else 
                set_condition_in_chase(change_to);
        }
        void set_condition_in_chase(bool change_to)
        {
            condition = Condition.chase;
            proverka_povorota = () => { (int xkp, int ykp, napravlenie top) = coords_from_player(); set_coords_of_target(xkp, ykp, top); going_to_target(false); };
            if (change_to)
            {
                to = (napravlenie)(((int)to + 2) % 4);
                (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
            }
        }
        void set_condition_in_run_away(bool change_to)
        {
            (xkt, ykt) = coords_for_run_away();
            condition = Condition.run_away;
            proverka_povorota = () => going_to_target(false);
            if (change_to)
            {
                to = (napravlenie)(((int)to + 2) % 4);
                (x_center_kletki, y_center_kletki) = (x_center_kletki + ((int)to - 1) % 2 * size_of_kletki, y_center_kletki + ((int)to - 2) % 2 * size_of_kletki);
            }
        }
        void set_condition_after_weakness()
        {
            if (condition == Condition.weakness)
            {
                timer_of_object.Interval = time_for_moving/*1*/;
                set_condition_in_general_condition(true);
            }
        }
        void point_eaten()
        {
            if (condition == Condition.moving_in_house)
            {
                if (--points_to_freedom == 0)
                {
                    after_moving_in_house();
                    set_condition_in_going_to_freedom();
                }
                else
                {
                    timer_for_getting_free?.Stop();
                    timer_for_getting_free?.Start();
                }
            }
        }
        protected override void set_other_in_restart(int lvl, int eat, int left_lives)
        {
            (timer_of_object.Interval, points_to_freedom, time_to_freedom) = (time_for_moving/*1*/, normal_number_of_points - lvl * 15 - (3 - left_lives) * (3 - left_lives) * 15, normal_time_to_freedom - lvl * 200 - (3 - left_lives) * (3 - left_lives) * 250);
            if (condition == Condition.moving_in_house)
            {
                if (points_to_freedom <= 0 || time_to_freedom <= 0)
                {
                    after_moving_in_house();
                    set_condition_in_going_to_freedom();
                }
                else
                    timer_for_getting_free.Interval = time_to_freedom;
            }
        }
        protected override void handler_for_animate_timer(object sender, ElapsedEventArgs e) => index_for_animate = ++index_for_animate % 2;
        protected override void handler_for_dead_timer(object sender, System.Timers.ElapsedEventArgs args) => call_event_delete_me(this.number_of_unit);
        protected ghost(int interval, Condition new_condition, Bitmap[] pictures, bool is_for_time, int normal_time_to_free, int normal_points_to_free, int number_of_unit, (int, int)[] coords, napravlenie napr) : base(time_for_moving/*interval*/, is_for_time, number_of_unit, pictures, napr, coords, time_for_object_for_time)
        {
            (this.normal_number_of_points, this.normal_time_to_freedom) = (normal_points_to_free, normal_time_to_free);
            make_images();
            podpis_on_events(new_condition);
        }
        void make_images()
        {
            my_animate_weakness = new Bitmap[] { new Bitmap(Properties.Resources.Weakness1, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Weakness2, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) };
            my_animate_end_weakness = new Bitmap[] { new Bitmap(Properties.Resources.WeaknessEnd1, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.WeaknessEnd2, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) };
            my_animate_dead = new Bitmap[] { new Bitmap(Properties.Resources.EyeLeft, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.EyeUp, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.EyeRight, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.EyeDown, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) };
            for (int i = 0; i < my_animate_dead.Length; i++)
                my_animate_dead[i].MakeTransparent(Color.Black);
            for (int i = 0; i < my_animate_end_weakness.Length; i++)
                my_animate_end_weakness[i].MakeTransparent(Color.Black);
            for (int i = 0; i < my_animate_weakness.Length; i++)
                my_animate_weakness[i].MakeTransparent(Color.Black);
        }
        void superpoint_eaten() { if (condition == Condition.chase || Condition.run_away == condition) set_condition_in_weakness(true); }
        void podpis_on_events(Condition my_condition)
        {
            if (my_condition == Condition.chase || my_condition == Condition.run_away)
                set_condition_in_general_condition(false);
            else if (my_condition == Condition.moving_in_house)
                set_condition_in_moving_in_house();
            else if (my_condition == Condition.weakness)
                set_condition_in_weakness(false);
            else if (my_condition == Condition.coming_to_free)
                set_condition_in_going_to_freedom();
            else if (my_condition == Condition.dead)
                set_condition_in_dead();
            else if (my_condition == Condition.entering_to_house)
                set_condition_in_entering_to_house();
            generall_condition_changed += after_generall_condition_changed;
            pacman.check_killing += check_killing;
            bullet.check_killing_units += check_killing_by_bullet;
            superpoint.superpoint_eaten += superpoint_eaten;
            weakness_ended_event += set_condition_after_weakness;
        }
        void after_generall_condition_changed()
        {
            if (this.condition == Condition.run_away || this.condition == Condition.chase)
                set_condition_in_general_condition(true);
        }
        public override void activate()
        {
            base.activate();
            podpis_on_events(condition);
        }
        public override void Dispose()
        {
            if (Condition.moving_in_house == condition)
                after_moving_in_house();
            generall_condition_changed -= after_generall_condition_changed;
            pacman.check_killing -= check_killing;
            bullet.check_killing_units -= check_killing_by_bullet;
            superpoint.superpoint_eaten -= superpoint_eaten;
            weakness_ended_event -= set_condition_after_weakness;
            for (int i = 0; i < my_animate_dead.Length; i++)
                my_animate_dead[i].Dispose();
            for (int i = 0; i < my_animate_end_weakness.Length; i++)
                my_animate_end_weakness[i].Dispose();
            for (int i = 0; i < my_animate_weakness.Length; i++)
                my_animate_weakness[i].Dispose();
            base.Dispose();
        }
    }
    [Serializable]
    class redghost : ghost
    {
        public static event Func<(int, int)> get_coords_for_run_away;
        static int points_to_free = 0, time_to_free = 0;
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Red1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_object_for_time);
        public redghost(bool is_for_time, int number_of_unit, (int, int)[] coords, Condition new_condition = Condition.chase) : base(1, new_condition, new Bitmap[] { new Bitmap(Properties.Resources.Red1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Red2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, is_for_time, time_to_free, points_to_free, number_of_unit, coords, napravlenie.left) { }
        protected override (int, int) coords_for_run_away() => get_coords_for_run_away();
        protected override void set_coords_of_target(int xkp, int ykp, napravlenie top) => (xkt, ykt) = (xkp, ykp);
    }
    [Serializable]
    class blueghost : ghost
    {
        public static event Func<(int, int)> get_coords_for_run_away;
        static int time_to_free = 8000, points_to_free = 60;
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Blue1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_object_for_time);
        protected override (int, int) coords_for_run_away() => get_coords_for_run_away();
        public blueghost(bool is_for_time, int number_of_unit, (int, int)[] coords, Condition new_condition = Condition.chase) : base(1, new_condition, new Bitmap[] { new Bitmap(Properties.Resources.Blue1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Blue2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, is_for_time, time_to_free, points_to_free, number_of_unit, coords, napravlenie.up) { }
        protected override void set_coords_of_target(int xkp, int ykp, napravlenie top) => (xkt, ykt) = (2 * xkp - xk, 2 * ykp - yk);
    }
    [Serializable]
    class pinkghost : ghost
    {
        public static event Func<(int, int)> get_coords_for_run_away;
        static int time_to_free = 4000, points_to_free = 30;
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Pink1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_object_for_time);
        public pinkghost(bool is_for_time, int number_of_unit, (int, int)[] coords, Condition new_condition = Condition.chase) : base(1, new_condition, new Bitmap[] { new Bitmap(Properties.Resources.Pink1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Pink2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, is_for_time, time_to_free, points_to_free, number_of_unit, coords, napravlenie.up) { }
        protected override (int, int) coords_for_run_away() => get_coords_for_run_away();
        protected override void set_coords_of_target(int xkp, int ykp, napravlenie top) => (xkt, ykt) = (xkp + ((int)top - 1) % 2 * 4, ykp - ((int)top - 2) % 2 * 4);
    }
    [Serializable]
    class orangeghost : ghost
    {
        public static event Func<(int, int)> get_coords_for_run_away;
        static int time_to_free = 12000, points_to_free = 90;
        protected override (int, Bitmap[], int) get_data_for_activate() => (1, new Bitmap[] { new Bitmap(Properties.Resources.Orange1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, time_for_object_for_time);
        public orangeghost(bool is_for_time, int number_of_unit, (int, int)[] coords = null, Condition new_condition = Condition.chase) : base(1, new_condition, new Bitmap[] { new Bitmap(Properties.Resources.Orange1Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Left, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Up, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Right, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange1Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)), new Bitmap(Properties.Resources.Orange2Down, new Size(size_of_kletki * 14 / 8, size_of_kletki * 14 / 8)) }, is_for_time, time_to_free, points_to_free, number_of_unit, coords, napravlenie.up) { }
        protected override (int, int) coords_for_run_away() => get_coords_for_run_away();
        protected override void set_coords_of_target(int xkp, int ykp, napravlenie top) => (xkt, ykt) = (Math.Pow(xkp - xk, 2) + Math.Pow(ykp - yk, 2) > 64) ? (xkp, ykp) : coords_for_run_away();
    }
}
