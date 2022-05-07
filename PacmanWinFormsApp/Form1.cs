using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanWinFormsApp
{
    public partial class Form1 : Form
    {
        System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            (button11.FlatAppearance.BorderSize, button11.FlatStyle, button10.FlatAppearance.BorderSize, button10.FlatStyle, button4.FlatAppearance.BorderSize, button4.FlatStyle, button1.FlatAppearance.BorderSize, button1.FlatStyle, button2.FlatAppearance.BorderSize, button2.FlatStyle, button3.FlatAppearance.BorderSize, button3.FlatStyle, button5.FlatAppearance.BorderSize, button5.FlatStyle, button6.FlatAppearance.BorderSize, button6.FlatStyle, button7.FlatAppearance.BorderSize, button7.FlatStyle, button8.FlatAppearance.BorderSize, button8.FlatStyle, button9.FlatAppearance.BorderSize, button9.FlatStyle) = (0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat, 0, FlatStyle.Flat);
            f.AddFontFile("Teletactile.ttf");
            (button11.Font, button7.Font, button1.Font, button2.Font, button3.Font, button4.Font, button5.Font, label8.Font, button6.Font, label17.Font, button9.Font, button8.Font) = (new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel), new Font(f.Families[0], 32, GraphicsUnit.Pixel));
            (label12.Font, label13.Font, label14.Font, label15.Font, label16.Font) = (new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel));
            (label12.Size, label13.Size, label15.Size, label16.Size) = (label14.Size, label14.Size, label14.Size, label14.Size);
            (label12.Location, label13.Location, label14.Location, label15.Location, label16.Location) = (new Point(0, 10), new Point(0, 10), new Point(0, 10), new Point(0, 10), new Point(0, 10));
            radioButton1.Location = new Point(label14.Size.Width, 0);
            (radioButton3.Location, radioButton5.Location, radioButton4.Location, radioButton6.Location, radioButton2.Location, radioButton7.Location) = (radioButton1.Location, radioButton1.Location, new Point(radioButton1.Location.X + radioButton1.Size.Width, 0), new Point(radioButton1.Location.X + radioButton1.Size.Width, 0), new Point(radioButton1.Location.X + radioButton1.Size.Width, 0), radioButton1.Location);
            (radioButton1.Font, radioButton2.Font, radioButton3.Font, radioButton4.Font, radioButton5.Font, radioButton6.Font, comboBox1.Font) = (new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel), new Font(f.Families[0], 15, GraphicsUnit.Pixel));
            (label4.Size, label5.Size, label6.Size, this.Size) = (label3.Size, label2.Size, label1.Size, new Size(59 * 16, 42 * 16));
            (panel6.Location, panel5.Location, pictureBox6.Location) = (new Point(Width / 2 - panel6.Width / 2, Height / 2 - panel6.Height / 2), new Point(Width / 2 - panel5.Width / 2, Height / 2 - panel5.Height / 2), new Point(Width / 2 - pictureBox6.Width / 2, Height / 2 - pictureBox6.Height / 2));
            panel12.Location = new Point(panel19.Size.Width / 2 - panel12.Size.Width / 2, panel12.Location.Y);
            panel18.Location = new Point(panel19.Size.Width / 2 - panel18.Size.Width / 2, panel18.Location.Y/*panel19.Size.Height / 2 - panel18.Size.Height / 2*/);
            label18.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label19.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label20.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label21.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label22.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            //label23.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            //label24.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            //label25.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            //label26.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            //label27.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            pictureBox1.Size = Properties.Resources.Screensaver.Size;
            pictureBox6.Size = Properties.Resources.Loading.Size;
            label28.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label29.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label30.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label31.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label32.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label33.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label34.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label35.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label36.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label37.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            label58.Font = new Font(f.Families[0], 24, GraphicsUnit.Pixel);
            label7.Font = new Font(f.Families[0], 24, GraphicsUnit.Pixel);
            button10.Font = new Font(f.Families[0], 32, GraphicsUnit.Pixel);
            textBox1.Font = new Font(f.Families[0], 19, GraphicsUnit.Pixel);
            pictureBox8.Width = label7.Width + 40;
            panel21.Width = pictureBox8.Width + 20;
            panel23.Width = pictureBox8.Width;
            panel24.Width = pictureBox8.Width;
            panel25.Width = pictureBox8.Width;
            panel26.Width = pictureBox8.Width;
            panel27.Width = pictureBox8.Width;
            pictureBox8.Location = new Point(panel21.Width / 2 - pictureBox8.Width / 2, pictureBox8.Location.Y);
            panel23.Location = new Point(pictureBox8.Location.X, panel23.Location.Y);
            panel24.Location = new Point(pictureBox8.Location.X, panel24.Location.Y);
            panel25.Location = new Point(pictureBox8.Location.X, panel25.Location.Y);
            panel26.Location = new Point(pictureBox8.Location.X, panel26.Location.Y);
            panel27.Location = new Point(pictureBox8.Location.X, panel27.Location.Y);
            label7.Location = new Point(pictureBox8.Width / 2 - label7.Width / 2 + 10, label7.Location.Y);
            label18.Location = new Point(0, 10);
            label19.Location = label18.Location;
            label20.Location = label18.Location;
            label21.Location = label18.Location;
            label22.Location = label18.Location;
            label28.Location = new Point(label18.Location.X + label18.Width + 10, 5);
            label29.Location = new Point(label18.Location.X + label18.Width + 10, label28.Location.Y + label28.Height + 5);
            label30.Location = new Point(label18.Location.X + label18.Width + 10, 5);
            label31.Location = new Point(label18.Location.X + label18.Width + 10, label28.Location.Y + label28.Height + 5);
            label32.Location = new Point(label18.Location.X + label18.Width + 10, 5);
            label33.Location = new Point(label18.Location.X + label18.Width + 10, label28.Location.Y + label28.Height + 5);
            label34.Location = new Point(label18.Location.X + label18.Width + 10, 5);
            label35.Location = new Point(label18.Location.X + label18.Width + 10, label28.Location.Y + label28.Height + 5);
            label36.Location = new Point(label18.Location.X + label18.Width + 10, 5);
            label37.Location = new Point(label18.Location.X + label18.Width + 10, label28.Location.Y + label28.Height + 5);
            Form1_Resize(null, null);
            pictureBox1.Location = new Point(panel5.Width / 2 - pictureBox1.Width / 2, pictureBox1.Location.Y);
            pictureBox6.Location = new Point(Width / 2 - pictureBox6.Width / 2, Height / 2 - pictureBox6.Height / 2);
            button3.Location = new Point(panel6.Width / 2 - button3.Width / 2, button3.Location.Y);
            button4.Location = new Point(panel6.Width / 2 - button4.Width / 2, button4.Location.Y);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //(panel5.Visible, panel10.Visible, panel10.Location) = (false, true, new Point(216,28));
            (panel5.Visible, panel10.Visible/*, panel10.Location*/) = (false, true/*, new Point(216,28)*/);
            panel10.Location = new Point(Size.Width / 2 - 512 / 2, Size.Height / 2 - 576 / 2 - 25);
            panel10.Focus();
            game.create_classic_game();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //(panel5.Visible, panel10.Visible, panel10.Location) = (false, true, new Point(32, 10));
            (panel5.Visible, panel10.Visible/*, panel10.Location*/) = (false, true/*, new Point(32, 10)*/);
            panel10.Location = new Point(Size.Width / 2 - 880 / 2, Size.Height / 2 - 608 / 2 - 25);
            panel10.Focus();
            game.create_new_game();
        }
        private void button5_Click(object sender, EventArgs e) => this.Close();
        private void Form1_Load(object sender, EventArgs e)
        {
            game = new(panel10, button3, button4);
            panel10.Visible = false;
            game.game_ended += () => { panel5.BeginInvoke(new Action(() => panel5.Visible = true)); panel10.BeginInvoke(new Action(() => panel10.Visible = false)); };
            if (Settings.control_of_pacman == Settings.Control_of_pacman.WASD) radioButton2.Checked = true; else radioButton1.Checked = true;
            if (Settings.control_of_bullets == Settings.Control_of_bullets.Shift) radioButton4.Checked = true; else radioButton6.Checked = true;
            if (Settings.difficult == pole.difficult.eazy) comboBox1.SelectedIndex = 0; else if (Settings.difficult == pole.difficult.medium) comboBox1.SelectedIndex = 1; else comboBox1.SelectedIndex = 2;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            (panel18.Visible, pictureBox6.Visible, panel19.Visible) = (true, false, true);
        }
        bool first_perekl = true;
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (first_perekl)
            {
                first_perekl = false;
                radioButton6.Checked = !radioButton4.Checked;
                radioButton5.Checked = radioButton4.Checked;
                if (radioButton4.Checked) pole.change_codes_for_buttons(new Keys[] { Keys.ShiftKey, Keys.Space }); else pole.change_codes_for_buttons(new Keys[] { Keys.Space, Keys.ShiftKey });
                Settings.control_of_bullets = Settings.Control_of_bullets.Shift;
                first_perekl = true;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (first_perekl)
            {
                first_perekl = false;
                radioButton4.Checked = !radioButton6.Checked;
                radioButton3.Checked = radioButton6.Checked;
                if (radioButton4.Checked) pole.change_codes_for_buttons(new Keys[] { Keys.ShiftKey, Keys.Space }); else pole.change_codes_for_buttons(new Keys[] { Keys.Space, Keys.ShiftKey });
                Settings.control_of_bullets = Settings.Control_of_bullets.Space;
                first_perekl = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Settings.control_of_pacman = Settings.Control_of_pacman.WASD;
                pacman.change_codes(new Keys[] { Keys.A, Keys.W, Keys.D, Keys.S });
            }
            else
            {
                Settings.control_of_pacman = Settings.Control_of_pacman.ArrowKey;
                pacman.change_codes(new Keys[] { Keys.Left, Keys.Up, Keys.Right, Keys.Down });
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: { pole.difficult_of_the_game = pole.difficult.eazy; Settings.difficult = pole.difficult.eazy; } break;
                case 1: { pole.difficult_of_the_game = pole.difficult.medium; Settings.difficult = pole.difficult.medium; } break;
                case 2: { pole.difficult_of_the_game = pole.difficult.hard; Settings.difficult = pole.difficult.hard; } break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (panel12.Visible, panel18.Visible) = (true, false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            (panel12.Visible, panel18.Visible) = (false, true);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel10.Location = new Point(Width / 2 - panel10.Width / 2, Height / 2 - panel10.Height / 2 - 25);
            pictureBox6.Location = new Point(Width / 2 - pictureBox6.Width / 2, Height / 2 - pictureBox6.Height / 2);
            panel5.Location = new Point(Width / 2 - panel5.Width / 2, Height / 2 - panel5.Height / 2);
        }
    }
}
