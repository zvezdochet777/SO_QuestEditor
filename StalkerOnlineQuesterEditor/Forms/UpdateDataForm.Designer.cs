namespace StalkerOnlineQuesterEditor.Forms
{
    partial class UpdateDataForm
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
            this.btnUpdateItems = new System.Windows.Forms.Button();
            this.btnUpdateNPCStats = new System.Windows.Forms.Button();
            this.btnUpdateTP = new System.Windows.Forms.Button();
            this.btnUpdateAreas = new System.Windows.Forms.Button();
            this.btnUpdateTriggers = new System.Windows.Forms.Button();
            this.btnUpdateMobs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdateItems
            // 
            this.btnUpdateItems.Location = new System.Drawing.Point(32, 12);
            this.btnUpdateItems.Name = "btnUpdateItems";
            this.btnUpdateItems.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateItems.TabIndex = 0;
            this.btnUpdateItems.Text = "Обновить Items";
            this.btnUpdateItems.UseVisualStyleBackColor = true;
            this.btnUpdateItems.Click += new System.EventHandler(this.btnUpdateItems_Click);
            // 
            // btnUpdateNPCStats
            // 
            this.btnUpdateNPCStats.Location = new System.Drawing.Point(32, 44);
            this.btnUpdateNPCStats.Name = "btnUpdateNPCStats";
            this.btnUpdateNPCStats.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateNPCStats.TabIndex = 2;
            this.btnUpdateNPCStats.Text = "Обновить NPC stats";
            this.btnUpdateNPCStats.UseVisualStyleBackColor = true;
            this.btnUpdateNPCStats.Click += new System.EventHandler(this.btnUpdateNPCStats_Click);
            // 
            // btnUpdateTP
            // 
            this.btnUpdateTP.Location = new System.Drawing.Point(32, 76);
            this.btnUpdateTP.Name = "btnUpdateTP";
            this.btnUpdateTP.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateTP.TabIndex = 3;
            this.btnUpdateTP.Text = "Обновить TPPoints";
            this.btnUpdateTP.UseVisualStyleBackColor = true;
            this.btnUpdateTP.Click += new System.EventHandler(this.btnUpdateTP_Click);
            // 
            // btnUpdateAreas
            // 
            this.btnUpdateAreas.Location = new System.Drawing.Point(32, 108);
            this.btnUpdateAreas.Name = "btnUpdateAreas";
            this.btnUpdateAreas.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateAreas.TabIndex = 4;
            this.btnUpdateAreas.Text = "Обновить Areas";
            this.btnUpdateAreas.UseVisualStyleBackColor = true;
            this.btnUpdateAreas.Click += new System.EventHandler(this.btnUpdateAreas_Click);
            // 
            // btnUpdateTriggers
            // 
            this.btnUpdateTriggers.Location = new System.Drawing.Point(32, 140);
            this.btnUpdateTriggers.Name = "btnUpdateTriggers";
            this.btnUpdateTriggers.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateTriggers.TabIndex = 5;
            this.btnUpdateTriggers.Text = "Обновить Триггеры";
            this.btnUpdateTriggers.UseVisualStyleBackColor = true;
            this.btnUpdateTriggers.Click += new System.EventHandler(this.btnUpdateTriggers_Click);
            // 
            // btnUpdateMobs
            // 
            this.btnUpdateMobs.Location = new System.Drawing.Point(32, 172);
            this.btnUpdateMobs.Name = "btnUpdateMobs";
            this.btnUpdateMobs.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateMobs.TabIndex = 6;
            this.btnUpdateMobs.Text = "Обновить Мобов";
            this.btnUpdateMobs.UseVisualStyleBackColor = true;
            this.btnUpdateMobs.Click += new System.EventHandler(this.btnUpdateMobs_Click);
            // 
            // UpdateDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 261);
            this.Controls.Add(this.btnUpdateMobs);
            this.Controls.Add(this.btnUpdateTriggers);
            this.Controls.Add(this.btnUpdateAreas);
            this.Controls.Add(this.btnUpdateTP);
            this.Controls.Add(this.btnUpdateNPCStats);
            this.Controls.Add(this.btnUpdateItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.Name = "UpdateDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateDataForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateItems;
        private System.Windows.Forms.Button btnUpdateNPCStats;
        private System.Windows.Forms.Button btnUpdateTP;
        private System.Windows.Forms.Button btnUpdateAreas;
        private System.Windows.Forms.Button btnUpdateTriggers;
        private System.Windows.Forms.Button btnUpdateMobs;
    }
}