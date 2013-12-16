using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEEBot
{
    public class BotUtility
    {
        public static string rot13(string arg1)
        {
            int num = 0;
            string str = "";
            for (int i = 0; i < arg1.Length; i++)
            {
                num = arg1[i];
                if ((num >= 0x61) && (num <= 0x7a))
                {
                    if (num > 0x6d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                else if ((num >= 0x41) && (num <= 90))
                {
                    if (num > 0x4d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                str = str + ((char)num);
            }
            return str;
        }
    }
}
