namespace StalkerOnlineQuesterEditor.Forms
{
    partial class LocaleQuestForm
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
            this.questInformationBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.localeDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.localeOnFailedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.localeOnWonTextBox = new System.Windows.Forms.MaskedTextBox();
            this.localeLitleTextBox = new System.Windows.Forms.MaskedTextBox();
            this.lFailed = new System.Windows.Forms.Label();
            this.lWin = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.onFailedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.onWonTextBox = new System.Windows.Forms.MaskedTextBox();
            this.titleTextBox = new System.Windows.Forms.MaskedTextBox();
            this.rewardGroupBox = new System.Windows.Forms.GroupBox();
            this.bItemReward = new System.Windows.Forms.Button();
            this.lQuestRules = new System.Windows.Forms.GroupBox();
            this.bItemQuestRules = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.questInformationBox.SuspendLayout();
            this.rewardGroupBox.SuspendLayout();
            this.lQuestRules.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // questInformationBox
            // 
            this.questInformationBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.questInformationBox.Controls.Add(this.label3);
            this.questInformationBox.Controls.Add(this.label2);
            this.questInformationBox.Controls.Add(this.label1);
            this.questInformationBox.Controls.Add(this.localeDescriptionTextBox);
            this.questInformationBox.Controls.Add(this.localeOnFailedTextBox);
            this.questInformationBox.Controls.Add(this.localeOnWonTextBox);
            this.questInformationBox.Controls.Add(this.localeLitleTextBox);
            this.questInformationBox.Controls.Add(this.lFailed);
            this.questInformationBox.Controls.Add(this.lWin);
            this.questInformationBox.Controls.Add(this.descriptionTextBox);
            this.questInformationBox.Controls.Add(this.lDescription);
            this.questInformationBox.Controls.Add(this.onFailedTextBox);
            this.questInformationBox.Controls.Add(this.onWonTextBox);
            this.questInformationBox.Controls.Add(this.titleTextBox);
            this.questInformationBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.questInformationBox.Location = new System.Drawing.Point(0, 0);
            this.questInformationBox.Name = "questInformationBox";
            this.questInformationBox.Size = new System.Drawing.Size(884, 198);
            this.questInformationBox.TabIndex = 1;
            this.questInformationBox.TabStop = false;
            this.questInformationBox.Text = "Информация";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(589, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Локализация";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Русский язык";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Заголовок:";
            // 
            // localeDescriptionTextBox
            // 
            this.localeDescriptionTextBox.Location = new System.Drawing.Point(427, 71);
            this.localeDescriptionTextBox.Multiline = true;
            this.localeDescriptionTextBox.Name = "localeDescriptionTextBox";
            this.localeDescriptionTextBox.Size = new System.Drawing.Size(407, 62);
            this.localeDescriptionTextBox.TabIndex = 14;
            // 
            // localeOnFailedTextBox
            // 
            this.localeOnFailedTextBox.Location = new System.Drawing.Point(427, 169);
            this.localeOnFailedTextBox.Name = "localeOnFailedTextBox";
            this.localeOnFailedTextBox.Size = new System.Drawing.Size(407, 20);
            this.localeOnFailedTextBox.TabIndex = 13;
            // 
            // localeOnWonTextBox
            // 
            this.localeOnWonTextBox.Location = new System.Drawing.Point(427, 143);
            this.localeOnWonTextBox.Name = "localeOnWonTextBox";
            this.localeOnWonTextBox.Size = new System.Drawing.Size(407, 20);
            this.localeOnWonTextBox.TabIndex = 11;
            // 
            // localeLitleTextBox
            // 
            this.localeLitleTextBox.Location = new System.Drawing.Point(427, 47);
            this.localeLitleTextBox.Name = "localeLitleTextBox";
            this.localeLitleTextBox.Size = new System.Drawing.Size(407, 20);
            this.localeLitleTextBox.TabIndex = 12;
            // 
            // lFailed
            // 
            this.lFailed.AutoSize = true;
            this.lFailed.Location = new System.Drawing.Point(5, 172);
            this.lFailed.Name = "lFailed";
            this.lFailed.Size = new System.Drawing.Size(60, 13);
            this.lFailed.TabIndex = 10;
            this.lFailed.Text = "Проигрыш";
            // 
            // lWin
            // 
            this.lWin.AutoSize = true;
            this.lWin.Location = new System.Drawing.Point(5, 150);
            this.lWin.Name = "lWin";
            this.lWin.Size = new System.Drawing.Size(55, 13);
            this.lWin.TabIndex = 9;
            this.lWin.Text = "Выигрыш";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(72, 71);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(349, 62);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(3, 74);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(60, 13);
            this.lDescription.TabIndex = 2;
            this.lDescription.Text = "Описание:";
            // 
            // onFailedTextBox
            // 
            this.onFailedTextBox.Location = new System.Drawing.Point(72, 169);
            this.onFailedTextBox.Name = "onFailedTextBox";
            this.onFailedTextBox.Size = new System.Drawing.Size(349, 20);
            this.onFailedTextBox.TabIndex = 1;
            // 
            // onWonTextBox
            // 
            this.onWonTextBox.Location = new System.Drawing.Point(72, 143);
            this.onWonTextBox.Name = "onWonTextBox";
            this.onWonTextBox.Size = new System.Drawing.Size(349, 20);
            this.onWonTextBox.TabIndex = 1;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(72, 47);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(349, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // rewardGroupBox
            // 
            this.rewardGroupBox.AutoSize = true;
            this.rewardGroupBox.Controls.Add(this.bItemReward);
            this.rewardGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.rewardGroupBox.Location = new System.Drawing.Point(0, 250);
            this.rewardGroupBox.Name = "rewardGroupBox";
            this.rewardGroupBox.Size = new System.Drawing.Size(884, 61);
            this.rewardGroupBox.TabIndex = 6;
            this.rewardGroupBox.TabStop = false;
            this.rewardGroupBox.Text = "Награда";
            // 
            // bItemReward
            // 
            this.bItemReward.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bItemReward.Location = new System.Drawing.Point(8, 19);
            this.bItemReward.Name = "bItemReward";
            this.bItemReward.Size = new System.Drawing.Size(104, 23);
            this.bItemReward.TabIndex = 32;
            this.bItemReward.Text = "Предметы";
            this.bItemReward.UseVisualStyleBackColor = true;
            this.bItemReward.Click += new System.EventHandler(this.bItemReward_Click);
            // 
            // lQuestRules
            // 
            this.lQuestRules.Controls.Add(this.bItemQuestRules);
            this.lQuestRules.Dock = System.Windows.Forms.DockStyle.Top;
            this.lQuestRules.Location = new System.Drawing.Point(0, 198);
            this.lQuestRules.Name = "lQuestRules";
            this.lQuestRules.Size = new System.Drawing.Size(884, 52);
            this.lQuestRules.TabIndex = 5;
            this.lQuestRules.TabStop = false;
            this.lQuestRules.Text = "Правила квеста";
            // 
            // bItemQuestRules
            // 
            this.bItemQuestRules.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bItemQuestRules.ImageKey = "(none)";
            this.bItemQuestRules.Location = new System.Drawing.Point(10, 19);
            this.bItemQuestRules.Name = "bItemQuestRules";
            this.bItemQuestRules.Size = new System.Drawing.Size(102, 23);
            this.bItemQuestRules.TabIndex = 11;
            this.bItemQuestRules.Text = "Предметы";
            this.bItemQuestRules.UseVisualStyleBackColor = true;
            this.bItemQuestRules.Click += new System.EventHandler(this.bItemQuestRules_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 311);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(884, 61);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(797, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(694, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // LocaleQuestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(884, 482);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rewardGroupBox);
            this.Controls.Add(this.lQuestRules);
            this.Controls.Add(this.questInformationBox);
            this.Name = "LocaleQuestForm";
            this.Text = "Локализация события";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LocaleQuestForm_FormClosing);
            this.questInformationBox.ResumeLayout(false);
            this.questInformationBox.PerformLayout();
            this.rewardGroupBox.ResumeLayout(false);
            this.lQuestRules.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox questInformationBox;
        private System.Windows.Forms.Label lFailed;
        private System.Windows.Forms.Label lWin;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.MaskedTextBox onFailedTextBox;
        private System.Windows.Forms.MaskedTextBox onWonTextBox;
        private System.Windows.Forms.MaskedTextBox titleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox localeDescriptionTextBox;
        private System.Windows.Forms.MaskedTextBox localeOnFailedTextBox;
        private System.Windows.Forms.MaskedTextBox localeOnWonTextBox;
        private System.Windows.Forms.MaskedTextBox localeLitleTextBox;
        private System.Windows.Forms.GroupBox rewardGroupBox;
        private System.Windows.Forms.Button bItemReward;
        private System.Windows.Forms.GroupBox lQuestRules;
        private System.Windows.Forms.Button bItemQuestRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}