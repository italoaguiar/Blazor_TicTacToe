using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// represents a parameter for board cell events
    /// </summary>
    public class GridCellEventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GridCellEventArgs()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">Cell position</param>
        public GridCellEventArgs(Position p)
        {
            Position = p;
        }


        /// <summary>
        /// Represents the coordinate of a cell
        /// </summary>
        public Position Position { get; set; }
    }
}
