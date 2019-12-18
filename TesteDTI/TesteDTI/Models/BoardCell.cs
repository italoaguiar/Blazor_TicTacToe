using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents a board cell
    /// </summary>
    public class BoardCell
    {
        /// <summary>
        /// Player who made the move
        /// </summary>
        public Player Player { get; set; }


        /// <summary>
        /// Movement coordinate
        /// </summary>
        public Position Position { get; set; }
    }
}
