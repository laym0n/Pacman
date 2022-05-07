
namespace PacmanWinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private pole game;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && (game != null))
            {
                game.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel14 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.panel18 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.panel21 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel25 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel27 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.button11 = new System.Windows.Forms.Button();
            this.panel22 = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel21.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.panel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "M A X  S C O R E";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "L V L";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "S C O R E";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(0, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(0, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(0, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "0";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(13, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 38);
            this.panel1.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.pictureBox4);
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(147, 32);
            this.panel8.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(33, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 21);
            this.label10.TabIndex = 1;
            this.label10.Text = "label10";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.Pacman2Right;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Desktop;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "EAZY",
            "MEDIUM",
            "HARD"});
            this.comboBox1.Location = new System.Drawing.Point(132, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 36);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(169, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(149, 38);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(348, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(52, 38);
            this.panel3.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(336, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(476, 47);
            this.panel4.TabIndex = 9;
            this.panel4.Visible = false;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.panel19);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Location = new System.Drawing.Point(337, 208);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(546, 496);
            this.panel5.TabIndex = 10;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.panel12);
            this.panel19.Controls.Add(this.panel18);
            this.panel19.Location = new System.Drawing.Point(0, 107);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(505, 382);
            this.panel19.TabIndex = 6;
            this.panel19.Visible = false;
            // 
            // panel12
            // 
            this.panel12.AutoSize = true;
            this.panel12.Controls.Add(this.panel17);
            this.panel12.Controls.Add(this.button7);
            this.panel12.Controls.Add(this.panel15);
            this.panel12.Controls.Add(this.panel16);
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Controls.Add(this.panel14);
            this.panel12.Location = new System.Drawing.Point(260, 9);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(433, 304);
            this.panel12.TabIndex = 18;
            this.panel12.Visible = false;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.comboBox1);
            this.panel17.Controls.Add(this.label16);
            this.panel17.Location = new System.Drawing.Point(14, 204);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(416, 39);
            this.panel17.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 32);
            this.label16.TabIndex = 0;
            this.label16.Text = "Difficult:";
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(82, 246);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(243, 55);
            this.button7.TabIndex = 1;
            this.button7.Text = "BACK";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.radioButton7);
            this.panel15.Controls.Add(this.label15);
            this.panel15.Location = new System.Drawing.Point(15, 155);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(415, 43);
            this.panel15.TabIndex = 19;
            // 
            // radioButton7
            // 
            this.radioButton7.Checked = true;
            this.radioButton7.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton7.ForeColor = System.Drawing.Color.White;
            this.radioButton7.Location = new System.Drawing.Point(131, 0);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(152, 32);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Escape";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(-1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 32);
            this.label15.TabIndex = 0;
            this.label15.Text = "Pause:";
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.radioButton5);
            this.panel16.Controls.Add(this.radioButton6);
            this.panel16.Controls.Add(this.label14);
            this.panel16.Location = new System.Drawing.Point(15, 103);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(415, 46);
            this.panel16.TabIndex = 19;
            // 
            // radioButton5
            // 
            this.radioButton5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton5.ForeColor = System.Drawing.Color.White;
            this.radioButton5.Location = new System.Drawing.Point(129, 0);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(152, 32);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Space";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton6.ForeColor = System.Drawing.Color.White;
            this.radioButton6.Location = new System.Drawing.Point(294, 0);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(129, 32);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.Text = "Shift";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 28);
            this.label14.TabIndex = 0;
            this.label14.Text = "Destroy wall:";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.radioButton1);
            this.panel13.Controls.Add(this.label12);
            this.panel13.Controls.Add(this.radioButton2);
            this.panel13.Location = new System.Drawing.Point(14, 8);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(416, 37);
            this.panel13.TabIndex = 2;
            // 
            // radioButton1
            // 
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(125, -8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(159, 32);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Arrow keys";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(1, -6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 32);
            this.label12.TabIndex = 0;
            this.label12.Text = "Moving:";
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(295, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(121, 32);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "WASD";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel14.Controls.Add(this.radioButton3);
            this.panel14.Controls.Add(this.label13);
            this.panel14.Controls.Add(this.radioButton4);
            this.panel14.Location = new System.Drawing.Point(15, 48);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(415, 49);
            this.panel14.TabIndex = 19;
            // 
            // radioButton3
            // 
            this.radioButton3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton3.ForeColor = System.Drawing.Color.White;
            this.radioButton3.Location = new System.Drawing.Point(131, 0);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(159, 32);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.Text = "Space";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(-1, -1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 32);
            this.label13.TabIndex = 0;
            this.label13.Text = "Bullet:";
            // 
            // radioButton4
            // 
            this.radioButton4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton4.ForeColor = System.Drawing.Color.White;
            this.radioButton4.Location = new System.Drawing.Point(295, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(129, 32);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Shift";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // panel18
            // 
            this.panel18.AutoSize = true;
            this.panel18.Controls.Add(this.button1);
            this.panel18.Controls.Add(this.button6);
            this.panel18.Controls.Add(this.button2);
            this.panel18.Controls.Add(this.button5);
            this.panel18.Location = new System.Drawing.Point(0, 6);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(254, 263);
            this.panel18.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(243, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "CLASSIC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(8, 137);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(243, 59);
            this.button6.TabIndex = 1;
            this.button6.Text = "SETTINGS";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(4, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(243, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "NEW GAME";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.RedButton;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(8, 202);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(243, 58);
            this.button5.TabIndex = 2;
            this.button5.Text = "EXIT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PacmanWinFormsApp.Properties.Resources.Screensaver;
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(531, 107);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::PacmanWinFormsApp.Properties.Resources.Loading;
            this.pictureBox6.Location = new System.Drawing.Point(186, 41);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(224, 83);
            this.pictureBox6.TabIndex = 16;
            this.pictureBox6.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.button4);
            this.panel6.Controls.Add(this.button3);
            this.panel6.Location = new System.Drawing.Point(14, 9);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(316, 276);
            this.panel6.TabIndex = 11;
            this.panel6.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(41)))));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(89, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "PAUSE";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.YellowLabel;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(23, 17);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(266, 84);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.RedButton;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(59, 187);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(190, 65);
            this.button4.TabIndex = 1;
            this.button4.Text = "BACK";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(59, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(190, 65);
            this.button3.TabIndex = 0;
            this.button3.Text = "PLAY";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.pictureBox3);
            this.panel7.Location = new System.Drawing.Point(39, 34);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(106, 43);
            this.panel7.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(26, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "label9";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.BulletUp;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 43);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.pictureBox5);
            this.panel9.Location = new System.Drawing.Point(151, 15);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(171, 35);
            this.panel9.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(33, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 21);
            this.label11.TabIndex = 1;
            this.label11.Text = "label11";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.WeaknessEnd2;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox5.Location = new System.Drawing.Point(0, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 3500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Controls.Add(this.panel6);
            this.panel10.Controls.Add(this.panel4);
            this.panel10.Controls.Add(this.panel20);
            this.panel10.Controls.Add(this.panel21);
            this.panel10.Controls.Add(this.panel22);
            this.panel10.Location = new System.Drawing.Point(80, 632);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(194, 67);
            this.panel10.TabIndex = 17;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.panel8);
            this.panel11.Controls.Add(this.panel9);
            this.panel11.Controls.Add(this.panel7);
            this.panel11.Location = new System.Drawing.Point(336, 62);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(335, 92);
            this.panel11.TabIndex = 18;
            // 
            // panel20
            // 
            this.panel20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel20.Controls.Add(this.label17);
            this.panel20.Controls.Add(this.pictureBox7);
            this.panel20.Controls.Add(this.button8);
            this.panel20.Controls.Add(this.button9);
            this.panel20.Location = new System.Drawing.Point(341, 160);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(330, 266);
            this.panel20.TabIndex = 11;
            this.panel20.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(41)))));
            this.label17.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(31, 41);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(285, 59);
            this.label17.TabIndex = 2;
            this.label17.Text = "LOAD GAME?";
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.YellowLabel;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(11, 13);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(305, 84);
            this.pictureBox7.TabIndex = 2;
            this.pictureBox7.TabStop = false;
            // 
            // button8
            // 
            this.button8.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.RedButton;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(61, 183);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(190, 65);
            this.button8.TabIndex = 1;
            this.button8.Text = "NO";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(61, 112);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(190, 65);
            this.button9.TabIndex = 0;
            this.button9.Text = "YES";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel21.Controls.Add(this.panel23);
            this.panel21.Controls.Add(this.panel24);
            this.panel21.Controls.Add(this.panel25);
            this.panel21.Controls.Add(this.panel26);
            this.panel21.Controls.Add(this.panel27);
            this.panel21.Controls.Add(this.label7);
            this.panel21.Controls.Add(this.pictureBox8);
            this.panel21.Controls.Add(this.button11);
            this.panel21.Location = new System.Drawing.Point(39, 291);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(265, 435);
            this.panel21.TabIndex = 18;
            this.panel21.Visible = false;
            // 
            // panel23
            // 
            this.panel23.AutoSize = true;
            this.panel23.Controls.Add(this.label28);
            this.panel23.Controls.Add(this.label29);
            this.panel23.Controls.Add(this.label18);
            this.panel23.Location = new System.Drawing.Point(3, 93);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(255, 46);
            this.panel23.TabIndex = 22;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(38, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(12, 15);
            this.label28.TabIndex = 21;
            this.label28.Text = "?";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(38, 25);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(12, 15);
            this.label29.TabIndex = 21;
            this.label29.Text = "?";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(10, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 15);
            this.label18.TabIndex = 21;
            this.label18.Text = "1)";
            // 
            // panel24
            // 
            this.panel24.AutoSize = true;
            this.panel24.Controls.Add(this.label31);
            this.panel24.Controls.Add(this.label30);
            this.panel24.Controls.Add(this.label19);
            this.panel24.Location = new System.Drawing.Point(3, 145);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(255, 46);
            this.panel24.TabIndex = 23;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(27, 7);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(12, 15);
            this.label31.TabIndex = 21;
            this.label31.Text = "?";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(27, 19);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(12, 15);
            this.label30.TabIndex = 21;
            this.label30.Text = "?";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(7, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 15);
            this.label19.TabIndex = 21;
            this.label19.Text = "2)";
            // 
            // panel25
            // 
            this.panel25.AutoSize = true;
            this.panel25.Controls.Add(this.label33);
            this.panel25.Controls.Add(this.label34);
            this.panel25.Controls.Add(this.label20);
            this.panel25.Location = new System.Drawing.Point(3, 199);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(255, 49);
            this.panel25.TabIndex = 24;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(38, 7);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(12, 15);
            this.label33.TabIndex = 21;
            this.label33.Text = "?";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label34.ForeColor = System.Drawing.Color.White;
            this.label34.Location = new System.Drawing.Point(38, 29);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(12, 15);
            this.label34.TabIndex = 21;
            this.label34.Text = "?";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(15, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 15);
            this.label20.TabIndex = 21;
            this.label20.Text = "3)";
            // 
            // panel26
            // 
            this.panel26.AutoSize = true;
            this.panel26.Controls.Add(this.label32);
            this.panel26.Controls.Add(this.label35);
            this.panel26.Controls.Add(this.label21);
            this.panel26.Location = new System.Drawing.Point(3, 251);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(255, 50);
            this.panel26.TabIndex = 19;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(35, 13);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(12, 15);
            this.label32.TabIndex = 21;
            this.label32.Text = "?";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(35, 30);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(12, 15);
            this.label35.TabIndex = 21;
            this.label35.Text = "?";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(12, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 15);
            this.label21.TabIndex = 21;
            this.label21.Text = "4)";
            // 
            // panel27
            // 
            this.panel27.AutoSize = true;
            this.panel27.Controls.Add(this.label36);
            this.panel27.Controls.Add(this.label37);
            this.panel27.Controls.Add(this.label22);
            this.panel27.Location = new System.Drawing.Point(3, 301);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(255, 49);
            this.panel27.TabIndex = 20;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(31, 11);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(12, 15);
            this.label36.TabIndex = 21;
            this.label36.Text = "?";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(31, 29);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(12, 15);
            this.label37.TabIndex = 21;
            this.label37.Text = "?";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(10, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 15);
            this.label22.TabIndex = 21;
            this.label22.Text = "5)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(41)))));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(16, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "HIGH SCORES";
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.YellowLabel;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(3, 3);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(255, 84);
            this.pictureBox8.TabIndex = 19;
            this.pictureBox8.TabStop = false;
            // 
            // button11
            // 
            this.button11.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.Location = new System.Drawing.Point(30, 351);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(186, 71);
            this.button11.TabIndex = 25;
            this.button11.Text = "MAIN MENU";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel22.Controls.Add(this.button10);
            this.panel22.Controls.Add(this.textBox1);
            this.panel22.Controls.Add(this.label58);
            this.panel22.Controls.Add(this.pictureBox9);
            this.panel22.Location = new System.Drawing.Point(684, 62);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(349, 228);
            this.panel22.TabIndex = 18;
            this.panel22.Visible = false;
            // 
            // button10
            // 
            this.button10.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.GreenButton;
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(107, 172);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(110, 45);
            this.button10.TabIndex = 22;
            this.button10.Text = "OK";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox1.Location = new System.Drawing.Point(16, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 23);
            this.textBox1.TabIndex = 21;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(226)))), ((int)(((byte)(41)))));
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(16, 35);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(113, 15);
            this.label58.TabIndex = 20;
            this.label58.Text = "ENTER YOUR NAME";
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackgroundImage = global::PacmanWinFormsApp.Properties.Resources.YellowLabel;
            this.pictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox9.Location = new System.Drawing.Point(3, 3);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(338, 84);
            this.pictureBox9.TabIndex = 19;
            this.pictureBox9.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(958, 634);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.panel5);
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(944, 672);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pacman";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Button button11;
    }
}

