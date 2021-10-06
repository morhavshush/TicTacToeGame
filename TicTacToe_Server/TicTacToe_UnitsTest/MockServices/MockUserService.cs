using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTacToe_UnitsTest.MockServices
{
    public class MockUserService
    {
        public List<User> Users;
        public MockUserService()
        {
            var user1 = new User { Id = 1, UserName = "alon", Password = "123", ConnectionId = "123" };
            var user2 = new User { Id = 2, UserName = "roni", Password = "345", ConnectionId = "345" };
            var user3 = new User { Id = 3, UserName = "adi", Password = "567", ConnectionId = "567" };
            var user4 = new User { Id = 4, UserName = "gali", Password = "789", ConnectionId = null };
            Users = new List<User>();
            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);
        }
        public async Task<bool> AddNewUser(string userName, string password, string connectionId)
        {
            User existUser = Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (existUser == null)
            {
                User user = new User { ConnectionId = connectionId, UserName = userName, Password = password };
                Users.Add(user);
                return true;
            }
            else return false;
        }

        public async Task<User> CanLogin(string userName, string password)
        {
            return Users.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.ConnectionId == null);
        }

        public async Task<IEnumerable<User>> GetAllConnectedUsers()
        {
            return Users.Where(u => u.ConnectionId != null);
        }

        public async Task<IEnumerable<User>> GetAllDisconnectedUsers()
        {
            return Users.Where(u => u.ConnectionId == null);
        }

        public async Task<User> GetUserByConnectionId(string connectionId)
        {
            return Users.FirstOrDefault(u => u.ConnectionId == connectionId);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName);
        }

        public async Task<bool> IsUserExist(string userName, string password)
        {
            var existUser = Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return existUser != null;
        }

        public async Task<bool> IsUserNameExist(string userName)
        {
            User existUser = Users.FirstOrDefault(u => u.UserName == userName);
            return existUser != null;
        }

        public async Task<bool> UpdateConnectionId(string userName, string password, string connectionId)
        {
            User existUser = Users.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.ConnectionId == null);
            if (existUser != null)
            {
                existUser.ConnectionId = connectionId;
                Users.Remove(existUser);
                Users.Add(existUser);
                return true;
            }
            else return false;
        }

        public async Task<bool> UserLogout(string userName, string password)
        {
            User existUser = Users.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.ConnectionId != null);
            if (existUser != null)
            {
                existUser.ConnectionId = null;
                Users.Remove(existUser);
                Users.Add(existUser);
                return true;
            }
            else return false;
        }
    }
}
