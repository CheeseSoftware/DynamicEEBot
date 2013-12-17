using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    class MazeGenerator : SubBot
    {
        BlockPos[] moves = new BlockPos[] { new BlockPos(0, -1, 0), new BlockPos(0, 1, 0), new BlockPos(0, 0, -1), new BlockPos(0, 0, 1) };

        Random random = new Random();

        public MazeGenerator(Bot bot)
            : base(bot)
        {
        }

        public override void onEnable(Bot bot)
        {
        }

        public override void onDisable(Bot bot)
        {
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
                    case "generatemaze":
                        {
                            List<BlockPos> points = new List<BlockPos>();

                            points.Add(new BlockPos(0, player.blockX & 0xFFFE, player.blockY & 0xFFFE));

                            bot.room.DrawBlock(Block.CreateBlock(0, points[0].x, points[0].y, 4, -1));

                            while (points.Count > 0)
                            {
                                int i = random.Next(points.Count);

                                foreach (BlockPos p in moves)
                                {
                                    BlockPos newPoint = new BlockPos(0, points[i].x + p.x * 2, points[i].y + p.y * 2);

                                    Block b = bot.room.getBotBlock(0, newPoint.x, newPoint.y);
                                    if (b.blockId > 8 && b.blockId < 218)
                                    {
                                        bot.room.DrawBlock(Block.CreateBlock(0, points[i].x + p.x, points[i].y + p.y, 4, -1));
                                        bot.room.DrawBlock(Block.CreateBlock(0, newPoint.x, newPoint.y, 4, -1));
                                        points.Add(newPoint);
                                    }
                                }

                                points.RemoveAt(i);
                            }
                        }
                        break;
                }
            }
        }

        public override void Update(Bot bot)
        {

        }
    }
}
