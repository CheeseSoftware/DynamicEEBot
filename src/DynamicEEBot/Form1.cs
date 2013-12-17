using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicEEBot
{
    public partial class Form1 : Form
    {
        Bot bot;
        Dictionary<string, string> passwordList = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            bot = new Bot(this);
        }

        private void SaveData()
        {
            string data = "#VILKEN BRA FIL" + Environment.NewLine;

            data += "servers:" + Environment.NewLine;
            foreach (var o in cbServer.Items)
                data += o.ToString() + Environment.NewLine;

            data += "accounts:" + Environment.NewLine;
            foreach (var o in cbEmail.Items)
            {
                data += o.ToString() + "\t";
                if (passwordList.ContainsKey(o.ToString()))
                    data += BotUtility.rot13(passwordList[o.ToString()]);

                data += Environment.NewLine;
            }

            data += "worlds:" + Environment.NewLine;
            foreach (var o in cbWorldId.Items)
                data += o.ToString() + Environment.NewLine;

            StreamWriter writer;
            if (File.Exists("roomData.1337"))
                writer = new StreamWriter("roomData.1337");
            else
                writer = new StreamWriter(File.Create("roomData.1337"));
            writer.Write(data);
            writer.Close();
        }

        private void LoadData()
        {
            if (!File.Exists("roomData.1337"))
                return;

            StreamReader reader = new StreamReader("roomData.1337");
            string type = "boring";

            while (!reader.EndOfStream)//foreach (string s in data)
            {
                string s = reader.ReadLine();

                if (s == "servers:" || s == "accounts:" || s == "worlds:")
                    type = s;
                else
                {
                    switch (type)
                    {
                        case "servers:":
                            cbServer.Items.Add(s);
                            break;
                        case "accounts:":
                            {
                                string[] pair = s.Split('\t');
                                string username = pair[0];
                                string password;
                                if (pair.Count() >= 2)
                                    password = BotUtility.rot13(pair[1]);
                                else
                                    password = "";

                                passwordList.Add(username, password);
                                cbEmail.Items.Add(username);
                            }
                            break;
                        case "worlds:":
                            cbWorldId.Items.Add(s);
                            break;
                    }
                }

            }

            reader.Close();

            if (cbServer.Items.Count >= 1)
            {
                cbServer.Text = cbServer.Items[0].ToString();
            }
            else
            {
                cbServer.Text = "everybody-edits-su9rn58o40itdbnw69plyw";
                cbServer.Items.Add("everybody-edits-su9rn58o40itdbnw69plyw");
            }

            if (cbEmail.Items.Count >= 1)
            {
                cbEmail.Text = cbEmail.Items[0].ToString();
                if (passwordList.ContainsKey(cbEmail.Items[0].ToString()))
                    tbPassword.Text = passwordList[cbEmail.Items[0].ToString()];
            }
            else
            {
                cbEmail.Text = "";
                tbPassword.Text = "";
            }
            if (cbWorldId.Items.Count >= 1)
                cbWorldId.Text = cbWorldId.Items[0].ToString();
            else
                cbWorldId.Text = "";

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (bot.Login(cbEmail.Text.Split('#').First(), tbPassword.Text.Split('#').First(), cbServer.Text.Split('#').First()))
                loginButton.Enabled = false;
            else
                loginButton.Text = "Login failed";
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!bot.connected)
            {
                connectButton.Text = "Connecting...";
                if (bot.Connect(cbWorldId.Text.Split('#').First(), "Everybodyedits176"))
                    connectButton.Text = "Disconnect";
                else
                    connectButton.Text = "Connect failed";
            }
            else
            {
                bot.Disconnect();
                connectButton.Text = "Connect";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bot.Disconnect();
        }

        private void subbotCheckedListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < subbotCheckedListBox.Items.Count; i++)
            {
                var subBot = subbotCheckedListBox.Items[i] as SubBot;
                subBot.enabled = subbotCheckedListBox.GetItemChecked(i);
            }
        }

        private void accessButton_Click(object sender, EventArgs e)
        {
            bot.connection.Send("access", tbCode.Text.Split('#').First());
        }

        private void btnAddEmail_Click(object sender, EventArgs e)
        {
            if (!cbEmail.Items.Contains(cbEmail.Text))
            {
                cbEmail.Items.Add(cbEmail.Text);
                passwordList.Add(cbEmail.Text, tbPassword.Text);
                SaveData();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonAddServer_Click(object sender, EventArgs e)
        {
            if (!cbServer.Items.Contains(cbServer.Text))
            {
                cbServer.Items.Add(cbServer.Text);
                SaveData();
            }
        }

        private void btnAddWorldId_Click(object sender, EventArgs e)
        {
            if (!cbWorldId.Items.Contains(cbWorldId.Text))
            {
                cbWorldId.Items.Add(cbWorldId.Text);
                SaveData();
            }
        }

        private void cbEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (passwordList.ContainsKey(cbEmail.Text))
                tbPassword.Text = passwordList[cbEmail.Text];
        }

        private void btnAbortTask_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedItem != null)
            {
                Task t = ((SubBots.TaskData)lbTasks.SelectedItem).task;
                lbTasks.Items.Remove(lbTasks.SelectedItem);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                List<SubBots.TaskData> deadTasks = bot.subBotHandler.Update((int)nudTaskTimeLimit.Value);

                foreach (var o in deadTasks)
                {
                    if (o.stopwatch.ElapsedMilliseconds / 1000 > nudTaskAbortLimit.Value)
                    {
                        if (BotUtility.isTaskRunning(o.task))//(o.task.IsCompleted == false || o.task.Status == TaskStatus.Running || o.task.Status == TaskStatus.WaitingToRun || o.task.Status == TaskStatus.WaitingForActivation)
                        {
                            o.task.Dispose();
                        }
                    }
                }

                {
                    foreach (var o in deadTasks)
                    {
                        lbTasks.Items.Add(o);
                    }
                }

                for (int i = 0; i < lbTasks.Items.Count; i++)
                {
                    SubBots.TaskData t = (SubBots.TaskData)lbTasks.Items[0];
                    lbTasks.Items.RemoveAt(0);


                    if ((t.task.IsCompleted == false || t.task.Status == TaskStatus.Running || t.task.Status == TaskStatus.WaitingToRun || t.task.Status == TaskStatus.WaitingForActivation))
                    {
                        lbTasks.Items.Add(t);
                    }
                }



            }));
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            bot.room.SetSleepTime((int)numericUpDown1.Value);
        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {
            if (cbServer.Items.Contains(cbServer.Text))
            {
                cbServer.Items.Remove(cbServer.Text);
                SaveData();
                cbServer.Text = "";
            }
        }

        private void btnRemoveEmail_Click(object sender, EventArgs e)
        {
            if (cbEmail.Items.Contains(cbEmail.Text))
            {
                cbEmail.Items.Remove(cbEmail.Text);
                passwordList.Remove(cbEmail.Text);
                SaveData();
                cbEmail.Text = "";
                tbPassword.Text = "";
            }
        }

        private void btnRemoveWorldId_Click(object sender, EventArgs e)
        {
            if (cbWorldId.Items.Contains(cbWorldId.Text))
            {
                cbWorldId.Items.Remove(cbWorldId.Text);
                SaveData();
                cbWorldId.Text = "";
            }
        }

    }
}
