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
    public partial class CreateCommentForm : Form
    {
        protected string _text = "";

        public CreateCommentForm()
        {
            InitializeComponent();
            btnOK.DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _text = tbText.Text;
        }

        public string getText()
        {
            return _text;
        }
    }
}
