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
    public partial class RewardBlackBoxDialog : Form
    {
        int questID;
        public MainForm parent;
        EditQuestForm parentForm;

        public RewardBlackBoxDialog(MainForm parent, EditQuestForm parentForm, int questID, int type)
        {
            InitializeComponent();
            this.parent = parent;
            this.parentForm = parentForm;
            this.questID = questID;
            CBlackBoxConstants blackBoxes = new CBlackBoxConstants();

            ((DataGridViewComboBoxColumn)itemGridView.Columns[0]).Items.Add("");

            foreach (string bb_name in blackBoxes.getAll())
                ((DataGridViewComboBoxColumn)itemGridView.Columns[0]).Items.Add(bb_name);

            foreach (string name in parentForm.editQuestReward.blackBoxes)
            {
                object[] row = { name };
                itemGridView.Rows.Add(row);
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            List<string> result = new List<string>();
            foreach (DataGridViewRow row in itemGridView.Rows)
            {
                string bb_name = row.Cells["name"].FormattedValue.ToString();
                if (!bb_name.Any()) continue;
                result.Add(bb_name);
            }
            parentForm.editQuestReward.blackBoxes = result;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
