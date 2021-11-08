using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;

namespace ProductsBusinessLayer.Commands
{
    public class UnmuteCommand : Command
    {
        protected override int? MinArgsCount => null;

        public UnmuteCommand(string[] args) : base(args) { }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            CommandOutput result = null;
            var id = _args[0];
            var userWithId = userSettings.GetSettingsByClientId(id);
            var currentUserSettings = userSettings.GetSettingsByClientId(callerId);

            if (userWithId != null &&
                currentUserSettings.MuteList.Contains(id))
            {
                currentUserSettings.MuteList.Remove(id);

                result = new CommandOutput
                {
                    Message = CreateSystemMessage(Consts.ServerMessages.UserUnmuted(id))
                };
            }

            return result;
        }
    }
}