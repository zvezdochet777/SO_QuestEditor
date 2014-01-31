namespace StalkerOnlineQuesterEditor
{
    partial class RewardFractions
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
            this.dataFractions = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iReward = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.unlimitedCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataFractions)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataFractions
            // 
            this.dataFractions.AllowUserToAddRows = false;
            this.dataFractions.AllowUserToDeleteRows = false;
            this.dataFractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataFractions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.sName,
            this.iReward});
            this.dataFractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataFractions.Location = new System.Drawing.Point(0, 0);
            this.dataFractions.Name = "dataFractions";
            this.dataFractions.Size = new System.Drawing.Size(449, 330);
            this.dataFractions.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // sName
            // 
            this.sName.HeaderText = "Имя фракции";
            this.sName.Name = "sName";
            this.sName.ReadOnly = true;
            this.sName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // iReward
            // 
            this.iReward.HeaderText = "Принадлежность";
            this.iReward.Name = "iReward";
            this.iReward.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iReward.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.unlimitedCheckBox);
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 330);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 61);
            this.panel1.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(367, 33);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(286, 33);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // unlimitedCheckBox
            // 
            this.unlimitedCheckBox.AutoSize = true;
            this.unlimitedCheckBox.Location = new System.Drawing.Point(3, 6);
            this.unlimitedCheckBox.Name = "unlimitedCheckBox";
            this.unlimitedCheckBox.Size = new System.Drawing.Size(96, 17);
            this.unlimitedCheckBox.TabIndex = 2;
            this.unlimitedCheckBox.Text = "Безлимитный";
            this.unlimitedCheckBox.UseVisualStyleBackColor = true;
            // 
            // RewardFractions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 391);
            this.Controls.Add(this.dataFractions);
            this.Controls.Add(this.panel1);
            this.Name = "RewardFractions";
            this.Text = "Репутация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RewardFractions_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataFractions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataFractions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iReward;
        private System.Windows.Forms.CheckBox unlimitedCheckBox;
    }
}