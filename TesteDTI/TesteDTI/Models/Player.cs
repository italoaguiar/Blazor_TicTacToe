using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents a player
    /// </summary>
    public enum Player
    {
        /// <summary>
        /// The X Player
        /// </summary>
        X,

        /// <summary>
        /// The O Player
        /// </summary>
        O,

        /// <summary>
        /// No Players
        /// </summary>
        Draw
    }
}
