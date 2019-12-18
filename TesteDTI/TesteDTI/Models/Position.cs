using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    public class Position
    {
        [Range(0,2, ErrorMessage = "Posição informada inválida.")]
        public int X { get; set; }

        [Range(0, 2, ErrorMessage = "Posição informada inválida.")]
        public int Y { get; set; }
    }
}
