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
    //! Форма для редактирования условий квеста
    public partial class EditQuestForm : Form
    {
        int SHOW_MESSAGE_TAKE = 1;
        int SHOW_MESSAGE_CLOSE = 2;
        int SHOW_MESSAGE_PROGRESS = 4;
        int SHOW_JOURNAL = 8;
        int SHOW_ONWIN = 16;
        int SHOW_ONFAILED = 32;
        int SHOW_TUTORIAL = 64;

        const int ADD_NEW = 1;
        const int EDIT = 2;
        const int EDIT_SUB = 3;
        const int ADD_SUB = 4;

        int ITEM_REWARD = 0;
        int ITEM_QUESTRULES = 1;
        int ITEM_LOCALIZATION = 2;
        int ITEM_PENALTY = 4;

        int QuestType;

        int iInformationHeight = 0;
        int iTargetHeight = 0;
        int iRulesHeight = 0;
        int iRewardHeight = 0;
        int QuestID;

        Dictionary<int, string> scenaryTypes = new Dictionary<int, string>() {{0, "None"}, {2, "2 ATTACK_ON_PLAYER"}, {4, "4 GO_TO_WAYPOINT"}};

        Dictionary<int, string> dReputation = new Dictionary<int, string>();

        public CQuestTarget editTarget = new CQuestTarget();
        public CQuestRules editQuestRules = new CQuestRules();
        public CQuestReward editQuestReward = new CQuestReward();
        public CQuestReward editQuestPenalty = new CQuestReward();
        public CQuestInformation editInformation = new CQuestInformation();
        public MainForm parent;
        public CQuest quest;
        int iState;

        //! Конструктор, заполянет форму данными
        public EditQuestForm(MainForm parent, int currentQuest, int iState, int subID = 0)
        {
            InitializeComponent();
            this.parent = parent;
            this.iState = iState;
            if (iState == ADD_SUB)
                this.QuestID = subID;
            else
                this.QuestID = currentQuest;

            if (iState != ADD_NEW)
                this.quest = parent.getQuestOnQuestID(currentQuest);
            else
                this.quest = new CQuest();

            if (iState == ADD_SUB || iState == EDIT_SUB)
            {
                titleTextBox.Enabled = false;
                showCloseCheckBox.Enabled = false;
                showCloseCheckBox.Checked = false;
                showTakeCheckBox.Enabled = false;
                showTakeCheckBox.Checked = false;
                tutorialCheckBox.Enabled = false;              
            }

            if (iState == ADD_SUB)
            {
                CQuest parent_quest = parent.getQuestOnQuestID(quest.Additional.IsSubQuest);
            }

            if (iState == EDIT || iState == EDIT_SUB)
            {
                editQuestRules = quest.QuestRules;
                editQuestReward = quest.Reward;
                editQuestPenalty = quest.QuestPenalty;
                editTarget = quest.Target;
                editInformation = quest.QuestInformation;
            }
            groupQuestBox.Text += " " + this.QuestID.ToString();
            this.Text += (": " + this.QuestID.ToString() + "   Версия: " + this.quest.Version.ToString());
            fillForm();
        }

        void setDefaultTargetState()
        {
           foreach (Control control in targetBox.Controls)
               control.Enabled = false;
        }

        void clearTargetContent()
        {
            targetComboBox.Items.Clear();
            resultComboBox.Items.Clear();
            //targetComboBox.SelectedText = "";
//          repeatComboBox.Items.Clear();
        }
        //! Заполняет форму данными о квесте
        void fillForm()
        {
            cbHidden.Checked = quest.hidden;
            //bItemQuestRules.ImageKey = "";
            //bItemQuestRules.ImageKey = "but_indicate";
            resultComboBox.Items.Clear();
            foreach (СQuestType eventDescription in parent.questConst.getListQuests())
                eventComboBox.Items.Add(eventDescription.getDescription());

            showProgressCheckBox.Checked = true;
            showCloseCheckBox.Checked = true;
            showTakeCheckBox.Checked = true;
            showJournalCheckBox.Checked = true;

            CQuest parentQuest =  new CQuest();
            //if (iState == EDIT_SUB)
            //{
            //    parentQuest = parent.getQuestOnQuestID(quest.Additional.IsSubQuest);
            //}
            if (iState == ADD_SUB)
            {
                if ((quest.Additional.ShowProgress & this.SHOW_TUTORIAL)> 0)
                {
                    tutorialCheckBox.Checked = true;
                    showJournalCheckBox.Checked = false;
                    showJournalCheckBox.Enabled = false;
                }
            }
            else if (iState != ADD_NEW)
            {
                if ((quest.Additional.ShowProgress & this.SHOW_TUTORIAL) > 0)
                {
                    //System.Console.WriteLine("iState != ADD_NEW set tutorial true");
                    tutorialCheckBox.Checked = true;
                }
                else
                    tutorialCheckBox.Checked = false;
            }

            fillPreconditiomForm();
            fillQuestRulesForm();
            initCreateNPCPanel();
            initCreateMobPanel();
            if (iState != ADD_NEW)
                fillTargetForm(quest.Target.QuestType);

            if (iState == EDIT || iState == EDIT_SUB)
            {
                if (quest.Target.onFin == 1)
                    winRButton.Checked = true;
                else
                    loseRButton.Checked = true;

                eventComboBox.SelectedItem = parent.questConst.getDescription(quest.Target.QuestType);
                titleTextBox.Text = quest.QuestInformation.Title;
                descriptionTextBox.Text = quest.QuestInformation.Description;
                onWonTextBox.Text = quest.QuestInformation.onWin;
                onFailedTextBox.Text = quest.QuestInformation.onFailed;
                cantCancelCheckBox.Checked = quest.Additional.CantCancel;

                if ((quest.Additional.ShowProgress & this.SHOW_MESSAGE_CLOSE) > 0)
                    showCloseCheckBox.Checked = true;
                else
                    showCloseCheckBox.Checked = false;
                if ((quest.Additional.ShowProgress & this.SHOW_MESSAGE_TAKE) > 0)
                    showTakeCheckBox.Checked = true;
                else
                    showTakeCheckBox.Checked = false;
                if ((quest.Additional.ShowProgress & this.SHOW_MESSAGE_PROGRESS) > 0)
                    showProgressCheckBox.Checked = true;
                else
                    showProgressCheckBox.Checked = false;
                if ((quest.Additional.ShowProgress & this.SHOW_JOURNAL) > 0)
                    showJournalCheckBox.Checked = true;
                else
                    showJournalCheckBox.Checked = false;

                if ((quest.Additional.ShowProgress & this.SHOW_ONWIN) > 0)
                    showWinCheckBox.Checked = true;
                else
                    showWinCheckBox.Checked = false;

                if ((quest.Additional.ShowProgress & this.SHOW_ONFAILED) > 0)
                    showFailedCheckBox.Checked = true;
                else
                    showFailedCheckBox.Checked = false;
                if (quest.Additional.DebugData != "")
                {
                    this.debugTextBox.Text = quest.Additional.DebugData;
                }
                
                fillTarget();
                fillPrecondition();
                fillQuestRules();
                fillReward();
                
            }
            else
            {
                winRButton.Checked = true;
            }

        }
        //! Заполняет форму условиями квеста (повторное взятие)
        void fillPreconditiomForm()
        {
            repeatComboBox.Items.Add("Повторное взятие квеста после его закрытия.");
            repeatComboBox.Items.Add("Нет повтора.");
            repeatComboBox.SelectedIndex = 1;
            if (iState == ADD_SUB || iState == EDIT_SUB)
                takenPeriodTextBox.Enabled = false;
        }
        //! Заполняет данные об условиях повторного взятия квеста
        void fillPrecondition()
        {
            if (quest.Precondition.Repeat == 1)
                repeatComboBox.SelectedIndex = 0;
            else
                repeatComboBox.SelectedIndex = 1;
            IsCounterCheckBox.Checked = quest.Precondition.omniCounter;
            takenPeriodTextBox.Text = quest.Precondition.TakenPeriod.ToString();
        }       
        //! Настраивает форму на определенный тип квеста
        void fillTargetForm(int QuestType)
        {
            this.QuestType = QuestType;
            setDefaultTargetState();            
            dynamicCheckBox.Enabled = false;
            lTargetAttr1.Enabled = false;
            targetAttributeComboBox2.Enabled = false;
            lTargetAttr1.Text = "Атрибут2";
            ltargetResult.Text = "Результат";
            cbState.Text = "Учитывать состояние";
            lState.Text = "Состояние";
            panelCreateNPC.Visible = QuestType == 51;
            panelCreateNPC.Enabled = QuestType == 51;

            panelCreateMob.Visible = QuestType == 52;
            panelCreateMob.Enabled = QuestType == 52;
            if (parent.questConst.isSimple(QuestType))
            {
                targetComboBox.Enabled = true;
                lNameObject.Enabled = true;
                lQuantity.Enabled = true;
                quantityUpDown.Enabled = true;
                cbState.Enabled = false;
                cbReputationLow.Enabled = false;
                cbReputationLow.Visible = false;
                quantityUpDown.Minimum = 0;
                //quantityUpDown.Maximum = 32000;

                targetComboBox.SelectedItem = null;
                targetComboBox.SelectedText = "";
                targetComboBox.Items.Clear();

                if (QuestType == 9)
                {
                    targetComboBox.Enabled = false;
                    quantityUpDown.Enabled = false;
                    resultComboBox.Enabled = true;
                    ltargetResult.Text = "Время (сек):";
                }

                if ((QuestType == 0) || (QuestType == 16))
                {
                    lNameObject.Text = "Тип предмета:";
                    foreach (CItem description in parent.itemConst.getAllItems().Values)
                        targetComboBox.Items.Add(description.getDescription());
                    targetAttributeComboBox.Items.Clear();
                    targetAttributeComboBox.Items.Add("Обычный.");
                    targetAttributeComboBox.Items.Add("Квестовый.");
                    targetAttributeComboBox.Enabled = true;
                    labelTargetAttr.Enabled = true;
                    ltargetResult.Enabled = true;
                    dynamicCheckBox.Enabled = true;
                    cbState.Enabled = true;
                }

                else if (QuestType == 7)
                {
                    lNameObject.Text = "Тип предмета:";
                    foreach (CItem description in parent.itemConst.getAllItems().Values)
                        targetComboBox.Items.Add(description.getDescription());
                    targetAttributeComboBox.Items.Clear();
                    targetAttributeComboBox.Items.Add("Обычный.");
                    targetAttributeComboBox.Items.Add("Квестовый.");
                    targetAttributeComboBox.SelectedItem = "Квестовый.";
                    quantityUpDown.Value = 1;
                    quantityUpDown.Enabled = false;
                    bTargetAddDynamic.Enabled = false;
                    bTargetClearDynamic.Enabled = false;
                }
                else if (QuestType == 1)
                {
                    lNameObject.Text = "Имя NPC:";
                    foreach (CNPCDescription description in parent.npcConst.getAllNPCsDescription().Values)
                        targetComboBox.Items.Add(description.getName());
                    lQuantity.Enabled = false;
                    quantityUpDown.Enabled = false;

                }
                else if ((QuestType == 2) || (QuestType == 3))
                {
                    lNameObject.Text = "Тип моба:";
                    ltargetResult.Text = "Уровень моба";
                    ltargetResult.Enabled = true;
                    lTargetAttr1.Enabled = true;

                    resultComboBox.Enabled = true;
                    targetComboBox.Items.Clear();
                    foreach (CMobDescription description in parent.mobConst.getAllDescriptions().Values)
                        targetComboBox.Items.Add(description.getName());

                    resultComboBox.Items.Clear();

                    cbState.Text = "Учитывать урон";
                    cbState.Enabled = true;
                    lState.Text = "Процент урона";
                    lQuantity.Text = "Количество:";
                    labelTargetAttr.Text = "Зона:";
                    targetAttributeComboBox.Items.Clear();
                    targetAttributeComboBox.Enabled = true;
                    targetAttributeComboBox.Items.Add("");
                   // targetAttributeComboBox2.Items.Clear();
                   // targetAttributeComboBox2.Enabled = true;
                    //targetAttributeComboBox2.Items.Add("");
                   
                    foreach (CZoneDescription description in parent.zoneMobConst.getAllZones().Values)
                        targetAttributeComboBox.Items.Add(description.getName());
                    targetAttributeComboBox.SelectedIndex = 0;
                    //targetAttributeComboBox2.SelectedIndex = 0;
                    dynamicCheckBox.Enabled = true;
                }
                else if ((QuestType == 4) || (QuestType == 8))
                {
                    lNameObject.Text = "Имя зоны:";
                    targetComboBox.Items.Clear();
                    foreach (CZoneDescription description in parent.zoneConst.getAllZones().Values)
                        targetComboBox.Items.Add(description.getName());
                    lQuantity.Enabled = false;
                    quantityUpDown.Enabled = false;
                    dynamicCheckBox.Enabled = true;
                }
                else if (QuestType == 5)
                {
                    lNameObject.Text = "Деньги:";
                    targetComboBox.SelectedText = "";
                    targetComboBox.Enabled = false;
                    lQuantity.Enabled = true;
                    quantityUpDown.Enabled = true;
                    targetAttributeComboBox.Enabled = false;
                    labelTargetAttr.Enabled = false;
                }
                else if (QuestType == 6)
                {
                    lNameObject.Text = "Триггер:";
                    targetComboBox.Items.Clear();
                    foreach (string item in parent.triggerConst.getTriggersDescription())
                        targetComboBox.Items.Add(item);
                    lQuantity.Enabled = false;
                    quantityUpDown.Enabled = false;
                    targetAttributeComboBox.Enabled = false;
                    labelTargetAttr.Enabled = false;
                    dynamicCheckBox.Enabled = true;
                }
                else if (QuestType == 201 ||
                    QuestType == 202 ||
                    QuestType == 203 ||
                    QuestType == 204)
                {
                }
                else if (QuestType == 19 || QuestType == 20)
                {
                    lNameObject.Text = "Тип предмета:";
                    targetComboBox.Items.Clear();
                    foreach (CItem description in parent.itemConst.getAllItems().Values)
                        targetComboBox.Items.Add(description.getDescription());
                    quantityUpDown.Enabled = false;
                    lQuantity.Enabled = false;
                }
                else if (QuestType == 206)
                {
                    targetComboBox.Items.Clear();
                    foreach (string descr in parent.gui.guiIDs.Keys)
                        targetComboBox.Items.Add(descr);
                    targetAttributeComboBox.Enabled = true;
                    targetAttributeComboBox.Items.Add("Показать.");
                    targetAttributeComboBox.Items.Add("Скрыть.");
                    targetAttributeComboBox.SelectedIndex = 0;
                }
                else if (QuestType == 21)
                {
                    lNameObject.Text = "Эффект";
                    targetComboBox.Items.Clear();
                    foreach (string description in parent.effects.getAllDescriptions())
                        targetComboBox.Items.Add(description);
                    quantityUpDown.Enabled = true;
                    targetComboBox.Enabled = true;
                    lNameObject.Enabled = true;
                    lQuantity.Enabled = false;
                }
                else if ((QuestType == 22) || (QuestType == 23))
                {
                    lNameObject.Text = "Репутация";
                    targetComboBox.Items.Clear();
                    foreach (KeyValuePair<int, string> pair in parent.fractions.getListOfFractions())
                        targetComboBox.Items.Add(pair.Value);
                    quantityUpDown.Enabled = true;
                    targetComboBox.Enabled = true;
                    lNameObject.Enabled = true;
                    lQuantity.Enabled = false;
                    quantityUpDown.Minimum = -1000000;
                    cbReputationLow.Visible = true;
                    cbReputationLow.Enabled = true;

                }
                else if (QuestType == 24)
                {
                    lNameObject.Text = "Тип убийства:";
                    targetComboBox.Items.Clear();
                    targetComboBox.Items.Add("0 Кого-либо");
                    targetComboBox.Items.Add("1 Creature");
                    targetComboBox.Items.Add("2 Avatar");
                    targetComboBox.Items.Add("3 NPC");
                }
                else if (QuestType == 53 || QuestType == 54)
                {
                    lNameObject.Text = "Тип NPC:";
                    targetComboBox.Enabled = false;
                    ltargetResult.Text = "Уровень NPC";
                    ltargetResult.Enabled = false;
                    lTargetAttr1.Enabled = false;

                    labelTargetAttr.Text = "Квестовый ID:";
                    labelTargetAttr.Enabled = true;
                    cbState.Text = "Засчитывать всем";
                    cbState.Enabled = true;

                    targetAttributeComboBox.Items.Clear();
                    targetAttributeComboBox.Enabled = true;
                }

            }
            else
            {
                targetComboBox.Enabled = false;
                lNameObject.Enabled = false;
                lQuantity.Enabled = false;
                quantityUpDown.Enabled = false;
            }

            if (iState != ADD_NEW)
            {
                isClanCheckBox.Checked = quest.Target.IsClan;
                //IsGroupCheckBox.Checked = quest.Target.IsGroup;
            }

            if (iState == ADD_SUB || iState == EDIT_SUB)
            {
                isClanCheckBox.Enabled = false;
                takenPeriodTextBox.Enabled = false;
                lDaily.Enabled = false; 
                lH.Enabled = false;
                //IsGroupCheckBox.Enabled = false;
            }
        }
        //! Заполняет данные о целях квеста (о боги какой говнокод)
        void fillTarget()
        {
            if ((quest.Target.QuestType == 0) || (quest.Target.QuestType == 16) || (quest.Target.QuestType == 7))
            {
                targetComboBox.SelectedItem = parent.itemConst.getDescriptionOnID(quest.Target.ObjectType);
                quantityUpDown.Value = quest.Target.NumOfObjects;
                targetAttributeComboBox.SelectedIndex = quest.Target.ObjectAttr;
                if (quest.Target.usePercent)
                {
                    cbState.Checked = true;
                    udState.Value = Convert.ToDecimal(quest.Target.percent * 100);
                }
                else cbState.Checked = false;

            }
            else if (quest.Target.QuestType == 1)
            {
                targetComboBox.SelectedItem = parent.npcConst.getDescriptionOnKey(quest.Target.ObjectName).getName();
            }
            else if ((quest.Target.QuestType == 2) || (quest.Target.QuestType == 3))
            {
                targetComboBox.SelectedItem = parent.mobConst.getDescriptionOnType(quest.Target.ObjectType).getName();
                if (!quest.Target.AreaName.Equals(""))
                {
                    targetAttributeComboBox.Items.Clear();
                    targetAttributeComboBox.Text = quest.Target.AreaName;
                    // иначе чёрт знает как засунуть текст, который не в Items
                    foreach (CZoneDescription description in parent.zoneMobConst.getAllZones().Values)
                        targetAttributeComboBox.Items.Add(description.getName());

                    string area = parent.zoneMobConst.getDescriptionOnKey(quest.Target.AreaName).getName();
                    if (area.Any())
                        targetAttributeComboBox.SelectedItem = area;
                }
                //if (quest.Target.ObjectAttr < 0)
                //    targetAttributeComboBox2.SelectedIndex = 0;
                //else
                //    targetAttributeComboBox2.SelectedIndex = parent.mobConst.getDescriptionOnType(quest.Target.ObjectType).getIndexOnLevel(quest.Target.ObjectAttr);
                resultComboBox.Text = quest.Target.ObjectAttr.ToString();
                if (quest.Target.usePercent)
                {
                    cbState.Checked = true;
                    udState.Value = Convert.ToDecimal(quest.Target.percent * 100);
                }
                else cbState.Checked = false;

                if (quest.Target.ObjectName.Contains(','))
                {
                    dynamicCheckBox.Checked = true;
                    resultComboBox.Text = quest.Target.ObjectName;
                }
                else
                {
                    quantityUpDown.Value = quest.Target.NumOfObjects;
                }

            }

            else if ((quest.Target.QuestType == 4) || (quest.Target.QuestType == 8))
            {

                if (quest.Target.ObjectName.Contains(','))
                {
                    dynamicCheckBox.Checked = true;
                    resultComboBox.Text = quest.Target.ObjectName;
                }
                else
                {
                    targetComboBox.SelectedItem = parent.zoneConst.getDescriptionOnKey(quest.Target.ObjectName).getName();
                }
            }
            else if (quest.Target.QuestType == 5)
            {
                quantityUpDown.Value = quest.Target.NumOfObjects;
            }
            else if (quest.Target.QuestType == 6)
            {

                if (quest.Target.ObjectName.Contains(','))
                {
                    dynamicCheckBox.Checked = true;
                    resultComboBox.Text = quest.Target.ObjectName;
                }
                else
                {
                    targetComboBox.SelectedItem = parent.triggerConst.getDescriptionOnId(quest.Target.ObjectType);
                }
            }
            else if (quest.Target.QuestType == 9)
            {
                resultComboBox.Text = resultComboBox.Text.Replace('.', ',');
                resultComboBox.Text = quest.Target.Time.ToString();
            }
            else if (quest.Target.QuestType == 19 || quest.Target.QuestType == 20)
            {
                //System.Console.WriteLine("getQuest: type" + quest.Target.QuestType.ToString());
                targetComboBox.SelectedItem = parent.itemConst.getDescriptionOnID(quest.Target.ObjectType);
            }
            else if (quest.Target.QuestType == 206)
            {
                targetComboBox.SelectedItem = parent.gui.getDescriptionOnID(quest.Target.ObjectType);
                targetAttributeComboBox.SelectedIndex = quest.Target.ObjectAttr;
            }
            else if (quest.Target.QuestType == 21)
            {
                targetComboBox.SelectedItem = parent.effects.getDescriptionOnID(quest.Target.ObjectType);
                quantityUpDown.Value = quest.Target.NumOfObjects;
            }
            else if (quest.Target.QuestType == 22 || quest.Target.QuestType == 23)
            {
                targetComboBox.SelectedItem = parent.fractions.getFractionDesctByID(quest.Target.ObjectType);
                quantityUpDown.Value = quest.Target.NumOfObjects;
                cbReputationLow.Checked = quest.Target.ObjectAttr != 0;
            }
            else if (quest.Target.QuestType == 24)
            {
                targetComboBox.SelectedIndex = quest.Target.ObjectType;
            }
            else if (quest.Target.QuestType == 51)
            {
                fillCreateNPCPanel();
            }
            else if (quest.Target.QuestType == 52)
            {
                fillCreateMobPanel();
            }
            else if ((quest.Target.QuestType == 53) || (quest.Target.QuestType == 54))
            {
                if (!quest.Target.AreaName.Equals(""))
                    targetAttributeComboBox.Text = quest.Target.AreaName;


                if (quest.Target.ObjectAttr != 0)
                {
                    cbState.Checked = true;
                }
                else cbState.Checked = false;
            }
        }

        CQuestRules.NPC getCreateNPC()
        {
            CQuestRules.NPC npc = new CQuestRules.NPC();

            npc.name = tbNpcName.Text;
            npc.displayName = tbDisplayName.Text;
            int.TryParse(tbReputation.Text, out npc.reputation);
            npc.way = tbWay.Text;
            npc.animation = tbNPCAnim.Text;

            npc.fraction = cbFractionID.SelectedItem == null ? 0 : parent.fractions.getFractionIDByDescr(cbFractionID.SelectedItem.ToString());
            npc.weapon = cbWeapon.SelectedItem == null ? 0 : parent.npcItems.weapons.getIDByName(cbWeapon.SelectedItem.ToString());
            npc.hand = cbHands.SelectedItem == null ? 0 : parent.npcItems.hands.getIDByName(cbHands.SelectedItem.ToString());
            npc.boots = cbBoots.SelectedItem == null ? 0 : parent.npcItems.boots.getIDByName(cbBoots.SelectedItem.ToString());
            npc.body = cbBody.SelectedItem == null ? 0 : parent.npcItems.body.getIDByName(cbBody.SelectedItem.ToString());
            npc.armor = cbArmor.SelectedItem == null ? 0 : parent.npcItems.armor.getIDByName(cbArmor.SelectedItem.ToString());
            npc.legs = cbLegs.SelectedItem == null ? 0 : parent.npcItems.legs.getIDByName(cbLegs.SelectedItem.ToString());
            npc.cap = cbCap.SelectedItem == null ? 0 : parent.npcItems.cap.getIDByName(cbCap.SelectedItem.ToString());
            npc.mask = cbMask.SelectedItem == null ? 0 : parent.npcItems.mask.getIDByName(cbMask.SelectedItem.ToString());
            npc.back = cbBackpack.SelectedItem == null ? 0 : parent.npcItems.back.getIDByName(cbBackpack.SelectedItem.ToString());
            npc.head = cbHead.SelectedItem == null ? 0 : parent.npcItems.head.getIDByName(cbHead.SelectedItem.ToString());
            npc.head = cbHead.SelectedItem == null ? 0 : parent.npcItems.head.getIDByName(cbHead.SelectedItem.ToString());
            npc.uniq = cbUniqNPC.Checked;
            npc.invulnerable = cbNPCInvul.Checked;
            npc.mobNoAgr = cbNPCMobNoArg.Checked;

            npc.walkSpeed = Convert.ToSingle(nupNPCSpeed.Value);
            return npc;
        }

        CQuestRules.Mob getCreateMob()
        {
            CQuestRules.Mob mob = new CQuestRules.Mob();

            if (cbMobType.SelectedItem != null)
                mob.mob_type = parent.mobConst.getTypeOnDescription(cbMobType.SelectedItem.ToString());

            mob.level = cbMobLevel.Text;
            if (cbScenaryType.Text.Any())
                int.TryParse(cbScenaryType.Text[0].ToString(), out mob.scen_type);
            mob.way = tbMobWay.Text;
            mob.count = Convert.ToInt32(nupMobCount.Value);
            mob.questID = tbUniqMobID.Text;
            mob.uniq = cbMobUniq.Checked;
            mob.invulnerable = cbMobInvul.Checked;

            return mob;
        }

        void fillCreateMobPanel()
        {
            if (!quest.QuestRules.mobs.Any())
                return;
            cbMobType.SelectedItem = parent.mobConst.getDescriptionOnType(quest.QuestRules.mobs.mob_type).getName();
            cbMobLevel.SelectedItem = quest.QuestRules.mobs.level.ToString();
            cbScenaryType.SelectedItem = scenaryTypes[quest.QuestRules.mobs.scen_type];
            tbMobWay.Text = quest.QuestRules.mobs.way;
            nupMobCount.Value = quest.QuestRules.mobs.count;
            tbUniqMobID.Text = quest.QuestRules.mobs.questID;
            cbMobUniq.Checked = quest.QuestRules.mobs.uniq;
            cbMobInvul.Checked = quest.QuestRules.mobs.invulnerable;
        }

        void fillCreateNPCPanel()
        {
            tbNpcName.Text = quest.QuestRules.npc.name;
            tbDisplayName.Text = quest.QuestRules.npc.displayName;
            tbReputation.Text = quest.QuestRules.npc.reputation.ToString();

            tbWay.Text = quest.QuestRules.npc.way;
            tbNPCAnim.Text = quest.QuestRules.npc.animation;
            cbFractionID.SelectedItem = parent.fractions.getFractionDesctByID(quest.QuestRules.npc.fraction);
            cbWeapon.SelectedItem = parent.npcItems.weapons.getNameByID(quest.QuestRules.npc.weapon);
            cbHands.SelectedItem = parent.npcItems.hands.getNameByID(quest.QuestRules.npc.hand);
            cbBoots.SelectedItem = parent.npcItems.boots.getNameByID(quest.QuestRules.npc.boots);
            cbBody.SelectedItem = parent.npcItems.body.getNameByID(quest.QuestRules.npc.body);
            cbArmor.SelectedItem = parent.npcItems.armor.getNameByID(quest.QuestRules.npc.armor);
            cbLegs.SelectedItem = parent.npcItems.legs.getNameByID(quest.QuestRules.npc.legs);
            cbCap.SelectedItem = parent.npcItems.cap.getNameByID(quest.QuestRules.npc.cap);
            cbMask.SelectedItem = parent.npcItems.mask.getNameByID(quest.QuestRules.npc.mask);
            cbBackpack.SelectedItem = parent.npcItems.back.getNameByID(quest.QuestRules.npc.back);
            cbHead.SelectedItem = parent.npcItems.head.getNameByID(quest.QuestRules.npc.head);
            cbUniqNPC.Checked = quest.QuestRules.npc.uniq;
            cbNPCInvul.Checked = quest.QuestRules.npc.invulnerable;
            cbNPCMobNoArg.Checked = quest.QuestRules.npc.mobNoAgr;
            nupNPCSpeed.Value = Convert.ToDecimal(quest.QuestRules.npc.walkSpeed);
        }
        void initCreateMobPanel()
        {
            cbMobType.Items.Clear();
            foreach (CMobDescription mob in parent.mobConst.getAllDescriptions().Values)
                cbMobType.Items.Add(mob.getName());

            cbMobLevel.Items.Clear();
            cbScenaryType.Items.Clear();
            cbScenaryType.Items.Add("2 ATTACK_ON_PLAYER");
            cbScenaryType.Items.Add("4 GO_TO_WAYPOINT");

        }

        void initCreateNPCPanel()
        {
            cbFractionID.Items.Clear();
            cbFractionID.Items.AddRange(parent.fractions.getListOfFractions().Values.ToArray());

            cbWeapon.Items.Clear();
            cbHands.Items.Clear();
            cbBoots.Items.Clear();
            cbBody.Items.Clear();
            cbArmor.Items.Clear();
            cbLegs.Items.Clear();
            cbCap.Items.Clear();
            cbMask.Items.Clear();
            cbBackpack.Items.Clear();
            cbHead.Items.Clear();

            cbWeapon.Items.AddRange(parent.npcItems.weapons.getNames().ToArray());
            cbHands.Items.AddRange(parent.npcItems.hands.getNames().ToArray());
            cbBoots.Items.AddRange(parent.npcItems.boots.getNames().ToArray());
            cbBody.Items.AddRange(parent.npcItems.body.getNames().ToArray());
            cbArmor.Items.AddRange(parent.npcItems.armor.getNames().ToArray());
            cbLegs.Items.AddRange(parent.npcItems.legs.getNames().ToArray());
            cbCap.Items.AddRange(parent.npcItems.cap.getNames().ToArray());
            cbMask.Items.AddRange(parent.npcItems.mask.getNames().ToArray());
            cbBackpack.Items.AddRange(parent.npcItems.back.getNames().ToArray());
            cbHead.Items.AddRange(parent.npcItems.head.getNames().ToArray());
        }

        void fillQuestRulesForm()
        {
            foreach (string space in parent.spacesConst.getSpacesDescription())
                instanceComboBox.Items.Add(space);

            nBaseToCapturePercent.Value = Convert.ToDecimal(quest.QuestRules.basePercent * 100);
        }
        //! Заполняет раздел Правила на форме - уровень игрока, сценарий... (???)
        void fillQuestRules()
        {
            foreach (int item in quest.QuestRules.Scenarios)
            {
                if (scenariosTextBox.Text == "")
                    scenariosTextBox.Text += item.ToString();
                else
                    scenariosTextBox.Text += ("," + item.ToString());
            }
            foreach (int item in quest.QuestRules.MassQuests)
            {
                if (massQuestsTextBox.Text == "")
                    massQuestsTextBox.Text += item.ToString();
                else
                    massQuestsTextBox.Text += ("," + item.ToString());
            }
            checkQuestRulesIndicates();
        }
        //! Создает индикацию зеленым светом в графе Награда, если выдаются предметы, эффекты, или статус
        public void checkRewardIndicates()
        {
            if (editQuestReward.TypeOfItems.Any())
                bRewardItem.Image = Properties.Resources.but_indicate;
            else
                bRewardItem.Image = null;

            if (editQuestPenalty.TypeOfItems.Any())
                bPenaltyItem.Image = Properties.Resources.but_indicate;
            else
                bPenaltyItem.Image = null;

            if (editQuestReward.ReputationNotEmpty())
                bRewardReputation.Image = Properties.Resources.but_indicate;
            else
                bRewardReputation.Image = null;

            if (editQuestPenalty.ReputationNotEmpty())
                bPenaltyReputation.Image = Properties.Resources.but_indicate;
            else
                bPenaltyReputation.Image = null;

            if (editQuestReward.Effects.Any())
                bRewardEffects.Image = Properties.Resources.but_indicate;
            else
                bRewardEffects.Image = null;

            if (editQuestPenalty.Effects.Any())
                bPenaltyEffects.Image = Properties.Resources.but_indicate;
            else
                bPenaltyEffects.Image = null; 
        }
        //! Создает индиикацию кнопки Предметы в графе Правила квеста
        public void checkQuestRulesIndicates()
        {
            if (editQuestRules.TypeOfItems.Any())
                bItemQuestRules.Image = Properties.Resources.but_indicate;
        }
        //! Заполняет данные о награде - опыт, деньги, карму
        void fillReward()
        {
            checkRewardIndicates();
           
            if (quest.Reward.Experience.Count == 3)
            {
                tExperience.Text = quest.Reward.Experience[0].ToString();
                tSurvival.Text = quest.Reward.Experience[1].ToString();
                tSupport.Text = quest.Reward.Experience[2].ToString();
            }

            if (quest.QuestPenalty.Experience.Count == 3)
            {
                tbPenaltyExperience.Text = quest.QuestPenalty.Experience[0].ToString();
                tbPenaltySurvival.Text = quest.QuestPenalty.Experience[1].ToString();
                tbPenaltySupport.Text = quest.QuestPenalty.Experience[2].ToString();
            }

            cbRewardWindow.Checked = quest.Reward.RewardWindow;
            creditsTextBox.Text = quest.Reward.Credits.ToString();
            tbPenaltyCredits.Text = quest.QuestPenalty.Credits.ToString();
            textBoxKarmaPK.Text = quest.Reward.KarmaPK.ToString();
            tbPenaltyKarmaPK.Text = quest.QuestPenalty.KarmaPK.ToString();
        }
        //! Собирает данные с формы, и возвращает экземпляр CQuest с этими данными
        public CQuest getQuest()
        {
            CQuestAdditional additional = new CQuestAdditional();
            CQuestInformation information = new CQuestInformation();
            CQuestPrecondition precondition = new CQuestPrecondition();
            CQuestReward reward = new CQuestReward();
            CQuestRules rules = new CQuestRules();
            CQuestTarget target = new CQuestTarget();
            CQuestReward penalty = new CQuestReward();

            information.Description = descriptionTextBox.Text;
            information.Title = titleTextBox.Text;
            additional.Holder = parent.GetCurrentNPC();
            information.onWin = onWonTextBox.Text;
            information.onFailed = onFailedTextBox.Text;
            rules.basePercent = Convert.ToSingle(nBaseToCapturePercent.Value) / 100;
            target.QuestType = parent.questConst.getQuestTypeOnDescription(eventComboBox.SelectedItem.ToString());
            if (loseRButton.Checked)
                target.onFin = 0;
            else
                target.onFin = 1;

            if ((target.QuestType == 0) || (target.QuestType == 16) || (target.QuestType == 7))
            {
                target.ObjectType = parent.itemConst.getIDOnDescription(targetComboBox.SelectedItem.ToString());
                target.NumOfObjects = int.Parse(quantityUpDown.Value.ToString());
                if (target.NumOfObjects < 1)
                {
                    MessageBox.Show("Цель->Количество - некорректное значение", "Ошибка");
                    return null;
                }
                target.ObjectAttr = targetAttributeComboBox.SelectedIndex;
                if (cbState.Checked && cbState.Enabled)
                {
                    target.usePercent = true;
                    target.percent = Convert.ToSingle(udState.Value) / 100;
                }
                else target.usePercent = false;
            }
            else if (target.QuestType == 1)
            {
                string key = parent.npcConst.getKeyOnDescription(targetComboBox.SelectedItem.ToString());
                if (key.Equals(""))
                    target.ObjectName = targetComboBox.SelectedItem.ToString();
                else
                    target.ObjectName = key;
            }
            else if ((target.QuestType == 2) || (target.QuestType == 3))
            {
                target.ObjectType = parent.mobConst.getTypeOnDescription(targetComboBox.SelectedItem.ToString());
                
                if (dynamicCheckBox.Checked)
                    target.ObjectName = resultComboBox.Text;
                else
                {
                    int level = ParseIntIfNotEmpty(resultComboBox.Text);
                    target.ObjectAttr = level;

                    target.NumOfObjects = int.Parse(quantityUpDown.Value.ToString());
                    if ((target.NumOfObjects > 32000) || (target.NumOfObjects < 1))
                    {
                        MessageBox.Show("Цель->Количество - некорректное значение(нужно 0 < x < 32000)", "Ошибка");
                        return null;
                    }
                }

                if (cbState.Checked && cbState.Enabled)
                {
                    target.usePercent = true;
                    target.percent = Convert.ToSingle(udState.Value) / 100;
                }
                else target.usePercent = false;

                if (targetAttributeComboBox.SelectedItem == null)
                    target.AreaName = targetAttributeComboBox.Text;
                else if (targetAttributeComboBox.SelectedItem.ToString().Equals(""))
                    target.AreaName = "";
                else
                {
                    string area = parent.zoneMobConst.getKeyOnDescription(targetAttributeComboBox.SelectedItem.ToString());
                    target.AreaName = area.Any() ? area : targetAttributeComboBox.Text;
                }
            }
            else if ((target.QuestType == 4) || (target.QuestType == 8))
            {
                if (dynamicCheckBox.Checked)
                    target.ObjectName = resultComboBox.Text;
                else
                {
                    string zone = parent.zoneConst.getKeyOnDescription(targetComboBox.SelectedItem.ToString());
                    if (zone.Equals(""))
                        target.ObjectName = targetComboBox.SelectedItem.ToString();
                    else
                        target.ObjectName = zone;
                }
            }
            else if (target.QuestType == 5)
            {
                target.NumOfObjects = (int)quantityUpDown.Value;
                if (target.NumOfObjects < 1)
                {
                    MessageBox.Show("Цель->Количество - некорректное значение", "Ошибка");
                    return null;
                }
            }
            else if (target.QuestType == 6)
            {
                if (dynamicCheckBox.Checked)
                    target.ObjectName = resultComboBox.Text;
                else
                    target.ObjectType = parent.triggerConst.getIdOnKey(targetComboBox.SelectedItem.ToString());
            }

            else if (target.QuestType == 9)
            {
                try
                {
                    target.Time = float.Parse(resultComboBox.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return null;
                }
            }
            else if ((target.QuestType == 19) || (target.QuestType == 20))
            {
                System.Console.WriteLine("QuestType == 19 || 20");
                target.ObjectType = parent.itemConst.getIDOnDescription(targetComboBox.SelectedItem.ToString());
            }
            else if (target.QuestType == 201 ||
                     target.QuestType == 202 ||
                    target.QuestType == 203 ||
                    target.QuestType == 204)
            {
            }
            else if (target.QuestType == 206)
            {
                target.ObjectType = parent.gui.getIDOnDescription(targetComboBox.SelectedItem.ToString());
                target.ObjectAttr = targetAttributeComboBox.SelectedIndex;
            }
            else if (target.QuestType == 21)
            {
                target.ObjectType = parent.effects.getIDOnDescription(targetComboBox.SelectedItem.ToString());
                target.NumOfObjects = int.Parse(quantityUpDown.Value.ToString());
                if (target.NumOfObjects < 1)
                {
                    MessageBox.Show("Цель->Количество - некорректное значение", "Ошибка");
                    return null;
                }
            }
            else if (target.QuestType == 22 || target.QuestType == 23)
            {
                target.ObjectType = parent.fractions.getFractionIDByDescr(targetComboBox.SelectedItem.ToString());
                target.NumOfObjects = int.Parse(quantityUpDown.Value.ToString());
                target.ObjectAttr = Convert.ToInt16(cbReputationLow.Checked);
                if (target.NumOfObjects < 1)
                {
                    MessageBox.Show("Цель->Количество - некорректное значение", "Ошибка");
                    return null;
                }
            }
            else if (target.QuestType == 24)
            {
                target.ObjectType = targetComboBox.SelectedIndex;
            }
            else if (target.QuestType == 51)
            {
                rules.npc = getCreateNPC();
            }
            else if (target.QuestType == 52)
            {
                rules.mobs = getCreateMob();
            }
            else if ((target.QuestType == 53) || (target.QuestType == 54))
            {
                if (cbState.Checked && cbState.Enabled)
                {
                    target.ObjectAttr = 1;
                }
                else target.ObjectAttr = 0;
                target.AreaName = targetAttributeComboBox.Text;
            }

            if (iState == EDIT_SUB || iState == EDIT)
                additional = quest.Additional;

            //target.IsGroup = IsGroupCheckBox.Checked;
            target.IsClan = isClanCheckBox.Checked;

            if (iState == EDIT)
            {
                parent.setQuestISClan(quest.QuestID, target.IsClan);
                //parent.setQuestISGroup(quest.QuestID, target.IsGroup);
            }

            if (repeatComboBox.SelectedItem.ToString().Equals("Повторное взятие квеста после его закрытия."))
                precondition.Repeat = 1;

            if (takenPeriodTextBox.Enabled && !takenPeriodTextBox.Text.Equals(""))
                precondition.TakenPeriod = double.Parse(takenPeriodTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

            precondition.omniCounter = IsCounterCheckBox.Checked;
            foreach (string item in scenariosTextBox.Text.Split(','))
                if (!item.Equals(""))
                    rules.Scenarios.Add(int.Parse(item));
            foreach (string item in massQuestsTextBox.Text.Split(','))
                if (!item.Equals(""))
                    rules.MassQuests.Add(int.Parse(item));

            reward.Experience.Add(ParseIntIfNotEmpty(tExperience.Text));
            reward.Experience.Add(ParseIntIfNotEmpty(tSurvival.Text));
            reward.Experience.Add(ParseIntIfNotEmpty(tSupport.Text));
            reward.Credits = ParseIntIfNotEmpty(creditsTextBox.Text);
            reward.KarmaPK = ParseIntIfNotEmpty(textBoxKarmaPK.Text);
            reward.RewardWindow = cbRewardWindow.Checked;

            penalty.Experience.Add(ParseIntIfNotEmpty(tbPenaltyExperience.Text));
            penalty.Experience.Add(ParseIntIfNotEmpty(tbPenaltySurvival.Text));
            penalty.Experience.Add(ParseIntIfNotEmpty(tbPenaltySupport.Text));
            penalty.Credits = ParseIntIfNotEmpty(tbPenaltyCredits.Text);
            penalty.KarmaPK = ParseIntIfNotEmpty(tbPenaltyKarmaPK.Text);

            int iProgressResult = 0;
            if (showCloseCheckBox.Checked)
                iProgressResult |= this.SHOW_MESSAGE_CLOSE;
            if (showTakeCheckBox.Checked)
                iProgressResult |= this.SHOW_MESSAGE_TAKE;
            if (showJournalCheckBox.Checked)
                iProgressResult |= this.SHOW_JOURNAL;
            if (showProgressCheckBox.Checked)
                iProgressResult |= this.SHOW_MESSAGE_PROGRESS;
            if (showWinCheckBox.Checked)
                iProgressResult |= this.SHOW_ONWIN;
            if (showFailedCheckBox.Checked)
                iProgressResult |= this.SHOW_ONFAILED;
            if (tutorialCheckBox.Checked)
            {
                System.Console.WriteLine("tutorialCheckBox.Checked != SHOW_TUTORIAL");
                iProgressResult |= this.SHOW_TUTORIAL;
            }
            additional.ShowProgress = iProgressResult;
            additional.CantCancel = cantCancelCheckBox.Checked;
            CQuest retQuest;

            reward.TypeOfItems = editQuestReward.TypeOfItems;
            reward.NumOfItems = editQuestReward.NumOfItems;
            reward.AttrOfItems = editQuestReward.AttrOfItems;
            reward.Probability = editQuestReward.Probability;
            reward.Reputation = editQuestReward.Reputation;
            reward.Effects = editQuestReward.Effects;

            penalty.TypeOfItems = editQuestPenalty.TypeOfItems;
            penalty.NumOfItems = editQuestPenalty.NumOfItems;
            penalty.AttrOfItems = editQuestPenalty.AttrOfItems;
            penalty.Probability = editQuestPenalty.Probability;
            penalty.Reputation = editQuestPenalty.Reputation;
            penalty.Effects = editQuestPenalty.Effects;

            rules.TypeOfItems = editQuestRules.TypeOfItems;
            rules.NumOfItems = editQuestRules.NumOfItems;
            rules.AttrOfItems = editQuestRules.AttrOfItems;            
            information.Items = editInformation.Items;

            target.AObjectAttrs = editTarget.AObjectAttrs;
            rules.basePercent = editQuestRules.basePercent;

            if (iState == ADD_NEW || iState == ADD_SUB)
            {
                if (iState == ADD_SUB)
                    additional.IsSubQuest = quest.QuestID;
                retQuest = new CQuest(this.QuestID, 1, information, precondition, rules, reward, additional, target, penalty);
                parent.incQuestNewID();
            }
            else
            {
                int version = quest.Version;
                if (quest.QuestInformation.Title != information.Title || quest.QuestInformation.Description != information.Description 
                        || quest.QuestInformation.onWin != information.onWin || quest.QuestInformation.onFailed != information.onFailed)
                    version++;
                 
                retQuest = new CQuest(quest.QuestID, version, information, precondition, rules, reward, additional, target, penalty, cbHidden.Checked);
            }
            return retQuest;
        }

        private int ParseIntIfNotEmpty(string text)
        {
            if (!text.Equals(""))
                return int.Parse(text);
            return 0;
        }
        //! Действия при смене типа кевеста
        private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedQuestType = parent.questConst.getQuestTypeOnDescription(eventComboBox.SelectedItem.ToString());
            //if (selectedQuestType != 0)
            //{
                clearTargetContent();
                fillTargetForm(selectedQuestType);
            //}
        }
        //! Закрытие окна без сохранения
        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.Enabled = true;
            this.Close();
        }
        //! Нажатие OK - сохранение квеста или создание нового
        private void okButton_Click(object sender, EventArgs e)
        {
            CQuest result = getQuest();
            if (result != null)
            {
                if (iState == ADD_NEW)
                    parent.createNewQuest(result);
                else if (iState == ADD_SUB)
                    parent.addQuest(result, quest.QuestID);
                else
                    parent.replaceQuest(result);

                if (result.Additional.IsSubQuest == 0)
                    parent.setTutorial(result.QuestID, (((result.Additional.ShowProgress & this.SHOW_TUTORIAL) > 0)));
                if (debugTextBox.Text != "") { result.Additional.DebugData = debugTextBox.Text; }
                parent.Enabled = true;
                this.Close();
            }
        }

        private void dynamicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dynamicCheckBox.Checked)
            {
                bTargetAddDynamic.Enabled = true;
                bTargetClearDynamic.Enabled = true;
                resultComboBox.Enabled = true;

                if (this.QuestType == 2 || this.QuestType == 3)
                    ltargetResult.Text = "Мин,Макс:";
                else if (this.QuestType == 4)
                    ltargetResult.Text = "Зоны:";
                else if (this.QuestType == 6)
                    ltargetResult.Text = "Триггеры:";
            }
            else
            {
                if (this.QuestType == 2 || this.QuestType == 3)
                {
                    ltargetResult.Text = "Уровень моба";
                    resultComboBox.Enabled = true;
                }
                else
                {
                    ltargetResult.Text = "Результат:";
                    resultComboBox.Enabled = false;
                }
                bTargetAddDynamic.Enabled = false;
                bTargetClearDynamic.Enabled = false;
            }
        }

        private void bTargetClearDynamic_Click(object sender, EventArgs e)
        {
            resultComboBox.Items.Clear();
            resultComboBox.Text = "";
        }

        private void bTargetAddDynamic_Click(object sender, EventArgs e)
        {
            string str = "";

            if (this.QuestType == 2 || this.QuestType == 3)
            {
                str += quantityUpDown.Value.ToString();
            }
            else if (this.QuestType == 4)
            {
                str += parent.zoneConst.getKeyOnDescription(targetComboBox.SelectedItem.ToString());
            }
            else if (this.QuestType == 6)
            {
                str += parent.triggerConst.getIdOnKey(targetComboBox.SelectedItem.ToString());
            }

            if (resultComboBox.Text.Equals(""))
                resultComboBox.Text += str;
            else
                resultComboBox.Text += ("," + str);
        }

        private void targetAttributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (targetAttributeComboBox.SelectedItem.ToString().Equals("Квестовый."))
                bItemQID.Enabled = true;
            else
                bItemQID.Enabled = false;
        }

        private void targetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.QuestType == 2 || this.QuestType == 3)
            {
                resultComboBox.Items.Clear();
                foreach (string level in parent.mobConst.getLevelsOnDescription(targetComboBox.SelectedItem.ToString()))
                    resultComboBox.Items.Add(level);
            }
        }
        //! Кнопка Скрыть информацию о квесте
        private void bHideInformation_Click(object sender, EventArgs e)
        {
            lTitle.Visible = !lTitle.Visible;
            titleTextBox.Visible = !titleTextBox.Visible;
            lDescription.Visible = !lDescription.Visible;
            descriptionTextBox.Visible = !descriptionTextBox.Visible;
            showCloseCheckBox.Visible = !showCloseCheckBox.Visible;
            showJournalCheckBox.Visible = !showJournalCheckBox.Visible;
            showProgressCheckBox.Visible = !showProgressCheckBox.Visible;
            showTakeCheckBox.Visible = !showTakeCheckBox.Visible;

            if (lTitle.Visible)
            {
                bHideInformation.Text = "Показать";
                iInformationHeight = questInformationBox.Height;
                questInformationBox.Height = 35;
            }
            else
            {
                bHideInformation.Text = "Скрыть";
                questInformationBox.Height = iInformationHeight;
            }
        }
        //! Кнопка Скрыть цели квеста
        private void bHideTarget_Click(object sender, EventArgs e)
        {
            ltargetResult.Visible = !ltargetResult.Visible;
            resultComboBox.Visible = !resultComboBox.Visible;
            dynamicCheckBox.Visible = !dynamicCheckBox.Visible;
            lQuantity.Visible = !lQuantity.Visible;
            quantityUpDown.Visible = !quantityUpDown.Visible;
            lNameObject.Visible = !lNameObject.Visible;
            targetComboBox.Visible = !targetComboBox.Visible;
            bTargetAddDynamic.Visible = !bTargetAddDynamic.Visible;
            labelTargetAttr.Visible = !labelTargetAttr.Visible;
            targetAttributeComboBox.Visible = !targetAttributeComboBox.Visible;
            bTargetClearDynamic.Visible = !bTargetClearDynamic.Visible;
            lTargetAttr1.Visible = !lTargetAttr1.Visible;
            targetAttributeComboBox2.Visible = !targetAttributeComboBox2.Visible;
            IsGroupCheckBox.Visible = !IsGroupCheckBox.Visible;
            isClanCheckBox.Visible = !isClanCheckBox.Visible;

            if (bHideTarget.Text == "Скрыть")
            {
                bHideTarget.Text = "Показать";
                iTargetHeight = targetBox.Height;
                targetBox.Height = 35;
            }
            else
            {
                bHideTarget.Text = "Скрыть";
                targetBox.Height = iTargetHeight;
            }
        }
        //! Кнопка Скрыть условия квеста
        private void bHideRules_Click(object sender, EventArgs e)
        {
            bItemQuestRules.Visible = !bItemQuestRules.Visible;
            labelScenarios.Visible = !labelScenarios.Visible;
            scenariosTextBox.Visible = !scenariosTextBox.Visible;
            massQuestsTextBox.Visible = !massQuestsTextBox.Visible;
            groupQuestRulesBox.Visible = !groupQuestRulesBox.Visible;

            if (bHideRules.Text == "Скрыть")
            {
                bHideRules.Text = "Показать";
                iRulesHeight = lQuestRules.Height;
                lQuestRules.Height = 35;
            }
            else
            {
                lQuestRules.Height = iRulesHeight;
                bHideRules.Text = "Скрыть";
            }
        }
        //! Кнопка Скрыть Награду квеста
        private void bHideReward_Click(object sender, EventArgs e)
        {
            lCredits.Visible = !lCredits.Visible;
            creditsTextBox.Visible = !creditsTextBox.Visible;
            lCombatSkills.Visible = !lCombatSkills.Visible;
            tExperience.Visible = !tExperience.Visible;
            lSurvivalSkills.Visible = !lSurvivalSkills.Visible;
            tSurvival.Visible = !tSurvival.Visible;
            lSupportSkills.Visible = !lSupportSkills.Visible;
            tSupport.Visible = !tSupport.Visible;
            lKarmaPK.Visible = !lKarmaPK.Visible;
            textBoxKarmaPK.Visible = !textBoxKarmaPK.Visible;

            if (lCredits.Visible)
            {
                bHideReward.Text = "Показать";
                iRewardHeight = rewardGroupBox.Height;
                rewardGroupBox.Height = 35;
            }
            else
            {
                bHideReward.Text = "Скрыть";
                rewardGroupBox.Height = iRewardHeight;
            }
        }
            
        private void QuestEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parent.Enabled = true;
        }

        private void bItemQID_Click(object sender, EventArgs e)
        {
            SelectQuestItem selectQItem = new SelectQuestItem(this, this.QuestID);
            this.Enabled = false;
        }
        //! Нажатие Репутация в наградах квеста - открывает окно редактирования репутаций
        private void bRewardReputation_Click(object sender, EventArgs e)
        {
            RewardFractions formFractions = new RewardFractions(this, ref this.editQuestReward.Reputation);
            formFractions.Visible = true;
            this.Enabled = false;
        }
        //! Изменение состояния квеста - Туториал или нет
        private void tutorialCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            showJournalCheckBox.Checked = !tutorialCheckBox.Checked;
            showJournalCheckBox.Enabled = !tutorialCheckBox.Checked;
        }
        //! Открывает окно редактирования эффектов (бафов)
        private void bRewardEffects_Click(object sender, EventArgs e)
        {
            EditDialogEffect edit_effect = new EditDialogEffect(this.parent, this, this.QuestID, ref this.editQuestReward.Effects);
            edit_effect.Visible = true;
            this.Enabled = false;
        }

        //! Нажатие Предметы в правилах квеста - открывает форму с редактором предметов
        private void bItemQuestRules_Click(object sender, EventArgs e)
        {
            ItemDialog itemDialog = new ItemDialog(this.parent, this, null, quest.QuestID, this.ITEM_QUESTRULES);
            itemDialog.Enabled = true;
            itemDialog.Visible = true;
            this.Enabled = false;
        }
        //! Нажатие Предметы в наградах квеста - открывает форму с редактором предметов
        private void bRewardItem_Click(object sender, EventArgs e)
        {
            ItemDialog itemDialog = new ItemDialog(this.parent, this, null, quest.QuestID, this.ITEM_REWARD);
            itemDialog.Enabled = true;
            itemDialog.Visible = true;
            this.Enabled = false;
        }

        private void nBaseToCapturePercent_ValueChanged(object sender, EventArgs e)
        {
            editQuestRules.basePercent = Convert.ToSingle(nBaseToCapturePercent.Value) / 100;
        }

        private void cbState_CheckedChanged(object sender, EventArgs e)
        {
                editTarget.usePercent = cbState.Checked;
                lState.Enabled = cbState.Checked;
                udState.Enabled = cbState.Checked;

        }

        private void cbMobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> levels = parent.mobConst.getLevelsOnDescription(cbMobType.SelectedItem.ToString());
            cbMobLevel.Items.Clear();
            foreach (string item in levels)
            {
                cbMobLevel.Items.Add(item.ToString());
            }  
        }

        private void cbMobUniq_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMobUniq.Checked)
            {
                nupMobCount.Value = 1;
                nupMobCount.Enabled = false;
            }
            else nupMobCount.Enabled = true;
        }

        private void bPenaltyItem_Click(object sender, EventArgs e)
        {
            ItemDialog itemDialog = new ItemDialog(this.parent, this, null, quest.QuestID, this.ITEM_PENALTY);
            itemDialog.Enabled = true;
            itemDialog.Visible = true;
            this.Enabled = false;
        }

        private void bPenaltyEffects_Click(object sender, EventArgs e)
        {
            EditDialogEffect edit_effect = new EditDialogEffect(this.parent, this, this.QuestID, ref this.editQuestPenalty.Effects);
            edit_effect.Visible = true;
            this.Enabled = false;
        }

        private void bPenaltyReputation_Click(object sender, EventArgs e)
        {
            RewardFractions formFractions = new RewardFractions(this, ref this.editQuestPenalty.Reputation);
            formFractions.Visible = true;
            this.Enabled = false;
        }
        
    }
}
