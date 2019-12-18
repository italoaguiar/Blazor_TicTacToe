using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents the partial or final result of the game.
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Determines if the game has ended
        /// </summary>
        public bool IsEnded { get; set; }

        /// <summary>
        /// Determines the winner of the game.
        /// </summary>
        public Player? Winner { get; set; }
    }
}
