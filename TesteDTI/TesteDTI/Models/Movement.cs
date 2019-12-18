using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents a player move
    /// </summary>
    public class Movement : BoardCell
    {
        /// <summary>
        /// Game movement identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
