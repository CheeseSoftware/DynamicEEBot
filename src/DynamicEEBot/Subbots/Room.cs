using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    public class Room : SubBot
    {
        RoomData data;
        public SafeDictionary<BlockPos, Block> blocksToPlace = new SafeDictionary<BlockPos, Block>();
        public bool loadedWorld = false;
        private Thread drawRepairThread;

        public Room(Bot bot)
            : base(bot)
        {
            data = new RoomData();
            ResetMap();
            drawRepairThread = new Thread(DrawRepair);
            drawRepairThread.Start();
            enabled = true;
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            switch (m.Type)
            {
                case "init":
                    {
                        loadedWorld = false;
                        data.owner = m.GetString(0);
                        data.name = m.GetString(1);
                        data.totalPlays = m.GetInt(2);
                        data.woots = m.GetInt(3);
                        data.totalWoots = m.GetInt(4);
                        data.key = derot(m.GetString(5));
                        Player player = new Player(bot, m.GetInt(6), m.GetString(9), 0, m.GetInt(7), m.GetInt(8), false, false, false, 0, false, false, 0);
                        lock (bot.playerList)
                            bot.playerList.Add(player.id, player);
                        data.width = m.GetInt(12);
                        data.height = m.GetInt(13);
                    }
                    goto case "reset";
                case "reset":
                    {
                        loadedWorld = false;
                        ResetMap();
                        data.DeSerialize(m);
                        loadedWorld = true;
                    }
                    break;
                case "b":
                    {
                        int layer = m.GetInt(0);
                        int x = m.GetInt(1);
                        int y = m.GetInt(2);
                        int blockID = m.GetInt(3);
                        int placer = m.GetInt(4);
                        Block b = Block.CreateBlock(layer, x, y, blockID, placer);
                        data.blockMap[layer, x, y].Add(b);
                        //lock (blocksToPlace)
                        {
                            BlockPos p = new BlockPos(b.layer, b.x, b.y);
                            while (blocksToPlace.ContainsKey(p))
                            {
                                blocksToPlace.Remove(p);
                            }
                        }
                    }
                    break;
                case "updatemeta":
                    {
                        data.owner = m.GetString(0);
                        data.name = m.GetString(1);
                        data.totalPlays = m.GetInt(2);
                    }
                    break;
                case "bc":
                    data.blockMap[0, m.GetInt(0), m.GetInt(1)].Add(Block.CreateBlockCoin(m.GetInt(0), m.GetInt(1), m.GetInt(2), m.GetInt(3)));
                    break;
                case "bs":
                    data.blockMap[0, m.GetInt(0), m.GetInt(1)].Add(Block.CreateNoteBlock(m.GetInt(0), m.GetInt(1), m.GetInt(2), m.GetInt(3)));
                    break;
                case "pt":
                    data.blockMap[0, m.GetInt(0), m.GetInt(1)].Add(Block.CreatePortal(m.GetInt(0), m.GetInt(1), m.GetInt(3), m.GetInt(4), m.GetInt(5)));
                    break;
            }
        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {
            blocksToPlace.Clear();
            drawRepairThread.Abort();
        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            switch (args[0])
            {
                case "rsize":
                    {
                        bot.connection.Send("say", "Size: " + blocksToPlace.Count);
                    }
                    break;
                case "checkblock":
                    {
                        Block b = data.blockMap[0, player.blockX, player.blockY].Last();
                        int id = -1;
                        if (b != null)
                            id = b.b_userId;
                        bot.connection.Send("say", "Block is placed by " + bot.playerList[id].name);
                    }
                    break;
                case "pos":
                    {
                        Player p = bot.playerList[player.id];
                        bot.connection.Send("say", "Your position: X:" + p.blockX + " Y:" + p.blockY);
                    }
                    break;
            }
        }

        private string derot(string arg1)
        {
            int num = 0;
            string str = "";
            for (int i = 0; i < arg1.Length; i++)
            {
                num = arg1[i];
                if ((num >= 0x61) && (num <= 0x7a))
                {
                    if (num > 0x6d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                else if ((num >= 0x41) && (num <= 90))
                {
                    if (num > 0x4d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                str = str + ((char)num);
            }
            return str;
        }

        public Block getBlock(int layer, int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return data.blockMap[layer, x, y].Last();
            }
            return Block.CreateBlock(layer, x, y, 0, -1);
        }

        public void ResetMap()
        {
            blocksToPlace.Clear();
            data.blockMap = new List<Block>[4, width, height];
            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        data.blockMap[i, x, y] = new List<Block>();
                        data.blockMap[i, x, y].Add(Block.CreateBlock(i, x, y, 0, -1));
                    }
                }
            }
        }

        private void DrawRepair()
        {
            while (true)
            {
                foreach (Block b in blocksToPlace.Values)
                {
                    b.Send(bot);
                    Thread.Sleep(8);
                }
            }
        }

        public void DrawBlock(Block b)
        {
            if (b.x > 0 && b.x < width - 1 && b.y > 0 && b.y < height - 1)
            {
                BlockPos p = new BlockPos(b.layer, b.x, b.y);
                if (!blocksToPlace.ContainsKey(p))
                {
                    if (data.blockMap[b.layer, b.x, b.y].Last().blockId != b.blockId)
                    {
                        lock (blocksToPlace)
                        {
                            blocksToPlace.Add(p, b);
                        }
                    }
                }
                else
                {
                    while (blocksToPlace.ContainsKey(p))
                        blocksToPlace.Remove(p);
                    DrawBlock(b);
                }
            }
        }

        public string owner
        {
            get { return data.owner; }
        }

        public string name
        {
            get { return data.name; }
        }

        public int totalPlays
        {
            get { return data.totalPlays; }
        }

        public int woots
        {
            get { return data.woots; }
        }

        public int totalWoots
        {
            get { return data.totalWoots; }
        }

        public int width
        {
            get { return data.width; }
        }

        public int height
        {
            get { return data.height; }
        }

        public string key
        {
            get { return data.key; }
        }

        public override void onEnable(Bot bot)
        {
            //throw new NotImplementedException();
        }

        public override void onDisable(Bot bot)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Bot bot)
        {
            //throw new NotImplementedException();
        }
    }
}
