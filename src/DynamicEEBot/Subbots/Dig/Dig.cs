using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Graphics.Tools.Noise;

namespace DynamicEEBot
{
    public partial class Dig : SubBot
    {
        protected Queue<Block> dugBlocksToPlaceQueue = new Queue<Block>();
        protected object dugBlocksToPlaceQueueLock = 0;
        protected float[,] digHardness;

        public Dig(Bot bot)
            : base(bot)
        {
            resetDigHardness();
            enabled = true;
        }

        public override void Update(Bot bot)
        {
            lock (dugBlocksToPlaceQueueLock)
            {
                while (dugBlocksToPlaceQueue.Count > bot.room.width * bot.room.height / 20)
                {
                    bot.room.DrawBlock(dugBlocksToPlaceQueue.Dequeue());
                }
            }
        }

        private bool isDigable(int blockId)
        {
            if (blockId >= 142 - 5 && blockId <= 142)
                return true;
            else if (blockId >= 16 && blockId <= 21)
                return true;
            else if (blockId == 197)
                return true;
            else
                return false;
        }

        private void DigBlock(int x, int y, Player player, float digStrength, bool mining)
        {
            if (digHardness == null)
                resetDigHardness();

            if (!(x > 0 && y > 0 && x < bot.room.width && y < bot.room.height))
                return;

            if (digHardness[x, y] <= 0)
                return;

            Block block = bot.room.getBlock(0, x, y);
            int blockId = -1;

            if (mining)
            {
                if (DigBlockMap.blockTranslator.ContainsKey(block.blockId))
                {
                    blockId = 4;
                    InventoryItem temp = DigBlockMap.blockTranslator[block.blockId];
                    if (player.digLevel >= Convert.ToInt32(temp.GetDataAt(5)))
                    {
                        if (digHardness[x, y] <= digStrength)
                        {

                            InventoryItem newsak = new InventoryItem(temp.GetData());
                            player.inventory.AddItem(newsak, 1);
                            player.digXp += Convert.ToInt32(temp.GetDataAt(1));
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }

            switch (block.blockId)
            {
                case 142:
                    blockId = 4;
                    break;

                case 197:
                    blockId = 119;
                    break;

                case 21:
                    blockId = 369;
                    break;

                default:
                    if (blockId == -1)
                        return;
                    else
                        break;
            }

            digHardness[x, y] -= digStrength;

            if (digHardness[x, y] <= 0)
            {
                bot.room.DrawBlock(Block.CreateBlock(0, x, y, blockId, -1));
                lock (dugBlocksToPlaceQueueLock)
                    dugBlocksToPlaceQueue.Enqueue(block);
            }
        }

        private void resetDigHardness()
        {
            digHardness = new float[bot.room.width, bot.room.height];

            for (int y = 0; y < bot.room.height; y++)
            {
                for (int x = 0; x < bot.room.width; x++)
                {
                    resetBlockHardness(x, y, bot.room.getBlock(0, x, y).blockId);
                }
            }
        }

        private void resetBlockHardness(int x, int y, int blockId)
        {
            if (x < 0 || y < 0 || x >= bot.room.width || y >= bot.room.height)
            {
                if (digHardness == null)
                {
                    digHardness = new float[bot.room.width, bot.room.height];
                }
            }

            if (isDigable(blockId))
            {
                digHardness[x, y] = 1F;
            }
            else if (DigBlockMap.blockTranslator.ContainsKey(blockId))
            {
                if (Shop.shopInventory.ContainsKey(DigBlockMap.blockTranslator[blockId].GetName()))
                    digHardness[x, y] = Convert.ToInt32(Shop.shopInventory[DigBlockMap.blockTranslator[blockId].GetName()].GetDataAt(4));
            }
        }


        public override void onEnable(Bot bot)
        {
            lock (bot.playerList)
            {
                foreach (Player p in bot.playerList.Values)
                {
                    p.Load();
                }
            }
        }

        public override void onDisable(Bot bot)
        {
            lock (bot.playerList)
            {
                foreach (Player p in bot.playerList.Values)
                {
                    p.Save();
                }
            }
        }
    }
}
