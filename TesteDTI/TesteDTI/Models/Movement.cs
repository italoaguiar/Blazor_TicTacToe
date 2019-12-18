using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    public class Movement
    {
        public Guid Id { get; set; }

        public Player Player { get; set; }

        public Position Position { get; set; }

    }
}
