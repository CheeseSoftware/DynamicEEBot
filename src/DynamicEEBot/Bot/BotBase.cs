using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlayerIOClient;
using System.Threading;

namespace DynamicEEBot
{
    public class BotBase
    {
        public Form1 form;
        public Connection connection;
        public Client client;
        public bool loggedIn = false;
        public Dictionary<int, Player> playerList = new Dictionary<int, Player>();
        public Dictionary<string, int> nameList = new Dictionary<string, int>();

        public BotBase(Form1 form)
        {
            this.form = form;
        }

        public bool Login(string email, string password)
        {
            PlayerIOError error = null;
            PlayerIO.QuickConnect.SimpleConnect(
                "everybody-edits-su9rn58o40itdbnw69plyw",
                email,
                password,
                delegate(Client tempClient)
                {
                    client = tempClient;
                    loggedIn = true;
                },
                delegate(PlayerIOError tempError)
                {
                    error = tempError;
                });
            while (!loggedIn && error == null) { }
            if (loggedIn)
                return true;
            else
            {
                MessageBox.Show("Could not login: " + error);
                return false;
            }
        }

        public bool Connect(string worldId, string roomType, string code = "")
        {
            if (loggedIn)
            {
                PlayerIOError error = null;
                client.Multiplayer.CreateJoinRoom(
                    worldId,
                    roomType,
                    true,
                    null,
                    null,
                    delegate(Connection tempConnection)
                    {
                        connection = tempConnection;
                        connection.OnDisconnect += this.OnDisconnect;
                        connection.OnMessage += this.OnMessage;
                        connection.Send("init");
                        connection.Send("init2");
                    },
                    delegate(PlayerIOError tempError)
                    {
                        error = tempError;
                    });
                while (!connected && error == null) { }
                if (connection.Connected)
                {
                    connection.Send("access", form.codeBox.Text);
                    return true;
                }
                else
                {
                    MessageBox.Show("Could not connect: " + error);
                    return false;
                }
            }
            else
                return false;
        }

        protected virtual void OnMessage(object sender, PlayerIOClient.Message m)
        {
            switch (m.Type)
            {
                case "add":
                    {
                        int ID = m.GetInt(0);
                        if (!playerList.ContainsKey(ID))
                        {
                            Player temp = new Player((Bot)this, ID, m.GetString(1), m.GetInt(2), m.GetFloat(3), m.GetFloat(4), m.GetBoolean(5), m.GetBoolean(6), m.GetBoolean(7), m.GetInt(8), m.GetBoolean(10), m.GetBoolean(9), m.GetInt(11));
                            temp.x = m.GetDouble(3);
                            temp.y = m.GetDouble(4);
                            lock (playerList)
                            {
                                playerList.Add(ID, temp);
                            }
                            nameList.Add(temp.name, ID);
                        }
                    }
                    break;
                case "left":
                    {
                        int tempKey = m.GetInt(0);
                        if (playerList.ContainsKey(tempKey))
                        {
                            nameList.Remove(playerList[tempKey].name);
                            lock (playerList)
                            {
                                playerList.Remove(tempKey);
                            }
                        }
                    }
                    break;
                case "m":
                    {
                        int playerID = int.Parse(m[0].ToString());
                        float playerXPos = float.Parse(m[1].ToString());
                        float playerYPos = float.Parse(m[2].ToString());
                        float playerXSpeed = float.Parse(m[3].ToString());
                        float playerYSpeed = float.Parse(m[4].ToString());
                        float modifierX = float.Parse(m[5].ToString());
                        float modifierY = float.Parse(m[6].ToString());
                        int xDir = int.Parse(m[7].ToString());
                        int yDir = int.Parse(m[8].ToString());
                        if (playerList.ContainsKey(playerID))
                        {
                            lock (playerList)
                            {
                                Player player = playerList[playerID];
                                player.x = playerXPos;
                                player.y = playerYPos;
                                player.speedX = playerXSpeed;
                                player.speedY = playerYSpeed;
                                player.modifierX = modifierX;
                                player.modifierY = modifierY;
                                player.horizontal = xDir;
                                player.vertical = yDir;
                                playerList[playerID] = player;
                            }
                        }
                    }
                    break;
                case "god":
                    {
                        int tempKey = m.GetInt(0);
                        if (playerList.ContainsKey(tempKey))
                        {
                            Player player = playerList[tempKey];
                            player.isgod = m.GetBoolean(1);
                            lock (playerList)
                            {
                                playerList[tempKey] = player;
                            }
                        }
                    }
                    break;
                case "face":
                    {
                        int tempKey = m.GetInt(0);
                        if (playerList.ContainsKey(tempKey))
                        {
                            Player player = playerList[tempKey];
                            player.frame(m.GetInt(1));
                            lock (playerList)
                            {
                                playerList[tempKey] = player;
                            }
                        }
                    }
                    break;
                case "lostaccess":
                    break;
                case "k":
                    {
                        int playerID = m.GetInt(0);
                        //Player got crown
                    }
                    break;
                case "ks":
                    {
                        int playerID = m.GetInt(0);
                        //Player won level
                    }
                    break;
                case "c":
                    connection.Send("autosay", 1);
                    break;
            }
        }

        protected virtual void OnDisconnect(object sender, string reason)
        {

        }

        public bool connected { get { return connection != null && connection.Connected; } }
    }
}
