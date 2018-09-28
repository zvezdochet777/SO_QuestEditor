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
            this.bTestButton = new System.Windows.Forms.Button();
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
            this.tabInfoNPC.SuspendLayout();
            this.panelNpcLinkControls.SuspendLayout();
            this.tabReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReview)).BeginInit();
            this.panelReviewButtons.SuspendLayout();
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
            resources.ApplyResources(this.splitDialogs.Panel1, "splitDialogs.Panel1");
            this.splitDialogs.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitDialogs.Panel1.Controls.Add(this.gbDialogsEditor);
            this.toolTipDialogs.SetToolTip(this.splitDialogs.Panel1, resources.GetString("splitDialogs.Panel1.ToolTip"));
            // 
            // splitDialogs.Panel2
            // 
            resources.ApplyResources(this.splitDialogs.Panel2, "splitDialogs.Panel2");
            this.splitDialogs.Panel2.Controls.Add(this.gbEmulator);
            this.toolTipDialogs.SetToolTip(this.splitDialogs.Panel2, resources.GetString("splitDialogs.Panel2.ToolTip"));
            this.splitDialogs.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.splitDialogs, resources.GetString("splitDialogs.ToolTip"));
            // 
            // gbDialogsEditor
            // 
            resources.ApplyResources(this.gbDialogsEditor, "gbDialogsEditor");
            this.gbDialogsEditor.Controls.Add(this.splitDialogsTreeAndCanvas);
            this.gbDialogsEditor.Controls.Add(this.panelDialogTools);
            this.gbDialogsEditor.Name = "gbDialogsEditor";
            this.gbDialogsEditor.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.gbDialogsEditor, resources.GetString("gbDialogsEditor.ToolTip"));
            // 
            // splitDialogsTreeAndCanvas
            // 
            resources.ApplyResources(this.splitDialogsTreeAndCanvas, "splitDialogsTreeAndCanvas");
            this.splitDialogsTreeAndCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitDialogsTreeAndCanvas.Name = "splitDialogsTreeAndCanvas";
            // 
            // splitDialogsTreeAndCanvas.Panel1
            // 
            resources.ApplyResources(this.splitDialogsTreeAndCanvas.Panel1, "splitDialogsTreeAndCanvas.Panel1");
            this.splitDialogsTreeAndCanvas.Panel1.Controls.Add(this.treeDialogs);
            this.toolTipDialogs.SetToolTip(this.splitDialogsTreeAndCanvas.Panel1, resources.GetString("splitDialogsTreeAndCanvas.Panel1.ToolTip"));
            // 
            // splitDialogsTreeAndCanvas.Panel2
            // 
            resources.ApplyResources(this.splitDialogsTreeAndCanvas.Panel2, "splitDialogsTreeAndCanvas.Panel2");
            this.splitDialogsTreeAndCanvas.Panel2.Controls.Add(this.DialogShower);
            this.toolTipDialogs.SetToolTip(this.splitDialogsTreeAndCanvas.Panel2, resources.GetString("splitDialogsTreeAndCanvas.Panel2.ToolTip"));
            this.splitDialogsTreeAndCanvas.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.splitDialogsTreeAndCanvas, resources.GetString("splitDialogsTreeAndCanvas.ToolTip"));
            // 
            // treeDialogs
            // 
            resources.ApplyResources(this.treeDialogs, "treeDialogs");
            this.treeDialogs.Name = "treeDialogs";
            this.treeDialogs.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.treeDialogs, resources.GetString("treeDialogs.ToolTip"));
            this.treeDialogs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDialogs_GotFocus);
            // 
            // DialogShower
            // 
            resources.ApplyResources(this.DialogShower, "DialogShower");
            this.DialogShower.AllowDrop = true;
            this.DialogShower.BackColor = System.Drawing.Color.White;
            this.DialogShower.GridFitText = false;
            this.DialogShower.Name = "DialogShower";
            this.DialogShower.RegionManagement = true;
            this.toolTipDialogs.SetToolTip(this.DialogShower, resources.GetString("DialogShower.ToolTip"));
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
            this.panelDialogTools.Controls.Add(this.bTestButton);
            this.panelDialogTools.Controls.Add(this.bCenterizeDialogShower);
            this.panelDialogTools.Controls.Add(this.labelYNode);
            this.panelDialogTools.Controls.Add(this.labelXNode);
            this.panelDialogTools.Controls.Add(this.bRemoveDialog);
            this.panelDialogTools.Controls.Add(this.bEditDialog);
            this.panelDialogTools.Controls.Add(this.bAddDialog);
            this.panelDialogTools.Name = "panelDialogTools";
            this.toolTipDialogs.SetToolTip(this.panelDialogTools, resources.GetString("panelDialogTools.ToolTip"));
            // 
            // lFindDialogID
            // 
            resources.ApplyResources(this.lFindDialogID, "lFindDialogID");
            this.lFindDialogID.Name = "lFindDialogID";
            this.toolTipDialogs.SetToolTip(this.lFindDialogID, resources.GetString("lFindDialogID.ToolTip"));
            // 
            // tbFindDialogID
            // 
            resources.ApplyResources(this.tbFindDialogID, "tbFindDialogID");
            this.tbFindDialogID.Name = "tbFindDialogID";
            this.toolTipDialogs.SetToolTip(this.tbFindDialogID, resources.GetString("tbFindDialogID.ToolTip"));
            this.tbFindDialogID.TextChanged += new System.EventHandler(this.tbFindDialogID_TextChanged);
            // 
            // btnClearRecycle
            // 
            resources.ApplyResources(this.btnClearRecycle, "btnClearRecycle");
            this.btnClearRecycle.Name = "btnClearRecycle";
            this.toolTipDialogs.SetToolTip(this.btnClearRecycle, resources.GetString("btnClearRecycle.ToolTip"));
            this.btnClearRecycle.UseVisualStyleBackColor = true;
            this.btnClearRecycle.Click += new System.EventHandler(this.btnClearRecycle_Click);
            // 
            // labelDrawingTip
            // 
            resources.ApplyResources(this.labelDrawingTip, "labelDrawingTip");
            this.labelDrawingTip.Name = "labelDrawingTip";
            this.toolTipDialogs.SetToolTip(this.labelDrawingTip, resources.GetString("labelDrawingTip.ToolTip"));
            // 
            // bTestButton
            // 
            resources.ApplyResources(this.bTestButton, "bTestButton");
            this.bTestButton.Name = "bTestButton";
            this.bTestButton.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.bTestButton, resources.GetString("bTestButton.ToolTip"));
            this.bTestButton.UseVisualStyleBackColor = true;
            this.bTestButton.Click += new System.EventHandler(this.bTestButton_Click);
            // 
            // bCenterizeDialogShower
            // 
            resources.ApplyResources(this.bCenterizeDialogShower, "bCenterizeDialogShower");
            this.bCenterizeDialogShower.ImageList = this.imageList;
            this.bCenterizeDialogShower.Name = "bCenterizeDialogShower";
            this.toolTipDialogs.SetToolTip(this.bCenterizeDialogShower, resources.GetString("bCenterizeDialogShower.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this.labelYNode, resources.GetString("labelYNode.ToolTip"));
            // 
            // labelXNode
            // 
            resources.ApplyResources(this.labelXNode, "labelXNode");
            this.labelXNode.Name = "labelXNode";
            this.toolTipDialogs.SetToolTip(this.labelXNode, resources.GetString("labelXNode.ToolTip"));
            // 
            // bRemoveDialog
            // 
            resources.ApplyResources(this.bRemoveDialog, "bRemoveDialog");
            this.bRemoveDialog.ImageList = this.imageList;
            this.bRemoveDialog.Name = "bRemoveDialog";
            this.toolTipDialogs.SetToolTip(this.bRemoveDialog, resources.GetString("bRemoveDialog.ToolTip"));
            this.bRemoveDialog.UseVisualStyleBackColor = true;
            this.bRemoveDialog.Click += new System.EventHandler(this.bRemoveDialog_Click);
            // 
            // bEditDialog
            // 
            resources.ApplyResources(this.bEditDialog, "bEditDialog");
            this.bEditDialog.ImageList = this.imageList;
            this.bEditDialog.Name = "bEditDialog";
            this.toolTipDialogs.SetToolTip(this.bEditDialog, resources.GetString("bEditDialog.ToolTip"));
            this.bEditDialog.UseVisualStyleBackColor = true;
            this.bEditDialog.Click += new System.EventHandler(this.bEditDialog_Click);
            // 
            // bAddDialog
            // 
            resources.ApplyResources(this.bAddDialog, "bAddDialog");
            this.bAddDialog.ImageList = this.imageList;
            this.bAddDialog.Name = "bAddDialog";
            this.toolTipDialogs.SetToolTip(this.bAddDialog, resources.GetString("bAddDialog.ToolTip"));
            this.bAddDialog.UseVisualStyleBackColor = true;
            this.bAddDialog.Click += new System.EventHandler(this.bAddDialog_Click);
            // 
            // gbEmulator
            // 
            resources.ApplyResources(this.gbEmulator, "gbEmulator");
            this.gbEmulator.Controls.Add(this.splitDialogsEmulator);
            this.gbEmulator.Name = "gbEmulator";
            this.gbEmulator.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.gbEmulator, resources.GetString("gbEmulator.ToolTip"));
            // 
            // splitDialogsEmulator
            // 
            resources.ApplyResources(this.splitDialogsEmulator, "splitDialogsEmulator");
            this.splitDialogsEmulator.Name = "splitDialogsEmulator";
            // 
            // splitDialogsEmulator.Panel1
            // 
            resources.ApplyResources(this.splitDialogsEmulator.Panel1, "splitDialogsEmulator.Panel1");
            this.toolTipDialogs.SetToolTip(this.splitDialogsEmulator.Panel1, resources.GetString("splitDialogsEmulator.Panel1.ToolTip"));
            // 
            // splitDialogsEmulator.Panel2
            // 
            resources.ApplyResources(this.splitDialogsEmulator.Panel2, "splitDialogsEmulator.Panel2");
            this.toolTipDialogs.SetToolTip(this.splitDialogsEmulator.Panel2, resources.GetString("splitDialogsEmulator.Panel2.ToolTip"));
            this.splitDialogsEmulator.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.splitDialogsEmulator, resources.GetString("splitDialogsEmulator.ToolTip"));
            // 
            // splitQuestsContainer
            // 
            resources.ApplyResources(this.splitQuestsContainer, "splitQuestsContainer");
            this.splitQuestsContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitQuestsContainer.Name = "splitQuestsContainer";
            // 
            // splitQuestsContainer.Panel1
            // 
            resources.ApplyResources(this.splitQuestsContainer.Panel1, "splitQuestsContainer.Panel1");
            this.splitQuestsContainer.Panel1.Controls.Add(this.treeQuest);
            this.splitQuestsContainer.Panel1.Controls.Add(this.labelQuestTree);
            this.splitQuestsContainer.Panel1.Controls.Add(this.treeQuestBuffer);
            this.splitQuestsContainer.Panel1.Controls.Add(this.labelBuffer);
            this.toolTipDialogs.SetToolTip(this.splitQuestsContainer.Panel1, resources.GetString("splitQuestsContainer.Panel1.ToolTip"));
            // 
            // splitQuestsContainer.Panel2
            // 
            resources.ApplyResources(this.splitQuestsContainer.Panel2, "splitQuestsContainer.Panel2");
            this.toolTipDialogs.SetToolTip(this.splitQuestsContainer.Panel2, resources.GetString("splitQuestsContainer.Panel2.ToolTip"));
            this.splitQuestsContainer.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.splitQuestsContainer, resources.GetString("splitQuestsContainer.ToolTip"));
            // 
            // treeQuest
            // 
            resources.ApplyResources(this.treeQuest, "treeQuest");
            this.treeQuest.Name = "treeQuest";
            this.toolTipDialogs.SetToolTip(this.treeQuest, resources.GetString("treeQuest.ToolTip"));
            this.treeQuest.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuest_AfterSelect);
            this.treeQuest.Click += new System.EventHandler(this.treeQuest_Click);
            this.treeQuest.DoubleClick += new System.EventHandler(this.treeQuestClicked);
            this.treeQuest.Leave += new System.EventHandler(this.treeQuest_Leave);
            // 
            // labelQuestTree
            // 
            resources.ApplyResources(this.labelQuestTree, "labelQuestTree");
            this.labelQuestTree.Name = "labelQuestTree";
            this.toolTipDialogs.SetToolTip(this.labelQuestTree, resources.GetString("labelQuestTree.ToolTip"));
            // 
            // treeQuestBuffer
            // 
            resources.ApplyResources(this.treeQuestBuffer, "treeQuestBuffer");
            this.treeQuestBuffer.Name = "treeQuestBuffer";
            this.toolTipDialogs.SetToolTip(this.treeQuestBuffer, resources.GetString("treeQuestBuffer.ToolTip"));
            this.treeQuestBuffer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuestBuffer_AfterSelect);
            this.treeQuestBuffer.Click += new System.EventHandler(this.treeQuestBuffer_Click);
            this.treeQuestBuffer.DoubleClick += new System.EventHandler(this.treeQuestBuffer_DoubleClick);
            // 
            // labelBuffer
            // 
            resources.ApplyResources(this.labelBuffer, "labelBuffer");
            this.labelBuffer.Name = "labelBuffer";
            this.toolTipDialogs.SetToolTip(this.labelBuffer, resources.GetString("labelBuffer.ToolTip"));
            // 
            // NPCBox
            // 
            resources.ApplyResources(this.NPCBox, "NPCBox");
            this.NPCBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.NPCBox.DropDownWidth = 280;
            this.NPCBox.FormattingEnabled = true;
            this.NPCBox.Name = "NPCBox";
            this.toolTipDialogs.SetToolTip(this.NPCBox, resources.GetString("NPCBox.ToolTip"));
            this.NPCBox.SelectedIndexChanged += new System.EventHandler(this.NPCBox_SelectedIndexChanged);
            this.NPCBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NPCBox_KeyDown);
            // 
            // labelChosenNPC
            // 
            resources.ApplyResources(this.labelChosenNPC, "labelChosenNPC");
            this.labelChosenNPC.Name = "labelChosenNPC";
            this.toolTipDialogs.SetToolTip(this.labelChosenNPC, resources.GetString("labelChosenNPC.ToolTip"));
            // 
            // CentralDock
            // 
            resources.ApplyResources(this.CentralDock, "CentralDock");
            this.CentralDock.Controls.Add(this.tabDialogs);
            this.CentralDock.Controls.Add(this.tabQuests);
            this.CentralDock.Controls.Add(this.tabInfoNPC);
            this.CentralDock.Controls.Add(this.tabReview);
            this.CentralDock.Controls.Add(this.tabManage);
            this.CentralDock.Controls.Add(this.tabTranslate);
            this.CentralDock.Controls.Add(this.tabSearch);
            this.CentralDock.HotTrack = true;
            this.CentralDock.Name = "CentralDock";
            this.CentralDock.SelectedIndex = 0;
            this.CentralDock.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.CentralDock.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.CentralDock, resources.GetString("CentralDock.ToolTip"));
            this.CentralDock.SelectedIndexChanged += new System.EventHandler(this.onSelectTab);
            // 
            // tabDialogs
            // 
            resources.ApplyResources(this.tabDialogs, "tabDialogs");
            this.tabDialogs.Controls.Add(this.splitDialogs);
            this.tabDialogs.Name = "tabDialogs";
            this.toolTipDialogs.SetToolTip(this.tabDialogs, resources.GetString("tabDialogs.ToolTip"));
            this.tabDialogs.UseVisualStyleBackColor = true;
            // 
            // tabQuests
            // 
            resources.ApplyResources(this.tabQuests, "tabQuests");
            this.tabQuests.Controls.Add(this.splitQuestsContainer);
            this.tabQuests.Controls.Add(this.panelQuestTools);
            this.tabQuests.Controls.Add(this.panelSelectQuest);
            this.tabQuests.Name = "tabQuests";
            this.toolTipDialogs.SetToolTip(this.tabQuests, resources.GetString("tabQuests.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this.panelQuestTools, resources.GetString("panelQuestTools.ToolTip"));
            // 
            // bClearBuffer
            // 
            resources.ApplyResources(this.bClearBuffer, "bClearBuffer");
            this.bClearBuffer.Name = "bClearBuffer";
            this.toolTipDialogs.SetToolTip(this.bClearBuffer, resources.GetString("bClearBuffer.ToolTip"));
            this.bClearBuffer.UseVisualStyleBackColor = true;
            this.bClearBuffer.Click += new System.EventHandler(this.bClearBuffer_Click);
            // 
            // bCutEvents
            // 
            resources.ApplyResources(this.bCutEvents, "bCutEvents");
            this.bCutEvents.Name = "bCutEvents";
            this.toolTipDialogs.SetToolTip(this.bCutEvents, resources.GetString("bCutEvents.ToolTip"));
            this.bCutEvents.UseVisualStyleBackColor = true;
            this.bCutEvents.Click += new System.EventHandler(this.bCutEvents_Click);
            // 
            // bPasteEvents
            // 
            resources.ApplyResources(this.bPasteEvents, "bPasteEvents");
            this.bPasteEvents.Name = "bPasteEvents";
            this.toolTipDialogs.SetToolTip(this.bPasteEvents, resources.GetString("bPasteEvents.ToolTip"));
            this.bPasteEvents.UseVisualStyleBackColor = true;
            this.bPasteEvents.Click += new System.EventHandler(this.bPasteEvents_Click);
            // 
            // bCopyEvents
            // 
            resources.ApplyResources(this.bCopyEvents, "bCopyEvents");
            this.bCopyEvents.Name = "bCopyEvents";
            this.toolTipDialogs.SetToolTip(this.bCopyEvents, resources.GetString("bCopyEvents.ToolTip"));
            this.bCopyEvents.UseVisualStyleBackColor = true;
            this.bCopyEvents.Click += new System.EventHandler(this.bCopyEvents_Click);
            // 
            // bQuestDown
            // 
            resources.ApplyResources(this.bQuestDown, "bQuestDown");
            this.bQuestDown.Name = "bQuestDown";
            this.toolTipDialogs.SetToolTip(this.bQuestDown, resources.GetString("bQuestDown.ToolTip"));
            this.bQuestDown.UseVisualStyleBackColor = true;
            this.bQuestDown.Click += new System.EventHandler(this.bQuestDown_Click);
            // 
            // bQuestUp
            // 
            resources.ApplyResources(this.bQuestUp, "bQuestUp");
            this.bQuestUp.Name = "bQuestUp";
            this.toolTipDialogs.SetToolTip(this.bQuestUp, resources.GetString("bQuestUp.ToolTip"));
            this.bQuestUp.UseVisualStyleBackColor = true;
            this.bQuestUp.Click += new System.EventHandler(this.bQuestUp_Click);
            // 
            // bRemoveEvent
            // 
            resources.ApplyResources(this.bRemoveEvent, "bRemoveEvent");
            this.bRemoveEvent.ImageList = this.imageList;
            this.bRemoveEvent.Name = "bRemoveEvent";
            this.toolTipDialogs.SetToolTip(this.bRemoveEvent, resources.GetString("bRemoveEvent.ToolTip"));
            this.bRemoveEvent.UseVisualStyleBackColor = true;
            this.bRemoveEvent.Click += new System.EventHandler(this.bRemoveEvent_Click);
            // 
            // bEditEvent
            // 
            resources.ApplyResources(this.bEditEvent, "bEditEvent");
            this.bEditEvent.ImageList = this.imageList;
            this.bEditEvent.Name = "bEditEvent";
            this.toolTipDialogs.SetToolTip(this.bEditEvent, resources.GetString("bEditEvent.ToolTip"));
            this.bEditEvent.UseVisualStyleBackColor = true;
            this.bEditEvent.Click += new System.EventHandler(this.bEditEvent_Click);
            // 
            // bAddEvent
            // 
            resources.ApplyResources(this.bAddEvent, "bAddEvent");
            this.bAddEvent.ImageList = this.imageList;
            this.bAddEvent.Name = "bAddEvent";
            this.toolTipDialogs.SetToolTip(this.bAddEvent, resources.GetString("bAddEvent.ToolTip"));
            this.bAddEvent.UseMnemonic = false;
            this.bAddEvent.UseVisualStyleBackColor = false;
            this.bAddEvent.Click += new System.EventHandler(this.bAddEvent_Click);
            // 
            // panelSelectQuest
            // 
            resources.ApplyResources(this.panelSelectQuest, "panelSelectQuest");
            this.panelSelectQuest.Controls.Add(this.bRemoveQuest);
            this.panelSelectQuest.Controls.Add(this.bAddQuest);
            this.panelSelectQuest.Controls.Add(this.QuestBox);
            this.panelSelectQuest.Controls.Add(this.labelChosenQuest);
            this.panelSelectQuest.Name = "panelSelectQuest";
            this.toolTipDialogs.SetToolTip(this.panelSelectQuest, resources.GetString("panelSelectQuest.ToolTip"));
            // 
            // bRemoveQuest
            // 
            resources.ApplyResources(this.bRemoveQuest, "bRemoveQuest");
            this.bRemoveQuest.Name = "bRemoveQuest";
            this.toolTipDialogs.SetToolTip(this.bRemoveQuest, resources.GetString("bRemoveQuest.ToolTip"));
            this.bRemoveQuest.UseVisualStyleBackColor = true;
            this.bRemoveQuest.Click += new System.EventHandler(this.bRemoveQuest_Click);
            // 
            // bAddQuest
            // 
            resources.ApplyResources(this.bAddQuest, "bAddQuest");
            this.bAddQuest.Name = "bAddQuest";
            this.toolTipDialogs.SetToolTip(this.bAddQuest, resources.GetString("bAddQuest.ToolTip"));
            this.bAddQuest.UseVisualStyleBackColor = true;
            this.bAddQuest.Click += new System.EventHandler(this.bAddQuest_Click);
            // 
            // QuestBox
            // 
            resources.ApplyResources(this.QuestBox, "QuestBox");
            this.QuestBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.QuestBox.FormattingEnabled = true;
            this.QuestBox.Name = "QuestBox";
            this.toolTipDialogs.SetToolTip(this.QuestBox, resources.GetString("QuestBox.ToolTip"));
            this.QuestBox.SelectedIndexChanged += new System.EventHandler(this.QuestBox_SelectedIndexChanged);
            this.QuestBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuestBox_KeyDown);
            // 
            // labelChosenQuest
            // 
            resources.ApplyResources(this.labelChosenQuest, "labelChosenQuest");
            this.labelChosenQuest.Name = "labelChosenQuest";
            this.toolTipDialogs.SetToolTip(this.labelChosenQuest, resources.GetString("labelChosenQuest.ToolTip"));
            // 
            // tabInfoNPC
            // 
            resources.ApplyResources(this.tabInfoNPC, "tabInfoNPC");
            this.tabInfoNPC.Controls.Add(this.npcLinkShower);
            this.tabInfoNPC.Controls.Add(this.panelNpcLinkControls);
            this.tabInfoNPC.Name = "tabInfoNPC";
            this.toolTipDialogs.SetToolTip(this.tabInfoNPC, resources.GetString("tabInfoNPC.ToolTip"));
            this.tabInfoNPC.UseVisualStyleBackColor = true;
            // 
            // npcLinkShower
            // 
            resources.ApplyResources(this.npcLinkShower, "npcLinkShower");
            this.npcLinkShower.AllowDrop = true;
            this.npcLinkShower.BackColor = System.Drawing.Color.White;
            this.npcLinkShower.GridFitText = false;
            this.npcLinkShower.Name = "npcLinkShower";
            this.npcLinkShower.RegionManagement = true;
            this.toolTipDialogs.SetToolTip(this.npcLinkShower, resources.GetString("npcLinkShower.ToolTip"));
            // 
            // panelNpcLinkControls
            // 
            resources.ApplyResources(this.panelNpcLinkControls, "panelNpcLinkControls");
            this.panelNpcLinkControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNpcLinkControls.Controls.Add(this.labelAdviceNpcLink);
            this.panelNpcLinkControls.Controls.Add(this.bNpcLinkExecute);
            this.panelNpcLinkControls.Name = "panelNpcLinkControls";
            this.toolTipDialogs.SetToolTip(this.panelNpcLinkControls, resources.GetString("panelNpcLinkControls.ToolTip"));
            // 
            // labelAdviceNpcLink
            // 
            resources.ApplyResources(this.labelAdviceNpcLink, "labelAdviceNpcLink");
            this.labelAdviceNpcLink.Name = "labelAdviceNpcLink";
            this.toolTipDialogs.SetToolTip(this.labelAdviceNpcLink, resources.GetString("labelAdviceNpcLink.ToolTip"));
            // 
            // bNpcLinkExecute
            // 
            resources.ApplyResources(this.bNpcLinkExecute, "bNpcLinkExecute");
            this.bNpcLinkExecute.Name = "bNpcLinkExecute";
            this.toolTipDialogs.SetToolTip(this.bNpcLinkExecute, resources.GetString("bNpcLinkExecute.ToolTip"));
            this.bNpcLinkExecute.UseVisualStyleBackColor = true;
            this.bNpcLinkExecute.Click += new System.EventHandler(this.bNpcLinkExecute_Click);
            // 
            // tabReview
            // 
            resources.ApplyResources(this.tabReview, "tabReview");
            this.tabReview.Controls.Add(this.dgvReview);
            this.tabReview.Controls.Add(this.panelReviewButtons);
            this.tabReview.Name = "tabReview";
            this.toolTipDialogs.SetToolTip(this.tabReview, resources.GetString("tabReview.ToolTip"));
            this.tabReview.UseVisualStyleBackColor = true;
            // 
            // dgvReview
            // 
            resources.ApplyResources(this.dgvReview, "dgvReview");
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
            this.dgvReview.Name = "dgvReview";
            this.toolTipDialogs.SetToolTip(this.dgvReview, resources.GetString("dgvReview.ToolTip"));
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
            resources.ApplyResources(this.panelReviewButtons, "panelReviewButtons");
            this.panelReviewButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReviewButtons.Controls.Add(this.gbNPCcheck);
            this.panelReviewButtons.Controls.Add(this.gbQuestCheck);
            this.panelReviewButtons.Name = "panelReviewButtons";
            this.toolTipDialogs.SetToolTip(this.panelReviewButtons, resources.GetString("panelReviewButtons.ToolTip"));
            // 
            // gbNPCcheck
            // 
            resources.ApplyResources(this.gbNPCcheck, "gbNPCcheck");
            this.gbNPCcheck.Controls.Add(this.cbOnlyOnLocation);
            this.gbNPCcheck.Controls.Add(this.cbLocation);
            this.gbNPCcheck.Controls.Add(this.numQuests);
            this.gbNPCcheck.Controls.Add(this.labelLessThan2);
            this.gbNPCcheck.Controls.Add(this.cbNumQuests);
            this.gbNPCcheck.Controls.Add(this.numDialogs);
            this.gbNPCcheck.Controls.Add(this.labelLessThan1);
            this.gbNPCcheck.Controls.Add(this.cbNumDialogs);
            this.gbNPCcheck.Controls.Add(this.bFindNPC);
            this.gbNPCcheck.Name = "gbNPCcheck";
            this.gbNPCcheck.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.gbNPCcheck, resources.GetString("gbNPCcheck.ToolTip"));
            // 
            // cbOnlyOnLocation
            // 
            resources.ApplyResources(this.cbOnlyOnLocation, "cbOnlyOnLocation");
            this.cbOnlyOnLocation.Name = "cbOnlyOnLocation";
            this.toolTipDialogs.SetToolTip(this.cbOnlyOnLocation, resources.GetString("cbOnlyOnLocation.ToolTip"));
            this.cbOnlyOnLocation.UseVisualStyleBackColor = true;
            // 
            // cbLocation
            // 
            resources.ApplyResources(this.cbLocation, "cbLocation");
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Name = "cbLocation";
            this.toolTipDialogs.SetToolTip(this.cbLocation, resources.GetString("cbLocation.ToolTip"));
            // 
            // numQuests
            // 
            resources.ApplyResources(this.numQuests, "numQuests");
            this.numQuests.Name = "numQuests";
            this.toolTipDialogs.SetToolTip(this.numQuests, resources.GetString("numQuests.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this.labelLessThan2, resources.GetString("labelLessThan2.ToolTip"));
            // 
            // cbNumQuests
            // 
            resources.ApplyResources(this.cbNumQuests, "cbNumQuests");
            this.cbNumQuests.Name = "cbNumQuests";
            this.toolTipDialogs.SetToolTip(this.cbNumQuests, resources.GetString("cbNumQuests.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this.numDialogs, resources.GetString("numDialogs.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this.labelLessThan1, resources.GetString("labelLessThan1.ToolTip"));
            // 
            // cbNumDialogs
            // 
            resources.ApplyResources(this.cbNumDialogs, "cbNumDialogs");
            this.cbNumDialogs.Name = "cbNumDialogs";
            this.toolTipDialogs.SetToolTip(this.cbNumDialogs, resources.GetString("cbNumDialogs.ToolTip"));
            this.cbNumDialogs.UseVisualStyleBackColor = true;
            // 
            // bFindNPC
            // 
            resources.ApplyResources(this.bFindNPC, "bFindNPC");
            this.bFindNPC.Name = "bFindNPC";
            this.toolTipDialogs.SetToolTip(this.bFindNPC, resources.GetString("bFindNPC.ToolTip"));
            this.bFindNPC.UseVisualStyleBackColor = true;
            this.bFindNPC.Click += new System.EventHandler(this.bFindNPC_Click);
            // 
            // gbQuestCheck
            // 
            resources.ApplyResources(this.gbQuestCheck, "gbQuestCheck");
            this.gbQuestCheck.Controls.Add(this.labelItemTarget);
            this.gbQuestCheck.Controls.Add(this.cbItemTarget);
            this.gbQuestCheck.Controls.Add(this.labelItemReward);
            this.gbQuestCheck.Controls.Add(this.cbItemReward);
            this.gbQuestCheck.Controls.Add(this.bFindQuest);
            this.gbQuestCheck.Name = "gbQuestCheck";
            this.gbQuestCheck.TabStop = false;
            this.toolTipDialogs.SetToolTip(this.gbQuestCheck, resources.GetString("gbQuestCheck.ToolTip"));
            // 
            // labelItemTarget
            // 
            resources.ApplyResources(this.labelItemTarget, "labelItemTarget");
            this.labelItemTarget.Name = "labelItemTarget";
            this.toolTipDialogs.SetToolTip(this.labelItemTarget, resources.GetString("labelItemTarget.ToolTip"));
            // 
            // cbItemTarget
            // 
            resources.ApplyResources(this.cbItemTarget, "cbItemTarget");
            this.cbItemTarget.FormattingEnabled = true;
            this.cbItemTarget.Name = "cbItemTarget";
            this.toolTipDialogs.SetToolTip(this.cbItemTarget, resources.GetString("cbItemTarget.ToolTip"));
            // 
            // labelItemReward
            // 
            resources.ApplyResources(this.labelItemReward, "labelItemReward");
            this.labelItemReward.Name = "labelItemReward";
            this.toolTipDialogs.SetToolTip(this.labelItemReward, resources.GetString("labelItemReward.ToolTip"));
            // 
            // cbItemReward
            // 
            resources.ApplyResources(this.cbItemReward, "cbItemReward");
            this.cbItemReward.FormattingEnabled = true;
            this.cbItemReward.Name = "cbItemReward";
            this.toolTipDialogs.SetToolTip(this.cbItemReward, resources.GetString("cbItemReward.ToolTip"));
            // 
            // bFindQuest
            // 
            resources.ApplyResources(this.bFindQuest, "bFindQuest");
            this.bFindQuest.Name = "bFindQuest";
            this.toolTipDialogs.SetToolTip(this.bFindQuest, resources.GetString("bFindQuest.ToolTip"));
            this.bFindQuest.UseVisualStyleBackColor = true;
            this.bFindQuest.Click += new System.EventHandler(this.bFindQuest_Click);
            // 
            // tabManage
            // 
            resources.ApplyResources(this.tabManage, "tabManage");
            this.tabManage.Controls.Add(this.dgvManage);
            this.tabManage.Controls.Add(this.panel2);
            this.tabManage.Name = "tabManage";
            this.toolTipDialogs.SetToolTip(this.tabManage, resources.GetString("tabManage.ToolTip"));
            this.tabManage.UseVisualStyleBackColor = true;
            // 
            // dgvManage
            // 
            resources.ApplyResources(this.dgvManage, "dgvManage");
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
            this.dgvManage.Name = "dgvManage";
            this.toolTipDialogs.SetToolTip(this.dgvManage, resources.GetString("dgvManage.ToolTip"));
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
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.bSaveManage);
            this.panel2.Name = "panel2";
            this.toolTipDialogs.SetToolTip(this.panel2, resources.GetString("panel2.ToolTip"));
            // 
            // bSaveManage
            // 
            resources.ApplyResources(this.bSaveManage, "bSaveManage");
            this.bSaveManage.Name = "bSaveManage";
            this.toolTipDialogs.SetToolTip(this.bSaveManage, resources.GetString("bSaveManage.ToolTip"));
            this.bSaveManage.UseVisualStyleBackColor = true;
            this.bSaveManage.Click += new System.EventHandler(this.bSaveManage_Click);
            // 
            // tabTranslate
            // 
            resources.ApplyResources(this.tabTranslate, "tabTranslate");
            this.tabTranslate.Controls.Add(this.dgvLocaleDiff);
            this.tabTranslate.Controls.Add(this.panelDiffLocale);
            this.tabTranslate.Name = "tabTranslate";
            this.toolTipDialogs.SetToolTip(this.tabTranslate, resources.GetString("tabTranslate.ToolTip"));
            this.tabTranslate.UseVisualStyleBackColor = true;
            // 
            // dgvLocaleDiff
            // 
            resources.ApplyResources(this.dgvLocaleDiff, "dgvLocaleDiff");
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
            this.dgvLocaleDiff.Name = "dgvLocaleDiff";
            this.dgvLocaleDiff.ReadOnly = true;
            this.toolTipDialogs.SetToolTip(this.dgvLocaleDiff, resources.GetString("dgvLocaleDiff.ToolTip"));
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
            resources.ApplyResources(this.panelDiffLocale, "panelDiffLocale");
            this.panelDiffLocale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDiffLocale.Controls.Add(this.labelLocalizeOuput);
            this.panelDiffLocale.Controls.Add(this.cbActualFinder);
            this.panelDiffLocale.Controls.Add(this.cbOutdatedFinder);
            this.panelDiffLocale.Controls.Add(this.bFindQuestDifference);
            this.panelDiffLocale.Controls.Add(this.bFindDialogDifference);
            this.panelDiffLocale.Name = "panelDiffLocale";
            this.toolTipDialogs.SetToolTip(this.panelDiffLocale, resources.GetString("panelDiffLocale.ToolTip"));
            // 
            // labelLocalizeOuput
            // 
            resources.ApplyResources(this.labelLocalizeOuput, "labelLocalizeOuput");
            this.labelLocalizeOuput.Name = "labelLocalizeOuput";
            this.toolTipDialogs.SetToolTip(this.labelLocalizeOuput, resources.GetString("labelLocalizeOuput.ToolTip"));
            // 
            // cbActualFinder
            // 
            resources.ApplyResources(this.cbActualFinder, "cbActualFinder");
            this.cbActualFinder.Name = "cbActualFinder";
            this.toolTipDialogs.SetToolTip(this.cbActualFinder, resources.GetString("cbActualFinder.ToolTip"));
            this.cbActualFinder.UseVisualStyleBackColor = true;
            // 
            // cbOutdatedFinder
            // 
            resources.ApplyResources(this.cbOutdatedFinder, "cbOutdatedFinder");
            this.cbOutdatedFinder.Checked = true;
            this.cbOutdatedFinder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOutdatedFinder.Name = "cbOutdatedFinder";
            this.toolTipDialogs.SetToolTip(this.cbOutdatedFinder, resources.GetString("cbOutdatedFinder.ToolTip"));
            this.cbOutdatedFinder.UseVisualStyleBackColor = true;
            // 
            // bFindQuestDifference
            // 
            resources.ApplyResources(this.bFindQuestDifference, "bFindQuestDifference");
            this.bFindQuestDifference.Name = "bFindQuestDifference";
            this.toolTipDialogs.SetToolTip(this.bFindQuestDifference, resources.GetString("bFindQuestDifference.ToolTip"));
            this.bFindQuestDifference.UseVisualStyleBackColor = true;
            this.bFindQuestDifference.Click += new System.EventHandler(this.bFindQuestDifference_Click);
            // 
            // bFindDialogDifference
            // 
            resources.ApplyResources(this.bFindDialogDifference, "bFindDialogDifference");
            this.bFindDialogDifference.Name = "bFindDialogDifference";
            this.toolTipDialogs.SetToolTip(this.bFindDialogDifference, resources.GetString("bFindDialogDifference.ToolTip"));
            this.bFindDialogDifference.UseVisualStyleBackColor = true;
            this.bFindDialogDifference.Click += new System.EventHandler(this.bFindDialogDifference_Click);
            // 
            // tabSearch
            // 
            resources.ApplyResources(this.tabSearch, "tabSearch");
            this.tabSearch.Controls.Add(this.dgvSearch);
            this.tabSearch.Controls.Add(this.panelSearchTools);
            this.tabSearch.Name = "tabSearch";
            this.toolTipDialogs.SetToolTip(this.tabSearch, resources.GetString("tabSearch.ToolTip"));
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // dgvSearch
            // 
            resources.ApplyResources(this.dgvSearch, "dgvSearch");
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colNPC,
            this.colID,
            this.colText,
            this.colEngText});
            this.dgvSearch.Name = "dgvSearch";
            this.toolTipDialogs.SetToolTip(this.dgvSearch, resources.GetString("dgvSearch.ToolTip"));
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
            resources.ApplyResources(this.panelSearchTools, "panelSearchTools");
            this.panelSearchTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchTools.Controls.Add(this.cbIgnoreCase);
            this.panelSearchTools.Controls.Add(this.labelSearchResult);
            this.panelSearchTools.Controls.Add(this.labelPhraseToSearch);
            this.panelSearchTools.Controls.Add(this.tbPhraseToSearch);
            this.panelSearchTools.Controls.Add(this.bStartSearch);
            this.panelSearchTools.Name = "panelSearchTools";
            this.toolTipDialogs.SetToolTip(this.panelSearchTools, resources.GetString("panelSearchTools.ToolTip"));
            // 
            // cbIgnoreCase
            // 
            resources.ApplyResources(this.cbIgnoreCase, "cbIgnoreCase");
            this.cbIgnoreCase.Checked = true;
            this.cbIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.toolTipDialogs.SetToolTip(this.cbIgnoreCase, resources.GetString("cbIgnoreCase.ToolTip"));
            this.cbIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // labelSearchResult
            // 
            resources.ApplyResources(this.labelSearchResult, "labelSearchResult");
            this.labelSearchResult.Name = "labelSearchResult";
            this.toolTipDialogs.SetToolTip(this.labelSearchResult, resources.GetString("labelSearchResult.ToolTip"));
            // 
            // labelPhraseToSearch
            // 
            resources.ApplyResources(this.labelPhraseToSearch, "labelPhraseToSearch");
            this.labelPhraseToSearch.Name = "labelPhraseToSearch";
            this.toolTipDialogs.SetToolTip(this.labelPhraseToSearch, resources.GetString("labelPhraseToSearch.ToolTip"));
            // 
            // tbPhraseToSearch
            // 
            resources.ApplyResources(this.tbPhraseToSearch, "tbPhraseToSearch");
            this.tbPhraseToSearch.Name = "tbPhraseToSearch";
            this.toolTipDialogs.SetToolTip(this.tbPhraseToSearch, resources.GetString("tbPhraseToSearch.ToolTip"));
            // 
            // bStartSearch
            // 
            resources.ApplyResources(this.bStartSearch, "bStartSearch");
            this.bStartSearch.Name = "bStartSearch";
            this.toolTipDialogs.SetToolTip(this.bStartSearch, resources.GetString("bStartSearch.ToolTip"));
            this.bStartSearch.UseVisualStyleBackColor = true;
            this.bStartSearch.Click += new System.EventHandler(this.bStartSearch_Click);
            // 
            // panelSelectNPC
            // 
            resources.ApplyResources(this.panelSelectNPC, "panelSelectNPC");
            this.panelSelectNPC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSelectNPC.Controls.Add(this.btnFilterNPC);
            this.panelSelectNPC.Controls.Add(this.btnNextNPC);
            this.panelSelectNPC.Controls.Add(this.btnBackNPC);
            this.panelSelectNPC.Controls.Add(this.bDelNPC);
            this.panelSelectNPC.Controls.Add(this.bAddNPC);
            this.panelSelectNPC.Controls.Add(this.labelChosenNPC);
            this.panelSelectNPC.Controls.Add(this.NPCBox);
            this.panelSelectNPC.Name = "panelSelectNPC";
            this.toolTipDialogs.SetToolTip(this.panelSelectNPC, resources.GetString("panelSelectNPC.ToolTip"));
            // 
            // btnFilterNPC
            // 
            resources.ApplyResources(this.btnFilterNPC, "btnFilterNPC");
            this.btnFilterNPC.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.filter_24;
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
            this.toolTipDialogs.SetToolTip(this.bDelNPC, resources.GetString("bDelNPC.ToolTip"));
            this.bDelNPC.UseVisualStyleBackColor = true;
            this.bDelNPC.Click += new System.EventHandler(this.bDelNPC_Click);
            // 
            // bAddNPC
            // 
            resources.ApplyResources(this.bAddNPC, "bAddNPC");
            this.bAddNPC.Name = "bAddNPC";
            this.toolTipDialogs.SetToolTip(this.bAddNPC, resources.GetString("bAddNPC.ToolTip"));
            this.bAddNPC.UseVisualStyleBackColor = true;
            this.bAddNPC.Click += new System.EventHandler(this.bAddNPC_Click);
            // 
            // menuMainControl
            // 
            resources.ApplyResources(this.menuMainControl, "menuMainControl");
            this.menuMainControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMain,
            this.menuSaveAll,
            this.menuExplorer,
            this.обновленияToolStripMenuItem,
            this.данныеToolStripMenuItem});
            this.menuMainControl.Name = "menuMainControl";
            this.menuMainControl.TabStop = true;
            this.toolTipDialogs.SetToolTip(this.menuMainControl, resources.GetString("menuMainControl.ToolTip"));
            // 
            // menuMain
            // 
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuSynchronize,
            this.menuStatistics,
            this.changeLanguageToolStripMenuItem,
            this.menuExit});
            this.menuMain.Name = "menuMain";
            // 
            // menuSettings
            // 
            resources.ApplyResources(this.menuSettings, "menuSettings");
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // menuSynchronize
            // 
            resources.ApplyResources(this.menuSynchronize, "menuSynchronize");
            this.menuSynchronize.Name = "menuSynchronize";
            this.menuSynchronize.Click += new System.EventHandler(this.SynchroToolStripMenuItem_Click);
            // 
            // menuStatistics
            // 
            resources.ApplyResources(this.menuStatistics, "menuStatistics");
            this.menuStatistics.Name = "menuStatistics";
            this.menuStatistics.Click += new System.EventHandler(this.StatisticsToolStripMenuItem_Click);
            // 
            // changeLanguageToolStripMenuItem
            // 
            resources.ApplyResources(this.changeLanguageToolStripMenuItem, "changeLanguageToolStripMenuItem");
            this.changeLanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.русскийToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.changeLanguageToolStripMenuItem.Name = "changeLanguageToolStripMenuItem";
            // 
            // русскийToolStripMenuItem
            // 
            resources.ApplyResources(this.русскийToolStripMenuItem, "русскийToolStripMenuItem");
            this.русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            this.русскийToolStripMenuItem.Click += new System.EventHandler(this.русскийToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // menuExit
            // 
            resources.ApplyResources(this.menuExit, "menuExit");
            this.menuExit.Name = "menuExit";
            this.menuExit.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // menuSaveAll
            // 
            resources.ApplyResources(this.menuSaveAll, "menuSaveAll");
            this.menuSaveAll.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.SaveDisk;
            this.menuSaveAll.Name = "menuSaveAll";
            this.menuSaveAll.Click += new System.EventHandler(this.SaveAllToolStripMenuItem_Click);
            // 
            // menuExplorer
            // 
            resources.ApplyResources(this.menuExplorer, "menuExplorer");
            this.menuExplorer.Image = global::StalkerOnlineQuesterEditor.Properties.Resources.Explorer;
            this.menuExplorer.Name = "menuExplorer";
            this.menuExplorer.Click += new System.EventHandler(this.ExplorerToolStripMenuItem_Click);
            // 
            // обновленияToolStripMenuItem
            // 
            resources.ApplyResources(this.обновленияToolStripMenuItem, "обновленияToolStripMenuItem");
            this.обновленияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.парсерыToolStripMenuItem,
            this.проверкаОшибокToolStripMenuItem});
            this.обновленияToolStripMenuItem.Name = "обновленияToolStripMenuItem";
            // 
            // парсерыToolStripMenuItem
            // 
            resources.ApplyResources(this.парсерыToolStripMenuItem, "парсерыToolStripMenuItem");
            this.парсерыToolStripMenuItem.Name = "парсерыToolStripMenuItem";
            this.парсерыToolStripMenuItem.Click += new System.EventHandler(this.парсерыToolStripMenuItem_Click);
            // 
            // проверкаОшибокToolStripMenuItem
            // 
            resources.ApplyResources(this.проверкаОшибокToolStripMenuItem, "проверкаОшибокToolStripMenuItem");
            this.проверкаОшибокToolStripMenuItem.Name = "проверкаОшибокToolStripMenuItem";
            this.проверкаОшибокToolStripMenuItem.Click += new System.EventHandler(this.проверкаОшибокToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            resources.ApplyResources(this.данныеToolStripMenuItem, "данныеToolStripMenuItem");
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.собратьЭдиторДляПередачиToolStripMenuItem,
            this.вытащитьНепереведённыеТекстыToolStripMenuItem,
            this.загрузитьПереводToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            // 
            // собратьЭдиторДляПередачиToolStripMenuItem
            // 
            resources.ApplyResources(this.собратьЭдиторДляПередачиToolStripMenuItem, "собратьЭдиторДляПередачиToolStripMenuItem");
            this.собратьЭдиторДляПередачиToolStripMenuItem.Name = "собратьЭдиторДляПередачиToolStripMenuItem";
            this.собратьЭдиторДляПередачиToolStripMenuItem.Click += new System.EventHandler(this.собратьЭдиторДляПередачиToolStripMenuItem_Click);
            // 
            // вытащитьНепереведённыеТекстыToolStripMenuItem
            // 
            resources.ApplyResources(this.вытащитьНепереведённыеТекстыToolStripMenuItem, "вытащитьНепереведённыеТекстыToolStripMenuItem");
            this.вытащитьНепереведённыеТекстыToolStripMenuItem.Name = "вытащитьНепереведённыеТекстыToolStripMenuItem";
            this.вытащитьНепереведённыеТекстыToolStripMenuItem.Click += new System.EventHandler(this.вытащитьНепереведённыеТекстыToolStripMenuItem_Click);
            // 
            // загрузитьПереводToolStripMenuItem
            // 
            resources.ApplyResources(this.загрузитьПереводToolStripMenuItem, "загрузитьПереводToolStripMenuItem");
            this.загрузитьПереводToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.диалоговToolStripMenuItem,
            this.квестовToolStripMenuItem});
            this.загрузитьПереводToolStripMenuItem.Name = "загрузитьПереводToolStripMenuItem";
            // 
            // диалоговToolStripMenuItem
            // 
            resources.ApplyResources(this.диалоговToolStripMenuItem, "диалоговToolStripMenuItem");
            this.диалоговToolStripMenuItem.Name = "диалоговToolStripMenuItem";
            this.диалоговToolStripMenuItem.Click += new System.EventHandler(this.диалоговToolStripMenuItem_Click);
            // 
            // квестовToolStripMenuItem
            // 
            resources.ApplyResources(this.квестовToolStripMenuItem, "квестовToolStripMenuItem");
            this.квестовToolStripMenuItem.Name = "квестовToolStripMenuItem";
            this.квестовToolStripMenuItem.Click += new System.EventHandler(this.квестовToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            this.statusStrip.Stretch = false;
            this.toolTipDialogs.SetToolTip(this.statusStrip, resources.GetString("statusStrip.ToolTip"));
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
            this.toolTipDialogs.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
            this.tabInfoNPC.ResumeLayout(false);
            this.panelNpcLinkControls.ResumeLayout(false);
            this.panelNpcLinkControls.PerformLayout();
            this.tabReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReview)).EndInit();
            this.panelReviewButtons.ResumeLayout(false);
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
        private PCanvas DialogShower;
        private System.Windows.Forms.SplitContainer splitDialogs;
        private System.Windows.Forms.GroupBox gbDialogsEditor;
        private System.Windows.Forms.Panel panelSelectNPC;
        private System.Windows.Forms.Panel panelDialogTools;
        private System.Windows.Forms.SplitContainer splitDialogsTreeAndCanvas;
        private System.Windows.Forms.TreeView treeDialogs;
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
        private System.Windows.Forms.Button bTestButton;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colNPCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDialogsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuestsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRussianName;
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
    }
}

