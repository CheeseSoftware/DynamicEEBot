using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace DynamicEEBot.SubBots
{
    public class OnMessage : Method
    {
        public Message m;

        public OnMessage(Message m)
        {
            this.m = m;
        }

        public override string ToString()
        {
            return "OnMessage - m.Type: " + m.Type; 
        }
    }
}
