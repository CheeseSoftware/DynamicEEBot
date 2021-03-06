﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
{
    class WorldEdit : SubBot
    {
        public bool bothPointsSet = false;
        public Point editBlock1 = new Point(-1, -1);
        public Point editBlock2 = new Point(-1, -1);
        private Point nullPoint = new Point(-1, -1);

        public WorldEdit(Bot bot)
            : base(bot)
        {
            Enabled = true;
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
                        int previousBlock = bot.room.getBlock(layer, x, y).blockId;
                        int blockId = m.GetInt(3);
                        int placer = -1;
                        if (m.Count > 4)
                            placer = m.GetInt(4);
                        if (bot.playerList.ContainsKey(placer))
                        {
                            Player player = bot.playerList[placer];
                            if (blockId == 32)
                            {
                                if (player.hasVar("brush") && (bool)player.getVar("brush"))
                                {
                                    if (player.hasVar("brushtype"))
                                    {
                                        int brushSize = player.getVar("brushsize") == null ? 1 : (int)player.getVar("brushsize");
                                        int brushBlock = player.getVar("brushblock") == null ? 9 : (int)player.getVar("brushblock");
                                        Brush brush = (Brush)player.getVar("brushtype");
                                        brush.Draw(bot, player, this, x, y);
                                    }
                                }
                                else
                                {
                                    if (editBlock1 != nullPoint && editBlock2 != nullPoint)
                                    {
                                        editBlock1 = nullPoint;
                                        editBlock2 = nullPoint;
                                    }

                                    if (editBlock1 == nullPoint)
                                    {
                                        editBlock1 = new Point(x, y);
                                        bothPointsSet = false;
                                        bot.connection.Send("say", x + y + " First block placed");
                                    }
                                    else if (editBlock2 == nullPoint)
                                    {
                                        editBlock2 = new Point(x, y);
                                        bothPointsSet = true;
                                        bot.connection.Send("say", x + y + " Second block placed");
                                        int temp;
                                        if (editBlock1.X > editBlock2.X)
                                        {
                                            temp = editBlock1.X;
                                            editBlock1.X = editBlock2.X;
                                            editBlock2.X = temp;
                                        }
                                        if (editBlock1.Y > editBlock2.Y)
                                        {
                                            temp = editBlock1.Y;
                                            editBlock1.Y = editBlock2.Y;
                                            editBlock2.Y = temp;
                                        }
                                    }
                                    bot.room.DrawBlock(Block.CreateBlock(0, x, y, previousBlock, player.id));
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
                case "bset":
                    {
                        if (args.Length > 2)
                        {
                            if (player.hasVar("brushtype"))
                            {
                                Brush brush = (Brush)player.getVar("brushtype");
                                brush.SetData(args[1], args[2], bot, player);
                            }
                            else
                                bot.connection.Send("say", player.name + ": You have no brush.");
                        }
                        else
                            bot.connection.Send("say", player.name + ": Usage: !brushset <var> <value>");
                    }
                    break;
                case "replace":
                    //if (isBotMod)
                    {
                        int from;
                        int to;
                        if (args.Length > 2 && int.TryParse(args[1], out from) && int.TryParse(args[2], out to))
                        {
                            int fromLayer = from >= 500 ? 1 : 0;
                            int toLayer = to >= 500 ? 1 : 0;
                            if (bothPointsSet)
                            {
                                for (int x = editBlock1.X; x <= editBlock2.X; x++)
                                {
                                    for (int y = editBlock1.Y; y <= editBlock2.Y; y++)
                                    {
                                        if (bot.room.getBlock(fromLayer, x, y).blockId == from)
                                            bot.room.DrawBlock(Block.CreateBlock(toLayer, x, y, to, player.id));
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "set":
                    //if (isBotMod)
                    {
                        if (player.hasVar("brushtype"))
                        {
                            string arg = "";
                            if (args.Length > 1)
                                arg = args[1];
                            Brush brush = (Brush)player.getVar("brushtype");
                            brush.DrawArea(bot, player, this, arg);
                        }
                    }
                    break;
                case "b":
                    //if (isBotMod)
                    {
                        if (args.Length > 1)
                        {
                            string brushName = args[1].Trim().ToLower();
                            Brush brush = (Brush)Brush.FromName(brushName);
                            if (brush != null)
                            {
                                player.setVar("brushtype", brush);
                                bot.connection.Send("say", "Your brush is now: " + brushName);
                            }
                            else
                                bot.connection.Send("say", player.name + ": Brush does not exist.");
                        }
                        else
                            bot.connection.Send("say", player.name + ": Usage: !brush <brushname>");
                    }
                    break;
                case "bon":
                    //if (isBotMod)
                    {
                        if (player.hasVar("brush") && (bool)player.getVar("brush"))
                            bot.connection.Send("say", player.name + ": Brush already ON");
                        else
                        {
                            player.setVar("brush", true);
                            bot.connection.Send("say", player.name + ": Brush ON");
                        }
                    }
                    break;
                case "boff":
                    //if (isBotMod)
                    {
                        if (!player.hasVar("brush"))
                        {
                            bot.connection.Send("say", player.name + ": Brush already OFF");
                        }
                        else
                        {
                            player.setVar("brush", false);
                            bot.connection.Send("say", player.name + ": Brush OFF");
                        }
                    }
                    break;
                case "fillworld":
                    if (args.Length > 1 && isBotMod)
                    {
                        int blockId;
                        Int32.TryParse(args[1], out blockId);
                        int layer = (blockId >= 500) ? 1 : 0;
                        for (int y = 1; y < bot.room.Height - 1; y++)
                        {
                            for (int x = 1; x < bot.room.Width - 1; x++)
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
                                        if (currentBlock.blockId == toReplace && !blocksToCheck.Contains(current) && !blocksToFill.Contains(current) && current.X >= 0 && current.Y >= 0 && current.X <= bot.room.Width && current.Y <= bot.room.Height)
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
                case "replaceworld":
                    if (isBotMod)
                    {
                        if (args.Length > 2 && isBotMod)
                        {
                            int blockId1, blockId2;
                            Int32.TryParse(args[1], out blockId1);
                            Int32.TryParse(args[2], out blockId2);
                            int layer1 = (blockId1 >= 500) ? 1 : 0;
                            int layer2 = (blockId2 >= 500) ? 1 : 0;
                            for (int y = 1; y < bot.room.Height - 1; y++)
                            {
                                for (int x = 1; x < bot.room.Width - 1; x++)
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

        public override bool HasForm
        {
            get { return false; }
        }
    }
}
