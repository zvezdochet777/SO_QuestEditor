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
    public partial class RewardQuestsDialog : Form
    {

        protected EditQuestForm form;
        protected Dictionary<int, int> quests;
        protected Dictionary<string, int> statuses = new Dictionary<string, int>();
        protected CQuestReward reward { get; set; }

        public RewardQuestsDialog(EditQuestForm form, ref CQuestReward reward)
        {
            InitializeComponent();
            this.form = form;
            this.quests = reward.ChangeQuests;
            this.reward = reward;

            cbRandom.Checked = reward.randomQuest;
            statuses.Clear();
            statuses.Add("Открыть (Open)", 0);
            statuses.Add("Закрыть (Close)", 1);
            statuses.Add("Провалить (Fail)", 2);
            statuses.Add("Отменить (Cancel)", 3);

            foreach (KeyValuePair<int, int> pair in this.quests)
            {
                int quest_id = pair.Key;
                string status = "";
                foreach (KeyValuePair<string, int> quest_status in statuses)
                {
                    if (quest_status.Value == pair.Value) status = quest_status.Key;
                }
                object[] row = { quest_id, status};
                dataGridQuests.Rows.Add(row);
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            quests.Clear();
            foreach (DataGridViewRow row in dataGridQuests.Rows)
            {
                int id;
                if (int.TryParse(row.Cells[0].FormattedValue.ToString(), out id))
                {
                    string sValue = row.Cells[1].FormattedValue.ToString();
                    quests[id] = statuses[sValue];
                }
            }
            reward.randomQuest = cbRandom.Checked;
            form.checkRewardIndicates();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
