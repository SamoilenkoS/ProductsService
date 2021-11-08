using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;

namespace ProductsBusinessLayer.Commands
{
    public class MuteCommand : Command
    {
        protected override int? MinArgsCount => 1;

        public MuteCommand(string[] args) : base(args)
        {}

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            CommandOutput result = null;
            var id = _args[0];
            var userWithId = userSettings.GetSettingsByClientId(id);
            var currentUserSettings = userSettings.GetSettingsByClientId(callerId);

            if (userWithId != null &&
                !currentUserSettings.MuteList.Contains(id))
            {
                currentUserSettings.MuteList.Add(id);

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