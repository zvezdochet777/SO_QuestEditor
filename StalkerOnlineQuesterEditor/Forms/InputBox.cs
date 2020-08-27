using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class InputBox : Form
    {
        string resultText = "";

        public InputBox(string title, string text = "")
        {
            InitializeComponent();
            this.Text = title;
            this.textBox1.Text = text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Height = 93 + Math.Max(textBox1.Lines.Length, 2) * 20;
            
        }

        public string getResult()
        {
            return resultText;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            resultText = this.textBox1.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            resultText = "";
            this.Close();
        }
    }
}
