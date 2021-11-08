using ProductsBusinessLayer.Commands;
using ProductsCore;

namespace ProductsBusinessLayer
{
    public class CommandHelper
    {
        public static Command CreateCommand(string message)
        {
            Command result;
            if (message.StartsWith(Consts.CommandStartSign))
            {
                message = message[1..];
                var splitted = message.Split(Consts.CommandElementSeparator, System.StringSplitOptions.RemoveEmptyEntries);
                var args = splitted[1..];
                result = splitted[0] switch
                {
                    Consts.Commands.PrivateMessage => new PrivateMessageCommand(args),
                    Consts.Commands.Help => new HelpCommand(args),
                    Consts.Commands.Color => new ColorCommand(args),
                    Consts.Commands.Mute => new MuteCommand(args),
                    Consts.Commands.Unmute => new UnmuteCommand(args),
                    Consts.Commands.MuteList => new MuteListCommand(args),
                    _ => new InvalidCommand(args),
                };
            }
            else
            {
                result = new MessageToAllCommand(message.Split(Consts.CommandElementSeparator));
            }

            return result;
        }
    }
}
