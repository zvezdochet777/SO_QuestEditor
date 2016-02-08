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
        public RewardFractions(EditQuestForm form)
        {
            this.form = form;
            InitializeComponent();

            foreach (KeyValuePair<int, string> pair in form.parent.fractions.getListOfFractions())
            {
                string id = pair.Key.ToString();
                string name = pair.Value;
                //bool rew = false;
                //if (form.editQuestReward.Fractions.Contains(pair.Key))
                    //rew = true;
                int rewardValue = 0;
                if (form.editQuestReward.Reputation.Keys.Contains(pair.Key))
                    rewardValue = form.editQuestReward.Reputation[pair.Key];
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
            form.editQuestReward.Reputation.Clear();
            //form.editQuestReward.Fractions.Clear();
            foreach (DataGridViewRow row in dataFractions.Rows)
            {
                int id = int.Parse(row.Cells[0].FormattedValue.ToString());

                //bool rew = false;
                //if (row.Cells[2].FormattedValue.ToString() != "")
                    //rew = bool.Parse(row.Cells[2].FormattedValue.ToString());
                //if (rew == true)
                    //form.editQuestReward.Fractions.Add(id); //.Reputation.Add(id, rew);
                string sValue = row.Cells[2].FormattedValue.ToString();
                int nValue;
                if (int.TryParse(sValue, out nValue))
                {
                    form.editQuestReward.Reputation[id] = nValue;
                }
            }

            form.checkRewardIndicates();
            this.Close();
        }

        
    }
}
