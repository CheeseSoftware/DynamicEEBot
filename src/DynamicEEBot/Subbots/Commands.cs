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
            Enabled = true;
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {

        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {

        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            if (isBotMod)
            {
                switch (args[0])
                {
                    case "crash":
                        Thread.Sleep(61000);
                        throw new System.Exception("I'm a happy error in a sunny day.");
                        break;
                    case "roomname":
                        if (args.Length > 1)
                            bot.connection.Send("name", string.Join(" ", args).Replace("roomname ", ""));
                        break;
                    case "woot":
                        bot.connection.Send("wootup");
                        break;
                    case "reset":
                        bot.connection.Send("say", "/reset");
                        break;
                    case "load":
                        bot.connection.Send("say", "/loadlevel");
                        break;
                    case "save":
                        bot.connection.Send("save");
                        break;
                    case "clear":
                        bot.connection.Send("clear");
                        break;
                    case "ping":
                        bot.connection.Send("say", "Pong!");
                        break;
                    case "pos":
                        bot.connection.Send("say", "Your position: X:" + player.blockX + " Y:" + player.blockY);
                        break;
                }
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

        public override bool HasForm
        {
            get { return false; }
        }
    }
}
