using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe_Models.Game_Models;
using TicTcToe_Service.Interfaces;

namespace TicTacToeWebApi.Hubs
{
    public class ChatHub : Hub
    {

        private IGameService _gameService;
        private IChatService _chatService;
        private IUserService _userService;
        TicTacToe game;

        public ChatHub(IChatService chatService, IUserService userService, IGameService gameService)
        {
            _chatService = chatService;
            _userService = userService;
            _gameService = gameService;
        }

        public void NewContosoChatMessage(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }

        public async Task GetHistoryChat(string sender, string reciever)
        {
            var historyChat = await _chatService.GetChatHistoriesOfTwoUsers(sender, reciever);
            await Clients.Caller.SendAsync("AllMessages", historyChat);
        }

        public async Task SendMessage(string sender, string message, string reciever)
        {
            var messageModel = await _chatService.SaveDirectMessage(sender, reciever, message);
            var userInfoReciever = await _userService.GetUserByName(reciever);
            var historyChat = await _chatService.GetChatHistoriesOfTwoUsers(sender, reciever);
            await Clients.Client(userInfoReciever.ConnectionId).SendAsync("Message", messageModel);
            await Clients.Caller.SendAsync("AllMessages", historyChat);
        }
        public async Task ConnectUserToChat(string userName, string password)
        {
            var connectedId = Context.ConnectionId;
            var isUpdate = await _userService.UpdateConnectionId(userName, password, connectedId);
            if (isUpdate)
            {
                var connected = await _userService.GetAllConnectedUsers();
                await Clients.All.SendAsync("ConnectedUsers", connected);

                var disconnected = await _userService.GetAllDisconnectedUsers();
                await Clients.All.SendAsync("DisconnectedUsers", disconnected);
            }
        }
        public async Task Logout(string userName, string password)
        {
            var isLogout = await _userService.UserLogout(userName, password);
            if (isLogout)
            {
                var connected = await _userService.GetAllConnectedUsers();
                await Clients.All.SendAsync("ConnectedUsers", connected);

                var disconnected = await _userService.GetAllDisconnectedUsers();
                await Clients.All.SendAsync("DisconnectedUsers", disconnected);
            }
        }
        public async override Task<Task> OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = await _userService.GetUserByConnectionId(Context.ConnectionId);
                if (user != null)
                {
                    var isLogout = await _userService.UserLogout(user.UserName, user.Password);
                    if (isLogout)
                    {
                        var connected = await _userService.GetAllConnectedUsers();
                        await Clients.All.SendAsync("ConnectedUsers", connected);

                        var disconnected = await _userService.GetAllDisconnectedUsers();
                        await Clients.All.SendAsync("DisconnectedUsers", disconnected);
                    }
                }
            }
            catch (Exception e)
            {
            }
            return base.OnDisconnectedAsync(exception);

        }

        public async Task InitGame(RequestForPlay req)
        {
            var offerUser = await _userService.GetUserByName(req.OfferUserName);
            var respondUser = await _userService.GetUserByName(req.RespondUserName);
            game = _gameService.InitGame(offerUser, respondUser);
            await Clients.Clients(offerUser.ConnectionId, respondUser.ConnectionId).SendAsync("StartPlay", game);
        }

        public async Task AskForPlay(string respondUserName, string offerUserName)
        {
            var userSend = await _userService.GetUserByName(offerUserName);
            var respondUser = await _userService.GetUserByName(respondUserName);
            if (userSend != null && respondUser != null)
            {
                RequestForPlay req = new RequestForPlay { RespondUserName = respondUser.UserName, OfferUserName = userSend.UserName };
                await Clients.Client(respondUser.ConnectionId).SendAsync("AskForPlay", req);
            }
        }
        public async Task RespondForPlay(RequestForPlay req)
        {
            var respondUser = await _userService.GetUserByName(req.RespondUserName);
            var offerUser = await _userService.GetUserByName(req.OfferUserName);
            if (respondUser != null && offerUser != null)
            {
                req.RespondUserName = respondUser.UserName;
                await Clients.Client(offerUser.ConnectionId).SendAsync("RespondForPlay", req);
            }
        }

        public async Task UserSendPosition(TicTacToe ticTac)
        {
            var user = await _userService.GetUserByConnectionId(Context.ConnectionId);
            if (ticTac != null || user != null)
            {
                ticTac.MovesLeft -= 1;
                if (ticTac.Player1.UserName == ticTac.UserNameTurn)
                {
                    ticTac.UserNameTurn = ticTac.Player2.UserName;
                }
                else if (ticTac.Player2.UserName == ticTac.UserNameTurn)
                {
                    ticTac.UserNameTurn = ticTac.Player1.UserName;
                }
                if (_gameService.CheckWinner(ticTac))
                {
                    ticTac.Winner = user.UserName;
                    await Clients.Clients(ticTac.Player1.ConnectionId, ticTac.Player2.ConnectionId).SendAsync("Winner", ticTac.Winner);
                }
                await Clients.Clients(ticTac.Player1.ConnectionId, ticTac.Player2.ConnectionId).SendAsync("PlayerMarker", ticTac);
            }
        }

        public async Task UserRegistretion(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new NullReferenceException();
            }
            var connectedId = Context.ConnectionId;
            var isAdd = await _userService.AddNewUser(userName, password, connectedId);
            if (isAdd)
            {
                var connected = await _userService.GetAllConnectedUsers();
                await Clients.All.SendAsync("ConnectedUsers", connected);

                var disconnected = await _userService.GetAllDisconnectedUsers();
                await Clients.All.SendAsync("DisconnectedUsers", disconnected);
            }
            else
            {
                await Clients.Caller.SendAsync("ProblemWithRegister", isAdd);
            }
        }
    }
}
