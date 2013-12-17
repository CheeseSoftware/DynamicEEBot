﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    struct Wire
    {
        public BlockPos position;
        public float resistance;

        public Wire(BlockPos position, float resistance)
        {
            this.position = position;
            this.resistance = resistance;
        }
    }
}
