using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicEEBot.SubBots
{
    public partial class Room_Form : MdiForm
    {
        Room room;

        public Room_Form(Bot bot, Room room)
            :base(bot)
        {
            InitializeComponent();
            this.room = room;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            room.DrawSleep = (int)numericUpDown1.Value;
        }

        private void clearRepairQueueButton_Click(object sender, EventArgs e)
        {
            room.ClearRepairQueue();
        }
    }
}
