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
            this.lWidthTip = new Telerik.WinControls.UI.RadLabel();
            this.lHeightTip = new Telerik.WinControls.UI.RadLabel();
            this.spinWidth = new Telerik.WinControls.UI.RadSpinEditor();
            this.spinHeight = new Telerik.WinControls.UI.RadSpinEditor();
            ((System.ComponentModel.ISupportInitialize)(this.lTextTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRectText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lColorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lWidthTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lHeightTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lTextTip
            // 
            this.lTextTip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lTextTip.Location = new System.Drawing.Point(22, 25);
            this.lTextTip.Name = "lTextTip";
            // 
            // 
            // 
            this.lTextTip.RootElement.AccessibleDescription = null;
            this.lTextTip.RootElement.AccessibleName = null;
            this.lTextTip.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.lTextTip.Size = new System.Drawing.Size(190, 18);
            this.lTextTip.TabIndex = 0;
            this.lTextTip.Text = "Введите текст для прямоугольника:";
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bOK.Location = new System.Drawing.Point(137, 202);
            this.bOK.Name = "bOK";
            // 
            // 
            // 
            this.bOK.RootElement.AccessibleDescription = null;
            this.bOK.RootElement.AccessibleName = null;
            this.bOK.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.bOK.Size = new System.Drawing.Size(128, 24);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bCancel.Location = new System.Drawing.Point(271, 202);
            this.bCancel.Name = "bCancel";
            // 
            // 
            // 
            this.bCancel.RootElement.AccessibleDescription = null;
            this.bCancel.RootElement.AccessibleName = null;
            this.bCancel.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.bCancel.Size = new System.Drawing.Size(128, 24);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbRectText
            // 
            this.tbRectText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbRectText.Location = new System.Drawing.Point(20, 50);
            this.tbRectText.Multiline = true;
            this.tbRectText.Name = "tbRectText";
            // 
            // 
            // 
            this.tbRectText.RootElement.AccessibleDescription = null;
            this.tbRectText.RootElement.AccessibleName = null;
            this.tbRectText.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.tbRectText.Size = new System.Drawing.Size(377, 40);
            this.tbRectText.TabIndex = 3;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.colorBox.Location = new System.Drawing.Point(111, 102);
            this.colorBox.Name = "colorBox";
            // 
            // 
            // 
            this.colorBox.RootElement.AccessibleDescription = null;
            this.colorBox.RootElement.AccessibleName = null;
            this.colorBox.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.colorBox.RootElement.StretchVertically = true;
            this.colorBox.Size = new System.Drawing.Size(148, 20);
            this.colorBox.TabIndex = 4;
            this.colorBox.Text = "WTF";
            this.colorBox.Value = System.Drawing.Color.Black;
            this.colorBox.ValueChanging += new Telerik.WinControls.UI.ValueChangingEventHandler(this.colorBox_ValueChanging);
            // 
            // lColorTip
            // 
            this.lColorTip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lColorTip.Location = new System.Drawing.Point(22, 102);
            this.lColorTip.Name = "lColorTip";
            // 
            // 
            // 
            this.lColorTip.RootElement.AccessibleDescription = null;
            this.lColorTip.RootElement.AccessibleName = null;
            this.lColorTip.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.lColorTip.Size = new System.Drawing.Size(76, 18);
            this.lColorTip.TabIndex = 5;
            this.lColorTip.Text = "Задайте цвет:";
            // 
            // lWidthTip
            // 
            this.lWidthTip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lWidthTip.Location = new System.Drawing.Point(22, 135);
            this.lWidthTip.Name = "lWidthTip";
            // 
            // 
            // 
            this.lWidthTip.RootElement.AccessibleDescription = null;
            this.lWidthTip.RootElement.AccessibleName = null;
            this.lWidthTip.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.lWidthTip.Size = new System.Drawing.Size(52, 18);
            this.lWidthTip.TabIndex = 6;
            this.lWidthTip.Text = "Ширина:";
            // 
            // lHeightTip
            // 
            this.lHeightTip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lHeightTip.Location = new System.Drawing.Point(22, 161);
            this.lHeightTip.Name = "lHeightTip";
            // 
            // 
            // 
            this.lHeightTip.RootElement.AccessibleDescription = null;
            this.lHeightTip.RootElement.AccessibleName = null;
            this.lHeightTip.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.lHeightTip.Size = new System.Drawing.Size(45, 18);
            this.lHeightTip.TabIndex = 7;
            this.lHeightTip.Text = "Высота:";
            // 
            // spinWidth
            // 
            this.spinWidth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.spinWidth.Location = new System.Drawing.Point(111, 133);
            this.spinWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinWidth.Name = "spinWidth";
            // 
            // 
            // 
            this.spinWidth.RootElement.AccessibleDescription = null;
            this.spinWidth.RootElement.AccessibleName = null;
            this.spinWidth.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.spinWidth.RootElement.StretchVertically = true;
            this.spinWidth.TabIndex = 8;
            this.spinWidth.TabStop = false;
            this.spinWidth.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // spinHeight
            // 
            this.spinHeight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.spinHeight.Location = new System.Drawing.Point(111, 159);
            this.spinHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinHeight.Name = "spinHeight";
            // 
            // 
            // 
            this.spinHeight.RootElement.AccessibleDescription = null;
            this.spinHeight.RootElement.AccessibleName = null;
            this.spinHeight.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.spinHeight.RootElement.StretchVertically = true;
            this.spinHeight.TabIndex = 9;
            this.spinHeight.TabStop = false;
            // 
            // EditRectangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 239);
            this.Controls.Add(this.spinHeight);
            this.Controls.Add(this.spinWidth);
            this.Controls.Add(this.lHeightTip);
            this.Controls.Add(this.lWidthTip);
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
            ((System.ComponentModel.ISupportInitialize)(this.lWidthTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lHeightTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).EndInit();
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
        private Telerik.WinControls.UI.RadLabel lWidthTip;
        private Telerik.WinControls.UI.RadLabel lHeightTip;
        private Telerik.WinControls.UI.RadSpinEditor spinWidth;
        private Telerik.WinControls.UI.RadSpinEditor spinHeight;
    }
}
