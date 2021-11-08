using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore;
using ProductsCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        protected override async Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            CommandOutput result = null;
            var id = _args[0];
            var userFromId = await settingsService.GetValueAsync(id);
            if (userFromId != null)
            {
                var igonoreList = new List<string> { callerId };

                var personalMessage = string.Join(
                    Consts.CommandElementSeparator,
                    _args[1..]);

                if (!igonoreList.Contains(id))
                {
                    var callerSettings = await settingsService.GetValueAsync(callerId);

                    result = new CommandOutput
                    {
                        Message = new ChatMessage
                        {
                            Sender = callerId,
                            MessageColor = callerSettings.UserConsoleColor,
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
