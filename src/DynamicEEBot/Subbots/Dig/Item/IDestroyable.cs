using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.Dig.Item
{
    interface IDestroyable
    {
        int Durability { get; }
        int Hardness { get; }
        void onDamage(int damage);
    }
}
