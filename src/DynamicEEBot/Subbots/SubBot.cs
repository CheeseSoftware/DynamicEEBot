using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DynamicEEBot
{
    public abstract class SubBot
    {
        protected Bot bot;
        protected Task updateTask;
        protected int UpdateSleep = 4;
        public int id = -1;
        private bool enabledValue;
        public bool enabled
        {
            get { return enabledValue; }
            set
            {
                if (value != enabled)
                {
                    enabledValue = value;
                    if (id != -1)
                    {
                        bot.form.Invoke(new Action(() =>
                            bot.form.subbotCheckedListBox.SetItemChecked(id, value)
                        ));
                    }

                    if (value)
                    {
                        this.onEnable(bot);
                        if (updateTask == null || updateTask.IsCanceled || updateTask.IsCompleted || updateTask.IsFaulted)
                            updateTask = new Task(updateTaskWork);
                        updateTask.Start();
                        Console.WriteLine(this.GetType().Name + ".cs is enabled.");
                    }
                    else
                    {
                        //if (updateTask.IsAlive)
                          //  this.updateTask.Suspend();
                        //if (BotUtility.isTaskRunning(updateTask))
                        //    updateTask.Dispose();

                        this.onDisable(bot);
                        Console.WriteLine(this.GetType().Name + ".cs is disabled.");
                    }
                }
            }
        }

        public SubBot(Bot bot)
        {
            this.bot = bot;
            updateTask = new Task(updateTaskWork);
        }

        ~SubBot()
        {
        }

        void updateTaskWork()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (enabled)
            {
                Update(bot);
                int time = (int)stopwatch.ElapsedMilliseconds;
                if (time < UpdateSleep)
                    Thread.Sleep(UpdateSleep - time);
                stopwatch.Reset();
            }
        }

        public abstract void onEnable(Bot bot);
        public abstract void onDisable(Bot bot);
        public abstract void onMessage(object sender, PlayerIOClient.Message m, Bot bot);
        public abstract void onDisconnect(object sender, string reason, Bot bot);
        public abstract void onCommand(object sender, string text, string[] args, Player player, bool isBotMod, Bot bot);
        public abstract void Update(Bot bot);

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
