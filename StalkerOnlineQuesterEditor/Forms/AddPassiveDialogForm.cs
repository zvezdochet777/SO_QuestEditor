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
    public partial class AddPassiveDialogForm : Form
    {
        MainForm parent;
        int dialogID;
        public AddPassiveDialogForm(MainForm parent , int dialogID)
        {
            InitializeComponent();
            this.parent = parent;
            this.dialogID = dialogID;
            parent.Enabled = false;
            foreach (TreeNode active in parent.tree.Nodes.Find("Active", true))
                foreach (TreeNode node in active.Nodes)
                    attachToComboBox.Items.Add(node.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            parent.Enabled = true;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
                parent.Enabled = true;
                parent.addPassiveDialog(int.Parse(attachToComboBox.SelectedItem.ToString()), this.dialogID);
                this.Close();
        }
    }
}
