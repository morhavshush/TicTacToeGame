using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTcToe_Service.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<Message>> GetChatHistoriesOfTwoUsers(string userName1, string userName2);
        Task<Message> SaveDirectMessage(string sender, string reciever, string message);
    }
}
