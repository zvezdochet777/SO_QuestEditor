using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

            foreach (CItem item in parent.itemConst.getAllItems().Values)
            {
                ((DataGridViewComboBoxColumn)GVItems.Columns[0]).Items.Add(item.getName());
                ((DataGridViewComboBoxColumn)GVNonItems.Columns[0]).Items.Add(item.getName());
            }
            ((DataGridViewComboBoxColumn)GVItems.Columns[0]).Sorted = true;
            ((DataGridViewComboBoxColumn)GVNonItems.Columns[0]).Sorted = true;

            foreach (string category in parent.itemCategories.getAllItems().Values)
            {
                cbCategory.Items.Add(category);
                cbNonCategory.Items.Add(category);
            }

            FillActionsComboBox();
            FillTutorialComboBox();
            if (parent.isRoot(currentDialogID) && (!isAdd))
                lReactionNPC.Text = "Приветствие:";
            cbRadioNode.SelectedIndex = 0;
            
            if (!isAdd)
            {
                fillDialogEditForm(currentDialogID);
                this.Text = "Редактирование диалога ID = " + currentDialogID + "";
            }

            this.Text += "   Версия: " + curDialog.version;
        }
        void fillClanOptions(string options)
        {
            List<int> list = new List<int>();
            if (options == "")
                return;
            switch(options[0])
                
            {
                case '&':
                    radioButtonAND.Checked = true;
                    radioButtonOR.Checked = false;
                    break;
                case '|':
                    radioButtonAND.Checked = false;
                    radioButtonOR.Checked = true;
                    break;
                default:
                    options = "&," + options;
                    break;

            }
            options = options.Substring(2);
            foreach (string node in options.Split(','))
                list.Add(int.Parse(node)); ;
            // какой-то пиздец c кланами и одиночками
            if (list.Contains(1))
                cbSameClanOnly.Checked = true;
            if (list.Contains(2))
                cbNotSameClanOnly.Checked = true;
            if (list.Contains(3))
                cbEnemy.Checked = true;
            if (list.Contains(4))
                cbNotEnemy.Checked = true;
            if (list.Contains(5))
                cbPeaceTime.Checked = true;
            if (list.Contains(6))
                cbWarTime.Checked = true;
            if (list.Contains(7))
                cbAnyClanOnly.Checked = true;
            if (list.Contains(8))
                cbLonerOnly.Checked = true;
            if (list.Contains(9))
                cbSecurExst.Checked = true;
            if (list.Contains(10))
                cbSecurNotExst.Checked = true;
            if (list.Contains(11))
                cbAllyance.Checked = true;
        }
        //! Заполняет всю форму данными из CDialog
        void fillDialogEditForm(int dialogID)
        {
            cbAutoNode.Checked = curDialog.isAutoNode;
            autoDefaultNode.Text = curDialog.defaultNode;
            foreach (CDialog dialog in parent.getDialogsWithDialogIDInNodes(dialogID))
                    NPCSaidIs.Text+=(dialog.DialogID.ToString()+":\n"+dialog.Text);
            // заполнение текста речевки и ответа ГГ
            tPlayerText.Text = curDialog.Title.Normalize();
            tReactionNPC.Text = curDialog.Text;

            foreach (TreeNode active in parent.tree.Nodes.Find("Active",true))
                foreach (TreeNode node in active.Nodes)
                    ToDialogComboBox.Items.Add(node.Text);
            // заполнение текстбокса "Поддиалоги" (Nodes)
            autoDefaultNode.Items.Clear();
            if (curDialog.Nodes.Any())
                foreach (int dialog in curDialog.Nodes)
                {
                    if (tNodes.Text.Equals(""))
                        tNodes.Text += dialog.ToString();
                    else
                        tNodes.Text += ("," + dialog.ToString());
                    autoDefaultNode.Items.Add(dialog.ToString());

                }
            if (curDialog.CheckNodes.Any())
                foreach (int node in curDialog.CheckNodes)
                {
                    if (tCheckNodes.Text.Equals(""))
                        tCheckNodes.Text += node.ToString();
                    else
                        tCheckNodes.Text += ("," + node.ToString());
                }
            this.fillClanOptions(curDialog.Precondition.clanOptions);

            if (curDialog.Actions.Any())
            {
                actionsCheckBox.Checked = true;
                ActionsComboBox.SelectedValue = curDialog.Actions.Event.Value;
                cbExit.Checked = curDialog.Actions.Exit;

                if (ActionsComboBox.Text == "Телепорт")
                {
                    string key = parent.tpConst.getName(curDialog.Actions.Data);
                    teleportComboBox.SelectedItem = key;
                }
                if (ActionsComboBox.Text == "Торговля" || ActionsComboBox.Text == "Бартер (обмен)")
                {
                    tbAvatarGoTo.Text = curDialog.Actions.Data;
                }
                if (ActionsComboBox.Text == "Команда НПЦ")
                {
                    string key = parent.cmConst.getName(curDialog.Actions.Data);
                    commandsComboBox.SelectedItem = key;
                }
                if ((ActionsComboBox.Text == "Починка") || (ActionsComboBox.Text == "Комплексная починка"))
                {
                    string key = parent.rpConst.getName(curDialog.Actions.Data);
                    commandsComboBox.SelectedItem = key;
                }
                if (ActionsComboBox.Text == "Перейти в точку")
                    tbAvatarGoTo.Text = curDialog.Actions.Data;
                
                
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
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfCompletedQuests == '|')
                    cbShouldntHaveCompletedQuests.Checked = true;
                else
                    cbShouldntHaveCompletedQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveOpenQuests);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfOpenedQuests == '|')
                    cbShouldntHaveOpenQuests.Checked = true;
                else
                    cbShouldntHaveOpenQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfFailQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveFailQuests);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfFailQuests == '|')
                    cbShouldntHaveFailQuests.Checked = true;
                else
                    cbShouldntHaveFailQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tShouldntHaveQuestsOnTest);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfOnTestQuest == '|')
                    cbShouldntHaveQuestsOnTest.Checked = true;
                else
                    cbShouldntHaveQuestsOnTest.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfCounters)
                    addItemToTextBox(quest.ToString(), tShouldntHaveCounters);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfCounterss == '|')
                    cbShouldntHaveCounters.Checked = true;
                else
                    cbShouldntHaveCounters.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfRepeat)
                    addItemToTextBox(quest.ToString(), tShouldntHaveRepeat);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfRepeat == '|')
                    cbShouldntHaveRepeat.Checked = true;
                else
                    cbShouldntHaveRepeat.Checked = false;

                addItemToTextBox(curDialog.Precondition.ListOfMustNoQuests.ListOfMassQuests, tShouldntHaveMassQuests);
                if (curDialog.Precondition.ListOfMustNoQuests.conditionOfMassQuests == '|')
                    cbShouldntHaveMassQuests.Checked = true;
                else
                    cbShouldntHaveMassQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveCompletedQuests);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfCompletedQuests == '|')
                    cbMustHaveCompletedQuests.Checked = true;
                else
                    cbMustHaveCompletedQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveQuestsOnTest);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfOnTestQuest == '|')
                    cbMustHaveQuestsOnTest.Checked = true;
                else
                    cbMustHaveQuestsOnTest.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveOpenQuests);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfOpenedQuests == '|')
                    cbMustHaveOpenQuests.Checked = true;
                else
                    cbMustHaveOpenQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfFailQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveFailQuests);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfFailQuests == '|')
                    cbMustHaveFailQuests.Checked = true;
                else
                    cbMustHaveFailQuests.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCounters)
                    addItemToTextBox(quest.ToString(), tMustHaveCounters);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfCounterss == '|')
                    cbMustHaveCounters.Checked = true;
                else
                    cbMustHaveCounters.Checked = false;

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfRepeat)
                    addItemToTextBox(quest.ToString(), tMustHaveRepeat);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfRepeat == '|')
                    cbMustHaveRepeat.Checked = true;
                else
                    cbMustHaveRepeat.Checked = false;

                addItemToTextBox(curDialog.Precondition.ListOfNecessaryQuests.ListOfMassQuests, tMustHaveMassQuests);
                if (curDialog.Precondition.ListOfNecessaryQuests.conditionOfMassQuests == '|')
                    cbMustHaveMassQuests.Checked = true;
                else
                    cbMustHaveMassQuests.Checked = false;
            }
            if (curDialog.DebugData != "") debugTextBox.Text = curDialog.DebugData;
                   
            calcSymbolMaxAnswer();


            if (curDialog.Precondition.Any())
            {
                editPrecondition = curDialog.Precondition;
            }
            if (curDialog.Precondition.KarmaPK.Any())
            {
                editKarmaPK = curDialog.Precondition.KarmaPK;
                checkKarmaIndicates();
            }
            if (curDialog.Precondition.Skills.Any())
                editPrecondition.Skills = curDialog.Precondition.Skills;

            cbRadioNode.SelectedIndex = (int)curDialog.Precondition.radioAvailable;

            cbForDev.Checked = curDialog.Precondition.forDev;
            cbHidden.Checked = curDialog.Precondition.hidden;
            this.initReputationTab();
            this.initKarmaPKTab();
            this.initEffectsTab();
            this.initLevelTab();
            this.initSkillsTab();
            this.initActionTab();
            this.initItemsTab();
            this.initTransportTab();
            this.initTutorialTab();
            checkClanOptionsIndicator();
        }

        //! Антиговнокод-функция, добавление номера квеста в текстбокс
        private void addItemToTextBox(string item, MaskedTextBox textBox)
        {
            if (textBox.Text.Equals(""))
                textBox.Text += item;
            else
                textBox.Text += ("," + item);
        }

        private void FillTutorialComboBox()
        {
            cbTutorialPhase.Items.Clear();
            cbTutorialPhase.DataSource = parent.tutorialPhases.getAllNames();
            cbTutorialPhase.SelectedItem = null;
        }

        private void FillActionsComboBox()
        {
            ActionsComboBox.Items.Clear();            
            ActionsComboBox.DataSource = parent.dialogEvents.GetFullList();
            ActionsComboBox.DisplayMember = "Display";
            ActionsComboBox.ValueMember = "Value";
        }

        private void checkClanOptionsIndicator()
        {
            if (checkClanOptions())
            {
                pictureClan.Visible = true;
            }
            else pictureClan.Visible = false;
        }
        private bool checkClanOptions()
        {
            if (cbSameClanOnly.Checked || cbAnyClanOnly.Checked || cbLonerOnly.Checked || cbWarTime.Checked ||
                cbEnemy.Checked || cbNotEnemy.Checked || cbPeaceTime.Checked || cbNotSameClanOnly.Checked ||
                cbSecurExst.Checked || cbSecurNotExst.Checked || cbAllyance.Checked)
            {
                return true;
            }
            else return false;
             
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
        //! Задать цвет кнопки репутации, если репутация задана
        public void checkReputationIndicates()
        {
            pictureReputation.Visible = editPrecondition.Reputation.Any() || editPrecondition.NPCReputation.Any();
        }
        //! Задать цвет кнопки кармы, если карма задана
        public void checkKarmaIndicates()
        {
            if (editKarmaPK.Any())
                pictureKarma.Visible = true;
            else
                pictureKarma.Visible = false;
        }

        //! Задать цвет кнопки Клановой, если карма задана
        public void checkClanIndicates()
        {
            if (editKarmaPK.Any())
                pictureKarma.Visible = true;
            else
                pictureKarma.Visible = false;
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
            commandsComboBox.Visible = (SelectedValue == 19) || (SelectedValue == 4) || (SelectedValue == 6);
            tbAvatarGoTo.Visible = (SelectedValue == 20) || (SelectedValue == 1) || (SelectedValue == 7);
            if (SelectedValue == 19)
            {
                commandsComboBox.Items.Clear();
                foreach (string key in parent.cmConst.getKeys())
                    commandsComboBox.Items.Add(key);
            }
            if ((SelectedValue == 4)|| (SelectedValue == 6))
            {
                commandsComboBox.Items.Clear();
                foreach (string key in parent.rpConst.getKeys())
                    commandsComboBox.Items.Add(key);
            }

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
            else if (ActionsComboBox.Text == "Команда охраннику")
            {
                if (commandsComboBox.Text.Equals(""))
                {
                    MessageBox.Show("Действие невозможно: не задано действие охраннику.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            List<int> check_nodes = new List<int>();
            //List<string> holder = new List<string>();
            string holder = parent.GetCurrentHolder();

            if (!tNodes.Text.Equals(""))
                foreach (string node in tNodes.Text.Split(','))
                {
                    int node_id = int.Parse(node);
                    if (!this.parent.getAllDialogsIDonCurrentNPC().Contains(node_id))
                    {
                        MessageBox.Show("Действие невозможно: диалог №"+ node_id.ToString() +" не существует у этого персонажа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (this.parent.getDialogOnDialogID(node_id).Nodes.Contains(this.currentDialogID))
                    {
                        MessageBox.Show("Действие невозможно: диалог №" + node_id.ToString() + " уже сам ссылается на текущий диалог.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (node_id == this.currentDialogID)
                    {
                        MessageBox.Show("Действие невозможно: диалог №" + node_id.ToString() + " ссылается сам на себя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nodes.Add(int.Parse(node));
                }
                    
            if (!tCheckNodes.Text.Equals(""))
                foreach (string node in tCheckNodes.Text.Split(','))
                    check_nodes.Add(int.Parse(node));

            if (!CheckConditions())
                return;

            if(!this.checkReputation()) 
                return;
            if (!this.checkSkills())
                return;
            this.checkKarmaPK();
            this.checkEffects();
            // заполняем действия диалога - торговля, бартер, починка, телепорт и т.д.
            if (actionsCheckBox.Checked)
            {
                actions.Exit = cbExit.Checked;
                actions.Event = parent.dialogEvents.GetEventFromID((int) ActionsComboBox.SelectedValue);
                if (actions.Event.Display == "Телепорт")
                    actions.Data = parent.tpConst.getTtID(teleportComboBox.SelectedItem.ToString());
                if (actions.Event.Display == "Переход к диалогу")
                    actions.ToDialog = int.Parse(ToDialogComboBox.Text.ToString());
                if (actions.Event.Display == "Команда НПЦ")
                    actions.Data = parent.cmConst.getTtID(commandsComboBox.SelectedItem.ToString());
                if ((actions.Event.Display == "Починка") || (actions.Event.Display == "Комплексная починка"))
                    actions.Data = parent.rpConst.getTtID(commandsComboBox.SelectedItem.ToString());
                if (actions.Event.Display == "Перейти в точку" || actions.Event.Display == "Бартер (обмен)" || actions.Event.Display == "Торговля")
                    actions.Data = tbAvatarGoTo.Text;
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

            if (cbCamera.Checked && tbCamera.Text.Any())
                actions.actionCameraSmoothly = cbCameraSmoothly.Checked;
                actions.actionCamera = tbCamera.Text;
            if (cbAnimationPlayer.Checked && tbAnimationPlayer.Text.Any())
                actions.actionAnimationPlayer = tbAnimationPlayer.Text;
            if (cbAnimationNPC.Checked && tbAnimationNPC.Text.Any())
                actions.actionAnimationNPC = tbAnimationNPC.Text;
            if (cbActionNPC.Checked && tbActionNPC.Text.Any())
            {
                actions.actionActionNPC = int.Parse(parent.npcActions.getTtID(tbActionNPC.Text));
                actions.actionAdditionalActionNPC = tbAdditionalAction.Text;
            }
               
          
            if (cbAvatarPoint.Checked && tbAvatarPoint.Text.Any())
                actions.actionAvatarPoint = tbAvatarPoint.Text;
            if (cbPlaySonund.Checked && tbPlaySonund.Text.Any())
                actions.actionPlaySound = tbPlaySonund.Text;
            if (nupChangeMoney.Value != 0)
            {
                actions.changeMoney = Convert.ToInt32(nupChangeMoney.Value);
                if (tbChangeMoneyFailNode.Text.Any())
                    int.TryParse(tbChangeMoneyFailNode.Text, out actions.changeMoneyFailNode);
            }


            // заполняем условия появления диалога - открытые и закрытые квесты и т.д.
            if (!tMustHaveOpenQuests.Text.Equals(""))
            {
                if (cbMustHaveOpenQuests.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfOpenedQuests = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfOpenedQuests = '&';
                foreach (string quest in tMustHaveOpenQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfOpenedQuests.Add(int.Parse(quest));
            }
            if (!tMustHaveFailQuests.Text.Equals(""))
            {
                if (cbMustHaveFailQuests.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfFailQuests = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfFailQuests = '&';
                foreach (string quest in tMustHaveFailQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfFailQuests.Add(int.Parse(quest));
            }
            if (!tMustHaveQuestsOnTest.Text.Equals(""))
            {
                if (cbMustHaveQuestsOnTest.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfOnTestQuest = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfOnTestQuest = '&';
                foreach (string quest in tMustHaveQuestsOnTest.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfOnTestQuests.Add(int.Parse(quest));
            }
            if (!tMustHaveCompletedQuests.Text.Equals(""))
            {
                if (cbMustHaveCompletedQuests.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfCompletedQuests = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfCompletedQuests = '&';
                foreach (string quest in tMustHaveCompletedQuests.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfCompletedQuests.Add(int.Parse(quest));
            }
            if (!tMustHaveMassQuests.Text.Equals(""))
            {
                if (cbMustHaveMassQuests.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfMassQuests = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfMassQuests = '&';
                precondition.ListOfNecessaryQuests.ListOfMassQuests = tMustHaveMassQuests.Text;
            }
            if (!tMustHaveCounters.Text.Equals(""))
            {
                if (cbMustHaveCounters.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfCounterss = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfCounterss = '&';
                foreach (string quest in tMustHaveCounters.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfCounters.Add(int.Parse(quest));
            }
            if (!tMustHaveRepeat.Text.Equals(""))
            {
                if (cbMustHaveRepeat.Checked)
                    precondition.ListOfNecessaryQuests.conditionOfRepeat = '|';
                else
                    precondition.ListOfNecessaryQuests.conditionOfRepeat = '&';
                foreach (string quest in tMustHaveRepeat.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfRepeat.Add(int.Parse(quest));
            }

            if (!tShouldntHaveOpenQuests.Text.Equals(""))
            {
                if (cbShouldntHaveOpenQuests.Checked)
                    precondition.ListOfMustNoQuests.conditionOfOpenedQuests = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfOpenedQuests = '&';
                foreach (string quest in tShouldntHaveOpenQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfOpenedQuests.Add(int.Parse(quest));
            }
            if (!tShouldntHaveFailQuests.Text.Equals(""))
            {
                if (cbShouldntHaveFailQuests.Checked)
                    precondition.ListOfMustNoQuests.conditionOfFailQuests = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfFailQuests = '&';
                foreach (string quest in tShouldntHaveFailQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfFailQuests.Add(int.Parse(quest));
            }
            if (!tShouldntHaveQuestsOnTest.Text.Equals(""))
            {
                if (cbShouldntHaveQuestsOnTest.Checked)
                    precondition.ListOfMustNoQuests.conditionOfOnTestQuest = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfOnTestQuest = '&';
                foreach (string quest in tShouldntHaveQuestsOnTest.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfOnTestQuests.Add(int.Parse(quest));
            }
            if (!tShouldntHaveCompletedQuests.Text.Equals(""))
            {
                if (cbShouldntHaveCompletedQuests.Checked)
                    precondition.ListOfMustNoQuests.conditionOfCompletedQuests = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfCompletedQuests = '&';
                foreach (string quest in tShouldntHaveCompletedQuests.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfCompletedQuests.Add(int.Parse(quest));
            }
            if (!tShouldntHaveMassQuests.Text.Equals(""))
            {
                if (cbShouldntHaveMassQuests.Checked)
                    precondition.ListOfMustNoQuests.conditionOfMassQuests = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfMassQuests = '&';
                precondition.ListOfMustNoQuests.ListOfMassQuests = tShouldntHaveMassQuests.Text;
            }
            if (!tShouldntHaveCounters.Text.Equals(""))
            {
                if (cbShouldntHaveCounters.Checked)
                    precondition.ListOfMustNoQuests.conditionOfCounterss = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfCounterss = '&';
                foreach (string quest in tShouldntHaveCounters.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfCounters.Add(int.Parse(quest));
            }
            if (!tShouldntHaveRepeat.Text.Equals(""))
            {
                if (cbShouldntHaveRepeat.Checked)
                    precondition.ListOfMustNoQuests.conditionOfRepeat = '|';
                else
                    precondition.ListOfMustNoQuests.conditionOfRepeat = '&';
                foreach (string quest in tShouldntHaveRepeat.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfRepeat.Add(int.Parse(quest));
            }

            precondition.transport.inTransportList = cbTransportInList.Checked;
            precondition.transport.notInTransportList = cbNotInTransportList.Checked;
            precondition.transport.inBoatList = cbInBoatList.Checked;
            precondition.transport.notInBoatList = cbNotInBoatList.Checked;
            precondition.transport.boatInTransit = cbIsBoatInTransit.Checked;
            precondition.transport.boatStopped = cbIsBoatStopped.Checked;

            if (cbIsBoatInTransit.Checked && cbIsBoatStopped.Checked)
            {
                MessageBox.Show("Перевозки. Некорректные правила для лодок");
                 return;
            }

            if (cbIsBoatInTransit.Checked || cbIsBoatStopped.Checked)
            {
                if (!boatName.Text.Any())
                {
                    MessageBox.Show("Перевозки. Не указано имя лодочника");
                    return;
                }
                precondition.transport.boatName = boatName.Text;
            }

                if (cbTutorialPhase.SelectedItem != null)
                precondition.tutorialPhase = this.parent.tutorialPhases.getIDByName(cbTutorialPhase.SelectedItem.ToString());

            if (checkClanOptions())
            {
                if (radioButtonAND.Checked)
                    precondition.clanOptions = "&";
                if (radioButtonOR.Checked)
                    precondition.clanOptions = "|";
                if (cbSameClanOnly.Checked)
                    precondition.clanOptions += ",1";
                if (cbNotSameClanOnly.Checked)
                    precondition.clanOptions += ",2";
                if (cbEnemy.Checked)
                    precondition.clanOptions += ",3";
                if (cbNotEnemy.Checked)
                    precondition.clanOptions += ",4";
                if (cbPeaceTime.Checked)
                    precondition.clanOptions += ",5";
                if (cbWarTime.Checked)
                    precondition.clanOptions += ",6";
                if (cbAnyClanOnly.Checked)
                    precondition.clanOptions += ",7";
                if (cbLonerOnly.Checked)
                    precondition.clanOptions += ",8";
                if (cbSecurExst.Checked)
                    precondition.clanOptions += ",9";
                if (cbSecurNotExst.Checked)
                    precondition.clanOptions += ",10";
                if (cbAllyance.Checked)
                    precondition.clanOptions += ",11";
            }             
            if (checkLevel())
            {
                precondition.PlayerLevel = mtbPlayerLevelMin.Text + ":" + mtbPlayerLevelMax.Text;
                precondition.playerCombatLvl = tbCombatLvlMin.Text + ":" + tbCombatLvlMax.Text;
                precondition.playerSurvLvl = tbSurvLvlMin.Text + ":" + tbSurvLvlMax.Text;
                precondition.playerOtherLvl = tbOtherLvlMin.Text + ":" + tbOtherLvlMax.Text;
            }
            precondition.Reputation = editPrecondition.Reputation;
            precondition.NPCReputation = editPrecondition.NPCReputation;
            precondition.KarmaPK = editKarmaPK;
            precondition.NecessaryEffects = editPrecondition.NecessaryEffects;
            precondition.MustNoEffects = editPrecondition.MustNoEffects;
            precondition.Skills = editPrecondition.Skills;
            precondition.forDev = cbForDev.Checked;
            precondition.hidden = cbHidden.Checked;

            precondition.items.is_or = rbItemsOr.Checked;
            precondition.itemsNone.is_or = rbNonItemsOr.Checked;

            if (rbCategory.Checked && cbCategory.SelectedIndex != -1)
            {
                precondition.items.itemCategory = parent.itemCategories.getID(cbCategory.Text);
                precondition.itemsNone.itemCategory = parent.itemCategories.getID(cbNonCategory.Text);
            }
            else
            {
                precondition.items.itemCategory = -1;
                precondition.itemsNone.itemCategory = -1;


                List<object[]> obj = this.getItemsDataGrid(GVItems);
                foreach (object[] tmp in obj)
                {
                    QuestItem item = new QuestItem();
                    item.itemType = Convert.ToInt32(tmp[0]);
                    item.count = Convert.ToInt32(tmp[1]);
                    item.attribute = (ItemAttribute)Convert.ToInt32(tmp[2]);
                    item.condition = Convert.ToSingle(tmp[3]);

                    precondition.items.items.Add(item);
                }
                obj = this.getItemsDataGrid(GVNonItems);
                foreach (object[] tmp in obj)
                {
                    QuestItem item = new QuestItem();
   
                    item.itemType = Convert.ToInt32(tmp[0]);
                    item.count = Convert.ToInt32(tmp[1]);
                    item.attribute = (ItemAttribute)Convert.ToInt32(tmp[2]);
                    item.condition = Convert.ToSingle(tmp[3]);
                    precondition.itemsNone.items.Add(item);
                }
            }

            precondition.radioAvailable = (RadioAvalible)cbRadioNode.SelectedIndex;

            if (debugTextBox.Text != "")
                DebugData = debugTextBox.Text;

            if (isAdd)
            {
                newID = CDialogs.getDialogsNewID();
                CDialog dialog = new CDialog(holder, tPlayerText.Text, tReactionNPC.Text, precondition, actions, nodes, check_nodes, newID, 1, coord, DebugData, cbAutoNode.Checked, autoDefaultNode.Text);
                parent.addActiveDialog(newID, dialog, currentDialogID);
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
                    precondition, actions, nodes, check_nodes, currentDialogID, version, coord, DebugData, cbAutoNode.Checked, autoDefaultNode.Text), currentDialogID);
            }
            parent.Enabled = true;
            parent.DialogSelected(true);
            parent.startEmulator(currentDialogID);
            this.Close();
        }


        private void setItemsInDataGrid(List<QuestItem> items, DataGridView dg)
        {
            foreach (QuestItem item in items)
            {
                int item_type = item.itemType;
                string item_name = parent.itemConst.getItemName(item_type);
                string item_attr;
                switch (item.attribute)
                {
                    case ItemAttribute.QUEST: item_attr = "Квестовый"; break;
                    case ItemAttribute.USE: item_attr = "Использовать"; break;
                    default: item_attr = "Обычный"; break;
                }
                int count = item.count;
                string cond;
                try
                {
                    cond = item.condition.ToString("G6", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    cond = "0";
                }
                object[] row = { item_name, item_attr, count, cond };
                dg.Rows.Add(row);
            }
        }

        private List<object[]> getItemsDataGrid(DataGridView dg)
        {
            List<object[]> result = new List<object[]>();
            foreach (DataGridViewRow row in dg.Rows)
            {
                string typeName = row.Cells[dg.Name + "_itemType"].FormattedValue.ToString();
                if (!typeName.Equals(""))
                {
                    int quantity = 1;
                    float cond = 0;

                    float.TryParse(row.Cells[dg.Name + "_itemCond"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out cond);

                    if ((int.TryParse(row.Cells[dg.Name + "_itemQuantity"].FormattedValue.ToString(), out quantity)) && (quantity >= 1))
                    {
                        int typeID = parent.itemConst.getIDOnName(typeName);
                        string attrName = row.Cells[dg.Name + "_itemAttr"].FormattedValue.ToString();
                        int attr;
                        switch (attrName)
                        {
                            case "Квестовый": attr = 1; break;
                            case "Использовать": attr = 2; break;
                            default: attr = 0; break;
                        }

                        object[] obj = { typeID, quantity, attr, cond };
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        private void initSkillsTab()
        {
            SkillConstants skills = this.parent.skills;
            int id = 0;
            foreach (string skill_name in skills.getKeys())
            {
                string a = "";
                string b = "";
                string skill_id = skills.getTtID(skill_name);
                DialogSkill skill = this.editPrecondition.Skills.getSkillValByName(skill_id);
                if (skill != null)
                {
                    a = skill.min;
                    b = skill.max;
                }
                object[] row = { id, skill_name, a, b };
                dataSkill.Rows.Add(row);
                id++;
            }
            this.checkSkills();
        }

        private bool checkSkills()
        {
            this.editPrecondition.Skills.Clear();
            foreach (DataGridViewRow row in dataSkill.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    string skill_name = parent.skills.getTtID(row.Cells[1].FormattedValue.ToString());
                    string stringA = row.Cells[2].FormattedValue.ToString().Replace('.', ',');
                    string stringB = row.Cells[3].FormattedValue.ToString().Replace('.', ',');

                    int intA;
                    int intB;
                    if ((stringA != "") && (!int.TryParse(stringA, out intA)))
                    {
                        MessageBox.Show("Неправильное условие по навыкам!");
                        return false;
                    }
                    if ((stringB != "") && (!int.TryParse(stringB, out intB)))
                    {
                        MessageBox.Show("Неправильное условие по навыкам!");
                        return false;
                    }


                    if ((stringA == "") && (stringB == ""))
                    {
                        continue;
                    }
                    this.editPrecondition.Skills.Add(skill_name, stringA, stringB);
                }
            }
            this.checkSkillIndicates();
            return true;
        }

        private void checkSkillIndicates()
        {
            if (editPrecondition.Skills.Any())
                pictureSkill.Visible = true;
            else
                pictureSkill.Visible = false;
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
            foreach(KeyValuePair<string, List<double>> pair in this.editPrecondition.NPCReputation)
            {
                string name = pair.Key;
                string a = "";
                string b = "";
                if (this.editPrecondition.NPCReputation[pair.Key].Count == 3)         // костыль для старой версии, выжечт огнем позже
                {
                    double type = this.editPrecondition.NPCReputation[pair.Key][0];
                    if (type == 0 || (type == 1))
                        a = this.editPrecondition.NPCReputation[pair.Key][1].ToString();
                    if (type == 0 || (type == 2))
                        b = this.editPrecondition.NPCReputation[pair.Key][2].ToString();
                }
                else if (this.editPrecondition.NPCReputation[pair.Key].Count == 2)
                {
                    if (this.editPrecondition.NPCReputation[pair.Key][0] != double.NegativeInfinity)
                        a = this.editPrecondition.NPCReputation[pair.Key][0].ToString();
                    if (this.editPrecondition.NPCReputation[pair.Key][1] != double.PositiveInfinity)
                        b = this.editPrecondition.NPCReputation[pair.Key][1].ToString();
                }
                object[] row = { "", name, a, b };
                dataReputation.Rows.Add(row);
            }
            this.checkReputation();
        }

        private bool checkReputation()
        {
            this.editPrecondition.Reputation.Clear();
            this.editPrecondition.NPCReputation.Clear();
            foreach (DataGridViewRow row in dataReputation.Rows)
            {
                    string fractionName = row.Cells[1].FormattedValue.ToString();
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

                        int fractionID = -1;
                        if (int.TryParse(row.Cells[0].FormattedValue.ToString(), out fractionID))
                        {
                            this.editPrecondition.Reputation.Add(fractionID, new List<double>() { doubleA, doubleB });
                        }
                        else
                        {
                            this.editPrecondition.NPCReputation.Add(fractionName.Trim(), new List<double>() { doubleA, doubleB });
                        }

                        
                    }
            }
            this.checkReputationIndicates();
            return true;
        }

        private bool checkEffects()
        {
            this.editPrecondition.MustNoEffects = new List<DialogEffect>();
            this.editPrecondition.NecessaryEffects = new List<DialogEffect>();
            foreach (DataGridViewRow row in dataGridNotEffects.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    string typeName = row.Cells[0].FormattedValue.ToString();
                    int id = parent.effects.getIDOnDescription(typeName);
                    string stack_from = row.Cells[1].FormattedValue.ToString();
                    string stack_before = row.Cells[2].FormattedValue.ToString();
                    this.editPrecondition.MustNoEffects.Add(new DialogEffect(id, stack_from, stack_before));
                }
            }

            foreach (DataGridViewRow row in dataGridEffects.Rows)
            {
                if (row.Cells[0].FormattedValue.ToString() != "")
                {
                    string typeName = row.Cells[0].FormattedValue.ToString();
                    int id = parent.effects.getIDOnDescription(typeName);
                    string stack_from = row.Cells[1].FormattedValue.ToString();
                    string stack_before = row.Cells[2].FormattedValue.ToString();
                    this.editPrecondition.NecessaryEffects.Add(new DialogEffect(id, stack_from, stack_before));
                }
            }

            this.checkEffectsIndicates();
            return true;
        }

        private void initTransportTab()
        {
            this.cbTransportInList.Checked = this.editPrecondition.transport.inTransportList;
            this.cbNotInTransportList.Checked = this.editPrecondition.transport.notInTransportList;

            this.cbInBoatList.Checked = this.editPrecondition.transport.inBoatList;
            this.cbNotInBoatList.Checked = this.editPrecondition.transport.notInBoatList;
            this.cbIsBoatInTransit.Checked = this.editPrecondition.transport.boatInTransit;
            this.cbIsBoatStopped.Checked = this.editPrecondition.transport.boatStopped;
            this.boatName.Text = this.editPrecondition.transport.boatName;

            this.checkTransportIndicates();
        }

        private void initTutorialTab()
        {
            if (this.curDialog.Precondition.tutorialPhase != -1)
            {
                this.cbTutorialPhase.SelectedItem = this.parent.tutorialPhases.getNameByID(this.curDialog.Precondition.tutorialPhase);
            }
            else { this.cbTutorialPhase.SelectedItem = null; }
            this.checkTutorialIndicates();
        }

        private void initItemsTab()
        {

            if (editPrecondition.items.is_or)
                rbItemsOr.Checked = true;
            if (editPrecondition.itemsNone.is_or)
                rbNonItemsOr.Checked = true;

            if ((this.editPrecondition.items.itemCategory != -1) || (this.editPrecondition.itemsNone.itemCategory != -1))
            {
                this.rbCategory.Checked = true;
                this.cbCategory.SelectedItem = parent.itemCategories.getNameOnID(this.editPrecondition.items.itemCategory);
                this.cbNonCategory.SelectedItem = parent.itemCategories.getNameOnID(this.editPrecondition.itemsNone.itemCategory);
            }
            else if (this.editPrecondition.items.Any() || this.editPrecondition.itemsNone.Any())
            {
                this.rbItems.Checked = true;
                setItemsInDataGrid(this.editPrecondition.items.items, GVItems);
                setItemsInDataGrid(this.editPrecondition.itemsNone.items, GVNonItems);
            }
            checkItemsIndicates();

        }
        private void initEffectsTab()
        {

            ((DataGridViewComboBoxColumn)dataGridEffects.Columns[0]).Items.Add("");
            ((DataGridViewComboBoxColumn)dataGridNotEffects.Columns[0]).Items.Add("");

            foreach (string effect_name in parent.effects.getAllDescriptions())
            {
                ((DataGridViewComboBoxColumn)dataGridEffects.Columns[0]).Items.Add(effect_name);
                ((DataGridViewComboBoxColumn)dataGridNotEffects.Columns[0]).Items.Add(effect_name);
            }
            ((DataGridViewComboBoxColumn)dataGridEffects.Columns[0]).Sorted = true;
            ((DataGridViewComboBoxColumn)dataGridNotEffects.Columns[0]).Sorted = true;

            foreach (DialogEffect effect in this.editPrecondition.MustNoEffects)
            {
                string name = parent.effects.getDescriptionOnID(effect.getID());
                string stack_from = effect.getStackFrom().ToString();
                string stack_before = effect.getStackBefore().ToString();
                object[] row = { name, stack_from, stack_before };
                dataGridNotEffects.Rows.Add(row);
            }

            foreach (DialogEffect effect in this.editPrecondition.NecessaryEffects)
            {
                string name = parent.effects.getDescriptionOnID(effect.getID());
                string stack_from = effect.getStackFrom().ToString();
                string stack_before = effect.getStackBefore().ToString();
                object[] row = { name, stack_from, stack_before };
                dataGridEffects.Rows.Add(row);
            }
            this.checkEffectsIndicates();

        }

        private void initActionTab()
        {
            if (curDialog.Actions.actionCamera.Any())
            {
                cbCamera.Checked = true;
                tbCamera.Text = curDialog.Actions.actionCamera;
                if (curDialog.Actions.actionCameraSmoothly)
                {
                    cbCameraSmoothly.Checked = curDialog.Actions.actionCameraSmoothly;
                }
            }

            tbAnimationPlayer.Items.Clear();
            tbAnimationNPC.Items.Clear();
            tbActionNPC.Items.Clear();
            
            foreach (string key in parent.avAmin.getKeys())
            {
                tbAnimationPlayer.Items.Add(key);
                tbAnimationNPC.Items.Add(key);
            }
            foreach (string key in parent.npcActions.getKeys())
            {
                tbActionNPC.Items.Add(key);
            }
            if (curDialog.Actions.actionActionNPC != 0)
            {
                tbActionNPC.SelectedItem = parent.npcActions.getName(curDialog.Actions.actionActionNPC.ToString());
                tbAdditionalAction.Text = curDialog.Actions.actionAdditionalActionNPC;
            }

            if (curDialog.Actions.actionAnimationPlayer.Any())
            {
                cbAnimationPlayer.Checked = true;
                tbAnimationPlayer.Text = curDialog.Actions.actionAnimationPlayer;
            }
            if (curDialog.Actions.actionAnimationNPC.Any())
            {
                cbAnimationNPC.Checked = true;
                tbAnimationNPC.Text = curDialog.Actions.actionAnimationNPC;
            }
            cbActionNPC.Checked = curDialog.Actions.actionActionNPC != 0;
            tbAdditionalAction.Text = curDialog.Actions.actionAdditionalActionNPC;
            if (curDialog.Actions.actionAvatarPoint.Any())
            {
                cbAvatarPoint.Checked = true;
                tbAvatarPoint.Text = curDialog.Actions.actionAvatarPoint;
            }
            foreach (string key in parent.listSouds.getKeys())
            {
                tbPlaySonund.Items.Add(key);
            }
            if (curDialog.Actions.actionPlaySound.Any())
            {
                cbPlaySonund.Checked = true;
                tbPlaySonund.Text = curDialog.Actions.actionPlaySound;
            }
            nupChangeMoney.Value = Convert.ToDecimal(curDialog.Actions.changeMoney);
            tbChangeMoneyFailNode.Text = "";
            if (curDialog.Actions.changeMoneyFailNode != 0)
            {
                tbChangeMoneyFailNode.Text = curDialog.Actions.changeMoneyFailNode.ToString();
            }
            this.checkActionIndicates();
        }

        private void initLevelTab()
        {
            
            string[] tmp;
            tmp = curDialog.Precondition.PlayerLevel.Split(':');
            if (tmp.Any())
            {
                if (tmp.Length > 1)
                {
                    mtbPlayerLevelMin.Text = tmp[0];
                    mtbPlayerLevelMax.Text = tmp[1];
                }
                else mtbPlayerLevelMin.Text = curDialog.Precondition.PlayerLevel.ToString();
            }

            
            tmp = curDialog.Precondition.playerCombatLvl.Split(':');
            if (tmp.Any() && tmp.Length > 1)
            {
                tbCombatLvlMin.Text = tmp[0];
                tbCombatLvlMax.Text = tmp[1];
            }
            tmp = curDialog.Precondition.playerSurvLvl.Split(':');
            if (tmp.Any() && tmp.Length > 1)
            {              
                tbSurvLvlMin.Text = tmp[0];
                tbSurvLvlMax.Text = tmp[1];
            }
            tmp = curDialog.Precondition.playerOtherLvl.Split(':');
            if (tmp.Any() && tmp.Length > 1)
            {               
                tbOtherLvlMin.Text = tmp[0];
                tbOtherLvlMax.Text = tmp[1];
            }
            this.checkLevelIndicates();

        }
        private bool checkLevel()
        {
            if ( 
                mtbPlayerLevelMin.Text.Any() || mtbPlayerLevelMax.Text.Any() ||
                tbCombatLvlMin.Text.Any() || tbCombatLvlMax.Text.Any() ||
                tbSurvLvlMin.Text.Any() || tbSurvLvlMax.Text.Any() ||
                tbOtherLvlMin.Text.Any() || tbOtherLvlMax.Text.Any()
               )
                return true;
            else return false;
        }

        private void checkLevelIndicates()
        {
            pictureLevel.Visible = checkLevel();
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
                    if (flag == 1) flag = 0;
                    else flag = 2;
                }

                this.editKarmaPK.Add(flag);
                this.editKarmaPK.Add(b);        // тот же костыль
                this.editKarmaPK.Add(a);        // тот же костыль
            }
            this.checkKarmaIndicates();
        }

        private void checkActionIndicates()
        {
            pictureAction.Visible = (cbAnimationNPC.Checked && tbAnimationNPC.Text.Any()) || (cbAnimationPlayer.Checked && tbAnimationPlayer.Text.Any()) ||
                (cbCamera.Checked && tbCamera.Text.Any()) || (cbPlaySonund.Checked && tbPlaySonund.Text.Any()) || (cbAvatarPoint.Checked && tbAvatarPoint.Text.Any()) ||
                (cbActionNPC.Checked && tbActionNPC.Text.Any());   
        }

        private void checkEffectsIndicates()
        {
            pictureEffects.Visible = editPrecondition.MustNoEffects.Any() || editPrecondition.NecessaryEffects.Any();
        }

        private void checkItemsIndicates()
        {
            pictureItems.Visible = (rbCategory.Checked && cbCategory.SelectedIndex != -1) || (rbItems.Checked && GVItems.Rows.Count > 0);
        }

        private void checkTransportIndicates()
        {
            pictureTransport.Visible = (cbNotInTransportList.Checked || cbTransportInList.Checked || cbNotInBoatList.Checked || cbInBoatList.Checked ||
                                        cbIsBoatInTransit.Checked || cbIsBoatStopped.Checked);
        }

        private void checkTutorialIndicates()
        {
            pictureTutorial.Visible = (cbTutorialPhase.SelectedItem != null);
        }

        private void tabQuestsCircs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkClanOptionsIndicator();
            this.checkKarmaPK();
            this.checkReputation();
            this.checkEffects();
            this.checkSkills();
            this.checkActionIndicates();
            this.checkItemsIndicates();
            this.checkTransportIndicates();
            this.checkTutorialIndicates();
        }

        private void digitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbAutoNode_CheckedChanged(object sender, EventArgs e)
        {
            bool check = !cbAutoNode.Checked;
            tPlayerText.Enabled = check;
            tReactionNPC.Enabled = check;
            /*
            tNodes.Enabled = check;
            gbPrecondition.Enabled = check;
            gbActions.Enabled = check;
            actionsCheckBox.Enabled = check;
            cbForDev.Enabled = check;
            */
            tPlayerText.Visible = check;
            tReactionNPC.Visible = check;
            /*
            tNodes.Visible = check;
            gbPrecondition.Visible = check;
            gbActions.Visible = check;
            actionsCheckBox.Visible = check;
            cbForDev.Visible = check;
            */
            autoPanel.Enabled = !check;
            autoPanel.Visible = !check;
        }

        private void rbItems_CheckedChanged(object sender, EventArgs e)
        {
            panelItems.Visible = rbItems.Checked;
            panel1.Visible = rbItems.Checked;
            panel3.Visible = rbItems.Checked;
            //GVNonItems.Visible = rbItems.Checked;
        }

        private void rbCategory_CheckedChanged(object sender, EventArgs e)
        {
            cbCategory.Visible = rbCategory.Checked;
            cbNonCategory.Visible = rbCategory.Checked;
        }

        private void tabItems_SizeChanged(object sender, EventArgs e)
        {
            GVItems.Width = (sender as Panel).Width / 2;
            GVNonItems.Width = (sender as Panel).Width / 2;
            GVNonItems.Location = new Point((sender as Panel).Width / 2, GVNonItems.Location.Y);
        }

        private void rbOr_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbAnd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbRadioNode_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
