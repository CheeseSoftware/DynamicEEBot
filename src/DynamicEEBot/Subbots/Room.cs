using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PlayerIOClient;

namespace DynamicEEBot
{
    public class Room : SubBot
    {
        //public SafeDictionary<BlockPos, Block> blocksToPlace = new SafeDictionary<BlockPos, Block>();

        private Thread drawThread;

        List<Block>[][,] blockMap;
        Queue<Block> blockQueue;
        Queue<Block> blockRepairQueue;
        HashSet<Block> blockSet;

        public bool loadedWorld = false;
        private string owner;
        private string name;
        private int totalPlays;
        private int woots;
        private int totalWoots;
        private int width;
        private int height;
        private string key;
        public int drawSleep = 5;

        public Room(Bot bot)
            : base(bot)
        {
            enabled = true;
        }

        public override void onEnable(Bot bot)
        {
            blockMap = new List<Block>[2][,];
            blockQueue = new Queue<Block>();
            blockRepairQueue = new Queue<Block>();
            blockSet = new HashSet<Block>();
            //ResetMap();
        }

        public override void onDisable(Bot bot)
        {
            lock (blockMap)
                blockMap = null;
            lock (blockQueue)
                blockQueue = null;
            lock (blockRepairQueue)
                blockRepairQueue = null;
            lock (blockSet)
                blockSet = null;
            StopDrawThread();
        }

        private void StopDrawThread()
        {
            if(drawThread != null)
            {
                if(drawThread.IsAlive)
                    drawThread.Abort();
            }
        }

        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            switch (m.Type)
            {
                case "init":
                    {
                        loadedWorld = false;
                        owner = m.GetString(0);
                        name = m.GetString(1);
                        totalPlays = m.GetInt(2);
                        woots = m.GetInt(3);
                        totalWoots = m.GetInt(4);
                        key = BotUtility.rot13(m.GetString(5));
                        //Player player = new Player(bot, m.GetInt(devil), m.GetString(9), 0, m.GetInt(7), m.GetInt(music), false, false, false, 0, false, false, 0);
                        //lock (bot.playerList)
                        //  bot.playerList.Add(player.id, player);
                        width = m.GetInt(12);
                        height = m.GetInt(13);


                        ResetMap();
                        LoadMap(m, 18);
                        loadedWorld = true;

                        bool isOwner = m.GetBoolean(11);
                        if (isOwner)
                            StartDrawThread();
                    }
                    break;
                case "reset":
                    {
                        loadedWorld = false;
                        //ResetMap();
                        LoadMap(m, 0);
                        loadedWorld = true;
                    }
                    break;

                case "access":
                    StartDrawThread();
                    break;

                case "lostaccess":
                    StopDrawThread();
                    break;

                case "b":
                case "bc":
                case "bs":
                case "pt":
                    {
                        /*int layer = m.GetInt(0);
                        int x = m.GetInt(1);
                        int y = m.GetInt(2);
                        int blockID = m.GetInt(3);
                        int placer = -1;
                        if (m.Count > 4)
                            placer = m.GetInt(4);
                        Block b = Block.CreateBlock(layer, x, y, blockID, placer);*/
                        Block b = new Block(m);

                        this.OnBlockDraw(b);

                    }
                    break;
                case "updatemeta":
                    {
                        owner = m.GetString(0);
                        name = m.GetString(1);
                        totalPlays = m.GetInt(2);
                    }
                    break;
            }
        }

        private void StartDrawThread()
        {
            bot.connection.Send(key + "k", true);
            if (drawThread == null || !drawThread.IsAlive)
            {
                Thread thread = new Thread(() =>
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    while (bot.connected)
                    {
                        while (bot.hasCode)
                        {
                            lock (blockQueue)
                            {
                                if (blockQueue.Count != 0)
                                {

                                    if (blockSet.Contains(blockQueue.Peek()))
                                    {
                                        //Console.WriteLine("jag är en sjuk sak");
                                        blockQueue.Peek().Send(bot);//.Send(bot.connection);
                                        lock (blockRepairQueue)
                                            blockRepairQueue.Enqueue(blockQueue.Dequeue());
                                        //Console.WriteLine("!!");
                                    }
                                    else
                                    {
                                        blockQueue.Dequeue();
                                        continue;
                                    }
                                }
                                else if (blockRepairQueue.Count != 0)
                                {
                                    while (!blockSet.Contains(blockRepairQueue.Peek()))
                                    {
                                        blockRepairQueue.Dequeue();
                                        if (blockRepairQueue.Count == 0)
                                            break;
                                    }

                                    if (blockRepairQueue.Count == 0)
                                        continue;

                                    blockRepairQueue.Peek().Send(bot);
                                    blockRepairQueue.Enqueue(blockRepairQueue.Dequeue());
                                }
                                else
                                {
                                    Thread.Sleep(5);
                                    continue;
                                }
                                double sleepTime = drawSleep - stopwatch.Elapsed.TotalMilliseconds;
                                if (sleepTime >= 0.5)
                                {
                                    Thread.Sleep((int)sleepTime);
                                }
                                stopwatch.Reset();
                            }
                        }
                        Thread.Sleep(100);
                    }
                });

                drawThread = thread;
                drawThread.Start();
            }
        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {
            //blocksToPlace.Clear();
            StopDrawThread();
        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            switch (args[0])
            {
                case "rsize":
                    lock (blockSet)
                    {
                        bot.connection.Send("say", "Size: " + blockSet.Count);
                    }
                    break;
                case "getplacer":
                    {
                        Block b = blockMap[0][player.blockX, player.blockY].Last();
                        int id = -1;
                        if (b != null)
                            id = b.b_userId;
                        bot.connection.Send("say", "Block is placed by " + bot.playerList[id].name);
                    }
                    break;
            }
        }

        public Block getBlock(int layer, int x, int y)
        {
            return getBlock(layer, x, y, 0);
        }

        public Block getBlock(int layer, int x, int y, int rollbacks)
        {
            while (blockMap == null || blockMap[layer] == null)
                Thread.Sleep(100);

            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                lock (blockMap)
                {
                    if (blockMap[layer][x, y].Count > 0)
                    {
                        if (blockMap[layer][x, y].Count <= rollbacks)
                            return Block.CreateBlock(layer, x, y, 0, -1);
                        else
                            return blockMap[layer][x, y][blockMap[layer][x, y].Count - 1 - rollbacks];
                    }
                }
            }
            return Block.CreateBlock(layer, x, y, 0, -1);
        }

        public void ResetMap()
        {
            lock (blockSet)
                blockSet.Clear();//blocksToPlace.Clear();
            for (int i = 0; i < 2; i++)
            {
                lock (blockMap)
                {
                    blockMap[i] = new List<Block>[width, height];
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            blockMap[i][x, y] = new List<Block>();
                        }
                    }
                }
            }
        }

        private void OnBlockDraw(Block b)
        {
            while (blockMap == null || blockMap[b.layer] == null)
                Thread.Sleep(50);

            lock (blockMap)
            {
                blockMap[b.layer][b.x, b.y].Add(b);
            }

            lock (blockSet)
            {
                while (blockSet.Contains(b))
                {
                    blockSet.Remove(b);
                }
                
                foreach (Block b2 in blockSet)
                {
                    if (b.Equals(b2))
                    {
                        lock (blockMap)
                        {
                            blockMap[b.layer][b.x, b.y].Add(b2);
                        }

                        blockSet.Remove(b2);
                        break;
                    }
                }
            }
        }

        public void DrawBlock(Block b)
        {

            if (b == null)
                return;
            if (Block.Compare(getBlock(b.layer, b.x, b.y), b))
                return;

            if (b.x > 0 && b.x < width - 1 && b.y > 0 && b.y < height - 1)
            {

                lock (blockSet)
                {
                    foreach (Block b2 in blockSet)
                    {
                        if (b == b2)
                        {
                           return;
                        }
                        else if (b2.layer == b.layer && b2.x == b.x && b2.y == b.y)
                        {
                            blockSet.Remove(b2);
                            break;
                        }
                    }

                    blockSet.Add(b);
                }


                lock (blockQueue)
                    blockQueue.Enqueue(b);
            }
        }

        private void LoadMap(Message m, uint position)
        {
            lock (blockMap)
            {
                byte[] xByteArray;
                byte[] yByteArray;
                for (uint i = position; i < m.Count; i++)
                {
                    if (m[i] is byte[])
                    {
                        int blockID = m.GetInt(i - 2);
                        int layer = m.GetInt(i - 1);
                        xByteArray = m.GetByteArray(i);
                        yByteArray = m.GetByteArray(i + 1);
                        int xIndex = 0;
                        int yIndex = 0;
                        i += 2;
                        for (int x = 0; x < xByteArray.Length; x += 2)
                        {
                            xIndex = (xByteArray[x] * 256) + xByteArray[x + 1];
                            yIndex = (yByteArray[x] * 256) + yByteArray[x + 1];

                            Block block;

                            switch (blockID)
                            {
                                case 165: //coin gate
                                case 43: //coin door
                                    {
                                        int coinsToOpen = m.GetInt(i);
                                        block = Block.CreateBlockCoin(xIndex, yIndex, blockID, coinsToOpen);
                                        break;
                                    }

                                case 83: //PERCUSSION
                                case 77: //piano
                                    {
                                        int soundId = m.GetInt(i);
                                        block = Block.CreateNoteBlock(xIndex, yIndex, blockID, soundId);
                                        break;
                                    }

                                case 381: //invisible portal
                                case 242: //portal
                                    {
                                        int rotation = m.GetInt(i);
                                        int id = m.GetInt(i + 1);
                                        int target = m.GetInt(i + 1);
                                        block = Block.CreatePortal(xIndex, yIndex, rotation, id, target);
                                        break;
                                    }

                                case 1000:
                                    {
                                        string text = m.GetString(i);
                                        block = Block.CreateText(xIndex, yIndex, text);
                                    }
                                    break;

                                case 361: //spikes
                                    {
                                        int rotation = m.GetInt(i);
                                        block = Block.CreateSpike(xIndex, yIndex, rotation);
                                        break;
                                    }

                                case 374: // world portal
                                default:
                                    block = Block.CreateBlock(layer, xIndex, yIndex, blockID, -1);
                                    break;

                            }
                            blockMap[layer][xIndex, yIndex].Add(block);//blockID;
                        }
                    }
                }
                DrawBorder();
            }
        }

        public void SetSleepTime(int sleep)
        {
            this.drawSleep = sleep;
        }

        public void DrawBorder()
        {
            lock (blockMap)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (x == 0 || y == 0 || x == width - 1 || y == width - 1)
                        {
                            blockMap[0][x, y].Clear();
                            blockMap[0][x, y].Add(Block.CreateBlock(0, x, y, 9, -1));
                            //Console.WriteLine("Border at " + x + " " + y);
                        }
                    }
                }
            }
        }

        public string Owner
        {
            get { return owner; }
        }

        public string Name
        {
            get { return name; }
        }

        public int TotalPlays
        {
            get { return totalPlays; }
        }

        public int Woots
        {
            get { return woots; }
        }

        public int TotalWoots
        {
            get { return totalWoots; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string Key
        {
            get { return key; }
        }

        public override void Update(Bot bot)
        {
            //throw new NotImplementedException();
        }
    }
}
