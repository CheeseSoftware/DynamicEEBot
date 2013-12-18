using DynamicEEBot.SubBots.Dig.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot.SubBots.Dig
{
    public class DigBlockMap
    {
        public static Dictionary<int, InventoryItem> blockTranslator = new Dictionary<int, InventoryItem>();
        public static Dictionary<string, InventoryItem> itemTranslator = new Dictionary<string, InventoryItem>();
        static DigBlockMap()
        {
            blockTranslator.Add((int)Blocks.Stone, new BlockItem(
                "stone",
                1, //XPGAIN
                10, //SHOPBUY
                1, //SHOPSELL
                5, //HARDNESS
                0  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Copper, new BlockItem(
                "copper",
                5, //XPGAIN
                5, //SHOPBUY
                2, //SHOPSELL
                10, //HARDNESS
                2  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Iron, new BlockItem(
                "iron",
                6, //XPGAIN
                8, //SHOPBUY
                3, //SHOPSELL
                14, //HARDNESS
                8  //LEVELREQ
            ));


            blockTranslator.Add((int)Blocks.Gold, new BlockItem(
                "gold",
                15, //XPGAIN
                15, //SHOPBUY
                14, //SHOPSELL
                18, //HARDNESS
                16  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Emerald, new BlockItem(
                "emerald",
                5, //XPGAIN
                5, //SHOPBUY
                0, //SHOPSELL
                24, //HARDNESS
                24  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Ruby, new BlockItem(
                "ruby",
                5, //XPGAIN
                5, //SHOPBUY
                0, //SHOPSELL
                30, //HARDNESS
                32  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Sapphire, new BlockItem(
                "sapphire",
                5, //XPGAIN
                5, //SHOPBUY
                0, //SHOPSELL
                36, //HARDNESS
                40  //LEVELREQ
            ));

            blockTranslator.Add((int)Blocks.Diamond, new BlockItem(
                "diamond",
                5, //XPGAIN
                5, //SHOPBUY
                0, //SHOPSELL
                56, //HARDNESS
                48  //LEVELREQ
            ));

            foreach (InventoryItem i in blockTranslator.Values)
            {
                itemTranslator.Add(i.GetName(), i);
            }

            itemTranslator.Add("mudpickaxe", new PickaxeItem(
                "Mud pickaxe",
                10, //SHOPBUY
                1, //SHOPSELL
                100, //DURABILITY
                1, //HARDNESS
                0.5F //XPMODIFIER
            ));

            itemTranslator.Add("stonepickaxe", new PickaxeItem(
                "Stone pickaxe",
                100, //SHOPBUY
                50, //SHOPSELL
                500, //DURABILITY
                3, //HARDNESS
                1.2F //XPMODIFIER
            ));

            itemTranslator.Add("ironpickaxe", new PickaxeItem(
                "Iron pickaxe",
                200, //SHOPBUY
                100, //SHOPSELL
                2000, //DURABILITY
                10, //HARDNESS
                1.5F //XPMODIFIER
            ));
        }
    }
    public enum Blocks
    {
        Stone = 159,
        Iron = 29,
        Copper = 30,
        Gold = 31,
        Diamond = 73,
        Ruby = 70,
        Sapphire = 72,
        Emerald = 74
    };
}
