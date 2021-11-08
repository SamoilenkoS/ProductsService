using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductsBusinessLayer.Commands
{
    public class MessageToAllCommand : Command
    {
        protected override int? MinArgsCount => 1;

        protected override MessageTarget GetMessageTarget()
        {
            return MessageTarget.All;
        }

        public MessageToAllCommand(string[] args) : base(args) { }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            var igonoreList = userSettings
                  .Where(x => x.MuteList
                      .Contains(callerId))
                  .Select(x => x.ClientId).ToList();
            igonoreList.Add(callerId);

            return new CommandOutput
            {
                Message = new ChatMessage
                {
                    Sender = callerId,
                    MessageColor = userSettings.GetSettingsByClientId(callerId).UserConsoleColor,
                    Text = string.Join(Consts.CommandElementSeparator, _args)
                },
                IgnoreList = igonoreList
            };
        }
    }
}