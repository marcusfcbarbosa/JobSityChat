using JobSityChat.Extensions;
using JobSityChat.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JobSityChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IStooqService _stooqService;
        public ChatHub(IAspNetUser aspNetUser, IStooqService stooqService)
        {
            _aspNetUser = aspNetUser;
            _stooqService = stooqService;
        }
        public async Task SendMessage(string user, string message)
        {
            if (message.Contains("stock"))
            {
                string[] stockDetail = message.Split("=");
                await Clients.All.SendAsync("ReceiveMessage", _aspNetUser.ObterUserEmail(), await _stooqService.GetStoqQuote(stockDetail[1]));
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", _aspNetUser.ObterUserEmail(), message);
            }
        }
    }
}
