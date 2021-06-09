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
        Dictionary<int, List<double>> reputation;
        public DialogReputation(EditDialogForm form, CFracConstants fractions, ref Dictionary<int, List<double>> reputation)
        {
            this.form = form;
            this.reputation = reputation;
            InitializeComponent();
            //CFracConstants frac = form.parent.fractions;
            foreach (KeyValuePair<int, string> pair in fractions.getListOfFractions())
            {
                int id = pair.Key;
                string name = pair.Value;
                string a = "";
                string b = "";
                if (reputation.Keys.Contains(pair.Key))
                {
                    if (reputation[id].Count == 3)         // костыль для старой версии, выжечт огнем позже
                    {
                        double type = reputation[pair.Key][0];
                        if (type == 0 || (type == 1))
                            a = reputation[pair.Key][1].ToString();
                        if (type == 0 || (type == 2))
                            b = reputation[pair.Key][2].ToString();
                    }
                    else if (reputation[id].Count == 2)
                    {
                        if (reputation[id][0] != double.NegativeInfinity)
                            a = reputation[id][0].ToString();
                        if (reputation[id][1] != double.PositiveInfinity)
                            b = reputation[id][1].ToString();
                    }
                }
                object[] row = { id, name, a, b };
                dataReputation.Rows.Add(row);
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.reputation.Clear();
            foreach (DataGridViewRow row in dataReputation.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    int fractionID = int.Parse(row.Cells[0].FormattedValue.ToString());
                    //string fractionName = row.Cells[1].FormattedValue.ToString();
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
                        if (doubleA >= doubleB)
                        {
                            MessageBox.Show("Неправильное условие по репутации! Значение А должно быть меньше B" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        this.reputation.Add( fractionID, new List<double>() {doubleA, doubleB} );
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
