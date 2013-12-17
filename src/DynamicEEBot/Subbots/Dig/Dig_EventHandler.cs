using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Graphics.Tools.Noise;
using DynamicEEBot.Subbots.Dig.Item;
using System.Drawing;

namespace DynamicEEBot.Subbots.Dig
{
    public partial class Dig : SubBot
    {
        //Dictionary<int, Point> firstGodPositions = new Dictionary<int, Point>();
        public override void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            switch (m.Type)
            {
                case "add":
                    {
                        string name = m.GetString(1);
                        //bot.connection.Send("say", "/giveedit " + name);
                    }
                    break;
                /*case "god":
                    {
                        int id = m.GetInt(0);
                        bool god = m.GetBoolean(1);
                        if (bot.playerList.ContainsKey(id))
                        {
                            Player p = bot.playerList[id];
                            if (p.name != "gustav9797" && p.name != "ostkaka")
                            {
                                if (god)
                                {
                                    if (!firstGodPositions.ContainsKey(id))
                                        firstGodPositions.Add(id, new Point(p.blockX, p.blockY));
                                    string name = bot.playerList[id].name;
                                    bot.connection.Send("say", "/removeedit " + name);
                                    bot.connection.Send("say", "/giveedit " + name);
                                }
                                else
                                {
                                    if (firstGodPositions.ContainsKey(id))
                                    {
                                        bot.connection.Send("say", "/teleport " + p.name + " " + firstGodPositions[id].X + " " + firstGodPositions[id].Y);
                                        firstGodPositions.Remove(id);
                                    }
                                }
                            }
                        }
                    }
                    break;*/
                case "init":
                    digHardness = new float[bot.room.Width, bot.room.Height];
                    resetDigHardness();
                    break;
                case "reset":
                    digHardness = new float[bot.room.Width, bot.room.Height];
                    resetDigHardness();
                    break;
                case "m":
                    {
                        int userId = m.GetInt(0);
                        float playerPosX = m.GetFloat(1);
                        float playerPosY = m.GetFloat(2);
                        float speedX = m.GetFloat(3);
                        float speedY = m.GetFloat(4);
                        float modifierX = m.GetFloat(5);
                        float modifierY = m.GetFloat(6);
                        float horizontal = m.GetFloat(7);
                        float vertical = m.GetFloat(8);
                        int Coins = m.GetInt(9);
                        int blockX = (int)(playerPosX / 16 + 0.5);
                        int blockY = (int)(playerPosY / 16 + 0.5);
                        int blockId = (bot.room.getBlock(0, blockX + (int)horizontal, blockY + (int)vertical).blockId);
                        Player player;

                        lock (bot.playerList)
                        {
                            if (!bot.playerList.ContainsKey(userId))
                                return;
                            else
                                player = bot.playerList[userId];
                        }

                        if (isDigable(blockId))
                        {
                            if (player.digRange > 1)
                            {
                                for (int x = (horizontal == 1) ? -1 : -player.digRange + 1; x < ((horizontal == -1) ? 2 : player.digRange); x++)
                                {
                                    for (int y = (vertical == 1) ? -1 : -player.digRange + 1; y < ((vertical == -1) ? 2 : player.digRange); y++)
                                    {
                                        float distance = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                                        if (distance <= 1.41421357 * (player.digRange - 1) || distance < 1.4142)
                                        {
                                            int cx = blockX + x + (int)Math.Ceiling(horizontal);
                                            int cy = blockY + y + (int)Math.Ceiling(vertical);
                                            if (player.hasPickaxe())
                                            {
                                                PickaxeItem pick = player.Pickaxe;
                                                if (pick.Durability >= digHardness[cx, cy])
                                                {
                                                    DigBlock(
                                                        cx,
                                                        cy,
                                                        player,
                                                        (player.digRange - distance) * player.digStrength,
                                                        false);
                                                }
                                            }
                                        }
                                    }
                                }
                                return;
                            }
                        }
                        if (player.hasPickaxe())
                        {
                            if (horizontal == 0 || vertical == 0)
                                DigBlock(blockX + (int)horizontal, blockY + (int)vertical, player, player.digStrength, true);
                            blockId = bot.room.getBlock(0, blockX, blockY).blockId;
                            DigBlock(blockX, blockY, player, player.digStrength, true);

                        }
                    }
                    break;
                case "b":
                    {
                        int layer = m.GetInt(0);
                        int x = m.GetInt(1);
                        int y = m.GetInt(2);
                        int blockId = m.GetInt(3);
                        resetBlockHardness(x, y, blockId);
                    }
                    break;
            }
        }

        public override void onDisconnect(object sender, string reason, Bot bot)
        {

        }

        public override void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot)
        {
            switch (args[0])
            {
                case "digcommands":
                    {
                        new Task(() =>
                            {
                                bot.connection.Send("say", "commands: !xp, !level, !inventory, ");
                                Thread.Sleep(1000);
                                bot.connection.Send("say", "!xpleft, !buy <item> <amount>, !sell <item> <amount> ");
                            }).Start();
                    }
                    break;
                case "generate":
                    if (isBotMod)
                    {
                        digHardness = new float[bot.room.Width, bot.room.Height];
                        Generate(bot.room.Width, bot.room.Height);//lock(bot.playerList
                    }
                    break;
                case "givexp":
                    if (isBotMod && args.Length > 2)
                    {
                        Player receiver;
                        lock (bot.playerList)
                        {
                            if (bot.nameList.ContainsKey(args[1]))
                            {
                                receiver = bot.playerList[bot.nameList[args[1]]];
                            }
                            else
                            {
                                break;
                            }
                        }

                        int xp = Int32.Parse(args[2]);
                        receiver.digXp += xp;
                    }
                    break;
                case "xp":
                    lock (bot.playerList)
                        bot.connection.Send("say", player.name + ": Your xp: " + player.digXp);
                    break;
                case "xpleft":
                    lock (bot.playerList)
                        bot.connection.Send("say", player.name + ": You need" + (player.xpRequired_ - player.digXp).ToString() + " for level " + player.digLevel.ToString());
                    break;
                case "level":
                    lock (bot.playerList)
                        bot.connection.Send("say", player.name + ": Level: " + player.digLevel);
                    break;
                case "inventory":
                    {
                        lock (bot.playerList)
                            bot.connection.Send("say", player.name + ": " + player.inventory.ToString());
                    }
                    break;
                case "save":
                    {
                        lock (bot.playerList)
                            player.Save();
                    }
                    break;
                case "setshop":
                    if (isBotMod)
                    {
                        lock (bot.playerList)
                        {
                            int x = player.blockX;
                            int y = player.blockY;
                            Shop.SetLocation(x, y);
                            bot.connection.Send("say", "Shop set at " + x + " " + y);
                            bot.room.DrawBlock(Block.CreateBlock(0, x, y, 9, 0));
                        }
                    }
                    break;
                case "money":
                    {
                        bot.connection.Send("say", player.name + ": Money: " + player.digMoney);
                    }
                    break;
                case "setmoney":
                    if (isBotMod)
                    {
                        int money;
                        if (args.Length > 1 && int.TryParse(args[1], out money))
                        {
                            player.digMoney = money;
                            bot.connection.Send("say", player.name + ": Money: " + player.digMoney);
                        }
                    }
                    break;
                case "buy":
                    {
                        lock (bot.playerList)
                        {
                            if (player.blockX > Shop.xPos - 2 && player.blockX < Shop.xPos + 2)
                            {
                                if (player.blockY > Shop.yPos - 2 && player.blockY < Shop.yPos + 2)
                                {
                                    if (args.Length > 1)
                                    {
                                        if (DigBlockMap.itemTranslator.ContainsKey(args[1].ToLower()))
                                        {
                                            InventoryItem item = DigBlockMap.itemTranslator[args[1].ToLower()];
                                            int itemPrice = Shop.GetBuyPrice(item);

                                            int amount = 1;
                                            if (args.Length >= 3)
                                                int.TryParse(args[2], out amount);
                                            if (player.digMoney >= (itemPrice * amount))
                                            {
                                                player.digMoney -= itemPrice;
                                                player.inventory.AddItem(item.onBought(player, amount), amount);
                                                bot.connection.Send("say", "Item bought!");
                                            }
                                            else
                                                bot.connection.Send("say", player.name + ": You do not have enough money.");
                                        }
                                        else
                                            bot.connection.Send("say", player.name + ": The requested item does not exist.");
                                    }
                                    else
                                        bot.connection.Send("say", player.name + ": Usage: !buy <item> [amount=1]");
                                }
                                else
                                    bot.connection.Send("say", player.name + ": You aren't near the shop.");
                            }
                            else
                                bot.connection.Send("say", player.name + ": You aren't near the shop.");
                        }
                    }
                    break;
                case "sell":
                    {
                        lock (bot.playerList)
                        {
                            if (player.blockX > Shop.xPos - 2 && player.blockX < Shop.xPos + 2)
                            {
                                if (player.blockY > Shop.yPos - 2 && player.blockY < Shop.yPos + 2)
                                {
                                    if (args.Length > 1)
                                    {
                                        int slot;
                                        if (int.TryParse(args[1], out slot))
                                        {
                                            InventoryItem item = player.inventory.GetItem(slot);
                                            if (item != null)
                                            {
                                                int itemSellPrice = item.SellPrice;
                                                int amount = 1;
                                                if (args.Length > 2)
                                                    int.TryParse(args[2], out amount);
                                                if (player.inventory.GetItemCount(item) >= amount)
                                                {
                                                    item.onSold(player, amount);
                                                    if (!player.inventory.RemoveItem(slot, amount))
                                                        throw new Exception("Could not remove item?D:");
                                                    bot.connection.Send("say", player.name + ": Item sold! You received " + (itemSellPrice * amount) + " money.");
                                                }
                                                else
                                                    bot.connection.Send("say", player.name + ": You don't have enough of that item.");
                                            }
                                            else
                                                bot.connection.Send("say", player.name + ": You don't have that item.");
                                        }
                                        else
                                            bot.connection.Send("say", player.name + ": Usage: !sell <slot> [amount=1]");
                                    }
                                    else
                                        bot.connection.Send("say", player.name + ": Usage: !sell <slot> [amount=1]");
                                }
                            }
                            bot.connection.Send("say", player.name + ": You aren't near the shop.");
                        }
                    }
                    break;
                case "usepick":
                    {
                        int slot;
                        if (args.Length > 1 && int.TryParse(args[1], out slot))
                        {
                            InventoryItem item = player.inventory.GetItem(slot);
                            if (item != null && item.ItemType == (int)ItemType.Pickaxe)
                            {
                                player.Pickaxe = (PickaxeItem)item;
                                bot.connection.Send("say", player.name + ": Using pick in slot " + slot);
                                break;
                            }
                        }
                    }
                    break;
                case "durability":
                    {
                        if (player.hasPickaxe())
                        {
                            bot.connection.Send("say", player.name + ": Durability: " + player.Pickaxe.Durability);
                        }
                        else
                            bot.connection.Send("say", player.name + ": You must use a pickaxe! !usepickaxe");
                    }
                    break;
                case "trashme":
                    {
                        new Thread(() =>
                            {
                                lock (player.inventory)
                                {
                                    player.inventory.Clear();
                                }
                                player.Pickaxe = null;
                            }).Start();
                    }
                    break;

            }
        }

        public override void onEnable(Bot bot)
        {

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
