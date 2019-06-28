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
    /// Mouse click and move handler
    /// </summary>
    class MouseClickHandler
    {
        // World window
        PictureBox _screen;

        // Draggable checker
        public Checker _drag = null;

        // Coordinates of draggable checker
        Point? _clicked = null;

        // Game state
        State _state = null;

        // Coordinates of continuing move checker
        Point? _continious = null;

        // Move estimator
        Evaluator _estimator;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="screen">World window</param>
        /// <param name="state">Game state</param>
        public MouseClickHandler(PictureBox screen, ref State state)
        {
            // Initialization
            _screen = screen;
            _state = state;
            _estimator = new Evaluator(state);

            // Computer move if red starts first
            if (!Properties.Settings.Default.white_first)
                ComputerTurn();
        }

        /// <summary>
        /// User-click event handler
        /// </summary>
        /// <param name="e">Mouse options</param>
        public void MouseDown(MouseEventArgs e)
        {
            // Definition of checkers under the cursor at the moment of pressing
            Ruler ruler = new Ruler(_screen);
            Rectangle board_coord = ruler.GetBoardCoordinates();
            Point click_coord = new Point(e.X - board_coord.X, e.Y - board_coord.Y);
            Point? checker_intersection = ruler.GetCellIntersection(e.Location);

            // Determining the selected checker and its visual characteristics
            if (!checker_intersection.HasValue) return;
            _clicked = checker_intersection;
            Rectangle checker_rect = ruler.GetCellCoordinates(checker_intersection.Value);

            // There are no checkers under the cursor
            if (_clicked == null) return;

            // Enemy checker is selected - the move is impossible
            if (_state.checkers[_clicked.Value.X, _clicked.Value.Y] != null && _state.checkers[_clicked.Value.X, _clicked.Value.Y].belong_to != _state.turn)
            {
                ShowIncorrectTurnMessage(Reason.EnemyChecker);
                return;
            }

            // Expected move continuation, another checker move is impossible.
            if (_continious != null && _clicked != _continious)
            {
                ShowIncorrectTurnMessage(Reason.TurnContinuationExpected);
                return;
            }

            // Checker selection
            _drag = _state.checkers[_clicked.Value.X, _clicked.Value.Y];
        }


        /// <summary>
        /// Handler event for moving the mouse
        /// </summary>
        /// <param name="e">Mouse options</param>
        public void MouseMove(MouseEventArgs e)
        {
            // Checker is not selected
            if (_drag == null) return;

            // Determining the visual characteristics of a dragging checker
            Ruler ruler = new Ruler(_screen);
            Rectangle board_coord = ruler.GetBoardCoordinates();
            Point click_coord = new Point(e.X - board_coord.X, e.Y - board_coord.Y);
            _drag.rect.X = click_coord.X - _drag.rect.Width / 2;
            _drag.rect.Y = click_coord.Y - _drag.rect.Height / 2;
            return;
        }


        /// <summary>
        /// Event handler for releasing a mouse button by user
        /// </summary>
        /// <param name="e">Mouse options</param>
        public void MouseUp(MouseEventArgs e)
        {
            // Checker is not selected
            if (_drag == null) return;

            // Definition of the field under the cursor at the moment of pressing
            Ruler ruler = new Ruler(_screen);
            Point? cell_intersection = ruler.GetCellIntersection(e.Location);
            Reason status = Reason.OK;
            bool beating;

            // Assessment of the possibility and correctness of move
            if (cell_intersection.HasValue)
            {
                // Verify that the selected cell matches the game logic
                if (_state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y] == null && cell_intersection.Value.X % 2 == cell_intersection.Value.Y % 2)
                {
                    // Checking the move correctness
                    status = _estimator.EvaluateStep(_clicked.Value, cell_intersection.Value, out beating);

                    // Move is correct
                    if (status == Reason.OK)
                    {
                        // Moving checkers to a new position
                        _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y] = _drag;
                        _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].rect =
                            ruler.GetCellRelativeCoord(new Point(cell_intersection.Value.X, cell_intersection.Value.Y));
                        _state.checkers[_clicked.Value.X, _clicked.Value.Y] = null;

                        // Check for the need to convert checkers into a king
                        if (!_state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].king)
                        {
                            // For white checkers
                            if (cell_intersection.Value.Y == Properties.Settings.Default.low_bound && _drag.belong_to == Belonging.WhiteRose)
                            {
                                _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].king = true;
                                _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].img = Properties.Resources.whiteking;
                            }
                            // For red checkers
                            else if (cell_intersection.Value.Y == Properties.Settings.Default.high_bound && _drag.belong_to == Belonging.RedRose)
                            {
                                _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].king = true;
                                _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].img = Properties.Resources.redking;
                            }
                        }

                        // Determining the possibility of continuing the turn
                        var next_move = _estimator.NeedCheckerBeating(cell_intersection.Value,
                            _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].king && Properties.Settings.Default.rules_russian ?
                                 Properties.Settings.Default.high_bound + 1 :
                            1,
                            _state.checkers[cell_intersection.Value.X, cell_intersection.Value.Y].belong_to == Belonging.RedRose ?
                                Belonging.WhiteRose :
                            Belonging.RedRose);

                        // Check the need of continuing the turn
                        if (next_move.Count > 0)
                        {
                            // The need to beat (mandatory continuation of the course)
                            if (beating)
                            {
                                ShowContiniousTurnMessage();
                                _continious = cell_intersection.Value;
                            }
                            else
                            {
                                // Moving side swap
                                if (_state.turn == Belonging.RedRose)
                                    _state.turn = Belonging.WhiteRose;
                                else
                                    _state.turn = Belonging.RedRose;

                                // Check of conditions of loss of the opponent
                                bool enemy_has_checkers = false;
                                bool enemy_can_move = false;
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        // Check for opponent’s checkers
                                        if (_state.checkers[j, i] != null && _state.checkers[j, i].belong_to == _state.turn)
                                        {
                                            // Definition of the enemy
                                            Belonging new_enemy;
                                            int distance = 1;
                                            if (_state.turn == Belonging.RedRose)
                                                new_enemy = Belonging.WhiteRose;
                                            else
                                                new_enemy = Belonging.RedRose;

                                            // Determining the distance of the checkers move
                                            if (_state.checkers[j, i].king)
                                            {
                                                if (Properties.Settings.Default.rules_russian)
                                                    distance = Properties.Settings.Default.high_bound + 1;
                                                else
                                                    distance = 1;
                                            }
                                            enemy_has_checkers = true;

                                            // Determining the possibility of moving
                                            if (_estimator.CanMove(new Point(j, i), distance, new_enemy))
                                                enemy_can_move = true;
                                        }
                                    }
                                }

                                // Check win / lose event
                                if (!enemy_has_checkers || !enemy_can_move)
                                {
                                    // Winning Report
                                    CheckForVictory();
                                }
                                else
                                {
                                    // Computer turn
                                    ComputerTurn();

                                    // Message about changing turn owner
                                    ShowTurnOwnerMessage();
                                }

                                // Moving checker reset
                                _continious = null;
                            }
                        }
                        else
                        {
                            // Moving side swap
                            if (_state.turn == Belonging.RedRose)
                                _state.turn = Belonging.WhiteRose;
                            else
                                _state.turn = Belonging.RedRose;

                            // Check of conditions of loss of the opponent
                            bool enemy_has_checkers = false;
                            bool enemy_can_move = false;
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    // Check for opponent’s checkers
                                    if (_state.checkers[j, i] != null && _state.checkers[j, i].belong_to == _state.turn)
                                    {
                                        // Definition of the enemy
                                        Belonging new_enemy;
                                        int distance = 1;
                                        if (_state.turn == Belonging.RedRose)
                                            new_enemy = Belonging.WhiteRose;
                                        else
                                            new_enemy = Belonging.RedRose;

                                        // Determining the distance of the checkers move
                                        if (_state.checkers[j, i].king)
                                        {
                                            if (Properties.Settings.Default.rules_russian)
                                                distance = Properties.Settings.Default.high_bound + 1;
                                            else
                                                distance = 1;
                                        }
                                        enemy_has_checkers = true;

                                        // Determining the possibility of moving
                                        if (_estimator.CanMove(new Point(j, i), distance, new_enemy))
                                            enemy_can_move = true;
                                    }
                                }
                            }

                            // Check win / lose event
                            if (!enemy_has_checkers || !enemy_can_move)
                            {
                                // Winning Report
                                CheckForVictory();
                            }
                            else
                            {
                                // Computer turn
                                ComputerTurn();

                                // Message about changing turn owner
                                ShowTurnOwnerMessage();
                            }

                            // Moving checker reset
                            _continious = null;
                        }
                    }
                    else
                    {
                        // Return to the previous position
                        _state.checkers[_clicked.Value.X, _clicked.Value.Y].rect =
                            ruler.GetCellRelativeCoord(new Point(_clicked.Value.X, _clicked.Value.Y));
                    }

                }
                else
                {
                    // Return to the previous position
                    status = Reason.ImpossibleTurn;
                    _state.checkers[_clicked.Value.X, _clicked.Value.Y].rect =
                        ruler.GetCellRelativeCoord(new Point(_clicked.Value.X, _clicked.Value.Y));
                }
            }
            else if (_clicked != null)
            {
                // Return to the previous position
                status = Reason.ImpossibleTurn;
                _state.checkers[_clicked.Value.X, _clicked.Value.Y].rect =
                    ruler.GetCellRelativeCoord(new Point(_clicked.Value.X, _clicked.Value.Y));
            }

            // Reset selected checker
            this._clicked = null;
            this._drag = null;

            // The message about the incorrectness of the move (if incorrect, an internal check is performed)
            ShowIncorrectTurnMessage(status);
        }

        private void CheckForVictory()
        {
            if (!Properties.Settings.Default.give_away)
            {
                ShowVictoryMessage();
            }
            else
            {
                ShowVictoryMessage(true);
            }
        }

        /// <summary>
        /// Computer turn
        /// </summary>
        public void ComputerTurn()
        {
            // Init
            List<Point[]>[] comp_turns = new List<Point[]>[3];
            for (int i = 0; i < 3; i++)
            {
                comp_turns[i] = new List<Point[]>();
            }
            Evaluator estimator = new Evaluator(_state);

            // Search and categorization of moves
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Finding "ally" checkers
                    if (_state.checkers[j, i] != null && _state.checkers[j, i].belong_to == Belonging.RedRose)
                    {
                        // Determining the moving range of the checkers
                        int distance;
                        if (_state.checkers[j, i].king && Properties.Settings.Default.rules_russian)
                            distance = Properties.Settings.Default.high_bound + 1;
                        else
                            distance = 1;

                        // Building a list of moves
                        var computer_list = estimator.GetComputerMoves(new Point(j, i), distance, Belonging.WhiteRose);

                        // Categorization of moves
                        if (computer_list[0].Count > 0)
                        {
                            // 1 category moves (beating checkers)
                            foreach (var item in computer_list[0])
                            {
                                comp_turns[0].Add(new Point[] { new Point(j, i), item });
                            }
                        }
                        else if (computer_list[1].Count > 0)
                        {
                            // 2 category moves (safe position)
                            foreach (var item in computer_list[1])
                            {
                                comp_turns[1].Add(new Point[] { new Point(j, i), item });
                            }
                        }
                        else if (computer_list[2].Count > 0)
                        {
                            // 3 category moves (dangerous position)
                            foreach (var item in computer_list[2])
                            {
                                comp_turns[2].Add(new Point[] { new Point(j, i), item });
                            }
                        }
                    }
                }
            }

            // Running a move / chain of moves by computer
            Random rand = new Random();
            Ruler ruler = new Ruler(_screen);

            List<Point[]>[] temp_comp_turns = new List<Point[]>[comp_turns.Length];

            if (Properties.Settings.Default.give_away)
            {
                if (Properties.Settings.Default.force_beating)
                {
                    for (int i = 1; i < comp_turns.Length; i++)
                    {
                        temp_comp_turns[i] = comp_turns[comp_turns.Length - i];
                    }
                    temp_comp_turns[0] = comp_turns[0];
                }
                else
                {
                    for (int i = 0; i < comp_turns.Length; i++)
                    {
                        temp_comp_turns[i] = comp_turns[comp_turns.Length - 1 - i];
                    }
                }
                
                comp_turns = temp_comp_turns;
            }

            // Attempt to perform a move of category 1
            if (comp_turns[0].Count > 0)
            {
#if DEBUG
                ShowDebugMessage(1);
#endif
                // Getting a list of possible moves
                var turn_list = comp_turns[0];
                var turn = turn_list[rand.Next(turn_list.Count - 1)];
                Point direction = new Point(Math.Sign(turn[1].X - turn[0].X), Math.Sign(turn[1].Y - turn[0].Y));

                // Taking checkers
                for (int y = turn[0].Y + direction.Y, x = turn[0].X + direction.X;
                        y != turn[1].Y;
                        y += direction.Y, x += direction.X)
                {
                    if (_state.checkers[x, y] != null)
                    {
                        if (_state.checkers[x, y].belong_to == Belonging.WhiteRose)
                        {
                            _state.checkers[x, y] = null;
                        }
                    }
                }

                // Moving a checker to a new position
                _state.checkers[turn[1].X, turn[1].Y] = _state.checkers[turn[0].X, turn[0].Y];
                _state.checkers[turn[0].X, turn[0].Y] = null;
                _state.checkers[turn[1].X, turn[1].Y].rect = ruler.GetCellRelativeCoord(turn[1]);

                // Check for the need to convert checkers into a king
                if (!_state.checkers[turn[1].X, turn[1].Y].king)
                {
                    // For white checkers
                    if (turn[1].Y == Properties.Settings.Default.low_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.WhiteRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.whiteking;
                    }
                    // For red checkers
                    else if (turn[1].Y == Properties.Settings.Default.high_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.RedRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.redking;
                    }
                }

                // Determining the possibility of continuing the turn
                var next_move = _estimator.NeedCheckerBeating(turn[1],
                    _state.checkers[turn[1].X, turn[1].Y].king && Properties.Settings.Default.rules_russian ?
                    Properties.Settings.Default.high_bound + 1 :
                    1,
                    Belonging.WhiteRose);
                bool to_king = false;
                Point inv_last_direction = new Point();

                // Continuing the movement
                while (next_move.Count > 0)
                {
                    // Determination of new coordinates
                    turn[0] = turn[1];
                    turn[1] = new Point(turn[0].X + next_move.First.Value.direction.X * next_move.First.Value.minimum_distance, turn[0].Y + next_move.First.Value.direction.Y * next_move.First.Value.minimum_distance);

                    // Cutting off an invalid move after setting a king
                    if (to_king && inv_last_direction == new Point(Math.Sign(turn[1].X - turn[0].X), Math.Sign(turn[1].Y - turn[0].Y)))
                    {
                        // If there is another move - reassigning the end position
                        if (next_move.Count > 1)
                            turn[1] = new Point(turn[0].X + next_move.First.Next.Value.direction.X * next_move.First.Next.Value.minimum_distance, turn[0].Y + next_move.First.Next.Value.direction.Y * next_move.First.Next.Value.minimum_distance);
                        else
                            break;
                    }

                    // Moving a checker to a new position
                    _state.checkers[turn[1].X, turn[1].Y] = _state.checkers[turn[0].X, turn[0].Y];
                    _state.checkers[turn[0].X, turn[0].Y] = null;
                    _state.checkers[turn[1].X, turn[1].Y].rect = ruler.GetCellRelativeCoord(turn[1]);

                    // Remove beaten checkers
                    Point dir_beat = new Point(Math.Sign(turn[1].X - turn[0].X), Math.Sign(turn[1].Y - turn[0].Y));
                    for (int y = turn[0].Y + dir_beat.Y, x = turn[0].X + dir_beat.X;
                            y != turn[1].Y;
                            y += dir_beat.Y, x += dir_beat.X)
                    {
                        if (_state.checkers[x, y] != null)
                        {
                            if (_state.checkers[x, y].belong_to == Belonging.WhiteRose)
                            {
                                _state.checkers[x, y] = null;
                            }
                        }
                    }

                    // Check for the need to convert checkers into a king
                    if (!_state.checkers[turn[1].X, turn[1].Y].king)
                    {
                        // For white checkers
                        if (turn[1].Y == Properties.Settings.Default.low_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.WhiteRose)
                        {
                            _state.checkers[turn[1].X, turn[1].Y].king = true;
                            _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.whiteking;
                            to_king = true;
                            inv_last_direction = new Point(-dir_beat.X, -dir_beat.Y);
                        }
                        // For red checkers
                        else if (turn[1].Y == Properties.Settings.Default.high_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.RedRose)
                        {
                            _state.checkers[turn[1].X, turn[1].Y].king = true;
                            _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.redking;
                            to_king = true;
                        }
                    }

                    // Determining the need to continue the turn
                    next_move = _estimator.NeedCheckerBeating(turn[1],
                        _state.checkers[turn[1].X, turn[1].Y].king && Properties.Settings.Default.rules_russian ?
                        Properties.Settings.Default.high_bound + 1 :
                        1,
                        Belonging.WhiteRose);
                }

                // Turn owner swap
                _state.turn = Belonging.WhiteRose;

                // Check of conditions of loss of the opponent
                bool enemy_has_checkers = false;
                bool enemy_can_move = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // Check for opponent’s checkers
                        if (_state.checkers[j, i] != null && _state.checkers[j, i].belong_to == _state.turn)
                        {
                            // Definition of the enemy
                            Belonging new_enemy;
                            int distance = 1;
                            if (_state.turn == Belonging.RedRose)
                                new_enemy = Belonging.WhiteRose;
                            else
                                new_enemy = Belonging.RedRose;

                            // Determining the distance of the move checkers
                            if (_state.checkers[j, i].king)
                            {
                                if (Properties.Settings.Default.rules_russian)
                                    distance = Properties.Settings.Default.high_bound + 1;
                                else
                                    distance = 1;
                            }
                            enemy_has_checkers = true;

                            // Determining the possibility of moving checkers
                            if (_estimator.CanMove(new Point(j, i), distance, new_enemy))
                                enemy_can_move = true;
                        }
                    }
                }

                // Check win / lose event
                if (!enemy_has_checkers || !enemy_can_move)
                {
                    // Winning Report
                    CheckForVictory();
                }
            }
            // Attempt to perform a 2 category move
            else if (comp_turns[1].Count > 0)
            {
#if DEBUG
                ShowDebugMessage(2);
#endif
                // Getting a list of possible moves
                var turn_list = comp_turns[1];
                var turn = turn_list[rand.Next(turn_list.Count - 1)];

                // Moving a checker to a new position
                _state.checkers[turn[1].X, turn[1].Y] = _state.checkers[turn[0].X, turn[0].Y];
                _state.checkers[turn[0].X, turn[0].Y] = null;
                _state.checkers[turn[1].X, turn[1].Y].rect = ruler.GetCellRelativeCoord(turn[1]);

                // Check for the need to convert checkers into a king
                if (!_state.checkers[turn[1].X, turn[1].Y].king)
                {
                    // For white checkers
                    if (turn[1].Y == Properties.Settings.Default.low_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.WhiteRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.whiteking;
                    }
                    // For red checkers
                    else if (turn[1].Y == Properties.Settings.Default.high_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.RedRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.redking;
                    }
                }

                // Turn owner swap
                _state.turn = Belonging.WhiteRose;
            }
            // Attempt to perform a 3 category move
            else if (comp_turns[2].Count > 0)
            {
#if DEBUG
                ShowDebugMessage(3);
#endif
                // Getting a list of possible moves
                var turn_list = comp_turns[2];
                var turn = turn_list[rand.Next(turn_list.Count - 1)];

                // Moving a checker to a new position
                _state.checkers[turn[1].X, turn[1].Y] = _state.checkers[turn[0].X, turn[0].Y];
                _state.checkers[turn[0].X, turn[0].Y] = null;
                _state.checkers[turn[1].X, turn[1].Y].rect = ruler.GetCellRelativeCoord(turn[1]);

                // Check for the need to convert checkers into a king
                if (!_state.checkers[turn[1].X, turn[1].Y].king)
                {
                    // For white checkers
                    if (turn[1].Y == Properties.Settings.Default.low_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.WhiteRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.whiteking;
                    }
                    // For red checkers
                    else if (turn[1].Y == Properties.Settings.Default.high_bound && _state.checkers[turn[1].X, turn[1].Y].belong_to == Belonging.RedRose)
                    {
                        _state.checkers[turn[1].X, turn[1].Y].king = true;
                        _state.checkers[turn[1].X, turn[1].Y].img = Properties.Resources.redking;
                    }
                }

                // Turn owner swap
                _state.turn = Belonging.WhiteRose;
            }
            else
            {
                // Winning Report
                CheckForVictory();
            }

            // Invalidation the scene
            _screen.Invalidate();
        }

        /// <summary>
        /// Displays incorrect turn message
        /// </summary>
        /// <param name="reason">Reason of not following the rules</param>
        public void ShowIncorrectTurnMessage(Reason reason)
        {
            if (reason == Reason.OK) return;

            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Невозможно совершить ход";
            tip.ToolTipIcon = ToolTipIcon.Warning;
            tip.UseAnimation = true;
            tip.UseFading = true;

            String msg = String.Empty;

            switch (reason)
            {
                case Reason.TooMuchRange:
                    msg = "Превышена максимальная дальность хода для шашки.";
                    break;
                case Reason.ImpossibleTurn:
                    msg = "Ход невозможен исходя из логики игры.";
                    break;
                case Reason.RulesMismatch:
                    msg = "Ход не сооветствует заданным правилам игры. (См. настройки)";
                    break;
                case Reason.IncorrectDirection:
                    msg = "Шашка не может двигаться в заданном направлении.";
                    break;
                case Reason.CheckerNeedsBeating:
                    msg = "По установленным правилам бить обязательно.";
                    break;
                case Reason.AnotherCheckerNeedsBeating:
                    msg = "Другая шашка может бить. По установленным правилам бить обязательно.";
                    break;
                case Reason.TurnContinuationExpected:
                    msg = "Ходившая шашка должна продолжить ход.";
                    break;
                case Reason.EnemyChecker:
                    msg = "Выбранная шашка принадлежит противнику";
                    break;
            }
            tip.Show(msg, _screen, new Point(20, 10), 1000);
        }


        /// <summary>
        /// Displays a message waiting to continue the move
        /// </summary>
        public void ShowContiniousTurnMessage()
        {
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Продолжение хода";
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.UseAnimation = true;
            tip.UseFading = true;

            tip.Show("Предусматривается продолжение хода", _screen, new Point(20, 10), 1000);
        }


        /// <summary>
        /// Display messages about turn owner
        /// </summary>
        public void ShowTurnOwnerMessage()
        {
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Ход игрока";
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.UseAnimation = true;
            tip.UseFading = true;
            String msg;
            if (_state.turn == Belonging.RedRose)
                msg = "красных.";
            else
                msg = "белых.";
            tip.Show("Ход " + msg, _screen, new Point(20, 10), 1000);
        }


        /// <summary>
        /// Displays a victory message
        /// </summary>
        public void ShowVictoryMessage(bool inverse = false)
        {
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Победа";
            tip.ToolTipIcon = ToolTipIcon.Error;
            tip.UseAnimation = true;
            tip.UseFading = true;
            String msg;

            Belonging belonging;
            if (inverse)
            {
                if(_state.turn == Belonging.RedRose)
                    belonging = Belonging.WhiteRose;
                else
                    belonging = Belonging.RedRose;
            }
            else
            {
                belonging = _state.turn;
            }
            
            if (belonging == Belonging.RedRose)
                msg = "белых.";
            else
                msg = "красных.";
            tip.Show("Победа " + msg, _screen, new Point(20, 10), 5000);
        }


        /// <summary>
        /// Displays debug message
        /// </summary>
        public void ShowDebugMessage(int category)
        {
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Дебаг";
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.UseAnimation = true;
            tip.UseFading = true;

            tip.Show($"Категория = {category}", _screen, new Point(20, 40), 2000);
        }
    }
}
