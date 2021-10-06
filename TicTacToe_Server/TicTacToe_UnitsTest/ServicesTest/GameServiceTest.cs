using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;
using TicTacToe_Models.Game_Models;
using TicTcToe_Service.Services;

namespace TicTacToe_UnitsTest.ServicesTest
{
    [TestClass]
    public class GameServiceTest
    {
        private GameService _gameService;
        private TicTacToe _ticTac;

        [TestInitialize]
        public void Init()
        {
            _gameService = new GameService();
            _ticTac = new TicTacToe();
        }

        [TestMethod]
        public void TestInitGame()
        {
            _ticTac = _gameService.InitGame(new User() { UserName = "moshe", Password = "123", ConnectionId = "123" }, new User() { UserName = "kobi", Password = "123", ConnectionId = "321" });
            Assert.IsNotNull(_ticTac);
        }


        [TestMethod]
        public void TestInitGame1()
        {
            _ticTac = _gameService.InitGame(new User() { UserName = "moshe", Password = "123", ConnectionId = "123" }, new User() { UserName = "kobi", Password = "123", ConnectionId = "321" });
            Assert.AreEqual(_ticTac.Player1.ConnectionId, "123");
        }

        [TestMethod]
        public void TestCheckWinner()
        {
            _ticTac.Fields[0] = _ticTac.Fields[1] = _ticTac.Fields[2] = "123";
            Assert.IsTrue(_gameService.CheckWinner(_ticTac));
        }

        [TestMethod]
        public void TestCheckWinner1()
        {
            _ticTac.Fields[0] = _ticTac.Fields[1] = _ticTac.Fields[3] = "123";
            Assert.IsFalse(_gameService.CheckWinner(_ticTac));
        }
    }
}
