using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.Subbots
{
    partial class Room
    {
        public override void onEnable(Bot bot)
        {
            blockMap = new List<Block>[2][,];
            blockQueue = new Queue<Block>();
            blockRepairQueue = new Queue<Block>();
            blockSet = new HashSet<Block>();
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
                        int myId = m.GetInt(6);
                        int myX = m.GetInt(7);
                        int myY = m.GetInt(8);
                        string myName = m.GetString(9);
                        bool hasCode = m.GetBoolean(10);
                        bool isOwner = m.GetBoolean(11);
                        width = m.GetInt(12);
                        height = m.GetInt(13);
                        bool isTutorialRoom = m.GetBoolean(14);

                        ResetMap();
                        DeSerialize(m);
                        loadedWorld = true;
                        if (isOwner || hasCode)
                            StartDrawThread();
                    }
                    break;
                case "reset":
                    {
                        loadedWorld = false;
                        ResetMap();
                        DeSerialize(m);
                        loadedWorld = true;
                    }
                    break;
                case "clear":
                    {
                        width = m.GetInt(0);
                        height = m.GetInt(1);
                        int borderId = m.GetInt(2);
                        int workareaId = m.GetInt(3);
                        ResetMap();
                        DrawBorder();
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

        public override void onDisconnect(object sender, string reason, Bot bot)
        {
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


        public override void Update(Bot bot)
        {

        }

    }
}
