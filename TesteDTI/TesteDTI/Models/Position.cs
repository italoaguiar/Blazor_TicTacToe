using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents game coordinates
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Represents the X coordinate of the board cell
        /// </summary>
        [Range(0,2, ErrorMessage = "Posição informada inválida.")]
        public int X { get; set; }


        /// <summary>
        /// Represents the Y coordinate of the board cell
        /// </summary>
        [Range(0, 2, ErrorMessage = "Posição informada inválida.")]
        public int Y { get; set; }
    }
}
