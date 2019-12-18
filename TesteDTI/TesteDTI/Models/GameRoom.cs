using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDTI.Models
{
    /// <summary>
    /// Represents a tic-tac-toe game room
    /// </summary>
    public class GameRoom
    {
        public GameRoom()
        {
            Board = new BoardCell[3,3];

            //initialize the board
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Board[i, j] = null;
        }

        /// <summary>
        /// Game Id
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The player who will start the game
        /// </summary>
        public Player FirstPlayer { get; set; }


        /// <summary>
        /// Matrix representing the game board
        /// </summary>
        [JsonIgnore]
        public BoardCell[,] Board { get; set; }
    }
}
