using Microsoft.AspNetCore.SignalR;
using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public abstract class Command
    {
        protected string[] _args;
        protected abstract int? MinArgsCount { get; }
        protected virtual MessageTarget GetMessageTarget()
        {
            return MessageTarget.Self;
        }

        public Command(string[] args)
        {
            _args = args;
        }

        public async Task Execute(
            Hub hub,
            ISettingsService<ChatUserSettings> settingsService)
        {
            CommandOutput commandOutput = null;
            if (MinArgsCount == null || _args.Length >= MinArgsCount)
            {
                commandOutput = await CreateCommandOutput(
                    hub.Context.ConnectionId,
                    settingsService);
            }
            if (commandOutput == null)
            {
                commandOutput = new CommandOutput
                {
                    ClientMethod = Consts.ClientMethods.ReceiveMessage,
                    Message = CreateSystemMessage("Invalid command!")
                };
            }

            await SendCommandToTarget(hub, commandOutput);
        }

        private async Task SendCommandToTarget(Hub hub, CommandOutput commandOutput)
        {
            switch (GetMessageTarget())
            {
                case MessageTarget.All:
                    await hub.Clients.AllExcept(
                        (IReadOnlyList<string>)commandOutput.IgnoreList)
                        .SendAsync(commandOutput.ClientMethod, commandOutput.Message);
                    break;
                case MessageTarget.Self:
                    await hub.Clients.Caller.SendAsync(
                       commandOutput.ClientMethod, commandOutput.Message);
                    break;
                case MessageTarget.Personal:
                    await hub.Clients.Client(commandOutput.TargetId)
                        .SendAsync(commandOutput.ClientMethod, commandOutput.Message);
                    break;
                case MessageTarget.None:
                    break;
            }
        }

        protected abstract Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService);

        protected ChatMessage CreateSystemMessage(string message)
            => new ChatMessage
            {
                Sender = Consts.ServerMessageSenderName,
                MessageColor = System.ConsoleColor.Blue,
                Text = message
            };
    }
}
