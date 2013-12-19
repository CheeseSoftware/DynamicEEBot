using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.WorldEdit
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
                    return;
            }
            base.SetData(key, value, bot, player);
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
            List<Point> blocks = shape.getBlocks(size, x, y, bot);
            foreach (Point p in blocks)
                bot.room.DrawBlock(Block.CreateBlock(blockId >= 500 ? 1 : 0, p.X, p.Y, blockId, player.id));
        }
    }
}
