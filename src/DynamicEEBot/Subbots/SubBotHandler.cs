using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicEEBot.SubBots
{
    public class SubBotHandler
    {
        private SafeDictionary<string, SubBot> subBots = new SafeDictionary<string, SubBot>();
        private SafeDictionary<string, TabPage> subBotTabPages = new SafeDictionary<string, TabPage>();

        private Queue<TaskData> tasks = new Queue<TaskData>();

        public SubBot getSubBot(string name)
        {
            lock (subBots)
            {
                if (subBots.ContainsKey(name))
                    return subBots[name];
                else
                    return null;
            }
        }

        public void AddSubBot(SubBot subBot, Bot bot)
        {
            lock (subBots)
            {
                subBots.Add(subBot.ToString(), subBot);
            }
            bot.form.subbotCheckedListBox.Items.Add(subBot);
            subBot.id = bot.form.subbotCheckedListBox.Items.Count - 1;
            bot.form.subbotCheckedListBox.SetItemChecked(subBot.id, subBot.Enabled);
            if (subBot.HasForm)
            {
                subBot.Form.TopLevel = false;
                subBot.Form.Parent = bot.form;
                subBot.Form.Location = new System.Drawing.Point(50, 50);
            }
        }

        public void RemoveSubBot(SubBot subBot, Bot bot)
        {
            lock (subBots)
            {
                subBots.Remove(subBot.ToString());
            }
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
            //lock (subBots)
            {
                foreach (SubBot o in subBots.Values)
                {
                    if (o != null && o.Enabled)
                    {
                        o.onMessage(sender, m, bot);
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
            lock (subBots)
            {
                foreach (SubBot o in subBots.Values)
                {
                    if (o != null)
                    {
                        o.onDisconnect(sender, reason, bot);
                        o.onDisable(bot);
                        bot.form.Invoke(new Action(() =>
                        {
                            if (o.HasForm)
                                o.Form.Close();
                        }));
                    }
                }

                System.Threading.Thread.Sleep(500);
                subBots.Clear();
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

            lock (subBots)
            {
                foreach (SubBot o in subBots.Values)
                {
                    if (o != null && o.Enabled)
                    {
                        o.onCommand(sender, text, args, player, isBotMod, bot);
                    }
                }
            }
        }

        public void ShowAllForms(Bot bot)
        {
            foreach (SubBot subBot in subBots.Values)
            {
                if (subBot.HasForm)
                {
                    subBot.Form.Show();
                    subBot.Form.BringToFront();
                    subBot.Form.Focus();
                }
            }
        }

        public void HideAllForms(Bot bot)
        {
            foreach (SubBot subBot in subBots.Values)
            {
                if (subBot.HasForm)
                    subBot.Form.Hide();
            }
        }
    }
}
