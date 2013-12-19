namespace DynamicEEBot.SubBots
{
    partial class Room_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.repairQueueSizeBox = new System.Windows.Forms.TextBox();
            this.clearRepairQueueButton = new System.Windows.Forms.Button();
            this.blockSetSizeBox = new System.Windows.Forms.TextBox();
            this.blockQueueSizeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draw speed:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(85, 7);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Repairqueue size:";
            // 
            // repairQueueSizeBox
            // 
            this.repairQueueSizeBox.Location = new System.Drawing.Point(113, 57);
            this.repairQueueSizeBox.Name = "repairQueueSizeBox";
            this.repairQueueSizeBox.Size = new System.Drawing.Size(92, 20);
            this.repairQueueSizeBox.TabIndex = 4;
            // 
            // clearRepairQueueButton
            // 
            this.clearRepairQueueButton.Location = new System.Drawing.Point(211, 81);
            this.clearRepairQueueButton.Name = "clearRepairQueueButton";
            this.clearRepairQueueButton.Size = new System.Drawing.Size(61, 23);
            this.clearRepairQueueButton.TabIndex = 5;
            this.clearRepairQueueButton.Text = "Clear";
            this.clearRepairQueueButton.UseVisualStyleBackColor = true;
            this.clearRepairQueueButton.Click += new System.EventHandler(this.clearRepairQueueButton_Click);
            // 
            // blockSetSizeBox
            // 
            this.blockSetSizeBox.Location = new System.Drawing.Point(113, 30);
            this.blockSetSizeBox.Name = "blockSetSizeBox";
            this.blockSetSizeBox.Size = new System.Drawing.Size(92, 20);
            this.blockSetSizeBox.TabIndex = 6;
            // 
            // blockQueueSizeBox
            // 
            this.blockQueueSizeBox.Location = new System.Drawing.Point(113, 83);
            this.blockQueueSizeBox.Name = "blockQueueSizeBox";
            this.blockQueueSizeBox.Size = new System.Drawing.Size(92, 20);
            this.blockQueueSizeBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Blockqueue size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Blockset size:";
            // 
            // Room_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 120);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.blockQueueSizeBox);
            this.Controls.Add(this.blockSetSizeBox);
            this.Controls.Add(this.clearRepairQueueButton);
            this.Controls.Add(this.repairQueueSizeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.Name = "Room_Form";
            this.Text = "Room";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox repairQueueSizeBox;
        private System.Windows.Forms.Button clearRepairQueueButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox blockSetSizeBox;
        public System.Windows.Forms.TextBox blockQueueSizeBox;
    }
}