using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace StalkerOnlineQuesterEditor
{
    //! Класс редактирования прямоугольников. Пока позволяет редактировать только отображаемый текст.
    public partial class EditRectangle : Telerik.WinControls.UI.RadForm
    {
        private CRectangle CurrentRectangle;
        public EditRectangle(CRectangle rectangle)
        {
            InitializeComponent();
            CurrentRectangle = rectangle;
        }

        private void EditRectangle_Load(object sender, EventArgs e)
        {
            tbRectText.Text = CurrentRectangle.GetText();
            colorBox.Value = CurrentRectangle.RectColor;
            spinWidth.Value = CurrentRectangle.Width;
            spinHeight.Value = CurrentRectangle.Height;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            CurrentRectangle.SetText(tbRectText.Text);
            CurrentRectangle.RectColor = colorBox.Value;
            CurrentRectangle.Width = (int) spinWidth.Value;
            CurrentRectangle.Height = (int) spinHeight.Value;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditRectangle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bOK_Click(sender, e);
        }

        private void colorBox_ValueChanging(object sender, Telerik.WinControls.UI.ValueChangingEventArgs e)
        {
            if (!((Color)e.NewValue).IsNamedColor)
            {
                e.Cancel = true;
                MessageBox.Show("Выберите цвет из доступных списков", "Совет", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
