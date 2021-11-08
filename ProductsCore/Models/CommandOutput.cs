using System.Collections.Generic;

namespace ProductsCore.Models
{
    public class CommandOutput
    {
        public string ClientMethod { get; set; } = Consts.ClientMethods.ReceiveMessage;
        public object Message { get; set; }
        public IEnumerable<string> IgnoreList { get; set; } = new List<string>();
        public string TargetId { get; set; }
    }
}
