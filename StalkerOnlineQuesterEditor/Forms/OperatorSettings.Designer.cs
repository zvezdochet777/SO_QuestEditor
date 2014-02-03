namespace StalkerOnlineQuesterEditor
{
    partial class OperatorSettings
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
            this.operatorSelectComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.localesTextBox = new System.Windows.Forms.TextBox();
            this.localizeCheckBox = new System.Windows.Forms.CheckBox();
            this.localeComboBox = new System.Windows.Forms.ComboBox();
            this.bLocaleRefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bCreateResult = new System.Windows.Forms.Button();
            this.bCreateExamples = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // operatorSelectComboBox
            // 
            this.operatorSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorSelectComboBox.FormattingEnabled = true;
            this.operatorSelectComboBox.Location = new System.Drawing.Point(99, 6);
            this.operatorSelectComboBox.Name = "operatorSelectComboBox";
            this.operatorSelectComboBox.Size = new System.Drawing.Size(127, 21);
            this.operatorSelectComboBox.TabIndex = 0;
            this.operatorSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.operatorSelectComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Оператор";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(58, 212);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(141, 212);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Локали";
            // 
            // localesTextBox
            // 
            this.localesTextBox.Location = new System.Drawing.Point(54, 18);
            this.localesTextBox.Name = "localesTextBox";
            this.localesTextBox.Size = new System.Drawing.Size(82, 20);
            this.localesTextBox.TabIndex = 6;
            // 
            // localizeCheckBox
            // 
            this.localizeCheckBox.AutoSize = true;
            this.localizeCheckBox.Location = new System.Drawing.Point(6, 44);
            this.localizeCheckBox.Name = "localizeCheckBox";
            this.localizeCheckBox.Size = new System.Drawing.Size(130, 17);
            this.localizeCheckBox.TabIndex = 7;
            this.localizeCheckBox.Text = "Режим локализации";
            this.localizeCheckBox.UseVisualStyleBackColor = true;
            this.localizeCheckBox.CheckedChanged += new System.EventHandler(this.localizeCheckBox_CheckedChanged);
            // 
            // localeComboBox
            // 
            this.localeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.localeComboBox.Enabled = false;
            this.localeComboBox.FormattingEnabled = true;
            this.localeComboBox.Location = new System.Drawing.Point(6, 67);
            this.localeComboBox.Name = "localeComboBox";
            this.localeComboBox.Size = new System.Drawing.Size(130, 21);
            this.localeComboBox.TabIndex = 8;
            // 
            // bLocaleRefresh
            // 
            this.bLocaleRefresh.Location = new System.Drawing.Point(142, 16);
            this.bLocaleRefresh.Name = "bLocaleRefresh";
            this.bLocaleRefresh.Size = new System.Drawing.Size(72, 23);
            this.bLocaleRefresh.TabIndex = 9;
            this.bLocaleRefresh.Text = "Обновить";
            this.bLocaleRefresh.UseVisualStyleBackColor = true;
            this.bLocaleRefresh.Click += new System.EventHandler(this.bLocaleRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bCreateResult);
            this.groupBox1.Controls.Add(this.bCreateExamples);
            this.groupBox1.Controls.Add(this.localizeCheckBox);
            this.groupBox1.Controls.Add(this.bLocaleRefresh);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.localeComboBox);
            this.groupBox1.Controls.Add(this.localesTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 173);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Локализация";
            // 
            // bCreateResult
            // 
            this.bCreateResult.Location = new System.Drawing.Point(6, 123);
            this.bCreateResult.Name = "bCreateResult";
            this.bCreateResult.Size = new System.Drawing.Size(130, 23);
            this.bCreateResult.TabIndex = 11;
            this.bCreateResult.Text = "Выгрузить результат";
            this.bCreateResult.UseVisualStyleBackColor = true;
            this.bCreateResult.Click += new System.EventHandler(this.bCreateResult_Click);
            // 
            // bCreateExamples
            // 
            this.bCreateExamples.Location = new System.Drawing.Point(6, 94);
            this.bCreateExamples.Name = "bCreateExamples";
            this.bCreateExamples.Size = new System.Drawing.Size(130, 23);
            this.bCreateExamples.TabIndex = 10;
            this.bCreateExamples.Text = "Создать заготовки";
            this.bCreateExamples.UseVisualStyleBackColor = true;
            this.bCreateExamples.Click += new System.EventHandler(this.bCreateExamples_Click);
            // 
            // OperatorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 251);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.operatorSelectComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OperatorSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Настройки оператора";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperatorSettings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox operatorSelectComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox localesTextBox;
        private System.Windows.Forms.CheckBox localizeCheckBox;
        private System.Windows.Forms.ComboBox localeComboBox;
        private System.Windows.Forms.Button bLocaleRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bCreateExamples;
        private System.Windows.Forms.Button bCreateResult;
    }
}