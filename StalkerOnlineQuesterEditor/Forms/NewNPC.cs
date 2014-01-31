using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class NewNPC : Form
    {
        MainForm parent;
        public NewNPC(MainForm parent)
        {
            InitializeComponent();
            parent.Enabled = false;
            this.parent = parent;


        }

        private void bOK_Click(object sender, EventArgs e)
        {
            try
            {
                parent.addNewNPC(tNPCName.Text);
                parent.Enabled = true;
                this.Close();
            }
            catch
            {
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            parent.Enabled = true;
            this.Close();
        }
    }
}
