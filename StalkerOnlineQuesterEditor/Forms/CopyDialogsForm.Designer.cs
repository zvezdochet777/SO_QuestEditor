namespace StalkerOnlineQuesterEditor.Forms
{
    partial class CopyDialogsForm
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
            this.labelChosenNPC = new System.Windows.Forms.Label();
            this.NPCBox = new System.Windows.Forms.ComboBox();
            this.lbDialogText = new System.Windows.Forms.Label();
            this.cbDialogs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.FakeNPCBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelChosenNPC
            // 
            this.labelChosenNPC.AutoSize = true;
            this.labelChosenNPC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChosenNPC.Location = new System.Drawing.Point(10, 16);
            this.labelChosenNPC.Name = "labelChosenNPC";
            this.labelChosenNPC.Size = new System.Drawing.Size(104, 13);
            this.labelChosenNPC.TabIndex = 6;
            this.labelChosenNPC.Text = "Копировать в NPC:";
            // 
            // NPCBox
            // 
            this.NPCBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.NPCBox.DropDownWidth = 280;
            this.NPCBox.FormattingEnabled = true;
            this.NPCBox.Location = new System.Drawing.Point(124, 12);
            this.NPCBox.Name = "NPCBox";
            this.NPCBox.Size = new System.Drawing.Size(196, 21);
            this.NPCBox.TabIndex = 5;
            this.NPCBox.SelectedIndexChanged += new System.EventHandler(this.NPCBox_SelectedIndexChanged);
            // 
            // lbDialogText
            // 
            this.lbDialogText.AutoSize = true;
            this.lbDialogText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbDialogText.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbDialogText.Location = new System.Drawing.Point(12, 74);
            this.lbDialogText.Name = "lbDialogText";
            this.lbDialogText.Size = new System.Drawing.Size(37, 15);
            this.lbDialogText.TabIndex = 7;
            this.lbDialogText.Text = "label1";
            this.lbDialogText.Visible = false;
            // 
            // cbDialogs
            // 
            this.cbDialogs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.cbDialogs.DropDownWidth = 280;
            this.cbDialogs.FormattingEnabled = true;
            this.cbDialogs.Location = new System.Drawing.Point(124, 40);
            this.cbDialogs.Name = "cbDialogs";
            this.cbDialogs.Size = new System.Drawing.Size(196, 21);
            this.cbDialogs.TabIndex = 8;
            this.cbDialogs.SelectedIndexChanged += new System.EventHandler(this.cbDialogs_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "как дочерний Dialog:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(246, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(158, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FakeNPCBox
            // 
            this.FakeNPCBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.FakeNPCBox.DropDownWidth = 280;
            this.FakeNPCBox.FormattingEnabled = true;
            this.FakeNPCBox.Location = new System.Drawing.Point(125, 12);
            this.FakeNPCBox.Name = "FakeNPCBox";
            this.FakeNPCBox.Size = new System.Drawing.Size(196, 21);
            this.FakeNPCBox.TabIndex = 13;
            this.FakeNPCBox.SelectedIndexChanged += new System.EventHandler(this.FakeNPCBox_SelectedIndexChanged);
            // 
            // CopyDialogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 138);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDialogs);
            this.Controls.Add(this.lbDialogText);
            this.Controls.Add(this.labelChosenNPC);
            this.Controls.Add(this.NPCBox);
            this.Controls.Add(this.FakeNPCBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CopyDialogsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CopyDialogsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelChosenNPC;
        private System.Windows.Forms.ComboBox NPCBox;
        private System.Windows.Forms.Label lbDialogText;
        private System.Windows.Forms.ComboBox cbDialogs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox FakeNPCBox;
    }
}