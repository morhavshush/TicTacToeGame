using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;
using TicTacToe_Models.Game_Models;
using TicTcToe_Service.Interfaces;

namespace TicTcToe_Service.Services
{
    public class GameService : IGameService
    {
        GameBoard _gameBoard;

        public GameService()
        {
            _gameBoard = new GameBoard();
        }

        public bool CheckWinner(TicTacToe game)
        {
            return _gameBoard.CheckWinner(game);
        }

        public TicTacToe InitGame(User user1, User user2)
        {
            return _gameBoard.InitGame(user1, user2);
        }

    }
}
