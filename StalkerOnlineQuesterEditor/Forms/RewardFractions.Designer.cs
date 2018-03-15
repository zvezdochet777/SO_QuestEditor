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
            this.pButtons = new System.Windows.Forms.Panel();
            this.lInfo = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataFractions)).BeginInit();
            this.pButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataFractions
            // 
            this.dataFractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataFractions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.sName,
            this.nValue});
            this.dataFractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataFractions.Location = new System.Drawing.Point(0, 0);
            this.dataFractions.Name = "dataFractions";
            this.dataFractions.Size = new System.Drawing.Size(453, 384);
            this.dataFractions.TabIndex = 0;
            // 
            // pButtons
            // 
            this.pButtons.Controls.Add(this.lInfo);
            this.pButtons.Controls.Add(this.bCancel);
            this.pButtons.Controls.Add(this.bOk);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pButtons.Location = new System.Drawing.Point(0, 384);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(453, 61);
            this.pButtons.TabIndex = 1;
            // 
            // lInfo
            // 
            this.lInfo.Location = new System.Drawing.Point(13, 11);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(267, 33);
            this.lInfo.TabIndex = 2;
            this.lInfo.Text = "Положительные значения - прибавка к репутации, отрицательные - штраф к репутации";
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
            this.sName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sName.Width = 150;
            // 
            // nValue
            // 
            this.nValue.HeaderText = "Значение";
            this.nValue.Name = "nValue";
            this.nValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RewardFractions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 445);
            this.Controls.Add(this.dataFractions);
            this.Controls.Add(this.pButtons);
            this.Name = "RewardFractions";
            this.Text = "Репутация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RewardFractions_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataFractions)).EndInit();
            this.pButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataFractions;
        private System.Windows.Forms.Panel pButtons;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sName;
        private System.Windows.Forms.DataGridViewTextBoxColumn nValue;
    }
}