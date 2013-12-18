namespace DynamicEEBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.loginTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.accessButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoveWorldId = new System.Windows.Forms.Button();
            this.btnAddWorldId = new System.Windows.Forms.Button();
            this.cbWorldId = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveEmail = new System.Windows.Forms.Button();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnAddEmail = new System.Windows.Forms.Button();
            this.buttonAddServer = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEmail = new System.Windows.Forms.ComboBox();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.subbotsTab = new System.Windows.Forms.TabPage();
            this.subbotCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAbortTask = new System.Windows.Forms.Button();
            this.nudTaskAbortLimit = new System.Windows.Forms.NumericUpDown();
            this.nudTaskTimeLimit = new System.Windows.Forms.NumericUpDown();
            this.lbTasks = new System.Windows.Forms.ListBox();
            this.subBotsTabPage = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabs.SuspendLayout();
            this.loginTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.subbotsTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskAbortLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskTimeLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "World ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Email:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(6, 99);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 23);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(6, 40);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 23);
            this.connectButton.TabIndex = 16;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.loginTab);
            this.tabs.Controls.Add(this.subbotsTab);
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.subBotsTabPage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(484, 362);
            this.tabs.TabIndex = 17;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // loginTab
            // 
            this.loginTab.Controls.Add(this.groupBox3);
            this.loginTab.Controls.Add(this.groupBox2);
            this.loginTab.Controls.Add(this.groupBox1);
            this.loginTab.Location = new System.Drawing.Point(4, 22);
            this.loginTab.Name = "loginTab";
            this.loginTab.Padding = new System.Windows.Forms.Padding(3);
            this.loginTab.Size = new System.Drawing.Size(476, 336);
            this.loginTab.TabIndex = 1;
            this.loginTab.Text = "Login";
            this.loginTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCode);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.accessButton);
            this.groupBox3.Location = new System.Drawing.Point(8, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(47, 13);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(100, 20);
            this.tbCode.TabIndex = 0;
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(9, 38);
            this.accessButton.Name = "accessButton";
            this.accessButton.Size = new System.Drawing.Size(100, 23);
            this.accessButton.TabIndex = 17;
            this.accessButton.Text = "Access";
            this.accessButton.UseVisualStyleBackColor = true;
            this.accessButton.Click += new System.EventHandler(this.accessButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemoveWorldId);
            this.groupBox2.Controls.Add(this.btnAddWorldId);
            this.groupBox2.Controls.Add(this.cbWorldId);
            this.groupBox2.Controls.Add(this.connectButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 75);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect";
            // 
            // btnRemoveWorldId
            // 
            this.btnRemoveWorldId.Location = new System.Drawing.Point(414, 11);
            this.btnRemoveWorldId.Name = "btnRemoveWorldId";
            this.btnRemoveWorldId.Size = new System.Drawing.Size(37, 23);
            this.btnRemoveWorldId.TabIndex = 24;
            this.btnRemoveWorldId.TabStop = false;
            this.btnRemoveWorldId.Text = "Del";
            this.btnRemoveWorldId.UseVisualStyleBackColor = true;
            this.btnRemoveWorldId.Click += new System.EventHandler(this.btnRemoveWorldId_Click);
            // 
            // btnAddWorldId
            // 
            this.btnAddWorldId.Location = new System.Drawing.Point(371, 11);
            this.btnAddWorldId.Name = "btnAddWorldId";
            this.btnAddWorldId.Size = new System.Drawing.Size(37, 23);
            this.btnAddWorldId.TabIndex = 23;
            this.btnAddWorldId.TabStop = false;
            this.btnAddWorldId.Text = "Add";
            this.btnAddWorldId.UseVisualStyleBackColor = true;
            this.btnAddWorldId.Click += new System.EventHandler(this.btnAddWorldId_Click);
            // 
            // cbWorldId
            // 
            this.cbWorldId.FormattingEnabled = true;
            this.cbWorldId.Location = new System.Drawing.Point(64, 13);
            this.cbWorldId.Name = "cbWorldId";
            this.cbWorldId.Size = new System.Drawing.Size(301, 21);
            this.cbWorldId.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveEmail);
            this.groupBox1.Controls.Add(this.btnRemoveServer);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.btnAddEmail);
            this.groupBox1.Controls.Add(this.buttonAddServer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbEmail);
            this.groupBox1.Controls.Add(this.cbServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.loginButton);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // btnRemoveEmail
            // 
            this.btnRemoveEmail.Location = new System.Drawing.Point(414, 71);
            this.btnRemoveEmail.Name = "btnRemoveEmail";
            this.btnRemoveEmail.Size = new System.Drawing.Size(37, 23);
            this.btnRemoveEmail.TabIndex = 24;
            this.btnRemoveEmail.TabStop = false;
            this.btnRemoveEmail.Text = "Del";
            this.btnRemoveEmail.UseVisualStyleBackColor = true;
            this.btnRemoveEmail.Click += new System.EventHandler(this.btnRemoveEmail_Click);
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.Location = new System.Drawing.Point(414, 17);
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Size = new System.Drawing.Size(37, 23);
            this.btnRemoveServer.TabIndex = 23;
            this.btnRemoveServer.TabStop = false;
            this.btnRemoveServer.Text = "Del";
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(73, 73);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(292, 20);
            this.tbPassword.TabIndex = 2;
            // 
            // btnAddEmail
            // 
            this.btnAddEmail.Location = new System.Drawing.Point(371, 71);
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.Size = new System.Drawing.Size(37, 23);
            this.btnAddEmail.TabIndex = 21;
            this.btnAddEmail.TabStop = false;
            this.btnAddEmail.Text = "Add";
            this.btnAddEmail.UseVisualStyleBackColor = true;
            this.btnAddEmail.Click += new System.EventHandler(this.btnAddEmail_Click);
            // 
            // buttonAddServer
            // 
            this.buttonAddServer.Location = new System.Drawing.Point(371, 17);
            this.buttonAddServer.Name = "buttonAddServer";
            this.buttonAddServer.Size = new System.Drawing.Size(37, 23);
            this.buttonAddServer.TabIndex = 20;
            this.buttonAddServer.TabStop = false;
            this.buttonAddServer.Text = "Add";
            this.buttonAddServer.UseVisualStyleBackColor = true;
            this.buttonAddServer.Click += new System.EventHandler(this.buttonAddServer_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Server:";
            // 
            // cbEmail
            // 
            this.cbEmail.FormattingEnabled = true;
            this.cbEmail.Location = new System.Drawing.Point(73, 46);
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.Size = new System.Drawing.Size(292, 21);
            this.cbEmail.TabIndex = 1;
            this.cbEmail.SelectedIndexChanged += new System.EventHandler(this.cbEmail_SelectedIndexChanged);
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Items.AddRange(new object[] {
            "everybody-edits-su9rn58o40itdbnw69plyw"});
            this.cbServer.Location = new System.Drawing.Point(73, 19);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(292, 21);
            this.cbServer.TabIndex = 0;
            this.cbServer.TabStop = false;
            this.cbServer.Text = "everybody-edits-su9rn58o40itdbnw69plyw";
            // 
            // subbotsTab
            // 
            this.subbotsTab.Controls.Add(this.subbotCheckedListBox);
            this.subbotsTab.Location = new System.Drawing.Point(4, 22);
            this.subbotsTab.Name = "subbotsTab";
            this.subbotsTab.Size = new System.Drawing.Size(476, 336);
            this.subbotsTab.TabIndex = 2;
            this.subbotsTab.Text = "Bot systems";
            this.subbotsTab.UseVisualStyleBackColor = true;
            // 
            // subbotCheckedListBox
            // 
            this.subbotCheckedListBox.FormattingEnabled = true;
            this.subbotCheckedListBox.Location = new System.Drawing.Point(3, 3);
            this.subbotCheckedListBox.Name = "subbotCheckedListBox";
            this.subbotCheckedListBox.Size = new System.Drawing.Size(425, 229);
            this.subbotCheckedListBox.TabIndex = 0;
            this.subbotCheckedListBox.SelectedValueChanged += new System.EventHandler(this.subbotCheckedListBox_SelectedValueChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnAbortTask);
            this.tabPage1.Controls.Add(this.nudTaskAbortLimit);
            this.tabPage1.Controls.Add(this.nudTaskTimeLimit);
            this.tabPage1.Controls.Add(this.lbTasks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 336);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Tasks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Abort time limit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Time limit:";
            // 
            // btnAbortTask
            // 
            this.btnAbortTask.Location = new System.Drawing.Point(245, 58);
            this.btnAbortTask.Name = "btnAbortTask";
            this.btnAbortTask.Size = new System.Drawing.Size(75, 23);
            this.btnAbortTask.TabIndex = 3;
            this.btnAbortTask.Text = "Abort task";
            this.btnAbortTask.UseVisualStyleBackColor = true;
            this.btnAbortTask.Click += new System.EventHandler(this.btnAbortTask_Click);
            // 
            // nudTaskAbortLimit
            // 
            this.nudTaskAbortLimit.Location = new System.Drawing.Point(329, 32);
            this.nudTaskAbortLimit.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.nudTaskAbortLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaskAbortLimit.Name = "nudTaskAbortLimit";
            this.nudTaskAbortLimit.Size = new System.Drawing.Size(120, 20);
            this.nudTaskAbortLimit.TabIndex = 2;
            this.nudTaskAbortLimit.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // nudTaskTimeLimit
            // 
            this.nudTaskTimeLimit.Location = new System.Drawing.Point(329, 6);
            this.nudTaskTimeLimit.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.nudTaskTimeLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaskTimeLimit.Name = "nudTaskTimeLimit";
            this.nudTaskTimeLimit.Size = new System.Drawing.Size(120, 20);
            this.nudTaskTimeLimit.TabIndex = 1;
            this.nudTaskTimeLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lbTasks
            // 
            this.lbTasks.FormattingEnabled = true;
            this.lbTasks.Location = new System.Drawing.Point(6, 6);
            this.lbTasks.Name = "lbTasks";
            this.lbTasks.Size = new System.Drawing.Size(233, 316);
            this.lbTasks.TabIndex = 0;
            // 
            // subBotsTabPage
            // 
            this.subBotsTabPage.Location = new System.Drawing.Point(4, 22);
            this.subBotsTabPage.Name = "subBotsTabPage";
            this.subBotsTabPage.Size = new System.Drawing.Size(476, 336);
            this.subBotsTabPage.TabIndex = 5;
            this.subBotsTabPage.Text = "SubBots";
            this.subBotsTabPage.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabs.ResumeLayout(false);
            this.loginTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.subbotsTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskAbortLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskTimeLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TabPage loginTab;
        private System.Windows.Forms.TabPage subbotsTab;
        public System.Windows.Forms.TabControl tabs;
        public System.Windows.Forms.CheckedListBox subbotCheckedListBox;
        private System.Windows.Forms.Button accessButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddWorldId;
        private System.Windows.Forms.ComboBox cbWorldId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnAddEmail;
        private System.Windows.Forms.Button buttonAddServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEmail;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown nudTaskAbortLimit;
        private System.Windows.Forms.NumericUpDown nudTaskTimeLimit;
        private System.Windows.Forms.ListBox lbTasks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAbortTask;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRemoveWorldId;
        private System.Windows.Forms.Button btnRemoveEmail;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.TabPage subBotsTabPage;
    }
}

