using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PlayerIOClient;

namespace DynamicEEBot.SubBots
{
    public partial class Room : SubBot
    {
        List<Block>[][,] blockMap;
        Queue<Block> blockQueue;
        Queue<Block> blockRepairQueue;
        HashSet<Block> blockSet;

        public bool loadedWorld = false;
        private Thread drawThread;
        private string owner;
        private string name;
        private int totalPlays;
        private int woots;
        private int totalWoots;
        private int width;
        private int height;
        private string key;
        private int drawSleep = 5;

        public Room(Bot bot)
            : base(bot)
        {
            form = new Room_Form(bot, this);
            Enabled = true;
        }

        #region draw functions
        public void ClearRepairQueue()
        {
            lock (blockQueue)
                blockQueue.Clear();
            lock (blockRepairQueue)
                blockRepairQueue.Clear();
            lock (blockSet)
                blockSet.Clear();
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
                                        blockQueue.Peek().Send(bot);
                                        lock (blockRepairQueue)
                                            blockRepairQueue.Enqueue(blockQueue.Dequeue());
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

        private void StopDrawThread()
        {
            if (drawThread != null)
            {
                if (drawThread.IsAlive)
                    drawThread.Abort();
            }
        }
        #endregion

        #region block get functions
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
        #endregion

        #region block draw functions
        /// <summary>
        /// Körs när "b" tas emot och lägger blocket i blockmap
        /// </summary>
        /// <param name="b"></param>
        public Block getBotBlock(int layer, int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                lock (blockSet)
                {
                    foreach (Block b in blockSet)
                    {
                        if (b.x == x && b.y == y && b.layer == layer)
                            return b;
                    }
                }

                while (blockMap == null)
                    Thread.Sleep(100);

                while (blockMap[layer] == null)
                    Thread.Sleep(100);

                lock (blockMap)
                {
                    if (blockMap[layer][x, y].Count > 0)
                    {
                        return blockMap[layer][x, y][blockMap[layer][x, y].Count - 1];
                    }
                }
            }
            return Block.CreateBlock(layer, x, y, 0, -1);
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
                this.form.Invoke(new Action(() =>
                {
                    ((Room_Form)this.form).repairQueueSizeBox.Text = blockSet.Count.ToString();
                }));
            }
        }

        /// <summary>
        /// Skickar blocket om det inte redan finns i blockskickningskön
        /// </summary>
        /// <param name="b"></param>
        public void DrawBlock(Block b)
        {

            if (b == null)
                return;
            if (Block.Compare(getBotBlock(b.layer, b.x, b.y), b))
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
        #endregion

        #region world functions
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
                                    Block block;

                                    switch (blockId)
                                    {
                                        case 165: //coin gate
                                        case 43: //coin door
                                            {
                                                int coinsToOpen = m.GetInt(i + 2);
                                                block = Block.CreateBlockCoin(xIndex, yIndex, blockId, coinsToOpen);
                                                break;
                                            }
                                        case 83: //drums
                                        case 77: //piano
                                            {
                                                int soundId = m.GetInt(i + 2);
                                                block = Block.CreateNoteBlock(xIndex, yIndex, blockId, soundId);
                                                break;
                                            }
                                        case 381: //invisible portal
                                        case 242: //portal
                                            {
                                                int rotation = m.GetInt(i + 2);
                                                int myId = m.GetInt(i + 3);
                                                int targetId = m.GetInt(i + 4);
                                                block = Block.CreatePortal(xIndex, yIndex, rotation, myId, targetId);
                                                break;
                                            }
                                        case 385: //sign
                                        case 374: //world portal
                                        case 1000: //admin text
                                            {
                                                string text = m.GetString(i + 2);
                                                block = Block.CreateText(xIndex, yIndex, text);
                                            }
                                            break;
                                        case 361: //spikes
                                            {
                                                int rotation = m.GetInt(i + 2);
                                                block = Block.CreateSpike(xIndex, yIndex, rotation);
                                                break;
                                            }
                                        default:
                                            block = Block.CreateBlock(layer, xIndex, yIndex, blockId, -1);
                                            break;
                                    }
                                    blockMap[layer][xIndex, yIndex].Add(block);
                                }
                                i += 1;
                            }
                            i += 1;
                        }
                        if (m[i - 1] is string && m.GetString(i - 1) == "we")
                        {
                            i -= 1;
                            break;
                        }
                        if (m[i] is string && m.GetString(i) == "we")
                        {
                            i -= 1;
                            break;
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
            DrawBorder();
        }

        public void ResetMap()
        {
            lock (blockSet)
                blockSet.Clear();
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
                        }
                    }
                }
            }
        }
        #endregion

        public int DrawSleep { get { return this.drawSleep; } set { this.drawSleep = value; } }

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
    }
}
