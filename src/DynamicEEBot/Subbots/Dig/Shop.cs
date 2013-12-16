using System;
using System.Collections.Generic;
using System.IO;
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
            Load();
        }

        static public int GetBuyPrice(InventoryItem item)
        {
            return (int)item.GetDataAt(3);
        }

        static public int GetSellPrice(InventoryItem item)
        {
            return (int)item.GetDataAt(4);
        }

        static public void SetLocation(int x, int y)
        {
            xPos = x;
            yPos = y;
            Save();
        }

        static public void Save()
        {
            if (File.Exists("data/shop"))
                File.Delete("data/shop");
            StreamWriter w = File.CreateText("data/shop");
            w.WriteLine(xPos);
            w.WriteLine(yPos);
            w.Close();
        }

        static public void Load()
        {
            if (File.Exists("data/shop"))
            {
                string[] data = File.ReadAllLines("data/shop");
                int.TryParse(data[0], out xPos);
                int.TryParse(data[1], out yPos);
            }
        }

    }
}
