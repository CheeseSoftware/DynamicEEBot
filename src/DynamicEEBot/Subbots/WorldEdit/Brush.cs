using DynamicEEBot.SubBots.WorldEdit.BrushTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
{
    abstract class Brush
    {
        protected int size = 1;
        protected BrushShape shape = new RoundBrushShape();
        public int Size { get { return size; } set { size = value; } }
        public BrushShape Shape { get { return shape; } set { shape = value; } }

        public abstract string Type { get; }
        public virtual void SetData(string key, string value, Bot bot, Player player)
        {
            switch (key)
            {
                case "size":
                case "radius":
                    {
                        if (int.TryParse(value, out size))
                        {
                            bot.connection.Send("say", player.name + ": Size: " + size);
                        }
                        else
                            bot.connection.Send("say", player.name + ": That is not a size.");
                    }
                    break;
                case "shape":
                    {
                        BrushShape brushShape = BrushShape.FromName(value);
                        if (brushShape != null)
                        {
                            shape = brushShape;
                            bot.connection.Send("say", player.name + ": Shape: " + brushShape.Name);
                        }
                        else
                            bot.connection.Send("say", player.name + ": Shape doesn't exist.");
                    }
                    break;
            }
        }
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
