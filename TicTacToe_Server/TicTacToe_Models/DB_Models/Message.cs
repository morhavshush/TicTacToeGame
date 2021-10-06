using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Models.DB_Models
{
    public class Message
    {
        public int Id { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
