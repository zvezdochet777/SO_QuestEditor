namespace StalkerOnlineQuesterEditor.Forms
{
    partial class LocaleDialogForm
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
            this.labelNameOfNPC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lRussian2 = new System.Windows.Forms.Label();
            this.lRussian1 = new System.Windows.Forms.Label();
            this.lLocalization2 = new System.Windows.Forms.Label();
            this.lAnswer = new System.Windows.Forms.Label();
            this.lLocalization1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.textBoxAnswerLocale = new System.Windows.Forms.TextBox();
            this.lNpcReaction = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.NPCReactiontextBox = new System.Windows.Forms.TextBox();
            this.textBoxNPCReactionLocale = new System.Windows.Forms.TextBox();
            this.lViewNpcName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.labelDialogID = new System.Windows.Forms.Label();
            this.lViewDialogId = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNameOfNPC
            // 
            this.labelNameOfNPC.AutoSize = true;
            this.labelNameOfNPC.Location = new System.Drawing.Point(13, 10);
            this.labelNameOfNPC.Name = "labelNameOfNPC";
            this.labelNameOfNPC.Size = new System.Drawing.Size(57, 13);
            this.labelNameOfNPC.TabIndex = 0;
            this.labelNameOfNPC.Text = "Имя NPC:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lViewDialogId);
            this.panel1.Controls.Add(this.labelDialogID);
            this.panel1.Controls.Add(this.lRussian2);
            this.panel1.Controls.Add(this.lRussian1);
            this.panel1.Controls.Add(this.lLocalization2);
            this.panel1.Controls.Add(this.lAnswer);
            this.panel1.Controls.Add(this.lLocalization1);
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Controls.Add(this.lNpcReaction);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.lViewNpcName);
            this.panel1.Controls.Add(this.labelNameOfNPC);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 345);
            this.panel1.TabIndex = 1;
            // 
            // lRussian2
            // 
            this.lRussian2.AutoSize = true;
            this.lRussian2.Location = new System.Drawing.Point(76, 212);
            this.lRussian2.Name = "lRussian2";
            this.lRussian2.Size = new System.Drawing.Size(49, 13);
            this.lRussian2.TabIndex = 5;
            this.lRussian2.Text = "Русский";
            // 
            // lRussian1
            // 
            this.lRussian1.AutoSize = true;
            this.lRussian1.Location = new System.Drawing.Point(76, 62);
            this.lRussian1.Name = "lRussian1";
            this.lRussian1.Size = new System.Drawing.Size(49, 13);
            this.lRussian1.TabIndex = 5;
            this.lRussian1.Text = "Русский";
            // 
            // lLocalization2
            // 
            this.lLocalization2.AutoSize = true;
            this.lLocalization2.Location = new System.Drawing.Point(362, 212);
            this.lLocalization2.Name = "lLocalization2";
            this.lLocalization2.Size = new System.Drawing.Size(75, 13);
            this.lLocalization2.TabIndex = 3;
            this.lLocalization2.Text = "Локализация";
            // 
            // lAnswer
            // 
            this.lAnswer.AutoSize = true;
            this.lAnswer.Location = new System.Drawing.Point(202, 194);
            this.lAnswer.Name = "lAnswer";
            this.lAnswer.Size = new System.Drawing.Size(86, 13);
            this.lAnswer.TabIndex = 4;
            this.lAnswer.Text = "Вариант ответа";
            // 
            // lLocalization1
            // 
            this.lLocalization1.AutoSize = true;
            this.lLocalization1.Location = new System.Drawing.Point(362, 62);
            this.lLocalization1.Name = "lLocalization1";
            this.lLocalization1.Size = new System.Drawing.Size(75, 13);
            this.lLocalization1.TabIndex = 3;
            this.lLocalization1.Text = "Локализация";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(1, 228);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBoxAnswer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxAnswerLocale);
            this.splitContainer2.Size = new System.Drawing.Size(594, 100);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 2;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAnswer.Location = new System.Drawing.Point(0, 0);
            this.textBoxAnswer.Multiline = true;
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(198, 100);
            this.textBoxAnswer.TabIndex = 0;
            // 
            // textBoxAnswerLocale
            // 
            this.textBoxAnswerLocale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAnswerLocale.Location = new System.Drawing.Point(0, 0);
            this.textBoxAnswerLocale.Multiline = true;
            this.textBoxAnswerLocale.Name = "textBoxAnswerLocale";
            this.textBoxAnswerLocale.Size = new System.Drawing.Size(392, 100);
            this.textBoxAnswerLocale.TabIndex = 0;
            // 
            // lNpcReaction
            // 
            this.lNpcReaction.AutoSize = true;
            this.lNpcReaction.Location = new System.Drawing.Point(202, 38);
            this.lNpcReaction.Name = "lNpcReaction";
            this.lNpcReaction.Size = new System.Drawing.Size(69, 13);
            this.lNpcReaction.TabIndex = 3;
            this.lNpcReaction.Text = "Реация NPC";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(1, 78);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.NPCReactiontextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxNPCReactionLocale);
            this.splitContainer1.Size = new System.Drawing.Size(594, 100);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 2;
            // 
            // NPCReactiontextBox
            // 
            this.NPCReactiontextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NPCReactiontextBox.Location = new System.Drawing.Point(0, 0);
            this.NPCReactiontextBox.Multiline = true;
            this.NPCReactiontextBox.Name = "NPCReactiontextBox";
            this.NPCReactiontextBox.Size = new System.Drawing.Size(198, 100);
            this.NPCReactiontextBox.TabIndex = 0;
            // 
            // textBoxNPCReactionLocale
            // 
            this.textBoxNPCReactionLocale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNPCReactionLocale.Location = new System.Drawing.Point(0, 0);
            this.textBoxNPCReactionLocale.Multiline = true;
            this.textBoxNPCReactionLocale.Name = "textBoxNPCReactionLocale";
            this.textBoxNPCReactionLocale.Size = new System.Drawing.Size(392, 100);
            this.textBoxNPCReactionLocale.TabIndex = 0;
            // 
            // lViewNpcName
            // 
            this.lViewNpcName.AutoSize = true;
            this.lViewNpcName.Location = new System.Drawing.Point(76, 10);
            this.lViewNpcName.Name = "lViewNpcName";
            this.lViewNpcName.Size = new System.Drawing.Size(0, 13);
            this.lViewNpcName.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bCancel);
            this.panel2.Controls.Add(this.bOk);
            this.panel2.Location = new System.Drawing.Point(12, 363);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 34);
            this.panel2.TabIndex = 2;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(542, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(461, 3);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "Ок";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // labelDialogID
            // 
            this.labelDialogID.AutoSize = true;
            this.labelDialogID.Location = new System.Drawing.Point(461, 10);
            this.labelDialogID.Name = "labelDialogID";
            this.labelDialogID.Size = new System.Drawing.Size(51, 13);
            this.labelDialogID.TabIndex = 6;
            this.labelDialogID.Text = "DialogID:";
            // 
            // lViewDialogId
            // 
            this.lViewDialogId.AutoSize = true;
            this.lViewDialogId.Location = new System.Drawing.Point(518, 10);
            this.lViewDialogId.Name = "lViewDialogId";
            this.lViewDialogId.Size = new System.Drawing.Size(0, 13);
            this.lViewDialogId.TabIndex = 7;
            // 
            // LocaleDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 427);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "LocaleDialogForm";
            this.Text = "Локализация диалога";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LocaleDialogForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNameOfNPC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lViewNpcName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lNpcReaction;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lLocalization1;
        private System.Windows.Forms.Label lAnswer;
        private System.Windows.Forms.Label lRussian1;
        private System.Windows.Forms.Label lRussian2;
        private System.Windows.Forms.Label lLocalization2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox NPCReactiontextBox;
        private System.Windows.Forms.TextBox textBoxNPCReactionLocale;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.TextBox textBoxAnswerLocale;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lViewDialogId;
        private System.Windows.Forms.Label labelDialogID;
    }
}