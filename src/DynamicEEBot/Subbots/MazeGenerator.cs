using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    class MazeGenerator : SubBot
    {
        BlockPos[] moves = new BlockPos[] { new BlockPos(0, -1, 0), new BlockPos(0, 1, 0), new BlockPos(0, 0, -1), new BlockPos(0, 0, 1) };
        private System.Windows.Forms.Button button1;

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
                    case "genmaze":
                        {
                            Stack<BlockPos> points = new Stack<BlockPos>();

                            points.Push(new BlockPos(0, player.blockX & 0xFFFE, player.blockY & 0xFFFE));
                            points.Push(new BlockPos(0, player.blockX & 0xFFFE, player.blockY & 0xFFFE));

                            int maxLength;

                            if (args.Count() >= 1)
                                int.TryParse(args[1], out maxLength);
                            else
                                maxLength = 1;

                            if (maxLength < 1)
                                maxLength = 1;

                            while (points.Count > 0)
                            {
                                BlockPos point = points.Pop();
                                BlockPos wallPoint = points.Pop();

                                if (point.x > 0 && point.x < bot.room.Width - 1 && point.y > 0 && point.y < bot.room.Height - 1)
                                {
                                    Block block = bot.room.getBotBlock(0, point.x, point.y);

                                    if (block.blockId > 8 && block.blockId < 226)
                                    {
                                        bot.room.DrawBlock(Block.CreateBlock(0, point.x, point.y, 4, -1));
                                        bot.room.DrawBlock(Block.CreateBlock(0, wallPoint.x, wallPoint.y, 4, -1));

                                        List<BlockPos> namnpriblem = new List<BlockPos>();

                                        for (int i = 0; i < moves.Length; i++)
                                            namnpriblem.Add(moves[i]);

                                        while(namnpriblem.Count > 0)
                                        {
                                            int index = random.Next(namnpriblem.Count);
                                            BlockPos newPoint = new BlockPos(0, point.x + namnpriblem[index].x * 2, point.y + namnpriblem[index].y * 2);
                                            BlockPos newWallPoint = new BlockPos(0, point.x + namnpriblem[index].x, point.y + namnpriblem[index].y);
                                            namnpriblem.RemoveAt(index);
             
                                            //BlockPos newWallPoint = new BlockPos(0, point.x + newPoint.x, point.y + newPoint.y);

                                            points.Push(newWallPoint);
                                            points.Push(newPoint);
                                        }
                                    }
                                }
                            }

                        }
                        break;

                    case "genemaze2":
                        {
                            int maxLength;

                            if (args.Count() >= 1)
                                int.TryParse(args[1], out maxLength);
                            else
                                maxLength = 1;

                            if (maxLength < 1)
                                maxLength = 1;

                            List<BlockPos> points = new List<BlockPos>();

                            points.Add(new BlockPos(0, player.blockX & 0xFFFE, player.blockY & 0xFFFE));

                            bot.room.DrawBlock(Block.CreateBlock(0, points[0].x, points[0].y, 4, -1));

                            while (points.Count > 0)
                            {
                                int i = random.Next(points.Count);

                                BlockPos p = moves[random.Next(moves.Count())];
                                {

                                    BlockPos pointA, pointB;

                                    pointA = new BlockPos(0, points[i].x + p.x, points[i].y + p.y); // is between the previous point nad pointB
                                    pointB = new BlockPos(0, points[i].x, points[i].y); //"destination"

                                    for (int j = 0; j < random.Next(1,maxLength); j++)
                                    {
                                        pointB = new BlockPos(0, pointB.x + p.x * 2, pointB.y + p.y * 2);
                                        //pointB.y = p.y * 2;
                                        //pointB.x //BlockPos pointA = new BlockPos(0, points[i].x + p.x * 2, points[i].y + p.y * 2);
                                        //BlockPos pointB = new BlockPos(0, points[i].x + p.x * 2, points[i].y + p.y * 2);

                                        Block b = bot.room.getBotBlock(0, pointB.x, pointB.y);
                                        Block b2 = bot.room.getBotBlock(0, pointA.x, pointA.y);

                                        if (b.blockId > 8 && b.blockId < 218 && b2.blockId > 8 && b2.blockId < 218
                                            && b.x > 1 && b.y > 1 && b.x < bot.room.Width-1 && b.y < bot.room.Height-1)
                                        {
                                            bot.room.DrawBlock(Block.CreateBlock(0, pointA.x, pointA.y, 4, -1));
                                            bot.room.DrawBlock(Block.CreateBlock(0, pointB.x, pointB.y, 4, -1));
                                            points.Add(pointB);

                                            pointA = new BlockPos(0, pointA.x + p.x * 2, pointA.y + p.y * 2);
                                            //pointA.x += p.x * 2;
                                            //pointA.y += p.y * 2;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }

                                bool noWay = false;

                                foreach (var m in moves)
                                {
                                    BlockPos a = new BlockPos(0, points[i].x + m.x, points[i].y + m.y);
                                    BlockPos b = new BlockPos(0, points[i].x + m.x, points[i].y + m.y);

                                    Block bl = bot.room.getBotBlock(0, b.x, b.y);
                                        Block bl2 = bot.room.getBotBlock(0, a.x, a.y);

                                        if (!(bl.blockId > 8 && bl.blockId < 218 && bl2.blockId > 8 && bl2.blockId < 218
                                            && bl.x > 1 && bl.y > 1 && bl.x < bot.room.Width - 1 && bl.y < bot.room.Height - 1))
                                        {
                                            noWay = true;
                                            break;
                                        }

                                }

                                if (noWay)
                                {
                                    points.RemoveAt(i);
                                }
                            }
                        }
                        break;
                }
            }
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
