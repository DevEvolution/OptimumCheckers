namespace Optimum
{
    partial class Optimum
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Optimum));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьИгруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьИгруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.close = new System.Windows.Forms.ToolStripMenuItem();
            this.maximize = new System.Windows.Forms.ToolStripMenuItem();
            this.screen = new System.Windows.Forms.PictureBox();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menu.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenu,
            this.close,
            this.maximize});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1153, 72);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            this.menu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menu_MouseDown);
            // 
            // mainMenu
            // 
            this.mainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграMenuItem,
            this.сохранитьИгруToolStripMenuItem,
            this.загрузитьИгруToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.mainMenu.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu.Image = global::Optimum.Properties.Resources.bird;
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(76, 68);
            // 
            // новаяИграMenuItem
            // 
            this.новаяИграMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.новаяИграMenuItem.ForeColor = System.Drawing.Color.LavenderBlush;
            this.новаяИграMenuItem.Name = "новаяИграMenuItem";
            this.новаяИграMenuItem.Size = new System.Drawing.Size(234, 26);
            this.новаяИграMenuItem.Text = "Новая игра";
            this.новаяИграMenuItem.Click += new System.EventHandler(this.новаяИграMenuItem_Click);
            // 
            // сохранитьИгруToolStripMenuItem
            // 
            this.сохранитьИгруToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.сохранитьИгруToolStripMenuItem.ForeColor = System.Drawing.Color.LavenderBlush;
            this.сохранитьИгруToolStripMenuItem.Name = "сохранитьИгруToolStripMenuItem";
            this.сохранитьИгруToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.сохранитьИгруToolStripMenuItem.Text = "Сохранить игру";
            this.сохранитьИгруToolStripMenuItem.Click += new System.EventHandler(this.сохранитьИгруToolStripMenuItem_Click);
            // 
            // загрузитьИгруToolStripMenuItem
            // 
            this.загрузитьИгруToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.загрузитьИгруToolStripMenuItem.ForeColor = System.Drawing.Color.LavenderBlush;
            this.загрузитьИгруToolStripMenuItem.Name = "загрузитьИгруToolStripMenuItem";
            this.загрузитьИгруToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.загрузитьИгруToolStripMenuItem.Text = "Загрузить игру";
            this.загрузитьИгруToolStripMenuItem.Click += new System.EventHandler(this.загрузитьИгруToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.настройкиToolStripMenuItem.ForeColor = System.Drawing.Color.LavenderBlush;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.выходToolStripMenuItem.ForeColor = System.Drawing.Color.LavenderBlush;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // close
            // 
            this.close.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.close.Font = new System.Drawing.Font("Wingdings 2", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.close.ForeColor = System.Drawing.Color.Red;
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(51, 68);
            this.close.Text = "";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // maximize
            // 
            this.maximize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.maximize.Font = new System.Drawing.Font("Wingdings 2", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.maximize.Name = "maximize";
            this.maximize.Size = new System.Drawing.Size(51, 68);
            this.maximize.Text = "";
            this.maximize.Click += new System.EventHandler(this.maximize_Click);
            // 
            // screen
            // 
            this.screen.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.screen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screen.Image = global::Optimum.Properties.Resources.board3;
            this.screen.Location = new System.Drawing.Point(0, 72);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(1153, 669);
            this.screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.screen.TabIndex = 1;
            this.screen.TabStop = false;
            this.screen.Paint += new System.Windows.Forms.PaintEventHandler(this.screen_Paint);
            this.screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screen_MouseDown);
            this.screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screen_MouseUp);
            // 
            // Optimum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 741);
            this.Controls.Add(this.screen);
            this.Controls.Add(this.menu);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Optimum";
            this.Text = "Optimum checkers";
            this.Load += new System.EventHandler(this.Optimum_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mainMenu;
        private System.Windows.Forms.ToolStripMenuItem close;
        private System.Windows.Forms.ToolStripMenuItem maximize;
        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.ToolStripMenuItem новаяИграMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьИгруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьИгруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
    }
}

