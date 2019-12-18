using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDTI.Models;

namespace TesteDTI.Data
{
    public class GameService
    {
        public IList<GameRoom> GameRooms { get; private set; }


        private static Random random = new Random();


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


        public void MakeMovement(Movement m)
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
        }

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
                if (gr.Board[(i * -1) + 2, (i * -1) + 2].Player == p)
                    h++;
            }
            if (v == 2 || h == 2)
                return true;

            return false;
        }
    }
}
