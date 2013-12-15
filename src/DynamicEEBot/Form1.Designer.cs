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
            this.codeBox = new System.Windows.Forms.TextBox();
            this.worldIdBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.loginTab = new System.Windows.Forms.TabPage();
            this.accessButton = new System.Windows.Forms.Button();
            this.subbotsTab = new System.Windows.Forms.TabPage();
            this.subbotCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tabs.SuspendLayout();
            this.loginTab.SuspendLayout();
            this.subbotsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "World ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "EE Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "EE Email:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(12, 113);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 23);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(91, 87);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(127, 20);
            this.codeBox.TabIndex = 10;
            this.codeBox.Text = "1111";
            // 
            // worldIdBox
            // 
            this.worldIdBox.Location = new System.Drawing.Point(91, 60);
            this.worldIdBox.Name = "worldIdBox";
            this.worldIdBox.Size = new System.Drawing.Size(127, 20);
            this.worldIdBox.TabIndex = 9;
            this.worldIdBox.Text = "PWlAD5rLY1bEI";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(90, 33);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(128, 20);
            this.passwordBox.TabIndex = 8;
            this.passwordBox.Text = "kasekakorna";
            // 
            // emailBox
            // 
            this.emailBox.Location = new System.Drawing.Point(90, 6);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(128, 20);
            this.emailBox.TabIndex = 7;
            this.emailBox.Text = "gustav9797@hotmail.se";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(118, 113);
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
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(439, 270);
            this.tabs.TabIndex = 17;
            // 
            // loginTab
            // 
            this.loginTab.Controls.Add(this.accessButton);
            this.loginTab.Controls.Add(this.emailBox);
            this.loginTab.Controls.Add(this.connectButton);
            this.loginTab.Controls.Add(this.passwordBox);
            this.loginTab.Controls.Add(this.label4);
            this.loginTab.Controls.Add(this.worldIdBox);
            this.loginTab.Controls.Add(this.label3);
            this.loginTab.Controls.Add(this.codeBox);
            this.loginTab.Controls.Add(this.label2);
            this.loginTab.Controls.Add(this.loginButton);
            this.loginTab.Controls.Add(this.label1);
            this.loginTab.Location = new System.Drawing.Point(4, 4);
            this.loginTab.Name = "loginTab";
            this.loginTab.Padding = new System.Windows.Forms.Padding(3);
            this.loginTab.Size = new System.Drawing.Size(431, 244);
            this.loginTab.TabIndex = 1;
            this.loginTab.Text = "Login";
            this.loginTab.UseVisualStyleBackColor = true;
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(224, 113);
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
            this.subbotsTab.Size = new System.Drawing.Size(431, 244);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 291);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabs.ResumeLayout(false);
            this.loginTab.ResumeLayout(false);
            this.loginTab.PerformLayout();
            this.subbotsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.TextBox worldIdBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TabPage loginTab;
        private System.Windows.Forms.TabPage subbotsTab;
        public System.Windows.Forms.TabControl tabs;
        public System.Windows.Forms.CheckedListBox subbotCheckedListBox;
        public System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Button accessButton;
    }
}

