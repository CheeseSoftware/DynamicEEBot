using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit.BrushTypes
{
    abstract class BrushShape
    {
        public abstract List<Point> getBlocks(int size, int x, int y, Bot bot);
        public abstract string Name { get; }
        public static BrushShape FromName(string name)
        {
            switch (name)
            {
                case "round":
                    return new RoundBrushShape();
                case "square":
                    return new SquareBrushShape();
                default:
                    return null;
            }
        }
    }
}
