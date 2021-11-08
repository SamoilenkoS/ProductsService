using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class InvalidCommand : Command
    {
        protected override int? MinArgsCount => null;

        public InvalidCommand(string[] args) : base(args) { }

        protected override Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            return Task.FromResult(
                new CommandOutput
                {
                    Message = CreateSystemMessage("Invalid command!")
                });
        }
    }
}