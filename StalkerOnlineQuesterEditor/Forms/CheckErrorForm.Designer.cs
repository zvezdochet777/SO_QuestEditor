namespace StalkerOnlineQuesterEditor.Forms
{
    partial class CheckErrorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckErrorForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lCurrentCheck = new System.Windows.Forms.Label();
            this.bpNoErrors = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bpNoErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 29);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.ContextMenuStrip = this.contextMenuStrip1;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(3, 42);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(623, 446);
            this.lbLog.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 26);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(94, 26);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(531, 10);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // lCurrentCheck
            // 
            this.lCurrentCheck.AutoSize = true;
            this.lCurrentCheck.Location = new System.Drawing.Point(94, 7);
            this.lCurrentCheck.Name = "lCurrentCheck";
            this.lCurrentCheck.Size = new System.Drawing.Size(219, 13);
            this.lCurrentCheck.TabIndex = 3;
            this.lCurrentCheck.Text = "Перед проверкой нужно расспарсить всё";
            // 
            // bpNoErrors
            // 
            this.bpNoErrors.Image = ((System.Drawing.Image)(resources.GetObject("bpNoErrors.Image")));
            this.bpNoErrors.Location = new System.Drawing.Point(174, 54);
            this.bpNoErrors.Name = "bpNoErrors";
            this.bpNoErrors.Size = new System.Drawing.Size(452, 434);
            this.bpNoErrors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bpNoErrors.TabIndex = 5;
            this.bpNoErrors.TabStop = false;
            this.bpNoErrors.Visible = false;
            // 
            // CheckErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 490);
            this.Controls.Add(this.bpNoErrors);
            this.Controls.Add(this.lCurrentCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btnStart);
            this.Name = "CheckErrorForm";
            this.Text = "CheckErrorForm";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bpNoErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lCurrentCheck;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.PictureBox bpNoErrors;
    }
}