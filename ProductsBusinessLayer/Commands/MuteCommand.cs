using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class MuteCommand : Command
    {
        protected override int? MinArgsCount => 1;

        public MuteCommand(string[] args) : base(args)
        {}

        protected override async Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            CommandOutput result = null;
            var id = _args[0];
            var userWithId = await settingsService.GetValueAsync(id);
            var currentUserSettings = await settingsService.GetValueAsync(callerId);

            if (userWithId != null &&
                !currentUserSettings.MuteList.Contains(id))
            {
                currentUserSettings.MuteList.Add(id);
                await settingsService.SetValueAsync(callerId, currentUserSettings);

                result = new CommandOutput
                {
                    ClientMethod = Consts.ClientMethods.ReceiveMessage,
                    Message = CreateSystemMessage(Consts.ServerMessages.UserMuted(id))
                };
            }

            return result;
        }
    }
}