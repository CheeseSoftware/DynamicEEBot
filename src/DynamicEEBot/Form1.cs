using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicEEBot
{
    public partial class Form1 : Form
    {
        Bot bot;

        public Form1()
        {
            InitializeComponent();
            bot = new Bot(this);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (bot.Login(emailBox.Text, passwordBox.Text))
                loginButton.Enabled = false;
            else
                loginButton.Text = "Login failed";
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!bot.connected)
            {
                connectButton.Text = "Connecting...";
                if (bot.Connect(worldIdBox.Text, "Everybodyedits176"))
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
            bot.connection.Send("access", codeBox.Text);
        }
    }
}
