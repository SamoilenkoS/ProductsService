using System;
using System.Collections.Generic;

namespace ProductsCore.Models
{
    public class ChatUserSettings
    {
        public string ClientId { get; set; }
        public string Nickname { get; set; }
        public ConsoleColor UserConsoleColor { get; set; }
        public IEnumerable<string> MuteList { get; set; }

        public ChatUserSettings()
        {
            UserConsoleColor = ConsoleColor.Red;
        }

        public override bool Equals(object obj)
        {
            var anotherSettings = obj as ChatUserSettings;
            return anotherSettings != null && anotherSettings.ClientId == ClientId;
        }
    }
}
