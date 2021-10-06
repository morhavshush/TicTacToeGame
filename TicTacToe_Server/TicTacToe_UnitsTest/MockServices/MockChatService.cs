using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;
using TicTcToe_Service.Interfaces;

namespace TicTacToe_UnitsTest.MockServices
{
    class MockChatService : IChatService
    {
        List<User> _users;
        public List<Message> Messages;
        public MockChatService()
        {
            var user1 = new User { Id = 1, UserName = "alon", Password = "123", ConnectionId = "123" };
            var user2 = new User { Id = 2, UserName = "roni", Password = "345", ConnectionId = "345" };
            var user3 = new User { Id = 3, UserName = "adi", Password = "567", ConnectionId = "567" };
            var user4 = new User { Id = 4, UserName = "gali", Password = "789", ConnectionId = null };
            _users = new List<User>();
            _users.Add(user1);
            _users.Add(user2);
            _users.Add(user3);
            _users.Add(user4);
            Messages = new List<Message>
            {
                new Message { Id=1, MessageText="hi good morning", Reciever=user1, Sender=user2, TimeSent=DateTime.Now},
                new Message { Id=2, MessageText="how are you?", Reciever=user2, Sender=user1, TimeSent=DateTime.Now},
                new Message { Id=3, MessageText="shabat shalom", Reciever=user2, Sender=user3, TimeSent=DateTime.Now}
            };

        }
        public async Task<IEnumerable<Message>> GetChatHistoriesOfTwoUsers(string userName1, string userName2)
        {
            return Messages.Where(m => m.Reciever.UserName == userName1 && m.Sender.UserName == userName2 ||
                m.Reciever.UserName == userName2 && m.Sender.UserName == userName1).OrderBy(m => m.TimeSent);
        }

        public async Task<Message> SaveDirectMessage(string sender, string reciever, string message)
        {
            User senderUser = _users.FirstOrDefault(u => u.UserName == sender);
            User recieverUser = _users.FirstOrDefault(u => u.UserName == reciever);
            if (senderUser != null && recieverUser != null && message != null)
            {
                Message msg = new Message { Reciever = recieverUser, Sender = senderUser, MessageText = message, TimeSent = DateTime.Now };
                Messages.Add(msg);
                return msg;
            }
            return null;

        }
    }
}
