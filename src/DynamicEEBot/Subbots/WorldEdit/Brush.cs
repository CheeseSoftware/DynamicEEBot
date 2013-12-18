using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
{
    abstract class Brush
    {
        public abstract string Type { get; }
        public abstract void SetData(string key, string value, Bot bot, Player player);
        public abstract void DrawArea(Bot bot, Player player, WorldEdit worldEdit, string arg = "");
        public abstract void Draw(Bot bot, Player player, WorldEdit worldEdit, int x, int y, string arg = "");
        public static Brush FromName(string name)
        {
            switch (name)
            {
                case "solid":
                    return new SolidBrush();
                case "random":
                    return new RandomBrush();
                default:
                    break;
            }
            return null;
        }
    }
}
