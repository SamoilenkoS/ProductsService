using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ProductsPresentationLayer
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            if (message.StartsWith('/'))
            {
                message = message[1..];
                var splitted = message.Split(' ');
                switch (splitted[0])
                {
                    case "msg":
                        if(splitted.Length > 2)
                        {
                            var id = splitted[1];
                            var personalMessage = string.Join(' ', splitted[2..]);
                            await Clients.Client(id).SendAsync("ReceiveMessage", Context.ConnectionId, personalMessage);
                        }
                        break;
                }
            }
            else
            {
                await Clients.Others.SendAsync("ReceiveMessage", user, message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("ReceiveMessage", "SYSTEM", $"User {Context.ConnectionId} connected!");
            await Clients.Caller.SendAsync("ReceiveMessage", "SYSTEM", $"Greetings newcomer!");
        }
    }
}
