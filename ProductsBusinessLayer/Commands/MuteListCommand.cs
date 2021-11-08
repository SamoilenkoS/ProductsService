using ProductsBusinessLayer.Services.ChatSettingsService;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Commands
{
    public class MuteListCommand : Command
    {
        protected override int? MinArgsCount => null;

        public MuteListCommand(string[] args) : base(args) { }

        private string FormIgnoreListMessage(HashSet<string> ignoreIds)
        {
            return string.Join(Environment.NewLine, ignoreIds);
        }

        protected override Task<CommandOutput> CreateCommandOutput(
            string callerId,
            ISettingsService<ChatUserSettings> settingsService)
        {
            return Task.FromResult(
                new CommandOutput
                {
                    Message = CreateSystemMessage(
                        FormIgnoreListMessage(new HashSet<string>()))
                });
        }
    }
}