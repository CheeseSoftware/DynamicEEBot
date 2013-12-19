using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit.BrushTypes
{
    class RoundBrushShape : BrushShape
    {
        public override string Name
        {
            get { return "Round"; }
        }

        public override List<Point> getBlocks(int size, int x, int y, Bot bot)
        {
            List<Point> blocks = new List<Point>();
            /*int lastX = -1;
            int lastY = -1;
            for (int a = size; a > 0; a--)
            {
                for (double i = 0.0; i < 360.0; i += 0.1)
                {
                    double mAngle = i * System.Math.PI / 180;
                    int tempx = x + (int)(a * System.Math.Cos(mAngle));
                    int tempy = y + (int)(a * System.Math.Sin(mAngle));
                    if ((lastX != tempx || lastY != tempy) && tempx > 0 && tempx < bot.room.Width && tempy > 0 && tempy < bot.room.Height)
                    {
                        blocks.Add(new Point(tempx, tempy));
                        lastX = tempx;
                        lastY = tempy;
                    }
                }
            }*/
            Point center = new Point(x, y);
            double distanceToCenter;
            int deltaX;
            int deltaY;
            for (int i = x - size; i <= x + size; i++)
            {
                for (int j = y - size; j <= y + size; j++)
                {
                    deltaX = Math.Abs(i - center.X);
                    deltaY = Math.Abs(j - center.Y);
                    distanceToCenter = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                    if (distanceToCenter <= size)
                    {
                        blocks.Add(new Point(i, j));
                    }
                }
            }
            return blocks;
        }
    }
}
