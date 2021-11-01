using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProductsCore;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsPresentationLayer
{
    public class ChatHub : Hub
    {
        private static IList<ChatUserSettings> UserSettings;
        private readonly ILogger<ChatHub> _logger;

        static ChatHub()
        {
            UserSettings = new List<ChatUserSettings>();
        }

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(string message)
        {
            if (message.StartsWith(Consts.CommandStartSign))
            {
                message = message[1..];
                var splitted = message.Split(Consts.CommandElementSeparator);
                var result = false;
                switch (splitted[0].ToLower())
                {
                    case Consts.Commands.PrivateMessage:
                        if(splitted.Length > 2)
                        {
                            var id = splitted[1];
                            if (this[id] != null)
                            {
                                var personalMessage = string.Join(
                                    Consts.CommandElementSeparator,
                                    splitted[2..]);

                                await Clients.Clients(id).SendAsync(Consts.ClientMethods.ReceiveMessage,
                                    new ChatMessage
                                    {
                                        Sender = Context.ConnectionId,
                                        MessageColor = this[Context.ConnectionId].UserConsoleColor,
                                        Text = personalMessage
                                    });

                                result = true;
                            }
                        }
                        break;
                    case Consts.Commands.Help:
                        await Clients.Caller.SendAsync(
                            Consts.ClientMethods.ReceiveMessage,
                            CreateSystemMessage(Consts.ServerMessages.Help));
                        result = true;
                        break;
                    case Consts.Commands.Color:
                        if(splitted.Length == 2)
                        {
                            var colorString = splitted[1];
                            if (Enum.TryParse(typeof(ConsoleColor), colorString, out var color))
                            {
                                var newColor = (ConsoleColor)color;
                                this[Context.ConnectionId].UserConsoleColor = newColor;

                                await Clients.Caller.SendAsync(
                                    Consts.ClientMethods.ColorChanged, newColor);

                                result = true;
                            }
                        }
                        break;
                }

                if (!result)
                {
                    await Clients.Caller.SendAsync(
                        Consts.ClientMethods.ReceiveMessage,
                        CreateSystemMessage("Invalid command!"));
                }
            }
            else
            {
                await Clients.Others.SendAsync(
                    Consts.ClientMethods.ReceiveMessage,
                    new ChatMessage
                    {
                        Sender = Context.ConnectionId,
                        MessageColor = this[Context.ConnectionId].UserConsoleColor,
                        Text = message
                    });
            }
        }

        public override async Task OnConnectedAsync()
        {
            UserSettings.Add(
                new ChatUserSettings
                {
                    ClientId = Context.ConnectionId
                });

            _logger.LogDebug(UserSettings.Count.ToString());

            await Clients.Others.SendAsync(Consts.ClientMethods.ReceiveMessage,
                CreateSystemMessage($"User {Context.ConnectionId} connected!"));
            await Clients.Caller.SendAsync(Consts.ClientMethods.ReceiveMessage,
               CreateSystemMessage($"Greetings newcomer!"));
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            UserSettings.Remove(
                new ChatUserSettings
                {
                    ClientId = Context.ConnectionId
                });
            _logger.LogDebug(UserSettings.Count.ToString());

            return base.OnDisconnectedAsync(exception);
        }

        private ChatMessage CreateSystemMessage(string message)
            => new ChatMessage
            {
                Sender = Consts.ServerMessageSenderName,
                MessageColor = System.ConsoleColor.Blue,
                Text = message
            };

        private ChatUserSettings this[string connectionId]
            => UserSettings.FirstOrDefault(x => x.ClientId == connectionId);
    }
}
