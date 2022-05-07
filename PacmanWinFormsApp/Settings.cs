using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacmanWinFormsApp
{
    static class Settings
    {
        static BinaryFormatter formatter = new();
        public enum Control_of_bullets { Shift, Space };
        public enum Control_of_pacman { WASD, ArrowKey };
        static public Control_of_bullets control_of_bullets 
        {
            get => Control_Of_Bullets;
            set
            {
                Control_of_bullets old = Control_Of_Bullets;
                Control_Of_Bullets = value;
                if (Control_Of_Bullets != old)
                    save_settings();
            }
        }
        static Control_of_bullets Control_Of_Bullets;
        static public pole.difficult difficult
        {
            get => Difficult;
            set
            {
                pole.difficult old = Difficult;
                Difficult = value;
                if (Difficult != old)
                    save_settings();
            }
        }
        static pole.difficult Difficult;
        static public Control_of_pacman control_of_pacman
        {
            get => Control_Of_Pacman;
            set
            {
                Control_of_pacman old = Control_Of_Pacman;
                Control_Of_Pacman = value;
                if (Control_Of_Pacman != old)
                    save_settings();
            }
        }
        static Control_of_pacman Control_Of_Pacman;
        static Settings() 
        {
            using (System.IO.FileStream fs = new("settings.dat", System.IO.FileMode.Open))
            {
                Object[] settings = (Object[])formatter.Deserialize(fs);
                Control_Of_Pacman = (Control_of_pacman)settings[0];
                Difficult = (pole.difficult)settings[1];
                Control_Of_Bullets = (Control_of_bullets)settings[2];
            }
        }
        static void save_settings()
        {
            using (System.IO.FileStream fs = new("settings.dat", System.IO.FileMode.Create))
            {
                formatter.Serialize(fs, new Object[] { Control_Of_Pacman, Difficult, Control_Of_Bullets });
            }

        }
    }
}
