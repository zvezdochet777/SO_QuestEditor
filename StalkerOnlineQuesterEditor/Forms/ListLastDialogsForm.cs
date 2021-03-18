using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class ListLastDialogsForm : Form
    {
        MainForm parent;

        public ListLastDialogsForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += lst_MeasureItem;
            listBox1.DrawItem += lst_DrawItem;
        }

        public void setListData(List<string> list)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(list.ToArray());
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            string text = listBox1.SelectedItem.ToString().Split()[0];
 
            EditDialogForm editDialogForm = new EditDialogForm(false, parent, int.Parse(text));
            editDialogForm.Visible = true;
        }

        

        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listBox1.Items[e.Index].ToString(), listBox1.Font, listBox1.Width).Height;
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
    }
}
