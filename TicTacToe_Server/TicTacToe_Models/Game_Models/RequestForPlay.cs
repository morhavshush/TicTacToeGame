using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Models.Game_Models
{
    public class RequestForPlay
    {
        public string OfferUserName { get; set; }
        public string RespondUserName { get; set; }
        public bool IsAgree { get; set; }
    }
}
