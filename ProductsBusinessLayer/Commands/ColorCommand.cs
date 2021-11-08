using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class ColorCommand : Command
    {
        protected override int? MinArgsCount => 1;

        public ColorCommand(string[] args) : base(args) { }

        protected override async Task<CommandOutput> CreateCommandOutput(
            string callerId,
             ISettingsService<ChatUserSettings> settingsService)
        {
            CommandOutput result = null;
            var colorString = _args[0];
            if (Enum.TryParse(typeof(ConsoleColor), colorString, out var color))
            {
                var newColor = (ConsoleColor)color;
                var currentUserSettings = await settingsService.GetValueAsync(callerId);
                currentUserSettings.UserConsoleColor = newColor;
                await settingsService.SetValueAsync(callerId, currentUserSettings);

                result = new CommandOutput
                {
                    ClientMethod = Consts.ClientMethods.ColorChanged,
                    Message = newColor
                };
            }

            return result;
        }
    }
}