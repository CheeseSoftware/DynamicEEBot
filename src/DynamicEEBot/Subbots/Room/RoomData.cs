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
        public List<Block>[, ,] blockMap;

    }
}
