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
    public partial class MdiForm : Form
    {
        Bot bot;

        public MdiForm()
        {
        }

        public MdiForm(Bot bot)
        {
            InitializeComponent();
            MaximizedBounds = new Rectangle(2, 23, bot.form.tabs.Width - 2, bot.form.tabs.Height - 25);
            this.bot = bot;
            bot.form.SizeChanged += delegate
            {
                MaximizedBounds = new Rectangle(2, 23, bot.form.tabs.Width - 2, bot.form.tabs.Height - 25);
            };
        }

        private void MdiForm_Resize(object sender, EventArgs e)
        {
            MdiForm form = (MdiForm)sender;
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
                Location = new Point(2, bot.form.tabs.Height - 25);
                Show();
            }
            else if (form.WindowState == FormWindowState.Normal)
            {
                form.WindowState = FormWindowState.Normal;
            }
        }
    }
}
