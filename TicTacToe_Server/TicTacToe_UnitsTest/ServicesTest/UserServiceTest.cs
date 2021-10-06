using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe_Data;
using TicTacToe_Models.DB_Models;
using TicTcToe_Service.Services;

namespace TicTacToe_UnitsTest.ServicesTest
{
    [TestClass]
    public class UserServiceTest
    {
        UserService _userService;

        [TestInitialize]
        public void Init()
        {
            DbContextOptions<DB_Context> _dbContextOptions = new DbContextOptionsBuilder<DB_Context>().UseInMemoryDatabase(databaseName: "TestDB1").Options;

            DB_Context myContext = new DB_Context(_dbContextOptions);

            _userService = new UserService(myContext);

            using (myContext = new DB_Context(_dbContextOptions))
            {
                myContext.Database.EnsureDeleted();
                myContext.Database.EnsureCreated();
                myContext.Users.Add(new User { Id = 1, UserName = "alon", Password = "123", ConnectionId = "123" });
                myContext.Users.Add(new User { Id = 2, UserName = "roni", Password = "345", ConnectionId = "345" });
                myContext.Users.Add(new User { Id = 3, UserName = "adi", Password = "567", ConnectionId = "567" });
                myContext.Users.Add(new User { Id = 4, UserName = "gali", Password = "789", ConnectionId = null });
                myContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            DbContextOptions<DB_Context> _dbContextOptions = new DbContextOptionsBuilder<DB_Context>().UseInMemoryDatabase(databaseName: "TestDB1").Options;

            DB_Context myContext = new DB_Context(_dbContextOptions);

            _userService = new UserService(myContext);

            using (myContext)
            {
                myContext.Database.EnsureDeleted();
                myContext.SaveChanges();
            }
        }

        [TestMethod]
        public async Task TestAddNewUser()
        {
            var isAdd = await _userService.AddNewUser("mor", "123", "abc");
            Assert.IsTrue(isAdd);
        }

        [TestMethod]
        public async Task TestAddNewUser1()
        {
            var beforeAdd = await _userService.GetAllConnectedUsers();
            await _userService.AddNewUser("dana", "123", "abc");
            var afterAdd = await _userService.GetAllConnectedUsers();
            Assert.AreEqual(beforeAdd.ToList().Count + 1, afterAdd.ToList().Count);
        }

        [TestMethod]
        public async Task TestIsUserExist()
        {
            var isUserExist = await _userService.IsUserExist("alon", "123");
            Assert.IsTrue(isUserExist);
        }

        [TestMethod]
        public async Task TestIsUserExist1()
        {
            var isUserExist = await _userService.IsUserExist("alon", "345");
            Assert.IsFalse(isUserExist);
        }

        [TestMethod]
        public async Task TestCanLogin()
        {
            var user = await _userService.CanLogin("gali", "789");
            Assert.IsNotNull(user);

        }

        [TestMethod]
        public async Task TestCanLogin1()
        {
            var user = await _userService.CanLogin("gali", "987");
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public async Task TestCanLogin2()
        {
            var user = await _userService.CanLogin("kjdufbivf", "123");
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public async Task TestGetAllConnectedUsers()
        {
            var users = await _userService.GetAllConnectedUsers();
            Assert.AreEqual(users.ToList().Count, 3);
        }

        [TestMethod]
        public async Task TestGetAllConnectedUsers1()
        {
            var users = await _userService.GetAllConnectedUsers();
            Assert.AreNotEqual(users.ToList().Count, 0);
        }

        [TestMethod]
        public async Task TestGetAllDisconnectedUsers()
        {
            var users = await _userService.GetAllDisconnectedUsers();
            Assert.AreEqual(users.ToList().Count, 1);
        }

        [TestMethod]
        public async Task TestGetAllDisconnectedUsers1()
        {
            var users = await _userService.GetAllDisconnectedUsers();
            Assert.AreNotEqual(users.ToList().Count, 0);
        }

        [TestMethod]
        public async Task TestGetUserByConnectionId1()
        {
            var user = await _userService.GetUserByConnectionId("345");
            Assert.AreNotEqual(user.UserName, "adi");
        }

        [TestMethod]
        public async Task TestGetUserByName1()
        {
            var user = await _userService.GetUserByName("alona");
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task TestIsUserNameExist()
        {
            var user = await _userService.IsUserNameExist("alona");
            Assert.IsFalse(user);
        }

        [TestMethod]
        public async Task TestIsUserNameExist1()
        {
            var user = await _userService.IsUserNameExist("gali");
            Assert.IsTrue(user);
        }

        [TestMethod]
        public async Task TestUpdateConnectionId()
        {
            var isUpdate = await _userService.UpdateConnectionId("gali", "789", "111");
            Assert.IsTrue(isUpdate);
        }

        [TestMethod]
        public async Task TestUpdateConnectionId1()
        {
            var isUpdate = await _userService.UpdateConnectionId("gali", "987", "111");
            Assert.IsFalse(isUpdate);
        }

        [TestMethod]
        public async Task TestUserLogout()
        {
            var isLogout = await _userService.UserLogout("roni", "345");
            Assert.IsTrue(isLogout);
        }

        [TestMethod]
        public async Task TestUserLogout1()
        {
            var isLogout = await _userService.UserLogout("roni", "543");
            Assert.IsFalse(isLogout);
        }
    }
}
