using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    public class Zombie : Monster
    {
        PathFinding pathFinding = new PathFinding();
        Block zombieBlock = null;
        Block zombieOldBlock = null;
        Player targetPlayer = null;
        Stopwatch updateTimer = new Stopwatch();
        Stopwatch lagTimer = new Stopwatch();
        Stack<Square> pathToGo = null;

        public Zombie(int x, int y)
            : base(x, y)
        {
            updateTimer.Start();
            lagTimer.Start();
            zombieBlock = Block.CreateBlock(0, xBlock, yBlock, 32, 0);
        }

        public static double GetDistanceBetween(Player player, int targetX, int targetY)
        {
            double a = player.blockX - targetX;
            double b = player.blockY - targetY;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        public override void Update(Bot bot)
        {
            if (updateTimer.ElapsedMilliseconds >= 1000)
            {
                updateTimer.Restart();
                double lowestDistance = 0;
                Player lowestDistancePlayer = null;
                lock (bot.playerList)
                {
                    foreach (Player player in bot.playerList.Values)
                    {
                        if (player.isgod)
                            continue;
                        double currentDistance = GetDistanceBetween(player, xBlock, yBlock);
                        if (currentDistance < lowestDistance || lowestDistance == 0)
                        {
                            lowestDistance = currentDistance;
                            lowestDistancePlayer = player;
                        }
                    }
                }
                if (lowestDistancePlayer != null)
                {
                    targetPlayer = lowestDistancePlayer;

                }
            }

            if (targetPlayer != null && xBlock != targetPlayer.x && yBlock != targetPlayer.y)
            {
                //pathFinding = null;
                //pathFinding = new PathFinding();
                //lagTimer.Restart();
                pathToGo = pathFinding.Start(xBlock, yBlock, targetPlayer.blockX, targetPlayer.blockY, bot);
                //Console.WriteLine("elapsed shitlagtime " + lagTimer.ElapsedMilliseconds + "MS");

                if (pathToGo != null && pathToGo.Count != 0)
                {
                    if (pathToGo.Count > 2)
                    {
                        pathToGo.Pop();
                        pathToGo.Pop();
                    }
                    Square next = pathToGo.Pop();
                    xBlock = next.x;
                    yBlock = next.y;
                }

                if (targetPlayer != null)
                {
                    if (GetDistanceBetween(targetPlayer, xBlock, yBlock) <= 1 && !targetPlayer.isgod)
                    {
                        targetPlayer.killPlayer();
                        bot.connection.Send("say", "/kill " + targetPlayer.name);
                    }
                }
            }
            base.Update(bot);
        }

        public override void Draw(Bot bot)
        {
            base.Draw(bot);
            if (xBlock != xOldBlock || yBlock != yOldBlock)
            {
                zombieBlock = Block.CreateBlock(0, xBlock, yBlock, 32, -1);
                bot.room.DrawBlock(zombieBlock);
                zombieOldBlock = Block.CreateBlock(0, xOldBlock, yOldBlock, 4, -1);
                bot.room.DrawBlock(zombieOldBlock);
            }
        }
    }
}
