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
    //! Форма правки диалога и всех его опций
    public partial class EditDialogForm : Form
    {
        public MainForm parent;
        public CDialogPrecondition editPrecondition = new CDialogPrecondition();
        public List<int> editKarmaPK = new List<int>();
        public CDialog curDialog;

        int dialogID;
        bool isAdd;
        //! Максимальная длина ответа ГГ, при превышении которого выводится сообщение
        int MAX_SYMBOL_ANSWER = 24;

        public EditDialogForm(bool isAdd, MainForm parent, int selectedDialogID)
        {
            InitializeComponent();
            dialogID = selectedDialogID;
            this.isAdd = isAdd;
            this.parent = parent;
            this.parent.Enabled = false;


            CDialog curDialog = parent.getDialogOnDialogID(dialogID);
            lAttention.Text = "";
            if (curDialog.Precondition.Reputation.Any())
            {
                editPrecondition = curDialog.Precondition;
                checkReputationIndicates();
            }
            if (curDialog.Precondition.KarmaPK.Any())
            {
                editKarmaPK = curDialog.Precondition.KarmaPK;
            }

            if (parent.isRoot(selectedDialogID) && (!isAdd))
            {
                NPCReactionText.Text = "Приветствие:";
            }

            else if (parent.isRoot(selectedDialogID) && (isAdd))
            {
                //MustPanel.Location = new Point(5, 8);
            }
            if (!isAdd)
            {
                fillDialogEditForm(selectedDialogID);
                this.Text = "Добавление диалога в " + selectedDialogID + "";
            }

            this.Text += "   Версия: " + curDialog.version;
        }

        void fillDialogEditForm(int dialogID)
        {

            //reputationLabel0.Text = "< Военные Любеча <";
            //reputationLabel1.Text = "< Ученые Любеча <";
            //reputationLabel2.Text = "< Законопослушные <";
            //reputationLabel3.Text = "< Отступники <";
            //reputationLabel4.Text = "< ЛВЗ <";

            //System.Console.WriteLine("fillDialogEditForm");

            foreach (CDialog dialog in parent.getDialogsWithDialogIDInNodes(dialogID))
                    NPCSaidIs.Text+=(dialog.DialogID.ToString()+":\n"+dialog.Text);

            curDialog = parent.getDialogOnDialogID(dialogID);
            tPlayerText.Text = curDialog.Title.Normalize();
            tNPCReactiontextBox.Text = curDialog.Text;

            foreach (TreeNode active in parent.tree.Nodes.Find("Active",true))
                foreach (TreeNode node in active.Nodes)
                    ToDialogComboBox.Items.Add(node.Text);

            if (curDialog.Nodes.Any())
                foreach (int dialog in curDialog.Nodes)
                {
                    System.Console.WriteLine(dialog.ToString());
                    if (tSubDialogsTextBox.Text.Equals(""))
                        tSubDialogsTextBox.Text += dialog.ToString();
                    else
                        tSubDialogsTextBox.Text += ("," + dialog.ToString());
                }
            if (curDialog.Precondition.tests.Contains(1))
                CheckClanIDcheckBox.Checked = true;
            if (curDialog.Precondition.tests.Contains(0))
                CheckClanCheckBox.Checked = true;
            if (curDialog.Precondition.tests.Contains(2))
                CheckLonerCheckBox.Checked = true;

            if (curDialog.Actions.CompleteQuests.Any() || curDialog.Actions.GetQuests.Any() ||
                curDialog.Actions.Exit || curDialog.Actions.ToDialog!=0 || curDialog.Actions.Event != 0)
            {
                actionsCheckBox.Checked = true;
                if (curDialog.Actions.ToDialog != 0)
                {
                    ToDialogCheckBox.Checked = true;
                    int neededDialog = ToDialogComboBox.FindStringExact(curDialog.Actions.ToDialog.ToString());
                    ToDialogComboBox.SelectedIndex = neededDialog;
                }
                if (curDialog.Actions.Event == 1)
                    toTradeCheckBox.Checked = true;

                if (curDialog.Actions.Event == 2)
                    changeCheckBox.Checked = true;
                if (curDialog.Actions.Event == 4)
                    toRepairCheckBox.Checked = true;
                if (curDialog.Actions.Event == 6)
                    toComplexRepairCheckBox.Checked = true;
                if (curDialog.Actions.Event == 5)
                {
                    System.Console.WriteLine("curDialog.Actions.Event == 5");
                    teleportCheckBox.Checked = true;
                    string key = parent.tpConst.getName(curDialog.Actions.Data);
                    teleportComboBox.SelectedItem = key;
                }
                if (curDialog.Actions.Event == 7)
                    barterCheckBox.Checked = true;
                ExitCheckBox.Checked = curDialog.Actions.Exit;



                if (curDialog.Actions.CompleteQuests.Any())
                {
                    CompleteQuestsCheckBox.Checked = true;
                    foreach (int completeQuest in curDialog.Actions.CompleteQuests)
                    {

                        if (CompleteQuetsTextBox.Text == "")
                            CompleteQuetsTextBox.Text += completeQuest.ToString();
                        else
                            CompleteQuetsTextBox.Text += ("," + completeQuest.ToString());
                    }
                }

                if (curDialog.Actions.GetQuests.Any())
                {
                    GetQuestsCheckBox.Checked = true;
                    foreach (int getQuest in curDialog.Actions.GetQuests)
                    {
                        if (GetQuestsTextBox.Text == "")
                            GetQuestsTextBox.Text += getQuest.ToString();
                        else
                            GetQuestsTextBox.Text += ("," + getQuest.ToString());
                    }
                }
            }

            if (curDialog.Precondition.Any())
            {
                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests)
                    if (tShouldntHaveCompletedQuests.Text.Equals(""))
                        tShouldntHaveCompletedQuests.Text += quest.ToString();
                    else
                        tShouldntHaveCompletedQuests.Text += (","+quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests)
                    if (tShouldntHaveOpenQuests.Text.Equals(""))
                        tShouldntHaveOpenQuests.Text += quest.ToString();
                    else
                        tShouldntHaveOpenQuests.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests)
                    if (tShouldntHaveQuestsOnTest.Text.Equals(""))
                        tShouldntHaveQuestsOnTest.Text += quest.ToString();
                    else
                        tShouldntHaveQuestsOnTest.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfFailedQuests)
                    if (tShouldntHaveFailedQuests.Text.Equals(""))
                        tShouldntHaveFailedQuests.Text += quest.ToString();
                    else
                        tShouldntHaveFailedQuests.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)
                    if (tMustHaveCompletedQuests.Text.Equals(""))
                        tMustHaveCompletedQuests.Text += quest.ToString();
                    else
                        tMustHaveCompletedQuests.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    if (tMustHaveQuestsOnTest.Text.Equals(""))
                        tMustHaveQuestsOnTest.Text += quest.ToString();
                    else
                        tMustHaveQuestsOnTest.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)
                    if (tMustHaveOpenQuests.Text.Equals(""))
                        tMustHaveOpenQuests.Text += quest.ToString();
                    else
                        tMustHaveOpenQuests.Text += ("," + quest.ToString());

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests)
                    if (tMustHaveFailedQuests.Text.Equals(""))
                        tMustHaveFailedQuests.Text += quest.ToString();
                    else
                        tMustHaveFailedQuests.Text += ("," + quest.ToString());
            }

            //foreach (int key in curDialog.Precondition.Reputation.Keys)
            //{
            //    if (curDialog.Precondition.Reputation[key].Any())
            //    {
            //        if (key == 0)
            //        {
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 1)
            //                reputationTextBox01.Text = curDialog.Precondition.Reputation[key][1].ToString();
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 2)
            //                reputationTextBox0.Text = curDialog.Precondition.Reputation[key][2].ToString();
            //        }
            //        if (key == 1)
            //        {
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 1)
            //                reputationTextBox11.Text = curDialog.Precondition.Reputation[key][1].ToString();
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 2)
            //                reputationTextBox1.Text = curDialog.Precondition.Reputation[key][2].ToString();
            //        }
            //        if (key == 2)
            //        {
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 1)
            //                reputationTextBox21.Text = curDialog.Precondition.Reputation[key][1].ToString();
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 2)
            //                reputationTextBox2.Text = curDialog.Precondition.Reputation[key][2].ToString();
            //        }
            //        if (key == 3)
            //        {
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 1)
            //                reputationTextBox31.Text = curDialog.Precondition.Reputation[key][1].ToString();
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 2)
            //                reputationTextBox3.Text = curDialog.Precondition.Reputation[key][2].ToString();
            //        }
            //        if (key == 4)
            //        {
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 1)
            //                reputationTextBox41.Text = curDialog.Precondition.Reputation[key][1].ToString();
            //            if (curDialog.Precondition.Reputation[key][0] == 0 || curDialog.Precondition.Reputation[key][0] == 2)
            //                reputationTextBox4.Text = curDialog.Precondition.Reputation[key][2].ToString();
            //        }
            //    }

            //}

            curDialog.QuestDialog = parent.getDialogOnDialogID(dialogID).QuestDialog;

            calcSymbolMaxAnswer();
        }

        //! Нажатие Отмена - выход без сохранения
        private void bEditDialogCancel_Click(object sender, EventArgs e)
        {
            parent.Enabled = true;
            this.Close();
        }
        //! Чекбокс действия - возможность добавить действия к узлу диалога
        private void actionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            actionsBox.Enabled = actionsCheckBox.Checked;
        }
        //! Блокировка комбобокса при клике на соответствующий чекбокс 
        private void GetQuestsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GetQuestsTextBox.Enabled = GetQuestsCheckBox.Checked;
        }
        //! Блокировка комбобокса при клике на соответствующий чекбокс 
        private void CompleteQuestsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CompleteQuetsTextBox.Enabled = CompleteQuestsCheckBox.Checked;
        }
        //! Блокировка других действий при выборе
        private void AnyActionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                lockOtherEvent();
            else
                unlockOtherEvent();
        }
        //! Блокировка других компонентов при выборе опции "переход на диалог № xxx"
        private void ToDialogCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ToDialogComboBox.Enabled = ToDialogCheckBox.Checked;
            if (ToDialogCheckBox.Checked)
                lockOtherEvent();
            else
                unlockOtherEvent();
        }

        //! Блокирует контролы на форме
        private void lockOtherEvent()
        {
            if (!ExitCheckBox.Checked)
            {
                if (!ToDialogCheckBox.Checked)
                    ExitCheckBox.Checked = true;
                ExitCheckBox.Enabled = false;
            }
            toTradeCheckBox.Enabled = toTradeCheckBox.Checked;
            changeCheckBox.Enabled = changeCheckBox.Checked;
            toRepairCheckBox.Enabled = toRepairCheckBox.Checked;
            toComplexRepairCheckBox.Enabled = toComplexRepairCheckBox.Checked;
            ToDialogCheckBox.Enabled = ToDialogCheckBox.Checked;
            teleportCheckBox.Enabled = teleportCheckBox.Checked;
            barterCheckBox.Enabled = barterCheckBox.Checked;
        }
        //! Разблокирует контролы на форме
        private void unlockOtherEvent()
        {
            ExitCheckBox.Checked = false;
            ExitCheckBox.Enabled = true;
            toTradeCheckBox.Enabled = true;
            changeCheckBox.Enabled = true;
            toRepairCheckBox.Enabled = true;
            toComplexRepairCheckBox.Enabled = true;
            ToDialogCheckBox.Enabled = true;
            teleportCheckBox.Enabled = true;
            barterCheckBox.Enabled = true;
        }
        //! При наборе текста проверяем число символов в строке
        private void tPlayerText_KeyPress(object sender, KeyPressEventArgs e)
        {
            calcSymbolMaxAnswer();
        }
        //! Считает число символов в строке ответа ГГ и выводит предупреждение
        private void calcSymbolMaxAnswer()
        {
            lAttention.Text = "";
            int length = tPlayerText.Text.Length;
            if (length > this.MAX_SYMBOL_ANSWER)
                lAttention.Text = "Внимание: Текст более 24 символов!";
        }

        private void EditDialogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parent.Enabled = true;
        }

        private void bReputation_Click(object sender, EventArgs e)
        {
            DialogReputation reputationForm = new DialogReputation(this);
            reputationForm.Visible = true;
            this.Enabled = false;
        }

        public void checkReputationIndicates()
        {
            if (editPrecondition.Reputation.Any())
                bReputation.Image = Properties.Resources.but_indicate;
        }

        private void bKarma_Click(object sender, EventArgs e)
        {
            DialogKarmaPK dialogKarma = new DialogKarmaPK(this);
            dialogKarma.Visible = true;
            this.Enabled = false;
        }
        //! Чекбокс телепорта - заполняем возможные локации
        private void teleportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            teleportComboBox.Items.Add("");
            foreach (string key in parent.tpConst.getKeys())
                teleportComboBox.Items.Add(key);

            if (teleportCheckBox.Checked)
                lockOtherEvent();
            else
                unlockOtherEvent();
            teleportComboBox.Enabled = !teleportComboBox.Enabled;
        }

        private void bEditDialogOk_Click(object sender, EventArgs e)
        {
            int newID;
            Actions actions = new Actions();
            CDialogPrecondition precondition = new CDialogPrecondition();
            List<int> nodes = new List<int>();
            int questDialog = parent.getDialogOnDialogID(dialogID).QuestDialog;
            //List<string> holder = new List<string>();
            string holder;

            //holder.Add(parent.currentNPC);
            holder = parent.currentNPC;

            if (!tSubDialogsTextBox.Text.Equals(""))
                foreach (string node in tSubDialogsTextBox.Text.Split(','))
                    nodes.Add(int.Parse(node));
            if (actionsCheckBox.Checked)
            {
                actions.Exit = ExitCheckBox.Checked;
                if (toTradeCheckBox.Checked)
                    actions.Event = 1;
                if (changeCheckBox.Checked)
                    actions.Event = 2;
                if (ToDialogCheckBox.Checked)
                    actions.ToDialog = int.Parse(ToDialogComboBox.SelectedItem.ToString());
                if (toRepairCheckBox.Checked)
                    actions.Event = 4;
                if (teleportCheckBox.Checked && !teleportComboBox.SelectedItem.ToString().Equals(""))
                {
                    actions.Event = 5;
                    actions.Data = parent.tpConst.getTtID(teleportComboBox.SelectedItem.ToString());
                }
                if (toComplexRepairCheckBox.Checked)
                    actions.Event = 6;
                if (barterCheckBox.Checked)
                    actions.Event = 7;

                if (GetQuestsCheckBox.Checked)
                    foreach (string quest in GetQuestsTextBox.Text.Split(','))
                        actions.GetQuests.Add(int.Parse(quest));
                if (CompleteQuestsCheckBox.Checked)
                    foreach (string quest in CompleteQuetsTextBox.Text.Split(','))
                        actions.CompleteQuests.Add(int.Parse(quest));
            }

            if (!tMustHaveOpenQuests.Text.Equals(""))
                foreach (string quest in tMustHaveOpenQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfOpenedQuests.Add(int.Parse(quest));
            if (!tMustHaveQuestsOnTest.Text.Equals(""))
                foreach (string quest in tMustHaveQuestsOnTest.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfOnTestQuests.Add(int.Parse(quest));
            if (!tMustHaveCompletedQuests.Text.Equals(""))
                foreach (string quest in tMustHaveCompletedQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfCompletedQuests.Add(int.Parse(quest));
            if (!tMustHaveFailedQuests.Text.Equals(""))
                foreach (string quest in tMustHaveFailedQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfFailedQuests.Add(int.Parse(quest));

            if (!tShouldntHaveOpenQuests.Text.Equals(""))
                foreach (string quest in tShouldntHaveOpenQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfOpenedQuests.Add(int.Parse(quest));
            if (!tShouldntHaveQuestsOnTest.Text.Equals(""))
                foreach (string quest in tShouldntHaveQuestsOnTest.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfOnTestQuests.Add(int.Parse(quest));
            if (!tShouldntHaveCompletedQuests.Text.Equals(""))
                foreach (string quest in tShouldntHaveCompletedQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfCompletedQuests.Add(int.Parse(quest));
            if (!tShouldntHaveFailedQuests.Text.Equals(""))
                foreach (string quest in tShouldntHaveFailedQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfFailedQuests.Add(int.Parse(quest));

            precondition.tests.Clear();
            if (CheckClanIDcheckBox.Checked)
                precondition.tests.Add(1);
            if (CheckClanCheckBox.Checked)
                precondition.tests.Add(0);
            if (CheckLonerCheckBox.Checked)
                precondition.tests.Add(2);

            //if (!reputationTextBox01.Text.Equals("") || !reputationTextBox0.Text.Equals(""))
            //{
            //    precondition.Reputation.Add(0, new List<int>());
            //    if (!reputationTextBox01.Text.Equals("") && !reputationTextBox0.Text.Equals(""))
            //    {
            //        precondition.Reputation[0].Add(0);
            //        precondition.Reputation[0].Add(int.Parse(reputationTextBox01.Text));
            //        precondition.Reputation[0].Add(int.Parse(reputationTextBox0.Text));
            //    }
            //    else if (!reputationTextBox01.Text.Equals("") && reputationTextBox0.Text.Equals(""))
            //    {
            //        precondition.Reputation[0].Add(1);
            //        precondition.Reputation[0].Add(int.Parse(reputationTextBox01.Text));
            //        precondition.Reputation[0].Add(0);
            //    }
            //    else if (reputationTextBox01.Text.Equals("") && !reputationTextBox0.Text.Equals(""))
            //    {
            //        precondition.Reputation[0].Add(2);
            //        precondition.Reputation[0].Add(0);
            //        precondition.Reputation[0].Add(int.Parse(reputationTextBox0.Text));
            //    }

            //}
            //if (!reputationTextBox11.Text.Equals("") || !reputationTextBox1.Text.Equals(""))
            //{
            //    precondition.Reputation.Add(1, new List<int>());
            //    if (!reputationTextBox11.Text.Equals("") && !reputationTextBox1.Text.Equals(""))
            //    {
            //        precondition.Reputation[1].Add(0);
            //        precondition.Reputation[1].Add(int.Parse(reputationTextBox11.Text));
            //        precondition.Reputation[1].Add(int.Parse(reputationTextBox1.Text));
            //    }
            //    else if (!reputationTextBox11.Text.Equals("") && reputationTextBox1.Text.Equals(""))
            //    {
            //        precondition.Reputation[1].Add(1);
            //        precondition.Reputation[1].Add(int.Parse(reputationTextBox11.Text));
            //        precondition.Reputation[1].Add(0);
            //    }
            //    else if (reputationTextBox11.Text.Equals("") && !reputationTextBox1.Text.Equals(""))
            //    {
            //        precondition.Reputation[1].Add(2);
            //        precondition.Reputation[1].Add(0);
            //        precondition.Reputation[1].Add(int.Parse(reputationTextBox1.Text));
            //    }

            //}
            //if (!reputationTextBox21.Text.Equals("") || !reputationTextBox2.Text.Equals(""))
            //{
            //    precondition.Reputation.Add(2, new List<int>());
            //    if (!reputationTextBox21.Text.Equals("") && !reputationTextBox2.Text.Equals(""))
            //    {
            //        precondition.Reputation[2].Add(0);
            //        precondition.Reputation[2].Add(int.Parse(reputationTextBox21.Text));
            //        precondition.Reputation[2].Add(int.Parse(reputationTextBox2.Text));
            //    }
            //    else if (!reputationTextBox21.Text.Equals("") && reputationTextBox2.Text.Equals(""))
            //    {
            //        precondition.Reputation[2].Add(1);
            //        precondition.Reputation[2].Add(int.Parse(reputationTextBox21.Text));
            //        precondition.Reputation[2].Add(0);
            //    }
            //    else if (reputationTextBox21.Text.Equals("") && !reputationTextBox2.Text.Equals(""))
            //    {
            //        precondition.Reputation[2].Add(2);
            //        precondition.Reputation[2].Add(0);
            //        precondition.Reputation[2].Add(int.Parse(reputationTextBox2.Text));
            //    }
            //}
            //if (!reputationTextBox31.Text.Equals("") || !reputationTextBox3.Text.Equals(""))
            //{
            //    precondition.Reputation.Add(3, new List<int>());
            //    if (!reputationTextBox21.Text.Equals("") && !reputationTextBox2.Text.Equals(""))
            //    {
            //        precondition.Reputation[3].Add(0);
            //        precondition.Reputation[3].Add(int.Parse(reputationTextBox31.Text));
            //        precondition.Reputation[3].Add(int.Parse(reputationTextBox3.Text));
            //    }
            //    else if (!reputationTextBox31.Text.Equals("") && reputationTextBox3.Text.Equals(""))
            //    {
            //        precondition.Reputation[3].Add(1);
            //        precondition.Reputation[3].Add(int.Parse(reputationTextBox31.Text));
            //        precondition.Reputation[3].Add(0);
            //    }
            //    else if (reputationTextBox31.Text.Equals("") && !reputationTextBox3.Text.Equals(""))
            //    {
            //        precondition.Reputation[3].Add(2);
            //        precondition.Reputation[3].Add(0);
            //        precondition.Reputation[3].Add(int.Parse(reputationTextBox3.Text));
            //    }
            //}
            //if (!reputationTextBox41.Text.Equals("") || !reputationTextBox4.Text.Equals(""))
            //{
            //    precondition.Reputation.Add(4, new List<int>());
            //    if (!reputationTextBox41.Text.Equals("") && !reputationTextBox4.Text.Equals(""))
            //    {
            //        precondition.Reputation[4].Add(0);
            //        precondition.Reputation[4].Add(int.Parse(reputationTextBox41.Text));
            //        precondition.Reputation[4].Add(int.Parse(reputationTextBox4.Text));
            //    }
            //    else if (!reputationTextBox41.Text.Equals("") && reputationTextBox4.Text.Equals(""))
            //    {
            //        precondition.Reputation[4].Add(1);
            //        precondition.Reputation[4].Add(int.Parse(reputationTextBox41.Text));
            //        precondition.Reputation[4].Add(0);
            //    }
            //    else if (reputationTextBox41.Text.Equals("") && !reputationTextBox4.Text.Equals(""))
            //    {
            //        precondition.Reputation[4].Add(2);
            //        precondition.Reputation[4].Add(0);
            //        precondition.Reputation[4].Add(int.Parse(reputationTextBox4.Text));
            //    }
            //}

            //if (editPrecondition.Reputation.Any())
            precondition.Reputation = editPrecondition.Reputation;
            precondition.KarmaPK = editKarmaPK;

            NodeCoordinates nodeC = new NodeCoordinates();
            if (isAdd)
            {
                //! @todo разбираться с созданием диалога
                newID = parent.getDialogsNewID();
                parent.addActiveDialog(newID, new CDialog(holder, tPlayerText.Text, tNPCReactiontextBox.Text, questDialog, precondition, actions, nodes, newID, 1, nodeC), dialogID);
            }
            else
            {
                parent.replaceDialog(new CDialog(holder
                    , tPlayerText.Text, tNPCReactiontextBox.Text, questDialog, precondition, actions, nodes, dialogID, (curDialog.version + 1), nodeC), dialogID);
            }
            parent.Enabled = true;
            this.Close();
        }
    }
}
