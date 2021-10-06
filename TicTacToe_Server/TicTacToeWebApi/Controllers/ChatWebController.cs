using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;
using TicTcToe_Service.Interfaces;

namespace TicTacToeWebApi.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatWebController : ControllerBase
    {
        private readonly IUserService _userService;

        public ChatWebController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("")]//path looks like this: https://localhost:44379/api/chat
        public IActionResult Start()
        {
            return Ok("App is Up!");
        }


        [Route("login")]//path looks like this: https://localhost:44379/api/chat/login
        [HttpPost]
        public async Task<User> Login([FromBody] User user)
        {
            var user1 = await _userService.CanLogin(user.UserName, user.Password);
            return user1;
        }

        [Route("register")]//path looks like this: https://localhost:44379/api/chat/login
        [HttpPost]
        public async Task<User> Register([FromBody] User user)
        {
            var user1= await _userService.Registeration(user.UserName, user.Password);
            return user1;
        }


    }
}
