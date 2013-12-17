using DynamicEEBot.Subbots;
using DynamicEEBot.Subbots.Dig;
using DynamicEEBot.Subbots.WorldEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicEEBot
{
    public class Bot : BotBase
    {
        public SubBots.SubBotHandler subBotHandler;
        public Room room;

        public Bot(Form1 form)
            : base(form)
        {
            subBotHandler = new SubBots.SubBotHandler();
            /*this.room = new Room(this);
            subBotHandler.AddSubBot(room, this);
            subBotHandler.AddSubBot(new PlayerPhysics(this), this);
            subBotHandler.AddSubBot(new Commands(this), this);
            subBotHandler.AddSubBot(new Zombies(this), this);
            subBotHandler.AddSubBot(new WorldEdit(this), this);
            subBotHandler.AddSubBot(new Dig(this), this);
            subBotHandler.AddSubBot(new MazeGenerator(this), this);*/
        }

        protected override void OnMessage(object sender, PlayerIOClient.Message m)
        {
            new System.Threading.Thread(() =>
                {
                    base.OnMessage(sender, m);
                    subBotHandler.onMessage(sender, m, this);
                    switch (m.Type)
                    {
                        case "say":
                            {
                                int player = m.GetInt(0);
                                string message = m.GetString(1);
                                if (message[0] == '!')
                                {
                                    message = message.TrimStart('!');
                                    if (playerList.ContainsKey(player))
                                        subBotHandler.onCommand(sender, message, playerList[player], this);
                                }
                            }
                            break;
                    }
                }).Start();
        }

        public void OnConnect()
        {
            subBotHandler = new SubBots.SubBotHandler();
            this.room = new Room(this);
            subBotHandler.AddSubBot(room, this);
            subBotHandler.AddSubBot(new PlayerPhysics(this), this);
            subBotHandler.AddSubBot(new Commands(this), this);
            subBotHandler.AddSubBot(new Zombies(this), this);
            subBotHandler.AddSubBot(new WorldEdit(this), this);
            subBotHandler.AddSubBot(new Dig(this), this);
            subBotHandler.AddSubBot(new MazeGenerator(this), this);
            subBotHandler.AddSubBot(new Redstone(this), this);
        }

        protected override void OnDisconnect(object sender, string reason)
        {
            subBotHandler.OnDisconnect(sender, reason, this);
        }

        public void Disconnect()
        {
            if (connection != null && connection.Connected)
                connection.Disconnect();
            else
                OnDisconnect(null, "Form closing");
        }
    }
}
