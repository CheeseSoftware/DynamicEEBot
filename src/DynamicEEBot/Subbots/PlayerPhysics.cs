using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    public class PlayerPhysics : SubBot
    {
        Stopwatch playerTickTimer = new Stopwatch();

        public PlayerPhysics(Bot bot)
            : base(bot)
        {
            Enabled = true;
            playerTickTimer.Start();
            UpdateSleep = 10;
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {

        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {
        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {

        }

        public override void Update(Bot bot)
        {
            if (bot.connected && bot.room.loadedWorld)
            {
                if (playerTickTimer.ElapsedMilliseconds >= Config.physics_ms_per_tick)
                {
                    playerTickTimer.Restart();
                    lock (bot.playerList)
                    {
                        foreach (Player player in bot.playerList.Values)
                        {
                            player.tick();
                            /*if (player.blockX > 0 && player.blockX < bot.room.Width - 1 && player.blockY > 0 && player.blockY < bot.room.Height - 1)
                            {
                                bot.room.DrawBlock(Block.CreateBlock(0, player.blockX, player.blockY, 16, -1)); 
                            }*/
                        }
                    }
                }
            }
        }

        public override void onEnable(Bot bot)
        {

        }

        public override void onDisable(Bot bot)
        {

        }

        public override bool HasForm
        {
            get { return false; }
        }
    }
}
