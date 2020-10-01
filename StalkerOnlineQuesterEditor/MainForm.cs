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
using Microsoft.CSharp;

namespace StalkerOnlineQuesterEditor
{
    using NPCQuestDict = Dictionary<int, CDialog>;
    using StalkerOnlineQuesterEditor.Forms;
    using System.IO;
    using System.Globalization;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using DialogDict = Dictionary<int, CDialog>;
    using NPCLocales = Dictionary<string, Dictionary<string, Dictionary<int, CDialog>>>;

    //! Главная форма программы, туча строк кода
    public partial class MainForm : Form
    {
        //! Текущий выбранный NPC (в комбобоксе вверху)
        private string currentNPC = "";
        
        private string currentFraction = "";
        //! Текущий выбранный Id диалога. Используется ТОЛЬКО для выделения диалога после смены режима эдитор-переводчик
        private int selectedDialogID = 0;
        //! Тип выбранного пользователем элемента - ничего, диалог или прямоугольник
        public SelectedItemType selectedItemType;
        //! Ссылка на экземпляр класса CDialogs, хранит все данные и функции по работе с диалогами
        public CDialogs dialogs;
        //! Ссылка на экземпляр класса CQuests
        CQuests quests;
        public CManagerNPC ManagerNPC;
        NodeDragHandler Listener;
        RectangleDrawingHandler RectDrawer;
        public RectangleManager RectManager;
        PanEventHandler PanHandler;
        ZoomEventHandler ZoomHandler;
        MouseHoverHandler HoverHandler;
        public PLayer edgeLayer;
        public PLayer drawingLayer;
        public PNodeList nodeLayer;
        public bool isDirty = false;
        Dictionary<PNode, GraphProperties> graphs = new Dictionary<PNode, GraphProperties>();
        Dictionary<Panel, int> panels = new Dictionary<Panel, int>();

        
        List<int> rootElements = new List<int>();
        public TreeView tree;
        List<PNode> subNodes = new List<PNode>();
        Dictionary<LinkLabel, int> titles;
        List<NPCNameDataSourceObject> npcNames = new List<NPCNameDataSourceObject>();

        List<int> npc_history = new List<int>();
        int current_npc_history_index = -1;

        public CQuestConstants questConst;
        public CItemConstants itemConst;
        public CItemCategories itemCategories;
        public CNPCConstants npcConst;
        public CMobConstants mobConst;
        public CZoneConstants zoneConst;
        public BillboardQuests billboardQuests;
        public CZoneMobConstants zoneMobConst;
        public CSpacesConstants spacesConst;
        public CDungeonSpacesConstants dungeonConst;
        public CTriggerConstants triggerConst;
        public CTPConstants tpConst;
        public CommandConstants cmConst;
        public RepairConstants rpConst;
        public AvatarActions avAmin;
        public NPCActions npcActions;
        public ListSounds listSouds;
        public NPCItems npcItems;

        public CSettings settings;
        public COperNotes manageNotes;
        public CFracConstants fractions;
        public SkillConstants skills;
        public PvPRanks pvPRanks;
        public CGUIConst gui;
        public CEffectConstants effects;
        public DialogEventsList dialogEvents;
        public CTutorialConstants tutorialPhases;
        public int currentQuest;
        public Dictionary<string, bool> npcFilters;

        public CKnowledgeConstans knowledgeCategory;
        IOClasses.TCPListener tcpListener;


        public MainForm(string[] args)
        {
            InitializeComponent();
            spacesConst = new CSpacesConstants();
            dungeonConst = new CDungeonSpacesConstants();
            RectManager = new RectangleManager();
            Listener = new NodeDragHandler(this);
            RectDrawer = new RectangleDrawingHandler(this, RectManager);
            PanHandler = new PanEventHandler();
            ZoomHandler = new ZoomEventHandler();
            HoverHandler = new MouseHoverHandler(this, RectManager);
            RectManager.LoadData();
            settings = new CSettings(this);
            ManagerNPC = new CManagerNPC();
            dialogEvents = new DialogEventsList();
            dialogs = new CDialogs(this, ManagerNPC);
            CFractionDialogs.load(this);
            QuestPVPConstance.parse();
            quests = new CQuests(this);
            tpConst = new CTPConstants();
            cmConst = new CommandConstants();
            rpConst = new RepairConstants();
            avAmin = new AvatarActions();
            npcActions = new NPCActions();
            npcItems = new NPCItems();
            listSouds = new ListSounds();
            npcFilters = ManagerNPC.getSpaces();
            settings.checkMode();
            tutorialPhases = new CTutorialConstants();
            tree = treeDialogs;
            questConst = new CQuestConstants();
            itemConst = new CItemConstants();
            itemCategories = new CItemCategories();
            npcConst = new CNPCConstants();
            
            triggerConst = new CTriggerConstants();
            manageNotes = new COperNotes("ManNotes.xml");
            fractions = new CFracConstants();
            skills = new SkillConstants();
            pvPRanks = new PvPRanks();
            gui = new CGUIConst();
            effects = new CEffectConstants();
            CPVPConstans.Load();
            knowledgeCategory = new CKnowledgeConstans();

            treeQuest.AfterSelect += new TreeViewEventHandler(this.treeQuestSelected);
            //fillNPCBox();
            fillLocationsBox();
            fillItemRewardsBox();
            fillFractionBox();
            fractionBox.SelectedIndex = 0;

            fillFractionsInManageTab();
            DialogShower.AddInputEventListener(Listener);
            DialogShower.AddInputEventListener(RectDrawer);
            DialogShower.AddInputEventListener(PanHandler);
            DialogShower.AddInputEventListener(ZoomHandler);
            DialogShower.AddInputEventListener(HoverHandler);
            DialogShower.PanEventHandler = null;
            DialogShower.ZoomEventHandler = null;

            fractionDialogShower.AddInputEventListener(Listener);
            fractionDialogShower.AddInputEventListener(RectDrawer);
            fractionDialogShower.AddInputEventListener(PanHandler);
            fractionDialogShower.AddInputEventListener(ZoomHandler);
            fractionDialogShower.AddInputEventListener(HoverHandler);
            fractionDialogShower.PanEventHandler = null;
            fractionDialogShower.ZoomEventHandler = null;

            foreach (string name in dialogs.getListOfNPC())
                if (!npcConst.NPCs.Keys.Contains(name))
                    npcConst.NPCs.Add(name, new CNPCDescription(name));
            fillRewardNPCReputationVox();
            this.mobConst = new CMobConstants();
            this.zoneConst = new CZoneConstants();
            this.billboardQuests = new BillboardQuests();
            this.zoneMobConst = new CZoneMobConstants();
            //SetMasterMode();

            tcpListener = new IOClasses.TCPListener(this);
            tcpListener.start();

            if (args.Length > 0)
            {
                Thread t = new Thread(() => this.openNPC(args[0].Replace("/", "")));
                t.Start();
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.getLanguage());
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            ApplyResourceToControl(resources, this);

        }
        //! Set mode for me, if Command line has /master parameter, TestButton and some labels will be shown
        void SetMasterMode()
        {
            string cmd = Environment.CommandLine;
            if (cmd.IndexOf("/master") != -1)
            {
                //bTestButton.Visible = true;
                labelXNode.Visible = true;
                labelYNode.Visible = true;
            }
        }

        public string GetCurrentNPC()
        {
            return currentNPC;
        }

        public string GetCurrentHolder()
        {
            if (CentralDock.SelectedIndex == 2) return currentFraction;
            return currentNPC;
        }

        //! Очищает данные о квестах - дерево квестов, комбобокс, подквесты
        void clearQuestTab()
        {
            treeQuest.Nodes.Clear();
            splitQuestsContainer.Panel2.Controls.Clear();
            QuestBox.Items.Clear();
        }

        public void openNPC(string open_npc_name)
        {
            int index = 0;
            for (int i = 0; i < NPCBox.Items.Count; i++)
            {
                string npc_name = (NPCBox.Items[i] as NPCNameDataSourceObject).Value;
                if (npc_name.Contains(open_npc_name))
                {
                    settings.setLastNpcIndex(index);
                    Console.WriteLine("NPC::" + index.ToString() + " " + npc_name);

                    if (NPCBox.InvokeRequired)
                    {
                        NPCBox.Invoke(new ThreadStart(delegate { NPCBox.SelectedIndex = index; }));
                    }
                    else
                    {
                        NPCBox.SelectedIndex = index;
                    }
                    break;
                }
                index++;
            }
        }

        private void btnBackNPC_Click(object sender, EventArgs e)
        {
            current_npc_history_index--;
            NPCBox.SelectedIndex = npc_history[current_npc_history_index];
            checkNavigationArrows();
        }

        private void btnNextNPC_Click(object sender, EventArgs e)
        {
            current_npc_history_index++;
            NPCBox.SelectedIndex = npc_history[current_npc_history_index];
            checkNavigationArrows();
        }

        private void checkNavigationArrows()
        {
            btnBackNPC.Enabled = (current_npc_history_index != -1) && (current_npc_history_index != 0);
            btnNextNPC.Enabled = current_npc_history_index != (npc_history.Count - 1);
        }

        //! Сменили NPC в комбобоксе выбора персонажа
        private void NPCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearQuestTab();
            try
            {
                currentNPC = NPCBox.SelectedValue.ToString();
                RectManager.SetCurrentNPC(currentNPC);
                if ((NPCBox.SelectedIndex != 0) && ((current_npc_history_index == -1) || (npc_history[current_npc_history_index] != NPCBox.SelectedIndex)))
                {
                    npc_history = npc_history.GetRange(0, Math.Max(current_npc_history_index + 1, 0));
                    npc_history.Add(NPCBox.SelectedIndex);
                    current_npc_history_index = Math.Max(npc_history.Count - 1, 0);
                    checkNavigationArrows();
                }
            }
            catch (Exception err)
            {
                string a = err.Message;
                return;
            }
            
            if (CentralDock.SelectedIndex == 0)
            {
                fillQuestChangeBox(true);
                QuestBox.Enabled = false;
                bAddQuest.Enabled = false;
                bRemoveQuest.Enabled = false;
                bAddDialog.Enabled = false;
                bEditDialog.Enabled = false;
                bRemoveDialog.Enabled = false;
                splitDialogsEmulator.Panel2.Controls.Clear();
                DialogSelected(true);
            }
            else if (CentralDock.SelectedIndex == 1)
            {
                fillQuestChangeBox(false);
                QuestBox.Text = "Пожалуйста, выберите квест";
            }
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            var text = resources.GetString(String.Format("{0}.Text", tabQuests.Name));
            tabQuests.Text = text + " (" + quests.getCountOfQuests(currentNPC) + ")";
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
            //NPCBox.SelectedItem = FakeNPCBox.SelectedItem.ToString();
        }

        private void NPCBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
                return;
            NPCBox.DroppedDown = false;
            string find_text = NPCBox.Text.ToLower();
            List<string> result = new List<string>();
            foreach (var item in npcNames)
            {
                if (item.DisplayString.ToLower().Contains(find_text))
                {
                    result.Add(item.DisplayString);
                }
            }
            FakeNPCBox.Items.Clear();
            FakeNPCBox.Items.AddRange(result.ToArray());
            FakeNPCBox.DroppedDown = true;
            //e.Handled = true;
        }

        //! Поиск в комбобоксe NPC по имени общему, или локализованному русскому, английскому
        private void NPCBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = NPCBox.Text;
                if (dialogs.dialogs.Keys.Contains(text))
                    return;
                string name = "";
                if (settings.getMode() == settings.MODE_EDITOR)
                {
                    /*
                    if (ManagerNPC.rusNamesToNPC.ContainsKey(text))
                    {
                        name = ManagerNPC.rusNamesToNPC[text];
                        NPCBox.SelectedValue = name;
                    }
                    */
                    if (FakeNPCBox.Items.Count > 0)
                        NPCBox.SelectedValue = FakeNPCBox.Items[0].ToString().Split('(')[0];
                    FakeNPCBox.DroppedDown = false;
                }
                else if (settings.getMode() == settings.MODE_LOCALIZATION)
                {
                    if (ManagerNPC.engNamesToNPC.ContainsKey(text))
                    {
                        name = ManagerNPC.engNamesToNPC[text];
                        NPCBox.SelectedValue = name;
                    }
                }
            }
            //else
            //    NPCBox.AutoCompleteCustomSource.Where(a => a.ToLower().Contains(NPCBox.Text.ToLower()));
        }


        //! Заполняет комбобокс со списком квестов у данного персонажа
        void fillQuestChangeBox(bool onlyDialogs)
        {
            QuestBox.SelectedItem = null;
            QuestBox.Items.Clear();
            if (!onlyDialogs)
            {
                if (settings.getMode() == settings.MODE_EDITOR)
                {
                    foreach (CQuest quest in quests.getQuestAndTitleOnNPCName(currentNPC))
                        QuestBox.Items.Add(quest.QuestID + ": " + quest.QuestInformation.Title);
                }
                else if (settings.getMode() == settings.MODE_LOCALIZATION)
                {
                    string locale = settings.getCurrentLocale();
                    foreach (CQuest quest in quests.getQuestAndTitleOnNPCName(currentNPC))
                    {
                        int id = quest.QuestID;
                        QuestBox.Items.Add(quest.QuestID + ": " + quests.locales[locale][id].QuestInformation.Title);
                    }
                }
            }
        }

        //! Сменили квест в комбобоксе, выводим дерево всех подквестов
        public void QuestBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QuestBox.SelectedItem == null) return;
            splitQuestsContainer.Panel2.Controls.Clear();
            treeQuest.Nodes.Clear();

            if (CentralDock.SelectedIndex == 1)
            {
                int index = QuestBox.SelectedItem.ToString().IndexOf(':');
                string qID = QuestBox.SelectedItem.ToString().Substring(0, index);
                int questID = int.Parse(qID);
                addNodeOnTreeQuest(questID);
                bRemoveQuest.Enabled = true;
            }
            treeQuest.ExpandAll();
        }

        //! Заполнение итемов в комбобоксе NPC
        void fillFractionBox()
        {
            fractionBox.Items.Clear();
            foreach (KeyValuePair<int, string> fract in this.fractions.getListOfFractions())
            {
                fractionBox.Items.Add(fract.Key.ToString() + " " + fract.Value);
            }
        }

        //! Заполнение итемов в комбобоксе NPC
        void fillNPCBox()
        {
                    npcNames.Clear();
                    //NPCBox.AutoCompleteCustomSource.Clear();

                    foreach (string holder in this.dialogs.dialogs.Keys)
                    {
                        string npcName = holder;
                        string space = "no map";
                        foreach (KeyValuePair<string, List<string>> mapData in ManagerNPC.mapToNPCList)
                        {
                            foreach (string name in mapData.Value)
                            {
                                if (name == npcName)
                                {
                                    space = mapData.Key;
                                }
                            }
                        }
                        string local_name = this.spacesConst.getLocalName(space);
                        if ((npcFilters.ContainsKey(space) && !npcFilters[space]) || (npcFilters.ContainsKey(local_name) && !npcFilters[local_name]))
                            continue;
                        //InvalidOperationException
                        string localName = "";
                        if (ManagerNPC.NpcData.ContainsKey(holder))
                        {
                            if (settings.getMode() == settings.MODE_EDITOR)
                                localName = ManagerNPC.NpcData[holder].rusName;
                            else if (settings.getMode() == settings.MODE_LOCALIZATION)
                                localName = ManagerNPC.NpcData[holder].engName;

                            //NPCBox.AutoCompleteCustomSource.Add(localName);
                            npcName += " (" + localName + ")";
                        }
                        npcNames.Add(new NPCNameDataSourceObject(holder, npcName));
                        //NPCBox.AutoCompleteCustomSource.Add(npcName);
                    }
                    npcNames.Sort();
                    NPCBox.DataSource = null;       // костыль для обновления данных в кмобобоксе NPC при добавлении/удалении
                    NPCBox.DisplayMember = "DisplayString";
                    NPCBox.ValueMember = "Value";
                    NPCBox.DataSource = npcNames;
                    if (npcNames.Count <= settings.getLastNpcIndex())
                        settings.setLastNpcIndex(npcNames.Count - 1);
                    NPCBox.SelectedIndex = settings.getLastNpcIndex();
                    //NPCBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //NPCBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    QuestBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    QuestBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    QuestBox.AutoCompleteCustomSource.AddRange(quests.getQuestsIDasString());
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
                case 2:
                    DialogSelected(true);
                    Console.WriteLine("fractionDialogShower.Location = (" + fractionDialogShower.Location.X + " " + fractionDialogShower.Location.Y + ")");
                    Console.WriteLine("fractionDialogShower.Size = (" + fractionDialogShower.Width + " " + fractionDialogShower.Height + ")");
                    break;
                case 3: // Вкладка Связи NPC 
                    SetControlsAbility(false);
                    NPCBox.Enabled = true;
                    break;
                case 4:
                case 5:         // Вкладка Управление (квестами)
                    FillTabManage();
                    break;
                case 7:     // Вкладки Проверка, Перевод, Баланс
                    SetControlsAbility(false);
                    break;
                case 8:     // Вкладка знания
                    updateKnowledgeTab();
                    break;

            }
        }

        // ************************ WORK WITH DIALOGS****************************************************

        void fillNPCBoxSubquests(CDialog sub, DialogDict dialogs, TreeView tree)
        {
            foreach (int subdialog in sub.Nodes)
            {
                foreach (TreeNode treeNode in tree.Nodes.Find("Active", true))
                {
                    if (!treeNode.Nodes.ContainsKey(subdialog.ToString()))
                    {
                        
                        if (!dialogs.ContainsKey(subdialog))
                        {
                            MessageBox.Show("Ошибка диалога, у NPC: нет диалога №" + subdialog + ", а ссылка есть");
                            continue;
                        }
                        treeNode.Nodes.Add(subdialog.ToString(), subdialog.ToString());
                        dialogs[subdialog].coordinates.Active = true;
                    }
                }
                this.fillNPCBoxSubquests(dialogs[subdialog], dialogs, tree);
            }
        }

        internal void onSelectNode(int dialogID)
        {
            if (selectedItemType == SelectedItemType.dialog)
            {
                if (CentralDock.SelectedIndex == 2)
                {
                    bAddFracDialog.Enabled = true;
                    bEditFracDialog.Enabled = true;
                    if (!isRoot(dialogID))
                        bRemoveFracDialog.Enabled = true;
                }
                else
                {
                    bAddDialog.Enabled = true;
                    bEditDialog.Enabled = true;
                    if (!isRoot(dialogID))
                        bRemoveDialog.Enabled = true;
                }
            }
            else if (selectedItemType == SelectedItemType.rectangle)
            {
                if (CentralDock.SelectedIndex == 2)
                {
                    bAddFracDialog.Enabled = false;
                    bEditFracDialog.Enabled = true;
                    bRemoveFracDialog.Enabled = true;
                }
                else
                {
                    bAddDialog.Enabled = false;
                    bEditDialog.Enabled = true;
                    bRemoveDialog.Enabled = true;
                }
            }
        }

        public void selectNodeOnDialogTree(int dialogID)
        {
            //treeDialogs.Focus();
            int index = CentralDock.SelectedIndex;
            switch (index)
            {
                case 0:
                    foreach (TreeNode node in treeDialogs.Nodes.Find(dialogID.ToString(), true))
                        treeDialogs.SelectedNode = node;
                    break;
                case 2:
                    foreach (TreeNode node in treeFractionDialogs.Nodes.Find(dialogID.ToString(), true))
                        treeFractionDialogs.SelectedNode = node;
                    break;
            }
        }

        internal void onDeselectNode()
        {
            bRemoveDialog.Enabled = false;
            bEditDialog.Enabled = false;
        }
        //! Удаление диалога в зависимости от статуса - в корзину или навсегда
        private void bRemoveDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (selectedItemType == SelectedItemType.dialog)
                {
                    if (Listener.SelectedNode != null)
                        removeNodeFromDialogGraphView(getDialogIDOnNode(Listener.SelectedNode), this.treeDialogs, dialogs.dialogs[currentNPC], dialogs.locales, DialogShower);
                    else
                        removeDialog(int.Parse(treeDialogs.SelectedNode.Text), getDialogDictionary(currentNPC, dialogs.dialogs, dialogs.locales), dialogs.locales, treeDialogs);
                }
                else if (selectedItemType == SelectedItemType.rectangle)
                {
                    RectManager.RemoveRectangle();
                    DrawRectangles();
                }
                onDeselectNode();
            }
        }

        private void bRemoveFracDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (selectedItemType == SelectedItemType.dialog)
                {
                    if (Listener.SelectedNode != null)
                        removeNodeFromDialogGraphView(getDialogIDOnNode(Listener.SelectedNode), this.treeFractionDialogs, CFractionDialogs.dialogs[currentFraction], CFractionDialogs.locales, fractionDialogShower);
                    else
                        removeDialog(int.Parse(treeDialogs.SelectedNode.Text), getDialogDictionary(currentFraction, CFractionDialogs.dialogs, CFractionDialogs.locales), CFractionDialogs.locales, treeFractionDialogs);
                }
                else if (selectedItemType == SelectedItemType.rectangle)
                {
                    RectManager.RemoveRectangle();
                    DrawRectangles();
                }
                onDeselectNode();
            }
        }

        public bool isRoot(int dialogID)
        {
            return rootElements.Contains(dialogID);
        }

        public Brush GetBrushForNode(PNode node)
        {
            if (isRoot(getDialogIDOnNode(node)))
                return Brushes.Green;
            else if (node == Listener.SelectedNode)
                return Brushes.Red;
            else if (subNodes.Contains(node))
                return Brushes.Yellow;
            else
                return Brushes.White;
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
                                Listener.SelectCurrentNode(treeID);
                    }
                    else if (treeDialogs.SelectedNode.Parent.Text == "Recycle")
                    {
                        onSelectNode(treeID);
                        Listener.SelectCurrentNode(0);
                    }
                }
            }
        }
        //! Добавляет поддиалог к текущему диалогу
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

        private void bAddFracDialog_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                if (!treeFractionDialogs.SelectedNode.ToString().Equals("Recycle") && !treeFractionDialogs.SelectedNode.ToString().Equals("Active"))
                {
                    if (!(Listener.getCurDialogID() == 0))
                    {
                        EditDialogForm editDialogForm = new EditDialogForm(true, this, int.Parse(treeFractionDialogs.SelectedNode.Text));
                        editDialogForm.Visible = true;
                    }
                    else
                    {
                        AddPassiveDialogForm AddPassiveDialog = new AddPassiveDialogForm(this, int.Parse(treeFractionDialogs.SelectedNode.Text));
                        AddPassiveDialog.Visible = true;
                    }
                }
            }
        }
        //! Нажатие на кнопку "Редактирование диалогов" - открывает редактор или переводчик диалогов
        public void bEditDialog_Click(object sender, EventArgs e)
        {
            int index = CentralDock.SelectedIndex;
            TreeView tree = treeDialogs;
            switch (index)
            {
                case 2:
                    tree = treeFractionDialogs; break;
            }

            if (selectedItemType == SelectedItemType.dialog)
            {
                if (tree.SelectedNode == null) return;
                if (settings.getMode() == settings.MODE_EDITOR)
                {
                    EditDialogForm editDialogForm = new EditDialogForm(false, this, int.Parse(tree.SelectedNode.Text));
                    editDialogForm.Visible = true;
                }
                else
                {
                    LocaleDialogForm editLocaleDialogForm = new LocaleDialogForm(this, int.Parse(tree.SelectedNode.Text));
                    editLocaleDialogForm.Visible = true;
                }
            }
            else if (selectedItemType == SelectedItemType.rectangle)
            {
                EditRectangle editRect = new EditRectangle(RectManager.GetSelectedRectangle());
                editRect.ShowDialog();
                DrawRectangles();
            }
        }

        //! Полностью удаляет диалог из эдитора (выбор в Recycled -> удаление)
        void removeDialog(int dialogID, DialogDict dialogs, NPCLocales locales, TreeView tree)
        {
            dialogs.Remove(dialogID);
            foreach (CDialog dialog in dialogs.Values)
                if (dialog.Actions.ToDialog == dialogID)
                    dialog.Actions.ToDialog = 0;
            // удаляем диалог из переводов
            locales[settings.getListLocales()[0]][currentNPC].Remove(dialogID);
            foreach (CDialog dialog in locales[settings.getListLocales()[0]][currentNPC].Values)
                if (dialog.Actions.ToDialog == dialogID)
                    dialog.Actions.ToDialog = 0;

            CDialog rootDialog = getRootDialog(dialogs);
            if (rootDialog != null)
                fillDialogTree(rootDialog, dialogs, tree, locales);
        }

        //! Старует эмулятор диалога (язык диалога зависит от режима, непереведенные фрагменты помечаются красным)
        public void startEmulator(int dialogID)
        {
            // получаем фразу NPC
            int index = CentralDock.SelectedIndex;

            CDialog rootDialog;
            if (index == 2)
                rootDialog = getDialogOnIDConditional(dialogID, getDialogDictionary(currentFraction, CFractionDialogs.dialogs, CFractionDialogs.locales), CFractionDialogs.locales);
            else
                rootDialog = getDialogOnIDConditional(dialogID, getDialogDictionary(currentNPC, dialogs.dialogs, dialogs.locales), dialogs.locales);

            selectedDialogID = dialogID;
            splitDialogsEmulator.Panel2.Controls.Clear();
            titles = new Dictionary<LinkLabel, int>();
            Label NPCText = new Label();
            NPCText.Text = rootDialog.Text;
            NPCText.ForeColor = (rootDialog.version != 0) ? (Color.Black) : (Color.Red);
            NPCText.AutoSize = false;
            NPCText.AutoEllipsis = true;
            NPCText.Dock = DockStyle.Top;

            foreach (int subdialogID in rootDialog.Nodes)
            {
                LinkLabel dialogLink = new LinkLabel();
                dialogLink.LinkClicked += new LinkLabelLinkClickedEventHandler(dialogLink_LinkClicked);
                string actionResult;
                if (index == 2)
                    actionResult = CFractionDialogs.dialogs[currentFraction][subdialogID].Actions.GetAsString();
                else
                    actionResult = dialogs.dialogs[currentNPC][subdialogID].Actions.GetAsString();

                CDialog answer;
                if (index == 2)
                    answer = getDialogOnIDConditional(subdialogID, getDialogDictionary(currentFraction, CFractionDialogs.dialogs, CFractionDialogs.locales), CFractionDialogs.locales);
                else
                    answer = getDialogOnIDConditional(subdialogID, getDialogDictionary(currentNPC, dialogs.dialogs, dialogs.locales), dialogs.locales);
                dialogLink.Text = subdialogID + ". " + answer.Title + actionResult;
                dialogLink.BackColor = (answer.version != 0) ? (Color.FromKnownColor(KnownColor.Transparent)) : (Color.FromArgb(0x7FAA45E0));
                dialogLink.AutoSize = true;
                dialogLink.Dock = DockStyle.Top;
                dialogLink.Links.Add(0, 0, subdialogID);
                titles.Add(dialogLink, subdialogID);
                splitDialogsEmulator.Panel2.Controls.Add(dialogLink);
            }
            splitDialogsEmulator.Panel2.Controls.Add(NPCText);
        }

        public bool isDialogActive(int dialogID)
        {
            foreach (TreeNode node in treeDialogs.Nodes.Find("Active", true)[0].Nodes)
                if (int.Parse(node.Name).Equals(dialogID))
                    return true;
            return false;
        }

        //! Отработка клика по ответу в эмуляторе диалогов, вывод результатов в статусбар и переход к след диалогу
        private void dialogLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            int choosenDialogID = (int)e.Link.LinkData;
            CDialog choosenDialog = dialogs.dialogs[currentNPC][choosenDialogID];
            statusLabel.Text = "";
            String actionString = "";
            if (choosenDialog.Actions.CheckAndGetString(out actionString))
                addActionTextToEmulator(actionString, choosenDialogID);

            if (choosenDialog.Actions.Exit)
                addActionTextToEmulator("Выход.", choosenDialogID);

            else if (choosenDialog.Actions.ToDialog != 0)
            {
                statusLabel.Text += "Переход на диалог: " + choosenDialog.Actions.ToDialog.ToString();
                Listener.SelectCurrentNode(choosenDialog.Actions.ToDialog);
            }
            else
                Listener.SelectCurrentNode(choosenDialogID);
        }
        //! Антиговнокод - добавление примечания к фразе диалога с действием
        void addActionTextToEmulator(string text, int dialogID)
        {
            if (statusLabel.Text != "")
                statusLabel.Text += "\n";
            statusLabel.Text += text;
            splitDialogsEmulator.Panel2.Controls.Clear();
            Listener.SelectCurrentNode(dialogID);
        }
        //! Выводит координаты узла как прямоугольника. Для отладки.
        public void setXYCoordinates(float X, float Y, float w, float h)
        {
            this.labelXNode.Text = "X=" + X.ToString();
            this.labelYNode.Text = "Y=" + Y.ToString();
            this.labelXNode.Text += " w=" + w.ToString();
            this.labelYNode.Text += " h=" + h.ToString();
        }

        public void ShowDialogTooltip(PNode CurrentNode)
        {
            int dialogID = getDialogIDOnNode(CurrentNode);
            string tooltip = "";
            if (dialogs.dialogs[currentNPC].ContainsKey(dialogID))
                tooltip = dialogs.dialogs[currentNPC][dialogID].GetNodeTooltip();
            toolTipDialogs.SetToolTip(DialogShower, tooltip);
        }

        public void ResetDialogsTooltip()
        {
            toolTipDialogs.SetToolTip(DialogShower, "");
        }

        // ************************END DIALOGS BLOCK****************************************************

        public void clearToolstripLabel()
        {
            statusLabel.Text = "";
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

        // ******************************* WORK WITH QUESTS ***************************************
        //! Возвращает экземпляр CQuest по его ID
        public CQuest getQuestOnQuestID(int questID)
        {
            return quests.getQuest(questID);
        }

        void addNodeOnTreeQuest(int currentQuest)
        {
            CQuest curQuest = getQuestOnQuestID(currentQuest);
            treeQuest.BeginUpdate();
            TreeNode curNode = null;
            if (!curQuest.Additional.IsSubQuest.Equals(0))
            {
                TreeNode[] nodes = treeQuest.Nodes.Find(getQuestOnQuestID(curQuest.Additional.IsSubQuest).QuestID.ToString(), true);
                if (nodes.Any())
                {
                    TreeNode parent = nodes[0];
                    curNode = parent.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                    int lastIndex = parent.Nodes.Count - 1;
                    if (curQuest.hidden)
                        parent.Nodes[lastIndex].BackColor = Color.CadetBlue;
                    else if (curQuest.Target.onFin == 1)
                        parent.Nodes[lastIndex].BackColor = Color.YellowGreen;
                    else
                        parent.Nodes[lastIndex].BackColor = Color.Red;
                }
            }
            else
            {
                curNode = treeQuest.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                if (curQuest.hidden)
                    curNode.BackColor = CQuests.QuestParentList.ContainsKey(currentQuest) ? Color.DodgerBlue : Color.CadetBlue;
                else if (curQuest.Target.onFin == 1)
                    curNode.BackColor = CQuests.QuestParentList.ContainsKey(currentQuest) ? Color.LightGreen : Color.YellowGreen;
                else
                    curNode.BackColor = CQuests.QuestParentList.ContainsKey(currentQuest) ? Color.IndianRed : Color.Red;
            }
            if (curQuest.Reward.ChangeQuests.Count > 0)
            {
                foreach (KeyValuePair<int, int> change_quest in curQuest.Reward.ChangeQuests)
                {
                    //if (change_quest.Value != 0) continue;
                    int quest_id = change_quest.Key;
                    int change_type = change_quest.Value;
                    TreeNode reward_quest = curNode.Nodes.Add(quest_id.ToString(), quest_id.ToString());
                    switch(change_quest.Value)
                    {
                        case 0: reward_quest.BackColor = Color.LightCyan; break;
                        case 1: reward_quest.BackColor = Color.Pink; break;
                        case 2: reward_quest.BackColor = Color.Gray; break;
                        case 3: reward_quest.BackColor = Color.SandyBrown; break;
                    }
                    
                }
                curNode.Nodes.Add("", "");
            }
            //if (!questConst.isSimple(curQuest.Target.QuestType) && (curQuest.Additional.ListOfSubQuest.Any()))
            if ((!questConst.isSimple(curQuest.Target.QuestType) || settings.getMode() == settings.MODE_LOCALIZATION)
                    && (curQuest.Additional.ListOfSubQuest.Any()))
                foreach (int subquest in curQuest.Additional.ListOfSubQuest)
                    addNodeOnTreeQuest(subquest);
            
            treeQuest.EndUpdate();
            treeQuest.Refresh();
            treeQuest.Update();
        }

        //! Создает плашку с информацией о субквесте
        void createQuestPanels(int questID)
        {
            //CQuest quest = getQuestOnQuestID(questID);
            CQuest quest = quests.getQuestLocalized(questID);

            Panel questPanel = new Panel();
            questPanel.AutoSize = true;
            questPanel.Dock = DockStyle.Top;
            questPanel.Size = new Size(splitQuestsContainer.Panel2.Width - 5, 100);
            questPanel.BorderStyle = BorderStyle.FixedSingle;
            questPanel.Click += questPanel_Click;

            GroupBox questBox = new GroupBox();
            questBox.AutoSize = true;
            questBox.Text = "Квест ID: " + questID;
            questBox.Dock = DockStyle.Top;

            Label eventLabel = new Label();
            eventLabel.Text = "Событие: " + questConst.getDescription(quest.Target.QuestType);
            eventLabel.Dock = DockStyle.Top;
            questBox.Controls.Add(eventLabel);
            questBox.Controls.SetChildIndex(eventLabel, 1);

            if (quests.getTargetString(quest) != "")
            {
                Label targetLabel = new Label();
                targetLabel.Text = "Цель: " + quests.getTargetString(quest);
                targetLabel.Dock = DockStyle.Top;
                questBox.Controls.Add(targetLabel);
                questBox.Controls.SetChildIndex(targetLabel, 1);
            }

            GroupBox infoBox = new GroupBox();
            infoBox.AutoSize = true;
            infoBox.Text = "Информация";
            infoBox.Dock = DockStyle.Top;
            infoBox.Click += questPanel_Click;
            infoBox.Tag = questID;

            Label titleLabel = new Label();
            titleLabel.Text = "Заголовок:" + quest.QuestInformation.Title;
            titleLabel.BackColor = (quest.Version != 0) ? (Color.FromKnownColor(KnownColor.Transparent)) : (Color.FromArgb(0x7FAA45E0));
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Click += questPanel_Click;
            titleLabel.Tag = questID;
            infoBox.Controls.Add(titleLabel);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Описание:" + quest.QuestInformation.Description;
            descriptionLabel.BackColor = (quest.Version != 0) ? (Color.FromKnownColor(KnownColor.Transparent)) : (Color.FromArgb(0x7FAA45E0));
            descriptionLabel.Dock = DockStyle.Top;
            descriptionLabel.Click += questPanel_Click;
            descriptionLabel.Tag = questID;
            infoBox.Controls.Add(descriptionLabel);

            questBox.Controls.Add(infoBox);
            questBox.Controls.SetChildIndex(infoBox, 0);

            questPanel.Controls.Add(questBox);

            questBox.Controls.SetChildIndex(eventLabel, 2);
            panels.Add(questPanel, questID);

            if (quest.Additional.ListOfSubQuest.Any())
                foreach (int subquest in quest.Additional.ListOfSubQuest)
                    createQuestPanels(subquest);
        }

        void questPanel_Click(object sender, EventArgs e)
        {
            currentQuest = (int)((Control)sender).Tag;
            treeQuestClicked(sender, e);
            //MessageBox.Show("It works! " + currentQuest.ToString());            
        }
        //! Выделение квеста в дереве квестов
        private void treeQuestSelected(object sender, EventArgs e)
        {
            if (!treeQuest.SelectedNode.Text.Any()) return;
            this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);
            //            int questID = int.Parse(treeQuest.SelectedNode.Text);
            checkQuestButton(getQuestOnQuestID(currentQuest).Target.QuestType, currentQuest);
            fillQuestPanel();
        }
        //! Заполнение подквестов в виде списка в splitQuestsContainer
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
        //! Двойной клик по квесту в дереве квестов. Открытие окна редактирования или перевода квеста
        private void treeQuestClicked(object sender, EventArgs e)
        {
            if (currentQuest == 0) return;
            bCopyEvents.Enabled = true;

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
                LocaleQuestForm questForm = new LocaleQuestForm(this, currentQuest);
                questForm.Show();
            }
        }

        //! Нажатие кнопки Добавить NPC - открывает форму с именем
        private void bAddNPC_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавление NPC сейчас происходит в NPCEditor", "Добавление NPC");
            //NewNPC newNPC = new NewNPC(this);
            //newNPC.Visible = true;
        }

        delegate void addNewNPCDelegate(string Name);
        //! Добавляет нового NPC в систему
        public void addNewNPC(string Name)
        {
            if (NPCBox.InvokeRequired)
            {
                var _addNewNPC = new addNewNPCDelegate(addNewNPC);
                NPCBox.Invoke(_addNewNPC, Name);
            }
            else
            {
                Dictionary<int, CDialog> firstDialog = new Dictionary<int, CDialog>();
                int dialogID = CDialogs.getDialogsNewID();
                NodeCoordinates nc = new NodeCoordinates(179, 125, true, true);
                firstDialog.Add(dialogID, new CDialog(Name, "", "", new CDialogPrecondition(), new Actions(), new List<int>(), new List<int>(),
                        dialogID, 0, nc));

                dialogs.dialogs.Add(Name, firstDialog);
                // добавляем ТЗс в английскую локаль, делаем копию словаря
                Dictionary<int, CDialog> engDialog = new Dictionary<int, CDialog>();
                engDialog.Add(dialogID, new CDialog(Name, "", "", new CDialogPrecondition(), new Actions(), new List<int>(), new List<int>(),
                        dialogID, 0, nc));
                dialogs.locales[settings.getListLocales()[0]].Add(Name, engDialog);
                fillNPCBox();
                NPCBox.SelectedValue = Name;
                npcConst.NPCs.Add(Name, new CNPCDescription(Name));
                this.isDirty = true;
            }
        }

        //! Нажатие на кнопку Удаление Персонажа NPC
        private void bDelNPC_Click(object sender, EventArgs e)
        {
            graphs.Clear();
            treeDialogs.Nodes.Clear();
            DialogShower.Layer.RemoveAllChildren();

            List<int> removedQuests = new List<int>();
            foreach (CQuest quest in quests.quest.Values)
                if (quest.Additional.Holder.Equals(currentNPC))
                    removedQuests.Add(quest.QuestID);

            foreach (int item in removedQuests)
            {
                quests.quest.Remove(item);
                quests.locales[settings.getListLocales()[0]].Remove(item);
            }
            dialogs.dialogs.Remove(currentNPC);
            dialogs.locales[settings.getListLocales()[0]].Remove(currentNPC);
            currentNPC = "";
            fillNPCBox();
        }
        //! Устанавливает доступность средств работы с квестами
        public void checkQuestButton(int questType, int questID)
        {
            bEditEvent.Enabled = true;
            if (questConst.isSimple(questType))// && getQuestOnQuestID(questID).Additional.ListOfSubQuest.Any())
                bAddEvent.Enabled = false;
            else
                bAddEvent.Enabled = true;

            if (getQuestOnQuestID(questID).Additional.IsSubQuest.Equals(0))
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

        public void incQuestNewID()
        {
            quests.last_quest_id++;
        }

        public int getQuestNewID()
        {
            
            string html = string.Empty;
            string url = @"http://hz-dev2.stalker.so:8011/getnextid?key=quest_id";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            try
            {
                JObject json = JObject.Parse(html);
                int new_quest_id = (int)json["quest_id"];
                return new_quest_id;
            }
            catch(Exception e)
            {
                MessageBox.Show("Ошибка получения нового ID квеста. Проверьте своё подключение к hz-dev", "Ошибка");
            }

            if (quests.last_quest_id != 0)
            {
                return quests.last_quest_id;
            }
            int iFirstQuestID = 1 + this.settings.getOperatorNumber() * 400;
            for (int questi = iFirstQuestID; ; questi++)
                if (!quests.quest.Keys.Contains(questi) && !quests.m_Buffer.Keys.Contains(questi) && !quests.deletedQuests.Contains(questi))
                    return questi;

        }

        //! Создает новый корневой квест у персонажа (не подквест, а именно новый)
        public void createNewQuest(CQuest newQuest)
        {
            quests.quest.Add(newQuest.QuestID, newQuest);
            CQuest engQuest = new CQuest();
            engQuest = (CQuest)newQuest.Clone();
            quests.locales[settings.getListLocales()[0]].Add(engQuest.QuestID, engQuest);

            QuestBox.Items.Add(newQuest.QuestID.ToString() + ": " + newQuest.QuestInformation.Title);
            QuestBox.SelectedIndex = QuestBox.Items.Count - 1;
            this.isDirty = true;
        }

        //! Добавление квеста в дерево квестов, вызывается из окна редактирования EditQuestForm
        public void addQuest(CQuest quest, int parent)
        {
            quests.quest[parent].Additional.ListOfSubQuest.Add(quest.QuestID);
            quests.quest.Add(quest.QuestID, quest);

            CQuest engQuest = new CQuest();
            engQuest = (CQuest)quest.Clone();
            quests.locales[settings.getListLocales()[0]][parent].Additional.ListOfSubQuest.Add(engQuest.QuestID);
            quests.locales[settings.getListLocales()[0]].Add(engQuest.QuestID, engQuest);

            checkQuestButton(quest.Target.QuestType, quest.QuestID);
            addNodeOnTreeQuest(quest.QuestID);
            treeQuest.ExpandAll();
            fillQuestPanel();
            this.isDirty = true;
        }
        //! Заменяет данные квеста при редактировании
        public void replaceQuest(CQuest quest)
        {
            //CQuest replacedQuest = quests.quest[quest.QuestID];
            //quests.quest[quest.QuestID] = quest;
            quests.replaceQuest(quest);
            if (quests.quest.Keys.Contains(quest.QuestID))
                quests.locales[settings.getListLocales()[0]][quest.QuestID].InsertNonTextData(quest);
            checkQuestButton(quest.Target.QuestType, quest.QuestID);
            this.isDirty = true;
        }
        //! Нажатие на кнопку "Добавление квеста", вызов окна EditQuestForm
        private void bAddEvent_Click(object sender, EventArgs e)
        {
            EditQuestForm questEditor = new EditQuestForm(this, currentQuest, 4, getQuestNewID());
            questEditor.Visible = true;
            this.Enabled = false;
        }
        //! Правка квеста, в зависимости от режима вызывается окно правки или окно перевода
        private void bEditEvent_Click(object sender, EventArgs e)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
            {
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
            else
            {
                LocaleQuestForm quest_form = new LocaleQuestForm(this, currentQuest);
                quest_form.Visible = true;
                this.Enabled = false;
            }
        }
        //! Сохранение данных по диалогам и квестам (если режим редактора - то сохранение и локализаций)
        void saveData()
        {
            this.Enabled = false;
            if (settings.getMode() == settings.MODE_EDITOR)
            {
                dialogs.SaveDialogs();
                CFractionDialogs.SaveDialogs();
                quests.SaveQuests();
            }
            dialogs.SaveLocales();
            CFractionDialogs.SaveLocales();
            quests.SaveLocales();
            RectManager.SaveData();
            Thread.Sleep(300);
            statusLabel.Text = "Данные успешно сохранены.";
            this.Enabled = true;
            this.isDirty = false;
        }
        //! Нажатие на кнопку "Удаление квеста"
        private void bRemoveEvent_Click(object sender, EventArgs e)
        {
            this.removeQuest(currentQuest, false);
        }

        //! Удаляет квест и  все его подквесты
        void removeQuest(int questID, bool recursiveCall)
        {
            List<int> temp = getQuestOnQuestID(questID).Additional.ListOfSubQuest;
            foreach (int subquest in temp) //getQuestOnQuestID(questID).Additional.ListOfSubQuest)
                removeQuest(subquest, true);

            if (!recursiveCall)
                if (getQuestOnQuestID(questID).Additional.IsSubQuest != 0)
                    quests.quest[getQuestOnQuestID(questID).Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);

            // удаляем квест из локализаций
            CQuest english = quests.locales[settings.getListLocales()[0]][questID];
            foreach (int subquest in english.Additional.ListOfSubQuest)
                removeQuest(subquest, true);
            if (english.Additional.IsSubQuest != 0)
                quests.locales[settings.getListLocales()[0]][english.Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);
            quests.locales[settings.getListLocales()[0]].Remove(questID);

            treeQuest.Nodes.Find(questID.ToString(), true)[0].Remove();
            quests.addDeletedQuests(questID);
            quests.quest.Remove(questID);
        }
        //! Перемещение квеста вверх по дереву квестов
        private void bQuestUp_Click(object sender, EventArgs e)
        {
            int temp;
            CQuest parent = getQuestOnQuestID(getQuestOnQuestID(currentQuest).Additional.IsSubQuest);
            for (int i = 0; i < parent.Additional.ListOfSubQuest.Count; i++)
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

            int id = parent.QuestID;
            quests.locales[settings.getListLocales()[0]][id].Additional.ListOfSubQuest = new List<int>(parent.Additional.ListOfSubQuest);
        }
        //! Перемещение квеста вниз по дереву квестов
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

            int id = parent.QuestID;
            quests.locales[settings.getListLocales()[0]][id].Additional.ListOfSubQuest = new List<int>(parent.Additional.ListOfSubQuest);
        }

        void findAndSelectNodeOnTreeQuest(int questID)
        {
            treeQuest.SelectedNode = treeQuest.Nodes.Find(questID.ToString(), true)[0];
        }
        //! Нажатие на кнопку "Удаление корневого квеста", т.е. удаляется корневой (не субквест) квест у персонажа
        private void bRemoveQuest_Click(object sender, EventArgs e)
        {
            int removedQuest = int.Parse(QuestBox.SelectedItem.ToString().Split(':')[0].Trim());
            QuestBox.Items.Remove(QuestBox.SelectedItem);
            removeQuest(removedQuest, false);
        }
        //! Создание нового корневого квеста у персонажа
        private void bAddQuest_Click(object sender, EventArgs e)
        {
            EditQuestForm newQuest = new EditQuestForm(this, getQuestNewID(), 1); //, true, true);
            newQuest.Visible = true;
            this.Enabled = false;
        }

        public void setQuestISClan(int questID, bool ClanState)
        {
            quests.setClan(questID, ClanState);
        }

        List<CQuest> getTreeItemIDs(int questID)
        {
            List<CQuest> ret = new List<CQuest>();
            CQuest quest = getQuestOnQuestID(questID);
            if (QuestItem.hasQuestItem(quest.Reward.items) || QuestItem.hasQuestItem(quest.QuestRules.items))
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
        //! Возвращает ID всех субквестов любой глубины вложенности (т.е субквесты субквеста и т.д.)
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

        void fillQuestDataInManageTab(string npcName, CQuest quest, bool force = false, string parentTitle = "")
        {
            if (quest.hidden && !force)
                return;
            //если 12 тип, то показать его детей первого уровня и его самого, всё равно, что сам он дочерний
            if (quest.Additional.Holder == npcName && (force || (quest.Additional.IsSubQuest == 0) || (quest.Target.QuestType == 12)))
            {

                //string id = quest.QuestID.ToString();
                List<int> iSubIDS = getSubIDs(quest.QuestID);
                List<string> sNPCLink = new List<string>();
                int rewardExpBattle = 0;
                int rewardExpSurvival = 0;
                int rewardExpSupport = 0;
                float rewardCredits = 0;
                int[] reputations = new int[1];
                //чтоб ничего не сломалось, если кто-то захотел удалить/добавить фракцию
                Array.Resize<int>(ref reputations, fractions.genLenListOfFractions());

                Dictionary<int, int> rewardItems = new Dictionary<int, int>();


                foreach (int quid in iSubIDS)
                {
                    CQuest q = getQuestOnQuestID(quid);

                    if (q.Reward.Experience.Any())
                    {
                        rewardExpBattle += q.Reward.Experience[0];
                        rewardExpSurvival += q.Reward.Experience[1];
                        rewardExpSupport += q.Reward.Experience[2];
                    }

                    rewardCredits += q.Reward.Credits;

                    foreach (QuestItem item in q.Reward.items)
                    {
                        int type = item.itemType;
                        int count = item.count;
                        int attr = (int)item.attribute;
                        if (attr == 0)
                        {
                            if (rewardItems.Keys.Contains(type))
                                rewardItems[type] += count;
                            else
                                rewardItems[type] = count;
                        }
                    }
                    int frac_index = 0;
                    foreach (KeyValuePair<int, string> fraction in this.fractions.getListOfFractions())
                    {
                        int fraction_id = fraction.Key;
                        int val = 0;
                        if (q.Reward.Reputation.ContainsKey(fraction_id))
                            val = q.Reward.Reputation[fraction_id];
                        reputations[frac_index] += val;
                        frac_index++;
                    }
                    if (quest.Target.QuestType == 12)
                    {
                        break;
                    }
                }

                string npcLinks = "";
                string getDialogs = "";
                string subIDs = Global.GetListAsString(iSubIDS);
                string title;
                if (parentTitle != "")
                    title = parentTitle;
                else
                    title = quest.QuestInformation.Title;
                string description = quest.QuestInformation.Description;
                string npcNe = quest.Additional.Holder;
                string srewardExpBattle = rewardExpBattle.ToString();
                string srewardSurvival = rewardExpSurvival.ToString();
                string srewardExpSupport = rewardExpSupport.ToString();
                string srewardCredits = rewardCredits.ToString();
                string sRewardItem = "";
                int sRepeat = quest.Precondition.Repeat;
                double sPeriod = quest.Precondition.TakenPeriod;

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
                    string itemName = itemConst.getItemName(type);
                    string count = rewardItems[type].ToString();

                    if (sRewardItem == "")
                        sRewardItem += itemName + ":" + count.ToString();
                    else
                        sRewardItem += "\n" + itemName + ":" + count.ToString();
                }
                object[] row = { quest.QuestID, subIDs, title, description, npcNe, npcLinks, getDialogs, rewardExpBattle, rewardExpSurvival, rewardExpSupport, rewardCredits, sRewardItem, sRepeat, sPeriod, sLevel, sAuthor, sLegend, sWorked };
                int old_len = row.Count();
                Array.Resize(ref row, old_len + reputations.Count());
                for (int i = old_len, j = 0; i < row.Count(); i++, j++)
                {
                    row[i] = reputations[j];
                }

                dgvManage.Rows.Add(row);
                if (quest.Target.QuestType == 12)
                {
                    CQuest sub_quest;
                    foreach (int subquestID in quest.Additional.ListOfSubQuest)
                    {
                        sub_quest = getQuestOnQuestID(subquestID);
                        fillQuestDataInManageTab(npcName, sub_quest, true, quest.QuestInformation.Title);
                    }

                }
            }
        }

        //! Заполняет вкладку Управление
        void FillTabManage()
        {
            dgvManage.Rows.Clear();

            ((DataGridViewComboBoxColumn)dgvManage.Columns[17]).Items.Clear();
            ((DataGridViewComboBoxColumn)dgvManage.Columns[17]).Items.Add("Да.");
            ((DataGridViewComboBoxColumn)dgvManage.Columns[17]).Items.Add("Нет.");
            ((DataGridViewComboBoxColumn)dgvManage.Columns[17]).Items.Add("Правка.");

            foreach (string npcName in npcConst.NPCs.Keys)
                foreach (CQuest quest in quests.quest.Values)
                {
                    fillQuestDataInManageTab(npcName, quest);
                }

        }

        private void bSaveManage_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvManage.Rows)
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

        //! Нажатие на кнопку "Копировать" квесты в буфер обмена
        private void bCopyEvents_Click(object sender, EventArgs e)
        {
            quests.PutQuestsToBuffer(currentQuest, false);
            treeQuestBuffer.Nodes.Clear();
            addNodeOnTreeBuffer(quests.m_Buffer.First().Value.QuestID);
        }

        //! Нажатие на кнопку "Вырезать" квесты и поместить их в буфер обмена квестов
        private void bCutEvents_Click(object sender, EventArgs e)
        {
            quests.PutQuestsToBuffer(currentQuest, true);
            treeQuestBuffer.Nodes.Clear();
            addNodeOnTreeBuffer(quests.m_Buffer.First().Value.QuestID);
        }

        private void treeQuest_Leave(object sender, EventArgs e)
        {
            if (currentQuest == 0)
                bCopyEvents.Enabled = false;
            //bPasteEvents.Enabled = false;
        }

        private void treeQuest_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!treeQuest.SelectedNode.Text.Any()) return;
            this.currentQuest = int.Parse(treeQuest.SelectedNode.Text);

            bCopyEvents.Enabled = true;
            if (this.quests.m_Buffer.Any())
                bPasteEvents.Enabled = true;
        }

        //! Добавляет узел квеста в дерево буфера квестов
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
                    if (curQuest.hidden)
                        parent.Nodes[lastIndex].BackColor = Color.CadetBlue;
                    else if (curQuest.Target.onFin == 1)
                        parent.Nodes[lastIndex].BackColor = Color.YellowGreen;
                    else
                        parent.Nodes[lastIndex].BackColor = Color.Red;
                }
            }
            else
            {
                treeQuestBuffer.Nodes.Add(curQuest.QuestID.ToString(), curQuest.QuestID.ToString());
                int lastIndex = treeQuest.Nodes.Count - 1;
                if (curQuest.hidden)
                    treeQuestBuffer.Nodes[lastIndex].BackColor = Color.CadetBlue;
                else if (curQuest.Target.onFin == 1)
                    treeQuestBuffer.Nodes[lastIndex].BackColor = Color.YellowGreen;
                else
                    treeQuestBuffer.Nodes[lastIndex].BackColor = Color.Red;
            }
            if (!questConst.isSimple(curQuest.Target.QuestType) && (curQuest.Additional.ListOfSubQuest.Any()))
                foreach (int subquest in curQuest.Additional.ListOfSubQuest)
                    addNodeOnTreeBuffer(subquest);
        }
        //! Дабл клик по квесту в буфере обмена, открывает редактор квеста
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
            int temp;
            if (treeQuest.SelectedNode != null)
                if (int.TryParse(treeQuest.SelectedNode.Text, out temp))
                    this.currentQuest = temp;
            if (quests.m_Buffer.Any())
                bPasteEvents.Enabled = true;
        }

        //! Нажатие на кнопку "Вставить" квесты из буфера в дерево квестов
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

        //! Вставляет квесты из буфера обмена как подквест 
        private void pasteBuffer()
        {
            quests.PasteBuffer(currentQuest);
            var rootID = quests.getRoot(currentQuest);
            treeQuest.Nodes.Clear();
            addNodeOnTreeQuest(rootID);
            treeQuest.ExpandAll();

            if (quests.CutQuests)
                clearQuestsBuffer();
        }

        //! Заменяет выделенный квест на квесты из буфера обмена
        private void replaceBuffer()
        {
            var quest = getQuestOnQuestID(currentQuest);
            int root = quests.ReplaceBuffer(currentQuest);
            if (root == 0) return;
            treeQuest.Nodes.Clear();
            addNodeOnTreeQuest(root);

            treeQuest.ExpandAll();
            if (quests.CutQuests)
                clearQuestsBuffer();
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

        //! Очищает буфер обмена квестов
        public void clearQuestsBuffer()
        {
            quests.m_Buffer.Clear();
            quests.m_EngBuffer.Clear();
            treeQuestBuffer.Nodes.Clear();
            quests.bufferTop = 0;
            bPasteEvents.Enabled = false;
        }

        //! Нажатие на кнопку Очистка буфера обмена квестов
        private void bClearBuffer_Click(object sender, EventArgs e)
        {
            clearQuestsBuffer();
        }

        //*************************************************LOCALES*******************

        public CDialog getLocaleDialog(int dialogID, string npcName)
        {
            var locale = settings.getCurrentLocale();
            return CDialogs.getLocaleDialog(dialogID, locale, npcName, this.dialogs.locales);
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
            int currentNpcIndex = NPCBox.SelectedIndex;
            if (settings.getMode() == settings.MODE_LOCALIZATION)
            {
                for (int i = 4; i < CentralDock.TabPages.Count; i++)
                {
                    if (CentralDock.TabPages[i].Name != "tabTranslate" && CentralDock.TabPages[i].Name != "tabSearch")
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
            fillNPCBox();
            if (currentNpcIndex > -1)
                NPCBox.SelectedIndex = currentNpcIndex;
            if (selectedDialogID != 0)
                Listener.SelectCurrentNode(selectedDialogID);
        }

        //! Выводит диалоги для локализации. В зависимости от помеченных чекбоксов - актуальные или устаревшие
        private void bFindDialogDifference_Click(object sender, EventArgs e)
        {
            int actual = (cbActualFinder.Checked) ? (1) : (0);
            int outdated = (cbOutdatedFinder.Checked) ? (1) : (0);
            FindType findType = (FindType)(actual + (outdated << 1));
            this.translate_checker = 1;
            dgvLocaleDiff.Rows.Clear();
            string loc = settings.getCurrentLocale();
            var diff = dialogs.getDialogDifference(settings.getCurrentLocale(), findType);
            var type = "Диалог";
            int count = 0;

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    string location = (ManagerNPC.NpcData.ContainsKey(name)) ? (ManagerNPC.NpcData[name].location) : ("НЕТ ИМЕНИ");
                    string rustext1 = dialogs.dialogs[name][id].Title;
                    string engtext1 = dialogs.locales[loc][name][id].Title;
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version, location, rustext1, engtext1 };
                    dgvLocaleDiff.Rows.Add(row);
                    count++;
                }
            }
            labelLocalizeOuput.Text = "Выведено: " + count.ToString();
            labelLocalizeOuput.Update();
        }

        int translate_checker = 0;
        private void dgvLocaleDiff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var id = int.Parse(dgvLocaleDiff.Rows[index].Cells[2].Value.ToString());
            currentNPC = dgvLocaleDiff.Rows[index].Cells[1].Value.ToString();
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
            int actual = (cbActualFinder.Checked) ? (1) : (0);
            int outdated = (cbOutdatedFinder.Checked) ? (1) : (0);
            FindType findType = (FindType)(actual + (outdated << 1));
            this.translate_checker = 2;
            dgvLocaleDiff.Rows.Clear();
            string loc = settings.getCurrentLocale();
            var diff = quests.getQuestDifference(settings.getCurrentLocale(), findType);
            var type = "Квест";
            int count = 0;

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    string location = (ManagerNPC.NpcData.ContainsKey(name)) ? (ManagerNPC.NpcData[name].location) : ("НЕТ ИМЕНИ");
                    string rustext1 = quests.quest[id].QuestInformation.Title;
                    string engtext1 = quests.locales[loc][id].QuestInformation.Title;
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version, location, rustext1, engtext1 };
                    dgvLocaleDiff.Rows.Add(row);
                    count++;
                }
            }
            labelLocalizeOuput.Text = "Выведено: " + count.ToString();
            labelLocalizeOuput.Update();
        }

        //! Сохраняет Баланс фракций
        private void bSaveBalance_Click(object sender, EventArgs e)
        {

        }
        //! Нажатие на кнопку Отцентрировать - приводит DialogShower к исходному виду
        private void bCenterizeDialogShower_Click(object sender, EventArgs e)
        {
            // важное место - ставим зум на 1
            DialogShower.Camera.Scale = 1;

            float coordX = 0, coordY = 0;
            foreach (KeyValuePair<int, CDialog> dialog in dialogs.dialogs[this.currentNPC])
            {
                if (dialog.Value.coordinates.RootDialog)
                {
                    coordX = dialog.Value.coordinates.X;
                    coordY = dialog.Value.coordinates.Y;
                    break;
                }
            }

            // сдвиг ставим на 0 -камера возвращается в исходное положение
            DialogShower.Camera.SetOffset(-coordX + DialogShower.Width / 2, -coordY + DialogShower.Height / 2);

        }

        //*************************** DATA CHECK - missed dialogs, quests, NPC and so on**************************
        //! Заполнение итемов в комбобоксе выбора локации (во вкладке Проверка)
        void fillLocationsBox()
        {
            cbLocation.Sorted = true;
            cbLocation.DataSource = null;
            cbLocation.DataSource = ManagerNPC.locationNames;
        }

        //! добавляет в таблицу Управление столбцы фракций
        void fillFractionsInManageTab()
        {
            foreach (KeyValuePair<int, string> fraction in this.fractions.getListOfFractions())
            {
                string id = fraction.Key.ToString();
                string name = fraction.Value;
                this.dgvManage.Columns.Add(name, name);
            }
            //this.dgvManage;
        }



        void fillItemRewardsBox()
        {
            foreach (CItem item in itemConst.getAllItems().Values)
            {
                cbItemReward.Items.Add(item.getName());
                cbItemTarget.Items.Add(item.getName());
            }
            cbItemReward.Sorted = true;
            cbItemTarget.Sorted = true;
        }


        void fillRewardNPCReputationVox()
        {
            foreach (string npc_name in this.npcConst.NPCs.Keys)
                cbNPCList.Items.Add(npc_name);
        }
        //! Устанавливает надписи в таблице вкладки Проверка для поиска нужны NPC
        void setNPCCheckEnvironment()
        {
            dgvReview.Columns[0].HeaderText = "Имя NPC";
            dgvReview.Columns[1].HeaderText = "Диалоги";
            dgvReview.Columns[2].HeaderText = "Квесты";
            dgvReview.Columns[3].HeaderText = "Карта";
            dgvReview.Columns[4].HeaderText = "Координаты";
            dgvReview.Columns[5].HeaderText = "Русское имя";
            dgvReview.Columns[4].Visible = true;
            dgvReview.Columns[5].Visible = true;
        }
        //! Устанавливает надписи в таблице вкладки Проверка для поиска квестов
        void setQuestCheckEnvironment()
        {
            dgvReview.Columns[0].HeaderText = "Имя NPC";
            dgvReview.Columns[1].HeaderText = "Квест";
            dgvReview.Columns[2].HeaderText = "Открыт квест";
            dgvReview.Columns[3].HeaderText = "Закрыт квест";
            dgvReview.Columns[4].Visible = false;
            dgvReview.Columns[5].Visible = false;
        }

        //! Устанавливает надписи в таблице вкладки Проверка для поиска квестов дающих личную репутацию
        void setNPCReputationCheckEnvironment()
        {
            dgvReview.Columns[0].HeaderText = "Квест";
            dgvReview.Columns[1].HeaderText = "Название";
            dgvReview.Columns[2].HeaderText = "NPC_name";
            dgvReview.Columns[3].HeaderText = "Репутация";
            dgvReview.Columns[4].Visible = false;
            dgvReview.Columns[5].Visible = false;
        }


        //! Нажатие "Найти NPC" на вкладке Проверка - поиск NPC с условием
        private void bFindNPC_Click(object sender, EventArgs e)
        {
            setNPCCheckEnvironment();
            dgvReview.Rows.Clear();
            bool checkDialog = cbNumDialogs.Checked;
            bool checkQuest = cbNumQuests.Checked;
            bool checkLocation = cbOnlyOnLocation.Checked;
            foreach (string npc in dialogs.dialogs.Keys)
            {
                int d_num = dialogs.dialogs[npc].Count;
                int q_num = quests.getCountOfQuests(npc);
                string neededLocation = cbLocation.Text;
                string location = (ManagerNPC.NpcData.ContainsKey(npc)) ? (ManagerNPC.NpcData[npc].location) : ("НЕТ ИМЕНИ");
                string rusname = (ManagerNPC.NpcData.ContainsKey(npc)) ? (ManagerNPC.NpcData[npc].rusName) : ("НЕ ПЕРЕВЕДЕН");
                string coord = (ManagerNPC.NpcData.ContainsKey(npc)) ? (ManagerNPC.NpcData[npc].coordinates) : ("НЕТ КООРДИНАТ");
                if ((!checkDialog || (checkDialog && d_num < numDialogs.Value))
                    && (!checkQuest || (checkQuest && q_num < numQuests.Value))
                    && (!checkLocation || (checkLocation && location == neededLocation)))
                {
                    object[] row = { npc, d_num, q_num, location, coord, rusname };
                    dgvReview.Rows.Add(row);
                }
            }
            statusLabel.Text = "Выведено: " + dgvReview.RowCount.ToString();
        }
        //! Нажатие "Найти квесты", выводит несоответствия в квестах (не открыт, не закрыт, не существует)
        private void bFindQuest_Click(object sender, EventArgs e)
        {
            setQuestCheckEnvironment();
            dgvReview.Rows.Clear();
            if (cbItemReward.SelectedIndex < 0 && cbItemTarget.SelectedIndex < 0)
            {
                foreach (CQuest quest in quests.quest.Values)
                {
                    if (quest.Additional.IsSubQuest != 0)
                        continue;

                    int questID = quest.QuestID;
                    int open = 0, close = 0;
                    foreach (string npc in dialogs.dialogs.Keys)
                        foreach (CDialog dialog in dialogs.dialogs[npc].Values)
                        {
                            if (dialog.Actions.GetQuests.Contains(questID))
                                open = dialog.DialogID;
                            if (dialog.Actions.CompleteQuests.Contains(questID))
                                close = dialog.DialogID;
                        }
                    if (open == 0 || close == 0)
                    {
                        object[] row = { quest.Additional.Holder, questID, open, close };
                        dgvReview.Rows.Add(row);
                    }
                }
            }
            else
            {
                int itemID = -1;
                int targetItemID = -1;
                if (cbItemReward.SelectedIndex > -1)
                    itemID = itemConst.getIDOnName(cbItemReward.SelectedItem.ToString());
                if (cbItemTarget.SelectedIndex > -1)
                    targetItemID = itemConst.getIDOnName(cbItemTarget.SelectedItem.ToString());
                foreach (CQuest quest in quests.quest.Values)
                {
                    bool itemFound = false;
                    foreach (QuestItem item in quest.Reward.items)
                        if (item.itemType == itemID) { itemFound = true; break; } 
                    if (itemFound)
                    {
                        object[] row = { quest.Additional.Holder, quest.QuestID };
                        dgvReview.Rows.Add(row);
                    }
                    if (((quest.Target.QuestType == CQuestConstants.TYPE_FARM) || (quest.Target.QuestType == CQuestConstants.TYPE_FARM_AUTO)) && (quest.Target.ObjectType == targetItemID))
                    {
                        object[] row = { quest.Additional.Holder, quest.QuestID };
                        dgvReview.Rows.Add(row);
                    }
                }
            }
            statusLabel.Text = "Выведено: " + dgvReview.RowCount.ToString();
        }
        //***************************END OF DATA CHECK - missed dialogs, quests, NPC and so on**************************

        //! Поиск по номеру квеста в комбобоксе квеста и вывод информации
        private void QuestBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = QuestBox.Text;
                int id;
                if (int.TryParse(text, out id))     // вводят цифровой ID квеста
                {
                    CQuest quest = quests.getQuest(id);
                    if (quest != null)
                    {
                        CQuest temp = quest;
                        while (temp.Additional.IsSubQuest != 0)
                            temp = quests.getQuest(temp.Additional.IsSubQuest);

                        string qtext = temp.QuestID.ToString() + ": ";
                        qtext += temp.QuestInformation.Title;
                        NPCBox.SelectedValue = quest.Additional.Holder;
                        QuestBox.SelectedItem = qtext;
                    }
                }
                else    // вводят название квеста
                {
                    int questNum = 0;
                    foreach (int questID in quests.quest.Keys)
                        if (quests.quest[questID].QuestInformation.Title == text)
                        {
                            questNum = questID;
                            break;
                        }
                    if (questNum != 0)
                    {
                        string qtext = questNum.ToString() + ": ";
                        qtext += quests.quest[questNum].QuestInformation.Title;
                        NPCBox.SelectedValue = quests.quest[questNum].Additional.Holder;
                        QuestBox.SelectedItem = qtext;
                    }
                }
            }
        }

       
        private void bStartSearch_Click(object sender, EventArgs e)
        {
            int count = 0;
            string locale = settings.getCurrentLocale();
            string textToFind = tbPhraseToSearch.Text;
            string rusText = "";
            string engText = "";
            string npcName;
            bool found = false;
            dgvSearch.Rows.Clear();
            StringComparison compare = (cbIgnoreCase.Checked) ? (StringComparison.OrdinalIgnoreCase) : (StringComparison.Ordinal);
            foreach (CQuest quest in quests.quest.Values)
            {
                if (quest.QuestInformation.Title.IndexOf(textToFind, compare) != -1)
                {
                    rusText = quest.QuestInformation.Title;
                    engText = quests.locales[locale][quest.QuestID].QuestInformation.Title;
                    found = true;
                }
                else if (quest.QuestInformation.Description.IndexOf(textToFind, compare) != -1)
                {
                    rusText = quest.QuestInformation.Description;
                    engText = quests.locales[locale][quest.QuestID].QuestInformation.Description;
                    found = true;
                }
                if (found)
                {
                    npcName = quest.Additional.Holder;
                    count++;
                    object[] row = { "Квест", npcName, quest.QuestID, rusText, engText };
                    dgvSearch.Rows.Add(row);
                    found = false;
                }
            }

            found = false;
            foreach (string Npc in dialogs.dialogs.Keys)
                foreach (CDialog dialog in dialogs.dialogs[Npc].Values)
                {
                    if (dialog.Text.IndexOf(textToFind, compare) != -1)
                    {
                        rusText = dialog.Text;
                        engText = dialogs.locales[locale][Npc][dialog.DialogID].Text;
                        found = true;
                    }
                    else if (dialog.Title.IndexOf(textToFind, compare) != -1)
                    {
                        rusText = dialog.Title;
                        engText = dialogs.locales[locale][Npc][dialog.DialogID].Title;
                        found = true;
                    }
                    if (found)
                    {
                        count++;
                        object[] row = { "Диалог", Npc, dialog.DialogID, rusText, engText };
                        dgvSearch.Rows.Add(row);
                        found = false;
                    }
                }
            labelSearchResult.Text = "Найдено: " + count.ToString();
        }

        private void dgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int id = int.Parse(dgvSearch.Rows[index].Cells[2].Value.ToString());
            currentNPC = dgvSearch.Rows[index].Cells[1].Value.ToString();
            string type = dgvSearch.Rows[index].Cells[0].Value.ToString();
            if (type == "Квест")
            {
                if (settings.getMode() == settings.MODE_EDITOR)
                {
                    int mode = (quests.quest[id].Additional.IsSubQuest == 0) ? (2) : (3);
                    EditQuestForm editQuestForm = new EditQuestForm(this, id, mode);
                    editQuestForm.Visible = true;
                }
                else
                {
                    LocaleQuestForm editLocaleQuestform = new LocaleQuestForm(this, id);
                    editLocaleQuestform.Visible = true;
                }
            }
            else if (type == "Диалог")
            {
                if (settings.getMode() == settings.MODE_EDITOR)
                {
                    EditDialogForm editDialogForm = new EditDialogForm(false, this, id);
                    editDialogForm.Visible = true;
                }
                else
                {
                    LocaleDialogForm editLocaleDialogForm = new LocaleDialogForm(this, id);
                    editLocaleDialogForm.Visible = true;
                }

            }
        }
        //***************************MAIN MENU ITEMS **************************
        //! Пункт главного меню - открытие папки с данными 
        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = System.IO.Path.GetDirectoryName(settings.GetDialogDataPath());
            System.Diagnostics.Process.Start(path);
        }
        //! Пункт главного меню - сохранение всех данных
        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveData();
        }
        //! Пункт главного меню - Настройки
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperatorSettings fOperator = new OperatorSettings(this);
            this.Enabled = false;
            fOperator.Show();
        }
        //! Пункт главного меню - Синхронизация 
        private void SynchroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //синхронизация диалогов
            string loc = settings.getCurrentLocale();
            int added = 0;
            int copied = 0;
            foreach (string npc in dialogs.dialogs.Keys)
            {
                if (!dialogs.locales[loc].ContainsKey(npc))
                {
                    dialogs.locales[loc].Add(npc, new Dictionary<int, CDialog>());
                }
                foreach (int dialogID in dialogs.dialogs[npc].Keys)
                {
                    bool exist = true;
                    CDialog rus = dialogs.dialogs[npc][dialogID];
                    CDialog eng = new CDialog();
                    try
                    {
                        eng = dialogs.locales[loc][npc][dialogID];
                    }
                    catch
                    {
                        exist = false;
                    }

                    eng.Precondition = rus.Precondition;
                    eng.Nodes = rus.Nodes;
                    eng.Actions = rus.Actions;
                    eng.coordinates.Active = rus.coordinates.Active;

                    if (!exist)
                    {
                        eng.Holder = rus.Holder;
                        eng.coordinates = rus.coordinates;
                        eng.DialogID = rus.DialogID;
                        eng.Text = rus.Text;
                        eng.Title = rus.Title;
                        eng.version = 0;
                        dialogs.locales[loc][npc].Add(dialogID, eng);
                        added++;
                    }
                    else
                    {
                        dialogs.locales[loc][npc][dialogID] = eng;

                        // копирование русских строчек вместо пустых
                        if (rus.Title.Length > 0 && eng.Title.Length == 0)
                        {
                            eng.Title = rus.Title;
                            eng.version = rus.version - 1;
                            copied++;
                        }
                        if (rus.Text.Length > 0 && eng.Text.Length == 0)
                        {
                            eng.Text = rus.Text;
                            eng.version = rus.version - 1;
                            copied++;
                        }
                    }

                }
            }
            // удаление лишних диалогов
            int garb = 0;
            Dictionary<string, int> del = new Dictionary<string, int>();
            foreach (string npc in dialogs.locales[loc].Keys)
                foreach (int dialID in dialogs.locales[loc][npc].Keys)
                {
                    if (!dialogs.dialogs.ContainsKey(npc) && !del.ContainsKey(npc))
                    {
                        del.Add(npc, 0);
                        garb++;
                    }
                    else if (!dialogs.dialogs.ContainsKey(npc) && del.ContainsKey(npc))
                    {
                        garb++;
                        del[npc]++;
                    }
                    //else if (dialogs.dialogs.ContainsKey(npc) && !dialogs.dialogs[npc].ContainsKey(dialID))
                    //    del.Add(npc, dialID);
                }
            foreach (string npc in del.Keys)
                dialogs.locales[loc].Remove(npc);

            // синхронизация квестов            
            int empty = 0;
            int title = 0, desc = 0;

            foreach (CQuest quest in quests.quest.Values)
            {
                bool exist = true;
                CQuest local = new CQuest();
                try
                {
                    local = this.quests.locales[loc][quest.QuestID];
                }
                catch
                {
                    exist = false;
                }
                if (!exist)
                {
                    CQuest toadd = new CQuest();
                    toadd = (CQuest)quest.Clone();
                    toadd.Version = 0;
                    quests.locales[loc].Add(toadd.QuestID, toadd);
                    empty++;
                }
                else
                {
                    local.QuestInformation.Items = quest.QuestInformation.Items;
                    local.Additional.ShowProgress = quest.Additional.ShowProgress;
                    local.Additional.ListOfSubQuest = quest.Additional.ListOfSubQuest;
                    local.Additional.IsSubQuest = quest.Additional.IsSubQuest;
                    local.Additional.CantCancel = quest.Additional.CantCancel;
                    local.Precondition = quest.Precondition;
                    local.QuestPenalty = quest.QuestPenalty;
                    local.QuestRules = quest.QuestRules;
                    local.Reward = quest.Reward;
                    local.Target = quest.Target;
                    // копирование русских строчек вместо пустых
                    if (quest.QuestInformation.Title.Length > 0 && local.QuestInformation.Title.Length == 0)
                    {
                        local.QuestInformation.Title = quest.QuestInformation.Title;
                        local.Version = quest.Version - 1;
                        title++;
                    }
                    if (quest.QuestInformation.Description.Length > 0 && local.QuestInformation.Description.Length == 0)
                    {
                        local.QuestInformation.Description = quest.QuestInformation.Description;
                        local.Version = quest.Version - 1;
                        desc++;
                    }
                    if (quest.QuestInformation.DescriptionOnTest.Length > 0 && local.QuestInformation.DescriptionOnTest.Length == 0)
                    {
                        local.QuestInformation.DescriptionOnTest = quest.QuestInformation.DescriptionOnTest;
                        local.Version = quest.Version - 1;
                        desc++;
                    }
                    if (quest.QuestInformation.DescriptionClosed.Length > 0 && local.QuestInformation.DescriptionClosed.Length == 0)
                    {
                        local.QuestInformation.DescriptionClosed = quest.QuestInformation.DescriptionClosed;
                        local.Version = quest.Version - 1;
                        desc++;
                    }

                    if (quest.QuestInformation.onWin.Length > 0 && local.QuestInformation.onWin.Length == 0)
                    {
                        local.QuestInformation.onWin = quest.QuestInformation.onWin;
                        local.Version = quest.Version - 1;
                        title++;
                    }
                    if (quest.QuestInformation.onFailed.Length > 0 && local.QuestInformation.onFailed.Length == 0)
                    {
                        local.QuestInformation.onFailed = quest.QuestInformation.onFailed;
                        local.Version = quest.Version - 1;
                        desc++;
                    }
                }
            }


            // удаляем лишние квесты 
            List<int> trash = new List<int>();
            foreach (CQuest local in quests.locales[loc].Values)
            {
                if (!quests.quest.ContainsKey(local.QuestID))
                {
                    trash.Add(local.QuestID);
                    //if (local.QuestID <= 3500 && local.QuestID >= 2500)
                }
            }
            foreach (int shit in trash)
                quests.locales[loc].Remove(shit);

            string info = "Синхронизация исходных данных и переводов: \n";
            info += "Диалогов добавлено: " + added.ToString() + " Удалено: " + garb.ToString() + " Скопировано: " + copied.ToString() + "\n";
            info += "Квестов добавлено: " + empty.ToString() + " Удалено: " + trash.Count.ToString() + " Заголовков: " + title.ToString() + " Описаний: " + desc.ToString();
            MessageBox.Show(info, "Результаты синхронизации", MessageBoxButtons.OK, MessageBoxIcon.Information);

            /*
             * // дублированные ID диалогов
            Dictionary<int, int> shitty = new Dictionary<int, int>();
            Dictionary<int, int> dupl = new Dictionary<int, int>();
            foreach (string npc in dialogs.dialogs.Keys)
            {
                foreach (int dialogID in dialogs.dialogs[npc].Keys)
                {
                    if (!shitty.ContainsKey(dialogID))
                        shitty.Add(dialogID, 1);
                    else
                        shitty[dialogID]++;
                }
            }
            int stup = 0;
            foreach (int key in shitty.Keys)
                if (shitty[key] > 1)
                {
                    dupl.Add(key, shitty[key]);
                    stup += shitty[key];
                }
            labelXNode.Text = "dupl = " + dupl.Count.ToString() + ", total=" + stup.ToString();
             */
                }

                //! Пункт главного меню - Cтатистика
                private void StatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticsForm sf = new StatisticsForm(this, NPCBox.Items.Count, quests, dialogs, ManagerNPC);
            sf.Show();
        }
        //! Пункт главного меню - Выход
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            if (this.isDirty)
            {
                result = MessageBox.Show("Сохранить изменения?", "Вы не сохранили", MessageBoxButtons.YesNoCancel);
                switch(result)
                {
                    case DialogResult.Cancel: e.Cancel = true;  return;
                    case DialogResult.Yes: this.saveData(); break;
                    case DialogResult.No: break;
                }
            }
            settings.setLastNpcIndex(NPCBox.SelectedIndex);
            settings.saveSettings();
            tcpListener.stop();
        }

        private void DialogShower_MouseMove(object sender, MouseEventArgs e)
        {
            //DialogShower.Focus();
        }

        private void парсерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDataForm udf = new UpdateDataForm(this);
            udf.ShowDialog();
        }

        private void проверкаОшибокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckErrorForm cef = new CheckErrorForm(dialogs, quests, this);
            cef.Show();
        }

        private void btnClearRecycle_Click(object sender, EventArgs e)
        {
            foreach (TreeNode treeNode in this.treeDialogs.Nodes.Find("Recycle", true))
            {
                foreach (TreeNode recyckeNode in treeNode.Nodes)
                {
                    string nodeId = recyckeNode.Text;
                    removeDialog(int.Parse(nodeId), getDialogDictionary(currentNPC, dialogs.dialogs, dialogs.locales), dialogs.locales, treeDialogs);
                }
            }
        }

        public void selectQuestByID(int quest_id)
        {
            this.CentralDock.SelectedIndex = 1;
            CQuest quest = quests.getQuest(quest_id);
            if (quest != null)
            {
                CQuest temp = quest;
                while (temp.Additional.IsSubQuest != 0)
                    temp = quests.getQuest(temp.Additional.IsSubQuest);

                string qtext = temp.QuestID.ToString() + ": ";
                qtext += temp.QuestInformation.Title;
                NPCBox.SelectedValue = quest.Additional.Holder;
                QuestBox.SelectedItem = qtext;
            }

        }

        private void DialogShower_Click(object sender, EventArgs e)
        {
            DialogShower.Focus();
        }

        private void btnFilterNPC_Click(object sender, EventArgs e)
        {
            FilterNPCForm form = new FilterNPCForm(ref npcFilters, this);
            form.ShowDialog();
            this.fillNPCBox();
            updateNPCButtons();
        }

        private void updateNPCButtons()
        {
            bAddNPC.Enabled = true;
            bDelNPC.Enabled = true;
            foreach (KeyValuePair<string, bool> val in npcFilters)
            {
                if (!val.Value)
                {
                    bAddNPC.Enabled = false;
                    bDelNPC.Enabled = false;
                    return;
                }
            }
        }

        private void собратьЭдиторДляПередачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Несохранённые изменения не будут собраны. Вы уверены, что сейчас хотите собрать QuestEditor?", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            //Копировать сам эдитор
            string path = dialog.SelectedPath + "\\QuestEditor";
            string sourceParh = Directory.GetCurrentDirectory();
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить QuestEditor в путь :" + path + "\\QuestEditor", "Ошибка сохранения");
                return;
            }

            foreach (string dirPath in Directory.GetDirectories(sourceParh, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(sourceParh, path));
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось создать папку в пути:" + dirPath.Replace(sourceParh, path), "Ошибка сохранения");
                    return;
                }
            }

            foreach (string newPath in Directory.GetFiles(sourceParh, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    if (Path.GetFileName(newPath) == "Settings.xml") continue;
                    File.Copy(newPath, newPath.Replace(sourceParh, path), true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось скопировать файл " + newPath + " в пути:" + newPath.Replace(sourceParh, path), "Ошибка сохранения");
                    return;
                }
            }

            //Копировать локализацию
            sourceParh = settings.pathToLocalFiles;
            string locale_path = path + "\\source\\" + "\\local";
            Directory.CreateDirectory(locale_path);

            List<string> locales = new List<string>(settings.getListLocales());
            locales.Add("Russian");
            foreach (string locale in locales)
            {
                string lang_path = locale_path + "\\" + locale;
                Directory.CreateDirectory(lang_path);
                File.Copy(sourceParh + "\\" + locale + "\\DialogTexts.xml", lang_path + "\\DialogTexts.xml", true);
                File.Copy(sourceParh + "\\" + locale + "\\QuestTexts.xml", lang_path + "\\QuestTexts.xml", true);
            }

            //Копировать effects.json
            try
            {
                File.Copy(CEffectConstants.getPath(), path + "\\source\\Effects.json", true);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось скопировать файл " + CEffectConstants.getPath() + " в " + path + "\\source\\Effects.json", "Ошибка сохранения");
            }

            //Копировать blackbox
            try
            {
                File.Copy(CBlackBoxConstants.getPath(), path + "\\source\\blackboxs.json", true);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось скопировать файл " + CBlackBoxConstants.getPath() + " в " + path + "\\source\\blackboxs.json", "Ошибка сохранения");
            }
            try
            {
                File.Copy(CTutorialConstants.getPath(), path + "\\source\\TutorialPhases.json", true);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось скопировать файл " + CTutorialConstants.getPath() + " в " + path + "\\source\\TutorialPhases.json", "Ошибка сохранения");
            }

            try
            {
                File.Copy(CDungeonSpacesConstants.getPath(), path + "\\source\\dungeon_data.json", true);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось скопировать файл " + CDungeonSpacesConstants.getPath() + " в " + path + "\\source\\dungeon_data.json", "Ошибка сохранения");
            }
            //Копировать QuestData и DialogData

            string quest_data_path = path + "\\source\\Quests\\";
            string data_path = path + "\\source\\DialogData\\";
            if (loadFiles(settings.pathQuestDataFiles, quest_data_path) && loadFiles(settings.new_pathDialogsDataFiles, data_path))
                MessageBox.Show("QuestEditor готов. Сохранён в " + path, "Успех");
        }

        private bool loadFiles(string sourceParh, string data_path)
        {
            Directory.CreateDirectory(data_path);

            foreach (string dirPath in Directory.GetDirectories(sourceParh, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(sourceParh, data_path));
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось создать папку в пути:" + dirPath.Replace(sourceParh, data_path), "Ошибка сохранения");
                    return false;
                }
            }

            foreach (string newPath in Directory.GetFiles(sourceParh, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(sourceParh, data_path), true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось скопировать файл " + newPath + " в пути:" + newPath.Replace(sourceParh, data_path), "Ошибка сохранения");
                    return false;
                }
            }
            return true;
        }

        private void переместитьЛокализациюВИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tbFindDialogID_TextChanged(object sender, EventArgs e)
        {
            string text = (sender as TextBox).Text;
            int finded_dialogID;
            string finded_npcName = "";
            if (!int.TryParse(text, out finded_dialogID))
                return;
            foreach (int dialog_id in dialogs.dialogIDList.Keys)
            {
                if (dialog_id == finded_dialogID)
                {
                    finded_npcName = dialogs.dialogIDList[dialog_id];
                    break;
                }
            }
            if (finded_npcName == "") return;

            openNPC(finded_npcName);
            Listener.SelectCurrentNode(finded_dialogID);
        }

        private int count_the_words(string text)
        {
            int result = 0;
            foreach (string word in text.Split())
            {
                if (word.Trim().Any()) result++;
            }
            return result;
        }

        private void вытащитьНепереведённыеТекстыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SynchroToolStripMenuItem_Click(sender, e);
            string loc = settings.getCurrentLocale();
            string quest_path = "quests_" + loc + ".xlsx";
            string dialog_path = "dialog_" + loc + ".xlsx";
            int all_words = 0;
            int non_localcount_words = 0;
            int row = 1;
            var excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excel.Interactive = false;

            var worKbooK_dialogs = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK_dialogs.ActiveSheet;
            workSheet.Name = "Sheet1";

            foreach (string npc in dialogs.dialogs.Keys)
            {
                foreach (KeyValuePair<int, CDialog> value in dialogs.dialogs[npc])
                {
                    CDialog origin_d = value.Value;
                    if (origin_d.isAutoNode) continue;
                    CDialog local_d = this.dialogs.locales[loc][npc][value.Key];
                    int count_words = 0;
                    count_words += count_the_words(origin_d.Title);
                    count_words += count_the_words(origin_d.Text);
                    all_words += count_words;
                    if (origin_d.version != local_d.version)
                    {

                        workSheet.Cells[row, 1] = "Dialog";
                        workSheet.Cells[row, 2] = value.Key.ToString() + "_" + origin_d.version.ToString();
                        row++;
                        workSheet.Cells[row, 1] = "Title";
                        workSheet.Cells[row, 2] = origin_d.Title;
                        workSheet.Cells[row, 3] = local_d.Title;
                        row++;
                        workSheet.Cells[row, 1] = "Text";
                        workSheet.Cells[row, 2] = origin_d.Text;
                        workSheet.Cells[row, 3] = local_d.Text;
                        row++;
                        non_localcount_words += count_words;
                    }
                }
            }
            worKbooK_dialogs.SaveAs(Path.GetFullPath(dialog_path)); ;
            worKbooK_dialogs.Close();


            var worKbooK = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "Sheet1";

            row = 1;
            foreach (CQuest quest in quests.quest.Values)
                {
                    CQuest local = this.quests.locales[loc][quest.QuestID];

                    if (quest.QuestInformation.Title.Any() || quest.QuestInformation.Description.Any() ||
                        quest.QuestInformation.onWin.Any() || quest.QuestInformation.onFailed.Any() || quest.QuestInformation.onGet.Any())
                        if ((quest.Additional.ShowProgress > 0) && (quest.Additional.ShowProgress != 64))
                        {
                            int count_words = 0;
                            count_words += count_the_words(quest.QuestInformation.Title);
                            count_words += count_the_words(quest.QuestInformation.Description);
                            count_words += count_the_words(quest.QuestInformation.DescriptionClosed);
                            count_words += count_the_words(quest.QuestInformation.DescriptionOnTest);
                            count_words += count_the_words(quest.QuestInformation.onGet);
                            count_words += count_the_words(quest.QuestInformation.onWin);
                            count_words += count_the_words(quest.QuestInformation.onFailed);
                            all_words += count_words;
                            if (local.Version != quest.Version)
                            {
                                non_localcount_words += count_words;
                                worKsheeT.Cells[row, 1] = "Quest";
                                worKsheeT.Cells[row, 2] = quest.QuestID.ToString() + "_" + quest.Version.ToString();
                                row++;
                                worKsheeT.Cells[row, 1] = "Title";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.Title;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.Title;
                                
                                row++;
                                worKsheeT.Cells[row, 1] = "Description";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.Description;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.Description;
                                row++;
                                worKsheeT.Cells[row, 1] = "DescriptionClosed";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.DescriptionClosed;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.DescriptionClosed;
                                row++;
                                worKsheeT.Cells[row, 1] = "DescriptionOnTest";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.DescriptionOnTest;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.DescriptionOnTest;
                                row++;
                                worKsheeT.Cells[row, 1] = "onGet";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.onGet;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.onGet;
                                row++;
                                worKsheeT.Cells[row, 1] = "onWin";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.onWin;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.onWin;
                                row++;
                                worKsheeT.Cells[row, 1] = "onFailed";
                                worKsheeT.Cells[row, 2] = quest.QuestInformation.onFailed;
                                worKsheeT.Cells[row, 3] = local.QuestInformation.onFailed;
                                row++;
                            }
                        }
                }
            worKbooK.SaveAs(Path.GetFullPath(quest_path)); ;

            excel.Quit();
            MessageBox.Show("Количество слов: " + all_words + ". Непереведённых: "+ non_localcount_words + "("+ Convert.ToDouble(non_localcount_words)/all_words*100 + "%)", "Итог");
        }

        private void диалоговToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Exel File(*.xlsx)|*.xlsx";
            if (DialogResult.OK != openFile.ShowDialog()) return;
            string locale = Path.GetFileNameWithoutExtension(openFile.FileName).Replace("dialog_", "");

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(openFile.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets["Sheet1"];

            int rw = sheet.UsedRange.Rows.Count;
            int cl = sheet.UsedRange.Columns.Count;
            int count = 0;
            for (int i = 1; i <= rw; i++)
            {
                string line = (string)(sheet.UsedRange.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2;
                if (line == null || !line.Contains("Dialog")) continue;

                line = (string)(sheet.UsedRange.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value2;
                string[] tmp = line.Replace(";", "").Split('_');
                int dialog_id = int.Parse(tmp[0]);
                int version = int.Parse(tmp[1]);
                i++;
                string Title = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                i++;
                string Text = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;

                foreach (string npc in dialogs.locales[locale].Keys)
                {
                    foreach (KeyValuePair<int, CDialog> dialog in dialogs.locales[locale][npc])
                    {
                        if (dialog.Key == dialog_id)
                            if (dialog.Value.version <= version)
                            {
                                dialog.Value.version = version;
                                dialog.Value.Title = Title;
                                dialog.Value.Text = Text;
                            }
                            else count++;
                    }
                }
            }
            wb.Close();
            Console.WriteLine(count);
        }

        private void квестовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Exel File(*.xlsx)|*.xlsx";
            if (DialogResult.OK != openFile.ShowDialog()) return;
            string locale = Path.GetFileNameWithoutExtension(openFile.FileName).Replace("quests_", "");

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(openFile.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets["Sheet1"];

            int rw = sheet.UsedRange.Rows.Count;
            int cl = sheet.UsedRange.Columns.Count;
            int count = 0;
            for (int i = 1; i <= rw; i++)
            {

                string line = (string)(sheet.UsedRange.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2;
                if(line == null ||!line.Contains("Quest")) continue;
                line = Convert.ToString((sheet.UsedRange.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value2);
                string[] tmp = line.Replace(";", "").Split('_');
                int quest_id = int.Parse(tmp[0]);
                int version = int.Parse(tmp[1]);
                i++;
                string Title = Convert.ToString((sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2);
                i++;
                string Description = Convert.ToString((sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2);
                i++;
                string DescriptionClosed = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                i++;
                string DescriptionOnTest = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                i++;
                string onGet = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                i++;
                string onWin = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                i++;
                string onFailed = (string)(sheet.UsedRange.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;

                if (!quests.locales[locale].ContainsKey(quest_id))
                {
                    count++;
                    Console.WriteLine("Такого квеста нет " + quest_id.ToString());
                    continue;
                }
                CQuest quest = quests.locales[locale][quest_id];
                if (quest.Version <= version)
                {
                    quest.QuestInformation.Title = Title;
                    quest.QuestInformation.Description = Description;
                    quest.QuestInformation.DescriptionOnTest = DescriptionOnTest;
                    quest.QuestInformation.DescriptionClosed = DescriptionClosed;
                    quest.QuestInformation.onWin = onWin;
                    quest.QuestInformation.onFailed = onFailed;
                    quest.QuestInformation.onGet = onGet;
                    quest.Version = version;
                }
                else count++;
            }
            Console.WriteLine(count);
            app.Quit();
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            settings.setLanguage("ru-RU");
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            ApplyResourceToControl(resources, this);
            ApplyResourceToControl(resources, this.menuMainControl);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            settings.setLanguage("en");
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            ApplyResourceToControl(resources, this);
            ApplyResourceToControl(resources, this.menuMainControl);
        }

        private static void ApplyResourceToControl(ComponentResourceManager res, Control control)
        {
            if (control.Name == "menuMainControl")
            {
                foreach (ToolStripMenuItem c in (control as MenuStrip).Items)
                    ApplyResourceToToolStrip(res, c);
            }
            foreach (Control c in control.Controls)
                ApplyResourceToControl(res, c);

            var text = res.GetString(String.Format("{0}.Text", control.Name));
            control.Text = text ?? control.Text;
        }


        private static void ApplyResourceToToolStrip(ComponentResourceManager res, ToolStripItem control)
        { 
            foreach (ToolStripDropDownItem c in (control as ToolStripDropDownItem).DropDownItems)
                ApplyResourceToToolStrip(res, c);
            var text = res.GetString(String.Format("{0}.Text", control.Name));
            Console.WriteLine(control.Name + " " + text + "; " + control.Text);
            control.Text = text ?? control.Text;
        }

        private void bFindNPCReputation_Click(object sender, EventArgs e)
        {
            setNPCReputationCheckEnvironment();
            dgvReview.Rows.Clear();
            string NPC_name = cbNPCList.Text;
            
            foreach (CQuest quest in quests.quest.Values)
            {
                int questID = quest.QuestID;
                foreach (KeyValuePair<string, int> keyValue in quest.Reward.NPCReputation)
                {
                    if (!NPC_name.Any()||NPC_name == keyValue.Key)
                    {
                        object[] row = { questID, quest.QuestInformation.Title, keyValue.Key, keyValue.Value };
                        dgvReview.Rows.Add(row);
                    }
                }
            }
            statusLabel.Text = "Выведено: " + dgvReview.RowCount.ToString();
        }

        private void fractionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFraction = fractionBox.SelectedItem.ToString().Split(' ')[0].Trim();
            var currentDialogs = getDialogDictionary(currentFraction, CFractionDialogs.dialogs, CFractionDialogs.locales);
            CDialog rootDialog = getRootDialog(currentDialogs);
            if (rootDialog != null)
                fillDialogTree(rootDialog, currentDialogs, this.treeFractionDialogs, CFractionDialogs.locales);
            DialogSelected(true);
        }

        private void updateKnowledgeTab()
        {
            cbKnowledgeCategory.Items.Clear();
            cbKnowledgeCategory.Enabled = true;
            cbKnowledgeCategory.Parent.Enabled = true;
            foreach (KeyValuePair<int, string> i in this.knowledgeCategory.getAllItems())
            {
                cbKnowledgeCategory.Items.Add(i.Value);
            }
        }

        private void NPCBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbKnowledgeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbKnowlegeTypeValue.Items.Clear();
            int id = knowledgeCategory.getID(cbKnowledgeCategory.SelectedItem.ToString());
            switch(id)
            {
                case 0:
                    foreach (var i in this.ManagerNPC.NpcData.Values)
                        cbKnowlegeTypeValue.Items.Add(i.rusName);
                    break;
                case 1:
                    foreach (var i in this.fractions.getListOfFractions())
                        cbKnowlegeTypeValue.Items.Add(i.Value);
                    break;
                case 2:
                    foreach (var i in this.spacesConst.getSpacesNames())
                        cbKnowlegeTypeValue.Items.Add(i);
                    break;
            }
            cbKnowlegeTypeValue.SelectedIndex = 1;
        }

        private void cbKnowlegeTypeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeKnowladge.Nodes.Clear();
            string key = cbKnowlegeTypeValue.SelectedItem.ToString();
            TreeNode a = treeKnowladge.Nodes.Add(key, key);

            a.Nodes.Add("true", "Обязательные знания");
            a.Nodes.Add("false", "Дополнительные знания знания");
        }

        private void updateKnowladgeTree()
        {

        }

    }
}
 