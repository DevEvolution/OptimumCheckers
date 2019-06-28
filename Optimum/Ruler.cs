using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimum
{
    /// <summary>
    /// Ruler, sizing and screen coordinates of all components
    /// </summary>
    class Ruler
    {
        // World window
        PictureBox _screen;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="screen">World window</param>
        public Ruler(PictureBox screen)
        {
            _screen = screen;
        }

        /// <summary>
        /// Finding checkers at the specified world coordinates
        /// </summary>
        /// <param name="coord">World coordinates</param>
        /// <returns>Checker coordinates or null</returns>
        public Point? GetCellIntersection(Point coord)
        {
            // Determination of all sizes and coordinates
            Size image_size = _screen.Image.Size;
            Point image_coord = new Point(_screen.Width / 2 - image_size.Width / 2, _screen.Height / 2 - image_size.Height / 2);
            Rectangle image_rect = new Rectangle(image_coord, image_size);
            Rectangle board_rect = new Rectangle(new Point(image_coord.X + 118, image_coord.Y + 150), new Size(448, 452));
            Size cell_size = new Size(board_rect.Width / 8, board_rect.Height / 8);

            // Check the coordinates of all checkers
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle temp_rect = new Rectangle(new Point(board_rect.X + cell_size.Width * j, board_rect.Y + cell_size.Height * (7-i)), cell_size);
                    if (temp_rect.Contains(coord))
                        return new Point(j, i);
                }
            }
            return null;
        }


        /// <summary>
        /// Getting the world coordinates of the specified cell
        /// </summary>
        /// <param name="cell">Cell coordinates</param>
        /// <returns>World cell coordinates</returns>
        public Rectangle GetCellCoordinates(Point cell)
        {
            Size image_size = _screen.Image.Size;
            Point image_coord = new Point(_screen.Width / 2 - image_size.Width / 2, _screen.Height / 2 - image_size.Height / 2);
            Rectangle image_rect = new Rectangle(image_coord, image_size);
            Rectangle board_rect = new Rectangle(new Point(image_coord.X + 118, image_coord.Y + 150), new Size(448, 452));
            Size cell_size = new Size(board_rect.Width / 8, board_rect.Height / 8);
            return new Rectangle(new Point(board_rect.X + cell_size.Width * cell.X, board_rect.Y + cell_size.Height * (7-cell.Y)), cell_size);
        }


        /// <summary>
        /// Getting the coordinates of the board
        /// </summary>
        /// <returns>World coordinates of the board</returns>
        public Rectangle GetBoardCoordinates()
        {
            Size image_size = _screen.Image.Size;
            Point image_coord = new Point(_screen.Width / 2 - image_size.Width / 2, _screen.Height / 2 - image_size.Height / 2);
            Rectangle image_rect = new Rectangle(image_coord, image_size);
            Rectangle board_rect = new Rectangle(new Point(image_coord.X + 118, image_coord.Y + 150), new Size(448, 452));
            return board_rect;
        }


        /// <summary>
        /// Getting the relative world coordinates of the specified cell (relative to the board)
        /// </summary>
        /// <param name="cell">Cell coordinates</param>
        /// <returns>Relative world coordinates of cell</returns>
        public Rectangle GetCellRelativeCoord(Point cell)
        {
            Size image_size = _screen.Image.Size;
            Point image_coord = new Point(_screen.Width / 2 - image_size.Width / 2, _screen.Height / 2 - image_size.Height / 2);
            Rectangle image_rect = new Rectangle(image_coord, image_size);
            Rectangle board_rect = new Rectangle(new Point(image_coord.X + 118, image_coord.Y + 150), new Size(448, 452));
            Size cell_size = new Size(board_rect.Width / 8, board_rect.Height / 8);
            return new Rectangle(new Point(cell_size.Width * cell.X, cell_size.Height * (7 - cell.Y)), cell_size);
        }
    }
}
