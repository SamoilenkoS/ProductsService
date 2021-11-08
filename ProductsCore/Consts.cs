using System;

namespace ProductsCore
{
    public static class Consts
    {
        public const char CommandStartSign = '/';
        public const char CommandElementSeparator = ' ';
        public const string ServerMessageSenderName = "System";

        public static class Commands
        {
            public const string PrivateMessage = "msg";
            public const string Help = "help";
            public const string Color = "color";
            public const string Mute = "mute";
            public const string MuteList = "mutelist";
            public const string Unmute = "unmute";
        }

        public static class ClientMethods
        {
            public const string ReceiveMessage = nameof(ReceiveMessage);
            public const string ColorChanged = nameof(ColorChanged);
        }

        public static class ServerMessages
        {
            public static string Help =
                "/msg <CallerId> Your message text => send private message to client"
                + Environment.NewLine +
                "/help => call this help"
                + Environment.NewLine +
                "Text of your message => send message to all in chat";

            public static string ColorChanged(string color)
                => string.Format("Color changed to {0}", color);

            public static string UserMuted(string userId)
                => string.Format("{0} muted!", userId);

            public static string UserUnmuted(string userId)
                => string.Format("{0} unmuted!", userId);
        }
    }
}
