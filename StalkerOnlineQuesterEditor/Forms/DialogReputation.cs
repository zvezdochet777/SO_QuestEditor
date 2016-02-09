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
                    if (form.editPrecondition.Reputation[id].Count == 3)         // костыль для старой версии, выжечт огнем позже
                    {
                        double type = form.editPrecondition.Reputation[pair.Key][0];
                        if (type == 0 || (type == 1))
                            a = form.editPrecondition.Reputation[pair.Key][1].ToString();
                        if (type == 0 || (type == 2))
                            b = form.editPrecondition.Reputation[pair.Key][2].ToString();
                    }
                    else if (form.editPrecondition.Reputation[id].Count == 2)
                    {
                        if (form.editPrecondition.Reputation[id][0] != double.NegativeInfinity)
                            a = form.editPrecondition.Reputation[id][0].ToString();
                        if (form.editPrecondition.Reputation[id][1] != double.PositiveInfinity)
                            b = form.editPrecondition.Reputation[id][1].ToString();
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
                    int fractionID = int.Parse(row.Cells[0].FormattedValue.ToString());
                    string stringA = row.Cells[2].FormattedValue.ToString().Replace('.',',');
                    string stringB = row.Cells[3].FormattedValue.ToString().Replace('.', ',');

                    if ((stringA != "") || (stringB != ""))
                    {
                        double doubleA;
                        double doubleB;
                        if (!double.TryParse(stringA, out doubleA))
                            doubleA = double.NegativeInfinity;
                        if (!double.TryParse(stringB, out doubleB))
                            doubleB = double.PositiveInfinity;

                        form.editPrecondition.Reputation.Add( fractionID, new List<double>() {doubleA, doubleB} );
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
