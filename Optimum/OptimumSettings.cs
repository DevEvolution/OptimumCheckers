using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimum
{
    /// <summary>
    /// Application Settings Window
    /// </summary>
    public partial class OptimumSettings : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OptimumSettings()
        {
            InitializeComponent();
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
            Properties.Settings.Default.Save();
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
    }
}
