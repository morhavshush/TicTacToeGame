using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_UnitsTest.MockServices;

namespace TicTacToe_UnitsTest.ServicesTest
{
    [TestClass]
    public class ChatServiceTestWithMock
    {
        private MockChatService _chatService = new MockChatService();

        [TestMethod]
        public void TestGetChatHistoriesOfTwoUsers()
        {
            var messagesCount = _chatService.GetChatHistoriesOfTwoUsers("alon", "gali").Result.ToList().Count;
            Assert.AreEqual(messagesCount, 0);
        }

        [TestMethod]
        public void TestGetChatHistoriesOfTwoUsers1()
        {
            var messagesCount = _chatService.GetChatHistoriesOfTwoUsers("alon", "roni").Result.ToList().Count;
            Assert.AreEqual(messagesCount, 2);
        }

        [TestMethod]
        public void TestGetChatHistoriesOfTwoUsers2()
        {
            var messagesCount = _chatService.GetChatHistoriesOfTwoUsers("adi", "roni").Result.ToList().Count;
            Assert.AreEqual(messagesCount, 1);
        }

        [TestMethod]
        public void TestSaveDirectMessage()
        {
            var beforeAdd = _chatService.Messages.Count;
            _chatService.SaveDirectMessage("adi", "roni", "sela");
            var afterAdd = _chatService.Messages.Count;
            Assert.AreEqual(beforeAdd + 1, afterAdd);
        }

        [TestMethod]
        public void TestSaveDirectMessage1()
        {
            var message = _chatService.SaveDirectMessage("adi", "dana", "sela").Result;
            Assert.AreEqual(message, null);
        }

    }
}
