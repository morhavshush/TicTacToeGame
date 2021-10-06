using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;
using TicTacToe_Models.Game_Models;

namespace TicTcToe_Service.Interfaces
{
    public interface IGameService
    {
        TicTacToe InitGame(User userName1, User userName2);
        bool CheckWinner(TicTacToe game);
    }
}
