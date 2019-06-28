namespace Optimum
{
    partial class OptimumSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptimumSettings));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rules_group = new System.Windows.Forms.GroupBox();
            this.give_away = new System.Windows.Forms.CheckBox();
            this.english_rules_lbl = new System.Windows.Forms.Label();
            this.russian_rules_lbl = new System.Windows.Forms.Label();
            this.english_checkers = new System.Windows.Forms.RadioButton();
            this.russian_checkers = new System.Windows.Forms.RadioButton();
            this.move_first = new System.Windows.Forms.GroupBox();
            this.red_first_pix = new System.Windows.Forms.PictureBox();
            this.white_first_pix = new System.Windows.Forms.PictureBox();
            this.red_first = new System.Windows.Forms.RadioButton();
            this.white_first = new System.Windows.Forms.RadioButton();
            this.board_group = new System.Windows.Forms.GroupBox();
            this.board_var2_chk = new System.Windows.Forms.RadioButton();
            this.board_var3_chk = new System.Windows.Forms.RadioButton();
            this.board_var2_pix = new System.Windows.Forms.PictureBox();
            this.board_var3_pix = new System.Windows.Forms.PictureBox();
            this.beating_rules = new System.Windows.Forms.GroupBox();
            this.soft_beating = new System.Windows.Forms.RadioButton();
            this.force_beating = new System.Windows.Forms.RadioButton();
            this.language_group = new System.Windows.Forms.GroupBox();
            this.lang_russian = new System.Windows.Forms.RadioButton();
            this.lang_english = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.rules_group.SuspendLayout();
            this.move_first.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.red_first_pix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.white_first_pix)).BeginInit();
            this.board_group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board_var2_pix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board_var3_pix)).BeginInit();
            this.beating_rules.SuspendLayout();
            this.language_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.close,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(796, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            // 
            // close
            // 
            this.close.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.close.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.close.ForeColor = System.Drawing.Color.Red;
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(34, 22);
            this.close.Text = "";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuItem1.Image = global::Optimum.Properties.Resources.bird;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(236, 22);
            this.toolStripMenuItem1.Text = "Настройки приложения";
            // 
            // rules_group
            // 
            this.rules_group.Controls.Add(this.give_away);
            this.rules_group.Controls.Add(this.english_rules_lbl);
            this.rules_group.Controls.Add(this.russian_rules_lbl);
            this.rules_group.Controls.Add(this.english_checkers);
            this.rules_group.Controls.Add(this.russian_checkers);
            this.rules_group.Location = new System.Drawing.Point(34, 45);
            this.rules_group.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rules_group.Name = "rules_group";
            this.rules_group.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rules_group.Size = new System.Drawing.Size(315, 242);
            this.rules_group.TabIndex = 1;
            this.rules_group.TabStop = false;
            this.rules_group.Text = "Правила игры";
            // 
            // give_away
            // 
            this.give_away.AutoSize = true;
            this.give_away.Location = new System.Drawing.Point(22, 194);
            this.give_away.Name = "give_away";
            this.give_away.Size = new System.Drawing.Size(107, 22);
            this.give_away.TabIndex = 6;
            this.give_away.Text = "Поддавки";
            this.give_away.UseVisualStyleBackColor = true;
            // 
            // english_rules_lbl
            // 
            this.english_rules_lbl.AutoSize = true;
            this.english_rules_lbl.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.english_rules_lbl.Location = new System.Drawing.Point(19, 126);
            this.english_rules_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.english_rules_lbl.Name = "english_rules_lbl";
            this.english_rules_lbl.Size = new System.Drawing.Size(256, 64);
            this.english_rules_lbl.TabIndex = 5;
            this.english_rules_lbl.Text = "  - простые шашки бьют только\r\n    вперед\r\n  - дамка ходит в любую сторону\r\n    н" +
    "а дистанцию 1 клетки";
            // 
            // russian_rules_lbl
            // 
            this.russian_rules_lbl.AutoSize = true;
            this.russian_rules_lbl.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.russian_rules_lbl.Location = new System.Drawing.Point(19, 50);
            this.russian_rules_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.russian_rules_lbl.Name = "russian_rules_lbl";
            this.russian_rules_lbl.Size = new System.Drawing.Size(280, 48);
            this.russian_rules_lbl.TabIndex = 4;
            this.russian_rules_lbl.Text = "  - простые шашки могут бить назад\r\n  - дамка может ходить на любую \r\n    дистанц" +
    "ию ";
            // 
            // english_checkers
            // 
            this.english_checkers.AutoSize = true;
            this.english_checkers.Location = new System.Drawing.Point(22, 101);
            this.english_checkers.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.english_checkers.Name = "english_checkers";
            this.english_checkers.Size = new System.Drawing.Size(186, 22);
            this.english_checkers.TabIndex = 3;
            this.english_checkers.TabStop = true;
            this.english_checkers.Text = "Английские шашки";
            this.english_checkers.UseVisualStyleBackColor = true;
            // 
            // russian_checkers
            // 
            this.russian_checkers.AutoSize = true;
            this.russian_checkers.Location = new System.Drawing.Point(22, 25);
            this.russian_checkers.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.russian_checkers.Name = "russian_checkers";
            this.russian_checkers.Size = new System.Drawing.Size(156, 22);
            this.russian_checkers.TabIndex = 2;
            this.russian_checkers.TabStop = true;
            this.russian_checkers.Text = "Русские шашки";
            this.russian_checkers.UseVisualStyleBackColor = true;
            // 
            // move_first
            // 
            this.move_first.Controls.Add(this.red_first_pix);
            this.move_first.Controls.Add(this.white_first_pix);
            this.move_first.Controls.Add(this.red_first);
            this.move_first.Controls.Add(this.white_first);
            this.move_first.Location = new System.Drawing.Point(34, 297);
            this.move_first.Name = "move_first";
            this.move_first.Size = new System.Drawing.Size(314, 142);
            this.move_first.TabIndex = 2;
            this.move_first.TabStop = false;
            this.move_first.Text = "Первыми ходят";
            // 
            // red_first_pix
            // 
            this.red_first_pix.Image = global::Optimum.Properties.Resources.redking;
            this.red_first_pix.Location = new System.Drawing.Point(191, 25);
            this.red_first_pix.Name = "red_first_pix";
            this.red_first_pix.Size = new System.Drawing.Size(66, 63);
            this.red_first_pix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.red_first_pix.TabIndex = 9;
            this.red_first_pix.TabStop = false;
            this.red_first_pix.Click += new System.EventHandler(this.red_first_pix_Click);
            // 
            // white_first_pix
            // 
            this.white_first_pix.Image = global::Optimum.Properties.Resources.whiteking;
            this.white_first_pix.Location = new System.Drawing.Point(42, 25);
            this.white_first_pix.Name = "white_first_pix";
            this.white_first_pix.Size = new System.Drawing.Size(66, 63);
            this.white_first_pix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.white_first_pix.TabIndex = 8;
            this.white_first_pix.TabStop = false;
            this.white_first_pix.Click += new System.EventHandler(this.white_first_pix_Click);
            // 
            // red_first
            // 
            this.red_first.AutoSize = true;
            this.red_first.Location = new System.Drawing.Point(179, 94);
            this.red_first.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.red_first.Name = "red_first";
            this.red_first.Size = new System.Drawing.Size(96, 22);
            this.red_first.TabIndex = 7;
            this.red_first.TabStop = true;
            this.red_first.Text = "Красные";
            this.red_first.UseVisualStyleBackColor = true;
            // 
            // white_first
            // 
            this.white_first.AutoSize = true;
            this.white_first.Location = new System.Drawing.Point(32, 94);
            this.white_first.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.white_first.Name = "white_first";
            this.white_first.Size = new System.Drawing.Size(76, 22);
            this.white_first.TabIndex = 6;
            this.white_first.TabStop = true;
            this.white_first.Text = "Белые";
            this.white_first.UseVisualStyleBackColor = true;
            // 
            // board_group
            // 
            this.board_group.Controls.Add(this.board_var2_chk);
            this.board_group.Controls.Add(this.board_var3_chk);
            this.board_group.Controls.Add(this.board_var2_pix);
            this.board_group.Controls.Add(this.board_var3_pix);
            this.board_group.Location = new System.Drawing.Point(354, 45);
            this.board_group.Name = "board_group";
            this.board_group.Size = new System.Drawing.Size(401, 242);
            this.board_group.TabIndex = 3;
            this.board_group.TabStop = false;
            this.board_group.Text = "Доска";
            // 
            // board_var2_chk
            // 
            this.board_var2_chk.AutoSize = true;
            this.board_var2_chk.Location = new System.Drawing.Point(291, 217);
            this.board_var2_chk.Name = "board_var2_chk";
            this.board_var2_chk.Size = new System.Drawing.Size(14, 13);
            this.board_var2_chk.TabIndex = 1;
            this.board_var2_chk.TabStop = true;
            this.board_var2_chk.UseVisualStyleBackColor = true;
            // 
            // board_var3_chk
            // 
            this.board_var3_chk.AutoSize = true;
            this.board_var3_chk.Location = new System.Drawing.Point(101, 217);
            this.board_var3_chk.Name = "board_var3_chk";
            this.board_var3_chk.Size = new System.Drawing.Size(14, 13);
            this.board_var3_chk.TabIndex = 1;
            this.board_var3_chk.TabStop = true;
            this.board_var3_chk.UseVisualStyleBackColor = true;
            // 
            // board_var2_pix
            // 
            this.board_var2_pix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.board_var2_pix.Image = global::Optimum.Properties.Resources.board2;
            this.board_var2_pix.Location = new System.Drawing.Point(206, 25);
            this.board_var2_pix.Name = "board_var2_pix";
            this.board_var2_pix.Size = new System.Drawing.Size(184, 186);
            this.board_var2_pix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.board_var2_pix.TabIndex = 0;
            this.board_var2_pix.TabStop = false;
            this.board_var2_pix.Click += new System.EventHandler(this.board_var2_pix_Click);
            // 
            // board_var3_pix
            // 
            this.board_var3_pix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.board_var3_pix.Image = global::Optimum.Properties.Resources.board3;
            this.board_var3_pix.Location = new System.Drawing.Point(16, 25);
            this.board_var3_pix.Name = "board_var3_pix";
            this.board_var3_pix.Size = new System.Drawing.Size(184, 186);
            this.board_var3_pix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.board_var3_pix.TabIndex = 0;
            this.board_var3_pix.TabStop = false;
            this.board_var3_pix.Click += new System.EventHandler(this.board_var3_pix_Click);
            // 
            // beating_rules
            // 
            this.beating_rules.Controls.Add(this.soft_beating);
            this.beating_rules.Controls.Add(this.force_beating);
            this.beating_rules.Location = new System.Drawing.Point(354, 297);
            this.beating_rules.Name = "beating_rules";
            this.beating_rules.Size = new System.Drawing.Size(401, 94);
            this.beating_rules.TabIndex = 4;
            this.beating_rules.TabStop = false;
            this.beating_rules.Text = "Правила взятия шашек";
            // 
            // soft_beating
            // 
            this.soft_beating.AutoSize = true;
            this.soft_beating.Location = new System.Drawing.Point(101, 53);
            this.soft_beating.Name = "soft_beating";
            this.soft_beating.Size = new System.Drawing.Size(176, 22);
            this.soft_beating.TabIndex = 1;
            this.soft_beating.TabStop = true;
            this.soft_beating.Text = "Бить по желанию";
            this.soft_beating.UseVisualStyleBackColor = true;
            // 
            // force_beating
            // 
            this.force_beating.AutoSize = true;
            this.force_beating.Location = new System.Drawing.Point(101, 25);
            this.force_beating.Name = "force_beating";
            this.force_beating.Size = new System.Drawing.Size(186, 22);
            this.force_beating.TabIndex = 0;
            this.force_beating.TabStop = true;
            this.force_beating.Text = "Бить обязательно";
            this.force_beating.UseVisualStyleBackColor = true;
            // 
            // language_group
            // 
            this.language_group.Controls.Add(this.lang_russian);
            this.language_group.Controls.Add(this.lang_english);
            this.language_group.Location = new System.Drawing.Point(354, 391);
            this.language_group.Name = "language_group";
            this.language_group.Size = new System.Drawing.Size(401, 48);
            this.language_group.TabIndex = 5;
            this.language_group.TabStop = false;
            this.language_group.Text = "Язык";
            // 
            // lang_russian
            // 
            this.lang_russian.AutoSize = true;
            this.lang_russian.Image = global::Optimum.Properties.Resources.ru_ru;
            this.lang_russian.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lang_russian.Location = new System.Drawing.Point(250, 18);
            this.lang_russian.Name = "lang_russian";
            this.lang_russian.Size = new System.Drawing.Size(120, 24);
            this.lang_russian.TabIndex = 1;
            this.lang_russian.TabStop = true;
            this.lang_russian.Text = "Русский";
            this.lang_russian.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.lang_russian.UseVisualStyleBackColor = true;
            this.lang_russian.CheckedChanged += new System.EventHandler(this.Lang_russian_CheckedChanged);
            // 
            // lang_english
            // 
            this.lang_english.AutoSize = true;
            this.lang_english.Image = global::Optimum.Properties.Resources.en_uk;
            this.lang_english.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lang_english.Location = new System.Drawing.Point(16, 18);
            this.lang_english.Name = "lang_english";
            this.lang_english.Size = new System.Drawing.Size(120, 24);
            this.lang_english.TabIndex = 0;
            this.lang_english.TabStop = true;
            this.lang_english.Text = "English";
            this.lang_english.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.lang_english.UseVisualStyleBackColor = true;
            this.lang_english.CheckedChanged += new System.EventHandler(this.Lang_english_CheckedChanged);
            // 
            // OptimumSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(796, 447);
            this.Controls.Add(this.language_group);
            this.Controls.Add(this.beating_rules);
            this.Controls.Add(this.board_group);
            this.Controls.Add(this.move_first);
            this.Controls.Add(this.rules_group);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "OptimumSettings";
            this.Text = "Настройки приложения";
            this.Load += new System.EventHandler(this.OptimumSettings_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.rules_group.ResumeLayout(false);
            this.rules_group.PerformLayout();
            this.move_first.ResumeLayout(false);
            this.move_first.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.red_first_pix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.white_first_pix)).EndInit();
            this.board_group.ResumeLayout(false);
            this.board_group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board_var2_pix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board_var3_pix)).EndInit();
            this.beating_rules.ResumeLayout(false);
            this.beating_rules.PerformLayout();
            this.language_group.ResumeLayout(false);
            this.language_group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem close;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.GroupBox rules_group;
        private System.Windows.Forms.Label english_rules_lbl;
        private System.Windows.Forms.Label russian_rules_lbl;
        private System.Windows.Forms.RadioButton english_checkers;
        private System.Windows.Forms.RadioButton russian_checkers;
        private System.Windows.Forms.GroupBox move_first;
        private System.Windows.Forms.RadioButton red_first;
        private System.Windows.Forms.RadioButton white_first;
        private System.Windows.Forms.GroupBox board_group;
        private System.Windows.Forms.RadioButton board_var2_chk;
        private System.Windows.Forms.PictureBox board_var2_pix;
        private System.Windows.Forms.RadioButton board_var3_chk;
        private System.Windows.Forms.PictureBox board_var3_pix;
        private System.Windows.Forms.PictureBox red_first_pix;
        private System.Windows.Forms.PictureBox white_first_pix;
        private System.Windows.Forms.GroupBox beating_rules;
        private System.Windows.Forms.RadioButton soft_beating;
        private System.Windows.Forms.RadioButton force_beating;
        private System.Windows.Forms.CheckBox give_away;
        private System.Windows.Forms.GroupBox language_group;
        private System.Windows.Forms.RadioButton lang_english;
        private System.Windows.Forms.RadioButton lang_russian;
    }
}