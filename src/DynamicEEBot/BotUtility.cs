using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public static int Fibonacci(int i)
        {
            if (i < 1)
            {
                return 0;
            }
            else
            {
                int j = 1;
                int k = 0;


                for (int l = 1; l < i; l++)
                {
                    j += k;
                    k = j;
                }

                return j;
            }
        }

        public static bool isTaskRunning(Task task)
        {
            return (task.IsCompleted == false
                || task.Status == TaskStatus.Running
                || task.Status == TaskStatus.WaitingToRun
                || task.Status == TaskStatus.WaitingForActivation);
        }

    }
}
