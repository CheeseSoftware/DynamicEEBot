using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot.SubBots
{
    public class OnCommand : Method
    {
        public string text;
        public int userId;
        public Player player;

        public OnCommand(string text, int userId, Player player)
        {
            this.text = text;
            this.userId = userId;
            this.player = player;
        }

        public override string ToString()
        {
            return "OnCommand - p: " + player.name + " text: " + text;
        }
    }
}
