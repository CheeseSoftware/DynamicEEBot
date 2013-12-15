using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DynamicEEBot.SubBots
{
    public struct TaskData
    {
        public string name;
        public Task task;
        public SubBots.Method method;
        public Stopwatch stopwatch;

        public TaskData(string name, Task task, SubBots.Method method)
        {
            this.name = name;
            this.task = task;
            this.method = method;

            this.stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public override string ToString()
        {
            return name + " | " + method.ToString() + " | t: " + (stopwatch.ElapsedMilliseconds/1000).ToString(); 
        }
    }
}
