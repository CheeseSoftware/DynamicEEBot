using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot.SubBots
{
    public class SubBotHandler
    {
        private SafeDictionary<string, SubBot> SubBots = new SafeDictionary<string, SubBot>();

        private Queue<TaskData> tasks = new Queue<TaskData>();

        public SubBot getSubBot(string name)
        {
            lock (SubBots)
            {
                if (SubBots.ContainsKey(name))
                    return SubBots[name];
                else
                    return null;
            }
        }

        public void AddSubBot(SubBot SubBot, Bot bot)
        {
            lock (SubBots)
            {
                SubBots.Add(SubBot.GetType().ToString(), SubBot);
            }
            bot.form.subbotCheckedListBox.Items.Add(SubBot);
            SubBot.id = bot.form.subbotCheckedListBox.Items.Count - 1;
            bot.form.subbotCheckedListBox.SetItemChecked(SubBot.id, SubBot.enabled);
        }

        public void RemoveSubBot(SubBot SubBot)
        {
            lock (SubBots)
                SubBots.Remove(SubBot.GetType().ToString());
        }

        public List<TaskData> Update(int seconds)
        {
            List<TaskData> deadTasks = new List<TaskData>();

            lock (tasks)
            {
                while (tasks.Count > 0)
                {
                    if (tasks.Peek().stopwatch.ElapsedMilliseconds / 1000 >= seconds)
                    {
                        TaskData task = tasks.Dequeue();

                        if (task.task.IsCompleted == false || task.task.Status == TaskStatus.Running || task.task.Status == TaskStatus.WaitingToRun || task.task.Status == TaskStatus.WaitingForActivation)
                            deadTasks.Add(task);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return deadTasks;
        }

        public void onMessage(object sender, PlayerIOClient.Message m, Bot bot)
        {
            //lock (SubBots)
            {
                foreach (SubBot o in SubBots.Values)
                {
                    if (o != null && o.enabled)
                    {
                        //Task task = new Task(() =>//new Thread(() =>
                        //{
                        o.onMessage(sender, m, bot);
                        //                        });

                        lock (tasks)
                        {
                            //tasks.Enqueue(new TaskData(o.GetType().ToString(), task, new SubBots.OnMessage(m)));
                        }

                        //task.Start();
                    }
                }
            }
        }

        public void OnDisconnect(object sender, string reason, Bot bot)
        {
            lock (tasks)
            {
                while (tasks.Count > 0)
                {
                    Task task = tasks.Dequeue().task;

                    if (BotUtility.isTaskRunning(task))
                        task.Dispose();
                }
            }
            lock (SubBots)
            {
                foreach (SubBot o in SubBots.Values)
                {
                    if (o != null)
                    {
                        o.onDisconnect(sender, reason, bot);
                        o.onDisable(bot);
                    }
                }

                System.Threading.Thread.Sleep(500);
                SubBots.Clear();
                bot.form.Invoke(new Action(() =>
                    bot.form.subbotCheckedListBox.Items.Clear()
                    ));
            }
        }

        public void onCommand(object sender, string text, Player player, Bot bot)
        {
            string[] args = text.Split(' ');

            string[] arg = text.ToLower().Split(' ');
            string name = player.name;
            bool isBotMod = (name == "ostkaka" || name == "botost" || name == "gustav9797" || name == "gbot" || player.ismod || player.id == -1);

            lock (SubBots)
            {
                foreach (SubBot o in SubBots.Values)
                {
                    if (o != null && o.enabled)
                    {
                        //Task task = new Task(() =>//new Thread(() =>
                        //{
                        o.onCommand(sender, text, args, player, isBotMod, bot);
                        //});

                        lock (tasks)
                        {
                            //tasks.Enqueue(new TaskData(o.GetType().ToString(), task, new SubBots.OnCommand(text, player.id, player)));
                        }

                        //task.Start();
                    }
                }
            }
        }
    }
}
