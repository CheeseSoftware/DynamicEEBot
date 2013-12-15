using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    class WorldEdit : SubBot
    {

        public WorldEdit(Bot bot)
            : base(bot)
        {
            enabled = true;
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            switch (m.Type)
            {
                case "b":
                    {
                        int layer = m.GetInt(0);
                        int x = m.GetInt(1);
                        int y = m.GetInt(2);
                        int blockId = m.GetInt(3);
                        int placer = m.GetInt(4);
                        Player player = bot.playerList[placer];
                        if(player.hasVar("brush") && blockId == 32)
                        {
                            if((bool)player.getVar("brush"))
                            {
                                int brushSize = player.getVar("brushsize") == null ? 1 : (int)player.getVar("brushsize");
                                int brushBlock = player.getVar("brushblock") == null ? 9 : (int)player.getVar("brushblock");
                                for (int a = brushSize; a > 0; a--)
                                {
                                    for (double i = 0.0; i < 360.0; i += (10 / brushSize))
                                    {
                                        double mAngle = i * System.Math.PI / 180;
                                        int tempx = x + (int)(a * System.Math.Cos(mAngle));
                                        int tempy = y + (int)(a * System.Math.Sin(mAngle));
                                        if (tempx > 0 && tempx < bot.room.width && tempy > 0 && tempy < bot.room.height)
                                        {
                                            bot.room.DrawBlock(Block.CreateBlock(brushBlock >= 500 ? 1 : 0, tempx, tempy, brushBlock, -1));
                                        }
                                    }
                                }
                            }
                        }

                    }
                    break;
            }
        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            switch (args[0])
            {
                case "b":
                    if (isBotMod)
                    {
                        if (player.hasVar("brush"))
                            player.setVar("brush", !(bool)player.getVar("brush"));
                        else
                            player.setVar("brush", true);
                        bool playerHasBrush = (bool)player.getVar("brush");
                        bot.connection.Send("say", player.name + ": Brush is " + (playerHasBrush ? "ON" : "OFF"));
                    }
                    break;
                case "bs":
                    if (isBotMod)
                    {
                        int size;
                        if (args.Length > 1 && int.TryParse(args[1], out size))
                        {
                            if(size < 10)
                                player.setVar("brushsize", size);
                            else
                                bot.connection.Send("say", "Too big size! 0-9");
                        }
                        else
                            bot.connection.Send("say", "Usage: !bs <size>");
                    }
                    break;
                case "bb":
                    if(isBotMod)
                    {
                        int blockId;
                        if (args.Length > 1 && int.TryParse(args[1], out blockId))
                        {
                            player.setVar("brushblock", blockId);
                        }
                        else
                            bot.connection.Send("say", "Usage: !bb <id>");
                    }
                    break;
                case "fill":
                    if (args.Length > 1 && isBotMod)
                    {
                        int blockId;
                        Int32.TryParse(args[1], out blockId);
                        int layer = (blockId >= 500) ? 1 : 0;
                        for (int y = 1; y < bot.room.height - 1; y++)
                        {
                            for (int x = 1; x < bot.room.width - 1; x++)
                            {
                                bot.room.DrawBlock(Block.CreateBlock(layer, x, y, blockId, -1));
                            }
                        }
                    }
                    break;
                case "fillexpand":
                    {
                        if (isBotMod)
                        {
                            int toReplace = 0;
                            int toReplaceLayer = 0;
                            int toReplaceWith = 0;
                            if (args.Length == 2)
                            {
                                if (!int.TryParse(args[1], out toReplaceWith))
                                {
                                    bot.connection.Send("say", "Usage: !fillexpand <from id=0> <to id>");
                                    return;
                                }
                            }
                            else if (args.Length == 3)
                            {
                                if (!int.TryParse(args[2], out toReplaceWith) || !int.TryParse(args[1], out toReplace))
                                {
                                    bot.connection.Send("say", "Usage: !fillexpand <from id=0> <to id>");
                                    return;
                                }
                            }
                            if (toReplace >= 500)
                                toReplaceLayer = 1;
                            Block startBlock = bot.room.getBlock(toReplaceLayer, player.blockX, player.blockY);
                            if (startBlock.blockId == toReplace)
                            {
                                int total = 0;
                                List<Point> closeBlocks = new List<Point> { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1) };
                                Queue<Point> blocksToCheck = new Queue<Point>();
                                List<Point> blocksToFill = new List<Point>();
                                blocksToCheck.Enqueue(new Point(startBlock.x, startBlock.y));
                                while (blocksToCheck.Count > 0)
                                {
                                    Point parent = blocksToCheck.Dequeue();
                                    //if (!blocksToFill.Contains(parent))
                                    for (int i = 0; i < closeBlocks.Count; i++)
                                    {
                                        Point current = new Point(closeBlocks[i].X + parent.X, closeBlocks[i].Y + parent.Y);
                                        Block currentBlock = bot.room.getBlock(toReplaceLayer, current.X, current.Y);
                                        if (currentBlock.blockId == toReplace && !blocksToCheck.Contains(current) && !blocksToFill.Contains(current) && current.X >= 0 && current.Y >= 0 && current.X <= bot.room.width && current.Y <= bot.room.height)
                                        {
                                            blocksToFill.Add(current);
                                            blocksToCheck.Enqueue(current);
                                            total++;
                                            if (total > 10000)
                                            {
                                                bot.connection.Send("say", "Don't try to fill the whole world, fool!");
                                                return;
                                            }
                                        }
                                    }
                                }
                                bot.connection.Send("say", "total blocks: " + total + ". Filling..");
                                int layer = 0;
                                if (toReplaceWith >= 500)
                                    layer = 1;
                                foreach (Point p in blocksToFill)
                                {
                                    bot.room.DrawBlock(Block.CreateBlock(layer, (int)p.X, (int)p.Y, toReplaceWith, player.id));
                                }
                            }
                        }
                    }
                    break;
                case "replace":
                    if (isBotMod)
                    {
                        if (args.Length > 2 && isBotMod)
                        {
                            int blockId1, blockId2;
                            Int32.TryParse(args[1], out blockId1);
                            Int32.TryParse(args[2], out blockId2);
                            int layer1 = (blockId1 >= 500) ? 1 : 0;
                            int layer2 = (blockId2 >= 500) ? 1 : 0;
                            for (int y = 1; y < bot.room.height - 1; y++)
                            {
                                for (int x = 1; x < bot.room.width - 1; x++)
                                {
                                    if (bot.room.getBlock(layer1, x, y).blockId == blockId1)
                                        bot.room.DrawBlock(Block.CreateBlock(layer2, x, y, blockId2, -1));
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {

        }

        public override void onEnable(Bot bot)
        {
            //throw new NotImplementedException();
        }

        public override void onDisable(Bot bot)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Bot bot)
        {
            //throw new NotImplementedException();
        }
    }
}
