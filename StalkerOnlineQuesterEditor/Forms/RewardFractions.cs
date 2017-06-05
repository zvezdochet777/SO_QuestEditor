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
    public partial class RewardFractions : Form
    {
        EditQuestForm form;
        Dictionary<int, int> reputations;
        public RewardFractions(EditQuestForm form, ref Dictionary<int, int> reputations)
        {
            this.form = form;
            this.reputations = reputations;
            InitializeComponent();

            foreach (KeyValuePair<int, string> pair in form.parent.fractions.getListOfFractions())
            {
                string id = pair.Key.ToString();
                string name = pair.Value;
                int rewardValue = 0;
                if (reputations.Keys.Contains(pair.Key))
                    rewardValue = reputations[pair.Key];
                object[] row = { id, name, rewardValue };
                dataFractions.Rows.Add(row);
            }
        }

        private void RewardFractions_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Enabled = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            reputations.Clear();
            foreach (DataGridViewRow row in dataFractions.Rows)
            {
                int id = int.Parse(row.Cells[0].FormattedValue.ToString());
                string sValue = row.Cells[2].FormattedValue.ToString();
                int nValue;
                if (int.TryParse(sValue, out nValue))
                    reputations[id] = nValue;
            }

            form.checkRewardIndicates();
            this.Close();
        }

        
    }
}
