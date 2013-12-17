using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    class Lamp : Destination
    {
        protected bool enabled = false;

        public override void onSignal(System.Diagnostics.Stopwatch currentRedTime, float power)
        {
            enabled = true;
            base.onSignal(currentRedTime, power);
        }

        public override void Update(Bot bot, System.Diagnostics.Stopwatch currentRedTime, BlockPos pos)
        {
            if (enabled)
            {
                if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 33/*glossy black special*/)
                {
                    bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 143/*cloud white*/, -2));
                }
            }
            else if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 143/*cloud white*/)
            {
                bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 33/*glossy black special*/, -2));
            }
            enabled = false;
            base.Update(bot, currentRedTime, pos);
        }
    }
}
