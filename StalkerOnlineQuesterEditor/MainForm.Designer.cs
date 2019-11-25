using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;

namespace StalkerOnlineQuesterEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitDialogs = new System.Windows.Forms.SplitContainer();
            this.gbDialogsEditor = new System.Windows.Forms.GroupBox();
            this.splitDialogsTreeAndCanvas = new System.Windows.Forms.SplitContainer();
            this.treeDialogs = new System.Windows.Forms.TreeView();
            this.DialogShower = new UMD.HCIL.Piccolo.PCanvas();
            this.panelDialogTools = new System.Windows.Forms.Panel();
            this.lFindDialogID = new System.Windows.Forms.Label();
            this.tbFindDialogID = new System.Windows.Forms.TextBox();
            this.btnClearRecycle = new System.Windows.Forms.Button();
            this.labelDrawingTip = new System.Windows.Forms.Label();
            this.bCenterizeDialogShower = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.labelYNode = new System.Windows.Forms.Label();
            this.labelXNode = new System.Windows.Forms.Label();
            this.bRemoveDialog = new System.Windows.Forms.Button();
            this.bEditDialog = new System.Windows.Forms.Button();
            this.bAddDialog = new System.Windows.Forms.Button();
            this.gbEmulator = new System.Windows.Forms.GroupBox();
            this.splitDialogsEmulator = new System.Windows.Forms.SplitContainer();
            this.splitQuestsContainer = new System.Windows.Forms.SplitContainer();
            this.treeQuest = new System.Windows.Forms.TreeView();
            this.labelQuestTree = new System.Windows.Forms.Label();
            this.treeQuestBuffer = new System.Windows.Forms.TreeView();
            this.labelBuffer = new System.Windows.Forms.Label();
            this.NPCBox = new System.Windows.Forms.ComboBox();
            this.labelChosenNPC = new System.Windows.Forms.Label();
            this.CentralDock = new System.Windows.Forms.TabControl();
            this.tabDialogs = new System.Windows.Forms.TabPage();
            this.tabQuests = new System.Windows.Forms.TabPage();
            this.panelQuestTools = new System.Windows.Forms.Panel();
            this.bClearBuffer = new System.Windows.Forms.Button();
            this.bCutEvents = new System.Windows.Forms.Button();
            this.bPasteEvents = new System.Windows.Forms.Button();
            this.bCopyEvents = new System.Windows.Forms.Button();
            this.bQuestDown = new System.Windows.Forms.Button();
            this.bQuestUp = new System.Windows.Forms.Button();
            this.bRemoveEvent = new System.Windows.Forms.Button();
            this.bEditEvent = new System.Windows.Forms.Button();
            this.bAddEvent = new System.Windows.Forms.Button();
            this.panelSelectQuest = new System.Windows.Forms.Panel();
            this.bRemoveQuest = new System.Windows.Forms.Button();
            this.bAddQuest = new System.Windows.Forms.Button();
            this.QuestBox = new System.Windows.Forms.ComboBox();
            this.labelChosenQuest = new System.Windows.Forms.Label();
            this.tabFraction = new System.Windows.Forms.TabPage();
            this.bRemoveFracDialog = new System.Windows.Forms.Button();
            this.bEditFracDialog = new System.Windows.Forms.Button();
            this.bAddFracDialog = new System.Windows.Forms.Button();
            this.treeFractionDialogs = new System.Windows.Forms.TreeView();
            this.fractionDialogShower = new UMD.HCIL.Piccolo.PCanvas();
            this.label1 = new System.Windows.Forms.Label();
            this.fractionBox = new System.Windows.Forms.ComboBox();
            this.tabInfoNPC = new System.Windows.Forms.TabPage();
            this.npcLinkShower = new UMD.HCIL.Piccolo.PCanvas();
            this.panelNpcLinkControls = new System.Windows.Forms.Panel();
            this.labelAdviceNpcLink = new System.Windows.Forms.Label();
            this.bNpcLinkExecute = new System.Windows.Forms.Button();
            this.tabReview = new System.Windows.Forms.TabPage();
            this.dgvReview = new System.Windows.Forms.DataGridView();
            this.colNPCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDialogsNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuestsNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRussianName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelReviewButtons = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNPCList = new System.Windows.Forms.ComboBox();
            this.bFindNPCReputation = new System.Windows.Forms.Button();
            this.gbNPCcheck = new System.Windows.Forms.GroupBox();
            this.cbOnlyOnLocation = new System.Windows.Forms.CheckBox();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.numQuests = new System.Windows.Forms.NumericUpDown();
            this.labelLessThan2 = new System.Windows.Forms.Label();
            this.cbNumQuests = new System.Windows.Forms.CheckBox();
            this.numDialogs = new System.Windows.Forms.NumericUpDown();
            this.labelLessThan1 = new System.Windows.Forms.Label();
            this.cbNumDialogs = new System.Windows.Forms.CheckBox();
            this.bFindNPC = new System.Windows.Forms.Button();
            this.gbQuestCheck = new System.Windows.Forms.GroupBox();
            this.labelItemTarget = new System.Windows.Forms.Label();
            this.cbItemTarget = new System.Windows.Forms.ComboBox();
            this.labelItemReward = new System.Windows.Forms.Label();
            this.cbItemReward = new System.Windows.Forms.ComboBox();
            this.bFindQuest = new System.Windows.Forms.Button();
            this.tabManage = new System.Windows.Forms.TabPage();
            this.dgvManage = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subevents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subNPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dialogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rewardBattle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rewardSurvive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rewardSupport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rewardCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rewardItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeatPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Legend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.worked = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bSaveManage = new System.Windows.Forms.Button();
            this.tabTranslate = new System.Windows.Forms.TabPage();
            this.dgvLocaleDiff = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npc_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cur_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.new_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RusText1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EngText1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelDiffLocale = new System.Windows.Forms.Panel();
            this.labelLocalizeOuput = new System.Windows.Forms.Label();
            this.cbActualFinder = new System.Windows.Forms.CheckBox();
            this.cbOutdatedFinder = new System.Windows.Forms.CheckBox();
            this.bFindQuestDifference = new System.Windows.Forms.Button();
            this.bFindDialogDifference = new System.Windows.Forms.Button();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEngText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSearchTools = new System.Windows.Forms.Panel();
            this.cbIgnoreCase = new System.Windows.Forms.CheckBox();
            this.labelSearchResult = new System.Windows.Forms.Label();
            this.labelPhraseToSearch = new System.Windows.Forms.Label();
            this.tbPhraseToSearch = new System.Windows.Forms.TextBox();
            this.bStartSearch = new System.Windows.Forms.Button();
            this.tabKnowledge = new System.Windows.Forms.TabPage();
            this.cbKnowlegeTypeValue = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbKnowledgeCategory = new System.Windows.Forms.ComboBox();
            this.panelSelectNPC = new System.Windows.Forms.Panel();
            this.btnFilterNPC = new System.Windows.Forms.Button();
            this.btnNextNPC = new System.Windows.Forms.Button();
            this.btnBackNPC = new System.Windows.Forms.Button();
            this.bDelNPC = new System.Windows.Forms.Button();
            this.bAddNPC = new System.Windows.Forms.Button();
            this.menuMainControl = new System.Windows.Forms.MenuStrip();
            this.menuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSynchronize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.русскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.обновленияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.парсерыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверкаОшибокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.собратьЭдиторДляПередачиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вытащитьНепереведённыеТекстыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьПереводToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.диалоговToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.квестовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolTipDialogs = new System.Windows.Forms.ToolTip(this.components);
            this.treeKnowladge = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogs)).BeginInit();
            this.splitDialogs.Panel1.SuspendLayout();
            this.splitDialogs.Panel2.SuspendLayout();
            this.splitDialogs.SuspendLayout();
            this.gbDialogsEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogsTreeAndCanvas)).BeginInit();
            this.splitDialogsTreeAndCanvas.Panel1.SuspendLayout();
            this.splitDialogsTreeAndCanvas.Panel2.SuspendLayout();
            this.splitDialogsTreeAndCanvas.SuspendLayout();
            this.panelDialogTools.SuspendLayout();
            this.gbEmulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogsEmulator)).BeginInit();
            this.splitDialogsEmulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQuestsContainer)).BeginInit();
            this.splitQuestsContainer.Panel1.SuspendLayout();
            this.splitQuestsContainer.SuspendLayout();
            this.CentralDock.SuspendLayout();
            this.tabDialogs.SuspendLayout();
            this.tabQuests.SuspendLayout();
            this.panelQuestTools.SuspendLayout();
            this.panelSelectQuest.SuspendLayout();
            this.tabFraction.SuspendLayout();
            this.tabInfoNPC.SuspendLayout();
            this.panelNpcLinkControls.SuspendLayout();
            this.tabReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReview)).BeginInit();
            this.panelReviewButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbNPCcheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDialogs)).BeginInit();
            this.gbQuestCheck.SuspendLayout();
            this.tabManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManage)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabTranslate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocaleDiff)).BeginInit();
            this.panelDiffLocale.SuspendLayout();
            this.tabSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.panelSearchTools.SuspendLayout();
            this.tabKnowledge.SuspendLayout();
            this.panelSelectNPC.SuspendLayout();
            this.menuMainControl.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitDialogs
            // 
            resources.ApplyResources(this.splitDialogs, "splitDialogs");
            this.splitDialogs.Name = "splitDialogs";
            // 
            // splitDialogs.Panel1
            // 
            this.splitDialogs.Panel1.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.splitDialogs.Panel1, "splitDialogs.Panel1");
            this.splitDialogs.Panel1.Controls.Add(this.gbDialogsEditor);
            // 
            // splitDialogs.Panel2
            // 
            this.splitDialogs.Panel2.Controls.Add(this.gbEmulator);
            this.splitDialogs.TabStop = false;
            // 
            // gbDialogsEditor
            // 
            this.gbDialogsEditor.Controls.Add(this.splitDialogsTreeAndCanvas);
            this.gbDialogsEditor.Controls.Add(this.panelDialogTools);
            resources.ApplyResources(this.gbDialogsEditor, "gbDialogsEditor");
            this.gbDialogsEditor.Name = "gbDialogsEditor";
            this.gbDialogsEditor.TabStop = false;
            // 
            // splitDialogsTreeAndCanvas
            // 
            this.splitDialogsTreeAndCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitDialogsTreeAndCanvas, "splitDialogsTreeAndCanvas");
            this.splitDialogsTreeAndCanvas.Name = "splitDialogsTreeAndCanvas";
            // 
            // splitDialogsTreeAndCanvas.Panel1
            // 
            this.splitDialogsTreeAndCanvas.Panel1.Controls.Add(this.treeDialogs);
            // 
            // splitDialogsTreeAndCanvas.Panel2
            // 
            this.splitDialogsTreeAndCanvas.Panel2.Controls.Add(this.DialogShower);
            this.splitDialogsTreeAndCanvas.TabStop = false;
            // 
            // treeDialogs
            // 
            resources.ApplyResources(this.treeDialogs, "treeDialogs");
            this.treeDialogs.Name = "treeDialogs";
            this.treeDialogs.TabStop = false;
            this.treeDialogs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDialogs_GotFocus);
            // 
            // DialogShower
            // 
            this.DialogShower.AllowDrop = true;
            this.DialogShower.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.DialogShower, "DialogShower");
            this.DialogShower.GridFitText = false;
            this.DialogShower.Name = "DialogShower";
            this.DialogShower.RegionManagement = true;
            this.DialogShower.Click += new System.EventHandler(this.DialogShower_Click);
            this.DialogShower.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DialogShower_MouseMove);
            // 
            // panelDialogTools
            // 
            resources.ApplyResources(this.panelDialogTools, "panelDialogTools");
            this.panelDialogTools.Controls.Add(this.lFindDialogID);
            this.panelDialogTools.Controls.Add(this.tbFindDialogID);
            this.panelDialogTools.Controls.Add(this.btnClearRecycle);
            this.panelDialogTools.Controls.Add(this.labelDrawingTip);
            this.panelDialogTools.Controls.Add(this.bCenterizeDialogShower);
            this.panelDialogTools.Controls.Add(this.labelYNode);
            this.panelDialogTools.Controls.Add(this.labelXNode);
            this.panelDialogTools.Controls.Add(this.bRemoveDialog);
            this.panelDialogTools.Controls.Add(this.bEditDialog);
            this.panelDialogTools.Controls.Add(this.bAddDialog);
            this.panelDialogTools.Name = "panelDialogTools";
            // 
            // lFindDialogID
            // 
            resources.ApplyResources(this.lFindDialogID, "lFindDialogID");
            this.lFindDialogID.Name = "lFindDialogID";
            // 
            // tbFindDialogID
            // 
            resources.ApplyResources(this.tbFindDialogID, "tbFindDialogID");
            this.tbFindDialogID.Name = "tbFindDialogID";
            this.tbFindDialogID.TextChanged += new System.EventHandler(this.tbFindDialogID_TextChanged);
            // 
            // btnClearRecycle
            // 
            resources.ApplyResources(this.btnClearRecycle, "btnClearRecycle");
            this.btnClearRecycle.Name = "btnClearRecycle";
            this.btnClearRecycle.UseVisualStyleBackColor = true;
            this.btnClearRecycle.Click += new System.EventHandler(this.btnClearRecycle_Click);
            // 
            // labelDrawingTip
            // 
            resources.ApplyResources(this.labelDrawingTip, "labelDrawingTip");
            this.labelDrawingTip.Name = "labelDrawingTip";
            // 
            // bCenterizeDialogShower
            // 
            resources.ApplyResources(this.bCenterizeDialogShower, "bCenterizeDialogShower");
            this.bCenterizeDialogShower.ImageList = this.imageList;
            this.bCenterizeDialogShower.Name = "bCenterizeDialogShower";
            this.bCenterizeDialogShower.UseVisualStyleBackColor = true;
            this.bCenterizeDialogShower.Click += new System.EventHandler(this.bCenterizeDialogShower_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "add.png");
            this.imageList.Images.SetKeyName(1, "Edit.png");
            this.imageList.Images.SetKeyName(2, "delete.png");
            this.imageList.Images.SetKeyName(3, "ArrowsBig.png");
            // 
            // labelYNode
            // 
            resources.ApplyResources(this.labelYNode, "labelYNode");
            this.labelYNode.Name = "labelYNode";
            // 
            // labelXNode
            // 
            resources.ApplyResources(this.labelXNode, "labelXNode");
            this.labelXNode.Name = "labelXNode";
            // 
            // bRemoveDialog
            // 
            resources.ApplyResources(this.bRemoveDialog, "bRemoveDialog");
            this.bRemoveDialog.ImageList = this.imageList;
            this.bRemoveDialog.Name = "bRemoveDialog";
            this.bRemoveDialog.UseVisualStyleBackColor = true;
            this.bRemoveDialog.Click += new System.EventHandler(this.bRemoveDialog_Click);
            // 
            // bEditDialog
            // 
            resources.ApplyResources(this.bEditDialog, "bEditDialog");
            this.bEditDialog.ImageList = this.imageList;
            this.bEditDialog.Name = "bEditDialog";
            this.bEditDialog.UseVisualStyleBackColor = true;
            this.bEditDialog.Click += new System.EventHandler(this.bEditDialog_Click);
            // 
            // bAddDialog
            // 
            resources.ApplyResources(this.bAddDialog, "bAddDialog");
            this.bAddDialog.ImageList = this.imageList;
            this.bAddDialog.Name = "bAddDialog";
            this.bAddDialog.UseVisualStyleBackColor = true;
            this.bAddDialog.Click += new System.EventHandler(this.bAddDialog_Click);
            // 
            // gbEmulator
            // 
            resources.ApplyResources(this.gbEmulator, "gbEmulator");
            this.gbEmulator.Controls.Add(this.splitDialogsEmulator);
            this.gbEmulator.Name = "gbEmulator";
            this.gbEmulator.TabStop = false;
            // 
            // splitDialogsEmulator
            // 
            resources.ApplyResources(this.splitDialogsEmulator, "splitDialogsEmulator");
            this.splitDialogsEmulator.Name = "splitDialogsEmulator";
            // 
            // splitDialogsEmulator.Panel2
            // 
            resources.ApplyResources(this.splitDialogsEmulator.Panel2, "splitDialogsEmulator.Panel2");
            this.splitDialogsEmulator.TabStop = false;
            // 
            // splitQuestsContainer
            // 
            this.splitQuestsContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitQuestsContainer, "splitQuestsContainer");
            this.splitQuestsContainer.Name = "splitQuestsContainer";
            // 
            // splitQuestsContainer.Panel1
            // 
            this.splitQuestsContainer.Panel1.Controls.Add(this.treeQuest);
            this.splitQuestsContainer.Panel1.Controls.Add(this.labelQuestTree);
            this.splitQuestsContainer.Panel1.Controls.Add(this.treeQuestBuffer);
            this.splitQuestsContainer.Panel1.Controls.Add(this.labelBuffer);
            // 
            // splitQuestsContainer.Panel2
            // 
            resources.ApplyResources(this.splitQuestsContainer.Panel2, "splitQuestsContainer.Panel2");
            this.splitQuestsContainer.TabStop = false;
            // 
            // treeQuest
            // 
            resources.ApplyResources(this.treeQuest, "treeQuest");
            this.treeQuest.Name = "treeQuest";
            this.treeQuest.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuest_AfterSelect);
            this.treeQuest.Click += new System.EventHandler(this.treeQuest_Click);
            this.treeQuest.DoubleClick += new System.EventHandler(this.treeQuestClicked);
            this.treeQuest.Leave += new System.EventHandler(this.treeQuest_Leave);
            // 
            // labelQuestTree
            // 
            resources.ApplyResources(this.labelQuestTree, "labelQuestTree");
            this.labelQuestTree.Name = "labelQuestTree";
            // 
            // treeQuestBuffer
            // 
            resources.ApplyResources(this.treeQuestBuffer, "treeQuestBuffer");
            this.treeQuestBuffer.Name = "treeQuestBuffer";
            this.treeQuestBuffer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuestBuffer_AfterSelect);
            this.treeQuestBuffer.Click += new System.EventHandler(this.treeQuestBuffer_Click);
            this.treeQuestBuffer.DoubleClick += new System.EventHandler(this.treeQuestBuffer_DoubleClick);
            // 
            // labelBuffer
            // 
            resources.ApplyResources(this.labelBuffer, "labelBuffer");
            this.labelBuffer.Name = "labelBuffer";
            // 
            // NPCBox
            // 
            this.NPCBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.NPCBox.DropDownWidth = 280;
            this.NPCBox.FormattingEnabled = true;
            resources.ApplyResources(this.NPCBox, "NPCBox");
            this.NPCBox.Name = "NPCBox";
            this.NPCBox.SelectedIndexChanged += new System.EventHandler(this.NPCBox_SelectedIndexChanged);
            this.NPCBox.TextChanged += new System.EventHandler(this.NPCBox_TextChanged);
            this.NPCBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NPCBox_KeyDown);
            // 
            // labelChosenNPC
            // 
            resources.ApplyResources(this.labelChosenNPC, "labelChosenNPC");
            this.labelChosenNPC.Name = "labelChosenNPC";
            // 
            // CentralDock
            // 
            this.CentralDock.Controls.Add(this.tabDialogs);
            this.CentralDock.Controls.Add(this.tabQuests);
            this.CentralDock.Controls.Add(this.tabFraction);
            this.CentralDock.Controls.Add(this.tabInfoNPC);
            this.CentralDock.Controls.Add(this.tabReview);
            this.CentralDock.Controls.Add(this.tabManage);
            this.CentralDock.Controls.Add(this.tabTranslate);
            this.CentralDock.Controls.Add(this.tabSearch);
            this.CentralDock.Controls.Add(this.tabKnowledge);
            resources.ApplyResources(this.CentralDock, "CentralDock");
            this.CentralDock.HotTrack = true;
            this.CentralDock.Name = "CentralDock";
            this.CentralDock.SelectedIndex = 0;
            this.CentralDock.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.CentralDock.TabStop = false;
            this.CentralDock.SelectedIndexChanged += new System.EventHandler(this.onSelectTab);
            // 
            // tabDialogs
            // 
            this.tabDialogs.Controls.Add(this.splitDialogs);
            resources.ApplyResources(this.tabDialogs, "tabDialogs");
            this.tabDialogs.Name = "tabDialogs";
            this.tabDialogs.UseVisualStyleBackColor = true;
            // 
            // tabQuests
            // 
            this.tabQuests.Controls.Add(this.splitQuestsContainer);
            this.tabQuests.Controls.Add(this.panelQuestTools);
            this.tabQuests.Controls.Add(this.panelSelectQuest);
            resources.ApplyResources(this.tabQuests, "tabQuests");
            this.tabQuests.Name = "tabQuests";
            this.tabQuests.UseVisualStyleBackColor = true;
            // 
            // panelQuestTools
            // 
            resources.ApplyResources(this.panelQuestTools, "panelQuestTools");
            this.panelQuestTools.Controls.Add(this.bClearBuffer);
            this.panelQuestTools.Controls.Add(this.bCutEvents);
            this.panelQuestTools.Controls.Add(this.bPasteEvents);
            this.panelQuestTools.Controls.Add(this.bCopyEvents);
            this.panelQuestTools.Controls.Add(this.bQuestDown);
            this.panelQuestTools.Controls.Add(this.bQuestUp);
            this.panelQuestTools.Controls.Add(this.bRemoveEvent);
            this.panelQuestTools.Controls.Add(this.bEditEvent);
            this.panelQuestTools.Controls.Add(this.bAddEvent);
            this.panelQuestTools.Name = "panelQuestTools";
            // 
            // bClearBuffer
            // 
            resources.ApplyResources(this.bClearBuffer, "bClearBuffer");
            this.bClearBuffer.Name = "bClearBuffer";
            this.bClearBuffer.UseVisualStyleBackColor = true;
            this.bClearBuffer.Click += new System.EventHandler(this.bClearBuffer_Click);
            // 
            // bCutEvents
            // 
            resources.ApplyResources(this.bCutEvents, "bCutEvents");
            this.bCutEvents.Name = "bCutEvents";
            this.bCutEvents.UseVisualStyleBackColor = true;
            this.bCutEvents.Click += new System.EventHandler(this.bCutEvents_Click);
            // 
            // bPasteEvents
            // 
            resources.ApplyResources(this.bPasteEvents, "bPasteEvents");
            this.bPasteEvents.Name = "bPasteEvents";
            this.bPasteEvents.UseVisualStyleBackColor = true;
            this.bPasteEvents.Click += new System.EventHandler(this.bPasteEvents_Click);
            // 
            // bCopyEvents
            // 
            resources.ApplyResources(this.bCopyEvents, "bCopyEvents");
            this.bCopyEvents.Name = "bCopyEvents";
            this.bCopyEvents.UseVisualStyleBackColor = true;
            this.bCopyEvents.Click += new System.EventHandler(this.bCopyEvents_Click);
            // 
            // bQuestDown
            // 
            resources.ApplyResources(this.bQuestDown, "bQuestDown");
            this.bQuestDown.Name = "bQuestDown";
            this.bQuestDown.UseVisualStyleBackColor = true;
            this.bQuestDown.Click += new System.EventHandler(this.bQuestDown_Click);
            // 
            // bQuestUp
            // 
            resources.ApplyResources(this.bQuestUp, "bQuestUp");
            this.bQuestUp.Name = "bQuestUp";
            this.bQuestUp.UseVisualStyleBackColor = true;
            this.bQuestUp.Click += new System.EventHandler(this.bQuestUp_Click);
            // 
            // bRemoveEvent
            // 
            resources.ApplyResources(this.bRemoveEvent, "bRemoveEvent");
            this.bRemoveEvent.ImageList = this.imageList;
            this.bRemoveEvent.Name = "bRemoveEvent";
            this.bRemoveEvent.UseVisualStyleBackColor = true;
            this.bRemoveEvent.Click += new System.EventHandler(this.bRemoveEvent_Click);
            // 
            // bEditEvent
            // 
            resources.ApplyResources(this.bEditEvent, "bEditEvent");
            this.bEditEvent.ImageList = this.imageList;
            this.bEditEvent.Name = "bEditEvent";
            this.bEditEvent.UseVisualStyleBackColor = true;
            this.bEditEvent.Click += new System.EventHandler(this.bEditEvent_Click);
            // 
            // bAddEvent
            // 
            resources.ApplyResources(this.bAddEvent, "bAddEvent");
            this.bAddEvent.ImageList = this.imageList;
            this.bAddEvent.Name = "bAddEvent";
            this.bAddEvent.UseMnemonic = false;
            this.bAddEvent.UseVisualStyleBackColor = false;
            this.bAddEvent.Click += new System.EventHandler(this.bAddEvent_Click);
            // 
            // panelSelectQuest
            // 
            this.panelSelectQuest.Controls.Add(this.bRemoveQuest);
            this.panelSelectQuest.Controls.Add(this.bAddQuest);
            this.panelSelectQuest.Controls.Add(this.QuestBox);
            this.panelSelectQuest.Controls.Add(this.labelChosenQuest);
            resources.ApplyResources(this.panelSelectQuest, "panelSelectQuest");
            this.panelSelectQuest.Name = "panelSelectQuest";
            // 
            // bRemoveQuest
            // 
            resources.ApplyResources(this.bRemoveQuest, "bRemoveQuest");
            this.bRemoveQuest.Name = "bRemoveQuest";
            this.bRemoveQuest.UseVisualStyleBackColor = true;
            this.bRemoveQuest.Click += new System.EventHandler(this.bRemoveQuest_Click);
            // 
            // bAddQuest
            // 
            resources.ApplyResources(this.bAddQuest, "bAddQuest");
            this.bAddQuest.Name = "bAddQuest";
            this.bAddQuest.UseVisualStyleBackColor = true;
            this.bAddQuest.Click += new System.EventHandler(this.bAddQuest_Click);
            // 
            // QuestBox
            // 
            this.QuestBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            resources.ApplyResources(this.QuestBox, "QuestBox");
            this.QuestBox.FormattingEnabled = true;
            this.QuestBox.Name = "QuestBox";
            this.QuestBox.SelectedIndexChanged += new System.EventHandler(this.QuestBox_SelectedIndexChanged);
            this.QuestBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuestBox_KeyDown);
            // 
            // labelChosenQuest
            // 
            resources.ApplyResources(this.labelChosenQuest, "labelChosenQuest");
            this.labelChosenQuest.Name = "labelChosenQuest";
            // 
            // tabFraction
            // 
            this.tabFraction.BackColor = System.Drawing.SystemColors.Control;
            this.tabFraction.Controls.Add(this.bRemoveFracDialog);
            this.tabFraction.Controls.Add(this.bEditFracDialog);
            this.tabFraction.Controls.Add(this.bAddFracDialog);
            this.tabFraction.Controls.Add(this.treeFractionDialogs);
            this.tabFraction.Controls.Add(this.fractionDialogShower);
            this.tabFraction.Controls.Add(this.label1);
            this.tabFraction.Controls.Add(this.fractionBox);
            resources.ApplyResources(this.tabFraction, "tabFraction");
            this.tabFraction.Name = "tabFraction";
            // 
            // bRemoveFracDialog
            // 
            resources.ApplyResources(this.bRemoveFracDialog, "bRemoveFracDialog");
            this.bRemoveFracDialog.ImageList = this.imageList;
            this.bRemoveFracDialog.Name = "bRemoveFracDialog";
            this.bRemoveFracDialog.UseVisualStyleBackColor = true;
            this.bRemoveFracDialog.Click += new System.EventHandler(this.bRemoveFracDialog_Click);
            // 
            // bEditFracDialog
            // 
            resources.ApplyResources(this.bEditFracDialog, "bEditFracDialog");
            this.bEditFracDialog.ImageList = this.imageList;
            this.bEditFracDialog.Name = "bEditFracDialog";
            this.bEditFracDialog.UseVisualStyleBackColor = true;
            this.bEditFracDialog.Click += new System.EventHandler(this.bEditDialog_Click);
            // 
            // bAddFracDialog
            // 
            resources.ApplyResources(this.bAddFracDialog, "bAddFracDialog");
            this.bAddFracDialog.ImageList = this.imageList;
            this.bAddFracDialog.Name = "bAddFracDialog";
            this.bAddFracDialog.UseVisualStyleBackColor = true;
            this.bAddFracDialog.Click += new System.EventHandler(this.bAddFracDialog_Click);
            // 
            // treeFractionDialogs
            // 
            resources.ApplyResources(this.treeFractionDialogs, "treeFractionDialogs");
            this.treeFractionDialogs.Name = "treeFractionDialogs";
            this.treeFractionDialogs.TabStop = false;
            // 
            // fractionDialogShower
            // 
            this.fractionDialogShower.AllowDrop = true;
            resources.ApplyResources(this.fractionDialogShower, "fractionDialogShower");
            this.fractionDialogShower.BackColor = System.Drawing.Color.White;
            this.fractionDialogShower.GridFitText = false;
            this.fractionDialogShower.Name = "fractionDialogShower";
            this.fractionDialogShower.RegionManagement = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // fractionBox
            // 
            this.fractionBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.fractionBox.DropDownWidth = 280;
            this.fractionBox.FormattingEnabled = true;
            resources.ApplyResources(this.fractionBox, "fractionBox");
            this.fractionBox.Name = "fractionBox";
            this.fractionBox.SelectedIndexChanged += new System.EventHandler(this.fractionBox_SelectedIndexChanged);
            // 
            // tabInfoNPC
            // 
            this.tabInfoNPC.Controls.Add(this.npcLinkShower);
            this.tabInfoNPC.Controls.Add(this.panelNpcLinkControls);
            resources.ApplyResources(this.tabInfoNPC, "tabInfoNPC");
            this.tabInfoNPC.Name = "tabInfoNPC";
            this.tabInfoNPC.UseVisualStyleBackColor = true;
            // 
            // npcLinkShower
            // 
            this.npcLinkShower.AllowDrop = true;
            this.npcLinkShower.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.npcLinkShower, "npcLinkShower");
            this.npcLinkShower.GridFitText = false;
            this.npcLinkShower.Name = "npcLinkShower";
            this.npcLinkShower.RegionManagement = true;
            // 
            // panelNpcLinkControls
            // 
            this.panelNpcLinkControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNpcLinkControls.Controls.Add(this.labelAdviceNpcLink);
            this.panelNpcLinkControls.Controls.Add(this.bNpcLinkExecute);
            resources.ApplyResources(this.panelNpcLinkControls, "panelNpcLinkControls");
            this.panelNpcLinkControls.Name = "panelNpcLinkControls";
            // 
            // labelAdviceNpcLink
            // 
            resources.ApplyResources(this.labelAdviceNpcLink, "labelAdviceNpcLink");
            this.labelAdviceNpcLink.Name = "labelAdviceNpcLink";
            // 
            // bNpcLinkExecute
            // 
            resources.ApplyResources(this.bNpcLinkExecute, "bNpcLinkExecute");
            this.bNpcLinkExecute.Name = "bNpcLinkExecute";
            this.bNpcLinkExecute.UseVisualStyleBackColor = true;
            this.bNpcLinkExecute.Click += new System.EventHandler(this.bNpcLinkExecute_Click);
            // 
            // tabReview
            // 
            this.tabReview.Controls.Add(this.dgvReview);
            this.tabReview.Controls.Add(this.panelReviewButtons);
            resources.ApplyResources(this.tabReview, "tabReview");
            this.tabReview.Name = "tabReview";
            this.tabReview.UseVisualStyleBackColor = true;
            // 
            // dgvReview
            // 
            this.dgvReview.AllowUserToAddRows = false;
            this.dgvReview.AllowUserToDeleteRows = false;
            this.dgvReview.AllowUserToOrderColumns = true;
            this.dgvReview.AllowUserToResizeRows = false;
            this.dgvReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNPCName,
            this.colDialogsNum,
            this.colQuestsNum,
            this.colLocation,
            this.colCoordinates,
            this.colRussianName});
            resources.ApplyResources(this.dgvReview, "dgvReview");
            this.dgvReview.Name = "dgvReview";
            // 
            // colNPCName
            // 
            resources.ApplyResources(this.colNPCName, "colNPCName");
            this.colNPCName.Name = "colNPCName";
            // 
            // colDialogsNum
            // 
            resources.ApplyResources(this.colDialogsNum, "colDialogsNum");
            this.colDialogsNum.Name = "colDialogsNum";
            // 
            // colQuestsNum
            // 
            resources.ApplyResources(this.colQuestsNum, "colQuestsNum");
            this.colQuestsNum.Name = "colQuestsNum";
            // 
            // colLocation
            // 
            resources.ApplyResources(this.colLocation, "colLocation");
            this.colLocation.Name = "colLocation";
            // 
            // colCoordinates
            // 
            resources.ApplyResources(this.colCoordinates, "colCoordinates");
            this.colCoordinates.Name = "colCoordinates";
            this.colCoordinates.ReadOnly = true;
            // 
            // colRussianName
            // 
            resources.ApplyResources(this.colRussianName, "colRussianName");
            this.colRussianName.Name = "colRussianName";
            // 
            // panelReviewButtons
            // 
            this.panelReviewButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReviewButtons.Controls.Add(this.groupBox1);
            this.panelReviewButtons.Controls.Add(this.gbNPCcheck);
            this.panelReviewButtons.Controls.Add(this.gbQuestCheck);
            resources.ApplyResources(this.panelReviewButtons, "panelReviewButtons");
            this.panelReviewButtons.Name = "panelReviewButtons";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbNPCList);
            this.groupBox1.Controls.Add(this.bFindNPCReputation);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbNPCList
            // 
            this.cbNPCList.FormattingEnabled = true;
            resources.ApplyResources(this.cbNPCList, "cbNPCList");
            this.cbNPCList.Name = "cbNPCList";
            // 
            // bFindNPCReputation
            // 
            resources.ApplyResources(this.bFindNPCReputation, "bFindNPCReputation");
            this.bFindNPCReputation.Name = "bFindNPCReputation";
            this.bFindNPCReputation.UseVisualStyleBackColor = true;
            this.bFindNPCReputation.Click += new System.EventHandler(this.bFindNPCReputation_Click);
            // 
            // gbNPCcheck
            // 
            this.gbNPCcheck.Controls.Add(this.cbOnlyOnLocation);
            this.gbNPCcheck.Controls.Add(this.cbLocation);
            this.gbNPCcheck.Controls.Add(this.numQuests);
            this.gbNPCcheck.Controls.Add(this.labelLessThan2);
            this.gbNPCcheck.Controls.Add(this.cbNumQuests);
            this.gbNPCcheck.Controls.Add(this.numDialogs);
            this.gbNPCcheck.Controls.Add(this.labelLessThan1);
            this.gbNPCcheck.Controls.Add(this.cbNumDialogs);
            this.gbNPCcheck.Controls.Add(this.bFindNPC);
            resources.ApplyResources(this.gbNPCcheck, "gbNPCcheck");
            this.gbNPCcheck.Name = "gbNPCcheck";
            this.gbNPCcheck.TabStop = false;
            // 
            // cbOnlyOnLocation
            // 
            resources.ApplyResources(this.cbOnlyOnLocation, "cbOnlyOnLocation");
            this.cbOnlyOnLocation.Name = "cbOnlyOnLocation";
            this.cbOnlyOnLocation.UseVisualStyleBackColor = true;
            // 
            // cbLocation
            // 
            this.cbLocation.FormattingEnabled = true;
            resources.ApplyResources(this.cbLocation, "cbLocation");
            this.cbLocation.Name = "cbLocation";
            // 
            // numQuests
            // 
            resources.ApplyResources(this.numQuests, "numQuests");
            this.numQuests.Name = "numQuests";
            this.numQuests.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // labelLessThan2
            // 
            resources.ApplyResources(this.labelLessThan2, "labelLessThan2");
            this.labelLessThan2.Name = "labelLessThan2";
            // 
            // cbNumQuests
            // 
            resources.ApplyResources(this.cbNumQuests, "cbNumQuests");
            this.cbNumQuests.Name = "cbNumQuests";
            this.cbNumQuests.UseVisualStyleBackColor = true;
            // 
            // numDialogs
            // 
            resources.ApplyResources(this.numDialogs, "numDialogs");
            this.numDialogs.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numDialogs.Name = "numDialogs";
            this.numDialogs.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelLessThan1
            // 
            resources.ApplyResources(this.labelLessThan1, "labelLessThan1");
            this.labelLessThan1.Name = "labelLessThan1";
            // 
            // cbNumDialogs
            // 
            resources.ApplyResources(this.cbNumDialogs, "cbNumDialogs");
            this.cbNumDialogs.Name = "cbNumDialogs";
            this.cbNumDialogs.UseVisualStyleBackColor = true;
            // 
            // bFindNPC
            // 
            resources.ApplyResources(this.bFindNPC, "bFindNPC");
            this.bFindNPC.Name = "bFindNPC";
            this.bFindNPC.UseVisualStyleBackColor = true;
            this.bFindNPC.Click += new System.EventHandler(this.bFindNPC_Click);
            // 
            // gbQuestCheck
            // 
            this.gbQuestCheck.Controls.Add(this.labelItemTarget);
            this.gbQuestCheck.Controls.Add(this.cbItemTarget);
            this.gbQuestCheck.Controls.Add(this.labelItemReward);
            this.gbQuestCheck.Controls.Add(this.cbItemReward);
            this.gbQuestCheck.Controls.Add(this.bFindQuest);
            resources.ApplyResources(this.gbQuestCheck, "gbQuestCheck");
            this.gbQuestCheck.Name = "gbQuestCheck";
            this.gbQuestCheck.TabStop = false;
            // 
            // labelItemTarget
            // 
            resources.ApplyResources(this.labelItemTarget, "labelItemTarget");
            this.labelItemTarget.Name = "labelItemTarget";
            // 
            // cbItemTarget
            // 
            this.cbItemTarget.FormattingEnabled = true;
            resources.ApplyResources(this.cbItemTarget, "cbItemTarget");
            this.cbItemTarget.Name = "cbItemTarget";
            // 
            // labelItemReward
            // 
            resources.ApplyResources(this.labelItemReward, "labelItemReward");
            this.labelItemReward.Name = "labelItemReward";
            // 
            // cbItemReward
            // 
            this.cbItemReward.FormattingEnabled = true;
            resources.ApplyResources(this.cbItemReward, "cbItemReward");
            this.cbItemReward.Name = "cbItemReward";
            // 
            // bFindQuest
            // 
            resources.ApplyResources(this.bFindQuest, "bFindQuest");
            this.bFindQuest.Name = "bFindQuest";
            this.bFindQuest.UseVisualStyleBackColor = true;
            this.bFindQuest.Click += new System.EventHandler(this.bFindQuest_Click);
            // 
            // tabManage
            // 
            this.tabManage.Controls.Add(this.dgvManage);
            this.tabManage.Controls.Add(this.panel2);
            resources.ApplyResources(this.tabManage, "tabManage");
            this.tabManage.Name = "tabManage";
            this.tabManage.UseVisualStyleBackColor = true;
            // 
            // dgvManage
            // 
            this.dgvManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvManage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.subevents,
            this.title,
            this.Description,
            this.npcName,
            this.subNPC,
            this.dialogID,
            this.rewardBattle,
            this.rewardSurvive,
            this.rewardSupport,
            this.rewardCredits,
            this.rewardItems,
            this.repeat,
            this.repeatPeriod,
            this.Level,
            this.author,
            this.Legend,
            this.worked});
            resources.ApplyResources(this.dgvManage, "dgvManage");
            this.dgvManage.Name = "dgvManage";
            // 
            // id
            // 
            resources.ApplyResources(this.id, "id");
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // subevents
            // 
            resources.ApplyResources(this.subevents, "subevents");
            this.subevents.Name = "subevents";
            this.subevents.ReadOnly = true;
            // 
            // title
            // 
            resources.ApplyResources(this.title, "title");
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // Description
            // 
            resources.ApplyResources(this.Description, "Description");
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // npcName
            // 
            resources.ApplyResources(this.npcName, "npcName");
            this.npcName.Name = "npcName";
            this.npcName.ReadOnly = true;
            // 
            // subNPC
            // 
            resources.ApplyResources(this.subNPC, "subNPC");
            this.subNPC.Name = "subNPC";
            this.subNPC.ReadOnly = true;
            // 
            // dialogID
            // 
            resources.ApplyResources(this.dialogID, "dialogID");
            this.dialogID.Name = "dialogID";
            this.dialogID.ReadOnly = true;
            // 
            // rewardBattle
            // 
            resources.ApplyResources(this.rewardBattle, "rewardBattle");
            this.rewardBattle.Name = "rewardBattle";
            this.rewardBattle.ReadOnly = true;
            // 
            // rewardSurvive
            // 
            resources.ApplyResources(this.rewardSurvive, "rewardSurvive");
            this.rewardSurvive.Name = "rewardSurvive";
            this.rewardSurvive.ReadOnly = true;
            // 
            // rewardSupport
            // 
            resources.ApplyResources(this.rewardSupport, "rewardSupport");
            this.rewardSupport.Name = "rewardSupport";
            this.rewardSupport.ReadOnly = true;
            // 
            // rewardCredits
            // 
            resources.ApplyResources(this.rewardCredits, "rewardCredits");
            this.rewardCredits.Name = "rewardCredits";
            this.rewardCredits.ReadOnly = true;
            // 
            // rewardItems
            // 
            resources.ApplyResources(this.rewardItems, "rewardItems");
            this.rewardItems.Name = "rewardItems";
            this.rewardItems.ReadOnly = true;
            // 
            // repeat
            // 
            resources.ApplyResources(this.repeat, "repeat");
            this.repeat.Name = "repeat";
            this.repeat.ReadOnly = true;
            // 
            // repeatPeriod
            // 
            resources.ApplyResources(this.repeatPeriod, "repeatPeriod");
            this.repeatPeriod.Name = "repeatPeriod";
            this.repeatPeriod.ReadOnly = true;
            // 
            // Level
            // 
            resources.ApplyResources(this.Level, "Level");
            this.Level.Name = "Level";
            // 
            // author
            // 
            resources.ApplyResources(this.author, "author");
            this.author.Name = "author";
            // 
            // Legend
            // 
            resources.ApplyResources(this.Legend, "Legend");
            this.Legend.Name = "Legend";
            // 
            // worked
            // 
            resources.ApplyResources(this.worked, "worked");
            this.worked.Name = "worked";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bSaveManage);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // bSaveManage
            // 
            resources.ApplyResources(this.bSaveManage, "bSaveManage");
            this.bSaveManage.Name = "bSaveManage";
            this.bSaveManage.UseVisualStyleBackColor = true;
            this.bSaveManage.Click += new System.EventHandler(this.bSaveManage_Click);
            // 
            // tabTranslate
            // 
            this.tabTranslate.Controls.Add(this.dgvLocaleDiff);
            this.tabTranslate.Controls.Add(this.panelDiffLocale);
            resources.ApplyResources(this.tabTranslate, "tabTranslate");
            this.tabTranslate.Name = "tabTranslate";
            this.tabTranslate.UseVisualStyleBackColor = true;
            // 
            // dgvLocaleDiff
            // 
            this.dgvLocaleDiff.AllowUserToAddRows = false;
            this.dgvLocaleDiff.AllowUserToDeleteRows = false;
            this.dgvLocaleDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocaleDiff.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.npc_name,
            this.identif,
            this.cur_ver,
            this.new_ver,
            this.ColumnLocation,
            this.RusText1,
            this.EngText1});
            resources.ApplyResources(this.dgvLocaleDiff, "dgvLocaleDiff");
            this.dgvLocaleDiff.Name = "dgvLocaleDiff";
            this.dgvLocaleDiff.ReadOnly = true;
            this.dgvLocaleDiff.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocaleDiff_CellDoubleClick);
            // 
            // type
            // 
            resources.ApplyResources(this.type, "type");
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // npc_name
            // 
            resources.ApplyResources(this.npc_name, "npc_name");
            this.npc_name.Name = "npc_name";
            this.npc_name.ReadOnly = true;
            // 
            // identif
            // 
            resources.ApplyResources(this.identif, "identif");
            this.identif.Name = "identif";
            this.identif.ReadOnly = true;
            // 
            // cur_ver
            // 
            resources.ApplyResources(this.cur_ver, "cur_ver");
            this.cur_ver.Name = "cur_ver";
            this.cur_ver.ReadOnly = true;
            // 
            // new_ver
            // 
            resources.ApplyResources(this.new_ver, "new_ver");
            this.new_ver.Name = "new_ver";
            this.new_ver.ReadOnly = true;
            // 
            // ColumnLocation
            // 
            resources.ApplyResources(this.ColumnLocation, "ColumnLocation");
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.ReadOnly = true;
            // 
            // RusText1
            // 
            resources.ApplyResources(this.RusText1, "RusText1");
            this.RusText1.Name = "RusText1";
            this.RusText1.ReadOnly = true;
            // 
            // EngText1
            // 
            resources.ApplyResources(this.EngText1, "EngText1");
            this.EngText1.Name = "EngText1";
            this.EngText1.ReadOnly = true;
            // 
            // panelDiffLocale
            // 
            this.panelDiffLocale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDiffLocale.Controls.Add(this.labelLocalizeOuput);
            this.panelDiffLocale.Controls.Add(this.cbActualFinder);
            this.panelDiffLocale.Controls.Add(this.cbOutdatedFinder);
            this.panelDiffLocale.Controls.Add(this.bFindQuestDifference);
            this.panelDiffLocale.Controls.Add(this.bFindDialogDifference);
            resources.ApplyResources(this.panelDiffLocale, "panelDiffLocale");
            this.panelDiffLocale.Name = "panelDiffLocale";
            // 
            // labelLocalizeOuput
            // 
            resources.ApplyResources(this.labelLocalizeOuput, "labelLocalizeOuput");
            this.labelLocalizeOuput.Name = "labelLocalizeOuput";
            // 
            // cbActualFinder
            // 
            resources.ApplyResources(this.cbActualFinder, "cbActualFinder");
            this.cbActualFinder.Name = "cbActualFinder";
            this.cbActualFinder.UseVisualStyleBackColor = true;
            // 
            // cbOutdatedFinder
            // 
            resources.ApplyResources(this.cbOutdatedFinder, "cbOutdatedFinder");
            this.cbOutdatedFinder.Checked = true;
            this.cbOutdatedFinder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOutdatedFinder.Name = "cbOutdatedFinder";
            this.cbOutdatedFinder.UseVisualStyleBackColor = true;
            // 
            // bFindQuestDifference
            // 
            resources.ApplyResources(this.bFindQuestDifference, "bFindQuestDifference");
            this.bFindQuestDifference.Name = "bFindQuestDifference";
            this.bFindQuestDifference.UseVisualStyleBackColor = true;
            this.bFindQuestDifference.Click += new System.EventHandler(this.bFindQuestDifference_Click);
            // 
            // bFindDialogDifference
            // 
            resources.ApplyResources(this.bFindDialogDifference, "bFindDialogDifference");
            this.bFindDialogDifference.Name = "bFindDialogDifference";
            this.bFindDialogDifference.UseVisualStyleBackColor = true;
            this.bFindDialogDifference.Click += new System.EventHandler(this.bFindDialogDifference_Click);
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.dgvSearch);
            this.tabSearch.Controls.Add(this.panelSearchTools);
            resources.ApplyResources(this.tabSearch, "tabSearch");
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // dgvSearch
            // 
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colNPC,
            this.colID,
            this.colText,
            this.colEngText});
            resources.ApplyResources(this.dgvSearch, "dgvSearch");
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellDoubleClick);
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            this.colType.Name = "colType";
            // 
            // colNPC
            // 
            resources.ApplyResources(this.colNPC, "colNPC");
            this.colNPC.Name = "colNPC";
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            this.colID.Name = "colID";
            // 
            // colText
            // 
            resources.ApplyResources(this.colText, "colText");
            this.colText.Name = "colText";
            // 
            // colEngText
            // 
            resources.ApplyResources(this.colEngText, "colEngText");
            this.colEngText.Name = "colEngText";
            // 
            // panelSearchTools
            // 
            this.panelSearchTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchTools.Controls.Add(this.cbIgnoreCase);
            this.panelSearchTools.Controls.Add(this.labelSearchResult);
            this.panelSearchTools.Controls.Add(this.labelPhraseToSearch);
            this.panelSearchTools.Controls.Add(this.tbPhraseToSearch);
            this.panelSearchTools.Controls.Add(this.bStartSearch);
            resources.ApplyResources(this.panelSearchTools, "panelSearchTools");
            this.panelSearchTools.Name = "panelSearchTools";
            // 
            // cbIgnoreCase
            // 
            resources.ApplyResources(this.cbIgnoreCase, "cbIgnoreCase");
            this.cbIgnoreCase.Checked = true;
            this.cbIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.cbIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // labelSearchResult
            // 
            resources.ApplyResources(this.labelSearchResult, "labelSearchResult");
            this.labelSearchResult.Name = "labelSearchResult";
            // 
            // labelPhraseToSearch
            // 
            resources.ApplyResources(this.labelPhraseToSearch, "labelPhraseToSearch");
            this.labelPhraseToSearch.Name = "labelPhraseToSearch";
            // 
            // tbPhraseToSearch
            // 
            resources.ApplyResources(this.tbPhraseToSearch, "tbPhraseToSearch");
            this.tbPhraseToSearch.Name = "tbPhraseToSearch";
            // 
            // bStartSearch
            // 
            resources.ApplyResources(this.bStartSearch, "bStartSearch");
            this.bStartSearch.Name = "bStartSearch";
            this.bStartSearch.UseVisualStyleBackColor = true;
            this.bStartSearch.Click += new System.EventHandler(this.bStartSearch_Click);
            // 
            // tabKnowledge
            // 
            this.tabKnowledge.Controls.Add(this.treeKnowladge);
            this.tabKnowledge.Controls.Add(this.cbKnowlegeTypeValue);
            this.tabKnowledge.Controls.Add(this.label4);
            this.tabKnowledge.Controls.Add(this.label3);
            this.tabKnowledge.Controls.Add(this.cbKnowledgeCategory);
            resources.ApplyResources(this.tabKnowledge, "tabKnowledge");
            this.tabKnowledge.Name = "tabKnowledge";
            this.tabKnowledge.UseVisualStyleBackColor = true;
            // 
            // cbKnowlegeTypeValue
            // 
            this.cbKnowlegeTypeValue.FormattingEnabled = true;
            resources.ApplyResources(this.cbKnowlegeTypeValue, "cbKnowlegeTypeValue");
            this.cbKnowlegeTypeValue.Name = "cbKnowlegeTypeValue";
            this.cbKnowlegeTypeValue.SelectedIndexChanged += new System.EventHandler(this.cbKnowlegeTypeValue_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbKnowledgeCategory
            // 
            this.cbKnowledgeCategory.FormattingEnabled = true;
            resources.ApplyResources(this.cbKnowledgeCategory, "cbKnowledgeCategory");
            this.cbKnowledgeCategory.Name = "cbKnowledgeCategory";
            this.cbKnowledgeCategory.SelectedIndexChanged += new System.EventHandler(this.cbKnowledgeCategory_SelectedIndexChanged);
            // 
            // panelSelectNPC
            // 
            this.panelSelectNPC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSelectNPC.Controls.Add(this.btnFilterNPC);
            this.panelSelectNPC.Controls.Add(this.btnNextNPC);
            this.panelSelectNPC.Controls.Add(this.btnBackNPC);
            this.panelSelectNPC.Controls.Add(this.bDelNPC);
            this.panelSelectNPC.Controls.Add(this.bAddNPC);
            this.panelSelectNPC.Controls.Add(this.labelChosenNPC);
            this.panelSelectNPC.Controls.Add(this.NPCBox);
            resources.ApplyResources(this.panelSelectNPC, "panelSelectNPC");
            this.panelSelectNPC.Name = "panelSelectNPC";
            // 
            // btnFilterNPC
            // 
            this.btnFilterNPC.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.filter_24;
            resources.ApplyResources(this.btnFilterNPC, "btnFilterNPC");
            this.btnFilterNPC.Name = "btnFilterNPC";
            this.toolTipDialogs.SetToolTip(this.btnFilterNPC, resources.GetString("btnFilterNPC.ToolTip"));
            this.btnFilterNPC.UseVisualStyleBackColor = true;
            this.btnFilterNPC.Click += new System.EventHandler(this.btnFilterNPC_Click);
            // 
            // btnNextNPC
            // 
            resources.ApplyResources(this.btnNextNPC, "btnNextNPC");
            this.btnNextNPC.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.next_24;
            this.btnNextNPC.Name = "btnNextNPC";
            this.toolTipDialogs.SetToolTip(this.btnNextNPC, resources.GetString("btnNextNPC.ToolTip"));
            this.btnNextNPC.UseVisualStyleBackColor = true;
            this.btnNextNPC.Click += new System.EventHandler(this.btnNextNPC_Click);
            // 
            // btnBackNPC
            // 
            resources.ApplyResources(this.btnBackNPC, "btnBackNPC");
            this.btnBackNPC.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.back_24;
            this.btnBackNPC.Name = "btnBackNPC";
            this.toolTipDialogs.SetToolTip(this.btnBackNPC, resources.GetString("btnBackNPC.ToolTip"));
            this.btnBackNPC.UseVisualStyleBackColor = true;
            this.btnBackNPC.Click += new System.EventHandler(this.btnBackNPC_Click);
            // 
            // bDelNPC
            // 
            resources.ApplyResources(this.bDelNPC, "bDelNPC");
            this.bDelNPC.Name = "bDelNPC";
            this.bDelNPC.UseVisualStyleBackColor = true;
            this.bDelNPC.Click += new System.EventHandler(this.bDelNPC_Click);
            // 
            // bAddNPC
            // 
            resources.ApplyResources(this.bAddNPC, "bAddNPC");
            this.bAddNPC.Name = "bAddNPC";
            this.bAddNPC.UseVisualStyleBackColor = true;
            this.bAddNPC.Click += new System.EventHandler(this.bAddNPC_Click);
            // 
            // menuMainControl
            // 
            this.menuMainControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMain,
            this.menuSaveAll,
            this.menuExplorer,
            this.обновленияToolStripMenuItem,
            this.данныеToolStripMenuItem});
            resources.ApplyResources(this.menuMainControl, "menuMainControl");
            this.menuMainControl.Name = "menuMainControl";
            this.menuMainControl.TabStop = true;
            // 
            // menuMain
            // 
            this.menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuSynchronize,
            this.menuStatistics,
            this.changeLanguageToolStripMenuItem,
            this.menuExit});
            this.menuMain.Name = "menuMain";
            resources.ApplyResources(this.menuMain, "menuMain");
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            resources.ApplyResources(this.menuSettings, "menuSettings");
            this.menuSettings.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // menuSynchronize
            // 
            this.menuSynchronize.Name = "menuSynchronize";
            resources.ApplyResources(this.menuSynchronize, "menuSynchronize");
            this.menuSynchronize.Click += new System.EventHandler(this.SynchroToolStripMenuItem_Click);
            // 
            // menuStatistics
            // 
            this.menuStatistics.Name = "menuStatistics";
            resources.ApplyResources(this.menuStatistics, "menuStatistics");
            this.menuStatistics.Click += new System.EventHandler(this.StatisticsToolStripMenuItem_Click);
            // 
            // changeLanguageToolStripMenuItem
            // 
            this.changeLanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.русскийToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.changeLanguageToolStripMenuItem.Name = "changeLanguageToolStripMenuItem";
            resources.ApplyResources(this.changeLanguageToolStripMenuItem, "changeLanguageToolStripMenuItem");
            // 
            // русскийToolStripMenuItem
            // 
            this.русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            resources.ApplyResources(this.русскийToolStripMenuItem, "русскийToolStripMenuItem");
            this.русскийToolStripMenuItem.Click += new System.EventHandler(this.русскийToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            resources.ApplyResources(this.menuExit, "menuExit");
            this.menuExit.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // menuSaveAll
            // 
            this.menuSaveAll.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.SaveDisk;
            this.menuSaveAll.Name = "menuSaveAll";
            resources.ApplyResources(this.menuSaveAll, "menuSaveAll");
            this.menuSaveAll.Click += new System.EventHandler(this.SaveAllToolStripMenuItem_Click);
            // 
            // menuExplorer
            // 
            this.menuExplorer.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.Explorer;
            this.menuExplorer.Name = "menuExplorer";
            resources.ApplyResources(this.menuExplorer, "menuExplorer");
            this.menuExplorer.Click += new System.EventHandler(this.ExplorerToolStripMenuItem_Click);
            // 
            // обновленияToolStripMenuItem
            // 
            this.обновленияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.парсерыToolStripMenuItem,
            this.проверкаОшибокToolStripMenuItem});
            this.обновленияToolStripMenuItem.Name = "обновленияToolStripMenuItem";
            resources.ApplyResources(this.обновленияToolStripMenuItem, "обновленияToolStripMenuItem");
            // 
            // парсерыToolStripMenuItem
            // 
            this.парсерыToolStripMenuItem.Name = "парсерыToolStripMenuItem";
            resources.ApplyResources(this.парсерыToolStripMenuItem, "парсерыToolStripMenuItem");
            this.парсерыToolStripMenuItem.Click += new System.EventHandler(this.парсерыToolStripMenuItem_Click);
            // 
            // проверкаОшибокToolStripMenuItem
            // 
            this.проверкаОшибокToolStripMenuItem.Name = "проверкаОшибокToolStripMenuItem";
            resources.ApplyResources(this.проверкаОшибокToolStripMenuItem, "проверкаОшибокToolStripMenuItem");
            this.проверкаОшибокToolStripMenuItem.Click += new System.EventHandler(this.проверкаОшибокToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.собратьЭдиторДляПередачиToolStripMenuItem,
            this.вытащитьНепереведённыеТекстыToolStripMenuItem,
            this.загрузитьПереводToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            resources.ApplyResources(this.данныеToolStripMenuItem, "данныеToolStripMenuItem");
            // 
            // собратьЭдиторДляПередачиToolStripMenuItem
            // 
            this.собратьЭдиторДляПередачиToolStripMenuItem.Name = "собратьЭдиторДляПередачиToolStripMenuItem";
            resources.ApplyResources(this.собратьЭдиторДляПередачиToolStripMenuItem, "собратьЭдиторДляПередачиToolStripMenuItem");
            this.собратьЭдиторДляПередачиToolStripMenuItem.Click += new System.EventHandler(this.собратьЭдиторДляПередачиToolStripMenuItem_Click);
            // 
            // вытащитьНепереведённыеТекстыToolStripMenuItem
            // 
            this.вытащитьНепереведённыеТекстыToolStripMenuItem.Name = "вытащитьНепереведённыеТекстыToolStripMenuItem";
            resources.ApplyResources(this.вытащитьНепереведённыеТекстыToolStripMenuItem, "вытащитьНепереведённыеТекстыToolStripMenuItem");
            this.вытащитьНепереведённыеТекстыToolStripMenuItem.Click += new System.EventHandler(this.вытащитьНепереведённыеТекстыToolStripMenuItem_Click);
            // 
            // загрузитьПереводToolStripMenuItem
            // 
            this.загрузитьПереводToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.диалоговToolStripMenuItem,
            this.квестовToolStripMenuItem});
            this.загрузитьПереводToolStripMenuItem.Name = "загрузитьПереводToolStripMenuItem";
            resources.ApplyResources(this.загрузитьПереводToolStripMenuItem, "загрузитьПереводToolStripMenuItem");
            // 
            // диалоговToolStripMenuItem
            // 
            this.диалоговToolStripMenuItem.Name = "диалоговToolStripMenuItem";
            resources.ApplyResources(this.диалоговToolStripMenuItem, "диалоговToolStripMenuItem");
            this.диалоговToolStripMenuItem.Click += new System.EventHandler(this.диалоговToolStripMenuItem_Click);
            // 
            // квестовToolStripMenuItem
            // 
            this.квестовToolStripMenuItem.Name = "квестовToolStripMenuItem";
            resources.ApplyResources(this.квестовToolStripMenuItem, "квестовToolStripMenuItem");
            this.квестовToolStripMenuItem.Click += new System.EventHandler(this.квестовToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            this.statusStrip.Stretch = false;
            // 
            // treeKnowladge
            // 
            resources.ApplyResources(this.treeKnowladge, "treeKnowladge");
            this.treeKnowladge.Name = "treeKnowladge";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CentralDock);
            this.Controls.Add(this.panelSelectNPC);
            this.Controls.Add(this.menuMainControl);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitDialogs.Panel1.ResumeLayout(false);
            this.splitDialogs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogs)).EndInit();
            this.splitDialogs.ResumeLayout(false);
            this.gbDialogsEditor.ResumeLayout(false);
            this.gbDialogsEditor.PerformLayout();
            this.splitDialogsTreeAndCanvas.Panel1.ResumeLayout(false);
            this.splitDialogsTreeAndCanvas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogsTreeAndCanvas)).EndInit();
            this.splitDialogsTreeAndCanvas.ResumeLayout(false);
            this.panelDialogTools.ResumeLayout(false);
            this.panelDialogTools.PerformLayout();
            this.gbEmulator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDialogsEmulator)).EndInit();
            this.splitDialogsEmulator.ResumeLayout(false);
            this.splitQuestsContainer.Panel1.ResumeLayout(false);
            this.splitQuestsContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQuestsContainer)).EndInit();
            this.splitQuestsContainer.ResumeLayout(false);
            this.CentralDock.ResumeLayout(false);
            this.tabDialogs.ResumeLayout(false);
            this.tabQuests.ResumeLayout(false);
            this.tabQuests.PerformLayout();
            this.panelQuestTools.ResumeLayout(false);
            this.panelSelectQuest.ResumeLayout(false);
            this.panelSelectQuest.PerformLayout();
            this.tabFraction.ResumeLayout(false);
            this.tabFraction.PerformLayout();
            this.tabInfoNPC.ResumeLayout(false);
            this.panelNpcLinkControls.ResumeLayout(false);
            this.panelNpcLinkControls.PerformLayout();
            this.tabReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReview)).EndInit();
            this.panelReviewButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbNPCcheck.ResumeLayout(false);
            this.gbNPCcheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDialogs)).EndInit();
            this.gbQuestCheck.ResumeLayout(false);
            this.gbQuestCheck.PerformLayout();
            this.tabManage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvManage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabTranslate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocaleDiff)).EndInit();
            this.panelDiffLocale.ResumeLayout(false);
            this.panelDiffLocale.PerformLayout();
            this.tabSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.panelSearchTools.ResumeLayout(false);
            this.panelSearchTools.PerformLayout();
            this.tabKnowledge.ResumeLayout(false);
            this.tabKnowledge.PerformLayout();
            this.panelSelectNPC.ResumeLayout(false);
            this.panelSelectNPC.PerformLayout();
            this.menuMainControl.ResumeLayout(false);
            this.menuMainControl.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox NPCBox;
        private System.Windows.Forms.Label labelChosenNPC;
        private System.Windows.Forms.TabPage tabDialogs;
        private System.Windows.Forms.TabPage tabQuests;
        public PCanvas DialogShower;
        private System.Windows.Forms.SplitContainer splitDialogs;
        private System.Windows.Forms.GroupBox gbDialogsEditor;
        private System.Windows.Forms.Panel panelSelectNPC;
        private System.Windows.Forms.Panel panelDialogTools;
        private System.Windows.Forms.SplitContainer splitDialogsTreeAndCanvas;
        public System.Windows.Forms.TreeView treeDialogs;
        private System.Windows.Forms.Button bAddDialog;
        private System.Windows.Forms.Button bEditDialog;
        private System.Windows.Forms.Button bRemoveDialog;
        private System.Windows.Forms.GroupBox gbEmulator;
        private System.Windows.Forms.SplitContainer splitDialogsEmulator;
        private System.Windows.Forms.SplitContainer splitQuestsContainer;
        private System.Windows.Forms.TreeView treeQuest;
        private System.Windows.Forms.Button bDelNPC;
        private System.Windows.Forms.Button bAddNPC;
        private System.Windows.Forms.MenuStrip menuMainControl;
        private System.Windows.Forms.ToolStripMenuItem menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.TabPage tabInfoNPC;
        private PCanvas npcLinkShower;
        private System.Windows.Forms.TabPage tabManage;
        private System.Windows.Forms.DataGridView dgvManage;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bSaveManage;
        private System.Windows.Forms.TreeView treeQuestBuffer;
        private System.Windows.Forms.Label labelQuestTree;
        private System.Windows.Forms.Label labelBuffer;
        private System.Windows.Forms.TabPage tabTranslate;
        private System.Windows.Forms.Panel panelDiffLocale;
        private System.Windows.Forms.Button bFindDialogDifference;
        private System.Windows.Forms.DataGridView dgvLocaleDiff;
        private System.Windows.Forms.Button bFindQuestDifference;
        private System.Windows.Forms.CheckBox cbActualFinder;
        private System.Windows.Forms.CheckBox cbOutdatedFinder;
        private System.Windows.Forms.Label labelLocalizeOuput;
        private System.Windows.Forms.TabControl CentralDock;
        private System.Windows.Forms.Label labelYNode;
        private System.Windows.Forms.Label labelXNode;
        private System.Windows.Forms.Button bCenterizeDialogShower;
        private System.Windows.Forms.TabPage tabReview;
        private System.Windows.Forms.Panel panelReviewButtons;
        private System.Windows.Forms.GroupBox gbNPCcheck;
        private System.Windows.Forms.NumericUpDown numDialogs;
        private System.Windows.Forms.Label labelLessThan1;
        private System.Windows.Forms.CheckBox cbNumDialogs;
        private System.Windows.Forms.Button bFindNPC;
        private System.Windows.Forms.DataGridView dgvReview;
        private System.Windows.Forms.NumericUpDown numQuests;
        private System.Windows.Forms.Label labelLessThan2;
        private System.Windows.Forms.CheckBox cbNumQuests;
        private System.Windows.Forms.Panel panelNpcLinkControls;
        private System.Windows.Forms.Button bNpcLinkExecute;
        private System.Windows.Forms.Label labelAdviceNpcLink;
        private System.Windows.Forms.GroupBox gbQuestCheck;
        private System.Windows.Forms.Button bFindQuest;
        private System.Windows.Forms.Panel panelQuestTools;
        private System.Windows.Forms.Button bPasteEvents;
        private System.Windows.Forms.Button bCopyEvents;
        private System.Windows.Forms.Button bQuestDown;
        private System.Windows.Forms.Button bQuestUp;
        private System.Windows.Forms.Button bRemoveEvent;
        private System.Windows.Forms.Button bEditEvent;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.CheckBox cbOnlyOnLocation;
        private System.Windows.Forms.Button bCutEvents;
        private System.Windows.Forms.Button bClearBuffer;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.Panel panelSearchTools;
        private System.Windows.Forms.Button bStartSearch;
        private System.Windows.Forms.Label labelPhraseToSearch;
        private System.Windows.Forms.TextBox tbPhraseToSearch;
        private System.Windows.Forms.Label labelSearchResult;
        private System.Windows.Forms.DataGridView dgvSearch;
        private System.Windows.Forms.CheckBox cbIgnoreCase;
        private System.Windows.Forms.ToolStripMenuItem menuExplorer;
        private System.Windows.Forms.ToolStripMenuItem menuStatistics;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAll;
        private System.Windows.Forms.Panel panelSelectQuest;
        private System.Windows.Forms.Button bRemoveQuest;
        private System.Windows.Forms.Button bAddQuest;
        private System.Windows.Forms.ComboBox QuestBox;
        private System.Windows.Forms.Label labelChosenQuest;
        private System.Windows.Forms.Button bAddEvent;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem menuSynchronize;
        private System.Windows.Forms.Label labelItemReward;
        private System.Windows.Forms.ComboBox cbItemReward;
        private System.Windows.Forms.Label labelDrawingTip;
        private System.Windows.Forms.ToolTip toolTipDialogs;
        private System.Windows.Forms.ComboBox cbItemTarget;
        private System.Windows.Forms.Label labelItemTarget;
        private System.Windows.Forms.ToolStripMenuItem обновленияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem парсерыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверкаОшибокToolStripMenuItem;
        private System.Windows.Forms.Button btnBackNPC;
        private System.Windows.Forms.Button btnNextNPC;
        private System.Windows.Forms.Button btnClearRecycle;
        private System.Windows.Forms.Button btnFilterNPC;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem собратьЭдиторДляПередачиToolStripMenuItem;
        private System.Windows.Forms.Label lFindDialogID;
        private System.Windows.Forms.TextBox tbFindDialogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn subevents;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn npcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn subNPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dialogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardBattle;
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardSurvive;
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardSupport;
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardCredits;
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn repeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn repeatPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Legend;
        private System.Windows.Forms.DataGridViewComboBoxColumn worked;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn npc_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn identif;
        private System.Windows.Forms.DataGridViewTextBoxColumn cur_ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn new_ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RusText1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EngText1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEngText;
        private System.Windows.Forms.ToolStripMenuItem вытащитьНепереведённыеТекстыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьПереводToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem диалоговToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem квестовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem русскийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbNPCList;
        private System.Windows.Forms.Button bFindNPCReputation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNPCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDialogsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuestsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRussianName;
        private System.Windows.Forms.TabPage tabFraction;
        private System.Windows.Forms.TreeView treeFractionDialogs;
        private PCanvas fractionDialogShower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox fractionBox;
        private System.Windows.Forms.Button bRemoveFracDialog;
        private System.Windows.Forms.Button bEditFracDialog;
        private System.Windows.Forms.Button bAddFracDialog;
        private System.Windows.Forms.TabPage tabKnowledge;
        private System.Windows.Forms.ComboBox cbKnowlegeTypeValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbKnowledgeCategory;
        private System.Windows.Forms.TreeView treeKnowladge;
    }
}

