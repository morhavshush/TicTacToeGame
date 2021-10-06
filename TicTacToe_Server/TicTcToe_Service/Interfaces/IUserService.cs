using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTcToe_Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserExist(string userName, string password);
        Task<IEnumerable<User>> GetAllDisconnectedUsers();
        Task<IEnumerable<User>> GetAllConnectedUsers();
        Task<User> GetUserByName(string userName);
        Task<User> CanLogin(string userName, string password);
        Task<bool> AddNewUser(string userName, string password, string connectionId);
        Task<bool> UpdateConnectionId(string userName, string password, string connectionId);
        Task<bool> IsUserNameExist(string userName);
        Task<bool> UserLogout(string userName, string password);
        Task<User> GetUserByConnectionId(string connectionId);
        Task<User> Registeration(string userName, string password);
    }
}
