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
    public partial class MainForm : Form
    {

        bool inSaving = false;

        int STARTTALKQUESTS = 2500;
        int ENDTALKQUESTS = 3500;

        public string currentNPC="";
        CDialogs dialogs;
        CQuests quests;
        NodeDragHandler Listener;
        MapNodeDragHandler MapListener;
        PLayer edgeLayer;
        PNodeList nodeLayer;
        PLayer edgeNPClinkLayer;
        PNodeList nodeNPClinkLayer;

        Dictionary<PNode, GraphProperties> graphs = new Dictionary<PNode, GraphProperties>();
        Dictionary<string, PNode> mapGraphs = new Dictionary<string, PNode>();
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
        public CZoneConstatnts zoneConst;
        public CSpacesConstants spacesConst;
        public CTriggerConstants triggerConst;
        public CTPConstants tpConst;
        public CSettings settings;
        public COperNotes manageNotes;
        public CFracConstants fractions;
        public CGUIConst gui;
        public CEffectConstants effects;
        public CBalance balances;

        int currentQuestDialog;
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
            this.zoneConst = new CZoneConstatnts();

        }

        void clearQuestTab()
        {
            treeQuest.Nodes.Clear();
            splitQuestsContainer.Panel2.Controls.Clear();
            QuestBox.Items.Clear();
        }

        private void NPCBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!NPCBox.SelectedItem.ToString().Equals(currentNPC))
            {
                clearQuestTab();
                currentNPC = NPCBox.SelectedItem.ToString();

                if (CentralDock.SelectedIndex == 0)
                {
                    fillQuestChangeBox(true);
                    QuestBox.SelectedIndex = 0;
                    QuestBox.Enabled = false;
                    bAddQuest.Enabled = false;
                    bRemoveQuest.Enabled = false;
                }
                else if (CentralDock.SelectedIndex == 1)
                    fillQuestChangeBox(false);

            }
        }

        void fillNPCBOX()
        {
            this.NPCBox.Items.Clear();
            foreach (string holder in this.dialogs.dialogs.Keys)
            {
                this.NPCBox.Items.Add(holder.ToString());
                this.NPCBox.Sorted = true;
            }
            this.NPCBox.Text = "Пожалуйста, выберите NPC.";
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
                        treeNode.Nodes.Add(subdialog.ToString(), subdialog.ToString());

                }

                this.fillNPCBoxSubquests(this.dialogs.dialogs[currentNPC][subdialog]);

            }

        }

        internal void onSelectNode(int dialogID)
        {
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
            if (settings.getMode() == 0)
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
            if (settings.getMode() == 0)
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
            if (settings.getMode() == 0)
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

        public void startEmulator(int dialogID)//, bool isHandle)
        {
            CDialog rootDialog = getDialogOnDialogID(dialogID);
            EmulatorsplitContainer.Panel2.Controls.Clear();
            titles = new Dictionary<LinkLabel,int>();
            Label NPCText = new Label();
            NPCText.Text = rootDialog.Text;
            NPCText.AutoSize = true;
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


                dialogLink.Text = dialog + ". " + dialogs.dialogs[currentNPC][dialog].Title + actionResult;
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

        private void dialogLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            int choosenDialogID = (int)e.Link.LinkData;
            CDialog choosenDialog = dialogs.dialogs[currentNPC][choosenDialogID];
            toolStripStatusLabel.Text = "";
            if (choosenDialog.Actions.CompleteQuests.Any())
                toolStripStatusLabel.Text += "Завершенные: " + getListAsString(choosenDialog.Actions.CompleteQuests);
            if (choosenDialog.Actions.GetQuests.Any())
                toolStripStatusLabel.Text += " Взятые: " + getListAsString(choosenDialog.Actions.GetQuests);

            if (choosenDialog.Actions.Event == 1)
            {
                toolStripStatusLabel.Text += " Торговля. Выход.";
                EmulatorsplitContainer.Panel2.Controls.Clear();
                Listener.setCurrentNode(choosenDialogID);
                //Listener.setCurrentNode(0);
            }
            else if (choosenDialog.Actions.Event == 2)
            {
                toolStripStatusLabel.Text += " Обмен. Выход.";
                EmulatorsplitContainer.Panel2.Controls.Clear();
                Listener.setCurrentNode(choosenDialogID);
            }
            else if (choosenDialog.Actions.Exit)
            {
                toolStripStatusLabel.Text += " Выход.";
                EmulatorsplitContainer.Panel2.Controls.Clear();
                Listener.setCurrentNode(choosenDialogID);

            }
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

        void fillQuestChangeBox(bool onlyDialogs)
        {
            QuestBox.SelectedItem = null;
            QuestBox.Items.Clear();
            foreach (CQuest quest in quests.getQuestAndTitleOnNPCName(currentNPC))
            {
                if (!onlyDialogs)
                {
                    if (quest.QuestID < 2500 || quest.QuestID > 3500)
                     QuestBox.Items.Add(quest.QuestID + ": " + quest.QuestInformation.Title);
                }
                else if (quest.QuestID >= 2500 && quest.QuestID < 3500)
                    QuestBox.Items.Add(quest.QuestID + ": " + quest.QuestInformation.Title);
            }
        }

        private void QuestBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitQuestsContainer.Panel2.Controls.Clear();
            treeQuest.Nodes.Clear();

            if (CentralDock.SelectedIndex == 0)
            {
                if (!QuestBox.SelectedItem.ToString().Equals(""))
                {
                    this.currentQuestDialog = int.Parse(QuestBox.SelectedItem.ToString().Split(':')[0].Trim());
                    DialogSelected(true);
                }

            }       
            else if (CentralDock.SelectedIndex == 1)
            {
                this.currentQuestDialog = int.Parse(QuestBox.SelectedItem.ToString().Split(':')[0].Trim());
                addNodeOnTreeQuest(this.currentQuestDialog);
                bRemoveQuest.Enabled = true;
            }
            treeQuest.ExpandAll();
        }

        private void bZoomIn_Click(object sender, EventArgs e)
        {
   
        }

        private void bZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void onSelectTab(object sender, EventArgs e)
        {
            bCopyEvents.Enabled = false;

            if (CentralDock.SelectedIndex == 0)
            {
                NPCBox.Enabled = true;
                bAddNPC.Enabled = true;
                bDelNPC.Enabled = true;
                if (!currentNPC.Equals(""))
                {
                    QuestBox.Items.Clear();
                    fillQuestChangeBox(true);
                    QuestBox.Enabled = false;
                    bRemoveQuest.Enabled = false;
                    bAddQuest.Enabled = false;
                    QuestBox.SelectedIndex = 0;
                }
            }
            else if (CentralDock.SelectedIndex == 1)
            {
                //System.Console.WriteLine("SelectQueststab");
                NPCBox.Enabled = true;
                bAddNPC.Enabled = true;
                bDelNPC.Enabled = true;
                QuestBox.Enabled = true;
                bRemoveQuest.Enabled = false;
                bAddQuest.Enabled = true;
                QuestBox.Items.Clear();
                fillQuestChangeBox(false);
                QuestBox.Text = "Пожалуйста, выберите квест.";
            }
            else if (CentralDock.SelectedIndex == 2)
            {
                NPCBox.Enabled = false;
                QuestBox.Enabled = false;
                bAddNPC.Enabled = false;
                bDelNPC.Enabled = false;
                bAddQuest.Enabled = false;
                bRemoveQuest.Enabled = false;
                fillNPCLinkView();
            }
            else if (CentralDock.SelectedIndex == 3)
            {
                NPCBox.Enabled = false;
                QuestBox.Enabled = false;
                bAddNPC.Enabled = false;
                bDelNPC.Enabled = false;
                bAddQuest.Enabled = false;
                bRemoveQuest.Enabled = false;
                calcStatistic();
            }
            else if (CentralDock.SelectedIndex == 4)
            {
                FillTabManage();
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
            if ((!questConst.isSimple(curQuest.Target.QuestType) || settings.getMode() == settings.MODE_LOCALIZATION)  && (curQuest.Additional.ListOfSubQuest.Any()))
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


        private void bSaveDialogs_Click(object sender, EventArgs e)
        {

                this.saveData();
        }

        private void bSaveQuests_Click(object sender, EventArgs e)
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
            //while (iFirstDialogID < 2500)
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
            firstDialog.Add(dialogID,new CDialog(Name,"","",newNPCID,new CDialogPrecondition(),new Actions(),new List<int>(),dialogID, 0));

            dialogs.dialogs.Add(Name, firstDialog);
            quests.quest.Add(newNPCID, new CQuest(newNPCID, 0, new CQuestInformation(), new CQuestPrecondition(), new CQuestRules(), new CQuestReward(), new CQuestAdditional(Name), new CQuestTarget(1), new CQuestPenalty()));
            quests.startQuests.Add(newNPCID);

            NPCBox.Items.Add(Name);
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
                    if (quest.QuestID >= 2500 && quest.QuestID < 3500)
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
            {
                bAddEvent.Enabled = true;
            }

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
            if (settings.getMode() == 0)
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
                if (settings.getMode() == 0)
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
            {
                quests.quest[getQuestOnQuestID(questID).Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);
            }
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
            QuestBox.SelectedIndex = QuestBox.Items.Count - 1;
        }

        public void setQuestISGroup(int questID, bool GroupState)
        {
            quests.setGroup(questID, GroupState);
        }

        public void setQuestISClan(int questID, bool ClanState)
        {
            quests.setClan(questID, ClanState);
        }

        private void выборОператораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperatorSettings fOperator = new OperatorSettings(this);
            this.Enabled = false;
            fOperator.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void calcStatistic()
        {
            string str = "";
            
            float Credits = new float();
            int countOfQuests = 0;

            int countOfEx0Quests = 0;
            int countOfEx1Quests = 0;
            int countOfEx2Quests = 0;
            int countOfAmountGold = 0;
            

            List<int> lExpirience = new List<int>(3);
            lExpirience.Add(0); lExpirience.Add(0); lExpirience.Add(0);
            foreach (CQuest quest in quests.quest.Values)
            {
                if (quest.Additional.IsSubQuest == 0)
                {
                    countOfQuests++;
                    //if (quest.Reward.Expirience.Any())
                    //{
                    //    if (quest.Reward.Expirience[0] != 0)
                    //        countOfEx0Quests++;
                    //    if (quest.Reward.Expirience[1] != 0)
                    //        countOfEx1Quests++;
                    //    if (quest.Reward.Expirience[2] != 0)
                    //        countOfEx2Quests++;
                    //}
                    if (quest.Reward.Credits != 0)
                        countOfAmountGold++;
                }
                Credits += quest.Reward.Credits;
                //if (quest.Reward.Expirience.Any())
                //{
                //    lExpirience[0] += quest.Reward.Expirience[0];
                //    lExpirience[1] += quest.Reward.Expirience[1];
                //    lExpirience[2] += quest.Reward.Expirience[2];
                //}
            }

            str += "Общее количество квестов:        " + countOfQuests.ToString() +"\n\n";
            str += "По наградам:\n";
            str += "   Общее количество денег:         ";
            str += (Credits.ToString() + "руб." + "   Среднее:" + (Credits / countOfAmountGold).ToString() + "руб.\n");
            str += "   Общее количество опыта:\n";
            str += "      Боевого:         ";
            if (countOfEx0Quests != 0 || lExpirience[0]!=0)
                str += (lExpirience[0].ToString() + "   Среднее:" + (lExpirience[0] / countOfEx0Quests).ToString() + "\n");

            str += "      Сурайвального:         ";
            if (countOfEx1Quests != 0 || lExpirience[1] != 0)
            str += (lExpirience[1].ToString() + "   Среднее:" + (lExpirience[1] / countOfEx1Quests).ToString() + "\n");
            str += "      Поддержка.:         ";
            if (countOfEx2Quests != 0 || lExpirience[2] != 0)
            str += (lExpirience[2].ToString() + "   Среднее:" + (lExpirience[2] / countOfEx2Quests).ToString() + "\n");
            lStatistic.Text = str;

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


        public void onChangeMode()
        {
            //System.Console.WriteLine("MainForm::onChangeMode");
            //System.Console.WriteLine("mode:" + settings.getMode().ToString());

            if (settings.getMode() == 1)
            {

                for (int i = 2; i < CentralDock.TabPages.Count; i++)
                {

                    if (CentralDock.TabPages[i].Name != "tabTranslate")
                    {
                        //System.Console.WriteLine("tab:" + CentralDock.TabPages[i].Name + ":false");
                        CentralDock.TabPages[i].Enabled = false;
                    }
                    else
                    {
                        //System.Console.WriteLine("tab:" + CentralDock.TabPages[i].Name + ":true");
                        CentralDock.TabPages[i].Enabled = true;
                    }
                }
            }
            else
            {
                for (int i = 1; i < CentralDock.TabPages.Count; i++)
                {

                    if (CentralDock.TabPages[i].Name == "tabTranslate")
                    {
                        //System.Console.WriteLine("tab:" + CentralDock.TabPages[i].Name + ":false");
                        CentralDock.TabPages[i].Enabled = false;
                    }
                    else
                    {
                        //System.Console.WriteLine("tab:" + CentralDock.TabPages[i].Name + ":true");
                        CentralDock.TabPages[i].Enabled = true;
                    }
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

        private void bFindDialogDifference_Click(object sender, EventArgs e)
        {
            this.translate_checker = 1;
            diffGridView.Rows.Clear();
            var diff = dialogs.getDialogDifference(settings.getCurrentLocale());
            var type = "Диалог";

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version };
                    diffGridView.Rows.Add(row);
                }
            }
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

        private void bFindQuestDifference_Click(object sender, EventArgs e)
        {
            diffGridView.Rows.Clear();
            this.translate_checker = 2;
            var diff = quests.getQuestDifference(settings.getCurrentLocale());
            var type = "Квест";

            foreach (var name in diff.Keys)
            {
                foreach (var id in diff[name].Keys)
                {
                    object[] row = { type, name, id, diff[name][id].old_version, diff[name][id].cur_version };
                    diffGridView.Rows.Add(row);
                }
            }
        }

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

        public bool isStartQuest(int quest_id)
        {
            return quests.isStartQuest(quest_id);
        }

        private void bSaveQuests_Click_1(object sender, EventArgs e)
        {
            this.saveData();
        }


    }
}
 