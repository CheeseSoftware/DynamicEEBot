using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit.BrushTypes
{
    class SquareBrushShape : BrushShape
    {
        public override string Name
        {
            get { return "Square"; }
        }

        public override List<System.Drawing.Point> getBlocks(int size, int x, int y, Bot bot)
        {
            List<Point> blocks = new List<Point>();
            for (int i = x - size + 1; i < x + size; i++)
            {
                for (int j = y - size + 1; j < y + size; j++)
                {
                    blocks.Add(new Point(i, j));
                }
            }
            return blocks;
        }
    }
}
