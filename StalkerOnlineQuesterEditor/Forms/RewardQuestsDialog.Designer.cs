namespace StalkerOnlineQuesterEditor
{
    partial class RewardQuestsDialog
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
            this.bCancel = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.dataGridQuests = new System.Windows.Forms.DataGridView();
            this.stack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cbRandom = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuests)).BeginInit();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(327, 29);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(327, 2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // dataGridQuests
            // 
            this.dataGridQuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridQuests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stack,
            this.name});
            this.dataGridQuests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridQuests.Location = new System.Drawing.Point(0, 0);
            this.dataGridQuests.Name = "dataGridQuests";
            this.dataGridQuests.Size = new System.Drawing.Size(411, 227);
            this.dataGridQuests.TabIndex = 3;
            // 
            // stack
            // 
            this.stack.HeaderText = "ID квеста";
            this.stack.Name = "stack";
            // 
            // name
            // 
            this.name.HeaderText = "Действие с квестом";
            this.name.Items.AddRange(new object[] {
            "Открыть (Open)",
            "Закрыть (Close)",
            "Провалить (Fail)",
            "Отменить (Cancel)"});
            this.name.Name = "name";
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.name.Width = 180;
            // 
            // cbRandom
            // 
            this.cbRandom.AutoSize = true;
            this.cbRandom.Location = new System.Drawing.Point(327, 59);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(66, 17);
            this.cbRandom.TabIndex = 5;
            this.cbRandom.Text = "Random";
            this.cbRandom.UseVisualStyleBackColor = true;
            // 
            // RewardQuestsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 227);
            this.Controls.Add(this.cbRandom);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.dataGridQuests);
            this.Name = "RewardQuestsDialog";
            this.Text = "RewardQuestsDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.DataGridView dataGridQuests;
        private System.Windows.Forms.DataGridViewTextBoxColumn stack;
        private System.Windows.Forms.DataGridViewComboBoxColumn name;
        private System.Windows.Forms.CheckBox cbRandom;
    }
}