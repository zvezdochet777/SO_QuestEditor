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
        private RectangleManager RectManager;
        public EditRectangle(RectangleManager rectManager)
        {
            InitializeComponent();
            RectManager = rectManager;
        }

        private void EditRectangle_Load(object sender, EventArgs e)
        {
            tbRectText.Text = RectManager.GetTextOfSelectedRect();
            colorBox.Value = RectManager.GetColorOfSelectedRect();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            string text = tbRectText.Text;
            RectManager.ChangeText(text);
            Color color = colorBox.Value;
            RectManager.ChangeColor(color);
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
