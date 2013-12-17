using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    class Door : Lamp
    {
        public override void Update(Bot bot, System.Diagnostics.Stopwatch currentRedTime, BlockPos pos)
        {
            if (enabled)
            {
                if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 86/*scifi gray*/)
                {
                    bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 243/*secrets nonsolid*/, -2));
                }
            }
            else if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 243/*secrets nonsolid*/)
            {
                bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 86/*scifi gray*/, -2));
            }
            enabled = false;
        }
    }
}
