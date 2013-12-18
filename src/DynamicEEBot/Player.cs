using DynamicEEBot.SubBots.Dig.Item;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DynamicEEBot
{
    public class Player : PhysicsPlayer
    {
        Dictionary<string, object> vars = new Dictionary<string, object>();
        Stopwatch betaDigTimer = new Stopwatch();
        public Inventory inventory = new Inventory(100);
        protected int xp = 0;
        protected int xpRequired;
        protected int digLevel_ = 0;
        protected int digMoney_ = 0;
        protected bool betaDig = false;
        protected bool fastDig = true;
        protected PickaxeItem currentPickaxe = null;
        //protected int currentPickaxeSlot = -1;

        public Player(Bot bot, int ID, string name, int frame, float xPos, float yPos, bool isGod, bool isMod, bool bla, int coins, bool purple, bool isFriend, int level)
            : base(bot, ID, name, frame, xPos, yPos, isGod, isMod, bla, coins, purple, isFriend, level)
        {
            Load();
        }

        public void setVar(string key, object value)
        {
            if (vars.ContainsKey(key))
                vars.Remove(key);
            vars.Add(key, value);
        }

        public object getVar(string key)
        {
            if (vars.ContainsKey(key))
                return vars[key];
            return null;
        }

        public bool hasVar(string key)
        {
            if (vars.ContainsKey(key))
                return true;
            return false;
        }

        public bool hasPickaxe()
        {
            return currentPickaxe != null;
        }

        public PickaxeItem Pickaxe { get { return currentPickaxe; } set { currentPickaxe = value; } }

        public object[] getData()
        {
            return new object[] { id, name, frame(), x, y, isgod, ismod, true, coins, false, purple, level };
        }

        public void Save()
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");
            Pair<IFormatter, Stream> writeStuff = inventory.Save(@"data\" + name);
            writeStuff.first.Serialize(writeStuff.second, digXp);
            writeStuff.first.Serialize(writeStuff.second, digMoney);
            writeStuff.second.Close();
        }

        public void Load()
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");
            if (File.Exists(@"data\" + name))
            {
                digLevel_ = 1;
                Pair<IFormatter, Stream> writeStuff = inventory.Load(@"data\" + name);
                if (writeStuff != null)
                {
                    digXp = (int)writeStuff.first.Deserialize(writeStuff.second);
                    digMoney_ = (int)writeStuff.first.Deserialize(writeStuff.second);
                    writeStuff.second.Close();
                    xpRequired = getXpRequired(digLevel);
                }
            }
        }

        public int digRange { get { return ((digLevel_ > 0 && fastDig) ? 2 : 1) + ((betaDig) ? 1 : 0); } }

        public int xpRequired_ { get { return xpRequired; } }

        public int digLevel { get { return digLevel_; } }

        public int digMoney { get { return digMoney_; } set { digMoney_ = value; } }

        public int digStrength { get { int bla = !hasPickaxe() ? 1 + digLevel / 4 : Pickaxe.Hardness * digLevel/4; return bla; } }

        private static int getXpRequired(int level) { return BotUtility.Fibonacci(level + 2) * 8; }

        public int digXp
        {
            get { return xp; }
            set
            {
                if (value > xp)
                {
                    xp = value;
                    if (xp >= xpRequired)
                        while (xp >= xpRequired)
                            xpRequired = getXpRequired(++digLevel_);
                    else
                        xpRequired = getXpRequired((digLevel_ = getLevel(xp)));
                }
            }
        }

        private static int getLevel(int xp)
        {
            int level = 0;

            while (xp > getXpRequired(level))
                level++;

            return level;
        }
    }
}
