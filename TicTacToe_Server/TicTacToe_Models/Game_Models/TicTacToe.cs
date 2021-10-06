using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTacToe_Models.Game_Models
{
    public class TicTacToe
    {
        public int Id { get; set; }

        public bool IsGameOver { get; set; }

        public virtual User Player1 { get; set; }

        public virtual User Player2 { get; set; }

        public virtual string Winner { get; set; }

        public string[] Fields { get; set; }

        public int MovesLeft { get; set; }

        public string UserNameTurn { get; set; }


        public TicTacToe()
        {
            MovesLeft = 9;
            Fields = new string[9];
            // Reset game
            for (var i = 0; i < Fields.Length; i++)
            {
                Fields[i] = "-";
            }
        }


    }
}
