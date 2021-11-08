using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductsBusinessLayer.Commands
{
    public class PrivateMessageCommand : Command
    {
        private MessageTarget _target;

        protected override int? MinArgsCount => 2;

        protected override MessageTarget GetMessageTarget()
        {
            return _target;
        }

        public PrivateMessageCommand(string[] args) : base(args)
        {
            _target = MessageTarget.Personal;
        }

        protected override CommandOutput CreateCommandOutput(
            string callerId,
            IList<ChatUserSettings> userSettings)
        {
            CommandOutput result = null;
            var id = _args[0];
            if (userSettings.GetSettingsByClientId(id) != null)
            {
                var igonoreList = userSettings
                  .Where(x => x.MuteList
                      .Contains(callerId))
                  .Select(x => x.ClientId).ToList();
                igonoreList.Add(callerId);

                var personalMessage = string.Join(
                    Consts.CommandElementSeparator,
                    _args[1..]);

                if (!igonoreList.Contains(id))
                {
                    result = new CommandOutput
                    {
                        Message = new ChatMessage
                        {
                            Sender = callerId,
                            MessageColor = userSettings
                                .GetSettingsByClientId(callerId)
                                .UserConsoleColor,
                            Text = personalMessage,
                        },
                        IgnoreList = igonoreList,
                        TargetId = id
                    };
                }
                else
                {
                    _target = MessageTarget.None;
                }
            }

            return result;
        }
    }
}
