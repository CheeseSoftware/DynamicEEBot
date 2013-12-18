using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    public class Zombies : SubBot
    {
        public static List<Zombie> zombieList = new List<Zombie>();
        public static Stopwatch zombieUpdateStopWatch = new Stopwatch();
        public static Stopwatch zombieDrawStopWatch = new Stopwatch();
        Random r = new Random();

        public Zombies(Bot bot)
            : base(bot)
        {
            UpdateSleep = 300;
            zombieUpdateStopWatch.Start();
            zombieDrawStopWatch.Start();
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            //throw new NotImplementedException();
        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {

        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            switch (args[0])
            {
                case "zombie":
                    if(isBotMod)
                    {
                        Zombie zombie = new Zombie(player.blockX * 16, player.blockY * 16);
                        lock (zombieList)
                        {
                            zombieList.Add(zombie);
                        }
                        //bot.room.DrawBlock(Block.CreateBlock(0, bot.playerList[playerId].blockX, bot.playerList[playerId].blockY, 32, 0));
                    }
                    break;
                case "zombies":
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int x = r.Next(1, bot.room.Width - 1);
                            int y = r.Next(1, bot.room.Height - 1);
                            Zombie zombie = new Zombie(x * 16, y * 16);
                            lock (zombieList)
                            {
                                zombieList.Add(zombie);
                            }
                        }
                    }
                    break;
                case "removezombies":
                    {
                        lock (zombieList)
                        {
                            zombieList.Clear();
                        }
                    }
                    break;
            }
        }

        public override void Update(Bot bot)
        {
            if (bot.connected)
            {
                long lag = 0;
                zombieUpdateStopWatch.Restart();
                lock (zombieList)
                {
                    foreach (Zombie zombie in zombieList)
                    {
                        zombie.Update(bot);
                        zombie.Draw(bot);
                    }
                }
                lag = zombieUpdateStopWatch.ElapsedMilliseconds;
                //Console.WriteLine(lag);
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
