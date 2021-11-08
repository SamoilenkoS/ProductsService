using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class UnmuteCommand : Command
    {
        protected override int? MinArgsCount => null;

        public UnmuteCommand(string[] args) : base(args) { }

        protected override async Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            CommandOutput result = null;
            var id = _args[0];
            var userWithId = await settingsService.GetValueAsync(id);
            var currentUserSettings = await settingsService.GetValueAsync(callerId);

            if (userWithId != null &&
                currentUserSettings.MuteList.Contains(id))
            {
                currentUserSettings.MuteList.Remove(id);
                await settingsService.SetValueAsync(callerId, currentUserSettings);

                result = new CommandOutput
                {
                    Message = CreateSystemMessage(Consts.ServerMessages.UserUnmuted(id))
                };
            }

            return result;
        }
    }
}