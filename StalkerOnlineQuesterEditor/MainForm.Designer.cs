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
            this.DialogShower = new UMD.HCIL.Piccolo.PCanvas();
            this.NPCBox = new System.Windows.Forms.ComboBox();
            this.labelChosenNPC = new System.Windows.Forms.Label();
            this.CentralDock = new System.Windows.Forms.TabControl();
            this.tabDialogs = new System.Windows.Forms.TabPage();
            this.splitDialogs = new System.Windows.Forms.SplitContainer();
            this.DialogsEditor = new System.Windows.Forms.GroupBox();
            this.GrafAndAllQuests = new System.Windows.Forms.SplitContainer();
            this.treeDialogs = new System.Windows.Forms.TreeView();
            this.bZoomIn = new System.Windows.Forms.Button();
            this.bZoomOut = new System.Windows.Forms.Button();
            this.DialogActions = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.bCenterizeDialogShower = new System.Windows.Forms.Button();
            this.labelYNode = new System.Windows.Forms.Label();
            this.labelXNode = new System.Windows.Forms.Label();
            this.bSaveDialogs = new System.Windows.Forms.Button();
            this.bRemoveDialog = new System.Windows.Forms.Button();
            this.bEditDialog = new System.Windows.Forms.Button();
            this.bAddDialog = new System.Windows.Forms.Button();
            this.EmulatorGroupBox = new System.Windows.Forms.GroupBox();
            this.EmulatorsplitContainer = new System.Windows.Forms.SplitContainer();
            this.tabQuests = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bPasteEvents = new System.Windows.Forms.Button();
            this.bCopyEvents = new System.Windows.Forms.Button();
            this.bQuestDown = new System.Windows.Forms.Button();
            this.bQuestUp = new System.Windows.Forms.Button();
            this.bSaveQuests = new System.Windows.Forms.Button();
            this.bRemoveEvent = new System.Windows.Forms.Button();
            this.bEditEvent = new System.Windows.Forms.Button();
            this.bAddEvent = new System.Windows.Forms.Button();
            this.splitQuestsContainer = new System.Windows.Forms.SplitContainer();
            this.treeQuest = new System.Windows.Forms.TreeView();
            this.labelQuestTree = new System.Windows.Forms.Label();
            this.treeQuestBuffer = new System.Windows.Forms.TreeView();
            this.labelBuffer = new System.Windows.Forms.Label();
            this.npcLinksTabPage = new System.Windows.Forms.TabPage();
            this.npcLinkShower = new UMD.HCIL.Piccolo.PCanvas();
            this.tabReview = new System.Windows.Forms.TabPage();
            this.gridViewReview = new System.Windows.Forms.DataGridView();
            this.colNPCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.сolDialogsNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.сolQuestsNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelReviewButtons = new System.Windows.Forms.Panel();
            this.labelReviewOutputed = new System.Windows.Forms.Label();
            this.gbNPC = new System.Windows.Forms.GroupBox();
            this.numQuests = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNumQuests = new System.Windows.Forms.CheckBox();
            this.numDialogs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNumDialogs = new System.Windows.Forms.CheckBox();
            this.bFindNPC = new System.Windows.Forms.Button();
            this.tabManage = new System.Windows.Forms.TabPage();
            this.manageGridView = new System.Windows.Forms.DataGridView();
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
            this.rewardReputation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeatPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Legend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.worked = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bSaveManage = new System.Windows.Forms.Button();
            this.tabTranslate = new System.Windows.Forms.TabPage();
            this.diffGridView = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npc_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cur_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.new_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_diff_locale = new System.Windows.Forms.Panel();
            this.labelLocalizeOuput = new System.Windows.Forms.Label();
            this.bSaveLocale = new System.Windows.Forms.Button();
            this.ActualCheckBox = new System.Windows.Forms.CheckBox();
            this.OutdatedCheckBox = new System.Windows.Forms.CheckBox();
            this.bFindQuestDifference = new System.Windows.Forms.Button();
            this.bFindDialogDifference = new System.Windows.Forms.Button();
            this.tabBalance = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bSaveBalance = new System.Windows.Forms.Button();
            this.balanceTabControl = new System.Windows.Forms.TabControl();
            this.fractionsBalanceTabPage = new System.Windows.Forms.TabPage();
            this.fractionBalanceDataGridView = new System.Windows.Forms.DataGridView();
            this.fraction_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fraction_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.penalty_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cat_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cat_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cat_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectNPC = new System.Windows.Forms.Panel();
            this.bRemoveQuest = new System.Windows.Forms.Button();
            this.bAddQuest = new System.Windows.Forms.Button();
            this.bDelNPC = new System.Windows.Forms.Button();
            this.bAddNPC = new System.Windows.Forms.Button();
            this.QuestBox = new System.Windows.Forms.ComboBox();
            this.labelChosenQuest = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusDialogStrip = new System.Windows.Forms.StatusStrip();
            this.pNpcLinkControls = new System.Windows.Forms.Panel();
            this.bNpcLinkExecute = new System.Windows.Forms.Button();
            this.lAdviceNpcLink = new System.Windows.Forms.Label();
            this.CentralDock.SuspendLayout();
            this.tabDialogs.SuspendLayout();
            this.splitDialogs.Panel1.SuspendLayout();
            this.splitDialogs.Panel2.SuspendLayout();
            this.splitDialogs.SuspendLayout();
            this.DialogsEditor.SuspendLayout();
            this.GrafAndAllQuests.Panel1.SuspendLayout();
            this.GrafAndAllQuests.Panel2.SuspendLayout();
            this.GrafAndAllQuests.SuspendLayout();
            this.DialogActions.SuspendLayout();
            this.EmulatorGroupBox.SuspendLayout();
            this.EmulatorsplitContainer.SuspendLayout();
            this.tabQuests.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitQuestsContainer.Panel1.SuspendLayout();
            this.splitQuestsContainer.SuspendLayout();
            this.npcLinksTabPage.SuspendLayout();
            this.tabReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReview)).BeginInit();
            this.panelReviewButtons.SuspendLayout();
            this.gbNPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDialogs)).BeginInit();
            this.tabManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manageGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabTranslate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diffGridView)).BeginInit();
            this.panel_diff_locale.SuspendLayout();
            this.tabBalance.SuspendLayout();
            this.panel3.SuspendLayout();
            this.balanceTabControl.SuspendLayout();
            this.fractionsBalanceTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fractionBalanceDataGridView)).BeginInit();
            this.SelectNPC.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusDialogStrip.SuspendLayout();
            this.pNpcLinkControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // DialogShower
            // 
            this.DialogShower.AllowDrop = true;
            this.DialogShower.BackColor = System.Drawing.Color.White;
            this.DialogShower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DialogShower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DialogShower.GridFitText = false;
            this.DialogShower.Location = new System.Drawing.Point(0, 0);
            this.DialogShower.Name = "DialogShower";
            this.DialogShower.RegionManagement = true;
            this.DialogShower.Size = new System.Drawing.Size(468, 399);
            this.DialogShower.TabIndex = 0;
            this.DialogShower.Text = "8";
            // 
            // NPCBox
            // 
            this.NPCBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.NPCBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.NPCBox.FormattingEnabled = true;
            this.NPCBox.Location = new System.Drawing.Point(96, 3);
            this.NPCBox.Name = "NPCBox";
            this.NPCBox.Size = new System.Drawing.Size(196, 21);
            this.NPCBox.TabIndex = 3;
            this.NPCBox.SelectedIndexChanged += new System.EventHandler(this.NPCBox_SelectedIndexChanged);
            // 
            // labelChosenNPC
            // 
            this.labelChosenNPC.AutoSize = true;
            this.labelChosenNPC.Location = new System.Drawing.Point(3, 6);
            this.labelChosenNPC.Name = "labelChosenNPC";
            this.labelChosenNPC.Size = new System.Drawing.Size(91, 13);
            this.labelChosenNPC.TabIndex = 4;
            this.labelChosenNPC.Text = "Выбранный NPC";
            // 
            // CentralDock
            // 
            this.CentralDock.Controls.Add(this.tabDialogs);
            this.CentralDock.Controls.Add(this.tabQuests);
            this.CentralDock.Controls.Add(this.npcLinksTabPage);
            this.CentralDock.Controls.Add(this.tabReview);
            this.CentralDock.Controls.Add(this.tabManage);
            this.CentralDock.Controls.Add(this.tabTranslate);
            this.CentralDock.Controls.Add(this.tabBalance);
            this.CentralDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentralDock.HotTrack = true;
            this.CentralDock.Location = new System.Drawing.Point(0, 53);
            this.CentralDock.Name = "CentralDock";
            this.CentralDock.SelectedIndex = 0;
            this.CentralDock.Size = new System.Drawing.Size(914, 566);
            this.CentralDock.TabIndex = 2;
            this.CentralDock.SelectedIndexChanged += new System.EventHandler(this.onSelectTab);
            // 
            // tabDialogs
            // 
            this.tabDialogs.Controls.Add(this.splitDialogs);
            this.tabDialogs.Location = new System.Drawing.Point(4, 22);
            this.tabDialogs.Name = "tabDialogs";
            this.tabDialogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabDialogs.Size = new System.Drawing.Size(906, 540);
            this.tabDialogs.TabIndex = 0;
            this.tabDialogs.Text = "Диалоги";
            this.tabDialogs.UseVisualStyleBackColor = true;
            // 
            // splitDialogs
            // 
            this.splitDialogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDialogs.Location = new System.Drawing.Point(3, 3);
            this.splitDialogs.Name = "splitDialogs";
            this.splitDialogs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitDialogs.Panel1
            // 
            this.splitDialogs.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitDialogs.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitDialogs.Panel1.Controls.Add(this.DialogsEditor);
            this.splitDialogs.Panel1MinSize = 10;
            // 
            // splitDialogs.Panel2
            // 
            this.splitDialogs.Panel2.Controls.Add(this.EmulatorGroupBox);
            this.splitDialogs.Panel2MinSize = 10;
            this.splitDialogs.Size = new System.Drawing.Size(900, 534);
            this.splitDialogs.SplitterDistance = 422;
            this.splitDialogs.TabIndex = 1;
            // 
            // DialogsEditor
            // 
            this.DialogsEditor.Controls.Add(this.GrafAndAllQuests);
            this.DialogsEditor.Controls.Add(this.DialogActions);
            this.DialogsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DialogsEditor.Location = new System.Drawing.Point(0, 0);
            this.DialogsEditor.Name = "DialogsEditor";
            this.DialogsEditor.Size = new System.Drawing.Size(900, 422);
            this.DialogsEditor.TabIndex = 1;
            this.DialogsEditor.TabStop = false;
            this.DialogsEditor.Text = "Правка диалогов";
            // 
            // GrafAndAllQuests
            // 
            this.GrafAndAllQuests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GrafAndAllQuests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrafAndAllQuests.Location = new System.Drawing.Point(3, 16);
            this.GrafAndAllQuests.Name = "GrafAndAllQuests";
            // 
            // GrafAndAllQuests.Panel1
            // 
            this.GrafAndAllQuests.Panel1.Controls.Add(this.treeDialogs);
            // 
            // GrafAndAllQuests.Panel2
            // 
            this.GrafAndAllQuests.Panel2.Controls.Add(this.bZoomIn);
            this.GrafAndAllQuests.Panel2.Controls.Add(this.bZoomOut);
            this.GrafAndAllQuests.Panel2.Controls.Add(this.DialogShower);
            this.GrafAndAllQuests.Size = new System.Drawing.Size(711, 403);
            this.GrafAndAllQuests.SplitterDistance = 235;
            this.GrafAndAllQuests.TabIndex = 2;
            // 
            // treeDialogs
            // 
            this.treeDialogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDialogs.Location = new System.Drawing.Point(0, 0);
            this.treeDialogs.Name = "treeDialogs";
            this.treeDialogs.Size = new System.Drawing.Size(231, 399);
            this.treeDialogs.TabIndex = 0;
            this.treeDialogs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDialogs_GotFocus);
            // 
            // bZoomIn
            // 
            this.bZoomIn.Dock = System.Windows.Forms.DockStyle.Right;
            this.bZoomIn.Location = new System.Drawing.Point(380, 0);
            this.bZoomIn.MaximumSize = new System.Drawing.Size(44, 21);
            this.bZoomIn.Name = "bZoomIn";
            this.bZoomIn.Size = new System.Drawing.Size(44, 21);
            this.bZoomIn.TabIndex = 2;
            this.bZoomIn.Text = "+";
            this.bZoomIn.UseVisualStyleBackColor = true;
            this.bZoomIn.Visible = false;
            // 
            // bZoomOut
            // 
            this.bZoomOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bZoomOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.bZoomOut.Location = new System.Drawing.Point(424, 0);
            this.bZoomOut.MaximumSize = new System.Drawing.Size(44, 21);
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(44, 21);
            this.bZoomOut.TabIndex = 1;
            this.bZoomOut.Text = "-";
            this.bZoomOut.UseVisualStyleBackColor = true;
            this.bZoomOut.Visible = false;
            // 
            // DialogActions
            // 
            this.DialogActions.AutoSize = true;
            this.DialogActions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DialogActions.Controls.Add(this.button1);
            this.DialogActions.Controls.Add(this.bCenterizeDialogShower);
            this.DialogActions.Controls.Add(this.labelYNode);
            this.DialogActions.Controls.Add(this.labelXNode);
            this.DialogActions.Controls.Add(this.bSaveDialogs);
            this.DialogActions.Controls.Add(this.bRemoveDialog);
            this.DialogActions.Controls.Add(this.bEditDialog);
            this.DialogActions.Controls.Add(this.bAddDialog);
            this.DialogActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.DialogActions.Location = new System.Drawing.Point(714, 16);
            this.DialogActions.Name = "DialogActions";
            this.DialogActions.Size = new System.Drawing.Size(183, 403);
            this.DialogActions.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Пробежать все";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bCenterizeDialogShower
            // 
            this.bCenterizeDialogShower.Location = new System.Drawing.Point(7, 242);
            this.bCenterizeDialogShower.Name = "bCenterizeDialogShower";
            this.bCenterizeDialogShower.Size = new System.Drawing.Size(172, 23);
            this.bCenterizeDialogShower.TabIndex = 6;
            this.bCenterizeDialogShower.Text = "Отцентрировать";
            this.bCenterizeDialogShower.UseVisualStyleBackColor = true;
            this.bCenterizeDialogShower.Click += new System.EventHandler(this.bCenterizeDialogShower_Click);
            // 
            // labelYNode
            // 
            this.labelYNode.AutoSize = true;
            this.labelYNode.Location = new System.Drawing.Point(26, 352);
            this.labelYNode.Name = "labelYNode";
            this.labelYNode.Size = new System.Drawing.Size(35, 13);
            this.labelYNode.TabIndex = 5;
            this.labelYNode.Text = "label4";
            // 
            // labelXNode
            // 
            this.labelXNode.AutoSize = true;
            this.labelXNode.Location = new System.Drawing.Point(26, 316);
            this.labelXNode.Name = "labelXNode";
            this.labelXNode.Size = new System.Drawing.Size(35, 13);
            this.labelXNode.TabIndex = 4;
            this.labelXNode.Text = "label3";
            // 
            // bSaveDialogs
            // 
            this.bSaveDialogs.Location = new System.Drawing.Point(7, 163);
            this.bSaveDialogs.Name = "bSaveDialogs";
            this.bSaveDialogs.Size = new System.Drawing.Size(173, 23);
            this.bSaveDialogs.TabIndex = 3;
            this.bSaveDialogs.Text = "Сохранить изменения";
            this.bSaveDialogs.UseVisualStyleBackColor = true;
            this.bSaveDialogs.Click += new System.EventHandler(this.bSaveDialogs_Click);
            // 
            // bRemoveDialog
            // 
            this.bRemoveDialog.Enabled = false;
            this.bRemoveDialog.Location = new System.Drawing.Point(6, 61);
            this.bRemoveDialog.Name = "bRemoveDialog";
            this.bRemoveDialog.Size = new System.Drawing.Size(173, 23);
            this.bRemoveDialog.TabIndex = 2;
            this.bRemoveDialog.Text = "Удалить узел/ветку";
            this.bRemoveDialog.UseVisualStyleBackColor = true;
            this.bRemoveDialog.Click += new System.EventHandler(this.bRemoveDialog_Click);
            // 
            // bEditDialog
            // 
            this.bEditDialog.Enabled = false;
            this.bEditDialog.Location = new System.Drawing.Point(6, 32);
            this.bEditDialog.Name = "bEditDialog";
            this.bEditDialog.Size = new System.Drawing.Size(173, 23);
            this.bEditDialog.TabIndex = 1;
            this.bEditDialog.Text = "Править узел диалога";
            this.bEditDialog.UseVisualStyleBackColor = true;
            this.bEditDialog.Click += new System.EventHandler(this.bEditDialog_Click);
            // 
            // bAddDialog
            // 
            this.bAddDialog.Enabled = false;
            this.bAddDialog.Location = new System.Drawing.Point(6, 3);
            this.bAddDialog.Name = "bAddDialog";
            this.bAddDialog.Size = new System.Drawing.Size(173, 23);
            this.bAddDialog.TabIndex = 0;
            this.bAddDialog.Text = "Добавить узел диалога";
            this.bAddDialog.UseVisualStyleBackColor = true;
            this.bAddDialog.Click += new System.EventHandler(this.bAddDialog_Click);
            // 
            // EmulatorGroupBox
            // 
            this.EmulatorGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EmulatorGroupBox.Controls.Add(this.EmulatorsplitContainer);
            this.EmulatorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmulatorGroupBox.Location = new System.Drawing.Point(0, 0);
            this.EmulatorGroupBox.Name = "EmulatorGroupBox";
            this.EmulatorGroupBox.Size = new System.Drawing.Size(900, 108);
            this.EmulatorGroupBox.TabIndex = 0;
            this.EmulatorGroupBox.TabStop = false;
            this.EmulatorGroupBox.Text = "Эмулятор";
            // 
            // EmulatorsplitContainer
            // 
            this.EmulatorsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmulatorsplitContainer.Location = new System.Drawing.Point(3, 16);
            this.EmulatorsplitContainer.Name = "EmulatorsplitContainer";
            // 
            // EmulatorsplitContainer.Panel2
            // 
            this.EmulatorsplitContainer.Panel2.AutoScroll = true;
            this.EmulatorsplitContainer.Size = new System.Drawing.Size(894, 89);
            this.EmulatorsplitContainer.SplitterDistance = 150;
            this.EmulatorsplitContainer.TabIndex = 0;
            // 
            // tabQuests
            // 
            this.tabQuests.Controls.Add(this.panel1);
            this.tabQuests.Controls.Add(this.splitQuestsContainer);
            this.tabQuests.Location = new System.Drawing.Point(4, 22);
            this.tabQuests.Name = "tabQuests";
            this.tabQuests.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuests.Size = new System.Drawing.Size(906, 540);
            this.tabQuests.TabIndex = 1;
            this.tabQuests.Text = "Квесты";
            this.tabQuests.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bPasteEvents);
            this.panel1.Controls.Add(this.bCopyEvents);
            this.panel1.Controls.Add(this.bQuestDown);
            this.panel1.Controls.Add(this.bQuestUp);
            this.panel1.Controls.Add(this.bSaveQuests);
            this.panel1.Controls.Add(this.bRemoveEvent);
            this.panel1.Controls.Add(this.bEditEvent);
            this.panel1.Controls.Add(this.bAddEvent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(703, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 534);
            this.panel1.TabIndex = 0;
            // 
            // bPasteEvents
            // 
            this.bPasteEvents.Enabled = false;
            this.bPasteEvents.Location = new System.Drawing.Point(4, 216);
            this.bPasteEvents.Name = "bPasteEvents";
            this.bPasteEvents.Size = new System.Drawing.Size(191, 23);
            this.bPasteEvents.TabIndex = 7;
            this.bPasteEvents.Text = "Вставить";
            this.bPasteEvents.UseVisualStyleBackColor = true;
            this.bPasteEvents.Click += new System.EventHandler(this.bPasteEvents_Click);
            // 
            // bCopyEvents
            // 
            this.bCopyEvents.Enabled = false;
            this.bCopyEvents.Location = new System.Drawing.Point(4, 187);
            this.bCopyEvents.Name = "bCopyEvents";
            this.bCopyEvents.Size = new System.Drawing.Size(191, 23);
            this.bCopyEvents.TabIndex = 6;
            this.bCopyEvents.Text = "Копировать";
            this.bCopyEvents.UseVisualStyleBackColor = true;
            this.bCopyEvents.Click += new System.EventHandler(this.bCopyEvents_Click);
            // 
            // bQuestDown
            // 
            this.bQuestDown.Enabled = false;
            this.bQuestDown.Location = new System.Drawing.Point(4, 120);
            this.bQuestDown.Name = "bQuestDown";
            this.bQuestDown.Size = new System.Drawing.Size(193, 23);
            this.bQuestDown.TabIndex = 5;
            this.bQuestDown.Text = "Вниз";
            this.bQuestDown.UseVisualStyleBackColor = true;
            this.bQuestDown.Click += new System.EventHandler(this.bQuestDown_Click);
            // 
            // bQuestUp
            // 
            this.bQuestUp.Enabled = false;
            this.bQuestUp.Location = new System.Drawing.Point(4, 91);
            this.bQuestUp.Name = "bQuestUp";
            this.bQuestUp.Size = new System.Drawing.Size(193, 23);
            this.bQuestUp.TabIndex = 4;
            this.bQuestUp.Text = "Вверх";
            this.bQuestUp.UseVisualStyleBackColor = true;
            this.bQuestUp.Click += new System.EventHandler(this.bQuestUp_Click);
            // 
            // bSaveQuests
            // 
            this.bSaveQuests.Location = new System.Drawing.Point(7, 281);
            this.bSaveQuests.Name = "bSaveQuests";
            this.bSaveQuests.Size = new System.Drawing.Size(193, 23);
            this.bSaveQuests.TabIndex = 3;
            this.bSaveQuests.Text = "Сохранить изменения";
            this.bSaveQuests.UseVisualStyleBackColor = true;
            this.bSaveQuests.Click += new System.EventHandler(this.bSaveQuests_Click);
            // 
            // bRemoveEvent
            // 
            this.bRemoveEvent.Enabled = false;
            this.bRemoveEvent.Location = new System.Drawing.Point(4, 62);
            this.bRemoveEvent.Name = "bRemoveEvent";
            this.bRemoveEvent.Size = new System.Drawing.Size(193, 23);
            this.bRemoveEvent.TabIndex = 2;
            this.bRemoveEvent.Text = "Удалить событие";
            this.bRemoveEvent.UseVisualStyleBackColor = true;
            this.bRemoveEvent.Click += new System.EventHandler(this.bRemoveEvent_Click);
            // 
            // bEditEvent
            // 
            this.bEditEvent.Enabled = false;
            this.bEditEvent.Location = new System.Drawing.Point(3, 32);
            this.bEditEvent.Name = "bEditEvent";
            this.bEditEvent.Size = new System.Drawing.Size(194, 23);
            this.bEditEvent.TabIndex = 1;
            this.bEditEvent.Text = "Править событие";
            this.bEditEvent.UseVisualStyleBackColor = true;
            this.bEditEvent.Click += new System.EventHandler(this.bEditEvent_Click);
            // 
            // bAddEvent
            // 
            this.bAddEvent.Enabled = false;
            this.bAddEvent.Location = new System.Drawing.Point(3, 3);
            this.bAddEvent.Name = "bAddEvent";
            this.bAddEvent.Size = new System.Drawing.Size(194, 23);
            this.bAddEvent.TabIndex = 0;
            this.bAddEvent.Text = "Добавить событие";
            this.bAddEvent.UseVisualStyleBackColor = true;
            this.bAddEvent.Click += new System.EventHandler(this.bAddEvent_Click);
            // 
            // splitQuestsContainer
            // 
            this.splitQuestsContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitQuestsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitQuestsContainer.Location = new System.Drawing.Point(3, 3);
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
            this.splitQuestsContainer.Panel2.AutoScroll = true;
            this.splitQuestsContainer.Size = new System.Drawing.Size(900, 534);
            this.splitQuestsContainer.SplitterDistance = 299;
            this.splitQuestsContainer.TabIndex = 0;
            // 
            // treeQuest
            // 
            this.treeQuest.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeQuest.Location = new System.Drawing.Point(0, 192);
            this.treeQuest.Name = "treeQuest";
            this.treeQuest.Size = new System.Drawing.Size(295, 530);
            this.treeQuest.TabIndex = 0;
            this.treeQuest.DoubleClick += new System.EventHandler(this.treeQuestClicked);
            this.treeQuest.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuest_AfterSelect);
            this.treeQuest.Leave += new System.EventHandler(this.treeQuest_Leave);
            this.treeQuest.Click += new System.EventHandler(this.treeQuest_Click);
            // 
            // labelQuestTree
            // 
            this.labelQuestTree.AutoSize = true;
            this.labelQuestTree.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelQuestTree.Location = new System.Drawing.Point(0, 179);
            this.labelQuestTree.Name = "labelQuestTree";
            this.labelQuestTree.Size = new System.Drawing.Size(87, 13);
            this.labelQuestTree.TabIndex = 3;
            this.labelQuestTree.Text = "Дерево квеста:";
            // 
            // treeQuestBuffer
            // 
            this.treeQuestBuffer.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeQuestBuffer.Location = new System.Drawing.Point(0, 13);
            this.treeQuestBuffer.Name = "treeQuestBuffer";
            this.treeQuestBuffer.Size = new System.Drawing.Size(295, 166);
            this.treeQuestBuffer.TabIndex = 1;
            this.treeQuestBuffer.DoubleClick += new System.EventHandler(this.treeQuestBuffer_DoubleClick);
            this.treeQuestBuffer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeQuestBuffer_AfterSelect);
            this.treeQuestBuffer.Click += new System.EventHandler(this.treeQuestBuffer_Click);
            // 
            // labelBuffer
            // 
            this.labelBuffer.AutoSize = true;
            this.labelBuffer.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBuffer.Location = new System.Drawing.Point(0, 0);
            this.labelBuffer.Name = "labelBuffer";
            this.labelBuffer.Size = new System.Drawing.Size(50, 13);
            this.labelBuffer.TabIndex = 2;
            this.labelBuffer.Text = "Буффер:";
            // 
            // npcLinksTabPage
            // 
            this.npcLinksTabPage.Controls.Add(this.npcLinkShower);
            this.npcLinksTabPage.Controls.Add(this.pNpcLinkControls);
            this.npcLinksTabPage.Location = new System.Drawing.Point(4, 22);
            this.npcLinksTabPage.Name = "npcLinksTabPage";
            this.npcLinksTabPage.Size = new System.Drawing.Size(906, 540);
            this.npcLinksTabPage.TabIndex = 2;
            this.npcLinksTabPage.Text = "Связи NPC";
            this.npcLinksTabPage.UseVisualStyleBackColor = true;
            // 
            // npcLinkShower
            // 
            this.npcLinkShower.AllowDrop = true;
            this.npcLinkShower.BackColor = System.Drawing.Color.White;
            this.npcLinkShower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.npcLinkShower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.npcLinkShower.GridFitText = false;
            this.npcLinkShower.Location = new System.Drawing.Point(0, 52);
            this.npcLinkShower.Name = "npcLinkShower";
            this.npcLinkShower.RegionManagement = true;
            this.npcLinkShower.Size = new System.Drawing.Size(906, 488);
            this.npcLinkShower.TabIndex = 1;
            this.npcLinkShower.Text = "8";
            // 
            // tabReview
            // 
            this.tabReview.Controls.Add(this.gridViewReview);
            this.tabReview.Controls.Add(this.panelReviewButtons);
            this.tabReview.Location = new System.Drawing.Point(4, 22);
            this.tabReview.Name = "tabReview";
            this.tabReview.Size = new System.Drawing.Size(906, 540);
            this.tabReview.TabIndex = 3;
            this.tabReview.Text = "Проверки";
            this.tabReview.UseVisualStyleBackColor = true;
            // 
            // gridViewReview
            // 
            this.gridViewReview.AllowUserToAddRows = false;
            this.gridViewReview.AllowUserToDeleteRows = false;
            this.gridViewReview.AllowUserToOrderColumns = true;
            this.gridViewReview.AllowUserToResizeRows = false;
            this.gridViewReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewReview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNPCName,
            this.сolDialogsNum,
            this.сolQuestsNum,
            this.colLocation});
            this.gridViewReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewReview.Location = new System.Drawing.Point(0, 104);
            this.gridViewReview.Name = "gridViewReview";
            this.gridViewReview.Size = new System.Drawing.Size(906, 436);
            this.gridViewReview.TabIndex = 1;
            // 
            // colNPCName
            // 
            this.colNPCName.HeaderText = "Имя NPC";
            this.colNPCName.Name = "colNPCName";
            // 
            // сolDialogsNum
            // 
            this.сolDialogsNum.HeaderText = "Диалоги";
            this.сolDialogsNum.Name = "сolDialogsNum";
            // 
            // сolQuestsNum
            // 
            this.сolQuestsNum.HeaderText = "Квесты";
            this.сolQuestsNum.Name = "сolQuestsNum";
            // 
            // colLocation
            // 
            this.colLocation.HeaderText = "Карта";
            this.colLocation.Name = "colLocation";
            // 
            // panelReviewButtons
            // 
            this.panelReviewButtons.Controls.Add(this.labelReviewOutputed);
            this.panelReviewButtons.Controls.Add(this.gbNPC);
            this.panelReviewButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReviewButtons.Location = new System.Drawing.Point(0, 0);
            this.panelReviewButtons.Name = "panelReviewButtons";
            this.panelReviewButtons.Size = new System.Drawing.Size(906, 104);
            this.panelReviewButtons.TabIndex = 0;
            // 
            // labelReviewOutputed
            // 
            this.labelReviewOutputed.AutoSize = true;
            this.labelReviewOutputed.Location = new System.Drawing.Point(782, 83);
            this.labelReviewOutputed.Name = "labelReviewOutputed";
            this.labelReviewOutputed.Size = new System.Drawing.Size(0, 13);
            this.labelReviewOutputed.TabIndex = 1;
            // 
            // gbNPC
            // 
            this.gbNPC.Controls.Add(this.numQuests);
            this.gbNPC.Controls.Add(this.label2);
            this.gbNPC.Controls.Add(this.cbNumQuests);
            this.gbNPC.Controls.Add(this.numDialogs);
            this.gbNPC.Controls.Add(this.label1);
            this.gbNPC.Controls.Add(this.cbNumDialogs);
            this.gbNPC.Controls.Add(this.bFindNPC);
            this.gbNPC.Location = new System.Drawing.Point(3, 4);
            this.gbNPC.Name = "gbNPC";
            this.gbNPC.Size = new System.Drawing.Size(272, 100);
            this.gbNPC.TabIndex = 0;
            this.gbNPC.TabStop = false;
            this.gbNPC.Text = "NPC";
            // 
            // numQuests
            // 
            this.numQuests.Location = new System.Drawing.Point(175, 72);
            this.numQuests.Name = "numQuests";
            this.numQuests.Size = new System.Drawing.Size(62, 20);
            this.numQuests.TabIndex = 6;
            this.numQuests.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "меньше";
            // 
            // cbNumQuests
            // 
            this.cbNumQuests.AutoSize = true;
            this.cbNumQuests.Location = new System.Drawing.Point(7, 72);
            this.cbNumQuests.Name = "cbNumQuests";
            this.cbNumQuests.Size = new System.Drawing.Size(102, 17);
            this.cbNumQuests.TabIndex = 4;
            this.cbNumQuests.Text = "Число квестов";
            this.cbNumQuests.UseVisualStyleBackColor = true;
            // 
            // numDialogs
            // 
            this.numDialogs.Location = new System.Drawing.Point(175, 48);
            this.numDialogs.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numDialogs.Name = "numDialogs";
            this.numDialogs.Size = new System.Drawing.Size(62, 20);
            this.numDialogs.TabIndex = 3;
            this.numDialogs.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "меньше";
            // 
            // cbNumDialogs
            // 
            this.cbNumDialogs.AutoSize = true;
            this.cbNumDialogs.Location = new System.Drawing.Point(7, 49);
            this.cbNumDialogs.Name = "cbNumDialogs";
            this.cbNumDialogs.Size = new System.Drawing.Size(108, 17);
            this.cbNumDialogs.TabIndex = 1;
            this.cbNumDialogs.Text = "Число диалогов";
            this.cbNumDialogs.UseVisualStyleBackColor = true;
            // 
            // bFindNPC
            // 
            this.bFindNPC.Location = new System.Drawing.Point(7, 19);
            this.bFindNPC.Name = "bFindNPC";
            this.bFindNPC.Size = new System.Drawing.Size(98, 23);
            this.bFindNPC.TabIndex = 0;
            this.bFindNPC.Text = "Найти NPC";
            this.bFindNPC.UseVisualStyleBackColor = true;
            this.bFindNPC.Click += new System.EventHandler(this.bFindNPC_Click);
            // 
            // tabManage
            // 
            this.tabManage.Controls.Add(this.manageGridView);
            this.tabManage.Controls.Add(this.panel2);
            this.tabManage.Location = new System.Drawing.Point(4, 22);
            this.tabManage.Name = "tabManage";
            this.tabManage.Size = new System.Drawing.Size(906, 540);
            this.tabManage.TabIndex = 4;
            this.tabManage.Text = "Управление";
            this.tabManage.UseVisualStyleBackColor = true;
            // 
            // manageGridView
            // 
            this.manageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.manageGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.rewardReputation,
            this.repeat,
            this.repeatPeriod,
            this.Level,
            this.author,
            this.Legend,
            this.worked});
            this.manageGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manageGridView.Location = new System.Drawing.Point(0, 0);
            this.manageGridView.Name = "manageGridView";
            this.manageGridView.Size = new System.Drawing.Size(906, 514);
            this.manageGridView.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // subevents
            // 
            this.subevents.HeaderText = "Вложенные события";
            this.subevents.Name = "subevents";
            this.subevents.ReadOnly = true;
            // 
            // title
            // 
            this.title.HeaderText = "Заголовок";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Описание";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // npcName
            // 
            this.npcName.HeaderText = "NPC";
            this.npcName.Name = "npcName";
            this.npcName.ReadOnly = true;
            // 
            // subNPC
            // 
            this.subNPC.HeaderText = "Список NPC";
            this.subNPC.Name = "subNPC";
            this.subNPC.ReadOnly = true;
            // 
            // dialogID
            // 
            this.dialogID.HeaderText = "id диалога";
            this.dialogID.Name = "dialogID";
            this.dialogID.ReadOnly = true;
            // 
            // rewardBattle
            // 
            this.rewardBattle.HeaderText = "Награда опыт-бой";
            this.rewardBattle.Name = "rewardBattle";
            this.rewardBattle.ReadOnly = true;
            // 
            // rewardSurvive
            // 
            this.rewardSurvive.HeaderText = "Награда опыт-выживание";
            this.rewardSurvive.Name = "rewardSurvive";
            this.rewardSurvive.ReadOnly = true;
            // 
            // rewardSupport
            // 
            this.rewardSupport.HeaderText = "Награда опыт-поддержка";
            this.rewardSupport.Name = "rewardSupport";
            this.rewardSupport.ReadOnly = true;
            // 
            // rewardCredits
            // 
            this.rewardCredits.HeaderText = "Награда деньги";
            this.rewardCredits.Name = "rewardCredits";
            this.rewardCredits.ReadOnly = true;
            // 
            // rewardItems
            // 
            this.rewardItems.HeaderText = "Награда вещи";
            this.rewardItems.Name = "rewardItems";
            this.rewardItems.ReadOnly = true;
            // 
            // rewardReputation
            // 
            this.rewardReputation.HeaderText = "Награда репутация";
            this.rewardReputation.Name = "rewardReputation";
            this.rewardReputation.ReadOnly = true;
            // 
            // repeat
            // 
            this.repeat.HeaderText = "Повторяемый";
            this.repeat.Name = "repeat";
            this.repeat.ReadOnly = true;
            // 
            // repeatPeriod
            // 
            this.repeatPeriod.HeaderText = "Период повтора (часы)";
            this.repeatPeriod.Name = "repeatPeriod";
            this.repeatPeriod.ReadOnly = true;
            // 
            // Level
            // 
            this.Level.HeaderText = "Уровень ( комментарий)";
            this.Level.Name = "Level";
            // 
            // author
            // 
            this.author.HeaderText = "Автор ( комментарий)";
            this.author.Name = "author";
            // 
            // Legend
            // 
            this.Legend.HeaderText = "Легенда ( комментарий)";
            this.Legend.Name = "Legend";
            // 
            // worked
            // 
            this.worked.HeaderText = "Рабочий";
            this.worked.Name = "worked";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bSaveManage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 514);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(906, 26);
            this.panel2.TabIndex = 1;
            // 
            // bSaveManage
            // 
            this.bSaveManage.Dock = System.Windows.Forms.DockStyle.Right;
            this.bSaveManage.Location = new System.Drawing.Point(831, 0);
            this.bSaveManage.Name = "bSaveManage";
            this.bSaveManage.Size = new System.Drawing.Size(75, 26);
            this.bSaveManage.TabIndex = 0;
            this.bSaveManage.Text = "Сохранить";
            this.bSaveManage.UseVisualStyleBackColor = true;
            this.bSaveManage.Click += new System.EventHandler(this.bSaveManage_Click);
            // 
            // tabTranslate
            // 
            this.tabTranslate.Controls.Add(this.diffGridView);
            this.tabTranslate.Controls.Add(this.panel_diff_locale);
            this.tabTranslate.Location = new System.Drawing.Point(4, 22);
            this.tabTranslate.Name = "tabTranslate";
            this.tabTranslate.Size = new System.Drawing.Size(906, 540);
            this.tabTranslate.TabIndex = 5;
            this.tabTranslate.Text = "Переводы";
            this.tabTranslate.UseVisualStyleBackColor = true;
            // 
            // diffGridView
            // 
            this.diffGridView.AllowUserToAddRows = false;
            this.diffGridView.AllowUserToDeleteRows = false;
            this.diffGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.diffGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.npc_name,
            this.identif,
            this.cur_ver,
            this.new_ver});
            this.diffGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffGridView.Location = new System.Drawing.Point(0, 57);
            this.diffGridView.Name = "diffGridView";
            this.diffGridView.ReadOnly = true;
            this.diffGridView.Size = new System.Drawing.Size(906, 483);
            this.diffGridView.TabIndex = 1;
            this.diffGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.diffGridView_CellDoubleClick);
            // 
            // type
            // 
            this.type.HeaderText = "Тип";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // npc_name
            // 
            this.npc_name.HeaderText = "Имя NPC";
            this.npc_name.Name = "npc_name";
            this.npc_name.ReadOnly = true;
            // 
            // identif
            // 
            this.identif.HeaderText = "ID";
            this.identif.Name = "identif";
            this.identif.ReadOnly = true;
            // 
            // cur_ver
            // 
            this.cur_ver.HeaderText = "Текущая версия";
            this.cur_ver.Name = "cur_ver";
            this.cur_ver.ReadOnly = true;
            // 
            // new_ver
            // 
            this.new_ver.HeaderText = "Новая версия";
            this.new_ver.Name = "new_ver";
            this.new_ver.ReadOnly = true;
            // 
            // panel_diff_locale
            // 
            this.panel_diff_locale.Controls.Add(this.labelLocalizeOuput);
            this.panel_diff_locale.Controls.Add(this.bSaveLocale);
            this.panel_diff_locale.Controls.Add(this.ActualCheckBox);
            this.panel_diff_locale.Controls.Add(this.OutdatedCheckBox);
            this.panel_diff_locale.Controls.Add(this.bFindQuestDifference);
            this.panel_diff_locale.Controls.Add(this.bFindDialogDifference);
            this.panel_diff_locale.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_diff_locale.Location = new System.Drawing.Point(0, 0);
            this.panel_diff_locale.Name = "panel_diff_locale";
            this.panel_diff_locale.Size = new System.Drawing.Size(906, 57);
            this.panel_diff_locale.TabIndex = 0;
            // 
            // labelLocalizeOuput
            // 
            this.labelLocalizeOuput.AutoSize = true;
            this.labelLocalizeOuput.Location = new System.Drawing.Point(693, 19);
            this.labelLocalizeOuput.Name = "labelLocalizeOuput";
            this.labelLocalizeOuput.Size = new System.Drawing.Size(0, 13);
            this.labelLocalizeOuput.TabIndex = 5;
            // 
            // bSaveLocale
            // 
            this.bSaveLocale.Location = new System.Drawing.Point(802, 8);
            this.bSaveLocale.Name = "bSaveLocale";
            this.bSaveLocale.Size = new System.Drawing.Size(88, 40);
            this.bSaveLocale.TabIndex = 4;
            this.bSaveLocale.Text = "Сохранить";
            this.bSaveLocale.UseVisualStyleBackColor = true;
            this.bSaveLocale.Click += new System.EventHandler(this.bSaveLocale_Click);
            // 
            // ActualCheckBox
            // 
            this.ActualCheckBox.AutoSize = true;
            this.ActualCheckBox.Location = new System.Drawing.Point(359, 9);
            this.ActualCheckBox.Name = "ActualCheckBox";
            this.ActualCheckBox.Size = new System.Drawing.Size(87, 17);
            this.ActualCheckBox.TabIndex = 3;
            this.ActualCheckBox.Text = "Актуальные";
            this.ActualCheckBox.UseVisualStyleBackColor = true;
            // 
            // OutdatedCheckBox
            // 
            this.OutdatedCheckBox.AutoSize = true;
            this.OutdatedCheckBox.Checked = true;
            this.OutdatedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OutdatedCheckBox.Location = new System.Drawing.Point(359, 32);
            this.OutdatedCheckBox.Name = "OutdatedCheckBox";
            this.OutdatedCheckBox.Size = new System.Drawing.Size(89, 17);
            this.OutdatedCheckBox.TabIndex = 2;
            this.OutdatedCheckBox.Text = "Устаревшие";
            this.OutdatedCheckBox.UseVisualStyleBackColor = true;
            // 
            // bFindQuestDifference
            // 
            this.bFindQuestDifference.Location = new System.Drawing.Point(195, 9);
            this.bFindQuestDifference.Name = "bFindQuestDifference";
            this.bFindQuestDifference.Size = new System.Drawing.Size(154, 23);
            this.bFindQuestDifference.TabIndex = 1;
            this.bFindQuestDifference.Text = "Найти квесты";
            this.bFindQuestDifference.UseVisualStyleBackColor = true;
            this.bFindQuestDifference.Click += new System.EventHandler(this.bFindQuestDifference_Click);
            // 
            // bFindDialogDifference
            // 
            this.bFindDialogDifference.Location = new System.Drawing.Point(35, 9);
            this.bFindDialogDifference.Name = "bFindDialogDifference";
            this.bFindDialogDifference.Size = new System.Drawing.Size(154, 23);
            this.bFindDialogDifference.TabIndex = 0;
            this.bFindDialogDifference.Text = "Найти диалоги";
            this.bFindDialogDifference.UseVisualStyleBackColor = true;
            this.bFindDialogDifference.Click += new System.EventHandler(this.bFindDialogDifference_Click);
            // 
            // tabBalance
            // 
            this.tabBalance.Controls.Add(this.panel3);
            this.tabBalance.Controls.Add(this.balanceTabControl);
            this.tabBalance.Location = new System.Drawing.Point(4, 22);
            this.tabBalance.Name = "tabBalance";
            this.tabBalance.Size = new System.Drawing.Size(906, 540);
            this.tabBalance.TabIndex = 6;
            this.tabBalance.Text = "Баланс";
            this.tabBalance.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bSaveBalance);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 514);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(906, 26);
            this.panel3.TabIndex = 2;
            // 
            // bSaveBalance
            // 
            this.bSaveBalance.Dock = System.Windows.Forms.DockStyle.Right;
            this.bSaveBalance.Location = new System.Drawing.Point(831, 0);
            this.bSaveBalance.Name = "bSaveBalance";
            this.bSaveBalance.Size = new System.Drawing.Size(75, 26);
            this.bSaveBalance.TabIndex = 0;
            this.bSaveBalance.Text = "Сохранить";
            this.bSaveBalance.UseVisualStyleBackColor = true;
            this.bSaveBalance.Click += new System.EventHandler(this.bSaveBalance_Click);
            // 
            // balanceTabControl
            // 
            this.balanceTabControl.Controls.Add(this.fractionsBalanceTabPage);
            this.balanceTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.balanceTabControl.Location = new System.Drawing.Point(0, 0);
            this.balanceTabControl.Name = "balanceTabControl";
            this.balanceTabControl.SelectedIndex = 0;
            this.balanceTabControl.Size = new System.Drawing.Size(906, 540);
            this.balanceTabControl.TabIndex = 0;
            // 
            // fractionsBalanceTabPage
            // 
            this.fractionsBalanceTabPage.Controls.Add(this.fractionBalanceDataGridView);
            this.fractionsBalanceTabPage.Location = new System.Drawing.Point(4, 22);
            this.fractionsBalanceTabPage.Name = "fractionsBalanceTabPage";
            this.fractionsBalanceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.fractionsBalanceTabPage.Size = new System.Drawing.Size(898, 514);
            this.fractionsBalanceTabPage.TabIndex = 0;
            this.fractionsBalanceTabPage.Text = "Опыт фракций";
            this.fractionsBalanceTabPage.UseVisualStyleBackColor = true;
            // 
            // fractionBalanceDataGridView
            // 
            this.fractionBalanceDataGridView.AllowUserToAddRows = false;
            this.fractionBalanceDataGridView.AllowUserToDeleteRows = false;
            this.fractionBalanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fractionBalanceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fraction_id,
            this.fraction_name,
            this.penalty_name,
            this.limit,
            this.cat_1,
            this.cat_2,
            this.cat_3});
            this.fractionBalanceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fractionBalanceDataGridView.Location = new System.Drawing.Point(3, 3);
            this.fractionBalanceDataGridView.Name = "fractionBalanceDataGridView";
            this.fractionBalanceDataGridView.Size = new System.Drawing.Size(892, 508);
            this.fractionBalanceDataGridView.TabIndex = 0;
            // 
            // fraction_id
            // 
            this.fraction_id.HeaderText = "id";
            this.fraction_id.Name = "fraction_id";
            this.fraction_id.Visible = false;
            // 
            // fraction_name
            // 
            this.fraction_name.HeaderText = "Имя фракции";
            this.fraction_name.Name = "fraction_name";
            this.fraction_name.ReadOnly = true;
            // 
            // penalty_name
            // 
            this.penalty_name.HeaderText = "Штрафы/Бонусы к фракциям NPC";
            this.penalty_name.Name = "penalty_name";
            this.penalty_name.ReadOnly = true;
            // 
            // limit
            // 
            this.limit.HeaderText = "Суточный лимит";
            this.limit.Name = "limit";
            // 
            // cat_1
            // 
            this.cat_1.HeaderText = "Категория 1";
            this.cat_1.Name = "cat_1";
            // 
            // cat_2
            // 
            this.cat_2.HeaderText = "Категория 2";
            this.cat_2.Name = "cat_2";
            // 
            // cat_3
            // 
            this.cat_3.HeaderText = "Категория 3";
            this.cat_3.Name = "cat_3";
            // 
            // SelectNPC
            // 
            this.SelectNPC.Controls.Add(this.bRemoveQuest);
            this.SelectNPC.Controls.Add(this.bAddQuest);
            this.SelectNPC.Controls.Add(this.bDelNPC);
            this.SelectNPC.Controls.Add(this.bAddNPC);
            this.SelectNPC.Controls.Add(this.QuestBox);
            this.SelectNPC.Controls.Add(this.labelChosenQuest);
            this.SelectNPC.Controls.Add(this.labelChosenNPC);
            this.SelectNPC.Controls.Add(this.NPCBox);
            this.SelectNPC.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectNPC.Location = new System.Drawing.Point(0, 24);
            this.SelectNPC.Name = "SelectNPC";
            this.SelectNPC.Size = new System.Drawing.Size(914, 29);
            this.SelectNPC.TabIndex = 0;
            // 
            // bRemoveQuest
            // 
            this.bRemoveQuest.Enabled = false;
            this.bRemoveQuest.Location = new System.Drawing.Point(854, 1);
            this.bRemoveQuest.Name = "bRemoveQuest";
            this.bRemoveQuest.Size = new System.Drawing.Size(50, 23);
            this.bRemoveQuest.TabIndex = 10;
            this.bRemoveQuest.Text = "Удал.";
            this.bRemoveQuest.UseVisualStyleBackColor = true;
            this.bRemoveQuest.Click += new System.EventHandler(this.bRemoveQuest_Click);
            // 
            // bAddQuest
            // 
            this.bAddQuest.Enabled = false;
            this.bAddQuest.Location = new System.Drawing.Point(806, 1);
            this.bAddQuest.Name = "bAddQuest";
            this.bAddQuest.Size = new System.Drawing.Size(42, 23);
            this.bAddQuest.TabIndex = 9;
            this.bAddQuest.Text = "Доб.";
            this.bAddQuest.UseVisualStyleBackColor = true;
            this.bAddQuest.Click += new System.EventHandler(this.bAddQuest_Click);
            // 
            // bDelNPC
            // 
            this.bDelNPC.Location = new System.Drawing.Point(345, 2);
            this.bDelNPC.Name = "bDelNPC";
            this.bDelNPC.Size = new System.Drawing.Size(52, 23);
            this.bDelNPC.TabIndex = 8;
            this.bDelNPC.Text = "Удал.";
            this.bDelNPC.UseVisualStyleBackColor = true;
            this.bDelNPC.Click += new System.EventHandler(this.bDelNPC_Click);
            // 
            // bAddNPC
            // 
            this.bAddNPC.Location = new System.Drawing.Point(298, 2);
            this.bAddNPC.Name = "bAddNPC";
            this.bAddNPC.Size = new System.Drawing.Size(42, 23);
            this.bAddNPC.TabIndex = 7;
            this.bAddNPC.Text = "Доб.";
            this.bAddNPC.UseVisualStyleBackColor = true;
            this.bAddNPC.Click += new System.EventHandler(this.bAddNPC_Click);
            // 
            // QuestBox
            // 
            this.QuestBox.Enabled = false;
            this.QuestBox.FormattingEnabled = true;
            this.QuestBox.Location = new System.Drawing.Point(507, 2);
            this.QuestBox.Name = "QuestBox";
            this.QuestBox.Size = new System.Drawing.Size(293, 21);
            this.QuestBox.TabIndex = 6;
            this.QuestBox.SelectedIndexChanged += new System.EventHandler(this.QuestBox_SelectedIndexChanged);
            // 
            // labelChosenQuest
            // 
            this.labelChosenQuest.AutoSize = true;
            this.labelChosenQuest.Location = new System.Drawing.Point(403, 5);
            this.labelChosenQuest.Name = "labelChosenQuest";
            this.labelChosenQuest.Size = new System.Drawing.Size(98, 13);
            this.labelChosenQuest.TabIndex = 5;
            this.labelChosenQuest.Text = "Выбранный квест";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.StatisticsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(914, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.SettingsToolStripMenuItem.Text = "Настройка оператора";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // StatisticsToolStripMenuItem
            // 
            this.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem";
            this.StatisticsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.StatisticsToolStripMenuItem.Text = "Статистика";
            this.StatisticsToolStripMenuItem.Click += new System.EventHandler(this.StatisticsToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusDialogStrip
            // 
            this.statusDialogStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusDialogStrip.Location = new System.Drawing.Point(0, 619);
            this.statusDialogStrip.Name = "statusDialogStrip";
            this.statusDialogStrip.Size = new System.Drawing.Size(914, 22);
            this.statusDialogStrip.SizingGrip = false;
            this.statusDialogStrip.Stretch = false;
            this.statusDialogStrip.TabIndex = 1;
            // 
            // pNpcLinkControls
            // 
            this.pNpcLinkControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNpcLinkControls.Controls.Add(this.lAdviceNpcLink);
            this.pNpcLinkControls.Controls.Add(this.bNpcLinkExecute);
            this.pNpcLinkControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pNpcLinkControls.Location = new System.Drawing.Point(0, 0);
            this.pNpcLinkControls.Name = "pNpcLinkControls";
            this.pNpcLinkControls.Size = new System.Drawing.Size(906, 52);
            this.pNpcLinkControls.TabIndex = 2;
            // 
            // bNpcLinkExecute
            // 
            this.bNpcLinkExecute.Location = new System.Drawing.Point(261, 15);
            this.bNpcLinkExecute.Name = "bNpcLinkExecute";
            this.bNpcLinkExecute.Size = new System.Drawing.Size(107, 23);
            this.bNpcLinkExecute.TabIndex = 0;
            this.bNpcLinkExecute.Text = "Посмотреть";
            this.bNpcLinkExecute.UseVisualStyleBackColor = true;
            this.bNpcLinkExecute.Click += new System.EventHandler(this.bNpcLinkExecute_Click);
            // 
            // lAdviceNpcLink
            // 
            this.lAdviceNpcLink.AutoSize = true;
            this.lAdviceNpcLink.Location = new System.Drawing.Point(7, 20);
            this.lAdviceNpcLink.Name = "lAdviceNpcLink";
            this.lAdviceNpcLink.Size = new System.Drawing.Size(250, 13);
            this.lAdviceNpcLink.TabIndex = 1;
            this.lAdviceNpcLink.Text = "Выберете NPC в комбобоксе вверху и нажмите";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 641);
            this.Controls.Add(this.CentralDock);
            this.Controls.Add(this.SelectNPC);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusDialogStrip);
            this.Name = "MainForm";
            this.CentralDock.ResumeLayout(false);
            this.tabDialogs.ResumeLayout(false);
            this.splitDialogs.Panel1.ResumeLayout(false);
            this.splitDialogs.Panel2.ResumeLayout(false);
            this.splitDialogs.ResumeLayout(false);
            this.DialogsEditor.ResumeLayout(false);
            this.DialogsEditor.PerformLayout();
            this.GrafAndAllQuests.Panel1.ResumeLayout(false);
            this.GrafAndAllQuests.Panel2.ResumeLayout(false);
            this.GrafAndAllQuests.ResumeLayout(false);
            this.DialogActions.ResumeLayout(false);
            this.DialogActions.PerformLayout();
            this.EmulatorGroupBox.ResumeLayout(false);
            this.EmulatorsplitContainer.ResumeLayout(false);
            this.tabQuests.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitQuestsContainer.Panel1.ResumeLayout(false);
            this.splitQuestsContainer.Panel1.PerformLayout();
            this.splitQuestsContainer.ResumeLayout(false);
            this.npcLinksTabPage.ResumeLayout(false);
            this.tabReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReview)).EndInit();
            this.panelReviewButtons.ResumeLayout(false);
            this.panelReviewButtons.PerformLayout();
            this.gbNPC.ResumeLayout(false);
            this.gbNPC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDialogs)).EndInit();
            this.tabManage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.manageGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabTranslate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diffGridView)).EndInit();
            this.panel_diff_locale.ResumeLayout(false);
            this.panel_diff_locale.PerformLayout();
            this.tabBalance.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.balanceTabControl.ResumeLayout(false);
            this.fractionsBalanceTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fractionBalanceDataGridView)).EndInit();
            this.SelectNPC.ResumeLayout(false);
            this.SelectNPC.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusDialogStrip.ResumeLayout(false);
            this.statusDialogStrip.PerformLayout();
            this.pNpcLinkControls.ResumeLayout(false);
            this.pNpcLinkControls.PerformLayout();
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
        private System.Windows.Forms.GroupBox DialogsEditor;
        private System.Windows.Forms.Panel SelectNPC;
        private System.Windows.Forms.Panel DialogActions;
        private System.Windows.Forms.SplitContainer GrafAndAllQuests;
        private System.Windows.Forms.TreeView treeDialogs;
        private System.Windows.Forms.Button bAddDialog;
        private System.Windows.Forms.Button bEditDialog;
        private System.Windows.Forms.Button bRemoveDialog;
        private System.Windows.Forms.GroupBox EmulatorGroupBox;
        private System.Windows.Forms.SplitContainer EmulatorsplitContainer;
        private System.Windows.Forms.SplitContainer splitQuestsContainer;
        private System.Windows.Forms.TreeView treeQuest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox QuestBox;
        private System.Windows.Forms.Label labelChosenQuest;
        private System.Windows.Forms.Button bDelNPC;
        private System.Windows.Forms.Button bAddNPC;
        private System.Windows.Forms.Button bRemoveQuest;
        private System.Windows.Forms.Button bAddQuest;
        private System.Windows.Forms.Button bZoomOut;
        private System.Windows.Forms.Button bZoomIn;
        private System.Windows.Forms.Button bSaveDialogs;
        private System.Windows.Forms.Button bSaveQuests;
        private System.Windows.Forms.Button bRemoveEvent;
        private System.Windows.Forms.Button bEditEvent;
        private System.Windows.Forms.Button bAddEvent;
        private System.Windows.Forms.Button bQuestDown;
        private System.Windows.Forms.Button bQuestUp;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.TabPage npcLinksTabPage;
        private PCanvas npcLinkShower;
        private System.Windows.Forms.TabPage tabManage;
        private System.Windows.Forms.DataGridView manageGridView;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusDialogStrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bSaveManage;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn rewardReputation;
        private System.Windows.Forms.DataGridViewTextBoxColumn repeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn repeatPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Legend;
        private System.Windows.Forms.DataGridViewComboBoxColumn worked;
        private System.Windows.Forms.TreeView treeQuestBuffer;
        private System.Windows.Forms.Button bCopyEvents;
        private System.Windows.Forms.Label labelQuestTree;
        private System.Windows.Forms.Label labelBuffer;
        private System.Windows.Forms.Button bPasteEvents;
        private System.Windows.Forms.TabPage tabTranslate;
        private System.Windows.Forms.Panel panel_diff_locale;
        private System.Windows.Forms.Button bFindDialogDifference;
        private System.Windows.Forms.DataGridView diffGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn npc_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn identif;
        private System.Windows.Forms.DataGridViewTextBoxColumn cur_ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn new_ver;
        private System.Windows.Forms.Button bFindQuestDifference;
        private System.Windows.Forms.TabPage tabBalance;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bSaveBalance;
        private System.Windows.Forms.TabControl balanceTabControl;
        private System.Windows.Forms.TabPage fractionsBalanceTabPage;
        private System.Windows.Forms.DataGridView fractionBalanceDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn fraction_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fraction_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn penalty_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cat_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cat_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cat_3;
        private System.Windows.Forms.CheckBox ActualCheckBox;
        private System.Windows.Forms.CheckBox OutdatedCheckBox;
        private System.Windows.Forms.Button bSaveLocale;
        private System.Windows.Forms.Label labelLocalizeOuput;
        public System.Windows.Forms.TabControl CentralDock;
        private System.Windows.Forms.Label labelYNode;
        private System.Windows.Forms.Label labelXNode;
        private System.Windows.Forms.Button bCenterizeDialogShower;
        private System.Windows.Forms.ToolStripMenuItem StatisticsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabReview;
        private System.Windows.Forms.Panel panelReviewButtons;
        private System.Windows.Forms.GroupBox gbNPC;
        private System.Windows.Forms.NumericUpDown numDialogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbNumDialogs;
        private System.Windows.Forms.Button bFindNPC;
        private System.Windows.Forms.DataGridView gridViewReview;
        private System.Windows.Forms.NumericUpDown numQuests;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbNumQuests;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelReviewOutputed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNPCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn сolDialogsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn сolQuestsNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.Panel pNpcLinkControls;
        private System.Windows.Forms.Button bNpcLinkExecute;
        private System.Windows.Forms.Label lAdviceNpcLink;


    }
}

