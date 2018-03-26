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
    public partial class FilterNPCForm : Form
    {
        Dictionary<string, Boolean> filters;
        List<CheckBox> checkBoxes = new List<CheckBox>();

        public FilterNPCForm(ref Dictionary<string, bool> filters)
        {
            this.filters = filters;
            InitializeComponent();
        }

        private void updateFilters()
        {
            int x = 10, y = 40;

            foreach (CheckBox cb in checkBoxes)
            {
                this.Container.Remove(cb);
            }
            checkBoxes.Clear();

            int count = 1;
            foreach( KeyValuePair<string, bool> val in filters)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 150;
                cb.Location = new Point(x, y);
                
                cb.Text = val.Key;
                cb.Checked = val.Value;
                this.Controls.Add(cb);
                this.checkBoxes.Add(cb);
                y += 30;
                if (count >= 18)
                {

                    this.Height = y + 80;
                    y = 40;
                    x += 150;
                    count = 0;
                }
                count++;
                

            }
            this.Width = x + 150;

        }

        private void FilterNPCForm_Load(object sender, EventArgs e)
        {
            updateFilters();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            filters.Clear();
            foreach (CheckBox cb in checkBoxes)
            {
                filters.Add(cb.Text, cb.Checked);
            }
            this.Close();
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cb in checkBoxes)
            {
                cb.Checked = cbAll.Checked;
            }
        }
    }
}
