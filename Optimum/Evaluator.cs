using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimum
{
    /// <summary>
    /// Movement evaluator
    /// </summary>
    class Evaluator
    {
        State _state;

        /// <summary>
        /// Creates evaluator
        /// </summary>
        /// <param name="state">Game state</param>
        public Evaluator(State state)
        {
            _state = state;
        }


        /// <summary>
        /// Assessment of the correctness of the checkers move
        /// </summary>
        /// <param name="from">From which position</param>
        /// <param name="to">To which position</param>
        /// <param name="beating">Flag of checker capture</param>
        /// <returns>The reason why the move is rejected</returns>
        public Reason EvaluateStep(Point from, Point to, out bool beating)
        {
            // Initial values
            Point distance = new Point(to.X - from.X, to.Y - from.Y);
            int max_distance = 1;
            beating = false;

            // Increase the distance of the course for the king in Russian drafts
            if (Properties.Settings.Default.rules_russian && _state.checkers[from.X, from.Y].king)
                max_distance += Properties.Settings.Default.high_bound;

            // Find out the side of the enemy
            Belonging enemy;
            if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose)
                enemy = Belonging.WhiteRose;
            else
                enemy = Belonging.RedRose;

            // Direction of travel and information about the capture of checkers
            Point direction = new Point(Math.Sign(to.X - from.X), Math.Sign(to.Y - from.Y));
            var beating_directions = NeedCheckerBeating(from, max_distance, enemy);

            // Limiting the taking of checkers to comply with the rules of English checkers
            if (!Properties.Settings.Default.rules_russian && !_state.checkers[from.X, from.Y].king)
            {
                // Build a list of restricted directions
                LinkedList<BeatDirection> restricted_directions = new LinkedList<BeatDirection>();
                int restricted_direction;
                if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose)
                    restricted_direction = -1;
                else
                    restricted_direction = 1;
                foreach (BeatDirection dir in beating_directions)
                {
                    if (dir.direction.Y == restricted_direction)
                        restricted_directions.AddLast(dir);
                }

                // Removing the wrong directions
                foreach (BeatDirection dir in restricted_directions)
                    beating_directions.Remove(dir);
            }

            // Check: exceeding the maximum distance
            if (Math.Abs(distance.X) != Math.Abs(distance.Y))
                return Reason.ImpossibleTurn;

            // Check: obligatory beating of checkers
            if (beating_directions.Count > 0)
            {
                foreach (BeatDirection beat_dir in beating_directions)
                {
                    if (direction == beat_dir.direction && Math.Abs(distance.Y) >= beat_dir.minimum_distance)
                    {
                        beating = true;
                    }
                }
                if (!beating && Properties.Settings.Default.force_beating)
                {
                    return Reason.CheckerNeedsBeating;
                }
            }
            else
            {
                // Check: the need to beat the other side walking checkers
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (_state.checkers[j, i] != null && from != new Point(j, i) && _state.checkers[j, i].belong_to != enemy)
                        {
                            // Setting the required move length for checkers
                            int another_distance;
                            if (_state.checkers[j, i].king)
                                another_distance = Properties.Settings.Default.high_bound + 1;
                            else
                                another_distance = 1;
                            var another_beating_directions = NeedCheckerBeating(new Point(j, i), another_distance, enemy);

                            // Limiting the taking of checkers to comply with the rules of English checkers
                            if (!Properties.Settings.Default.rules_russian && !_state.checkers[j, i].king)
                            {
                                // Build a list of wrong directions
                                LinkedList<BeatDirection> restricted_directions = new LinkedList<BeatDirection>();
                                int restricted_direction;
                                if (_state.checkers[j, i].belong_to == Belonging.RedRose)
                                    restricted_direction = -1;
                                else
                                    restricted_direction = 1;
                                foreach (BeatDirection dir in beating_directions)
                                {
                                    if (dir.direction.Y == restricted_direction)
                                        restricted_directions.AddLast(dir);
                                }

                                // Removing the wrong directions
                                foreach (BeatDirection dir in restricted_directions)
                                    beating_directions.Remove(dir);
                            }

                            // The need to hit other checkers walking side
                            if (another_beating_directions.Count > 0)
                                return Reason.AnotherCheckerNeedsBeating;
                        }
                    }
                }
            }

            // If the operation of taking a checker is confirmed, increase the movement distance for correct further evaluation of the course
            if (beating)
                max_distance += 1;

            // Check: the maximum allowable distance of checkers move is exceeded
            if (Math.Abs(distance.X) > max_distance)
                return Reason.TooMuchRange;

            // Check: correctness of the direction of beating the checkers
            if (!_state.checkers[from.X, from.Y].king)
            {
                if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose && direction.Y == -1 ||
                    _state.checkers[from.X, from.Y].belong_to == Belonging.WhiteRose && direction.Y == 1)
                {
                    if ((beating && !Properties.Settings.Default.rules_russian) || !beating)
                        return Reason.IncorrectDirection;
                }

            }

            // Check: checker jumped only through enemy checkers
            if (beating)
                for (int y = from.Y + direction.Y, x = from.X + direction.X;
                        y != to.Y;
                        y += direction.Y, x += direction.X)
                {
                    if (_state.checkers[x, y] != null)
                    {
                        if (_state.checkers[x, y].belong_to == enemy)
                        {
                            // Removing beaten checkers
                            _state.checkers[x, y] = null;
                        }
                        else
                        {
                            // Attempt to jump over an ally
                            return Reason.ImpossibleTurn;
                        }
                    }
                }
            else
            {
                // Test: the move was made without jumping over obstacles
                for (int y = from.Y + direction.Y, x = from.X + direction.X;
                    y != to.Y;
                    y += direction.Y, x += direction.X)
                {
                    if (_state.checkers[x, y] != null)
                    {
                        return Reason.ImpossibleTurn;
                    }
                }
            }

            // The move has passed validation
            return Reason.OK;
        }


        /// <summary>
        /// Assessing the need to beat a checker and build a list of possible directions for beating
        /// </summary>
        /// <param name="from">Position of moving checker</param>
        /// <param name="max_distance">Distance of moving checkers</param>
        /// <param name="enemy">Enemy side</param>
        /// <returns>The list of possible directions for beating a checker</returns>
        public LinkedList<BeatDirection> NeedCheckerBeating(Point from, int max_distance, Belonging enemy)
        {
            // Initial values
            LinkedList<BeatDirection> beating_directions_list = new LinkedList<BeatDirection>();
            bool single_enemy = false;
            bool fail = false;
            int minimum_beating_distance = 0;

            // Diagonal test in all directions
            for (int kx = -1; kx <= 1; kx += 2)
            {
                for (int ky = -1; ky <= 1; ky += 2)
                {
                    // Restriction of permissible take sides to comply with the rules of English drafts
                    if (!Properties.Settings.Default.rules_russian && !_state.checkers[from.X, from.Y].king)
                    {
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.WhiteRose && ky == 1)
                            continue;
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose && ky == -1)
                            continue;
                    }

                    // Checking the diagonal for the presence of single opponent checkers
                    for (int y = from.Y + ky, x = from.X + kx;
                        Math.Abs(y - from.Y) < max_distance + 1;
                        y += ky, x += kx)
                    {
                        // If going beyond the board stop the diagonal test
                        if (!CheckBounds(x, y))
                        {
                            if (single_enemy)
                                fail = true;
                            break;
                        }

                        // Checker found on the diagonal
                        if (_state.checkers[x, y] != null)
                        {
                            // If the found checker belongs to the enemy
                            if (_state.checkers[x, y].belong_to == enemy)
                            {
                                // Checking the position following the checker
                                Point next = new Point(x + kx, y + ky);

                                // End of the board, termination of the diagonal test
                                if (!CheckBounds(next.X, next.Y))
                                {
                                    fail = true;
                                    break;
                                }

                                // Free field, can beat
                                if (_state.checkers[next.X, next.Y] == null)
                                {
                                    fail = false;
                                    single_enemy = true;
                                    minimum_beating_distance = Math.Abs(next.Y - from.Y);
                                    break;
                                }
                                else
                                {
                                    // The field is occupied by another checker, the termination of the diagonal test
                                    fail = true;
                                    break;
                                }
                            }
                            else
                            {
                                // Found allied checker, stopping diagonal test
                                fail = true;
                                break;
                            }
                        }
                    }

                    // If the search for checkers that can be beat was successful
                    if (single_enemy && !fail)
                        beating_directions_list.AddLast(new BeatDirection(new Point(kx, ky), minimum_beating_distance));

                    // Clearing all flags to test the next diagonal
                    fail = false;
                    single_enemy = false;
                }
            }
            return beating_directions_list;
        }


        /// <summary>
        /// Building the list of available moves for computer
        /// Move can rely to one of following categories:
        /// 1 - one or more enemy checker was beaten
        /// 2 - safe move (checker is not on the dangerous position)
        /// 3 - unsafe move (checker is on dangerous position)
        /// </summary>
        /// <param name="from">Moving checker position</param>
        /// <param name="max_distance">Move distance of checker</param>
        /// <param name="enemy">Enemy side</param>
        /// <returns>List of all available moves</returns>
        public List<Point>[] GetComputerMoves(Point from, int max_distance, Belonging enemy)
        {
            // Categories init
            List<Point>[] moves = new List<Point>[3];
            for (int i = 0; i < 3; i++)
            {
                moves[i] = new List<Point>();
            }

            // Try finding the moves of 1st category
            var beat_dir = NeedCheckerBeating(from, max_distance, enemy);
            if (beat_dir.Count > 0)
            {
                foreach (var item in beat_dir)
                {
                    Point to = new Point(from.X+item.direction.X * item.minimum_distance, from.Y + item.direction.Y * item.minimum_distance);
                    moves[0].Add(to);
                }
                return moves;
            }

            // Try finding the moves of 2nd and 3rd categories
            for (int kx = -1; kx <= 1; kx += 2)
            {
                for (int ky = -1; ky <= 1; ky += 2)
                {
                    // Determining the permissible sides of a move without beating a checker
                    if (!_state.checkers[from.X, from.Y].king)
                    {
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.WhiteRose && ky == 1)
                            continue;
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose && ky == -1)
                            continue;
                    }

                    // Assessment of the possibility of a move to different positions in the diagonal
                    for (int y = from.Y + ky, x = from.X + kx;
                        Math.Abs(y - from.Y) < max_distance + 1;
                        y += ky, x += kx)
                    {
                        // End of the board, termination of directional assessment
                        if (!CheckBounds(x, y)) break;

                        // Free cell, move is possible
                        if (_state.checkers[x, y] == null)
                        {
                            // Definition of a category of a move, fitting of a checker on an estimated position
                            bool prioritized = true;
                            for (int predict_kx = -1; predict_kx <= 1; predict_kx += 2)
                            {
                                for (int predict_ky = -1; predict_ky <= 1; predict_ky += 2)
                                {
                                    // Restriction of permissible take sides to comply with the rules of English checkers
                                    if (!Properties.Settings.Default.rules_russian)
                                    {
                                        if (enemy == Belonging.WhiteRose && ky == 1)
                                            continue;
                                        if (enemy == Belonging.RedRose && ky == -1)
                                            continue;
                                    }

                                    // Finding the enemy's checkers located nearby and threatening to walk
                                    if (CheckBounds(x + predict_kx, y + predict_ky) && _state.checkers[x + predict_kx, y + predict_ky] != null &&
                                        _state.checkers[x + predict_kx, y + predict_ky].belong_to == enemy)
                                    {
                                        // Check for the presence of a free cell, allowing the enemy to beat the moving checker
                                        if (CheckBounds(x - predict_kx, y - predict_ky) && (_state.checkers[x - predict_kx, y - predict_ky] == null || from == new Point(x - predict_kx, y - predict_ky)))
                                            prioritized = false;
                                    }

                                }
                            }

                            // The final determination of the move to 2 or 3 categories
                            if (prioritized)
                                moves[1].Add(new Point(x, y));
                            else
                                moves[2].Add(new Point(x, y));
                        }
                        // The cell is occupied by an ally - the move is impossible
                        else if (_state.checkers[x, y].belong_to != enemy)
                            break;
                    }
                }
            }
            return moves;
        }


        /// <summary>
        /// Assessment of the overall ability to move checkers
        /// </summary>
        /// <param name="from">Checker position</param>
        /// <param name="max_distance">Move distance of checker</param>
        /// <param name="enemy">Enemy side</param>
        /// <returns>Can checker move</returns>
        public bool CanMove(Point from, int max_distance, Belonging enemy)
        {
            // Beating a checker is required - move available
            if (NeedCheckerBeating(from, max_distance, enemy).Count > 0)
                return true;

            // Evaluation in all directions
            for (int kx = -1; kx <= 1; kx += 2)
            {
                for (int ky = -1; ky <= 1; ky += 2)
                {
                    // Limiting directions for simple checkers
                    if (!_state.checkers[from.X, from.Y].king)
                    {
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.WhiteRose && ky == 1)
                            continue;
                        if (_state.checkers[from.X, from.Y].belong_to == Belonging.RedRose && ky == -1)
                            continue;
                    }

                    // Assessment of the possibility of a diagonal move
                    for (int y = from.Y + ky, x = from.X + kx;
                        Math.Abs(y - from.Y) < max_distance + 1;
                        y += ky, x += kx)
                    {
                        // The end of the board, the inability to move in a given direction
                        if (!CheckBounds(x, y)) break;

                        // Free field, move is possible
                        if (_state.checkers[x, y] == null)
                            return true;
                    }
                }
            }

            // Move is impossible
            return false;
        }

        /// <summary>
        /// Check position is on board
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns>Is position on board</returns>
        private bool CheckBounds(int x, int y)
        {
            return x >= Properties.Settings.Default.low_bound && x <= Properties.Settings.Default.high_bound &&
                        y >= Properties.Settings.Default.low_bound && y <= Properties.Settings.Default.high_bound;
        }
    }
}
