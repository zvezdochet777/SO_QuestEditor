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
    public partial class CreateLinkForm : Form
    {
        protected string resultLink = "";

        public CreateLinkForm(TreeNodeCollection nodes, string text = "")
        {
            InitializeComponent();
            btnOK.DialogResult = DialogResult.OK; 
            setData(nodes, text);
        }

        protected void setData(TreeNodeCollection nodes, string text)
        {
            foreach(TreeNode node in nodes)
            {
                CustomListBoxItem item = new CustomListBoxItem(Convert.ToInt32(node.Tag), node.Text);
                listBox1.Items.Add(item);
                foreach (TreeNode child in node.Nodes)
                {
                    CustomListBoxItem child_item = new CustomListBoxItem(Convert.ToInt32(child.Tag), "\t"+child.Text);
                    listBox1.Items.Add(child_item);
                }
            }
            textBox1.Text = text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            int node_id = (listBox1.SelectedItem as CustomListBoxItem).id;
            resultLink = "<font color='#0082ff'><a href='event:" + node_id + "'>" + textBox1.Text + "</a></font>";
        }

        public string getText()
        {
            return resultLink;
        }
    }

    public class CustomListBoxItem
    {
        public int id;
        public string text;

        public CustomListBoxItem(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public override string ToString()
        {
            return text;
        }

    }
}
