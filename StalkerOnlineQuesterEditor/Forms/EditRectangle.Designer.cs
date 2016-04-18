namespace StalkerOnlineQuesterEditor
{
    partial class EditRectangle
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
            this.lTip = new Telerik.WinControls.UI.RadLabel();
            this.bOK = new Telerik.WinControls.UI.RadButton();
            this.bCancel = new Telerik.WinControls.UI.RadButton();
            this.tbRectText = new Telerik.WinControls.UI.RadTextBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.lTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRectText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lTip
            // 
            this.lTip.Location = new System.Drawing.Point(22, 25);
            this.lTip.Name = "lTip";
            this.lTip.Size = new System.Drawing.Size(190, 18);
            this.lTip.TabIndex = 0;
            this.lTip.Text = "Введите текст для прямоугольника:";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(163, 140);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(110, 24);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(289, 140);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(110, 24);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbRectText
            // 
            this.tbRectText.Location = new System.Drawing.Point(22, 77);
            this.tbRectText.Name = "tbRectText";
            this.tbRectText.Size = new System.Drawing.Size(377, 20);
            this.tbRectText.TabIndex = 3;
            // 
            // EditRectangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 190);
            this.Controls.Add(this.tbRectText);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lTip);
            this.KeyPreview = true;
            this.Name = "EditRectangle";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Правка прямоугольника";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.EditRectangle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditRectangle_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.lTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRectText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lTip;
        private Telerik.WinControls.UI.RadButton bOK;
        private Telerik.WinControls.UI.RadButton bCancel;
        private Telerik.WinControls.UI.RadTextBoxControl tbRectText;
    }
}
