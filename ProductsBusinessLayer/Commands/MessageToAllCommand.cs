using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class MessageToAllCommand : Command
    {
        protected override int? MinArgsCount => 1;

        protected override MessageTarget GetMessageTarget()
        {
            return MessageTarget.All;
        }

        public MessageToAllCommand(string[] args) : base(args) { }

        protected override async Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            var igonoreList = new List<string> { callerId };
            var userSettings = await settingsService.GetValueAsync(callerId);

            return new CommandOutput
            {
                Message = new ChatMessage
                {
                    Sender = callerId,
                    MessageColor = userSettings.UserConsoleColor,
                    Text = string.Join(Consts.CommandElementSeparator, _args)
                },
                IgnoreList = igonoreList
            };
        }
    }
}