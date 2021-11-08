using ProductsCore.Models;
using System.Collections.Generic;

namespace ProductsBusinessLayer.Commands
{
    public class InvalidCommand : Command
    {
        protected override int? MinArgsCount => null;

        public InvalidCommand(string[] args) : base(args) { }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            return new CommandOutput
            {
                Message = CreateSystemMessage("Invalid command!")
            };
        }
    }
}