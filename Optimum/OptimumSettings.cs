using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimum
{
    /// <summary>
    /// Application Settings Window
    /// </summary>
    public partial class OptimumSettings : Form, ILocalizable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OptimumSettings()
        {
            InitializeComponent();

            Program.LocalizationChanged += LoadLocalizedText;
            LoadLocalizedText();
        }

        /// <summary>
        /// Closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.rules_russian = russian_checkers.Checked;
            Properties.Settings.Default.white_first = white_first.Checked;
            Properties.Settings.Default.board_imperian = board_var2_chk.Checked;
            Properties.Settings.Default.force_beating = force_beating.Checked;
            Properties.Settings.Default.give_away = give_away.Checked;
            Properties.Settings.Default.language = lang_russian.Checked ? 1 : 0;
            Properties.Settings.Default.Save();
            Program.LocalizationChanged -= LoadLocalizedText;
            Close();
        }

        /// <summary>
        /// Initialization of switches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptimumSettings_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.rules_russian)
            {
                russian_checkers.Checked = true;
                english_checkers.Checked = false;
            }
            else
            {
                russian_checkers.Checked = false;
                english_checkers.Checked = true;
            }

            if (Properties.Settings.Default.white_first)
            {
                white_first.Checked = true;
                red_first.Checked = false;
            }
            else
            {
                white_first.Checked = false;
                red_first.Checked = true;
            }

            if (Properties.Settings.Default.board_imperian)
            {
                board_var2_chk.Checked = true;
                board_var3_chk.Checked = false;
            }
            else
            {
                board_var2_chk.Checked = false;
                board_var3_chk.Checked = true;
            }

            if (Properties.Settings.Default.force_beating)
            {
                force_beating.Checked = true;
                soft_beating.Checked = false;
            }
            else
            {
                force_beating.Checked = false;
                soft_beating.Checked = true;
            }

            if (Properties.Settings.Default.language == 0)
            {
                lang_english.Checked = true;
                lang_russian.Checked = false;
            }
            else
            {
                lang_english.Checked = false;
                lang_russian.Checked = true;
            }

            give_away.Checked = Properties.Settings.Default.give_away;
        }

        /// <summary>
        /// Window dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Capture = false;
                Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref m);
            }
        }

        /// <summary>
        /// Click on image switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void board_var3_pix_Click(object sender, EventArgs e)
        {
            board_var3_chk.Checked = true;
            board_var2_chk.Checked = false;
        }

        /// <summary>
        /// Click on image switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void board_var2_pix_Click(object sender, EventArgs e)
        {
            board_var3_chk.Checked = false;
            board_var2_chk.Checked = true;
        }

        /// <summary>
        /// Click on image switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void white_first_pix_Click(object sender, EventArgs e)
        {
            white_first.Checked = true;
            red_first.Checked = false;
        }

        /// <summary>
        /// Click on image switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void red_first_pix_Click(object sender, EventArgs e)
        {
            white_first.Checked = false;
            red_first.Checked = true;
        }

        /// <summary>
        /// Switch to English
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lang_english_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.language = 0;
            Program.LocalizedText = new LocalizedText();
        }

        /// <summary>
        /// Load localization
        /// </summary>
        public void LoadLocalizedText()
        {
            toolStripMenuItem1.Text = Program.LocalizedText.settings.title;
            rules_group.Text = Program.LocalizedText.settings.rules;
            russian_checkers.Text = Program.LocalizedText.settings.russianDraughts;
            russian_rules_lbl.Text = Program.LocalizedText.settings.russianDraughtsRules;
            english_checkers.Text = Program.LocalizedText.settings.englishCheckers;
            english_rules_lbl.Text = Program.LocalizedText.settings.englishCheckersRules;
            give_away.Text = Program.LocalizedText.settings.giveaway;
            move_first.Text = Program.LocalizedText.settings.firstTurn;
            white_first.Text = Program.LocalizedText.settings.white;
            red_first.Text = Program.LocalizedText.settings.red;
            board_group.Text = Program.LocalizedText.settings.board;
            beating_rules.Text = Program.LocalizedText.settings.beatingRules;
            force_beating.Text = Program.LocalizedText.settings.beatingIsNecessary;
            soft_beating.Text = Program.LocalizedText.settings.beatAtWill;
            language_group.Text = Program.LocalizedText.settings.language;
        }

        /// <summary>
        /// Switch to russian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lang_russian_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.language = 1;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LocalizedText));
            using (MemoryStream ms = new MemoryStream(Properties.Resources.russian))
            {
                Program.LocalizedText = (LocalizedText)serializer.ReadObject(ms);
            }
        }
    }
}
