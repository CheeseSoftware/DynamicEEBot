using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    public class BlockPos
    {
        public int layer;
        public int x;
        public int y;

        public int l
        {
            get { return layer; }
        }

        public BlockPos(int layer, int x, int y)
        {
            this.layer = layer;
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            BlockPos p = (BlockPos)obj;
            return p.layer == layer && p.x == x && p.y == y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 64;
                hash = hash * 29 + layer.GetHashCode() + x.GetHashCode() + y.GetHashCode() - 1;
                return hash;
            }
        }
    }
}
