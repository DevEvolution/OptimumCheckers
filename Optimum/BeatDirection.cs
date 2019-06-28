using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimum
{
    /// <summary>
    /// Direction of beating checkers
    /// </summary>
    struct BeatDirection
    {
        // Direction
        public Point direction;
        // Minimum distance to beat checkers
        public int minimum_distance;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="direct">Direction</param>
        /// <param name="min_distance">Minimal distance</param>
        public BeatDirection(Point direct, int min_distance)
        {
            direction = direct;
            minimum_distance = min_distance;
        }
    }
}
