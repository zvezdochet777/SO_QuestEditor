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

            if (parent.isRoot(currentDialogID) && (!isAdd))
                NPCReactionText.Text = "Приветствие:";
            if (isAdd)
                mtbPlayerLevel.Text = "0";
            if (!isAdd)
            {
                fillDialogEditForm(currentDialogID);
                this.Text = "Добавление диалога в " + currentDialogID + "";
            }

            this.Text += "   Версия: " + curDialog.version;
        }

        //! Заполняет всю форму данными из CDialog
        void fillDialogEditForm(int dialogID)
        {
            foreach (CDialog dialog in parent.getDialogsWithDialogIDInNodes(dialogID))
                    NPCSaidIs.Text+=(dialog.DialogID.ToString()+":\n"+dialog.Text);
            // заполнение текста речевки и ответа ГГ
            tPlayerText.Text = curDialog.Title.Normalize();
            tNPCReactiontextBox.Text = curDialog.Text;

            foreach (TreeNode active in parent.tree.Nodes.Find("Active",true))
                foreach (TreeNode node in active.Nodes)
                    ToDialogComboBox.Items.Add(node.Text);
            // заполнение текстбокса "Поддиалоги" (Nodes)
            if (curDialog.Nodes.Any())
                foreach (int dialog in curDialog.Nodes)
                {
                    if (tSubDialogsTextBox.Text.Equals(""))
                        tSubDialogsTextBox.Text += dialog.ToString();
                    else
                        tSubDialogsTextBox.Text += ("," + dialog.ToString());
                }
            // какой-то пиздец c кланами и одиночками
            if (curDialog.Precondition.tests.Contains(1))
                cbSameClanOnly.Checked = true;
            if (curDialog.Precondition.tests.Contains(0))
                cbAnyClanOnly.Checked = true;
            if (curDialog.Precondition.tests.Contains(2))
                cbLonerOnly.Checked = true;
            ShowClanOptions();

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
                else if (curDialog.Actions.Event == (int) DialogEvents.trade)
                    toTradeCheckBox.Checked = true;
                else if (curDialog.Actions.Event == (int) DialogEvents.change)
                    changeCheckBox.Checked = true;
                else if (curDialog.Actions.Event == (int) DialogEvents.repair)
                    toRepairCheckBox.Checked = true;
                else if (curDialog.Actions.Event == (int) DialogEvents.complex_repair)
                    toComplexRepairCheckBox.Checked = true;
                else if (curDialog.Actions.Event == (int) DialogEvents.teleport)
                {
                    teleportCheckBox.Checked = true;
                    string key = parent.tpConst.getName(curDialog.Actions.Data);
                    teleportComboBox.SelectedItem = key;
                }
                else if (curDialog.Actions.Event == (int) DialogEvents.barter)
                    barterCheckBox.Checked = true;
                else if (curDialog.Actions.Event == (int) DialogEvents.clan_base)
                    ToClanBaseCheckBox.Checked = true;
                ExitCheckBox.Checked = curDialog.Actions.Exit;

                if (curDialog.Actions.CompleteQuests.Any())
                {
                    CompleteQuestsCheckBox.Checked = true;
                    foreach (int completeQuest in curDialog.Actions.CompleteQuests)
                        addItemToTextBox(completeQuest.ToString(), CompleteQuestsTextBox);
                }

                if (curDialog.Actions.GetQuests.Any())
                {
                    GetQuestsCheckBox.Checked = true;
                    foreach (int getQuest in curDialog.Actions.GetQuests)
                        addItemToTextBox(getQuest.ToString(), GetQuestsTextBox);
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

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveCompletedQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveQuestsOnTest);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveOpenQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveFailedQuests);
            }
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
            CompleteQuestsTextBox.Enabled = CompleteQuestsCheckBox.Checked;
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
            ToClanBaseCheckBox.Enabled = ToClanBaseCheckBox.Checked;
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
            ToClanBaseCheckBox.Enabled = true;
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

        //! Чекбокс телепорта - заполняем возможные локации
        private void teleportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (teleportCheckBox.Checked)
                lockOtherEvent();
            else
                unlockOtherEvent();
            teleportComboBox.Enabled = !teleportComboBox.Enabled;
        }

        //! Нажатие ОК - сохранение всех опций диалога и выход на главную
        private void bEditDialogOk_Click(object sender, EventArgs e)
        {
            int newID;
            Actions actions = new Actions();
            CDialogPrecondition precondition = new CDialogPrecondition();
            NodeCoordinates coord = new NodeCoordinates();
            List<int> nodes = new List<int>();
            //List<string> holder = new List<string>();
            string holder = parent.currentNPC;

            if (!tSubDialogsTextBox.Text.Equals(""))
                foreach (string node in tSubDialogsTextBox.Text.Split(','))
                    nodes.Add(int.Parse(node));

            // заполняем действия диалога - торговля, бартер, починка, телепорт и т.д.
            if (actionsCheckBox.Checked)
            {
                actions.Exit = ExitCheckBox.Checked;
                if (toTradeCheckBox.Checked)
                    actions.Event = (int)DialogEvents.trade;
                else if (changeCheckBox.Checked)
                    actions.Event = (int)DialogEvents.change;
                else if (ToDialogCheckBox.Checked)
                    actions.ToDialog = int.Parse(ToDialogComboBox.SelectedItem.ToString());
                else if (toRepairCheckBox.Checked)
                    actions.Event = (int)DialogEvents.repair;
                else if (teleportCheckBox.Checked && !teleportComboBox.SelectedItem.ToString().Equals(""))
                {
                    actions.Event = (int)DialogEvents.teleport;
                    actions.Data = parent.tpConst.getTtID(teleportComboBox.SelectedItem.ToString());
                }
                else if (toComplexRepairCheckBox.Checked)
                    actions.Event = (int)DialogEvents.complex_repair;
                else if (barterCheckBox.Checked)
                    actions.Event = (int)DialogEvents.barter;
                else if (ToClanBaseCheckBox.Checked)
                    actions.Event = (int)DialogEvents.clan_base;

                if (GetQuestsCheckBox.Checked)
                    foreach (string quest in GetQuestsTextBox.Text.Split(','))
                        actions.GetQuests.Add(int.Parse(quest));
                if (CompleteQuestsCheckBox.Checked)
                    foreach (string quest in CompleteQuestsTextBox.Text.Split(','))
                        actions.CompleteQuests.Add(int.Parse(quest));
            }

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
            if (cbSameClanOnly.Checked)
                precondition.tests.Add(1);
            if (cbAnyClanOnly.Checked)
                precondition.tests.Add(0);
            if (cbLonerOnly.Checked)
                precondition.tests.Add(2);

            precondition.PlayerLevel = int.Parse(mtbPlayerLevel.Text.ToString());
            precondition.Reputation = editPrecondition.Reputation;
            precondition.KarmaPK = editKarmaPK;

            if (isAdd)
            {
                newID = parent.getDialogsNewID();
                parent.addActiveDialog(newID, new CDialog(holder, tPlayerText.Text, tNPCReactiontextBox.Text, precondition, actions, nodes, newID, 1, coord), currentDialogID);
            }
            else
            {
                coord.X = curDialog.coordinates.X;
                coord.Y = curDialog.coordinates.Y;
                coord.RootDialog = curDialog.coordinates.RootDialog;
                coord.Active = curDialog.coordinates.Active;
                int version = curDialog.version;
                if (tPlayerText.Text != curDialog.Title || tNPCReactiontextBox.Text != curDialog.Text)
                    version++;
                parent.replaceDialog(new CDialog(holder, tPlayerText.Text, tNPCReactiontextBox.Text,
                    precondition, actions, nodes, currentDialogID, version , coord), currentDialogID);
            }
            parent.Enabled = true;
            parent.DialogSelected(true);
            this.Close();
        }

    }
}
