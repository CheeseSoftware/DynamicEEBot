using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
{
    class RandomBrush : Brush
    {
        Random r = new Random();
        Dictionary<int, double> chances = new Dictionary<int, double>();
        int totalChance = 0;
        int radius = 1;

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
                            if (percent != 0)
                                chances.Add(block, percent);
                            totalChance += percent;
                        }
                        bot.connection.Send("say", player.name + ": Chance set. Total: " + chances.Count);
                    }
                    break;
                case "size":
                case "radius":
                    {
                        int.TryParse(value, out radius);
                    }
                    break;
            }
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
            int lastX = -1;
            int lastY = -1;
            for (int a = radius; a > 0; a--)
            {
                for (double i = 0.0; i < 360.0; i += 0.1)
                {
                    double mAngle = i * System.Math.PI / 180;
                    int tempx = x + (int)(a * System.Math.Cos(mAngle));
                    int tempy = y + (int)(a * System.Math.Sin(mAngle));
                    if ((lastX != tempx || lastY != tempy) && tempx > 0 && tempx < bot.room.Width && tempy > 0 && tempy < bot.room.Height)
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
                                bot.room.DrawBlock(Block.CreateBlock(block >= 500 ? 1 : 0, tempx, tempy, block, player.id));
                                lastX = tempx;
                                lastY = tempy;
                                break;
                            }
                            current += (blabla - current);
                        }
                    }
                }
            }
        }
    }
}
