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
    public class UserService : IUserService
    {
        private readonly DB_Context _context;
        public UserService(DB_Context context)
        {
            _context = context;
            Init();
        }

        private async Task Init()
        {
            try
            {
                var users = GetAllDisconnectedUsers().Result.ToList();
                if (users.Count == 0)
                {
                    await Registeration("Mor", "12345678");
                    await Registeration("Moshe", "12345678");
                    await Registeration("David", "12345678");
                    await Registeration("Liri", "12345678");
                }
            }
            catch (Exception ex) { throw new Exception("Data error in IsUserNameExist", ex); }
        }

        public async Task<bool> IsUserNameExist(string userName)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                return existUser != null;
            }
            catch (Exception ex) { throw new Exception("Data error in IsUserNameExist", ex); }

        }
        public async Task<bool> IsUserExist(string userName, string password)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
                return existUser != null;
            }
            catch (Exception ex) { throw new Exception("Data error in IsUserExist", ex); }

        }
        public async Task<IEnumerable<User>> GetAllConnectedUsers()
        {
            try
            {
                var users = await _context.Users.Where(u => u.ConnectionId != null).ToListAsync();
                return users;
            }
            catch (Exception ex) { throw new Exception("Data error in GetAllConnectedUsers", ex); }
        }
        public async Task<IEnumerable<User>> GetAllDisconnectedUsers()
        {
            try
            {
                var users = await _context.Users.Where(u => u.ConnectionId == null).ToListAsync();
                return users;
            }
            catch (Exception ex) { throw new Exception("Data error in GetAllDisconnectedUsers", ex); }

        }

        public async Task<User> GetUserByName(string userName)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            }
            catch (Exception ex) { throw new Exception("Data error in GetUserByName", ex); }

        }

        public async Task<User> CanLogin(string userName, string password)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
            }
            catch (Exception ex) { throw new Exception("Data error in CanLogin", ex); }

        }

        public async Task<bool> AddNewUser(string userName, string password, string connectionId)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
                if (existUser == null)
                {
                    User user = new User { ConnectionId = connectionId, UserName = userName, Password = password };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { throw new Exception("Data error in AddNewUser", ex); }

        }

        public async Task<User> Registeration(string userName, string password)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
                if (existUser == null)
                {
                    User user = new User { UserName = userName, Password = password };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
                else return null;
            }
            catch (Exception ex) { throw new Exception("Data error in AddNewUser", ex); }

        }

        public async Task<bool> UpdateConnectionId(string userName, string password, string connectionId)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.ConnectionId == null);
                if (existUser != null)
                {
                    existUser.ConnectionId = connectionId;
                    _context.Users.Update(existUser);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { throw new Exception("Data error in UpdateConnectionId", ex); }
        }


        public async Task<bool> UserLogout(string userName, string password)
        {
            try
            {
                User existUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.ConnectionId != null);
                if (existUser != null)
                {
                    existUser.ConnectionId = null;
                    _context.Users.Update(existUser);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { throw new Exception("Data error in UserLogout", ex); }

        }

        public async Task<User> GetUserByConnectionId(string connectionId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.ConnectionId == connectionId);
            }
            catch (Exception ex) { throw new Exception("Data error in GetUserByName", ex); }
        }
    }
}
