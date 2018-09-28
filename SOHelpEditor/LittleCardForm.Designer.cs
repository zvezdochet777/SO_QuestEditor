namespace SOHelpEditor
{
    partial class LittleCardForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closeBackground = new System.Windows.Forms.PictureBox();
            this.backgorund = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgorund)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Silver;
            this.labelTitle.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTitle.Location = new System.Drawing.Point(180, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(175, 39);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Заголовок";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(476, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbText
            // 
            this.tbText.BackColor = System.Drawing.Color.Silver;
            this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbText.Font = new System.Drawing.Font("Roboto", 15F);
            this.tbText.ForeColor = System.Drawing.SystemColors.Window;
            this.tbText.Location = new System.Drawing.Point(30, 56);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(494, 90);
            this.tbText.TabIndex = 8;
            this.tbText.Text = "Бла-бла-бла, какой-то длинный текст";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Silver;
            this.pictureBox3.Image = global::SOHelpEditor.Properties.Resources.LittleLine;
            this.pictureBox3.Location = new System.Drawing.Point(-1, 38);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(555, 2);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Silver;
            this.pictureBox2.Image = global::SOHelpEditor.Properties.Resources.ArrowButtonRight;
            this.pictureBox2.Location = new System.Drawing.Point(530, 84);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 39);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Image = global::SOHelpEditor.Properties.Resources.ArrowButtonLeft;
            this.pictureBox1.Location = new System.Drawing.Point(12, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // closeBackground
            // 
            this.closeBackground.Image = global::SOHelpEditor.Properties.Resources.LittleCloseButton;
            this.closeBackground.Location = new System.Drawing.Point(236, 211);
            this.closeBackground.Name = "closeBackground";
            this.closeBackground.Size = new System.Drawing.Size(65, 11);
            this.closeBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeBackground.TabIndex = 2;
            this.closeBackground.TabStop = false;
            // 
            // backgorund
            // 
            this.backgorund.BackColor = System.Drawing.Color.Transparent;
            this.backgorund.Image = global::SOHelpEditor.Properties.Resources.LittleBackGround;
            this.backgorund.Location = new System.Drawing.Point(1, 1);
            this.backgorund.Name = "backgorund";
            this.backgorund.Size = new System.Drawing.Size(553, 210);
            this.backgorund.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgorund.TabIndex = 1;
            this.backgorund.TabStop = false;
            this.backgorund.MouseDown += new System.Windows.Forms.MouseEventHandler(this.backgorund_MouseDown);
            this.backgorund.MouseMove += new System.Windows.Forms.MouseEventHandler(this.backgorund_MouseMove);
            this.backgorund.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backgorund_MouseUp);
            // 
            // LittleCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(554, 244);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.closeBackground);
            this.Controls.Add(this.backgorund);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LittleCardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LittleCardForm";
            this.TransparencyKey = System.Drawing.SystemColors.Menu;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgorund)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox backgorund;
        private System.Windows.Forms.PictureBox closeBackground;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbText;
    }
}