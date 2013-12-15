using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    class RoomData
    {
        public string owner;
        public string name;
        public int totalPlays;
        public int woots;
        public int totalWoots;
        public int width;
        public int height;
        public string key;
        public Block[, ,] blockMap;

        public void DeSerialize(Message m)
        {
            byte[] xByteArray;
            byte[] yByteArray;
            for (uint i = 0; i < m.Count; i++)
            {
                if (m[i] is string && m.GetString(i) == "ws")
                {
                    //world start
                    for (i = i + 1; !(m[i] is string && m.GetString(i) == "we"); i++)
                    {
                        if (m[i - 1] is string && m.GetString(i - 1) == "we")
                        {
                            i -= 1;
                            break;
                        }
                        if (m[i] is byte[])
                        {
                            if (m[i + 1] is byte[])
                            {
                                int blockId = m.GetInt(i - 2);
                                int layer = m.GetInt(i - 1);
                                xByteArray = (byte[])m[i];
                                yByteArray = (byte[])m[i + 1];
                                for (int x = 0; x < xByteArray.Count(); x += 2)
                                {
                                    int xIndex = (xByteArray[x] * 256) + xByteArray[x + 1];
                                    int yIndex = (yByteArray[x] * 256) + yByteArray[x + 1];
                                    if (blockId == 242)
                                    {
                                        int rotation = m.GetInt(i);
                                        int thisId = m.GetInt(i + 1);
                                        int targetId = m.GetInt(i + 2);
                                        blockMap[layer, xIndex, yIndex] = Block.CreatePortal(xIndex, yIndex, rotation, thisId, targetId);
                                    }
                                    else
                                        blockMap[layer, xIndex, yIndex] = Block.CreateBlock(layer, xIndex, yIndex, blockId, -1);
                                }
                                i += 1;
                            }
                            i += 1;
                        }
                    }
                    //world end
                }
                else if (m[i] is string && m.GetString(i) == "ps")
                {
                    //potion start
                    for (i = i + 1; !(m[i] is string && m.GetString(i) == "pe"); i++)
                    {
                    }
                    //potion end
                }
            }
        }
    }
}
