using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.Dig.Item
{
    [Serializable]
    class BlockItem : InventoryItem
    {
        public BlockItem(string name, int xpGain, int buyPrice, int sellPrice, int hardness, int levelReq)
            : base()
        {
            this.SetData(new object[] { name, xpGain, buyPrice, sellPrice, hardness, levelReq });
        }

        public BlockItem(BlockItem item)
        {
            SetData(item.GetData());
        }

        public override int ItemType
        {
            get { return (int)Item.ItemType.Block; }
        }

        public int XPGain { get { return (int)GetDataAt(1); } }

        public int Hardness { get { return (int)GetDataAt(4); } }

        public override int BuyPrice
        {
            get { return (int)GetDataAt(2); }
        }

        public override int SellPrice
        {
            get { return (int)GetDataAt(3); }
        }

        public override bool Buyable
        {
            get { return BuyPrice != -1; }
        }

        public override bool Sellable
        {
            get { return SellPrice != -1; }
        }

        public override InventoryItem onBought(Player player, int amount)
        {
            player.digMoney -= BuyPrice * amount;
            return new BlockItem(this);
        }

        public override void onSold(Player player, int amount)
        {
            player.digMoney += SellPrice * amount;
        }
    }
}
