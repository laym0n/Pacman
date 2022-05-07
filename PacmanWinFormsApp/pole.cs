using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace PacmanWinFormsApp
{
    enum type_of_object { wall, point, superbonus, gates, bonus, superpoint, barrier, ghosts_wall };
    [Serializable]
    class generall_class
    {
        protected static int size_of_kletki = 16;
        protected static Control place_for_game;
        public static pole game;
    }
    [Serializable]
    class kletka : generall_class, IDisposable
    {
        int x, y, xk, yk;
        object_in_kletka? content;
        public (int, int) my_coords { get => (xk, yk); }
        public (int, int) left_top { get => (x, y); }
        public object_in_kletka? my_object_in_kletka { get => content; }
        public bool free_or_not_for_phisic_object { get => content?.free_or_not_for_phisic_object ?? true; }
        public bool free_or_not_for_ghost_object { get => content?.free_or_not_for_ghost_object ?? true; }
        public (int, int) this[napravlenie storona_kletki] { get => (storona_kletki == napravlenie.left) ? (x, y + size_of_kletki / 2) : (storona_kletki == napravlenie.right) ? (x + size_of_kletki, y + size_of_kletki / 2) : (storona_kletki == napravlenie.down) ? (x + size_of_kletki / 2, y + size_of_kletki) : (x + size_of_kletki / 2, y); }
        public (int, int) center_of_kletki { get => (x + size_of_kletki / 2, y + size_of_kletki / 2); }
        public void create_object_in_me(type_of_object? creating_object, bool need_in_show = true, bool is_temporary = true)
        {
            if (creating_object != null)
            {
                delete_my_object(false);
                switch (creating_object)
                {
                    case type_of_object.wall: { content = new wall(x, y, xk, yk/*, this*/); } break;
                    case type_of_object.point: content = new point(x, y/*, this*/); break;
                    case type_of_object.superbonus: { content = new superbonus(x, y/*, this*/, is_temporary); ((superbonus)content).i_am_dead += () => delete_my_object(true); } break;
                    case type_of_object.gates: { content = new gates(x, y, xk, yk/*, this*/); } break;
                    case type_of_object.bonus: { content = new bonus(x, y/*, this*/, is_temporary); ((bonus)content).i_am_dead += () => delete_my_object(true); } break;
                    case type_of_object.superpoint: content = new superpoint(x, y/*, this*/); break;
                    case type_of_object.ghosts_wall: { content = new ghosts_wall(x, y, xk, yk/*, this*/); } break;
                    case type_of_object.barrier: { content = new barrier(x, y, xk, yk/*, this*/); ((barrier)content).i_am_dead_by_time += () => delete_my_object(true); } break;
                    default: content = null; break;
                };
                if (need_in_show)
                    content.show();
            }
            else
                content = null;
        }
        public void show() { if (content != null) content.show(); else place_for_game.CreateGraphics().DrawImage(Properties.Resources.SimpleKletka, x, y);}
        public void show(Graphics frame, int i, int j)
        {
            if (content != null)
                content.show(frame, i, j);
            else
                frame.DrawImage(Properties.Resources.SimpleKletka, j * size_of_kletki, i * size_of_kletki);
        }
        public kletka(int x, int y, int xk, int yk, pole game) => (this.xk, this.x, this.yk, this.y) = (xk, x, yk, y);
        public void check_eat()
        {
            if (content != null)
            {
                object_in_kletka deleted_object_in_kletka = content;
                content = null;
                deleted_object_in_kletka.eaten();
                deleted_object_in_kletka.Dispose();
            }
        }
        public void activate()
        {
            content?.activate();
            if (content is superbonus) ((superbonus)content).i_am_dead += () => delete_my_object(true);
            if (content is bonus) ((bonus)content).i_am_dead += () => delete_my_object(true);
            if (content is barrier) ((barrier)content).i_am_dead_by_time += () => delete_my_object(true);
            show();
        }
        public void delete_my_object(bool need_in_show = true)
        {
            if (content != null)
            {
                object_in_kletka deleted_object_in_kletka = content;
                content = null;
                deleted_object_in_kletka.Dispose();
                if (need_in_show) show();
            }
        }
        public void Dispose()
        {
            content?.Dispose();
        }
    }
    enum napravlenie { left, up, right, down };
    class pole : generall_class, IDisposable
    {
        int sizex, sizey, karta, LIFE, SCORE, max_score, LVL, eat, max_eat, KOL_VO_EATEN_ENEMY = 0, KOL_VO_BULLETS;
        List<List<kletka>> map = new();
        static Keys[] codes_for_button = { Keys.Shift, Keys.Space };
        public enum difficult { eazy, medium, hard};
        static difficult Difficult_of_game = difficult.medium;
        static public difficult difficult_of_the_game 
        {
            get => Difficult_of_game;
            set => difficult_changed(Difficult_of_game = value);
        }
        static (int, string)[] scores_of_players = new (int, string)[5];
        List<unit> all_units = new(); 
        bool GAME_IS_STOPPED;
        System.Drawing.Text.PrivateFontCollection fontcollection = new System.Drawing.Text.PrivateFontCollection();
        private int score 
        { 
            get => SCORE; 
            set
            {
                panels[2].Controls[1].BeginInvoke(new Action(() => panels[2].Controls[0].Controls[1].Text = (SCORE = value).ToString()));
                if (SCORE > max_score)
                    panels[2].Controls[2].BeginInvoke(new Action(() => panels[2].Controls[2].Controls[1].Text = (max_score = SCORE).ToString()));
            }
        }
        private int lvl
        {
            get => LVL;
            set => panels[2].Controls[0].Controls[0].BeginInvoke(new Action(() => panels[2].Controls[1].Controls[1].Text = (LVL = value).ToString()));
        }
        private int kol_vo_eaten_enemy
        {
            get => KOL_VO_EATEN_ENEMY;
            set => panels[0].Controls[1].Controls[0].BeginInvoke(new Action(() => panels[0].Controls[1].Controls[0].Text = "X" + (KOL_VO_EATEN_ENEMY = value).ToString()));
        }
        private int kol_vo_bullets
        {
            get => KOL_VO_BULLETS;
            set => panels[0].Controls[2].Controls[0].BeginInvoke(new Action(() => panels[0].Controls[2].Controls[0].Text = "X" + (KOL_VO_BULLETS = value).ToString()));
        }
        private int life{ get => LIFE; set { LIFE = value; panels[0].Controls[0].Controls[0].BeginInvoke(new Action(() => panels[0].Controls[0].Controls[0].Text = "X" + value.ToString())); } }
        Mutex mutex_for_objects = new();
        Mutex mutex_for_map = new();
        Control[] panels;
        Control[,] panels_for_showing_score;
        public bool game_is_stopped_on_pause { get => GAME_IS_STOPPED; private set { if (GAME_IS_STOPPED != value) { if (GAME_IS_STOPPED = value) stop_game(); else start_game(); } } }
        public bool game_is_going { get; private set; }
        public bool map_is_not_exsist { get; private set; }

        public event Action<int, int, int> restart_event;
        public event Action start_game;
        public event Action stop_game;
        public event Action game_ended;
        public event Action<type_of_game> game_is_saving;
        public event Action<type_of_game> game_is_loading;
        public event Action<type_of_game> units_is_loading;
        public static event Action<difficult> difficult_changed;
        public enum type_of_game { begin_of_classic, begin_of_new, saved_of_classic, saved_of_new };
        static public void change_codes_for_buttons(Keys[] new_buttons) => codes_for_button = new_buttons;
        void handler_of_press_key(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { game_is_stopped_on_pause = !game_is_stopped_on_pause; panels[1].BeginInvoke(new Action(() => panels[1].Visible = game_is_stopped_on_pause)); }
            else if(e.KeyCode == codes_for_button[1]) delete_ghost_walls();
            else if (e.KeyCode == codes_for_button[0]) create_bullet();
        }
        void create_bullet()
        {
            if (kol_vo_bullets > 0)
            {
                kol_vo_bullets--;
                (int xk, int yk, napravlenie to, _) = ((pacman)all_units[0]);
                mutex_for_objects.WaitOne();
                all_units.Add(new bullet(all_units.Count, to, get_coords_for_object_in_center(xk, yk, to)));
                all_units[all_units.Count - 1].check_walls += check_walls_for_phisic_object;
                mutex_for_objects.ReleaseMutex();
            }
        }
        void save_game()
        {
            (string file, string file_for_units, string file_for_data_of_the_game) = (karta == 1) ? ("classic_game_saved.dat", "classic_game_saved_units.dat", "classic_game_saved_data.dat") : ("new_game_saved.dat", "new_game_saved_units.dat", "new_game_saved_data.dat");
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.Create))
            using (FileStream fs1 = new FileStream(file_for_units, FileMode.Create))
            using (FileStream fs2 = new FileStream(file_for_data_of_the_game, FileMode.Create))
            {
                formatter.Serialize(fs, map);
                formatter.Serialize(fs1, all_units);
                formatter.Serialize(fs2, new object[] { lvl, life, eat, map_is_not_exsist, score });
                
            }
            if (karta == 1) game_is_saving(type_of_game.saved_of_classic); else game_is_saving(type_of_game.saved_of_new);
        }
        void load_data_for_game()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            FileStream fs2 = (karta == 1) ? new FileStream("classic_game_saved_data.dat", FileMode.Open) : new FileStream("new_game_saved_data.dat", FileMode.Open);
            object[] data = (object[])formatter.Deserialize(fs2);
            (lvl, life, eat, map_is_not_exsist, score) = ((int)data[0], (int)data[1], (int)data[2], (bool)data[3], (int)data[4]);
            fs2.Dispose();
            fs2 = (karta == 1) ? new FileStream("classic_game_saved_data.dat", FileMode.Create) : new FileStream("new_game_saved_data.dat", FileMode.Create);
            fs2.Dispose();
        }
        void load_game(type_of_game type)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            string file = (type == type_of_game.begin_of_classic) ? "classic_game.dat" : (type == type_of_game.begin_of_new) ? "new_game.dat" : (type == type_of_game.saved_of_classic) ? "classic_game_saved.dat" : "new_game_saved.dat";
            FileStream fs = new FileStream(file, FileMode.Open);
            map = (List<List<kletka>>)formatter.Deserialize(fs);
            game_is_loading(type);
            fs.Dispose();
            if (type == type_of_game.saved_of_classic || type == type_of_game.saved_of_new)
            {
                fs = new FileStream(file, FileMode.Create);
                fs.Dispose();
            }
            (sizex, sizey) = (karta == 1) ? (32, 36) : (55, 38);
            (place_for_game.Width, place_for_game.Height) = (sizex * size_of_kletki, sizey * size_of_kletki);
            for (int i = 0; i < sizey; i++)
                for (int j = 0; j < sizex; j++)
                    map[i][j].activate();
            //fs = new FileStream(file, FileMode.Create);
            //formatter.Serialize(fs, map);
            //fs.Dispose();
        }
        void load_units(type_of_game type)
        {
            for (int i = 0; i < all_units.Count; i++)
                all_units[i].Dispose();
            all_units.Clear();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            string file_for_units = (type == type_of_game.begin_of_classic) ? "classic_game_units.dat" : (type == type_of_game.begin_of_new) ? "new_game_units.dat" : (type == type_of_game.saved_of_classic) ? "classic_game_saved_units.dat" : "new_game_saved_units.dat";
            FileStream fs = new FileStream(file_for_units, FileMode.Open);
            all_units = (List<unit>)formatter.Deserialize(fs);
            fs.Dispose();
            units_is_loading(type);
            if (type == type_of_game.saved_of_classic || type == type_of_game.saved_of_new)
            {
                fs = new FileStream(file_for_units, FileMode.Create);
                fs.Dispose();
            }
            for (int i = 0; i < all_units.Count; i++)
            {
                all_units[i].activate();
                if (all_units[i] is ghost)
                    all_units[i].check_walls += check_walls_for_ghost_object;
                else
                    all_units[i].check_walls += check_walls_for_phisic_object;
            }
        }
        void delete_ghost_walls()
        {
            if (kol_vo_eaten_enemy > 0)
            {
                (int xk, int yk, napravlenie to, napravlenie naprav) = (pacman)all_units[0];
                int xp = ((int)to - 1) % 2, yp = -((int)to - 2) % 2, i = 0;
                if (to == naprav)
                {
                    while (map[yk + i * yp][xk + i * xp].my_object_in_kletka is not generall_wall && xk + (i + 1) * xp < sizex && xk + (i + 1) * xp >= 0 && yk + (i + 1) * yp < sizey && yk + (i + 1) * yp >= 0) i++;
                    if (map[yk + i * yp][xk + i * xp].my_object_in_kletka is ghosts_wall)
                    {
                        kol_vo_eaten_enemy--;
                        while (map[yk + i * yp][xk + i * xp].my_object_in_kletka is ghosts_wall && xk + i * xp < sizex && xk + i * xp >= 0 && yk + i * yp < sizey && yk + i * yp >= 0)
                            map[yk + i * yp][xk + i++ * xp].delete_my_object();
                    }
                }
                else
                {
                    int xpnapr = ((int)naprav - 1) % 2, ypnapr = -((int)naprav - 2) % 2;
                    while (map[yk + yp * i + ypnapr][xk + xp * i + xpnapr].my_object_in_kletka is not ghosts_wall && map[yk + yp * i + ypnapr][xk + xp * i + xpnapr].my_object_in_kletka is generall_wall && map[yk + yp * i][xk + xp * i].my_object_in_kletka is not generall_wall && xk + i * xp < sizex && xk + i * xp >= 0 && yk + i * yp < sizey && yk + i * yp >= 0) i++;
                    if (map[yk + i * yp + ypnapr][xk + i * xp + xpnapr].my_object_in_kletka is ghosts_wall)
                    {
                        kol_vo_eaten_enemy--;
                        for (int j = 1; map[yk + yp * i + ypnapr * j][xk + i * xp + xpnapr * j].my_object_in_kletka is ghosts_wall; j++) map[yk + yp * i + ypnapr * j][xk + i * xp + xpnapr * j].delete_my_object();
                        for (int j = 0; map[yk + yp * i + ypnapr * j][xk + i * xp + xpnapr * j].my_object_in_kletka is ghosts_wall; j--) map[yk + yp * i + ypnapr * j][xk + i * xp + xpnapr * j].delete_my_object();
                    }
                }
            }
        }
        void restart_game()
        {
            game_is_stopped_on_pause = true;
            if (--life != 0)
            {
                if (karta == 1) load_units(type_of_game.begin_of_classic); else load_units(type_of_game.begin_of_new);
                restart_event(lvl, eat, life);
                game_is_stopped_on_pause = false;
            }
            else
                delete_this_game();
        }
        void increase_and_show_score(int count) => score += count;
        void next_lvl()
        {
            game_is_stopped_on_pause = true;
            delete_map_and_unit();
            if (karta == 1) load_game(type_of_game.begin_of_classic); else load_game(type_of_game.begin_of_new);
            (lvl, life, eat, map_is_not_exsist) = (lvl + 1, 4, 0, false);
            restart_game();
        }
        (int, int)[] get_coords_for_object_on_the_side(int xk, int yk, napravlenie to, napravlenie storona_kletki) => (to != storona_kletki) ? new (int, int)[] { map[yk][xk][storona_kletki], (xk, yk), map[yk][xk][to], map[yk][xk].center_of_kletki } : new (int, int)[] { map[yk - ((int)to - 2) % 2][xk + ((int)to - 1) % 2][(napravlenie)(((int)storona_kletki + 2) % 4)], (xk + ((int)to - 1) % 2, yk - ((int)to - 2) % 2), map[yk - ((int)to - 2) % 2][xk + ((int)to - 1) % 2][to], map[yk - ((int)to - 2) % 2][xk + ((int)to - 1) % 2].center_of_kletki };
        (int, int)[] get_coords_for_object_in_center(int xk, int yk, napravlenie to) => new (int, int)[] { map[yk][xk].center_of_kletki, (xk, yk), map[yk][xk][to], map[yk - ((int)to - 2) % 2][xk + ((int)to - 1) % 2].center_of_kletki };
        bool[] check_walls_for_phisic_object(int xk, int yk) => new bool[] { map[yk][xk - 1].free_or_not_for_phisic_object, map[yk + 1][xk].free_or_not_for_phisic_object, map[yk][xk + 1].free_or_not_for_phisic_object, map[yk - 1][xk].free_or_not_for_phisic_object };
        bool[] check_walls_for_ghost_object(int xk, int yk) => new bool[] { map[yk][xk - 1].free_or_not_for_ghost_object, map[yk + 1][xk].free_or_not_for_ghost_object, map[yk][xk + 1].free_or_not_for_ghost_object, map[yk - 1][xk].free_or_not_for_ghost_object };
        (int, int)[]? check_teleport(int xk, int yk, napravlenie to) => (to == napravlenie.left && xk <= 1) ? get_coords_for_object_on_the_side(sizex - 3, yk, to, napravlenie.right) : (to == napravlenie.right && xk >= sizex - 2) ? get_coords_for_object_on_the_side(2, yk, to, napravlenie.left) : (to == napravlenie.down && yk <= 1) ? get_coords_for_object_on_the_side(xk, sizey - 4, to, napravlenie.up) : (to == napravlenie.up && yk >= sizey - 3) ? get_coords_for_object_on_the_side(xk, 2, to, napravlenie.down) : null;
        void fill_backgraound_of_frame(unit.FrameEventArgs data)
        {
            mutex_for_map.WaitOne();
            if (game_is_going && !map_is_not_exsist && data.yk >= 0 && data.yk + data.height <= sizey && data.yk >= 0 && data.xk + data.width <= sizex)
                for (int i = 0; i < data.height; i++)
                    for (int j = 0; j < data.width; j++)
                        map[data.yk + i][data.xk + j].show(data.frame, data.height - 1 - i, j);
            mutex_for_map.ReleaseMutex();
        }
        void obnov_kletki(object sender, PaintEventArgs e)
        {
            Rectangle rectangle = e.ClipRectangle;
            if (sizex != 0 && !map_is_not_exsist && game_is_going)
            {
                (int x, int y) = map[sizey - 1][0].left_top;
                if (rectangle.IntersectsWith(new Rectangle(x, y, x + size_of_kletki * sizex, y + size_of_kletki * sizey)))
                {
                    Bitmap frame = new Bitmap((rectangle.Size.Width / size_of_kletki + 1) * size_of_kletki, (rectangle.Size.Height / size_of_kletki + 1) * size_of_kletki);
                    Graphics graphics = Graphics.FromImage(frame);
                    int yk = (rectangle.Top > y && rectangle.Bottom < y + sizey * size_of_kletki) ? (rectangle.Top - y) / size_of_kletki : (rectangle.Top <= y) ? 0 : sizey - 1;
                    int xk = (rectangle.Left > x && rectangle.Right < x + sizex * size_of_kletki) ? (rectangle.Left - x) / size_of_kletki : (rectangle.Left <= x) ? 0 : sizex - 1;
                    for (int i1 = 0, i = yk; i <= (rectangle.Bottom - y) / size_of_kletki && i < sizey; i++, i1++)
                        for (int j1 = 0, j = xk; j <= (rectangle.Right - x) / size_of_kletki && j < sizex; j++, j1++)
                            map[sizey - 1 - i][j].show(graphics, i1, j1);
                    (int cordx, int cordy) = map[sizey - 1 - yk][xk].left_top;
                    place_for_game.CreateGraphics().DrawImage(frame, cordx, cordy);
                }
            }
        }
        void delete_this_game()
        {
            if (life > 0)
            {
                save_game();
                deleting_game_in_exit_from_game();
            }
            else if (scores_of_players[^1].Item1 < score)
            {
                for (int i = 0; i < 4; i++)
                    panels[i].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[i]);
                panels[5].BeginInvoke(new Action<Panel>((panel) => panel.Visible = true), panels[5]);
            }
            else
            {
                panels[4].BeginInvoke(new Action<Panel>((panel) => panel.Visible = true), panels[4]);
                panels[0].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[0]);
                panels[1].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[1]);
                panels[2].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[2]);
                panels[3].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[3]);
                panels[5].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[5]);

            }
        }
        void deleting_game_in_exit_from_game()
        {
            game_ended();
            mutex_for_map.WaitOne();
            (score, lvl, sizex, sizey, game_is_going) = (0, 0, 0, 0, false);
            mutex_for_map.ReleaseMutex();
            delete_map_and_unit();
        }
        void delete_map_and_unit()
        {
            mutex_for_map.WaitOne();
            (game_is_stopped_on_pause, map_is_not_exsist) = (true, true);
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                    map[i][j].Dispose();
                map[i].Clear();
            }
            map.Clear();
            mutex_for_objects.WaitOne();
            for (int i = 0; i < all_units.Count; i++)
                all_units[i].Dispose();
            mutex_for_objects.ReleaseMutex();
            all_units.Clear();
            (sizex, sizey) = (0, 0);
            mutex_for_map.ReleaseMutex();
        }
        public pole(Control Form_for_game, Button button_for_continue, Button button_for_end_of_the_game)
        {
            fontcollection.AddFontFile("Teletactile.ttf");
            (place_for_game, generall_class.game) = (Form_for_game, this);
            panels = new Control[] { place_for_game.Controls[0], place_for_game.Controls[1], place_for_game.Controls[2], place_for_game.Controls[3], place_for_game.Controls[4], place_for_game.Controls[5] };
            (score, lvl, sizex, sizey, game_is_going, map_is_not_exsist) = (0, 0, 0, 0, false, true);
            place_for_game.Controls[1].BeginInvoke(new Action(() => place_for_game.Controls[1].Visible = false));
            button_for_continue.Click += delegate { 
                game_is_stopped_on_pause = true; 
                game_is_stopped_on_pause = !game_is_stopped_on_pause; 
                panels[1].BeginInvoke(new Action(() => { panels[1].Visible = game_is_stopped_on_pause; place_for_game.Focus(); }));  
            };
            button_for_end_of_the_game.Click += delegate 
            { 
                delete_this_game(); 
                panels[1].BeginInvoke(new Action(() => panels[1].Visible = false)); 
            };
            place_for_game.Controls[3].Controls[2].Click += delegate 
            { 
                place_for_game.Focus(); 
                next_lvl(); 
                set_interface(); 
            };
            place_for_game.Controls[3].Controls[3].Click += delegate 
            {
                place_for_game.Focus();
                if (karta == 1)
                {
                    load_game(type_of_game.saved_of_classic);
                    load_units(type_of_game.saved_of_classic);
                }
                else 
                { 
                    load_game(type_of_game.saved_of_new);
                    load_units(type_of_game.saved_of_new);
                }
                load_data_for_game();
                set_interface();
                game_is_stopped_on_pause = false;
            };
            panels_for_showing_score = new Control[5, 2] { { panels[4].Controls[0].Controls[0], panels[4].Controls[0].Controls[1] }, { panels[4].Controls[1].Controls[1], panels[4].Controls[1].Controls[0] }, { panels[4].Controls[2].Controls[1], panels[4].Controls[2].Controls[0] }, { panels[4].Controls[3].Controls[0], panels[4].Controls[3].Controls[1] }, { panels[4].Controls[4].Controls[0], panels[4].Controls[4].Controls[1] } };
            for (int i =0; i< scores_of_players.Length; i++)
            {
                panels_for_showing_score[i, 0].Text = scores_of_players[i].Item2;
                panels_for_showing_score[i, 1].Text = scores_of_players[i].Item1.ToString();
                //panels[4].Controls[i].Controls[0].Text = scores_of_players[i].Item2;
                //panels[4].Controls[i].Controls[1].Text = scores_of_players[i].Item1.ToString();
            }
            panels[4].Controls[^1].Click += delegate
            {

                panels[0].BeginInvoke(new Action<Panel>((panel) => panel.Visible = true), panels[0]);
                panels[2].BeginInvoke(new Action<Panel>((panel) => panel.Visible = true), panels[2]);
                panels[4].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[4]);
                deleting_game_in_exit_from_game();
            };
            panels[5].Controls[0].Click += delegate
            {
                for (int i = 4; i >= 0; i--)
                    if (score > scores_of_players[i].Item1 && i == 0)
                    {
                        scores_of_players[i] = (score, panels[5].Controls[1].Text);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i, 0], scores_of_players[i].Item2);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i, 1], scores_of_players[i].Item1.ToString());
                    }
                    else if (score > scores_of_players[i].Item1 && i != 0)
                    {
                        scores_of_players[i] = (Convert.ToInt32(panels_for_showing_score[i - 1, 1].Text), panels_for_showing_score[i - 1, 0].Text);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i, 0], scores_of_players[i].Item2);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i, 1], scores_of_players[i].Item1.ToString());
                    }
                    else
                    {
                        scores_of_players[i + 1] = (score, panels[5].Controls[1].Text);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i + 1, 0], scores_of_players[i + 1].Item2);
                        panels[4].BeginInvoke(new Action<Control, string>((panel, text) => panel.Text = text), panels_for_showing_score[i + 1, 1], scores_of_players[i + 1].Item1.ToString());
                        break;
                    }
                panels[5].BeginInvoke(new Action<Panel>((panel) => panel.Visible = false), panels[5]);
                panels[4].BeginInvoke(new Action<Panel>((panel) => panel.Visible = true), panels[4]);
            };
            place_for_game.KeyDown += handler_of_press_key;
            place_for_game.Paint += obnov_kletki;
            using (BinaryReader reader = new BinaryReader(File.Open("datamax_eat", FileMode.Open))) { max_score = reader.ReadInt32(); }
            place_for_game.Controls[2].Controls[2].Controls[1].Text = max_score.ToString();
            for (int i = 0; i < 3; i++)
                place_for_game.Controls[0].Controls[i].Controls[0].Font = new Font(fontcollection.Families[0], 2 * size_of_kletki, GraphicsUnit.Pixel);
            unit.check_teleport += check_teleport;
            redghost.get_coords_for_run_away += () => (0, sizey - 1);
            pinkghost.get_coords_for_run_away += () => (sizex - 1, sizey - 1);
            blueghost.get_coords_for_run_away += () => (sizex - 1, 0);
            orangeghost.get_coords_for_run_away += () => (0, 0);
            pacman.checking_eat_object_in_kletka += (xk, yk) => map[yk][xk].check_eat();
            pacman.pacman_dead += restart_game;
            ghost.ghost_eaten += (kol_vo_score) => { increase_and_show_score(kol_vo_score); kol_vo_eaten_enemy++; };
            simple_enemy.get_score += (score) => { increase_and_show_score(score); kol_vo_eaten_enemy++; };
            creater_of_ghost.get_ghost += (type_of_ghost) =>
            {
                mutex_for_objects.WaitOne();
                (int xk, int yk) = find_free_kletka().my_coords;
                switch (type_of_ghost)
                {
                    case 0: { all_units.Add(new redghost(true, all_units.Count, get_coords_for_object_in_center(xk,yk,napravlenie.left))); } break;
                    case 1: { all_units.Add(new pinkghost(true, all_units.Count, get_coords_for_object_in_center(xk, yk, napravlenie.up))); } break;
                    case 2: { all_units.Add(new blueghost(true, all_units.Count, get_coords_for_object_in_center(xk, yk, napravlenie.up))); } break;
                    case 3: { all_units.Add(new orangeghost(true, all_units.Count, get_coords_for_object_in_center(xk, yk, napravlenie.up))); } break;
                }
                all_units[all_units.Count - 1].check_walls += check_walls_for_ghost_object;
                mutex_for_objects.ReleaseMutex();
            };
            generall_bonus.eaten_generall_bonus_event += increase_and_show_score;
            point.eaten_point += point_eaten;
            superbonus.pribavlenie_bonusa += (superbonus.type_of_bonus type_bonusa) =>
            {
                if (type_bonusa == superbonus.type_of_bonus.deleting_ghost_wall) kol_vo_eaten_enemy++;
                else if (type_bonusa == superbonus.type_of_bonus.bullet) kol_vo_bullets++;
            };
            generall_wall.this_wall_destroyed_or_create += (xk, yk) =>
            {
                if (!map_is_not_exsist)
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (i != 0 || j != 0)
                                (map[yk + i][xk + j].my_object_in_kletka as generall_wall)?.nearest_wall_destoyed();
            };
            generall_wall.get_walls_for_changing_picture += (xk, yk) => new bool[,] { { map[yk - 1][xk - 1].free_or_not_for_phisic_object, map[yk - 1][xk].free_or_not_for_phisic_object, map[yk - 1][xk + 1].free_or_not_for_phisic_object }, { map[yk][xk - 1].free_or_not_for_phisic_object, false, map[yk][xk + 1].free_or_not_for_phisic_object }, { map[yk + 1][xk - 1].free_or_not_for_phisic_object, map[yk + 1][xk].free_or_not_for_phisic_object, map[yk + 1][xk + 1].free_or_not_for_phisic_object } };
            creater_of_barrier.barrier_created += (xk, yk) => { map[yk][xk].create_object_in_me(type_of_object.barrier); };
            bullet.delete_wall_in_my_kletka += (xk, yk) => { 
                if (map[yk][xk].my_object_in_kletka is generall_wall) 
                { 
                    map[yk][xk].delete_my_object(); 
                    return true; 
                } 
                else 
                    return false; 
            };
            unit.unit_is_deleting += (number) =>
            {
                mutex_for_objects.WaitOne();
                all_units[number].Dispose(); 
                all_units.RemoveAt(number);
                mutex_for_objects.ReleaseMutex();
            };
            unit.fill_background_in_frame += fill_backgraound_of_frame;
        }
        kletka find_free_kletka()
        {
            Random ran = new();
            int xk, yk;
            do { (xk, yk) = (ran.Next(2, sizex - 3), ran.Next(2, sizey - 3)); } while (!map[yk][xk].free_or_not_for_phisic_object);
            return map[yk][xk];
        }
        void point_eaten()
        {
            if (game_is_going && !map_is_not_exsist)
            {
                if (++eat == max_eat)
                    next_lvl();
                else
                {
                    if ((double)eat / max_eat >= 0.291 && (double)(eat - 1) / max_eat < 0.291 || (double)eat / max_eat >= 0.708 && (double)(eat - 1) / max_eat < 0.708)
                        find_free_kletka().create_object_in_me(type_of_object.bonus);
                    if (karta == 2 && (difficult_of_the_game == difficult.medium && eat % (max_eat / 3) > (eat + 1) % (max_eat / 3) || difficult_of_the_game == difficult.eazy && eat % (max_eat / 4) > (eat + 1) % (max_eat / 4) || difficult_of_the_game == difficult.hard && eat % (max_eat / 2) > (eat + 1) % (max_eat / 2)))//(karta == 2 && (difficult_of_the_game == difficult.medium && ((double)eat / max_eat >= 0.15 && (double)(eat - 1) / max_eat < 0.15 || (double)eat / max_eat >= 0.508 && (double)(eat - 1) / max_eat < 0.508)))
                        find_free_kletka().create_object_in_me(type_of_object.superbonus);
                    if (karta == 2 && (difficult_of_the_game == difficult.medium && eat % (max_eat / 15) > (eat + 1) % (max_eat / 15) || difficult_of_the_game == difficult.eazy && eat % (max_eat / 7) > (eat + 1) % (max_eat / 7) || difficult_of_the_game == difficult.hard && eat % (max_eat / 25) > (eat + 1) % (max_eat / 25)))
                    {
                        mutex_for_objects.WaitOne();
                        kletka spavn = find_free_kletka();
                        switch (new Random().Next() % 2)
                        {
                            case 0: all_units.Add(new creater_of_ghost(all_units.Count, new (int, int)[] { spavn.center_of_kletki, spavn.my_coords, spavn[napravlenie.left], spavn.center_of_kletki })); break;
                            case 1: all_units.Add(new creater_of_barrier(all_units.Count, new (int, int)[] { spavn.center_of_kletki, spavn.my_coords, spavn[napravlenie.left], spavn.center_of_kletki })); break;
                        }
                        all_units[all_units.Count - 1].check_walls += check_walls_for_phisic_object;
                        //all_units.Add(new redghost(false, all_units.Count, new (int, int)[] { spavn.center_of_kletki, spavn.my_coords, spavn[napravlenie.left], spavn.center_of_kletki }));
                        //all_units[all_units.Count - 1].check_walls += check_walls_for_ghost_object;
                        mutex_for_objects.ReleaseMutex();
                    }
                }
            }
        }
        void set_karta((int, int) data)
        {
            ((karta, max_eat), game_is_going) = (data, true);
            if (karta == 1)
            {
                (panels[2].Controls[0].Controls[0].Font, panels[2].Controls[0].Controls[1].Font, panels[2].Controls[1].Controls[0].Font, panels[2].Controls[1].Controls[1].Font, panels[2].Controls[2].Controls[0].Font, panels[2].Controls[2].Controls[1].Font, panels[0].Controls[1].Visible, panels[0].Controls[2].Visible) = (new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki * 3 / 4, GraphicsUnit.Pixel), false, false);
                ghost.get_coords_for_moving_in_house += () => { (_, int y1) = map[17][0].center_of_kletki; (_, int y2) = map[19][0].center_of_kletki; return (y1, y2); };
                ghost.get_absolute_coords_of_center_house += () => map[18][sizex / 2][napravlenie.left];
                ghost.get_coords_of_place_over_gates += (to) => get_coords_for_object_on_the_side(sizex / 2, 21, to, napravlenie.left);
            }
            else
            {
                (panels[2].Controls[0].Controls[0].Font, panels[2].Controls[0].Controls[1].Font, panels[2].Controls[1].Controls[0].Font, panels[2].Controls[1].Controls[1].Font, panels[2].Controls[2].Controls[0].Font, panels[2].Controls[2].Controls[1].Font, panels[0].Controls[1].Visible, panels[0].Controls[2].Visible) = (new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), new Font(fontcollection.Families[0], size_of_kletki, GraphicsUnit.Pixel), true, true);
                (kol_vo_eaten_enemy, kol_vo_bullets) = ( 0, 10);
                ghost.get_coords_for_moving_in_house += () => { (_, int y1) = map[16][0].center_of_kletki; (_, int y2) = map[20][0].center_of_kletki; return (y1, y2); };
                ghost.get_absolute_coords_of_center_house += () => map[18][sizex / 2].center_of_kletki;
                ghost.get_coords_of_place_over_gates += (to) => get_coords_for_object_in_center(sizex / 2, 22, to);
            }
            FileStream fs = (karta == 1) ? new FileStream("classic_game_saved.dat", FileMode.Open) : new FileStream("new_game_saved.dat", FileMode.Open);
            if (fs.Length == 0)
            {
                next_lvl();
                set_interface();
            }
            else
                (panels[3].Visible, panels[2].Visible, panels[0].Visible) = (true, false, false);
            fs.Dispose();
            (place_for_game.Width, place_for_game.Height) = (karta == 1) ? (32 * size_of_kletki, 36 * size_of_kletki) : (55 * size_of_kletki, 38 * size_of_kletki);
            panels[3].Location = new Point(place_for_game.Width / 2 - panels[3].Width / 2, place_for_game.Height / 2 - panels[3].Height / 2);
        }
        void set_interface()
        {
            ((int x1, int y1), (int x2, _), (_, int y2), (int x3, _), (_, int y3), (int x4, _), (_, int y4)) = (map[sizey - 1][2].left_top, map[1][2].center_of_kletki, map[1][2].left_top, map[1][sizex - 2 - 8].center_of_kletki, map[1][sizex - 2 - 8].left_top, map[1][sizex / 2 - 3].center_of_kletki, map[1][sizex / 2 - 3].left_top);
            panels[1].Location = new Point(place_for_game.Width / 2 - panels[1].Width / 2, place_for_game.Height / 2 - panels[1].Height / 2);//Меню паузы
            panels[2].Size = new Size((sizex - 4) * size_of_kletki, size_of_kletki * 2);// размер верхней панели
            panels[2].Location = new Point(x1, y1);//расположение верхней панели
            panels[2].Controls[0].Location = new Point((sizex / 4 - 6) * size_of_kletki, 0);//score
            panels[2].Controls[2].Location = new Point((sizex / 2 - 7) * size_of_kletki, 0);//maxscore
            panels[2].Controls[1].Location = new Point((sizex * 3 / 4 - 3) * size_of_kletki, 0);//lvl
            panels[0].Size = new Size((sizex - 4) * size_of_kletki, size_of_kletki * 2);// размер нижней панели
            panels[0].Location = new Point(x1, (sizey - 2) * size_of_kletki);//расположение нижней панели
            panels[0].Controls[0].Location = new Point(x2, 0);//lifes
            panels[0].Controls[0].Size = new Size(sizex / 4 * size_of_kletki, 2 * size_of_kletki);//lifes
            panels[0].Controls[0].Controls[1].Size = new Size(2 * size_of_kletki, 2 * size_of_kletki);//picture of lifes
            panels[0].Controls[0].Controls[0].Size = new Size(panels[0].Controls[0].Width - 2 * size_of_kletki, 2 * size_of_kletki);//label of lifes
            panels[0].Controls[0].Controls[0].Location = new Point(2 * size_of_kletki, panels[0].Controls[0].Height / 6);//label of lifes
            panels[0].Controls[1].Location = new Point((sizex / 2 - 4) * size_of_kletki, 0);//enemys
            panels[0].Controls[1].Size = new Size(sizex / 4 * size_of_kletki, 2 * size_of_kletki);//enemys
            panels[0].Controls[1].Controls[1].Size = new Size(2 * size_of_kletki, 2 * size_of_kletki);//picture of enemys
            panels[0].Controls[1].Controls[0].Size = new Size(panels[0].Controls[1].Width - 2 * size_of_kletki, 2 * size_of_kletki);//label of enemys
            panels[0].Controls[1].Controls[0].Location = new Point(2 * size_of_kletki, panels[0].Controls[1].Height / 6);//label of enemys
            panels[0].Controls[2].Location = new Point((sizex * 3 / 4/* - 3*/) * size_of_kletki, 0);//bullets
            panels[0].Controls[2].Size = new Size(sizex / 4 * size_of_kletki, 2 * size_of_kletki);//bullets
            panels[0].Controls[2].Controls[1].Size = new Size(size_of_kletki, 2 * size_of_kletki);//picture of bullets
            panels[0].Controls[2].Controls[0].Size = new Size(panels[0].Controls[1].Width - size_of_kletki, 2 * size_of_kletki);//label of enemys
            panels[0].Controls[2].Controls[0].Location = new Point(size_of_kletki, panels[0].Controls[2].Height / 6);//label of enemys
            panels[3].Location = new Point(place_for_game.Width / 2 - panels[3].Width / 2, place_for_game.Height / 2 - panels[3].Height / 2);
            (panels[3].Visible, panels[2].Visible, panels[1].Visible, panels[0].Visible) = (false, true, false, true);
            (panels[2].Controls[0].Controls[1].Size, panels[2].Controls[1].Controls[1].Size, panels[2].Controls[2].Controls[1].Size) = (panels[2].Controls[0].Controls[0].Size, panels[2].Controls[1].Controls[0].Size, panels[2].Controls[2].Controls[0].Size);
            panels[2].Controls[0].Width = panels[2].Controls[0].Controls[0].Width;
            panels[2].Controls[1].Width = panels[2].Controls[1].Controls[0].Width;
            panels[2].Controls[2].Width = panels[2].Controls[2].Controls[0].Width;
            panels[4].Location = new Point(place_for_game.Width / 2 - panels[4].Width / 2, place_for_game.Height / 2 - panels[4].Height / 2);
            panels[5].Location = new Point(place_for_game.Width / 2 - panels[5].Width / 2, place_for_game.Height / 2 - panels[5].Height / 2);
            panels[2].Size = new Size((sizex - 4) * size_of_kletki, size_of_kletki * 2);
        }
        public void create_classic_game() => set_karta((1, 242));
        public void create_new_game() => set_karta((2, 335));
        public void Dispose()
        {
            //scores_of_players[0].Item2 = "VICTOR";
            //scores_of_players[1].Item2 = "ELISEY";
            //scores_of_players[2].Item2 = "ILYA";
            //scores_of_players[3].Item2 = "VLAD";
            //scores_of_players[4].Item2 = "ELISEY";
            using (BinaryWriter writer = new BinaryWriter(File.Open("datamax_eat", FileMode.Create))) { writer.Write(max_score); }
            using (BinaryWriter writer = new BinaryWriter(File.Open("table_of_records.dat", FileMode.Create))) 
            {
                for (int i = 0; i < scores_of_players.Length; i++)
                {
                    writer.Write(scores_of_players[i].Item1);
                    writer.Write(scores_of_players[i].Item2);
                }
            }
            if (game_is_going)
            {
                game_is_stopped_on_pause = true;
                delete_this_game();
            }
        }
        static pole()
        {
            using (BinaryReader reader = new BinaryReader(File.Open("table_of_records.dat", FileMode.Open)))
            {
                for (int i = 0; i < scores_of_players.Length; i++)
                    scores_of_players[i] = (reader.ReadInt32(), reader.ReadString());
            }
        }
    }
}