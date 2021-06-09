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
        CFracConstants fractions;
        public Dictionary<int, int> reputations;
        public Dictionary<string, int> npc_reputations;

        public RewardFractions(EditQuestForm form, CFracConstants fractions, ref Dictionary<int, int> reputations, ref Dictionary<string, int> npc_reputations)
        {
            this.form = form;
            this.fractions = fractions;
            this.reputations = reputations;
            this.npc_reputations = npc_reputations;
            InitializeComponent();

            foreach (KeyValuePair<int, string> pair in fractions.getListOfFractions())
                ((DataGridViewComboBoxColumn)dataFractions.Columns["Fractions"]).Items.Add(pair.Value);

            foreach (KeyValuePair<string, int> pair in this.npc_reputations)
            {
                ((DataGridViewComboBoxColumn)dataFractions.Columns["Fractions"]).Items.Add(pair.Key);
            }

            foreach (KeyValuePair<int, string> pair in fractions.getListOfFractions())
            {
                string id = pair.Key.ToString();
                string name = pair.Value;
                int rewardValue = 0;
                if (reputations.Keys.Contains(pair.Key))
                    rewardValue = reputations[pair.Key];
                if (rewardValue == 0) continue;
                object[] row = { id, name, rewardValue };
                dataFractions.Rows.Add(row);
            }
            
            foreach (KeyValuePair<string, int> pair in this.npc_reputations)
            {
                string name = pair.Key;
                int rewardValue = pair.Value;
                object[] row = { "", name, rewardValue };
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
            npc_reputations.Clear();
            foreach (DataGridViewRow row in dataFractions.Rows)
            {
                
                string sValue = row.Cells[2].FormattedValue.ToString();
                int nValue;
                int.TryParse(sValue, out nValue);
                if (nValue == 0)
                    continue;
                int id = -1;
                string name = row.Cells["Fractions"].FormattedValue.ToString();
                id = this.fractions.getFractionIDByDescr(name);
                if (id >=0 )
                {
                    reputations[id] = nValue;
                }
                else
                {
                    npc_reputations[name] = nValue;
                }
            }

            form.checkRewardIndicates();
            this.Close();
        }

        
    }
}
