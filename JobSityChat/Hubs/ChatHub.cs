using JobSityChat.Extensions;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JobSityChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IAspNetUser _aspNetUser;
        public ChatHub(IAspNetUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", _aspNetUser.ObterUserEmail(), message);
        }
    }
}
