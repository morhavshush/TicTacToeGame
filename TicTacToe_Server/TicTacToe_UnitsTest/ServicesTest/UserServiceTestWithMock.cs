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
    public class UserServiceTestWithMock
    {
        private MockUserService _userService = new MockUserService();

        [TestMethod]
        public async void TestAddNewUser()
        {
            var beforeAdd = _userService.Users.Count;
            await _userService.AddNewUser("mor", "123", "abc");
            var afterAdd = _userService.Users.Count;
            Assert.AreEqual(beforeAdd + 1, afterAdd);
        }

        [TestMethod]
        public void TestAddNewUser1()
        {
            var isAdd = _userService.AddNewUser("mor", "123", "abc").Result;
            Assert.IsTrue(isAdd);
        }

        [TestMethod]
        public void TestIsUserExist()
        {
            var isUserExist = _userService.IsUserExist("alon", "123").Result;
            Assert.IsTrue(isUserExist);
        }

        [TestMethod]
        public void TestIsUserExist1()
        {
            var isUserExist = _userService.IsUserExist("alon", "345").Result;
            Assert.IsFalse(isUserExist);
        }

        [TestMethod]
        public void TestCanLogin()
        {
            var user = _userService.CanLogin("gali", "789").Result;
            Assert.IsNotNull(user);

        }

        [TestMethod]
        public void TestCanLogin1()
        {
            var user = _userService.CanLogin("gali", "987").Result;
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void TestCanLogin2()
        {
            var user = _userService.CanLogin("alon", "123").Result;
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void TestGetAllConnectedUsers()
        {
            var users = _userService.GetAllConnectedUsers().Result.ToList().Count;
            Assert.AreEqual(users, 3);
        }

        [TestMethod]
        public void TestGetAllConnectedUsers1()
        {
            var users = _userService.GetAllConnectedUsers().Result.ToList().Count;
            Assert.AreNotEqual(users, 0);
        }

        [TestMethod]
        public void TestGetAllDisconnectedUsers()
        {
            var users = _userService.GetAllDisconnectedUsers().Result.ToList().Count;
            Assert.AreEqual(users, 1);
        }

        [TestMethod]
        public void TestGetAllDisconnectedUsers1()
        {
            var users = _userService.GetAllDisconnectedUsers().Result.ToList().Count;
            Assert.AreNotEqual(users, 0);
        }

        [TestMethod]
        public void TestGetUserByConnectionId()
        {
            var user = _userService.GetUserByConnectionId("567").Result;
            Assert.AreEqual(user.UserName, "adi");
        }

        [TestMethod]
        public void TestGetUserByConnectionId1()
        {
            var user = _userService.GetUserByConnectionId("345").Result;
            Assert.AreNotEqual(user.UserName, "adi");
        }

        [TestMethod]
        public void TestGetUserByName()
        {
            var user = _userService.GetUserByName("alon").Result;
            Assert.AreEqual(user.ConnectionId, "123");
        }

        [TestMethod]
        public void TestGetUserByName1()
        {
            var user = _userService.GetUserByName("alona").Result;
            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestIsUserNameExist()
        {
            var user = _userService.IsUserNameExist("alona").Result;
            Assert.IsFalse(user);
        }

        [TestMethod]
        public void TestIsUserNameExist1()
        {
            var user = _userService.IsUserNameExist("gali").Result;
            Assert.IsTrue(user);
        }

        [TestMethod]
        public void TestUpdateConnectionId()
        {
            var isUpdate = _userService.UpdateConnectionId("gali", "789", "111").Result;
            Assert.IsTrue(isUpdate);
        }

        [TestMethod]
        public void TestUpdateConnectionId1()
        {
            var isUpdate = _userService.UpdateConnectionId("gali", "987", "111").Result;
            Assert.IsFalse(isUpdate);
        }

        [TestMethod]
        public void TestUserLogout()
        {
            var isLogout = _userService.UserLogout("roni", "345").Result;
            Assert.IsTrue(isLogout);
        }

        [TestMethod]
        public void TestUserLogout1()
        {
            var isLogout = _userService.UserLogout("roni", "543").Result;
            Assert.IsFalse(isLogout);
        }

    }
}
