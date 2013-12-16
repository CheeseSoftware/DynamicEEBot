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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.loginTab = new System.Windows.Forms.TabPage();
            this.accessButton = new System.Windows.Forms.Button();
            this.subbotsTab = new System.Windows.Forms.TabPage();
            this.subbotCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.cbEmail = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAddServer = new System.Windows.Forms.Button();
            this.btnAddEmail = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbWorldId = new System.Windows.Forms.ComboBox();
            this.btnAddWorldId = new System.Windows.Forms.Button();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.loginTab.SuspendLayout();
            this.subbotsTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "EE Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "EE Email:";
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
            this.tabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabs.Controls.Add(this.loginTab);
            this.tabs.Controls.Add(this.subbotsTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(484, 362);
            this.tabs.TabIndex = 17;
            // 
            // loginTab
            // 
            this.loginTab.Controls.Add(this.groupBox3);
            this.loginTab.Controls.Add(this.groupBox2);
            this.loginTab.Controls.Add(this.groupBox1);
            this.loginTab.Location = new System.Drawing.Point(4, 4);
            this.loginTab.Name = "loginTab";
            this.loginTab.Padding = new System.Windows.Forms.Padding(3);
            this.loginTab.Size = new System.Drawing.Size(476, 336);
            this.loginTab.TabIndex = 1;
            this.loginTab.Text = "Login";
            this.loginTab.UseVisualStyleBackColor = true;
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
            // subbotsTab
            // 
            this.subbotsTab.Controls.Add(this.subbotCheckedListBox);
            this.subbotsTab.Location = new System.Drawing.Point(4, 4);
            this.subbotsTab.Name = "subbotsTab";
            this.subbotsTab.Size = new System.Drawing.Size(453, 265);
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
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(310, 130);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddWorldId);
            this.groupBox2.Controls.Add(this.cbWorldId);
            this.groupBox2.Controls.Add(this.connectButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 75);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCode);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.accessButton);
            this.groupBox3.Location = new System.Drawing.Point(8, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 100);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(73, 19);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(121, 21);
            this.cbServer.TabIndex = 16;
            this.cbServer.Text = "everybody-edits-su9rn58o40itdbnw69plyw";
            // 
            // cbEmail
            // 
            this.cbEmail.FormattingEnabled = true;
            this.cbEmail.Location = new System.Drawing.Point(73, 46);
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.Size = new System.Drawing.Size(121, 21);
            this.cbEmail.TabIndex = 17;
            this.cbEmail.SelectedIndexChanged += new System.EventHandler(this.cbEmail_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "label5";
            // 
            // buttonAddServer
            // 
            this.buttonAddServer.Location = new System.Drawing.Point(200, 17);
            this.buttonAddServer.Name = "buttonAddServer";
            this.buttonAddServer.Size = new System.Drawing.Size(75, 23);
            this.buttonAddServer.TabIndex = 20;
            this.buttonAddServer.Text = "Add";
            this.buttonAddServer.UseVisualStyleBackColor = true;
            this.buttonAddServer.Click += new System.EventHandler(this.buttonAddServer_Click);
            // 
            // btnAddEmail
            // 
            this.btnAddEmail.Location = new System.Drawing.Point(200, 44);
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.Size = new System.Drawing.Size(75, 23);
            this.btnAddEmail.TabIndex = 21;
            this.btnAddEmail.Text = "Add";
            this.btnAddEmail.UseVisualStyleBackColor = true;
            this.btnAddEmail.Click += new System.EventHandler(this.btnAddEmail_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(73, 73);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(121, 20);
            this.tbPassword.TabIndex = 22;
            // 
            // cbWorldId
            // 
            this.cbWorldId.FormattingEnabled = true;
            this.cbWorldId.Location = new System.Drawing.Point(64, 13);
            this.cbWorldId.Name = "cbWorldId";
            this.cbWorldId.Size = new System.Drawing.Size(121, 21);
            this.cbWorldId.TabIndex = 17;
            // 
            // btnAddWorldId
            // 
            this.btnAddWorldId.Location = new System.Drawing.Point(191, 11);
            this.btnAddWorldId.Name = "btnAddWorldId";
            this.btnAddWorldId.Size = new System.Drawing.Size(75, 23);
            this.btnAddWorldId.TabIndex = 23;
            this.btnAddWorldId.Text = "Add";
            this.btnAddWorldId.UseVisualStyleBackColor = true;
            this.btnAddWorldId.Click += new System.EventHandler(this.btnAddWorldId_Click);
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(47, 13);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(100, 20);
            this.tbCode.TabIndex = 18;
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
            this.subbotsTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
    }
}

