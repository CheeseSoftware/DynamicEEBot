using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot.SubBots.Dig.Item
{
    [Serializable]
    public class PickaxeItem : InventoryItem, Item.IDestroyable
    {
        int totalDurability;
        public PickaxeItem(string name, int buyPrice, int sellPrice, int durability, int hardness, float xpModifier)
            : base()
        {
            this.SetData(new object[] { name, buyPrice, sellPrice, durability, hardness, xpModifier });
            totalDurability = durability;
        }

        public PickaxeItem(PickaxeItem pickaxe)
        {
            this.SetData(pickaxe.GetData());
            totalDurability = Durability;
        }

        public float XPModifier
        {
            get { return (float)GetDataAt(5); }
        }
        public int LevelReq
        {
            get { return (int)GetDataAt(6); }
        }

        public override int ItemType
        {
            get { return (int)Item.ItemType.Pickaxe; }
        }

        public override int BuyPrice
        {
            get { return (int)GetDataAt(1); }
        }

        public override int SellPrice
        {
            get { return (int)GetDataAt(2); }
        }

        public override bool Buyable
        {
            get { return BuyPrice != -1; }
        }

        public override bool Sellable
        {
            get { return SellPrice != -1; }
        }

        public int Durability
        {
            get { return (int)GetDataAt(3); }
            set { SetDataAt(3, value); }
        }

        public int Hardness
        {
            get { return (int)GetDataAt(4); }
        }

        public void onDamage(int damage)
        {
            int durability = (int)GetDataAt(3);
            durability -= damage;// * Hardness;
            SetDataAt(3, durability);
        }

        public override InventoryItem onBought(Player player, int amount)
        {
            player.digMoney -= BuyPrice * amount;
            return new PickaxeItem(this);
        }

        public override void onSold(Player player, int amount)
        {
            player.digMoney += SellPrice * (Durability / (totalDurability <= 0 ? Durability : totalDurability));
        }
    }
}
