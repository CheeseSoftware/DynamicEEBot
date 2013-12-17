using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.Subbots.WorldEdit
{
    class SolidBrush : Brush
    {
        int blockId = 8;
        int radius = 1;

        public override string Type
        {
            get { return "random"; }
        }

        public override void SetData(string key, string value, Bot bot, Player player)
        {
            switch (key)
            {
                case "id":
                    {
                        int.TryParse(value, out blockId);
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
            int id = blockId;
            if (arg != "")
                int.TryParse(arg, out id);
            if (worldEdit.bothPointsSet)
            {
                for (int x = worldEdit.editBlock1.X; x <= worldEdit.editBlock2.X; x++)
                {
                    for (int y = worldEdit.editBlock1.Y; y <= worldEdit.editBlock2.Y; y++)
                    {
                        bot.room.DrawBlock(Block.CreateBlock(id >= 500 ? 1 : 0, x, y, id, player.id));
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
                        bot.room.DrawBlock(Block.CreateBlock(blockId >= 500 ? 1 : 0, tempx, tempy, blockId, player.id));
                        lastX = tempx;
                        lastY = tempy;
                    }
                }
            }
        }
    }
}
