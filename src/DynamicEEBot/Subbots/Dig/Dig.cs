﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Graphics.Tools.Noise;
using DynamicEEBot.SubBots.Dig.Item;
using System.Diagnostics;

namespace DynamicEEBot.SubBots.Dig
{
    public partial class Dig : SubBot
    {
        protected Queue<Block> dugBlocksToPlaceQueue = new Queue<Block>();
        protected object dugBlocksToPlaceQueueLock = 0;
        protected float[,] digHardness;
        private Stopwatch playerSaveStopwatch;

        public Dig(Bot bot)
            : base(bot)
        {
            resetDigHardness();
            playerSaveStopwatch = new Stopwatch();
            playerSaveStopwatch.Start();
        }

        public override void Update(Bot bot)
        {
            lock (dugBlocksToPlaceQueueLock)
            {
                while (dugBlocksToPlaceQueue.Count > bot.room.Width * bot.room.Height / 20)
                {
                    bot.room.DrawBlock(dugBlocksToPlaceQueue.Dequeue());
                }
            }
            if (playerSaveStopwatch.ElapsedMilliseconds >= 5000)
            {
                lock (bot.playerList)
                {
                    foreach (Player p in bot.playerList.Values)
                    {
                        p.Save();
                    }
                }
                playerSaveStopwatch.Restart();
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

            if (!(x > 0 && y > 0 && x < bot.room.Width && y < bot.room.Height))
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
                    BlockItem temp = (BlockItem)DigBlockMap.blockTranslator[block.blockId];
                    if (player.digLevel + 5 >= temp.Hardness)
                    {
                        if (digHardness[x, y] <= digStrength)
                        {
                            BlockItem newsak = new BlockItem(temp);
                            player.inventory.AddItem(newsak, 1);
                            player.digXp += (int)Math.Round((float)temp.XPGain * (float)player.Pickaxe.XPModifier);
                        }
                        if (player.hasPickaxe())
                        {
                            player.Pickaxe.onDamage(temp.Hardness);
                            if (player.Pickaxe.Durability <= 0)
                            {
                                PickaxeItem bestPick = null;
                                for (int slot = 0; slot < player.inventory.capacity; slot++)
                                {
                                    InventoryItem item = player.inventory.GetItem(slot);
                                    if (item != null && item.ItemType == (int)ItemType.Pickaxe)
                                    {
                                        PickaxeItem pick = (PickaxeItem)item;
                                        if (bestPick == null || (pick.Hardness > bestPick.Hardness && pick.Hardness <= player.Pickaxe.Hardness && pick != player.Pickaxe))
                                        {
                                            bestPick = pick;
                                        }
                                    }
                                }
                                if (bestPick != null)
                                {
                                    player.Pickaxe = bestPick;
                                    bot.connection.Send("say", player.name + ": Pickaxe broke! You now use an other one in your inventory.");
                                }
                                else
                                    bot.connection.Send("say", player.name + ": Pickaxe broke! Get another one!");
                                player.inventory.RemoveItem(player.Pickaxe, 1);
                                return;
                            }
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
            digHardness = new float[bot.room.Width, bot.room.Height];

            for (int y = 0; y < bot.room.Height; y++)
            {
                for (int x = 0; x < bot.room.Width; x++)
                {
                    resetBlockHardness(x, y, bot.room.getBlock(0, x, y).blockId);
                }
            }
        }

        private void resetBlockHardness(int x, int y, int blockId)
        {
            if (x < 0 || y < 0 || x >= bot.room.Width || y >= bot.room.Height)
            {
                if (digHardness == null)
                {
                    digHardness = new float[bot.room.Width, bot.room.Height];
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

        public override bool HasForm
        {
            get { return false; }
        }
    }
}
