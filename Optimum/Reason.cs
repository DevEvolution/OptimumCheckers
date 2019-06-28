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
    /// Cause of move incorectness
    /// </summary>
    enum Reason
    {
        OK,

        TooMuchRange, // Move range exceeded
        ImpossibleTurn, // Turn is impossible
        RulesMismatch, // Does not comply with the rules (ex .: attempt to eat behind the checker in English checkers)
        IncorrectDirection, // Wrong turn direction
        CheckerNeedsBeating, // Need to beat the checker
        AnotherCheckerNeedsBeating, // Another allied checker can beat
        TurnContinuationExpected, // The move must be completed.
        EnemyChecker // An enemy checker is selected.
    }
}
