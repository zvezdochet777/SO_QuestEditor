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
            this.lTextTip = new Telerik.WinControls.UI.RadLabel();
            this.bOK = new Telerik.WinControls.UI.RadButton();
            this.bCancel = new Telerik.WinControls.UI.RadButton();
            this.tbRectText = new Telerik.WinControls.UI.RadTextBoxControl();
            this.colorBox = new Telerik.WinControls.UI.RadColorBox();
            this.lColorTip = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lTextTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRectText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lColorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lTextTip
            // 
            this.lTextTip.Location = new System.Drawing.Point(22, 25);
            this.lTextTip.Name = "lTextTip";
            this.lTextTip.Size = new System.Drawing.Size(190, 18);
            this.lTextTip.TabIndex = 0;
            this.lTextTip.Text = "Введите текст для прямоугольника:";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(137, 169);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(128, 24);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(271, 169);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(128, 24);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbRectText
            // 
            this.tbRectText.Location = new System.Drawing.Point(22, 61);
            this.tbRectText.Name = "tbRectText";
            this.tbRectText.Size = new System.Drawing.Size(377, 20);
            this.tbRectText.TabIndex = 3;
            // 
            // colorBox
            // 
            this.colorBox.Location = new System.Drawing.Point(137, 102);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(148, 20);
            this.colorBox.TabIndex = 4;
            this.colorBox.Text = "WTF";
            this.colorBox.Value = System.Drawing.Color.Black;
            this.colorBox.ValueChanging += new Telerik.WinControls.UI.ValueChangingEventHandler(this.colorBox_ValueChanging);
            // 
            // lColorTip
            // 
            this.lColorTip.Location = new System.Drawing.Point(22, 102);
            this.lColorTip.Name = "lColorTip";
            this.lColorTip.Size = new System.Drawing.Size(76, 18);
            this.lColorTip.TabIndex = 5;
            this.lColorTip.Text = "Задайте цвет:";
            // 
            // EditRectangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 216);
            this.Controls.Add(this.lColorTip);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.tbRectText);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lTextTip);
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
            ((System.ComponentModel.ISupportInitialize)(this.lTextTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRectText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lColorTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lTextTip;
        private Telerik.WinControls.UI.RadButton bOK;
        private Telerik.WinControls.UI.RadButton bCancel;
        private Telerik.WinControls.UI.RadTextBoxControl tbRectText;
        private Telerik.WinControls.UI.RadColorBox colorBox;
        private Telerik.WinControls.UI.RadLabel lColorTip;
    }
}
