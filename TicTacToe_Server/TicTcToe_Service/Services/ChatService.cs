using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Data;
using TicTacToe_Models.DB_Models;
using TicTcToe_Service.Interfaces;

namespace TicTcToe_Service.Services
{
    public class ChatService : IChatService
    {
        private readonly DB_Context _context;
        public ChatService(DB_Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetChatHistoriesOfTwoUsers(string userName1, string userName2)
        {
            return await _context.Messages.Include("Reciever").Include("Sender").Where(m => m.Reciever.UserName == userName1 && m.Sender.UserName == userName2 ||
                m.Reciever.UserName == userName2 && m.Sender.UserName == userName1).OrderBy(m => m.TimeSent).ToListAsync();
        }

        public async Task<Message> SaveDirectMessage(string sender, string reciever, string message)
        {
            User senderUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == sender);
            User recieverUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == reciever);
            if (senderUser != null && recieverUser != null && message != null)
            {
                Message msg = new Message { Reciever = recieverUser, Sender = senderUser, MessageText = message, TimeSent = DateTime.Now };
                _context.Messages.Add(msg);
                await _context.SaveChangesAsync();
                return msg;
            }
            return null;


        }
    }
}
