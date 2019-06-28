using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimum
{
    /// <summary>
    /// Game state
    /// </summary>
    [Serializable]
    public class State : ICloneable
    {
        // Checkers
        public Checker[,] checkers = new Checker[8,8];

        // Color of turn owner player
        public Belonging turn = Belonging.WhiteRose;


        /// <summary>
        /// Converting a checker array to a serialization list
        /// </summary>
        /// <returns>Checkers List</returns>
        public List<Checker> CheckersToList()
        {
            List<Checker> list = new List<Checker>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    list.Add(checkers[j, i]);
                }
            }
            return list;
        }


        /// <summary>
        /// Converting a checkers list into an array when deserializing
        /// </summary>
        /// <param name="list">Checkers List</param>
        public void CheckersFromList(List<Checker> list)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    checkers[j, i] = list[j+i*8];
                }
            }
        }


        /// <summary>
        /// Clone game state
        /// </summary>
        /// <returns>Copy of game state</returns>
        public object Clone()
        {
            State copy = new State();
            copy.turn = this.turn;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (checkers[j, i] != null)
                        copy.checkers[j, i] = new Checker()
                        {
                            img = checkers[j, i].img,
                            belong_to = checkers[j, i].belong_to,
                            king = checkers[j, i].king,
                            rect = checkers[j, i].rect
                        };
                    else
                        copy.checkers[j, i] = null;
            return copy;
        }
    }
}
