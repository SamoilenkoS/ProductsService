using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;

namespace ProductsBusinessLayer.Commands
{
    public class HelpCommand : Command
    {
        protected override int? MinArgsCount => null;

        public HelpCommand(string[] args) : base(args) { }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            return new CommandOutput
            {
                Message = CreateSystemMessage(Consts.ServerMessages.Help)
            };
        }
    }
}