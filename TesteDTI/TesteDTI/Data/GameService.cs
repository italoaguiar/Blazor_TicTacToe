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

        private IList<GameRoom> gameRooms;
        private static Random random = new Random();

        /// <summary>
        /// List of all game rooms
        /// </summary>
        public IList<GameRoom> GameRooms
        {
            get => gameRooms;
            set
            {
                gameRooms = value;
                NotifyDataChanged(); //Notify changes to UI
            }
        }


        //enable access as in a dictionary
        public GameRoom this[string id] => 
            GameRooms.FirstOrDefault(x=> x.Id.ToString() == id);



        /// <summary>
        /// Create a new game
        /// </summary>
        /// <returns>Object representing the created game</returns>
        public GameRoom NewGame()
        {
            GameRoom gr = new GameRoom()
            {
                Id = Guid.NewGuid(),
                FirstPlayer = (Player)random.Next(0, 2)
            };

            GameRooms.Add(gr);

            NotifyDataChanged();

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

            NotifyDataChanged(); //Notify changes to UI

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
            int vertO = 0, horiO = 0, vertX = 0, horiX = 0, draw = 0;

            //check horizontal and vertical coordinates
            for (int i = 0; i< 3; i++)
            {
                for(int j = 0; j< 3; j++)
                {
                    if (gr.Board[i, j] != null)
                    {
                        if (gr.Board[i, j].Player == Player.O)
                            vertO++;                        
                        else if (gr.Board[i, j].Player == Player.X)
                            vertX++;                        
                        draw++;
                    }
                    if (gr.Board[j, i] != null)
                    {
                        if (gr.Board[j, i].Player == Player.O)
                            horiO++;
                        else if (gr.Board[j, i].Player == Player.X)
                            horiX++;
                    }
                }
                if (vertX == 3 || horiX == 3)
                    return Player.X;

                if (vertO == 3 || horiO == 3)
                    return Player.O;
                
                vertX = horiX = vertO = horiO = 0; //clear
            }

            if (draw == 9)
                return Player.Draw;

            //check diagonal coordinates
            for(int i = 0; i< 3; i++)
            {
                if (gr.Board[i, i] != null)
                {
                    if (gr.Board[i, i].Player == Player.O)
                        vertO++;                    
                    else if (gr.Board[i, i].Player == Player.X)
                        vertX++;
                    
                }
                if(gr.Board[i, (i * -1) + 2] != null)
                {
                    if (gr.Board[i, (i * -1) + 2].Player == Player.X)
                        horiX++;
                    else if (gr.Board[i, (i * -1) + 2].Player == Player.O)
                        horiO++;
                }
            }
            if (vertX == 3 || horiX == 3)
                return Player.X;

            if (vertO == 3 || horiO == 3)
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


        public event Action OnChange;

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}
