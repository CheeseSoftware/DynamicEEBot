using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace DynamicEEBot
{
    public class Commands : SubBot
    {
        private List<string> disabledPlayers = new List<string>();
        private List<string> protectedPlayers = new List<string>();
        private List<string> getPlacerPlayers = new List<string>();

        public Commands(Bot bot)
            : base(bot)
        {
            enabled = true;
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {

        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {

        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {


            switch (args[0])
            {
                case "roomname":
                    if (isBotMod)
                        if (args.Length > 1)
                            bot.connection.Send("name", string.Join(" ", args).Replace("roomname ", ""));
                    break;
                case "woot":
                    if (isBotMod)
                        bot.connection.Send("wootup");
                    break;
                case "reset":
                    if (isBotMod)
                        bot.connection.Send("say", "/reset");
                    break;
                case "load":
                    if (isBotMod)
                        bot.connection.Send("say", "/loadlevel");
                    break;
                case "save":
                    if (isBotMod)
                        bot.connection.Send("save");
                    break;
                case "clear":
                    if (isBotMod)
                        bot.connection.Send("clear");
                    break;
            }
        }

        public override void onEnable(Bot bot)
        {

        }

        public override void onDisable(Bot bot)
        {

        }

        public override void Update(Bot bot)
        {

        }
    }
}
