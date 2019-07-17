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
            this.btnUpdateMobs = new System.Windows.Forms.Button();
            this.btnUpdateBoardQuests = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            // btnUpdateMobs
            // 
            this.btnUpdateMobs.Location = new System.Drawing.Point(32, 107);
            this.btnUpdateMobs.Name = "btnUpdateMobs";
            this.btnUpdateMobs.Size = new System.Drawing.Size(120, 26);
            this.btnUpdateMobs.TabIndex = 6;
            this.btnUpdateMobs.Text = "Обновить Мобов";
            this.btnUpdateMobs.UseVisualStyleBackColor = true;
            this.btnUpdateMobs.Click += new System.EventHandler(this.btnUpdateMobs_Click);
            // 
            // btnUpdateBoardQuests
            // 
            this.btnUpdateBoardQuests.Location = new System.Drawing.Point(32, 139);
            this.btnUpdateBoardQuests.Name = "btnUpdateBoardQuests";
            this.btnUpdateBoardQuests.Size = new System.Drawing.Size(120, 45);
            this.btnUpdateBoardQuests.TabIndex = 7;
            this.btnUpdateBoardQuests.Text = "Обновить Квесты с доски";
            this.btnUpdateBoardQuests.UseVisualStyleBackColor = true;
            this.btnUpdateBoardQuests.Click += new System.EventHandler(this.btnUpdateBoardQuests_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 57);
            this.button1.TabIndex = 8;
            this.button1.Text = "Обновить Объекты с карт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 26);
            this.button2.TabIndex = 9;
            this.button2.Text = "Обновить Данжи";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UpdateDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 261);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUpdateBoardQuests);
            this.Controls.Add(this.btnUpdateMobs);
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
        private System.Windows.Forms.Button btnUpdateMobs;
        private System.Windows.Forms.Button btnUpdateBoardQuests;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}