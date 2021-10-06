using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Data;
using TicTacToe_Models.DB_Models;
using TicTcToe_Service.Services;

namespace TicTacToe_UnitsTest.ServicesTest
{
    [TestClass]
    public class ChatServiceTest
    {
        ChatService _chatService;

        [TestInitialize]
        public void Init()
        {
            DbContextOptions<DB_Context> _dbContextOptions = new DbContextOptionsBuilder<DB_Context>()
           .UseInMemoryDatabase(databaseName: "TestDB").Options;

            DB_Context _myContext = new DB_Context(_dbContextOptions);

            _chatService = new ChatService(_myContext);

            using (_myContext = new DB_Context(_dbContextOptions))
            {
                var user1 = new User { Id = 1, UserName = "alon", Password = "123", ConnectionId = "123" };
                var user2 = new User { Id = 2, UserName = "roni", Password = "345", ConnectionId = "345" };
                var user3 = new User { Id = 3, UserName = "adi", Password = "567", ConnectionId = "567" };
                var user4 = new User { Id = 4, UserName = "gali", Password = "789", ConnectionId = null };
                _myContext.Users.Add(user1);
                _myContext.Users.Add(user2);
                _myContext.Users.Add(user3);
                _myContext.Users.Add(user4);

                _myContext.Messages.Add(new Message { Id = 1, MessageText = "hi good morning", Reciever = user1, Sender = user2, TimeSent = DateTime.Now });
                _myContext.Messages.Add(new Message { Id = 2, MessageText = "how are you?", Reciever = user2, Sender = user1, TimeSent = DateTime.Now });
                _myContext.Messages.Add(new Message { Id = 3, MessageText = "shabat shalom", Reciever = user2, Sender = user3, TimeSent = DateTime.Now });
                _myContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            DbContextOptions<DB_Context> _dbContextOptions = new DbContextOptionsBuilder<DB_Context>()
              .UseInMemoryDatabase(databaseName: "TestDB").Options;

            DB_Context _myContext = new DB_Context(_dbContextOptions);

            _chatService = new ChatService(_myContext);

            using (_myContext = new DB_Context(_dbContextOptions))
            {
                _myContext.Database.EnsureDeleted();
                _myContext.SaveChanges();
            }
        }

        [TestMethod]
        public async Task TestGetChatHistoriesOfTwoUsers()
        {
            var messagesCount = await _chatService.GetChatHistoriesOfTwoUsers("alon", "gali");
            Assert.AreEqual(messagesCount.ToList().Count, 0);
        }

        [TestMethod]
        public async Task TestGetChatHistoriesOfTwoUsers1()
        {
            var messagesCount = await _chatService.GetChatHistoriesOfTwoUsers("alon", "roni");
            Assert.AreEqual(messagesCount.ToList().Count, 2);
        }

        [TestMethod]
        public async Task TestGetChatHistoriesOfTwoUsers2()
        {
            var messagesCount = await _chatService.GetChatHistoriesOfTwoUsers("adi", "roni");
            Assert.AreEqual(messagesCount.ToList().Count, 1);
        }

        [TestMethod]
        public async Task TestSaveDirectMessage()
        {
            var message = await _chatService.SaveDirectMessage("adi", "roni", "sela");
            Assert.AreNotEqual(message, null);
            Assert.AreEqual(message.MessageText, "sela");
        }

        [TestMethod]
        public async Task TestSaveDirectMessage1()
        {
            //dana isn't user in database
            var message = await _chatService.SaveDirectMessage("adi", "dana", "sela");
            Assert.IsNull(message);
        }
    }
}
