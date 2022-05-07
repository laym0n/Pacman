using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace PacmanWinFormsApp
{
    [Serializable]
    abstract class object_in_kletka : generall_class, IDisposable
    {
        protected int x, y;
        protected Bitmap image_for_show;
        [NonSerialized]
        protected Mutex mutex_for_show;
        [NonSerialized]
        Graphics graphics;
        abstract public bool free_or_not_for_phisic_object { get; }
        abstract public bool free_or_not_for_ghost_object { get; }
        public object_in_kletka(int x, int y, Bitmap my_image)
        {
            (this.x, this.y, image_for_show, mutex_for_show, graphics) = (x, y, my_image, new(), place_for_game.CreateGraphics());
        }
        protected abstract Bitmap get_picture();
        public virtual void activate() 
        { 
            mutex_for_show = new();
            graphics = place_for_game.CreateGraphics();
        }
        public virtual void eaten() { }
        public virtual void show()
        {
            mutex_for_show.WaitOne();
            if (image_for_show != null)
                graphics.DrawImage(image_for_show, x, y);
            mutex_for_show.ReleaseMutex();
        }
        public virtual void show(Graphics frame, int i, int j)
        {
            mutex_for_show.WaitOne();
            if (image_for_show != null)
                frame.DrawImage(image_for_show, j * size_of_kletki, i * size_of_kletki);
            mutex_for_show.ReleaseMutex();
        }
        virtual public void Dispose()
        {
            graphics = null;
            mutex_for_show.WaitOne();
            image_for_show?.Dispose();
            image_for_show = null;
            mutex_for_show.ReleaseMutex();
            mutex_for_show.Dispose();
        }
    }
    [Serializable]
    abstract class generall_bonus : object_in_kletka
    {
        public static event Action<int> eaten_generall_bonus_event;
        public override bool free_or_not_for_phisic_object { get => true; }
        public override bool free_or_not_for_ghost_object { get => true; }
        protected generall_bonus(int x, int y, Bitmap my_image) : base(x, y, my_image) { }
        protected void this_eaten(int score) => eaten_generall_bonus_event(score);
    }
    [Serializable]
    abstract class temporary_generall_bonus : generall_bonus
    {
        [NonSerialized]
        System.Timers.Timer my_timer_for_dead;
        [field: NonSerialized]
        public event Action i_am_dead;
        public bool is_temporary;
        void call_event_dead(int int1, int int2, int int3) => i_am_dead();
        protected temporary_generall_bonus(int x, int y, bool is_temporary, Bitmap my_image) : base(x, y, my_image)
        {
            if (this.is_temporary = is_temporary)
            {
                my_timer_for_dead = new(10000) { AutoReset = false };
                my_timer_for_dead.Elapsed += delegate { i_am_dead(); };
                if (!game.game_is_stopped_on_pause)
                    start_timer();
                game.start_game += start_timer;
                game.stop_game += stop_timer;
                game.restart_event += call_event_dead;
            }
        }
        public override void activate()
        {
            base.activate();
            if (this.is_temporary)
            {
                my_timer_for_dead = new(10000) { AutoReset = false };
                my_timer_for_dead.Elapsed += delegate { i_am_dead(); };
                if (!game.game_is_stopped_on_pause)
                    start_timer();
                game.start_game += start_timer;
                game.stop_game += stop_timer;
                game.restart_event += call_event_dead;
            }
        }
        public override void Dispose()
        {
            i_am_dead = null;
            if (is_temporary)
            {
                my_timer_for_dead.Enabled = false;
                my_timer_for_dead.Dispose();
                game.start_game -= start_timer;
                game.stop_game -= stop_timer;
                game.restart_event -= call_event_dead;
            }
            base.Dispose();
        }
        void start_timer() => my_timer_for_dead.Start();
        void stop_timer() => my_timer_for_dead.Stop();
    }
    [Serializable]
    abstract class generall_wall : object_in_kletka
    {
        public static event Func<int, int, bool[,]> get_walls_for_changing_picture;
        public static event Action<int, int> this_wall_destroyed_or_create;
        protected enum napravlenie_for_wall { horizontal, vertical, left_up, right_up, right_down, left_down };
        protected napravlenie_for_wall? naprav;
        protected int xk, yk;
        protected virtual void opred_naprav(bool[,] walls_around)
        {
            if (!walls_around[0, 1] && !walls_around[2, 1] && (walls_around[1, 0] || walls_around[1, 2])) naprav = napravlenie_for_wall.vertical;
            else if ((walls_around[0, 1] || walls_around[2, 1]) && !walls_around[1, 0] && !walls_around[1, 2]) naprav = napravlenie_for_wall.horizontal;
            else if (!walls_around[2, 1] && !walls_around[1, 2] && (walls_around[2, 2] || walls_around[0, 1] && walls_around[1, 0])) naprav = napravlenie_for_wall.right_up;
            else if (!walls_around[1, 0] && !walls_around[0, 1] && (walls_around[0, 0] || walls_around[1, 2] && walls_around[2, 1])) naprav = napravlenie_for_wall.left_down;
            else if (!walls_around[0, 1] && !walls_around[1, 2] && (walls_around[0, 2] || walls_around[1, 0] && walls_around[2, 1])) naprav = napravlenie_for_wall.right_down;
            else if (!walls_around[1, 0] && !walls_around[2, 1] && (walls_around[2, 0] || walls_around[0, 1] && walls_around[1, 2])) naprav = napravlenie_for_wall.left_up;
            else naprav = null;
        }
        public void nearest_wall_destoyed()
        {
            opred_naprav(get_walls_for_changing_picture(xk, yk));
            mutex_for_show.WaitOne();
            image_for_show?.Dispose();
            image_for_show = new Bitmap(get_picture(), new Size(size_of_kletki, size_of_kletki));
            mutex_for_show.ReleaseMutex();
            show();
        }
        public override bool free_or_not_for_phisic_object { get => false; }
        public override bool free_or_not_for_ghost_object { get => false; }
        public override void Dispose()
        {
            this_wall_destroyed_or_create(xk, yk);
            base.Dispose();
        }
        protected generall_wall(int x, int y, int xk, int yk) : base(x, y,  Properties.Resources.SimpleKletka)
        {
            (this.xk, this.yk) = (xk, yk);
            nearest_wall_destoyed();
            this_wall_destroyed_or_create(xk, yk);
        }
        public override void activate()
        {
            base.activate();
            show();
        }
    }
    [Serializable]
    class point : generall_bonus
    {
        public static event Action eaten_point;
        public override void eaten()
        {
            base.eaten();
            this_eaten(10);
        }
        public point(int x, int y) : base(x, y, Properties.Resources.Point) { }
        public override void Dispose()
        {
            eaten_point();
            base.Dispose();
        }
        protected override Bitmap get_picture() => Properties.Resources.Point;
    }
    [Serializable]
    class superbonus : temporary_generall_bonus
    {
        public enum type_of_bonus { bullet, superpoint, deleting_ghost_wall }
        public static event Action<type_of_bonus> pribavlenie_bonusa;
        public override void eaten()
        {
            base.eaten();
            this_eaten(new Random().Next(50, 5000));
            pribavlenie_bonusa((type_of_bonus)(new Random().Next() % 3));
        }
        public superbonus(int x, int y, bool is_temporary) : base(x, y, is_temporary, Properties.Resources.SuperBonus) { }
        protected override Bitmap get_picture() => Properties.Resources.SuperBonus;
    }
    [Serializable]
    class wall : generall_wall
    {
        protected override Bitmap get_picture() => naprav switch { napravlenie_for_wall.horizontal => Properties.Resources.WallHorizontal, napravlenie_for_wall.vertical => Properties.Resources.WallVertical, napravlenie_for_wall.left_down => Properties.Resources.WallLeftDown, napravlenie_for_wall.left_up => Properties.Resources.WallLeftUp, napravlenie_for_wall.right_up => Properties.Resources.WallRightUp, napravlenie_for_wall.right_down => Properties.Resources.WallRightDown, null => Properties.Resources.SimpleKletka };
        public wall(int x, int y, int xk, int yk) : base(x, y, xk, yk) { }
    }
    [Serializable]
    class gates : generall_wall
    {
        protected override Bitmap get_picture() => Properties.Resources.Gates;
        public gates(int x, int y, int xk, int yk) : base(x, y, xk, yk) { }
    }
    [Serializable]
    class bonus : temporary_generall_bonus
    {
        public override void eaten()
        {
            base.eaten(); 
            this_eaten(new Random().Next(50, 5000));
        }
        protected override Bitmap get_picture() => new Random().Next(0, 3) switch { 0 => Properties.Resources.Apple, 1 => Properties.Resources.Peach, 2 => Properties.Resources.Cherry, 3 => Properties.Resources.Strawberry };
        public bonus(int x, int y, bool is_temporary) : base(x, y, is_temporary, new Random().Next(0, 3) switch { 0 => Properties.Resources.Apple, 1 => Properties.Resources.Peach, 2 => Properties.Resources.Cherry, 3 => Properties.Resources.Strawberry }) { }
    }
    [Serializable]
    class superpoint : generall_bonus
    {
        static Bitmap picture;
        static superpoint()
        {
            superbonus.pribavlenie_bonusa += (type) => { if (type == superbonus.type_of_bonus.superpoint) superpoint_eaten(); };
            picture = new(size_of_kletki, size_of_kletki);
            Brush br = new SolidBrush(Color.Black);
            Graphics.FromImage(picture).FillRectangle(br, 0, 0, size_of_kletki, size_of_kletki);
            br.Dispose();
            br = new SolidBrush(Color.Green);
            Graphics.FromImage(picture).FillEllipse(br, new Rectangle(0, 0, size_of_kletki, size_of_kletki));
            br.Dispose();
        }
        public static event Action superpoint_eaten;
        public override void eaten()
        {
            base.eaten();
            this_eaten(50);
            superpoint_eaten();
        }
        public superpoint(int x, int y) : base(x, y, picture) { }
        protected override Bitmap get_picture() => picture;
    }
    [Serializable]
    class barrier : generall_wall
    {
        [NonSerialized]
        System.Timers.Timer my_timer_for_dead;
        [field: NonSerialized]
        public event Action i_am_dead_by_time;
        protected override void opred_naprav(bool[,] walls_around) => naprav = (!walls_around[1, 0] && !walls_around[1, 2]) ? napravlenie_for_wall.horizontal : napravlenie_for_wall.vertical;
        protected override Bitmap get_picture() => naprav switch { napravlenie_for_wall.horizontal => Properties.Resources.BarrierHorizontal, napravlenie_for_wall.vertical => Properties.Resources.BarrierVertical, napravlenie_for_wall.left_down => Properties.Resources.BarrierLeftDown, napravlenie_for_wall.left_up => Properties.Resources.BarrierLeftUp, napravlenie_for_wall.right_up => Properties.Resources.BarrierRightUp, napravlenie_for_wall.right_down => Properties.Resources.BarrierRightDown, null => Properties.Resources.SimpleKletka };
        void call_event_dead(int int1, int int2, int int3) => i_am_dead_by_time();
        public barrier(int x, int y, int xk, int yk) : base(x, y, xk, yk)
        {
            my_timer_for_dead = new(10000);
            my_timer_for_dead.Elapsed += delegate { i_am_dead_by_time(); };
            game.stop_game += stop_timer;
            game.start_game += start_timer;
            game.restart_event += call_event_dead;
            my_timer_for_dead.Start();
        }
        public override void activate()
        {
            base.activate();
            my_timer_for_dead = new(10000);
            my_timer_for_dead.Elapsed += delegate { i_am_dead_by_time(); };
            game.stop_game += stop_timer;
            game.start_game += start_timer;
            game.restart_event += call_event_dead;
            if (!game.game_is_stopped_on_pause)
                my_timer_for_dead.Start();
        }
        void stop_timer() => my_timer_for_dead.Stop();
        void start_timer() => my_timer_for_dead.Start();
        public override void Dispose()
        {
            i_am_dead_by_time = null;
            my_timer_for_dead.Stop();
            my_timer_for_dead.Dispose();
            game.stop_game -= stop_timer;
            game.start_game -= start_timer;
            game.restart_event -= call_event_dead;
            base.Dispose();
        }
    }
    [Serializable]
    class ghosts_wall : generall_wall
    {
        protected override Bitmap get_picture() => naprav switch { napravlenie_for_wall.horizontal => Properties.Resources.GhostWallHorizontal, napravlenie_for_wall.vertical => Properties.Resources.GhostWallVertical, napravlenie_for_wall.left_down => Properties.Resources.GhostWallLeftDown, napravlenie_for_wall.left_up => Properties.Resources.GhostWallLeftUp, napravlenie_for_wall.right_up => Properties.Resources.GhostWallRightUp, napravlenie_for_wall.right_down => Properties.Resources.GhostWallRightDown, null => Properties.Resources.SimpleKletka };
        public override bool free_or_not_for_ghost_object { get => true; }
        public ghosts_wall(int x, int y, int xk, int yk) : base(x, y, xk, yk) { }
    }
}
