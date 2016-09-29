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
            this.fillClanOptions(curDialog.Precondition.clanOptions);

           

            if (curDialog.Actions.Exists() || curDialog.Actions.Exit || curDialog.Actions.ToDialog!=0 )
            {
                actionsCheckBox.Checked = true;
                ActionsComboBox.SelectedValue = curDialog.Actions.Event.Value;
                cbExit.Checked = curDialog.Actions.Exit;

                if (ActionsComboBox.Text == "Телепорт")
                {
                    string key = parent.tpConst.getName(curDialog.Actions.Data);
                    teleportComboBox.SelectedItem = key;
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

                foreach (int quest in curDialog.Precondition.ListOfMustNoQuests.ListOfCounters)
                    addItemToTextBox(quest.ToString(), tShouldntHaveCounters);

                addItemToTextBox(curDialog.Precondition.ListOfMustNoQuests.ListOfMassQuests, tShouldntHaveMassQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveCompletedQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveQuestsOnTest);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveOpenQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests)
                    addItemToTextBox(quest.ToString(), tMustHaveFailedQuests);

                foreach (int quest in curDialog.Precondition.ListOfNecessaryQuests.ListOfCounters)
                    addItemToTextBox(quest.ToString(), tMustHaveCounters);

                addItemToTextBox(curDialog.Precondition.ListOfNecessaryQuests.ListOfMassQuests, tMustHaveMassQuests);

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
            this.initReputationTab();
            this.initKarmaPKTab();
            this.initEffectsTab();
            this.initLevelTab();
            this.initSkillsTab();
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
                cbSecurExst.Checked || cbSecurNotExst.Checked)
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
            if (editPrecondition.Reputation.Any())
                pictureReputation.Visible = true;
            else
                pictureReputation.Visible = false;
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
            //List<string> holder = new List<string>();
            string holder = parent.GetCurrentNPC();
            if (!CheckConditions())
                return;
            if (!tNodes.Text.Equals(""))
                foreach (string node in tNodes.Text.Split(','))
                    nodes.Add(int.Parse(node));


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
                precondition.ListOfNecessaryQuests.ListOfMassQuests = tMustHaveMassQuests.Text;
            if (!tMustHaveCounters.Text.Equals(""))
                foreach (string quest in tMustHaveCounters.Text.Split(','))
                    precondition.ListOfNecessaryQuests.ListOfCounters.Add(int.Parse(quest));

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
                precondition.ListOfMustNoQuests.ListOfMassQuests = tShouldntHaveMassQuests.Text;
            if (!tShouldntHaveCounters.Text.Equals(""))
                foreach (string quest in tShouldntHaveCounters.Text.Split(','))
                    precondition.ListOfMustNoQuests.ListOfCounters.Add(int.Parse(quest));



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
            }             
            if (checkLevel())
            {
                precondition.PlayerLevel = mtbPlayerLevelMin.Text + ":" + mtbPlayerLevelMax.Text;
                precondition.playerCombatLvl = tbCombatLvlMin.Text + ":" + tbCombatLvlMax.Text;
                precondition.playerSurvLvl = tbSurvLvlMin.Text + ":" + tbSurvLvlMax.Text;
                precondition.playerOtherLvl = tbOtherLvlMin.Text + ":" + tbOtherLvlMax.Text;
            }
            precondition.Reputation = editPrecondition.Reputation;
            precondition.KarmaPK = editKarmaPK;
            precondition.NecessaryEffects = editPrecondition.NecessaryEffects;
            precondition.MustNoEffects = editPrecondition.MustNoEffects;
            precondition.Skills = editPrecondition.Skills;

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
            this.checkReputation();
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
            if (checkLevel())
            {
                pictureLevel.Visible = true;
            }
            else pictureLevel.Visible = false;
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

        private void checkEffectsIndicates()
        {
            if (editPrecondition.MustNoEffects.Any() || editPrecondition.NecessaryEffects.Any())
                pictureEffects.Visible = true;
            else
                pictureEffects.Visible = false;
        }
        private void tabQuestsCircs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkClanOptionsIndicator();
            this.checkKarmaPK();
            this.checkReputation();
            this.checkEffects();
            this.checkSkills();
        }

        private void digitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        

    }
}
