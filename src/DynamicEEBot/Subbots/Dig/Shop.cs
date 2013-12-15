using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    public static class Shop
    {
        public static int xPos = 0;
        public static int yPos = 0;

        public static Dictionary<string, InventoryItem> shopInventory = new Dictionary<string, InventoryItem>(DigBlockMap.itemTranslator);
        static Shop()
        {

        }

        static public int GetBuyPrice(InventoryItem item)
        {
            return (int)item.GetDataAt(2);
        }

        static public int GetSellPrice(InventoryItem item)
        {
            return (int)item.GetDataAt(3);
        }

        static public void SetLocation(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

    }
}
