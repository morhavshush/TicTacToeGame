using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTacToe_Models.Game_Models
{
    public class GameBoard
    {
        public int Id { get; set; }
        public TicTacToe TicTacToeGame { get; set; }

        public GameBoard()
        {
            TicTacToeGame = new TicTacToe();
        }
        public TicTacToe InitGame(User user1, User user2)
        {
            TicTacToeGame.Player1 = new User();
            TicTacToeGame.Player1 = user1;
            TicTacToeGame.Player1.Type = "X";
            TicTacToeGame.Player2 = new User();
            TicTacToeGame.Player2 = user2;
            TicTacToeGame.Player2.Type = "O";
            TicTacToeGame.UserNameTurn = user1.UserName;
            return TicTacToeGame;
        }

        public bool CheckWinner(TicTacToe game)
        {
            for (int i = 0; i < 3; i++)
            {
                if (
                    ((game.Fields[i * 3] != "-" && game.Fields[(i * 3)] == game.Fields[(i * 3) + 1] && game.Fields[(i * 3)] == game.Fields[(i * 3) + 2]) ||
                     (game.Fields[i] != "-" && game.Fields[i] == game.Fields[i + 3] && game.Fields[i] == game.Fields[i + 6])))
                {
                    return true;
                }
            }

            if ((game.Fields[0] != "-" && game.Fields[0] == game.Fields[4] && game.Fields[0] == game.Fields[8]) || (game.Fields[2] != "-" && game.Fields[2] == game.Fields[4] && game.Fields[2] == game.Fields[6]))
            {
                return true;
            }
            return false;
        }

    }
}
