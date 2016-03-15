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
            this.tabStats = new System.Windows.Forms.TabControl();
            this.pageTranslation = new System.Windows.Forms.TabPage();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lLocalizationInfo = new System.Windows.Forms.Label();
            this.pageReward = new System.Windows.Forms.TabPage();
            this.dataFractionStats = new System.Windows.Forms.DataGridView();
            this.lRewardInfo = new System.Windows.Forms.Label();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFractionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuestsNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabStats.SuspendLayout();
            this.pageTranslation.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.pageReward.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataFractionStats)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(251, 370);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(104, 34);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.pageTranslation);
            this.tabStats.Controls.Add(this.pageReward);
            this.tabStats.Location = new System.Drawing.Point(12, 12);
            this.tabStats.Name = "tabStats";
            this.tabStats.SelectedIndex = 0;
            this.tabStats.Size = new System.Drawing.Size(554, 352);
            this.tabStats.TabIndex = 2;
            // 
            // pageTranslation
            // 
            this.pageTranslation.Controls.Add(this.panelInfo);
            this.pageTranslation.Location = new System.Drawing.Point(4, 22);
            this.pageTranslation.Name = "pageTranslation";
            this.pageTranslation.Padding = new System.Windows.Forms.Padding(3);
            this.pageTranslation.Size = new System.Drawing.Size(546, 326);
            this.pageTranslation.TabIndex = 0;
            this.pageTranslation.Text = "Переводы";
            this.pageTranslation.UseVisualStyleBackColor = true;
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.lLocalizationInfo);
            this.panelInfo.Location = new System.Drawing.Point(3, 6);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(537, 314);
            this.panelInfo.TabIndex = 2;
            // 
            // lLocalizationInfo
            // 
            this.lLocalizationInfo.AutoSize = true;
            this.lLocalizationInfo.Location = new System.Drawing.Point(24, 20);
            this.lLocalizationInfo.Name = "lLocalizationInfo";
            this.lLocalizationInfo.Size = new System.Drawing.Size(81, 13);
            this.lLocalizationInfo.TabIndex = 0;
            this.lLocalizationInfo.Text = "label1ывапывп";
            // 
            // pageReward
            // 
            this.pageReward.Controls.Add(this.dataFractionStats);
            this.pageReward.Controls.Add(this.lRewardInfo);
            this.pageReward.Location = new System.Drawing.Point(4, 22);
            this.pageReward.Name = "pageReward";
            this.pageReward.Padding = new System.Windows.Forms.Padding(3);
            this.pageReward.Size = new System.Drawing.Size(546, 326);
            this.pageReward.TabIndex = 1;
            this.pageReward.Text = "Награды";
            this.pageReward.UseVisualStyleBackColor = true;
            // 
            // dataFractionStats
            // 
            this.dataFractionStats.AllowUserToAddRows = false;
            this.dataFractionStats.AllowUserToDeleteRows = false;
            this.dataFractionStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataFractionStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colFractionName,
            this.colPlus,
            this.colMinus,
            this.colQuestsNum});
            this.dataFractionStats.Location = new System.Drawing.Point(34, 125);
            this.dataFractionStats.Name = "dataFractionStats";
            this.dataFractionStats.ReadOnly = true;
            this.dataFractionStats.Size = new System.Drawing.Size(461, 195);
            this.dataFractionStats.TabIndex = 1;
            // 
            // lRewardInfo
            // 
            this.lRewardInfo.AutoSize = true;
            this.lRewardInfo.Location = new System.Drawing.Point(6, 14);
            this.lRewardInfo.Name = "lRewardInfo";
            this.lRewardInfo.Size = new System.Drawing.Size(91, 13);
            this.lRewardInfo.TabIndex = 0;
            this.lRewardInfo.Text = "label1sadgdfgdgh";
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 40;
            // 
            // colFractionName
            // 
            this.colFractionName.HeaderText = "Фракция";
            this.colFractionName.Name = "colFractionName";
            this.colFractionName.ReadOnly = true;
            // 
            // colPlus
            // 
            this.colPlus.HeaderText = "Бонусы";
            this.colPlus.Name = "colPlus";
            this.colPlus.ReadOnly = true;
            this.colPlus.Width = 70;
            // 
            // colMinus
            // 
            this.colMinus.HeaderText = "Штрафы";
            this.colMinus.Name = "colMinus";
            this.colMinus.ReadOnly = true;
            this.colMinus.Width = 70;
            // 
            // colQuestsNum
            // 
            this.colQuestsNum.HeaderText = "Число квестов";
            this.colQuestsNum.Name = "colQuestsNum";
            this.colQuestsNum.ReadOnly = true;
            this.colQuestsNum.Width = 70;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 416);
            this.Controls.Add(this.tabStats);
            this.Controls.Add(this.bOK);
            this.Name = "StatisticsForm";
            this.Text = "Статистика квестов и диалогов";
            this.tabStats.ResumeLayout(false);
            this.pageTranslation.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.pageReward.ResumeLayout(false);
            this.pageReward.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataFractionStats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.TabControl tabStats;
        private System.Windows.Forms.TabPage pageTranslation;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lLocalizationInfo;
        private System.Windows.Forms.TabPage pageReward;
        private System.Windows.Forms.Label lRewardInfo;
        private System.Windows.Forms.DataGridView dataFractionStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFractionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuestsNum;
    }
}