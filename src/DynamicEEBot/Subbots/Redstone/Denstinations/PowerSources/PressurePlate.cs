using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DynamicEEBot
{
    class PressurePlate : PowerSource
    {
        bool enabled = false;

        public override float getOutput(Stopwatch currentRedTime)
        {
            if (enabled)
                return 1.0F;
            else
                return 0.0F;
        }

        public override void Update(Bot bot, Stopwatch currentRedTime, BlockPos pos)
        {
            enabled = false;
            lock (bot.playerList)
            {
                foreach (var p in bot.playerList)
                {
                    if (p.Value.blockX == pos.x && p.Value.blockY == pos.y)
                    {
                        enabled = true;
                        break;
                    }
                }
            }

            if (!enabled)
            {
                if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 301/*sand white*/)
                {
                    bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 311/*cloud bottom*/, -2));
                }
            }
            else if (bot.room.getBlock(pos.l, pos.x, pos.y).blockId == 311/*cloud bottom*/)
            {
                bot.room.DrawBlock(Block.CreateBlock(pos.l, pos.x, pos.y, 301/*sand white*/, -2));
            }
        }

        public virtual void onSignal(Stopwatch currentRedTime, float power)
        {

        }

    }
}
