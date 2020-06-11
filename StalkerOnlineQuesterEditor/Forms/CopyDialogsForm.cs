using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class CopyDialogsForm : Form
    {
        public CopyDialogsForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.fillNPCBox();
        }

        MainForm parent;
        public string current_npc;
        public int current_dialogID;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.OK;
        }

        void fillNPCBox()
        {
            NPCBox.DataSource = null;       // костыль для обновления данных в кмобобоксе NPC при добавлении/удалении
            NPCBox.DisplayMember = "DisplayString";
            NPCBox.ValueMember = "Value";
            NPCBox.DataSource = parent.getCopyNpcNames();
            NPCBox.SelectedIndex = 1;
        }

        private void NPCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDialogs.Items.Clear();
            current_npc = (NPCBox.Items[NPCBox.SelectedIndex] as NPCNameDataSourceObject).Value;
            Dictionary<int, CDialog> dialogs = parent.dialogs.dialogs[current_npc];
            foreach (CDialog dialog in dialogs.Values)
            {
                cbDialogs.Items.Add(dialog.DialogID);
            }
            cbDialogs.SelectedIndex = 1;

        }

        private void FakeNPCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string open_npc_name = FakeNPCBox.SelectedItem.ToString();
            for (int i = 0; i < NPCBox.Items.Count; i++)
            {
                string npc_name = (NPCBox.Items[i] as NPCNameDataSourceObject).DisplayString;
                if (npc_name == open_npc_name)
                {
                    NPCBox.SelectedIndex = i;
                }
            }
        }

        private void NPCBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = NPCBox.Text;
                if (parent.dialogs.dialogs.Keys.Contains(text))
                    return;
                string name = "";
                if (parent.settings.getMode() == parent.settings.MODE_EDITOR)
                {
                    if (FakeNPCBox.Items.Count > 0)
                        NPCBox.SelectedValue = FakeNPCBox.Items[0].ToString().Split('(')[0];
                    FakeNPCBox.DroppedDown = false;
                }
                else if (parent.settings.getMode() == parent.settings.MODE_LOCALIZATION)
                {
                    if (parent.ManagerNPC.engNamesToNPC.ContainsKey(text))
                    {
                        name = parent.ManagerNPC.engNamesToNPC[text];
                        NPCBox.SelectedValue = name;
                    }
                }
            }
        }

        private void cbDialogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, CDialog> dialogs = parent.dialogs.dialogs[current_npc];
            CDialog dialog = dialogs[Convert.ToInt32(cbDialogs.SelectedItem.ToString())];
            current_dialogID = dialog.DialogID;
            lbDialogText.Visible = true;
            lbDialogText.Text = dialog.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
