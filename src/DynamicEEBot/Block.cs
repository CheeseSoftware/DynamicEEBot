using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    public class Block
    {
        string blockType;
        object[] dataArray;

        public bool isPortal()
        {
            return (blockType == "pt");
        }

        public int x
        {
            get { return Convert.ToInt32(dataArray[1]); }
            set { dataArray[1] = value; }
        }

        public int y
        {
            get { return Convert.ToInt32(dataArray[2]); }
            set { dataArray[2] = value; }
        }

        public int layer
        {
            get { return Convert.ToInt32(dataArray[0]); }
            set { dataArray[0] = value; }
        }

        public int blockId
        {
            get { return Convert.ToInt32(dataArray[3]); }
            set { dataArray[3] = value; }
        }

        public int b_userId
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "b")
                    return Convert.ToInt32(dataArray[4]);
                return 0;
            }
        }

        public int bc_coinsToOpen
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "bc")
                    return Convert.ToInt32(dataArray[4]);
                return 0;
            }
        }

        public int bs_soundId
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "bs")
                    return Convert.ToInt32(dataArray[4]);
                return 0;
            }
        }

        public int pt_rotation
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "pt")
                    return Convert.ToInt32(dataArray[4]);
                return 0;
            }
        }
        public int pt_id
        {
            get
            {
                if (dataArray.Length >= 6 && blockType == "pt")
                    return Convert.ToInt32(dataArray[5]);
                return 0;
            }
        }

        public int pt_target
        {
            get
            {
                if (dataArray.Length >= 7 && blockType == "pt")
                    return Convert.ToInt32(dataArray[6]);
                return 0;
            }
        }

        public string lb_text
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "lb")
                    return dataArray[4].ToString();
                return "";
            }
        }

        public int br_rotation
        {
            get
            {
                if (dataArray.Length >= 5 && blockType == "br")
                    return Convert.ToInt32(dataArray[4]);
                return 0;
            }
        }

        public Block(Message m)
            : this()
        {
            dataArray = new object[m.Count];

            blockType = m.Type;

            int i;

            if (blockType == "b")
            {
                i = 0;
            }
            else
            {
                i = 1;
                dataArray[0] = 0;
            }

            for (int j = i; j < m.Count; j++)
            {
                dataArray[j] = toObject(m, (uint)(j));
            }
        }

        protected Block()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Block block = obj as Block;
            if (this.layer == block.layer && this.x == block.x && this.y == block.y && this.blockId == block.blockId)
            {
                return true;
            }
            return false;
            /*if (block.GetType() == this.GetType())
            {
                if (this.layer == block.layer && this.x == block.x && this.y == block.y)
                {

                    if (block.blockId == this.blockId)
                    {
                        switch (this.blockType)
                        {
                            case "b":
                                return true;

                            case "bc":
                                return this.dataArray[3] == block.dataArray[3];

                            case "bs":
                                goto case "bc";

                            case "pt":
                                return this.dataArray[3] == block.dataArray[3] && this.dataArray[4] == block.dataArray[4] && this.dataArray[5] == block.dataArray[5];

                            case "lb":
                                goto case "bc";

                            case "br":
                                goto case "bc";

                            default:
                                return true;
                        }
                    }
                }
            }
            return false;*/
        }

        public static bool Compare(Block a, Block b)
        {
            if (a == null || b == null)
                return false;

            if (a == b)
                return true;

            if (a.blockType != b.blockType)
                return false;

            if (a.blockId != b.blockId)
                return false;

            /*object[] dataArrayA = new object[a.dataArray.Length];
            object[] dataArrayB = new object[b.dataArray.Length];

            Array.Copy(dataArrayA, a.dataArray, a.dataArray.Length);
            Array.Copy(dataArrayB, b.dataArray, b.dataArray.Length);*/

            switch (a.blockType)
            {
                case "b":
                    return true;

                case "bc":
                    return a.dataArray[3] == b.dataArray[3];

                case "bs":
                    goto case "bc";

                case "pt":
                    return a.dataArray[3] == b.dataArray[3] && a.dataArray[4] == b.dataArray[4] && a.dataArray[5] == b.dataArray[5];

                case "lb":
                    goto case "bc";

                case "br":
                    goto case "bc";

                default:
                    return true;
            }
        }

        public static Block CreateBlock(int layer, int x, int y, int blockId, int userId)
        {
            Block block = new Block();
            block.blockType = "b";
            block.dataArray = new object[] { layer, x, y, blockId, userId };
            return block;
        }

        public static Block CreateBlockCoin(int x, int y, int blockId, int coinsToOpen)
        {
            Block block = new Block();
            block.blockType = "bc";
            block.dataArray = new object[] { 0, x, y, blockId, coinsToOpen };
            return block;
        }

        public static Block CreateNoteBlock(int x, int y, int blockId, int soundId)
        {
            Block block = new Block();
            block.blockType = "bs";
            block.dataArray = new object[] { 0, x, y, blockId, soundId };
            return block;
        }

        public static Block CreatePortal(int x, int y, int rotation, int id, int target)
        {
            Block block = new Block();
            block.blockType = "pt";
            block.dataArray = new object[] { 0, x, y, 242, rotation, id, target };
            return block;
        }

        public static Block CreateText(int x, int y, string text)
        {
            Block block = new Block();
            block.blockType = "lb";
            block.dataArray = new object[] { 0, x, y, 1000, text };
            return block;
        }

        public static Block CreateSpike(int x, int y, int rotation)
        {
            Block block = new Block();
            block.blockType = "br";
            block.dataArray = new object[] { 0, x, y, 361, rotation };
            return block;
        }

        public object getObject(int index)
        {
            if (index < dataArray.Length)
                return dataArray[index];
            else
                return 0;
        }

        public int getDataSize()
        {
            return dataArray.Length;
        }

        public void Send(Bot bot)
        {
            if (blockType == "")
            {
                throw new System.Exception("Attempt to draw a block of void.");
            }
            else if (blockType == "b")
            {
                Object[] sendData = new object[dataArray.Length - 1];
                Array.Copy(dataArray, sendData, dataArray.Length - 1);
                bot.connection.Send(bot.room.key, sendData);
            }
            else
            {
                bot.connection.Send(bot.room.key, dataArray);
            }
        }

        public static bool operator !=(Block a, Block b)
        {
            if ((object)a == null)
                return ((object)b == null);

            return !a.Equals(b);
        }

        public static bool operator ==(Block a, Block b)
        {
            if ((object)a == null)
                return ((object)b == null);

            return a.Equals(b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 64;
                hash = hash * 27 + dataArray.GetHashCode() + blockType.GetHashCode() - 2;
                return hash;
            }
        }

        public static object toObject(Message m, uint index)
        {
            string type = m[index].GetType().ToString().Replace("System.", "").Replace("32", "");
            switch (type)
            {
                case "Boolean":
                    return m.GetBoolean(index);
                case "Int":
                    return m.GetInt(index);
                case "UInt":
                    return m.GetUInt(index);
                case "Long":
                    return m.GetLong(index);
                case "Ulong":
                    return m.GetULong(index);
                case "Double":
                    return m.GetDouble(index);
                case "Float":
                    return m.GetFloat(index);
                case "Single":
                    return m.GetFloat(index);
                case "String":
                    return m.GetString(index);
                case "Byte[]":
                    return m.GetByteArray(index);
                default:
                    Console.WriteLine("Type '{0}' not found!", type);
                    return 0;
            }
        }
    }
}
