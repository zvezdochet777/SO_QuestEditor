namespace StalkerOnlineQuesterEditor
{
    partial class StatisticsForm
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
            this.bOK = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(251, 381);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.labelInfo);
            this.panelInfo.Location = new System.Drawing.Point(17, 21);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(545, 332);
            this.panelInfo.TabIndex = 1;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(24, 20);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(81, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "label1ывапывп";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 416);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.bOK);
            this.Name = "StatisticsForm";
            this.Text = "Статистика квестов и диалогов";
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelInfo;
    }
}