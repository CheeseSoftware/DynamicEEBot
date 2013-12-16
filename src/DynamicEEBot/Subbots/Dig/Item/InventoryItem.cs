using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot.Subbots.Dig.Item
{
    [Serializable]
    public abstract class InventoryItem : Subbots.Dig.Item.IShopItem
    {
        private object[] data;

        public virtual int ItemType
        {
            get { return -1; }
        }

        public InventoryItem(object[] data)
        {
            this.data = data;
        }

        public InventoryItem(InventoryItem item)
        {
            this.data = item.data;
        }

        public InventoryItem()
        {

        }

        public string GetName()
        {
            return (string)this.data[0];
        }

        public object[] GetData()
        {
            return data;
        }

        public object GetDataAt(int index)
        {
            return (data[index]);
        }

        public void SetData(object[] data)
        {
            this.data = data;
        }

        public void SetDataAt(int index, object data)
        {
            this.data[index] = data;
        }

        public override string ToString()
        {
            return GetName();
        }

        public override bool Equals(object obj)
        {
            InventoryItem item = obj as InventoryItem;
            return item.GetData() == GetData() && item.GetName() == GetName();
        }

        public bool Equals(InventoryItem item)
        {
            return item.GetData() == GetData() && item.GetName() == GetName();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 64;
                hash = hash * 21 + data.GetHashCode();
                return hash;
            }
        }

        public static bool operator !=(InventoryItem a, InventoryItem b)
        {
            //if ((object)b == null && (object)a == null)
              //  return false;
            if ((object)b == null || (object)a == null)
                return !object.Equals(a, b);
            return a.GetData() != b.GetData() || a.GetName() != b.GetName();
        }

        public static bool operator ==(InventoryItem a, InventoryItem b)
        {
            if ((object)b == null || (object)a == null)
                return object.Equals(a, b);
            return a.GetData() == b.GetData() && a.GetName() == b.GetName();
        }

        public abstract int BuyPrice { get; }
        public abstract int SellPrice { get; }
        public abstract bool Buyable { get; }
        public abstract bool Sellable { get; }
        public abstract InventoryItem onBought(Player player, int amount);
        public abstract void onSold(Player player, int amount);
    }
}
