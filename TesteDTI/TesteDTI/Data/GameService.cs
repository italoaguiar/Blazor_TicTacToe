using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDTI.Models;

namespace TesteDTI.Data
{
    /// <summary>
    /// Control all game rooms
    /// </summary>
    public class GameService
    {
        /// <summary>
        /// List of all game rooms
        /// </summary>
        public IList<GameRoom> GameRooms { get; private set; }


        private static Random random = new Random();

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <returns>Object representing the created game</returns>
        public GameRoom NewGame()
        {
            GameRoom gr = new GameRoom()
            {
                Id = Guid.NewGuid(),
                FirstPlayer = (Player)random.Next(0, 1)
            };

            GameRooms.Add(gr);

            return gr;
        }





        /// <summary>
        /// Perform player movement
        /// </summary>
        /// <param name="m">The player movement</param>
        public GameResult MakeMovement(Movement m)
        {
            GameRoom gr = GameRooms.FirstOrDefault(x => x.Id == m.Id);

            //check if the game exists
            if (gr == null)
            {
                throw new ArgumentException("Partida não encontrada.");
            }

            //inverts the y coordinate
            int y = (m.Position.Y * -1) + 2;

            //check if the coordinate is empty
            if (gr.Board[m.Position.X,y] != null)
            {
                throw new InvalidOperationException("Posição não preenchida");
            }

            //check if the player is on the correct turn
            if (gr.FirstPlayer != m.Player)
            {
                throw new InvalidOperationException("Não é o turno do jogador");
            }

            //save movement on board
            gr.Board[m.Position.X, y] = m;

            gr.FirstPlayer = TogglePlayer(gr.FirstPlayer);


            //Player O won
            if (CheckWinner(gr, Player.O))
            {
                return new GameResult()
                {
                    IsEnded = true,
                    Winner = Player.O
                };
            }

            //Player X won
            if (CheckWinner(gr, Player.X))
            {
                return new GameResult()
                {
                    IsEnded = true,
                    Winner = Player.O
                };
            }

            return new GameResult();
        }




        /// <summary>
        /// Analyze the game board looking for a winner
        /// </summary>
        /// <param name="gr">The game room</param>
        /// <param name="p">The Player</param>
        /// <returns>Value indicating whether the informed player won the game.</returns>
        private bool CheckWinner(GameRoom gr, Player p)
        {
            int v = 0, h = 0;

            //check horizontal and vertical coordinates
            for (int i = 0; i< 2; i++)
            {
                for(int j = 0; j< 2; j++)
                {
                    if (gr.Board[i, j].Player == p)
                        v++;
                    if (gr.Board[j, i].Player == p)
                        v++;
                }
                if (v == 2 || h == 2)
                    return true;

                v = h = 0;
            }

            //check diagonal coordinates
            for(int i = 0; i< 2; i++)
            {
                if (gr.Board[i, i].Player == p)
                    v++;
                if (gr.Board[i, (i * -1) + 2].Player == p)
                    h++;
            }
            if (v == 2 || h == 2)
                return true;

            return false;
        }

        /// <summary>
        /// Returns the inverse of the entered value
        /// </summary>
        /// <param name="p">The Player</param>
        /// <returns>Inverted Player Value</returns>
        private Player TogglePlayer(Player p)
        {
            return p == Player.X ? Player.O : Player.X;
        }
    }
}
