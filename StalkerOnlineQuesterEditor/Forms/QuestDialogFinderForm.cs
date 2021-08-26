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
    public partial class QuestDialogFinderForm : Form
    {
        private MainForm parent;
        private int mode;

        public QuestDialogFinderForm(MainForm parent, int mode)
        {
            InitializeComponent();
            this.parent = parent;
            this.mode = mode;
            if (mode == 0)
                label1.Text = "Введите QuestID:";
            else if (mode == 1)
                label1.Text = "Введите ЗнаниеID:";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (mode == 0)
                findQuests();
            else if (mode == 1)
                findKnowleges();
        }


        private void findKnowleges()
        {
            int knowlegeID = 0;
            List<int> checking = new List<int>();
            List<int> opening = new List<int>();
            if (!int.TryParse(textBox1.Text, out knowlegeID) || knowlegeID == 0)
            {
                return;
            }
            foreach (var npc in parent.dialogs.dialogs)
            {
                foreach (var dialog in npc.Value)
                {
                    if (dialog.Value.Precondition.knowledges.mustKnowledge.Contains(knowlegeID) || dialog.Value.Precondition.knowledges.shouldntKnowledge.Contains(knowlegeID))
                        checking.Add(dialog.Key);
                    if (dialog.Value.Actions.GetKnowleges.Contains(knowlegeID))
                        opening.Add(dialog.Key);
                }
            }
            onFoundKnowleges(checking, opening);
        }

        private void findQuests()
        {
            int questID = 0;
            if (!int.TryParse(textBox1.Text, out questID) || questID == 0)
            {
                onNotFoundQuest(questID);
                return;
            }
            CQuest quest = this.parent.getQuestOnQuestID(questID);
            if (quest == null)
            {
                onNotFoundQuest(questID);
                return;
            }
            List<int> checking = new List<int>();
            List<int> opening = new List<int>();
            List<int> closing = new List<int>();
            foreach(var npc in parent.dialogs.dialogs)
            {
                foreach (var dialog in npc.Value)
                {
                    if (dialog.Value.Precondition.ListOfMustNoQuests.hasQuest(questID) || dialog.Value.Precondition.ListOfNecessaryQuests.hasQuest(questID))
                        checking.Add(dialog.Key);
                    if (dialog.Value.Actions.CancelQuests.Contains(questID) || dialog.Value.Actions.FailQuests.Contains(questID) ||
                        dialog.Value.Actions.CompleteQuests.Contains(questID))
                        closing.Add(dialog.Key);
                    if (dialog.Value.Actions.GetQuests.Contains(questID))
                        opening.Add(dialog.Key);
                }
            }

            if(!checking.Any() && !opening.Any() && !closing.Any())
            {
                onNotFoundDialogs();
                return;
            }

            onFound(checking, opening, closing);
        }

        private void onNotFoundQuest(int questID)
        {
            label2.Visible = true;
            label2.Text = "Квест ID:" + questID.ToString() + " не найден";
        }

        private void onNotFoundDialogs()
        {
            label2.Visible = true;
            label2.Text = "Диалоги не найдены";
        }

        private void onFoundKnowleges(List<int> checking, List<int> opening)
        {
            label2.Visible = false;
            treeView1.Nodes.Clear();
            if (checking.Any())
            {
                TreeNode node = new TreeNode("Проверяется в диалогах:");
                foreach (int questID in checking)
                {
                    node.Nodes.Add(questID.ToString());
                }
                treeView1.Nodes.Add(node);
            }
            if (opening.Any())
            {
                TreeNode node = new TreeNode("Выдаётся в диалогах:");
                foreach (int questID in opening)
                {
                    node.Nodes.Add(questID.ToString());
                }
                treeView1.Nodes.Add(node);
            }
        }

        private void onFound(List<int> checking, List<int> opening, List<int> closing)
        {
            label2.Visible = false;
            treeView1.Nodes.Clear();
            if (checking.Any())
            {
                TreeNode node = new TreeNode("Проверяется в диалогах:");
                foreach(int questID in checking)
                {
                    node.Nodes.Add(questID.ToString());
                }
                treeView1.Nodes.Add(node);
            }
            if (opening.Any())
            {
                TreeNode node = new TreeNode("Открывается в диалогах:");
                foreach (int questID in opening)
                {
                    node.Nodes.Add(questID.ToString());
                }
                treeView1.Nodes.Add(node);
            }

            if (closing.Any())
            {
                TreeNode node = new TreeNode("Закрывается в диалогах:");
                foreach (int questID in closing)
                {
                    node.Nodes.Add(questID.ToString());
                }
                treeView1.Nodes.Add(node);
            }

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int dialogID;
            if (!int.TryParse(e.Node.Text, out dialogID)) return;
            parent.findDialogByID(dialogID);
        }
    }
}
