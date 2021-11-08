using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProductsBusinessLayer;
using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System;
using System.Threading.Tasks;

namespace ProductsPresentationLayer
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly ISettingsService<ChatUserSettings> _settingsService;

        public ChatHub(
            ILogger<ChatHub> logger,
            ISettingsService<ChatUserSettings> settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
        }

        public async Task SendMessage(string message)
        {
            var command = CommandHelper.CreateCommand(message);

            await command.Execute(this, _settingsService);
        }

        public override async Task OnConnectedAsync()
        {
            var val = await RedisDB.GetValue(Context.ConnectionId);
            await _settingsService.SetValueAsync(Context.ConnectionId,
                new ChatUserSettings { ClientId = Context.ConnectionId });

            //_logger.LogDebug(UserSettings.Count.ToString());

            await Clients.Others.SendAsync(Consts.ClientMethods.ReceiveMessage,
                CreateSystemMessage($"User {Context.ConnectionId} connected!"));
            await Clients.Caller.SendAsync(Consts.ClientMethods.ReceiveMessage,
               CreateSystemMessage($"Greetings newcomer!"));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private ChatMessage CreateSystemMessage(string message)
            => new ChatMessage
            {
                Sender = Consts.ServerMessageSenderName,
                MessageColor = System.ConsoleColor.Blue,
                Text = message
            };
    }
}
