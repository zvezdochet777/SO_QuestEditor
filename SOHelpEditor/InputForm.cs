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
    public partial class InputForm : Form
    {
        public InputForm(string text = "")
        {
            InitializeComponent();
            inputBox.Text = text;
        }

        public string getText()
        {
            return inputBox.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
