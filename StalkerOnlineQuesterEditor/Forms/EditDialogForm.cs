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
        int currentDialogID;
        bool isAdd;
        //! Максимальная длина ответа ГГ, при превышении которого выводится сообщение
        int MAX_SYMBOL_ANSWER = 48;

        //! Конструктор
        public EditDialogForm(bool isAdd, MainForm parent, int selectedDialogID)
        {
            InitializeComponent();
            currentDialogID = selectedDialogID;
            this.isAdd = isAdd;
            this.parent = parent;
            this.parent.Enabled = false;

            curDialog = parent.getDialogOnDialogID(currentDialogID);
            lAttention.Text = "";
            teleportComboBox.Items.Clear();
            foreach (string key in parent.tpConst.getKeys())
                teleportComboBox.Items.Add(key);

            FillActionsComboBox();

            if (parent.isRoot(currentDialogID) && (!isAdd))
                lReactionNPC.Text = "Приветствие:";
            if (isAdd)
                mtbPlayerLevel.Text = "0";
            if (!isAdd)
            {
                fillDialogEditForm(currentDialogID);
                this.Text = "Редактирование диалога ID = " + currentDialogID + "";
            }
            this.initReputationTab();
            this.initKarmaPKTab();
            this.Text += "   Версия: " + curDialog.version;
        }

        //! Заполняет всю форму данными из CDialog
        void fillDialogEditForm(int dialogID)
        {
            foreach (CDialog dialog in parent.getDialogsWithDialogIDInNodes(dialogID))
                    NPCSaidIs.Text+=(dialog.DialogID.ToString()+":\n"+dialog.Text);
            // заполнение текста речевки и ответа ГГ
            tPlayerText.Text = curDialog.Title.Normalize();
            tReactionNPC.Text = curDialog.Text;

            foreach (TreeNode active in parent.tree.Nodes.Find("Active",true))
                foreach (TreeNode node in active.Nodes)
                    ToDialogComboBox.Items.Add(node.Text);
            // заполнение текстбокса "Поддиалоги" (Nodes)
            if (curDialog.Nodes.Any())
                foreach (int dialog in curDialog.Nodes)
                {
                    if (tNodes.Text.Equals(""))
                        tNodes.Text += dialog.ToString();
                    else
                        tNodes.Text += ("," + dialog.ToString());
                }
            // какой-то пиздец c кланами и одиночками
            if (curDialog.Precondition.tests.Contains(1))
                cbSameClanOnly.Checked = true;
            if (curDialog.Precondition.tests.Contains(0))
                cbAnyClanOnly.Checked = true;
            if (curDialog.Precondition.tests.Contains(2))
                cbLonerOnly.Checked = true;
            ShowClanOptions();

            if (curDialog.Precondition.Reputation.Any())
            {
                editPrecondition = curDialog.Precondition;
                checkReputationIndicates();
            }
            if (curDialog.Precondition.KarmaPK.Any())
            {
                editKarmaPK = curDialog.Precondition.KarmaPK;
                checkKarmaIndicates();
            }

            if (curDialog.Actions.Exists() || curDialog.Actions.Exit || curDialog.Actions.ToDialog!=0 )
            {
                actionsCheckBox.Checked = true;
                ActionsComboBox.SelectedValue = curDialog.Actions.Event.Value;
                cbExit.Checked = curDialog.Actions.Exit;

                if (ActionsComboBox.Text == "Телепорт")
                {
                    string key = parent.tpConst.getName(curDialog.Actions.Data);
                    teleportComboBox.SelectedItem = key;
                    teleportComboBox.Visible = true;
                }
                
                if (curDialog.Actions.ToDialog != 0)
                {
                    ActionsComboBox.SelectedValue = 100;
                    int neededDialog = ToDialogComboBox.FindStringExact(curDialog.Actions.ToDialog.ToString());
                    ToDialogComboBox.SelectedIndex = neededDialog;
                }

                if (curDialog.Actions.CompleteQuests.Any())
                {
                    cbCompleteQuests.Checked = true;
                    foreach (int completeQuest in curDialog.Actions.CompleteQuests)
                        addItemToTextBox(completeQuest.ToString(), tbCompleteQuests);
                }

                if (curDialog.Actions.GetQuests.Any())
                {
                    cbGetQuests.Checked = true;
                    foreach (int getQuest in curDialog.Actions.GetQuests)
                        addItemToTextBox(getQuest.ToString(), tbGetQuests);
                }

                if (curDialog.Actions.CancelQuests.Any())
                {
                    cbCancelQuests.Checked = true;
                    foreach (int cancelQuest in curDialog.Actions.CancelQuests)
                        addItemToTextBox(cancelQuest.ToString(), tbCancelQuests);
                }

                if (curDialog.Actions.FailQuests.Any())
                {
                    cbFailQuests.Checked = true;
                    foreach (int failQuest in curDialog.Actions.FailQuests)
                        addItemToTextBox(failQuest.ToString(), tbFailQuests);
                }
            }

            // заполнение условий для открытия диалога - список открытых, завершенных, заваленных квестов
            if (curDialog.Precondition.Any())
            {
                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveCompletedQuests);

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveOpenQuests);

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveQuestsOnTest);

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfFailedQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveFailedQuests);

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfMassQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveMassQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveCompletedQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveQuestsOnTest);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveOpenQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveFailedQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfMassQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveMassQuests);
            }
            if (curDialog.DebugData != "") debugTextBox.Text = curDialog.DebugData;
            mtbPlayerLevel.Text = curDialog.Precondition.PlayerLevel.ToString();            
            calcSymbolMaxAnswer();
        }

        //! Антиговнокод-функция, добавление номера квеста в текстбокс
        private void addItemToTextBox(string item, MaskedTextBox textBox)
        {
            if (textBox.Text.Equals(""))
                textBox.Text += item;
            else
                textBox.Text += ("," + item);
        }

        private void FillActionsComboBox()
        {
            ActionsComboBox.Items.Clear();            
            ActionsComboBox.DataSource = parent.dialogEvents.GetFullList();
            ActionsComboBox.DisplayMember = "Display";
            ActionsComboBox.ValueMember = "Value";
        }
        //! Скрывает опции кланов, если они не заполнены при открытии формы
        private void ShowClanOptions()
        {
            if (cbSameClanOnly.Checked || cbAnyClanOnly.Checked || cbLonerOnly.Checked)
                gbClanOptions.Visible = true;
            else
                gbClanOptions.Visible = false;
        }

        //! Клик по кнопке "показать клановые опции"
        private void cbShowClanOptions_Click(object sender, EventArgs e)
        {
            gbClanOptions.Visible = !gbClanOptions.Visible;     // cbShowClanOptions.CheckState == CheckState.Checked;
            if (!gbClanOptions.Visible && (cbSameClanOnly.Checked || cbAnyClanOnly.Checked || cbLonerOnly.Checked))
                cbShowClanOptions.CheckState = CheckState.Indeterminate;
            else if (gbClanOptions.Visible)
                cbShowClanOptions.CheckState = CheckState.Checked;
            else
                cbShowClanOptions.CheckState = CheckState.Unchecked;
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
            gbActions.Enabled = actionsCheckBox.Checked;
        }
        //! Блокировка комбобокса при клике на соответствующий чекбокс 
        private void cbGetQuests_CheckedChanged(object sender, EventArgs e)
        {
            tbGetQuests.Enabled = cbGetQuests.Checked;
        }
        //! Блокировка комбобокса при клике на соответствующий чекбокс 
        private void cbCompleteQuests_CheckedChanged(object sender, EventArgs e)
        {
            tbCompleteQuests.Enabled = cbCompleteQuests.Checked;
        }

        private void cbCancelQuests_CheckedChanged(object sender, EventArgs e)
        {
            tbCancelQuests.Enabled = cbCancelQuests.Checked;
        }

        private void cbFailQuests_CheckedChanged(object sender, EventArgs e)
        {
            tbFailQuests.Enabled = cbFailQuests.Checked;
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
                lAttention.Text = "Внимание: Текст более " + MAX_SYMBOL_ANSWER.ToString() + " символов!";
        }

        private void EditDialogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parent.Enabled = true;
        }
        //! Открытие окна выбора репутации
        private void bReputation_Click(object sender, EventArgs e)
        {
            DialogReputation reputationForm = new DialogReputation(this);
            reputationForm.Visible = true;
            this.Enabled = false;
        }
        //! Открытие окна кармы
        private void bKarma_Click(object sender, EventArgs e)
        {
            DialogKarmaPK dialogKarma = new DialogKarmaPK(this);
            dialogKarma.Visible = true;
            this.Enabled = false;
        }
        //! Задать цвет кнопки репутации, если репутация задана
        public void checkReputationIndicates()
        {
            if (editPrecondition.Reputation.Any())
                bReputation.Image = Properties.Resources.but_indicate;
            else
                bReputation.Image = null;
        }
        //! Задать цвет кнопки кармы, если карма задана
        public void checkKarmaIndicates()
        {
            if (editKarmaPK.Any())
                bKarma.Image = Properties.Resources.but_indicate;
            else
                bKarma.Image = null;
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            ActionsComboBox.Enabled = !cbExit.Checked;
        }

        //! Изменение текущего однократного действия
        private void ActionsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int SelectedValue = -1;
            if (!int.TryParse(ActionsComboBox.SelectedValue.ToString(), out SelectedValue))
                return;

            teleportComboBox.Visible = (SelectedValue == 5);
            ToDialogComboBox.Visible = (SelectedValue == 100);

            switch (SelectedValue)
            { 
                case 0:
                    cbExit.Checked = false;
                    cbExit.Enabled = true;
                    break;
                case 100:
                    cbExit.Checked = false;
                    cbExit.Enabled = false;
                    break;
                default:
                    cbExit.Checked = true;
                    cbExit.Enabled = false;
                    break;
            }
        }

        bool CheckConditions()
        {
            if (ActionsComboBox.Text == "Переход к диалогу")
            {
                String toDialog = ToDialogComboBox.Text;
                int index = ToDialogComboBox.Items.IndexOf(toDialog);
                //int index = ToDialogComboBox.FindString(toDialog);    // very bad way to do!!!
                if (index == -1)
                {
                    MessageBox.Show("Действие невозможно: такой диалог не существует у этого персонажа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else if (ActionsComboBox.Text == "Телепорт")
            {
                if (teleportComboBox.Text.Equals(""))
                {
                    MessageBox.Show("Действие невозможно: не задана точка для телепортации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        //! Нажатие ОК - сохранение всех опций диалога и выход на главную
        private void bEditDialogOk_Click(object sender, EventArgs e)
        {
            int newID;
            string DebugData = ""; 
            Actions actions = new Actions();
            CDialogPrecondition precondition = new CDialogPrecondition();
            NodeCoordinates coord = new NodeCoordinates();
            List<int> nodes = new List<int>();
            //List<string> holder = new List<string>();
            string holder = parent.GetCurrentNPC();
            if (!CheckConditions())
                return;
            if (!tNodes.Text.Equals(""))
                foreach (string node in tNodes.Text.Split(','))
                    nodes.Add(int.Parse(node));


            if(!this.checkReputation()) 
                return;
            this.checkKarmaPK();
            // заполняем действия диалога - торговля, бартер, починка, телепорт и т.д.
            if (actionsCheckBox.Checked)
            {
                actions.Exit = cbExit.Checked;
                actions.Event = parent.dialogEvents.GetEventFromID((int) ActionsComboBox.SelectedValue);
                if (actions.Event.Display == "Телепорт")
                    actions.Data = parent.tpConst.getTtID(teleportComboBox.SelectedItem.ToString());
                if (actions.Event.Display == "Переход к диалогу")
                    actions.ToDialog = int.Parse(ToDialogComboBox.Text.ToString());

                if (cbGetQuests.Checked)
                    foreach (string quest in tbGetQuests.Text.Split(','))
                        actions.GetQuests.Add(int.Parse(quest));
                if (cbCompleteQuests.Checked)
                    foreach (string quest in tbCompleteQuests.Text.Split(','))
                        actions.CompleteQuests.Add(int.Parse(quest));
                if (cbCancelQuests.Checked)
                    foreach (string quest in tbCancelQuests.Text.Split(','))
                        actions.CancelQuests.Add(int.Parse(quest));
                if (cbFailQuests.Checked)
                    foreach (string quest in tbFailQuests.Text.Split(','))
                        actions.FailQuests.Add(int.Parse(quest));
            }
            else
                actions.Event = parent.dialogEvents.GetEventFromID(0);

            // заполняем условия появления диалога - открытые и закрытые квесты и т.д.
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
            if (!tMustHaveMassQuests.Text.Equals(""))
                foreach (string quest in tMustHaveMassQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfMassQuests.Add(int.Parse(quest));

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
            if (!tShouldntHaveMassQuests.Text.Equals(""))
                foreach (string quest in tShouldntHaveMassQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfMassQuests.Add(int.Parse(quest));
            
            precondition.tests.Clear();
            if (cbSameClanOnly.Checked)
                precondition.tests.Add(1);
            if (cbAnyClanOnly.Checked)
                precondition.tests.Add(0);
            if (cbLonerOnly.Checked)
                precondition.tests.Add(2);

            precondition.PlayerLevel = int.Parse(mtbPlayerLevel.Text.ToString());
            precondition.Reputation = editPrecondition.Reputation;
            precondition.KarmaPK = editKarmaPK;

            if (debugTextBox.Text != "")
                DebugData = debugTextBox.Text;

            if (isAdd)
            {
                newID = parent.getDialogsNewID();
                parent.addActiveDialog(newID, new CDialog(holder, tPlayerText.Text, tReactionNPC.Text, precondition, actions, nodes, newID, 1, coord, DebugData), currentDialogID);
            }
            else
            {
                coord.X = curDialog.coordinates.X;
                coord.Y = curDialog.coordinates.Y;
                coord.RootDialog = curDialog.coordinates.RootDialog;
                coord.Active = curDialog.coordinates.Active;
                int version = curDialog.version;
                if (tPlayerText.Text != curDialog.Title || tReactionNPC.Text != curDialog.Text)
                    version++;
                parent.replaceDialog(new CDialog(holder, tPlayerText.Text, tReactionNPC.Text,
                    precondition, actions, nodes, currentDialogID, version, coord, DebugData), currentDialogID);
            }
            parent.Enabled = true;
            parent.DialogSelected(true);
            parent.startEmulator(currentDialogID);
            this.Close();
        }

        private void initReputationTab()
        {
            CFracConstants frac = this.parent.fractions;
            foreach (KeyValuePair<int, string> pair in frac.getListOfFractions())
            {
                int id = pair.Key;
                string name = pair.Value;
                string a = "";
                string b = "";
                if (this.editPrecondition.Reputation.Keys.Contains(pair.Key))
                {
                    if (this.editPrecondition.Reputation[id].Count == 3)         // костыль для старой версии, выжечт огнем позже
                    {
                        double type = this.editPrecondition.Reputation[pair.Key][0];
                        if (type == 0 || (type == 1))
                            a = this.editPrecondition.Reputation[pair.Key][1].ToString();
                        if (type == 0 || (type == 2))
                            b = this.editPrecondition.Reputation[pair.Key][2].ToString();
                    }
                    else if (this.editPrecondition.Reputation[id].Count == 2)
                    {
                        if (this.editPrecondition.Reputation[id][0] != double.NegativeInfinity)
                            a = this.editPrecondition.Reputation[id][0].ToString();
                        if (this.editPrecondition.Reputation[id][1] != double.PositiveInfinity)
                            b = this.editPrecondition.Reputation[id][1].ToString();
                    }
                }
                object[] row = { id, name, a, b };
                dataReputation.Rows.Add(row);
            }
        }

        private bool checkReputation()
        {
            this.editPrecondition.Reputation.Clear();
            foreach (DataGridViewRow row in dataReputation.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    int fractionID = int.Parse(row.Cells[0].FormattedValue.ToString());
                    //string fractionName = row.Cells[1].FormattedValue.ToString();
                    string stringA = row.Cells[2].FormattedValue.ToString().Replace('.', ',');
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
                            MessageBox.Show("Неправильное условие по репутации! Значение А должно быть меньше B", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        this.editPrecondition.Reputation.Add(fractionID, new List<double>() { doubleA, doubleB });
                    }
                }
            }
            this.checkReputationIndicates();
            return true;
        }

        private void initKarmaPKTab()
        {
            labelDescription.Text = "Задаются пороги Кармы A,B такие, что A < Karma < B \n" +
                "Игрок начинает игру с Кармой ПК = 0. \n" +
                "За каждое ПК убийство Карма увеличивается на 100. \n" +
                "После превышения Кармой 500 жизнь осложняется: \n" +
                "Дроп выше, NPC не разговаривают, ник подсвечен красным";

            List<int> karma = this.editKarmaPK;
            if (karma.Any())
            {
                if (karma[0] == 0 || karma[0] == 1)
                {
                    aTextBox.Text = karma[2].ToString();    // костыль, похоже перепутаны значения
                }
                if (karma[0] == 0 || karma[0] == 2)
                {
                    bTextBox.Text = karma[1].ToString();    // костыль, похоже перепутаны значения
                }
            }
        }

        private void checkKarmaPK()
        {
            this.editKarmaPK.Clear();
            int flag = 0;
            int a = 0;
            int b = 0;
            if ((aTextBox.Text != "") || (bTextBox.Text != ""))
            {
                if (aTextBox.Text != "")
                {
                    a = int.Parse(aTextBox.Text);
                    flag = 1;
                }
                if (bTextBox.Text != "")
                {
                    b = int.Parse(bTextBox.Text);
                    if (flag == 1)
                    {
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                    }
                }

                this.editKarmaPK.Add(flag);
                this.editKarmaPK.Add(b);        // тот же костыль
                this.editKarmaPK.Add(a);        // тот же костыль
            }
            this.checkKarmaIndicates();
        }
        

    }
}
