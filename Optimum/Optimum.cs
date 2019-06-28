using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Optimum
{
    public partial class Optimum : Form
    {
        // Game state
        public State _state;

        // Mouse click handler
        MouseClickHandler _mouseHandler;


        /// <summary>
        /// Constructor
        /// </summary>
        public Optimum()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Window dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Capture = false;
                Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref m);
            }
        }

        /// <summary>
        /// Closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Full screen mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="file"></param>
        private void State_Serialize(string file)
        {
            BinaryFormatter format = new BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                format.Serialize(fs, _state);
            }
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="file"></param>
        private void State_Deserialize(string file)
        {
            BinaryFormatter format = new BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                _state = (State)format.Deserialize(fs);
            }
        }

        /// <summary>
        /// Setting the starting position
        /// </summary>
        /// <param name="serialized_position"></param>
        private void State_SetStartPosition(byte[] serialized_position)
        {
            BinaryFormatter format = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(serialized_position))
            {
                _state = (State)format.Deserialize(ms);
            }
        }

        /// <summary>
        /// Temporary state saving
        /// </summary>
        /// <returns></returns>
        private byte[] State_SavePosition()
        {
            byte[] bytes;
            BinaryFormatter format = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                format.Serialize(ms, _state);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        /// <summary>
        /// Saving game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Бинарный файл|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                State_Serialize(sfd.FileName);
            }
        }

        /// <summary>
        /// Load the game at the beginning of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Optimum_Load(object sender, EventArgs e)
        {
            ApplySettings();
            State_SetStartPosition(Properties.Resources.start_position);
            _mouseHandler = new MouseClickHandler(screen, ref _state);
            _mouseHandler.ShowTurnOwnerMessage();
        }

        /// <summary>
        /// Scene redrawing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screen_Paint(object sender, PaintEventArgs e)
        {
            // Buffer creation, initialization
            Ruler sizes = new Ruler(screen);
            Rectangle board_coordinates = sizes.GetBoardCoordinates();
            Image gfx = new Bitmap(board_coordinates.Width, board_coordinates.Height);
            Graphics g = Graphics.FromImage(gfx);

            // Draw all the checkers
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_state.checkers[j, i] != null)
                    {
                        // Drawing static checkers
                        if (_state.checkers[j, i] != _mouseHandler._drag)
                            g.DrawImage(_state.checkers[j, i].img, _state.checkers[j, i].rect);
                    }
                }
            }

            // Drawing moving checker
            if (_mouseHandler._drag != null)
            {
                g.DrawImage(_mouseHandler._drag.img, _mouseHandler._drag.rect);
            }

            // Quickly draw a scene
            g.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
            e.Graphics.DrawImage(gfx, board_coordinates);
            e.Graphics.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
        }

        /// <summary>
        /// Loading game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void загрузитьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Бинарный файл|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    State_Deserialize(ofd.FileName);
                    _mouseHandler = new MouseClickHandler(screen, ref _state);
                    screen.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Файл поврежден или имеет неправильный формат:" + ex.Message);
                }
            }
            _mouseHandler.ShowTurnOwnerMessage();
        }

        /// <summary>
        /// Releasing the mouse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screen_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseHandler.MouseUp(e);
            screen.MouseMove -= screen_MouseMove;
            screen.Invalidate();
        }

        /// <summary>
        /// Mouse click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screen_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseHandler.MouseDown(e);
            screen.MouseMove += screen_MouseMove;
            screen.Invalidate();
        }

        /// <summary>
        /// Mouse movement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (!screen.ClientRectangle.Contains(e.Location))
            {
                screen.MouseMove -= screen_MouseMove;
                return;
            }

            _mouseHandler.MouseMove(e);
            screen.Invalidate();
            GC.Collect();
        }

        /// <summary>
        /// Opening application settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptimumSettings settings = new OptimumSettings();
            settings.ShowDialog();
            ApplySettings();
        }

        /// <summary>
        /// Applying application settings
        /// </summary>
        private void ApplySettings()
        {
            if (Properties.Settings.Default.board_imperian)
                screen.Image = Properties.Resources.board2;
            else
                screen.Image = Properties.Resources.board3;
            
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void новаяИграMenuItem_Click(object sender, EventArgs e)
        {
            ApplySettings();
            State_SetStartPosition(Properties.Resources.start_position);
            _mouseHandler = new MouseClickHandler(screen, ref _state);
            _mouseHandler.ShowTurnOwnerMessage();
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
