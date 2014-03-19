#define OLDVERSION
//#define OPERATOR2
//#define OPERATOR3
//#define OPERATOR4


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;
using System.Collections;
using System.Xml.Linq;
using System.Threading;

namespace StalkerOnlineQuesterEditor
{
    using NPCQuestDict = Dictionary<int, CDialog>;
    using StalkerOnlineQuesterEditor.Forms;
    //! Главная форма программы, туча строк кода
    public partial class MainForm : Form
    {
        //bool inSaving = false;

        int STARTTALKQUESTS = 2500;
        int ENDTALKQUESTS = 3500;

        //! Текущий выбранный NPC (в комбобоксе вверху)
        public string currentNPC="";
        //! Ссылка на экземпляр класса CDialogs, хранит все данные и функции по работе с диалогами
        CDialogs dialogs;
        //! Ссылка на экземпляр класса CQuests
        CQuests quests;
        NodeDragHandler Listener;
        MapNodeDragHandler MapListener;
        PLayer edgeLayer;
        PNodeList nodeLayer;

        Dictionary<PNode, GraphProperties> graphs = new Dictionary<PNode, GraphProperties>();
        Dictionary<Panel, int> panels = new Dictionary<Panel, int>();

        List<int> rootElements = new List<int>();
        public TreeView tree;
        List<PNode> subNodes = new List<PNode>();
        List<int> protectedTreeNode;
        Dictionary<LinkLabel,int> titles;

        public СQuestConstants questConst;
        public CItemConstants itemConst;
        public CNPCConstants npcConst;
        public CMobConstants mobConst;
        public CZoneConstants zoneConst;
        public CSpacesConstants spacesConst;
        public CTriggerConstants triggerConst;
        public CTPConstants tpConst;
        public CSettings settings;
        public COperNotes manageNotes;
        public CFracConstants fractions;
        public CGUIConst gui;
        public CEffectConstants effects;
        public CBalance balances;

        //! Дичайший костыль, хранит rootDialog, но называется CurrentQuestDialog
        int currentQuestDialog;
        //! Дичайший костыль, держит стартовые квесты в себе
        public string startQuests;
        public int currentQuest;

        public MainForm()
        {
            InitializeComponent();
            Listener = new NodeDragHandler(this);
            MapListener = new MapNodeDragHandler(this);
            settings = new CSettings(this);
            settings.checkMode();
            dialogs = new CDialogs(this);
            quests = new CQuests(this);
            tpConst = new CTPConstants();

            tree = treeDialogs;
            currentQuestDialog = new int();
            protectedTreeNode = new List<int>();
            questConst = new СQuestConstants();
            itemConst = new CItemConstants();
            npcConst = new CNPCConstants();
            spacesConst = new CSpacesConstants();
            triggerConst = new CTriggerConstants();
            manageNotes = new COperNotes("ManNotes.xml");
            fractions = new CFracConstants();
            gui = new CGUIConst();
            effects = new CEffectConstants();
            balances = new CBalance(this);

            treeQuest.AfterSelect += new TreeViewEventHandler(this.treeQuestSelected);
            fillNPCBOX();
            fillFractionBalance();
            DialogShower.AddInputEventListener(Listener);

            foreach (string name in dialogs.getListOfNPC())
                if (!npcConst.NPCs.Keys.Contains(name))
                 npcConst.NPCs.Add(name, new CNPCDescription(name));

            this.mobConst = new CMobConstants();
            this.zoneConst = new CZoneConstants();
        }

        //! Очищает данные о квестах - дерево квестов, комбобокс, подквесты
        void clearQuestTab()
        {
            treeQuest.Nodes.Clear();
            splitQuestsContainer.Panel2.Controls.Clear();
            QuestBox.Items.Clear();
        }

        //! Сменили NPC в комбобоксе выбора персонажа
        private void NPCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!NPCBox.SelectedItem.ToString().Equals(currentNPC))
            {
                clearQuestTab();
                currentNPC = NPCBox.SelectedItem.ToString();

                if (CentralDock.SelectedIndex == 0)
                {
                    fillQuestChangeBox(true);
                    QuestBox.Enabled = false;
                    bAddQuest.Enabled = false;
                    bRemoveQuest.Enabled = false;
                    bAddDialog.Enabled = false;
                    bEditDialog.Enabled = false;
                    bRemoveDialog.Enabled = false;
                    currentQuestDialog = int.Parse(startQuests.Split(':')[0].Trim());
                    QuestBox.Text = "Число квестов: " + quests.getCountOfQuests(currentNPC);
                    DialogSelected(true);
                }
                else if (CentralDock.SelectedIndex == 1)
                {
                    fillQuestChangeBox(false);
                    QuestBox.Text = "Пожалуйста, выберите квест";
                }
            }
        }
        //! Супер-мега костыль от мудаков. СтартКвест == RootDialog для персонажа
        void fillQuestChangeBox(bool onlyDialogs)
        {
            QuestBox.SelectedItem = null;
            QuestBox.Items.Clear();
            startQuests = "";
            foreach (CQuest quest in quests.getQuestAndTitleOnNPCName(currentNPC))
            {
                if (!onlyDialogs)
                {
                    if (quest.QuestID < STARTTALKQUESTS || quest.QuestID > ENDTALKQUESTS)
                    {
                        QuestBox.Items.Add(quest.QuestID + ": " + quest.QuestInformation.Title);
                        startQuests += quest.QuestID + ": ";
                    }
                }
                else if (quest.QuestID >= STARTTALKQUESTS && quest.QuestID < ENDTALKQUESTS)
                    startQuests += quest.QuestID + ": ";
            }
        }

        //! Что за хуйня здесь ????
        private void QuestBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitQuestsContainer.Panel2.Controls.Clear();
            treeQuest.Nodes.Clear();

            if (CentralDock.SelectedIndex == 1)
            {
                currentQuestDialog = int.Parse(startQuests.Split(':')[QuestBox.SelectedIndex].Trim());
                addNodeOnTreeQuest(this.currentQuestDialog);
                bRemoveQuest.Enabled = true;
            }
            treeQuest.ExpandAll();
        }

        //! Заполнение итемов в комбобоксе NPC
        void fillNPCBOX()
        {
            this.NPCBox.Items.Clear();
            foreach (string holder in this.dialogs.dialogs.Keys)
            {
                this.NPCBox.Items.Add(holder.ToString());
                this.NPCBox.Sorted = true;
            }
            this.NPCBox.Text = "Пожалуйста, выберите NPC.";
                       
            QuestBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            QuestBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            QuestBox.AutoCompleteCustomSource.AddRange(quests.getQuestsIDasString());
        }

        void protectNPCBoxQuest(CDialog quest)
        {
            if (!protectedTreeNode.Contains(quest.DialogID))
            {
                protectedTreeNode.Add(quest.DialogID);
                if (quest.Nodes.Any())
                    foreach (int subquest in quest.Nodes)
                        protectNPCBoxQuest(getDialogOnDialogID(subquest));
            }
        }

        void fillNPCBoxSubquests(CDialog sub)
        {
            foreach (int subdialog in sub.Nodes)
            {
                foreach (TreeNode treeNode in this.treeDialogs.Nodes.Find("Active", true))
                {
                    //System.Console.WriteLine("Write dialog:" + subdialog.ToString());
                    if (!treeNode.Nodes.ContainsKey(subdialog.ToString()))
                    {
                        treeNode.Nodes.Add(subdialog.ToString(), subdialog.ToString());
                        dialogs.dialogs[currentNPC][subdialog].coordinates.Active = true;
                    }
                }
                this.fillNPCBoxSubquests(this.dialogs.dialogs[currentNPC][subdialog]);
            }
        }

        internal void onSelectNode(int dialogID)
        {
            bAddDialog.Enabled = true;
            bEditDialog.Enabled = true;
            if (!isRoot(dialogID))
                bRemoveDialog.Enabled = true;
        }

        public void selectNodeOnDialogTree(int dialogID)
        {
            //treeDialogs.Focus();
            foreach (TreeNode node in treeDialogs.Nodes.Find(dialogID.ToString(), true))
                treeDialogs.SelectedNode = node;
        }

        internal void onDeselectNode()
        {
            bRemoveDialog.Enabled = false;
            bEditDialog.Enabled = false;
        }

        private void bRemoveDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (Listener.curNode != null)
                    removeNodeFromDialogGraphView(getDialogIDOnNode(Listener.curNode));
                else
                    removeDialog(int.Parse(treeDialogs.SelectedNode.Text));

                bRemoveDialog.Enabled = false;
                bEditDialog.Enabled = false;
            }
        }

        public bool isRoot(int dialogID)
        {
            return rootElements.Contains(dialogID);
        }

        public bool isRoot(PNode node)
        {
            return isRoot(getDialogIDOnNode(node));
        }

        private void treeDialogs_GotFocus(object sender, TreeViewEventArgs e)
        {
            if ((treeDialogs.SelectedNode.Text != "Active") && (treeDialogs.SelectedNode.Text != "Recycle"))
            {
                int treeID = int.Parse(treeDialogs.SelectedNode.Text);

                if ((treeID != 0) && (!Listener.getCurDialogID().Equals(treeID)))
                {
                    deselectSubNodesDialogGraphView();
                    if (treeDialogs.SelectedNode.Parent.Text == "Active")
                    {
                        foreach (PNode node in DialogShower.Layer.AllNodes)
                            if (getDialogIDOnNode(node).Equals(treeID))
                                Listener.setCurrentNode(treeID);
                    }
                    else if (treeDialogs.SelectedNode.Parent.Text == "Recycle")
                    {
                        onSelectNode(treeID);
                        Listener.setCurrentNode(0);
                    }
                }
            }
        }

        private void bAddDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (!treeDialogs.SelectedNode.ToString().Equals("Recycle") && !treeDialogs.SelectedNode.ToString().Equals("Active"))
                {
                    if (!(Listener.getCurDialogID() == 0))
                    {
                        EditDialogForm editDialogForm = new EditDialogForm(true, this, int.Parse(treeDialogs.SelectedNode.Text));
                        editDialogForm.Visible = true;
                    }
                    else
                    {
                        AddPassiveDialogForm AddPassiveDialog = new AddPassiveDialogForm(this, int.Parse(treeDialogs.SelectedNode.Text));
                        AddPassiveDialog.Visible = true;
                    }
                }
            }
        }

        private void bEditDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                EditDialogForm editDialogForm = new EditDialogForm(false, this, int.Parse(treeDialogs.SelectedNode.Text));
                editDialogForm.Visible = true;
            }
            else
            {
                LocaleDialogForm editLocaleDialogForm = new LocaleDialogForm(this, int.Parse(treeDialogs.SelectedNode.Text));
                editLocaleDialogForm.Visible = true;
            }
        }

        void removeDialog(int dialogID)
        {
            dialogs.dialogs[currentNPC].Remove(dialogID);
            foreach (CDialog dialog in dialogs.dialogs[currentNPC].Values)
                if (dialog.Actions.ToDialog == dialogID)
                    dialog.Actions.ToDialog = 0;
            CDialog rootDialog = getRootDialog();
            if (rootDialog!= null)
                fillDialogTree(rootDialog, this.dialogs.dialogs[currentNPC]);
        }

        //! Старует эмулятор диалога (язык диалога зависит от режима, непереведенные фрагменты помечаются красным)
        public void startEmulator(int dialogID)//, bool isHandle)
        {
            // получаем фразу NPC
            CDialog rootDialog = getDialogOnIDConditional(dialogID);
            EmulatorsplitContainer.Panel2.Controls.Clear();
            titles = new Dictionary<LinkLabel,int>();
            Label NPCText = new Label();
            NPCText.Text = rootDialog.Text;
            NPCText.ForeColor = (rootDialog.version != 0) ? (Color.Black) : (Color.Red);
            NPCText.AutoSize = false;
            NPCText.AutoEllipsis = true;
            
            //NPCText.Size = new Size(30, 30);
            NPCText.Dock = DockStyle.Top;
            
            foreach (int dialog in rootDialog.Nodes)
            {
                LinkLabel dialogLink = new LinkLabel();
                dialogLink.LinkClicked += new LinkLabelLinkClickedEventHandler(dialogLink_LinkClicked);
                string openedQuests = "";
                string closedQuests = "";

                foreach (int quid in dialogs.dialogs[currentNPC][dialog].Actions.CompleteQuests)
                    if (closedQuests == "")
                        closedQuests += quid.ToString();
                    else
                        closedQuests += "," + quid.ToString();

                foreach (int quid in dialogs.dialogs[currentNPC][dialog].Actions.GetQuests)
                    if (openedQuests == "")
                        openedQuests += quid.ToString();
                    else
                        openedQuests += "," + quid.ToString();

                string actionResult = "";
                if (openedQuests != "")
                    actionResult += " Взять:" + openedQuests;
                if (closedQuests != "")
                    actionResult += " Закрыть:" + closedQuests;
                if (actionResult != "")
                    actionResult = "(" + actionResult + ")";


                //dialogLink.Text = dialog + ". " + dialogs.dialogs[currentNPC][dialog].Title + actionResult;
                CDialog answer = getDialogOnIDConditional(dialog);
                dialogLink.Text = dialog + ". " + answer.Title + actionResult;
                dialogLink.BackColor = (answer.version != 0) ? (Color.FromKnownColor(KnownColor.Transparent)) : (Color.FromArgb(0x7FAA45E0));             
                dialogLink.AutoSize = true;
                dialogLink.Dock = DockStyle.Top;
                dialogLink.Links.Add(0, 0, dialog);
                titles.Add(dialogLink,dialog);
                EmulatorsplitContainer.Panel2.Controls.Add(dialogLink);
            }
            EmulatorsplitContainer.Panel2.Controls.Add(NPCText);
        }

        public bool isDialogActive(int dialogID)
        {
            foreach (TreeNode node in treeDialogs.Nodes.Find("Active",true)[0].Nodes)
                if (int.Parse(node.Name).Equals(dialogID))
                    return true;
            return false;
        }

        //! Отработка клика по ответу в эмуляторе диалогов, вывод результатов в статусбар и переход к след диалогу
        private void dialogLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            int choosenDialogID = (int)e.Link.LinkData;
            CDialog choosenDialog = dialogs.dialogs[currentNPC][choosenDialogID];
            toolStripStatusLabel.Text = "";
            if (choosenDialog.Actions.CompleteQuests.Any())
                toolStripStatusLabel.Text += "Завершенные: " + getListAsString(choosenDialog.Actions.CompleteQuests);
            if (choosenDialog.Actions.GetQuests.Any())
                toolStripStatusLabel.Text += " Взятые: " + getListAsString(choosenDialog.Actions.GetQuests);

            if (choosenDialog.Actions.Event == (int) DialogEvents.trade)
                addActionTextToEmulator(" Торговля. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Event == (int) DialogEvents.change)
                addActionTextToEmulator(" Обмен. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Event ==(int) DialogEvents.repair)
                addActionTextToEmulator(" Починка. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Event == (int)DialogEvents.complex_repair)
                addActionTextToEmulator(" Комплексная починка. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Event == (int)DialogEvents.teleport)
                addActionTextToEmulator(" Телепорт. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Event == (int)DialogEvents.clan_base)
                addActionTextToEmulator(" Телепорт на базу. Выход.", choosenDialogID);
            else if (choosenDialog.Actions.Exit)
                addActionTextToEmulator(" Выход.", choosenDialogID);

            else if (choosenDialog.Actions.ToDialog != 0)
            {
                toolStripStatusLabel.Text += " Переход на диал.: " + choosenDialog.Actions.ToDialog.ToString();
                //startEmulator(choosenDialog.Actions.ToDialog, false);
                Listener.setCurrentNode(choosenDialog.Actions.ToDialog);
            }
            else
            {
                //startEmulator(choosenDialogID,false);
                Listener.setCurrentNode(choosenDialogID);
            }            
        }
        //! Антиговнокод - добавлние примечания к фразе диалога с действием
        void addActionTextToEmulator(string text, int dialogID)
        {
            toolStripStatusLabel.Text += text;
            EmulatorsplitContainer.Panel2.Controls.Clear();
            Listener.setCurrentNode(dialogID);
        }
        //! Выводит координаты узла как прямоугольника. Для отладки.
        public void setXYCoordinates(float X, float Y, float w, float h)
        {
            this.labelXNode.Text = "X=" + X.ToString();
            this.labelYNode.Text = "Y=" + Y.ToString();
            this.labelXNode.Text += " w=" + w.ToString();
            this.labelYNode.Text += " h=" + h.ToString();
        }

        //! Вовзаращает список как строку со значениями через запятую
        string getListAsString(List<int> list)
        {
            string str = "";
            foreach (int element in list)
            {
                if (str.Equals(""))
                    str += element.ToString();
                else
                    str += "," + element.ToString();
            }
            return str;
        }

        public void clearToolstripLabel()
        {
            toolStripStatusLabel.Text = "";
        }

        //! Устанавливает доступность компонентов формы
        private void SetControlsAbility(bool enable)
        {
            NPCBox.Enabled = enable;
            bAddNPC.Enabled = enable;
            bDelNPC.Enabled = enable;
            QuestBox.Enabled = enable;
            bRemoveQuest.Enabled = enable;
            bAddQuest.Enabled = enable;
        }
        //! Блокировка компонентов и обновление данных при смене вкладки на форме
        private void onSelectTab(object sender, EventArgs e)
        {
            bCopyEvents.Enabled = false;
            int index = CentralDock.SelectedIndex;
            switch (index)
            {
                case 0:         // вкладка Диалоги
                    SetControlsAbility(true);
                    if (!currentNPC.Equals(""))
                    {
                        QuestBox.Items.Clear();
                        fillQuestChangeBox(true);
                        QuestBox.Enabled = false;
                        bRemoveQuest.Enabled = false;
                        bAddQuest.Enabled = false;
                        QuestBox.Text = "Число квестов: " + quests.getCountOfQuests(currentNPC);
                        currentQuestDialog = int.Parse(startQuests.Split(':')[0].Trim());
                        DialogSelected(true);
                    }
                    break;
                case 1:         // вкладка Квесты
                    SetControlsAbility(true);
                    bRemoveQuest.Enabled = false;
                    QuestBox.Items.Clear();
                    fillQuestChangeBox(false);
                    QuestBox.Text = "Пожалуйста, выберите квест";
                    break;                    
                case 2:         // Вкладка Связи NPC 
                    SetControlsAbility(false);
                    NPCBox.Enabled = true;
                    fillNPCLinkView();
                    break;                
                case 3: case 5: case 6:     // Вкладки Проверка, Перевод, Баланс
                    SetControlsAbility(false);
                    break;
                case 4:         // Вкладка Управление (квестами)
                    FillTabManage();
                    break;
            }
        }

        public CQuest getQuestOnQuestID(int questID)
        {
            return quests.getQuest(questID);
        }

        void addNodeOnTreeQuest(int currentQuest)
        {
            CQuest curQuest = getQuestOnQuestID(currentQuest);

            if (!curQuest.Additional.IsSubQuest.Equals(0))
            {
                TreeNode[] nodes = treeQuest.Nodes.Find(getQuestOnQuestID(curQuest.Additional.IsSubQuest).QuestID.ToString(), true);
                if (nodes.Any())
                {
                    TreeNode parent = nodes[0];
                    parent.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                    int lastIndex = parent.Nodes.Count - 1;
                    if (curQuest.Target.onFin == 1)
                        parent.Nodes[lastIndex].BackColor = Color.Green;
                    else
                        parent.Nodes[lastIndex].BackColor = Color.Red;
                }
            }
            else 
            {
                treeQuest.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                int lastIndex = treeQuest.Nodes.Count - 1;
                if (curQuest.Target.onFin == 1)
                    treeQuest.Nodes[lastIndex].BackColor = Color.Green;
                else
                    treeQuest.Nodes[lastIndex].BackColor = Color.Red;
            }
                //if (!questConst.isSimple(curQuest.Target.QuestType) && (curQuest.Additional.ListOfSubQuest.Any()))
            if ( (!questConst.isSimple(curQuest.Target.QuestType) || settings.getMode() == settings.MODE_LOCALIZATION)
                    && ( curQuest.Additional.ListOfSubQuest.Any() ) )
                    foreach (int subquest in curQuest.Additional.ListOfSubQuest)
                        addNodeOnTreeQuest(subquest);
        }

        void createQuestPanels(int questID)
        {
            CQuest quest = getQuestOnQuestID(questID);

            Panel questPanel = new Panel();
            questPanel.AutoSize = true;
            questPanel.Dock = DockStyle.Top;
            questPanel.Size = new Size(splitQuestsContainer.Panel2.Width - 5, 100);
            questPanel.BorderStyle = BorderStyle.FixedSingle;

            GroupBox questBox = new GroupBox();
            questBox.AutoSize = true;
            questBox.Text = "Квест ID: " + questID;
            questBox.Dock = DockStyle.Top;

            Label eventLabel = new Label();
            eventLabel.Text = "Событие: "  + questConst.getDescription(quest.Target.QuestType); ;
            eventLabel.Dock = DockStyle.Top;
            questBox.Controls.Add(eventLabel);
            questBox.Controls.SetChildIndex(eventLabel,3);

            GroupBox infoBox = new GroupBox();
            infoBox.AutoSize = true;
            infoBox.Text = "Информация";
            infoBox.Dock = DockStyle.Top;

            Label titleLabel = new Label();
            titleLabel.Text = "Заголовок:" + quest.QuestInformation.Title;
            titleLabel.Dock = DockStyle.Top;
            infoBox.Controls.Add(titleLabel);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Описание:" + quest.QuestInformation.Description;
            descriptionLabel.Dock = DockStyle.Top;
            infoBox.Controls.Add(descriptionLabel);

            questBox.Controls.Add(infoBox);
            questBox.Controls.SetChildIndex(infoBox,0);

            questPanel.Controls.Add(questBox);

            questBox.Controls.SetChildIndex(eventLabel, 1);
            panels.Add(questPanel, questID);

            if (quest.Additional.ListOfSubQuest.Any())
                foreach (int subquest in quest.Additional.ListOfSubQuest)
                    createQuestPanels(subquest);
        }

        private void treeQuestSelected(object sender, EventArgs e)
        {
            this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);
//            int questID = int.Parse(treeQuest.SelectedNode.Text);
            checkQuestButton(getQuestOnQuestID(currentQuest).Target.QuestType, currentQuest);
            fillQuestPanel();
        }

        void fillQuestPanel()
        {
            panels.Clear();
            splitQuestsContainer.Panel2.Controls.Clear();
            createQuestPanels(currentQuest);
            foreach (int subquest in getQuestOnQuestID(currentQuest).Additional.ListOfSubQuest)
            {
                Panel panel = getQuestPanelOnQuestID(subquest);
                splitQuestsContainer.Panel2.Controls.Add(panel);
                splitQuestsContainer.Panel2.Controls.SetChildIndex(panel, 0);
            }
            Panel Mpanel = getQuestPanelOnQuestID(currentQuest);

            splitQuestsContainer.Panel2.Controls.Add(Mpanel);
            splitQuestsContainer.Panel2.Controls.SetChildIndex(Mpanel, getQuestOnQuestID(currentQuest).Additional.ListOfSubQuest.Count);
        }


        Panel getQuestPanelOnQuestID(int questID)
        {
            foreach (Panel panel in panels.Keys)
                if (panels[panel] == questID)
                    return panel;
            return null;
        }

        private void treeQuestClicked(object sender, EventArgs e)
        {

            bCopyEvents.Enabled = true;
            //this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);
            //System.Console.WriteLine("Clicked on quest: "+currentQuest);
            if (getQuestOnQuestID(currentQuest).Additional.IsSubQuest == 0)
            {
                //openQuestEditForm(false);
                EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 2);
                questEditor.Visible = true;
                this.Enabled = false;
            }
            else
            {
                EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 3);
                questEditor.Visible = true;
                this.Enabled = false;
            }
        }

        //! Сохранение - почему-то общее для всех.
        private void bSaveDialogs_Click(object sender, EventArgs e)
        {
            this.saveData();
        }
        private void bSaveQuests_Click(object sender, EventArgs e)
        {
            this.saveData();
        }
        private void bSaveLocale_Click(object sender, EventArgs e)
        {
            this.saveData();
        }

        private void bAddNPC_Click(object sender, EventArgs e)
        {
            NewNPC newNPC = new NewNPC(this);
            newNPC.Visible = true;
        }

        int getStartQuestDialogsID()
        {
            //int iFirstDialogID = 0;
            //while (iFirstDialogID < STARTTALKQUESTS)
            int iFirstDialogID = 2500 + (this.settings.getOperatorNumber() * 20);

            for (int startQuest = iFirstDialogID; ; startQuest++)
                if (!quests.startQuests.Contains(startQuest))
                    return startQuest;
        }

        public void addNewNPC(string Name)
        {
            int newNPCID = getStartQuestDialogsID();
            Dictionary<int,CDialog> firstDialog = new Dictionary<int,CDialog>();
            int dialogID = getDialogsNewID();
            firstDialog.Add(dialogID,new CDialog(Name,"","",newNPCID,new CDialogPrecondition(),new Actions(),new List<int>(),dialogID, 0, new NodeCoordinates()));

            dialogs.dialogs.Add(Name, firstDialog);
            quests.quest.Add(newNPCID, new CQuest(newNPCID, 0, new CQuestInformation(), new CQuestPrecondition(), new CQuestRules(), new CQuestReward(), new CQuestAdditional(Name), new CQuestTarget(1), new CQuestPenalty()));
            quests.startQuests.Add(newNPCID);

            NPCBox.Items.Add(Name);
            //! выбирает левого NPC после создания нового - править
            NPCBox.SelectedIndex = NPCBox.Items.Count - 1;

            npcConst.NPCs.Add(Name, new CNPCDescription(Name));
        }

        private void bDelNPC_Click(object sender, EventArgs e)
        {
            graphs.Clear();
            treeDialogs.Nodes.Clear();
            DialogShower.Layer.RemoveAllChildren();

            List<int> removedItems = new List<int>();
            foreach (CQuest quest in quests.quest.Values)
                if (quest.Additional.Holder.Equals(currentNPC))
                {
                    if (quest.QuestID >= STARTTALKQUESTS && quest.QuestID < ENDTALKQUESTS)
                        quests.startQuests.Remove(quest.QuestID);
                    removedItems.Add(quest.QuestID);
                }

            foreach (int item in removedItems)
                quests.quest.Remove(item);
            dialogs.dialogs.Remove(currentNPC);
            fillNPCBOX();
        }

        public void checkQuestButton(int questType, int questID)
        {
            bEditEvent.Enabled = true;
            if (questConst.isSimple(questType))// && getQuestOnQuestID(questID).Additional.ListOfSubQuest.Any())
                bAddEvent.Enabled = false;
            else
                bAddEvent.Enabled = true;

            if (getQuestOnQuestID(questID).Additional.IsSubQuest.Equals(0) || (!questConst.isSimple(questType) && getQuestOnQuestID(questID).Additional.ListOfSubQuest.Any()))
                bRemoveEvent.Enabled = false;
            else
                bRemoveEvent.Enabled = true;

            if (getQuestOnQuestID(questID).Additional.IsSubQuest != 0)
            {
                CQuest parent = getQuestOnQuestID(getQuestOnQuestID(questID).Additional.IsSubQuest);

                if (parent.Additional.ListOfSubQuest.Any())
                {
                    if (parent.Additional.ListOfSubQuest.Last().Equals(questID))
                        bQuestDown.Enabled = false;
                    else
                        bQuestDown.Enabled = true;

                    if (parent.Additional.ListOfSubQuest.First().Equals(questID))
                        bQuestUp.Enabled = false;
                    else
                        bQuestUp.Enabled = true;
                }
            }
        }

        public int getQuestNewID()
        {
            int iFirstQuestID = 1 + this.settings.getOperatorNumber() * 400;
            for (int questi = iFirstQuestID; ; questi++)
                if (!quests.quest.Keys.Contains(questi) && !quests.buffer.Keys.Contains(questi))
                    return questi;
        }

        public int getQuestNewID(List<int> iLists)
        {
            int iFirstQuestID = 1 + this.settings.getOperatorNumber() * 400;
            for (int questi = iFirstQuestID; ; questi++)
                if (!quests.quest.Keys.Contains(questi) && !quests.buffer.Keys.Contains(questi) && !iLists.Contains(questi))
                    return questi;
        }

        public void addQuest(CQuest quest, int parent)
        {
            //System.Console.WriteLine("addQuest: "+ quest.QuestID);
            quests.quest[parent].Additional.ListOfSubQuest.Add(quest.QuestID);
            quests.quest.Add(quest.QuestID, quest);
            checkQuestButton(quest.Target.QuestType, quest.QuestID);

            treeQuest.Nodes.Find(parent.ToString(), true)[0].Nodes.Add(quest.QuestID.ToString(), quest.QuestID.ToString());
            treeQuest.ExpandAll();
        }

        public void replaceQuest(CQuest quest)
        {
            //CQuest replacedQuest = quests.quest[quest.QuestID];
            //quests.quest[quest.QuestID] = quest;
            quests.replaceQuest(quest);
            checkQuestButton(quest.Target.QuestType, quest.QuestID);
        }

        private void bAddEvent_Click(object sender, EventArgs e)
        {
            EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 4);
            questEditor.Visible = true;
            this.Enabled = false;
        }

        private void bEditEvent_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (getQuestOnQuestID(currentQuest).Additional.IsSubQuest == 0)
                {
                    //openQuestEditForm(false);
                    EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 2);
                    questEditor.Visible = true;
                    this.Enabled = false;
                }
                else
                {
                    EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 3);
                    questEditor.Visible = true;
                    this.Enabled = false;
                }
            }
            else
            {
                LocaleQuestForm quest_form = new LocaleQuestForm(this, currentQuest);
                quest_form.Visible = true;
                this.Enabled = false;
            }
        }

        void saveData()
        {
            this.Enabled = false;
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                quests.saveQuests(settings.questXML);
                quests.saveStartQuests(settings.startQuestXML);
                dialogs.saveDialogs(settings.dialogXML);
            }
            else
            {
                dialogs.saveLocales(settings.dialogXML);
                quests.saveLocales(settings.questXML);
            }
            Thread.Sleep(1000);
            toolStripStatusLabel.Text = "Данные успешно сохранены.";
            this.Enabled = true;
        }

        private void bRemoveEvent_Click(object sender, EventArgs e)
        {
            this.removeQuest(currentQuest);
        }

        void removeQuest(int questID)
        {
            foreach (int subquest in getQuestOnQuestID(questID).Additional.ListOfSubQuest)
                removeQuest(subquest);

            if (getQuestOnQuestID(questID).Additional.IsSubQuest!=0)
                quests.quest[getQuestOnQuestID(questID).Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);
            treeQuest.Nodes.Find(questID.ToString(), true)[0].Remove();
            quests.quest.Remove(questID);
        }

        private void bQuestUp_Click(object sender, EventArgs e)
        {
            int temp;
            CQuest parent = getQuestOnQuestID(getQuestOnQuestID(currentQuest).Additional.IsSubQuest);
            for (int i=0;i<parent.Additional.ListOfSubQuest.Count;i++)
                if (parent.Additional.ListOfSubQuest[i] == currentQuest)
                {
                    temp = parent.Additional.ListOfSubQuest[i - 1];
                    parent.Additional.ListOfSubQuest[i - 1] = currentQuest;
                    parent.Additional.ListOfSubQuest[i] = temp;

                    treeQuest.Nodes.Find(parent.QuestID.ToString(), true)[0].Nodes.Clear();
                    foreach (int subquests in parent.Additional.ListOfSubQuest)
                        addNodeOnTreeQuest(subquests);
                    treeQuest.ExpandAll();
                    findAndSelectNodeOnTreeQuest(currentQuest);

                    break;
                }
        }

        private void bQuestDown_Click(object sender, EventArgs e)
        {
            int temp;
            CQuest parent = getQuestOnQuestID(getQuestOnQuestID(currentQuest).Additional.IsSubQuest);
            for (int i = 0; i < parent.Additional.ListOfSubQuest.Count; i++)
                if (parent.Additional.ListOfSubQuest[i] == currentQuest)
                {
                    temp = parent.Additional.ListOfSubQuest[i + 1];
                    parent.Additional.ListOfSubQuest[i + 1] = currentQuest;
                    parent.Additional.ListOfSubQuest[i] = temp;

                    treeQuest.Nodes.Find(parent.QuestID.ToString(), true)[0].Nodes.Clear();
                    foreach (int subquests in parent.Additional.ListOfSubQuest)
                        addNodeOnTreeQuest(subquests);
                    treeQuest.ExpandAll();
                    findAndSelectNodeOnTreeQuest(currentQuest);
                    break;
                }
        }

        void findAndSelectNodeOnTreeQuest(int questID)
        {
            treeQuest.SelectedNode = treeQuest.Nodes.Find(questID.ToString(), true)[0];
        }

        private void bRemoveQuest_Click(object sender, EventArgs e)
        {
            int removedQuest = int.Parse(QuestBox.SelectedItem.ToString().Split(':')[0].Trim());
            QuestBox.Items.Remove(QuestBox.SelectedItem);
            removeQuest(removedQuest);
        }

        private void bAddQuest_Click(object sender, EventArgs e)
        {
            EditQuestForm newQuest = new EditQuestForm(this, getQuestNewID(), 1); //, true, true);
            newQuest.Visible = true;
            this.Enabled = false;
        }

        public void createNewQuest(CQuest newQuest)
        {
            quests.quest.Add(newQuest.QuestID, newQuest);
            QuestBox.Items.Add(newQuest.QuestID.ToString() + ": " + newQuest.QuestInformation.Title);
            startQuests += newQuest.QuestID.ToString() + ": ";
            QuestBox.SelectedIndex = QuestBox.Items.Count - 1;
        }
 
        public void setQuestISClan(int questID, bool ClanState)
        {
            quests.setClan(questID, ClanState);
        }

        List<CQuest> getTreeItemIDs(int questID)
        {
            List<CQuest> ret = new List<CQuest>();
            CQuest quest = getQuestOnQuestID(questID);
            if (quest.Reward.AttrOfItems.Contains(1) || quest.QuestRules.AttrOfItems.Contains(1))
                ret.Add(quest);
            foreach (int subquestID in quest.Additional.ListOfSubQuest)
                ret.AddRange(getTreeItemIDs(subquestID));
            return ret;
        }


        public List<CQuest> getTreesItemIDs(int questID)
        {
            CQuest quest = getQuestOnQuestID(questID);
            if (quest != null)
            {
                if (!quest.Additional.IsSubQuest.Equals(0))
                    return getTreesItemIDs(quest.Additional.IsSubQuest);
                else
                    return getTreeItemIDs(questID);
            }
            else return new List<CQuest>();
        }
        public List<int> getSubIDs(int questID)
        {
            List<int> ret = new List<int>();
            CQuest quest = getQuestOnQuestID(questID);
            if (quest != null)
            {
                ret.Add(questID);
                foreach (int subquestID in quest.Additional.ListOfSubQuest)
                    ret.AddRange(getSubIDs(subquestID));
            }
            return ret;
        }
        
        //! Заполняет вкладку Управление
        void FillTabManage()
        {
            manageGridView.Rows.Clear();
            
            ((DataGridViewComboBoxColumn)manageGridView.Columns[18]).Items.Clear();
            ((DataGridViewComboBoxColumn)manageGridView.Columns[18]).Items.Add("Да.");
            ((DataGridViewComboBoxColumn)manageGridView.Columns[18]).Items.Add("Нет.");
            ((DataGridViewComboBoxColumn)manageGridView.Columns[18]).Items.Add("Правка.");

            foreach (string npcName in npcConst.NPCs.Keys)
                foreach (CQuest quest in quests.quest.Values)
                    if (quest.Additional.Holder == npcName && (quest.Additional.IsSubQuest==0) && (quest.QuestID > ENDTALKQUESTS || quest.QuestID < STARTTALKQUESTS))
                    {

                        string id = quest.QuestID.ToString();
                        List<int> iSubIDS = getSubIDs(quest.QuestID);
                        List<string> sNPCLink = new List<string>();
                        int rewardExpBattle = 0;
                        int rewardExpSurvival = 0;
                        int rewardExpSupport = 0;
                        float rewardCredits = 0;
                        Dictionary<int, int> rewardItems = new Dictionary<int, int>();
                        int pkStatus = 0;

                        foreach (int quid in iSubIDS)
                        {
                            CQuest q = getQuestOnQuestID(quid);

                            //if (q.Reward.Expirience.Any())
                            //{
                            //    rewardExpBattle += q.Reward.Expirience[0];
                            //    rewardExpSurvival += q.Reward.Expirience[1];
                            //    rewardExpSupport += q.Reward.Expirience[2];
                            //}

                            rewardCredits += q.Reward.Credits;
                            pkStatus += q.Reward.KarmaPK;

                            for (int index = 0; index < q.Reward.TypeOfItems.Count; ++index)
                            {
                                int type = q.Reward.TypeOfItems[index];
                                int count = q.Reward.NumOfItems[index];
                                int attr = 0;
                                if (q.Reward.AttrOfItems.Any())
                                     attr = q.Reward.AttrOfItems[index];
                                if (attr == 0)
                                {
                                    if (rewardItems.Keys.Contains(type))
                                        rewardItems[type] += count;
                                    else
                                        rewardItems[type] = count;
                                }
                            }
                        }

                        string npcLinks = "";
                        string getDialogs = "";
                        string subIDs = getListAsString(iSubIDS);
                        string title = quest.QuestInformation.Title;
                        string description = quest.QuestInformation.Description;
                        string npcNe = quest.Additional.Holder;
                        string srewardExpBattle = rewardExpBattle.ToString();
                        string srewardSurvival = rewardExpSurvival.ToString();
                        string srewardExpSupport = rewardExpSupport.ToString();
                        string srewardCredits = rewardCredits.ToString();
                        string sPkStatus = pkStatus.ToString();
                        string sRewardItem = "";
                        string sRepeat = quest.Precondition.Repeat.ToString();
                        string sPeriod = quest.Precondition.TakenPeriod.ToString();

                        string sLevel = "";
                        string sAuthor = "";
                        string sLegend = "";
                        string sWorked = "Нет.";

                        COperNote note = manageNotes.getNote(quest.QuestID);
                        if (note != null)
                        {
                            sAuthor = note.iOperator;
                            sLegend = note.sHistory;
                            sLevel = note.iLevel;
                            if (note.iWorked == 1)
                                sWorked = "Да.";
                            else if (note.iWorked == 2)
                                sWorked = "Правка.";
                        }   

                        //NPC dialog statistic
                        foreach (KeyValuePair<string, Dictionary<int, CDialog>> dialogNPC in dialogs.dialogs)
                        {
                            foreach (KeyValuePair<int, CDialog> dialog in dialogNPC.Value)
                            {
                                if (dialog.Value.Holder == npcName)
                                    if (dialog.Value.Actions.GetQuests.Contains(quest.QuestID))
                                    {
                                        if (getDialogs == "")
                                            getDialogs += dialog.Value.DialogID.ToString();
                                        else
                                            getDialogs += ("," + dialog.Value.DialogID.ToString());
                                    }
                                if (!sNPCLink.Contains(dialog.Value.Holder) && dialog.Value.Holder != npcNe)
                                {
                                    foreach (int q in dialog.Value.Actions.CompleteQuests)
                                        if ((iSubIDS.Contains(q)) && (!sNPCLink.Contains(dialog.Value.Holder)))
                                                sNPCLink.Add(dialog.Value.Holder);
                                }
                            }

                        }
                        //NPC dialog statistic

                        foreach (string npc in sNPCLink)
                        {
                            if (npcLinks == "")
                                npcLinks += npc;
                            else
                                npcLinks += "," + npc;
                        }

                        //rewardItem
                        
                        foreach (int type in rewardItems.Keys)
                        {
                            string itemName = itemConst.getDescriptionOnID(type);
                            string count = rewardItems[type].ToString();

                            if (sRewardItem == "")
                                sRewardItem += itemName + ":" + count.ToString();
                            else
                                sRewardItem += "\n" + itemName + ":" + count.ToString();
                        }
                        object[] row = { id, subIDs, title, description, npcNe, npcLinks, getDialogs, srewardExpBattle, srewardSurvival, srewardExpSupport, srewardCredits, sRewardItem, sPkStatus, sRepeat, sPeriod, sLevel, sAuthor, sLegend, sWorked };
                        manageGridView.Rows.Add(row);
                    }
        }

        private void bSaveManage_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in manageGridView.Rows)
            {
                try
                {
                    int questID = int.Parse(row.Cells[0].FormattedValue.ToString());
                    string level = row.Cells[15].FormattedValue.ToString();
                    string author = row.Cells[16].FormattedValue.ToString();
                    string history = row.Cells[17].FormattedValue.ToString();
                    string worked = row.Cells[18].FormattedValue.ToString();

                    int iWorked = 0;
                    if (worked == "Да.")
                        iWorked = 1;
                    else if (worked == "Правка.")
                        iWorked = 2;
                    if (level != "" || author != "" || history != "" || iWorked != 0)
                        manageNotes.addNote(questID, level, iWorked, author, history);
                }
                catch
                {
                    System.Console.WriteLine("Can't parse row");
                }
            }
            manageNotes.save();
        }


//*************************************************BUFFER'S WORK*******************


        //private List<CQuest> getQuestsOnID(int questID)
        //{
        //    List<CQuest> ret = new List<CQuest>();
        //    CQuest quest = getQuestOnQuestID(questID);
        //    if (quest != null)
        //    {
        //        ret.Add(quest);
        //        if (quest.Additional.ListOfSubQuest.Any())
        //            foreach (int subquestID in quest.Additional.ListOfSubQuest)
        //                ret.AddRange(getQuestsOnID(subquestID));
        //    }
        //    return ret;
        //}

        private List<CQuest> selectQuestsOnID(int questID)
        {
            List<CQuest> ret = new List<CQuest>();
            CQuest quest = getQuestOnQuestID(questID);
            if (quest != null)
            {
                ret.Add((CQuest)quest.Clone());
                if (quest.Additional.ListOfSubQuest.Any())
                    foreach (int subquestID in quest.Additional.ListOfSubQuest)
                        ret.AddRange(selectQuestsOnID(subquestID));
            }
            return ret;
        }

        private List<CQuest> replaceQuestsIDs(List<CQuest> quests)
        {
            Dictionary<int, int> replace = new Dictionary<int,int>();
            foreach (CQuest quest in quests)
            {
                List<int> excepts = replace.Values.ToList();
                excepts.AddRange(replace.Keys.ToList());

                replace.Add(quest.QuestID, getQuestNewID(excepts));
                quest.QuestID = replace[quest.QuestID];
            }
            foreach (CQuest quest in quests)
            {
                if (replace.Keys.Contains(quest.Additional.IsSubQuest))
                    quest.Additional.IsSubQuest = replace[quest.Additional.IsSubQuest];

                List<int> newSubQuestIDs = new List<int>();
                foreach (int subqID in quest.Additional.ListOfSubQuest)
                {
                    newSubQuestIDs.Add(replace[subqID]);
                }
                quest.Additional.ListOfSubQuest = newSubQuestIDs;
            }
            return quests;

        }

        private void bCopyEvents_Click(object sender, EventArgs e)
        {
            quests.buffer.Clear();

            List<CQuest> buffer = new List<CQuest>(selectQuestsOnID(currentQuest));

            buffer[0].Additional.IsSubQuest = 0;
            foreach (CQuest quest in buffer)
                quest.Additional.Holder = "";
            replaceQuestsIDs(buffer);
            quests.setBuffer(buffer);
            treeQuestBuffer.Nodes.Clear();
            addNodeOnTreeBuffer(buffer[0].QuestID);
            quests.bufferTop = buffer[0].QuestID;
        }

        private void treeQuest_Leave(object sender, EventArgs e)
        {
            if (currentQuest == 0)
                bCopyEvents.Enabled = false;
            //bPasteEvents.Enabled = false;
        }

        private void treeQuest_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //System.Console.WriteLine("treeQuest_AfterSelect");
            this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);
            //System.Console.WriteLine("currentQuest:" + currentQuest.ToString());

            bCopyEvents.Enabled = true;
            if (this.quests.buffer.Any())
            {
                bPasteEvents.Enabled = true;
            }
        }

        void addNodeOnTreeBuffer(int currentQuest)
        {
            CQuest curQuest = getQuestOnQuestID(currentQuest);

            if (!curQuest.Additional.IsSubQuest.Equals(0))
            {
                TreeNode[] nodes = treeQuestBuffer.Nodes.Find(getQuestOnQuestID(curQuest.Additional.IsSubQuest).QuestID.ToString(), true);
                if (nodes.Any())
                {
                    TreeNode parent = nodes[0];
                    parent.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                    int lastIndex = parent.Nodes.Count - 1;
                    if (curQuest.Target.onFin == 1)
                        parent.Nodes[lastIndex].BackColor = Color.Green;
                    else
                        parent.Nodes[lastIndex].BackColor = Color.Red;
                }
            }
            else
            {
                treeQuestBuffer.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                int lastIndex = treeQuest.Nodes.Count - 1;
                if (curQuest.Target.onFin == 1)
                    treeQuestBuffer.Nodes[lastIndex].BackColor = Color.Green;
                else
                    treeQuestBuffer.Nodes[lastIndex].BackColor = Color.Red;
            }
            if (!questConst.isSimple(curQuest.Target.QuestType) && (curQuest.Additional.ListOfSubQuest.Any()))
                foreach (int subquest in curQuest.Additional.ListOfSubQuest)
                    addNodeOnTreeBuffer(subquest);
        }

        private void treeQuestBuffer_DoubleClick(object sender, EventArgs e)
        {
            bCopyEvents.Enabled = false;

            //this.currentQuest = int.Parse(treeQuestBuffer.SelectedNode.Text);
            if (getQuestOnQuestID(currentQuest).Additional.IsSubQuest == 0)
            {
                EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 2);
                questEditor.Visible = true;
                this.Enabled = false;
            }

            else
            {
                EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 3);
                questEditor.Visible = true;
                this.Enabled = false;
            }
        }

        private void treeQuestBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentQuest = int.Parse(treeQuestBuffer.SelectedNode.Text);
            }
            catch
            {
            }
            bCopyEvents.Enabled = false;
            bPasteEvents.Enabled = false;
        }

        private void treeQuest_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);
            }
            catch
            {
            }
            if (quests.buffer.Any())
                bPasteEvents.Enabled = true;
        }

        private void bPasteEvents_Click(object sender, EventArgs e)
        {
            //System.Console.WriteLine("MainForm::bPasteEvents_Click");
            var quest = getQuestOnQuestID(currentQuest);
            if (quest != null)
            {
                if (!questConst.isSimple(quest.Target.QuestType))
                {
                    switch (MessageBox.Show("Вставить как подквест?", "Выберите тип вставки", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes: pasteBuffer(); break;
                        case DialogResult.No: replaceBuffer(); break;
                    }
                }
                else
                    replaceBuffer();
            }
        }

        private void pasteBuffer()
        {
            //System.Console.WriteLine("MainForm::pasteBuffer");
            var quest = getQuestOnQuestID(currentQuest);
            List<CQuest> buffer = selectQuestsOnID(quests.bufferTop);
            buffer = replaceQuestsIDs(buffer);
            quest.Additional.ListOfSubQuest.Add(buffer[0].QuestID);
            buffer[0].Additional.IsSubQuest = quest.QuestID;
            foreach (CQuest bufQuest in buffer)
            {
                bufQuest.Additional.Holder = quest.Additional.Holder;
                quests.addQuest(bufQuest);
            }
            var rootID = quests.getRoot(currentQuest);
            treeQuest.Nodes.Clear();
            addNodeOnTreeQuest(rootID);
            treeQuest.ExpandAll();
        }

        private void replaceBuffer()
        {
            //System.Console.WriteLine("MainForm::replaceBuffer");
            var quest = getQuestOnQuestID(currentQuest);
            if (quest.Additional.IsSubQuest == 0)
            {
                //System.Console.WriteLine("-root");
                List<CQuest> buffer = selectQuestsOnID(quests.bufferTop);
                buffer = replaceQuestsIDs(buffer);
                var name = quest.Additional.Holder;
                var questID = quest.QuestID;
                quests.removeQuest(questID);
                buffer[0].QuestID = questID;
                foreach (var subQuest in buffer)
                {
                    if (buffer[0].Additional.ListOfSubQuest.Contains(subQuest.QuestID))
                    {
                        subQuest.Additional.IsSubQuest = questID;
                    }
                    subQuest.Additional.Holder = name;
                    //System.Console.WriteLine("Add:" + subQuest.QuestID.ToString());
                    quests.addQuest(subQuest);
                }
                treeQuest.Nodes.Clear();
                addNodeOnTreeQuest(questID);
                treeQuest.ExpandAll();
            }
            else
            {
                //System.Console.WriteLine("-not root");
                List<CQuest> buffer = selectQuestsOnID(quests.bufferTop);
                buffer = replaceQuestsIDs(buffer);
                var name = quest.Additional.Holder;
                var questID = quest.QuestID;
                var parentID = quest.Additional.IsSubQuest;
                quests.removeQuest(questID);
                var parentQuest = getQuestOnQuestID(parentID);
                int index = parentQuest.Additional.ListOfSubQuest.IndexOf(questID);
                parentQuest.Additional.ListOfSubQuest.Remove(questID);
                parentQuest.Additional.ListOfSubQuest.Insert(index, buffer[0].QuestID);
                buffer[0].Additional.IsSubQuest = parentID;
                foreach (var subQuest in buffer)
                {
                    subQuest.Additional.Holder = name;
                    quests.addQuest(subQuest);
                }
                var root = quests.getRoot(parentID);
                treeQuest.Nodes.Clear();
                addNodeOnTreeQuest(root);
                treeQuest.ExpandAll();
            }
        }

        private void treeQuestBuffer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //System.Console.WriteLine("treeQuestBuffer_AfterSelect");
            this.currentQuest = int.Parse(treeQuestBuffer.SelectedNode.Text);
            //System.Console.WriteLine("currentQuest:" + currentQuest.ToString());
        }

        public void setTutorial(int questID, bool isTutor)
        {
            quests.setTutorial(questID, isTutor);
        }

        //-------------------Locales---------------------------
        public CDialog getLocaleDialog(int dialogID, string npcName)
        {
            var locale = settings.getCurrentLocale();
            return dialogs.getLocaleDialog(dialogID, locale, npcName);
        }

        public void addLocaleDialog(CDialog dialog)
        {
            this.dialogs.addLocaleDialog(dialog, settings.getCurrentLocale());
        }

        public CQuest getLocaleQuest(int questID)
        {
            var locale = settings.getCurrentLocale();
            return quests.getLocaleQuest(questID, locale);
        }

        public void addLocaleQuest(CQuest quest)
        {
            var locale = settings.getCurrentLocale();
            this.quests.addLocaleQuest(quest, locale);
        }

        //! Изменения на форме при смене режима (Editor <-> Localization)
        public void onChangeMode()
        {
            if (settings.getMode() == settings.MODE_LOCALIZATION)
            {
                for (int i = 4; i < CentralDock.TabPages.Count; i++)
                {
                    if (CentralDock.TabPages[i].Name != "tabTranslate")
                        CentralDock.TabPages[i].Enabled = false;
                    else
                        CentralDock.TabPages[i].Enabled = true;
                }
            }
            else
            {
                for (int i = 2; i < CentralDock.TabPages.Count; i++)
                {
                    if (CentralDock.TabPages[i].Name == "tabTranslate")
                        CentralDock.TabPages[i].Enabled = false;
                    else
                        CentralDock.TabPages[i].Enabled = true;
                }
            }
        }

        //! Создает примеры диалогов и квестов
        public void createExamples()
        {
            dialogs.createExamples();
            quests.createExamples();
        }

        //! Создает результаты работы над диалогами и квестами
        public void createResults()
        {
            dialogs.createResults();
            quests.createResults();
        }

        //! Выводит диалоги для локализации. В зависимости от помеченных чекбоксов - актуальные или устаревшие
        private void bFindDialogDifference_Click(object sender, EventArgs e)
        {
            int actual = (ActualCheckBox.Checked) ? (1) : (0);
            int outdated = (OutdatedCheckBox.Checked) ? (1) : (0);
            FindType findType = (FindType)(actual + (outdated << 1) );
            this.translate_checker = 1;
            diffGridView.Rows.Clear();
            var diff = dialogs.getDialogDifference(settings.getCurrentLocale(), findType);
            var type = "Диалог";
            int count = 0;

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    string location = (dialogs.location.ContainsKey(name)) ? (dialogs.location[name]) : ("НЕТ ИМЕНИ");
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version, location };
                    diffGridView.Rows.Add(row);
                    count++;
                }
            }
            labelLocalizeOuput.Text = "Выведено: "+ count.ToString();
            labelLocalizeOuput.Update();
        }

        int translate_checker = 0;
        private void diffGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var id = int.Parse(diffGridView.Rows[index].Cells[2].Value.ToString());
            currentNPC = diffGridView.Rows[index].Cells[1].Value.ToString();
            if (this.translate_checker == 1)
            {
                LocaleDialogForm editLocaleDialogForm = new LocaleDialogForm(this, id);
                editLocaleDialogForm.Visible = true;
            }
            else if (this.translate_checker == 2)
            {
                LocaleQuestForm editLocaleQuestform = new LocaleQuestForm(this, id);
                editLocaleQuestform.Visible = true;
            }
        }
        //! Выводит квесты для локализации
        private void bFindQuestDifference_Click(object sender, EventArgs e)
        {
            int actual = (ActualCheckBox.Checked) ? (1) : (0);
            int outdated = (OutdatedCheckBox.Checked) ? (1) : (0);
            FindType findType = (FindType)(actual + (outdated << 1));
            this.translate_checker = 2;
            diffGridView.Rows.Clear();
            var diff = quests.getQuestDifference(settings.getCurrentLocale(), findType);
            var type = "Квест";
            int count = 0;

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    string location = (dialogs.location.ContainsKey(name)) ? (dialogs.location[name]) : ("НЕТ ИМЕНИ");
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version, location };
                    diffGridView.Rows.Add(row);
                    count++;
                }
            }
            labelLocalizeOuput.Text = "Выведено: " + count.ToString();
            labelLocalizeOuput.Update();
        }
        //! Заполняет раздел Баланс Фракций (пока не изведано)
        private void fillFractionBalance()
        {
            foreach (KeyValuePair<int, string> pair in fractions.getListOfFractions())
            {
                int id = pair.Key;
                string name = pair.Value;
                string penalty_name = "";
                double limit = 0;
                double cat_1 = 0;
                double cat_2 = 0;
                double cat_3 = 0;
                bool has_key = false;
                CBalanceFractions fract = new CBalanceFractions();

                if (balances.fraction.Keys.Contains(id))
                {
                    fract = balances.fraction[id];

                    limit = fract.limit;
                    cat_1 = fract.cat_1;
                    cat_2 = fract.cat_2;
                    cat_3 = fract.cat_3;
                    has_key = true;

                }
                object[] row = { id, name, penalty_name, limit, cat_1, cat_2, cat_3 };
                fractionBalanceDataGridView.Rows.Add(row);

                foreach (KeyValuePair<int, string> penalty_pair in fractions.getListOfFractions())
                {
                    int sub_id = penalty_pair.Key;
                    name = "";
                    penalty_name = penalty_pair.Value;
                    cat_1 = 0;
                    cat_2 = 0;
                    cat_3 = 0;
                    if (id != sub_id)
                    {
                        if (has_key)
                        {
                            if (fract.penalty.Keys.Contains(penalty_pair.Key))
                            {
                                cat_1 = fract.penalty[penalty_pair.Key].cat_1;
                                cat_2 = fract.penalty[penalty_pair.Key].cat_2;
                                cat_3 = fract.penalty[penalty_pair.Key].cat_3;
                            }
                        }
                        object[] sub_row = { sub_id, name, penalty_name, "", cat_1, cat_2, cat_3 };
                        fractionBalanceDataGridView.Rows.Add(sub_row);
                    }
                }
            }
        }
        //! Сохраняет Баланс фракций
        private void bSaveBalance_Click(object sender, EventArgs e)
        {
            balances.fraction.Clear();
            int parent_id = 0;
            foreach (DataGridViewRow row in fractionBalanceDataGridView.Rows)
            {
                int id    = int.Parse(row.Cells[0].FormattedValue.ToString());
                string name         = row.Cells[1].FormattedValue.ToString();
                string penalty_name = row.Cells[2].FormattedValue.ToString();
                double limit = 0;
                if (row.Cells[3].FormattedValue.ToString()!="")
                    limit = double.Parse(row.Cells[3].FormattedValue.ToString().Replace('.',','));
                double cat_1 = double.Parse(row.Cells[4].FormattedValue.ToString().Replace('.', ','));
                double cat_2 = double.Parse(row.Cells[5].FormattedValue.ToString().Replace('.', ','));
                double cat_3 = double.Parse(row.Cells[6].FormattedValue.ToString().Replace('.', ','));

                if (name != "")
                {
                    parent_id = id;
                    balances.fraction.Add(id, new CBalanceFractions());
                    balances.fraction[id].limit = limit;
                    balances.fraction[id].cat_1 = cat_1;
                    balances.fraction[id].cat_2 = cat_2;
                    balances.fraction[id].cat_3 = cat_3;
                }
                else if (parent_id != 0)
                {
                    balances.fraction[parent_id].penalty.Add(id, new CFractionPenalty(cat_1,cat_2,cat_3));
                }
            }
            balances.save();

        }
        //! Нажатие на кнопку Отцентрировать - приводит DialogShower к исходному виду
        private void bCenterizeDialogShower_Click(object sender, EventArgs e)
        {
            // важное место - ставим зум на 1
            DialogShower.Camera.ViewScale = 1;
            // сдвиг ставим на 0 -камера возвращается в исходное положение
            DialogShower.Camera.SetViewOffset(0, 0);
        }
        //! Пункт главного меню - Настройки
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperatorSettings fOperator = new OperatorSettings(this);
            this.Enabled = false;
            fOperator.Show();
        }
        //! Пункт главного меню - Выход
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //! Пункт главного меню - статистика
        private void StatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticsForm sf = new StatisticsForm(this, NPCBox.Items.Count, quests, dialogs);
            sf.Show();
        }
        //! Нажатие "Найти NPC" на вкладке Проверка - поиск NPC с условием
        private void bFindNPC_Click(object sender, EventArgs e)
        {
            gridViewReview.Rows.Clear();
            bool checkDialog = cbNumDialogs.Checked;
            bool checkQuest = cbNumQuests.Checked;
            foreach (string npc in dialogs.dialogs.Keys)
            {
                int d_num = dialogs.dialogs[npc].Count;
                int q_num = quests.getCountOfQuests(npc);
                string location = (dialogs.location.ContainsKey(npc))?(dialogs.location[npc]):("НЕТ ИМЕНИ");
                if( (checkDialog && d_num < numDialogs.Value) || (checkQuest && q_num < numQuests.Value) )
                {
                    object[] row = { npc, d_num, q_num, location };
                    gridViewReview.Rows.Add( row );
                }
            }
            labelReviewOutputed.Text = "Выведено: " + gridViewReview.RowCount.ToString();
        }

        //! Тeстовая фукция "пробежать", пробегает всех NPC (для заполнения полей в тестовом режиме)
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NPCBox.Items.Count; i++ )
                NPCBox.SelectedIndex = i;
        }

        //! Поиск по номеру квеста в комбобоксе квеста и вывод информации
        private void QuestBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = QuestBox.Text;
                int id;
                if (int.TryParse(text, out id))
                {
                    CQuest quest = quests.getQuest(id);
                    if (quest != null)
                    {
                        CQuest temp = quest;
                        while (temp.Additional.IsSubQuest != 0)
                            temp = quests.getQuest( temp.Additional.IsSubQuest);

                        string qtext = temp.QuestID.ToString() + ": ";
                        qtext += temp.QuestInformation.Title;
                        NPCBox.SelectedItem = quest.Additional.Holder;
                        QuestBox.SelectedItem = qtext;
                    }
                }
          
            }
        }
    }
}
 