using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOHelpEditor
{
    public partial class LittleCardForm : Form
    {
        public LittleCardForm(string title, string text)
        {
            InitializeComponent();
            this.title = title;
            this.text = text;
            updateText();
        }

        public string text = "";
        protected string title = "";

        protected bool is_moving = false;
        protected int x, y;

        private void button1_Click(object sender, EventArgs e)
        {
            text = this.tbText.Text;
            this.Hide();
        }

        protected void updateText()
        {
            this.labelTitle.Text = title;
            this.tbText.Text = text;
            labelTitle.Location = new Point(this.Width / 2 - labelTitle.Width / 2, labelTitle.Location.Y);
        }

        private void backgorund_MouseDown(object sender, MouseEventArgs e)
        {
            is_moving = true;
            x = e.X;
            y = e.Y;
        }

        private void backgorund_MouseMove(object sender, MouseEventArgs e)
        {
            if (!is_moving) return;
            this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
        }

        private void backgorund_MouseUp(object sender, MouseEventArgs e)
        {
            is_moving = false;
        }
    }
}
