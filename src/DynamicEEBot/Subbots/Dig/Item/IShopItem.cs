using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.Dig.Item
{
    interface IShopItem
    {
        int BuyPrice { get; }
        int SellPrice { get; }
        bool Buyable { get; }
        bool Sellable { get; }
        InventoryItem onBought(Player player, int amount);
        void onSold(Player player, int amount);
    }
}
