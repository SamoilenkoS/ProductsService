using System;
using System.Collections.Generic;

namespace ProductsCore.Models
{
    public class ChatUserSettings
    {
        public string ClientId { get; set; }
        public string Nickname { get; set; }
        public ConsoleColor UserConsoleColor { get; set; }
        public HashSet<string> MuteList { get; set; }

        public ChatUserSettings()
        {
            UserConsoleColor = ConsoleColor.Yellow;
            MuteList = new HashSet<string>();
        }
    }
}
