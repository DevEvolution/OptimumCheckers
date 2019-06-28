using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Optimum
{
    /// <summary>
    /// Checker
    /// </summary>
    [Serializable]
    public class Checker
    {
        // Checker image
        public Bitmap img;

        // World coordinates of a checker
        public Rectangle rect;

        // Which side belongs
        public Belonging belong_to;

        // Is king
        public bool king;
    }
}
