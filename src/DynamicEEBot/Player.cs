using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    public class Player : PhysicsPlayer
    {
        Dictionary<string, object> Var = new Dictionary<string, object>();

        public Player(Bot bot, int ID, string name, int frame, float xPos, float yPos, bool isGod, bool isMod, bool bla, int coins, bool purple, bool isFriend, int level)
            : base(bot, ID, name, frame, xPos, yPos, isGod, isMod, bla, coins, purple, isFriend, level)
        {
        }

        public void setVar(string key, object value)
        {
            if (Var.ContainsKey(key))
                Var.Remove(key);
            Var.Add(key, value);
        }

        public object getVar(string key)
        {
            if (Var.ContainsKey(key))
                return Var[key];
            return null;
        }

        public bool hasVar(string key)
        {
            if (Var.ContainsKey(key))
                return true;
            return false;
        }

        public object[] getData()
        {
            return new object[] { id, name, frame(), x, y, isgod, ismod, true, coins, false, purple, level };
        }
    }
}
