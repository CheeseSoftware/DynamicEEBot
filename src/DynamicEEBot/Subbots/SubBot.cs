using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicEEBot
{
    //[TypeDescriptionProvider(typeof(AbstractFormDescriptionProvider<SubBot, Form>))]
    public abstract class SubBot// : MdiForm
    {
        private bool enabledValue;
        protected Bot bot;
        protected Task updateTask;
        protected int UpdateSleep = 4;
        protected Form form;
        public int id = -1;
        public Form Form { get { return form; } }

        public new bool Enabled
        {
            get { return enabledValue; }
            set
            {
                if (value != Enabled)
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
                        if (!BotUtility.isTaskRunning(updateTask))
                            updateTask.Start();
                        Console.WriteLine(this.GetType() + ".cs is enabled.");
                    }
                    else
                    {
                        if (BotUtility.isTaskRunning(updateTask))
                            updateTask.Dispose();

                        this.onDisable(bot);
                        Console.WriteLine(this.GetType() + ".cs is disabled.");
                    }
                }
            }
        }

        public SubBot(Bot bot)
            : base()
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
            while (Enabled)
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
        public abstract bool HasForm { get; }

        public override string ToString()
        {
            return base.GetType().Name;
        }
    }
}
