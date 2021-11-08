using ProductsCore;
using ProductsCore.Models;
using System;
using System.Collections.Generic;

namespace ProductsBusinessLayer.Commands
{
    public class ColorCommand : Command
    {
        protected override int? MinArgsCount => 1;

        public ColorCommand(string[] args) : base(args) { }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            CommandOutput result = null;
            var colorString = _args[0];
            if (Enum.TryParse(typeof(ConsoleColor), colorString, out var color))
            {
                var newColor = (ConsoleColor)color;
                userSettings.GetSettingsByClientId(callerId).UserConsoleColor = newColor;

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