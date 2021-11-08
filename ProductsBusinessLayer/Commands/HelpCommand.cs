using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class HelpCommand : Command
    {
        protected override int? MinArgsCount => null;

        public HelpCommand(string[] args) : base(args) { }

        protected override Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            return Task.FromResult(
                new CommandOutput
                {
                    Message = CreateSystemMessage(Consts.ServerMessages.Help)
                });
        }
    }
}