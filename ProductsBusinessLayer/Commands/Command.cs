using Microsoft.AspNetCore.SignalR;
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
            IList<ChatUserSettings> userSettings)
        {
            CommandOutput commandOutput = null;
            if (MinArgsCount == null || _args.Length >= MinArgsCount)
            {
                commandOutput = CreateCommandOutput(
                    hub.Context.ConnectionId,
                    userSettings);
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

        protected abstract CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings);

        protected ChatMessage CreateSystemMessage(string message)
            => new ChatMessage
            {
                Sender = Consts.ServerMessageSenderName,
                MessageColor = System.ConsoleColor.Blue,
                Text = message
            };
    }
}
