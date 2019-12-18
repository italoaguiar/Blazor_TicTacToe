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

        public GameService()
        {
            GameRooms = new List<GameRoom>();
        }

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

            var result = CheckWinner(gr);
            
            return new GameResult()
            {
                IsEnded = result != null,
                Winner = result
            };
        }




        /// <summary>
        /// Analyze the game board looking for a winner
        /// </summary>
        /// <param name="gr">The game room</param>
        /// <returns>Value indicating whether the informed player won the game.</returns>
        private Player? CheckWinner(GameRoom gr)
        {
            int vO = 0, hO = 0, vX = 0, hX = 0, d = 0;

            //check horizontal and vertical coordinates
            for (int i = 0; i< 2; i++)
            {
                for(int j = 0; j< 2; j++)
                {
                    if (gr.Board[i, j] != null)
                    {
                        if (gr.Board[i, j].Player == Player.O)
                            vO++;                        
                        else if (gr.Board[i, j].Player == Player.X)
                            vX++;                        
                        d++;
                    }
                    else if (gr.Board[j, i] != null)
                    {
                        if (gr.Board[j, i].Player == Player.O)
                            hO++;
                        else if (gr.Board[j, i].Player == Player.X)
                            hX++;
                    }
                }
                if (vX == 2 || hX == 2)
                    return Player.X;

                if (vO == 2 || hO == 2)
                    return Player.O;
                
                vX = hX = vO = hO = 0; //clear
            }

            if (d == 9)
                return Player.Draw;

            //check diagonal coordinates
            for(int i = 0; i< 2; i++)
            {
                if (gr.Board[i, i] != null)
                {
                    if (gr.Board[i, i].Player == Player.O)
                        vO++;                    
                    else if (gr.Board[i, i].Player == Player.X)
                        vX++;
                    
                }
                else if(gr.Board[i, (i * -1) + 2] != null)
                {
                    if (gr.Board[i, (i * -1) + 2].Player == Player.X)
                        hX++;
                    else if (gr.Board[i, (i * -1) + 2].Player == Player.O)
                        hO++;
                }
            }
            if (vX == 3 || hX == 3)
                return Player.X;

            if (vO == 3 || hO == 3)
                return Player.O;

            return null;
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
