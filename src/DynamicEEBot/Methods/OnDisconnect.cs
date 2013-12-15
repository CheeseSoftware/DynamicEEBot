using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot.SubBots
{
    public class OnDisconnect : Method
    {
        public string reason;

        public OnDisconnect(string reason)
        {
            this.reason = reason;
        }

        public override string ToString()
        {
            return "OnDisconnect - reason: " + reason;
        }
    }
}
