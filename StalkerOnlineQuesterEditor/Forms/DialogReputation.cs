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
    public partial class DialogReputation : Form
    {
        EditDialogForm form;
        public DialogReputation(EditDialogForm form)
        {
            this.form = form;
            InitializeComponent();
            CFracConstants frac = form.parent.fractions;
            foreach (KeyValuePair<int, string> pair in frac.getListOfFractions())
            {
                int id = pair.Key;
                string name = pair.Value;
                string a = "";
                string b = "";
                if (form.editPrecondition.Reputation.Keys.Contains(pair.Key))
                {
                    double type = form.editPrecondition.Reputation[pair.Key][0];
                    if (type == 0 || (type == 1))
                    {
                        a = form.editPrecondition.Reputation[pair.Key][1].ToString();
                    }
                    if (type == 0 || (type == 2))
                    {
                        b = form.editPrecondition.Reputation[pair.Key][2].ToString();
                    }

                }
                object[] row = { id, name, a, b };
                dataReputation.Rows.Add(row);
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            form.editPrecondition.Reputation.Clear();
            foreach (DataGridViewRow row in dataReputation.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    int id = int.Parse(row.Cells[0].FormattedValue.ToString());

                    string a = row.Cells[2].FormattedValue.ToString().Replace('.',',');
                    string b = row.Cells[3].FormattedValue.ToString().Replace('.', ',');

                    double ia = 0;
                    double ib = 0;
                    int type = 0;
                    if ((a != "") || (b != ""))
                    {
                        if (a != "")
                        {
                            type = 1;
                            ia = double.Parse(a);
                        }
                        if (b != "")
                        {
                            if (type == 1)
                                type = 0;
                            else
                                type = 2;

                            ib = double.Parse(b);
                        }

                        List<double> lst = new List<double>();
                        lst.Add(type);
                        lst.Add(ia);
                        lst.Add(ib);

                        form.editPrecondition.Reputation.Add(id, lst);
                    }
                }
            }
            form.checkReputationIndicates();
            this.Close();            
        }

        private void DialogReputation_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Enabled = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
