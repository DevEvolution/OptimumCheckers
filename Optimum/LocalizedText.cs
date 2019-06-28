using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Optimum
{
    /// <summary>
    /// Localization
    /// </summary>
    [DataContract]
    class LocalizedText
    {
        // Language
        [DataMember]
        public string language = "English";

        // Menu text
        [DataMember]
        public Menu menu = new Menu();

        // Settings text
        [DataMember]
        public Settings settings = new Settings();

        // Messages
        [DataMember]
        public Messages messages = new Messages();

        /// <summary>
        /// Menu text
        /// </summary>
        [DataContract]
        public class Menu
        {
            [DataMember]
            public string newGame = "New game",
                loadGame = "Load game",
                saveGame = "Save game",
                settings = "Settings",
                exit = "Exit",

                binaryFile = "Binary file",
                fileCorrupted = "File is damaged or has incorrect format:";
        }

        /// <summary>
        /// Settings text
        /// </summary>
        [DataContract]
        public class Settings
        {
            [DataMember]
            public string title = "Application settings",
                language = "Language",
                rules = "Game rules",
                russianDraughts = "Russian draughts",
                russianDraughtsRules = "  - pieces can beat opposite\n  - king can move on any\n    distance ",
                englishCheckers = "English checkers",
                englishCheckersRules = "  - pieces can beat only in forward\n    direction\n  - king can move only\n    on distance of 1 cell",
                giveaway = "Giveaway",

                firstTurn = "First turn",
                white = "White",
                red = "Red",

                board = "Board",

                beatingRules = "Beating rules",
                beatingIsNecessary = "Beating is necessary",
                beatAtWill = "Beat at will";
        }

        /// <summary>
        /// Messages
        /// </summary>
        [DataContract]
        public class Messages
        {
            [DataMember]
            public string impossibleToMove = "Move is impossible",
                tooMuchRangeReason = "Maximum move distance for checkers exceeded.",
                impossibleTurn = "Move is impossible based on the logic of the game.",
                rulesMismatch = "A move does not follow the rules of the game. (See settings)",
                incorrectDirection = "A checker cannot move in a given direction.",
                checkerNeedsBeating = "According to the established rules it is necessary to beat.",
                anotherCheckerNeedsBeating = "Another checker can beat. According to the established rules it is necessary to beat.",
                turnContinuationExpected = "Walking checker should continue to move.",
                enemyChecker = "The selected piece belongs to the opponent",

                continuousTurn = "Continuing the move",
                continuousMessage = "Previously moved checker should continue its turn.",

                turnHeader = "Player's move",
                turnRed = "Red rose moves.",
                turnWhite = "White rose moves.",

                victoryHeader = "Victory",
                victoryRed = "Red rose is victorious.",
                victoryWhite = "White rose is victorious.",

                debug = "Debug",
                debugCategory = "Category = ";
        }
    }
}
