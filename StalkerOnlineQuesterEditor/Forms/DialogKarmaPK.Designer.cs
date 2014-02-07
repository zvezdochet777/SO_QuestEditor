namespace StalkerOnlineQuesterEditor
{
    partial class DialogKarmaPK
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.aTextBox = new System.Windows.Forms.MaskedTextBox();
            this.a = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.aTextBox);
            this.panel1.Controls.Add(this.a);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 55);
            this.panel1.TabIndex = 0;
            // 
            // bTextBox
            // 
            this.bTextBox.Location = new System.Drawing.Point(139, 24);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(100, 20);
            this.bTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "B";
            // 
            // aTextBox
            // 
            this.aTextBox.Location = new System.Drawing.Point(33, 24);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(100, 20);
            this.aTextBox.TabIndex = 1;
            // 
            // a
            // 
            this.a.AutoSize = true;
            this.a.Location = new System.Drawing.Point(79, 8);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(14, 13);
            this.a.TabIndex = 0;
            this.a.Text = "A";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bCancel);
            this.panel2.Controls.Add(this.bOK);
            this.panel2.Controls.Add(this.labelDescription);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 141);
            this.panel2.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(237, 114);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(158, 114);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(3, 10);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(90, 13);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "Описание здесь";
            // 
            // DialogKarmaPK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(324, 196);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "DialogKarmaPK";
            this.Text = "Фильтр кармы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogKarmaPK_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MaskedTextBox bTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox aTextBox;
        private System.Windows.Forms.Label a;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label labelDescription;
    }
}