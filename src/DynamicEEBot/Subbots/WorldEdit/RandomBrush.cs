using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
{
    class RandomBrush : Brush
    {
        Random r = new Random();
        Dictionary<int, double> chances = new Dictionary<int, double>();
        int totalChance = 0;

        public override string Type
        {
            get { return "random"; }
        }

        public override void SetData(string key, string value, Bot bot, Player player)
        {
            switch (key)
            {
                case "chance":
                    {
                        chances.Clear();
                        totalChance = 0;
                        string[] temp = value.Split(',');
                        for (int i = 0; i < temp.Length; i++)
                        {
                            string[] percentBlock = temp[i].Split('%');
                            int percent = 0;
                            int block = 0;
                            int.TryParse(percentBlock[0], out percent);
                            if (percentBlock.Length > 1)
                                int.TryParse(percentBlock[1], out block);
                            if (percent != 0 && !chances.ContainsKey(block))
                                chances.Add(block, percent);
                            totalChance += percent;
                        }
                        bot.connection.Send("say", player.name + ": Chance set. Total: " + chances.Count);
                    }
                    return;
            }
            base.SetData(key, value, bot, player);
        }

        public override void DrawArea(Bot bot, Player player, WorldEdit worldEdit, string arg = "")
        {
            if (worldEdit.bothPointsSet)
            {
                for (int x = worldEdit.editBlock1.X; x <= worldEdit.editBlock2.X; x++)
                {
                    for (int y = worldEdit.editBlock1.Y; y <= worldEdit.editBlock2.Y; y++)
                    {
                        int random = r.Next(totalChance);
                        int block = 0;
                        int current = 0;
                        foreach (KeyValuePair<int, double> pair in chances)
                        {
                            int blabla = current + (int)Math.Round((pair.Value / totalChance) * totalChance);
                            if (random >= current && random <= blabla)
                            {
                                block = pair.Key;
                                bot.room.DrawBlock(Block.CreateBlock(block >= 500 ? 1 : 0, x, y, block, player.id));
                                break;
                            }
                            current += (blabla - current);
                        }
                    }
                }
            }
        }

        public override void Draw(Bot bot, Player player, WorldEdit worldEdit, int x, int y, string arg = "")
        {
            List<Point> blocks = shape.getBlocks(size, x, y, bot);
            foreach (Point p in blocks)
            {
                int random = r.Next(totalChance + 1);
                int block = 0;
                int current = 0;
                foreach (KeyValuePair<int, double> pair in chances)
                {
                    int blabla = current + (int)Math.Round((pair.Value / totalChance) * totalChance);
                    if (random >= current && random <= blabla)
                    {
                        block = pair.Key;
                        bot.room.DrawBlock(Block.CreateBlock(block >= 500 ? 1 : 0, p.X, p.Y, block, player.id));
                        break;
                    }
                    current += (blabla - current);
                }
            }
        }
    }
}
