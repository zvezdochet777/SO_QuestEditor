namespace StalkerOnlineQuesterEditor
{
    partial class EditDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tPlayerText = new System.Windows.Forms.TextBox();
            this.lAnswerText = new System.Windows.Forms.Label();
            this.NPCSaidIs = new System.Windows.Forms.Label();
            this.lGreetNPC = new System.Windows.Forms.Label();
            this.lAttention = new System.Windows.Forms.Label();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.cbFailQuests = new System.Windows.Forms.CheckBox();
            this.cbCancelQuests = new System.Windows.Forms.CheckBox();
            this.tbFailQuests = new System.Windows.Forms.MaskedTextBox();
            this.tbCancelQuests = new System.Windows.Forms.MaskedTextBox();
            this.ActionsComboBox = new System.Windows.Forms.ComboBox();
            this.teleportComboBox = new System.Windows.Forms.ComboBox();
            this.tbCompleteQuests = new System.Windows.Forms.MaskedTextBox();
            this.tbGetQuests = new System.Windows.Forms.MaskedTextBox();
            this.ToDialogComboBox = new System.Windows.Forms.ComboBox();
            this.cbExit = new System.Windows.Forms.CheckBox();
            this.cbCompleteQuests = new System.Windows.Forms.CheckBox();
            this.cbGetQuests = new System.Windows.Forms.CheckBox();
            this.actionsCheckBox = new System.Windows.Forms.CheckBox();
            this.tReactionNPC = new System.Windows.Forms.TextBox();
            this.tNodes = new System.Windows.Forms.MaskedTextBox();
            this.lNodes = new System.Windows.Forms.Label();
            this.gbPrecondition = new System.Windows.Forms.GroupBox();
            this.tabQuestsCircs = new System.Windows.Forms.TabControl();
            this.tabQuests = new System.Windows.Forms.TabPage();
            this.gbQuestCondition = new System.Windows.Forms.GroupBox();
            this.lMassQuests = new System.Windows.Forms.Label();
            this.tShouldntHaveMassQuests = new System.Windows.Forms.MaskedTextBox();
            this.tMustHaveMassQuests = new System.Windows.Forms.MaskedTextBox();
            this.lFailedQuests = new System.Windows.Forms.Label();
            this.tShouldntHaveFailedQuests = new System.Windows.Forms.MaskedTextBox();
            this.tMustHaveFailedQuests = new System.Windows.Forms.MaskedTextBox();
            this.lShouldntHaveQuests = new System.Windows.Forms.Label();
            this.lNecessaryQuests = new System.Windows.Forms.Label();
            this.lCompletedQuests = new System.Windows.Forms.Label();
            this.lOnTestQuests = new System.Windows.Forms.Label();
            this.lOpenedQuests = new System.Windows.Forms.Label();
            this.tMustHaveQuestsOnTest = new System.Windows.Forms.MaskedTextBox();
            this.tShouldntHaveCompletedQuests = new System.Windows.Forms.MaskedTextBox();
            this.tMustHaveOpenQuests = new System.Windows.Forms.MaskedTextBox();
            this.tShouldntHaveQuestsOnTest = new System.Windows.Forms.MaskedTextBox();
            this.tShouldntHaveOpenQuests = new System.Windows.Forms.MaskedTextBox();
            this.tMustHaveCompletedQuests = new System.Windows.Forms.MaskedTextBox();
            this.tabReputation = new System.Windows.Forms.TabPage();
            this.lInfo = new System.Windows.Forms.Label();
            this.dataReputation = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabKarma = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.bTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.aTextBox = new System.Windows.Forms.MaskedTextBox();
            this.a = new System.Windows.Forms.Label();
            this.tabClan = new System.Windows.Forms.TabPage();
            this.gbClanOptions = new System.Windows.Forms.GroupBox();
            this.cbLonerOnly = new System.Windows.Forms.CheckBox();
            this.cbAnyClanOnly = new System.Windows.Forms.CheckBox();
            this.cbSameClanOnly = new System.Windows.Forms.CheckBox();
            this.cbShowClanOptions = new System.Windows.Forms.CheckBox();
            this.mtbPlayerLevel = new System.Windows.Forms.MaskedTextBox();
            this.lPlayerLevel = new System.Windows.Forms.Label();
            this.bKarma = new System.Windows.Forms.Button();
            this.bReputation = new System.Windows.Forms.Button();
            this.bEditDialogOk = new System.Windows.Forms.Button();
            this.bEditDialogCancel = new System.Windows.Forms.Button();
            this.lReactionNPC = new System.Windows.Forms.Label();
            this.pCommands = new System.Windows.Forms.Panel();
            this.debuglabel = new System.Windows.Forms.Label();
            this.debugTextBox = new System.Windows.Forms.MaskedTextBox();
            this.gbTexts = new System.Windows.Forms.GroupBox();
            this.gbActions.SuspendLayout();
            this.gbPrecondition.SuspendLayout();
            this.tabQuestsCircs.SuspendLayout();
            this.tabQuests.SuspendLayout();
            this.gbQuestCondition.SuspendLayout();
            this.tabReputation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReputation)).BeginInit();
            this.tabKarma.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabClan.SuspendLayout();
            this.gbClanOptions.SuspendLayout();
            this.pCommands.SuspendLayout();
            this.gbTexts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tPlayerText
            // 
            this.tPlayerText.Location = new System.Drawing.Point(103, 46);
            this.tPlayerText.Name = "tPlayerText";
            this.tPlayerText.Size = new System.Drawing.Size(479, 20);
            this.tPlayerText.TabIndex = 0;
            this.tPlayerText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tPlayerText_KeyPress);
            // 
            // lAnswerText
            // 
            this.lAnswerText.AutoSize = true;
            this.lAnswerText.Location = new System.Drawing.Point(3, 46);
            this.lAnswerText.Name = "lAnswerText";
            this.lAnswerText.Size = new System.Drawing.Size(86, 13);
            this.lAnswerText.TabIndex = 1;
            this.lAnswerText.Text = "Вариант ответа";
            // 
            // NPCSaidIs
            // 
            this.NPCSaidIs.AutoSize = true;
            this.NPCSaidIs.Location = new System.Drawing.Point(107, 9);
            this.NPCSaidIs.Name = "NPCSaidIs";
            this.NPCSaidIs.Size = new System.Drawing.Size(13, 13);
            this.NPCSaidIs.TabIndex = 11;
            this.NPCSaidIs.Text = "  ";
            // 
            // lGreetNPC
            // 
            this.lGreetNPC.AutoSize = true;
            this.lGreetNPC.Location = new System.Drawing.Point(3, 9);
            this.lGreetNPC.Name = "lGreetNPC";
            this.lGreetNPC.Size = new System.Drawing.Size(98, 13);
            this.lGreetNPC.TabIndex = 12;
            this.lGreetNPC.Text = "Приветствие NPC";
            // 
            // lAttention
            // 
            this.lAttention.AutoSize = true;
            this.lAttention.ForeColor = System.Drawing.Color.DarkRed;
            this.lAttention.Location = new System.Drawing.Point(107, 78);
            this.lAttention.Name = "lAttention";
            this.lAttention.Size = new System.Drawing.Size(51, 13);
            this.lAttention.TabIndex = 13;
            this.lAttention.Text = "lAttention";
            // 
            // gbActions
            // 
            this.gbActions.AutoSize = true;
            this.gbActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbActions.Controls.Add(this.cbFailQuests);
            this.gbActions.Controls.Add(this.cbCancelQuests);
            this.gbActions.Controls.Add(this.tbFailQuests);
            this.gbActions.Controls.Add(this.tbCancelQuests);
            this.gbActions.Controls.Add(this.ActionsComboBox);
            this.gbActions.Controls.Add(this.teleportComboBox);
            this.gbActions.Controls.Add(this.tbCompleteQuests);
            this.gbActions.Controls.Add(this.tbGetQuests);
            this.gbActions.Controls.Add(this.ToDialogComboBox);
            this.gbActions.Controls.Add(this.cbExit);
            this.gbActions.Controls.Add(this.cbCompleteQuests);
            this.gbActions.Controls.Add(this.cbGetQuests);
            this.gbActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbActions.Enabled = false;
            this.gbActions.Location = new System.Drawing.Point(0, 500);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(590, 144);
            this.gbActions.TabIndex = 8;
            this.gbActions.TabStop = false;
            // 
            // cbFailQuests
            // 
            this.cbFailQuests.AutoSize = true;
            this.cbFailQuests.Location = new System.Drawing.Point(296, 44);
            this.cbFailQuests.Name = "cbFailQuests";
            this.cbFailQuests.Size = new System.Drawing.Size(121, 17);
            this.cbFailQuests.TabIndex = 39;
            this.cbFailQuests.Text = "Провалить квесты";
            this.cbFailQuests.UseVisualStyleBackColor = true;
            this.cbFailQuests.CheckedChanged += new System.EventHandler(this.cbFailQuests_CheckedChanged);
            // 
            // cbCancelQuests
            // 
            this.cbCancelQuests.AutoSize = true;
            this.cbCancelQuests.Location = new System.Drawing.Point(296, 24);
            this.cbCancelQuests.Name = "cbCancelQuests";
            this.cbCancelQuests.Size = new System.Drawing.Size(116, 17);
            this.cbCancelQuests.TabIndex = 38;
            this.cbCancelQuests.Text = "Отменить квесты";
            this.cbCancelQuests.UseVisualStyleBackColor = true;
            this.cbCancelQuests.CheckedChanged += new System.EventHandler(this.cbCancelQuests_CheckedChanged);
            // 
            // tbFailQuests
            // 
            this.tbFailQuests.Enabled = false;
            this.tbFailQuests.Location = new System.Drawing.Point(426, 44);
            this.tbFailQuests.Name = "tbFailQuests";
            this.tbFailQuests.Size = new System.Drawing.Size(147, 20);
            this.tbFailQuests.TabIndex = 37;
            // 
            // tbCancelQuests
            // 
            this.tbCancelQuests.Enabled = false;
            this.tbCancelQuests.Location = new System.Drawing.Point(426, 22);
            this.tbCancelQuests.Name = "tbCancelQuests";
            this.tbCancelQuests.Size = new System.Drawing.Size(147, 20);
            this.tbCancelQuests.TabIndex = 36;
            // 
            // ActionsComboBox
            // 
            this.ActionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionsComboBox.FormattingEnabled = true;
            this.ActionsComboBox.Location = new System.Drawing.Point(31, 76);
            this.ActionsComboBox.Name = "ActionsComboBox";
            this.ActionsComboBox.Size = new System.Drawing.Size(160, 21);
            this.ActionsComboBox.TabIndex = 35;
            this.ActionsComboBox.SelectedValueChanged += new System.EventHandler(this.ActionsComboBox_SelectedValueChanged);
            // 
            // teleportComboBox
            // 
            this.teleportComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.teleportComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.teleportComboBox.DropDownWidth = 250;
            this.teleportComboBox.FormattingEnabled = true;
            this.teleportComboBox.Location = new System.Drawing.Point(222, 76);
            this.teleportComboBox.Name = "teleportComboBox";
            this.teleportComboBox.Size = new System.Drawing.Size(147, 21);
            this.teleportComboBox.TabIndex = 30;
            // 
            // tbCompleteQuests
            // 
            this.tbCompleteQuests.Enabled = false;
            this.tbCompleteQuests.Location = new System.Drawing.Point(136, 44);
            this.tbCompleteQuests.Name = "tbCompleteQuests";
            this.tbCompleteQuests.Size = new System.Drawing.Size(147, 20);
            this.tbCompleteQuests.TabIndex = 28;
            // 
            // tbGetQuests
            // 
            this.tbGetQuests.Enabled = false;
            this.tbGetQuests.Location = new System.Drawing.Point(136, 22);
            this.tbGetQuests.Name = "tbGetQuests";
            this.tbGetQuests.Size = new System.Drawing.Size(147, 20);
            this.tbGetQuests.TabIndex = 26;
            // 
            // ToDialogComboBox
            // 
            this.ToDialogComboBox.FormattingEnabled = true;
            this.ToDialogComboBox.Location = new System.Drawing.Point(222, 76);
            this.ToDialogComboBox.Name = "ToDialogComboBox";
            this.ToDialogComboBox.Size = new System.Drawing.Size(147, 21);
            this.ToDialogComboBox.TabIndex = 18;
            // 
            // cbExit
            // 
            this.cbExit.AutoSize = true;
            this.cbExit.Location = new System.Drawing.Point(31, 108);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(141, 17);
            this.cbExit.TabIndex = 24;
            this.cbExit.Text = "Закрыть окно диалога";
            this.cbExit.UseVisualStyleBackColor = true;
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // cbCompleteQuests
            // 
            this.cbCompleteQuests.AutoSize = true;
            this.cbCompleteQuests.Location = new System.Drawing.Point(16, 44);
            this.cbCompleteQuests.Name = "cbCompleteQuests";
            this.cbCompleteQuests.Size = new System.Drawing.Size(119, 17);
            this.cbCompleteQuests.TabIndex = 27;
            this.cbCompleteQuests.Text = "Закончить квесты";
            this.cbCompleteQuests.UseVisualStyleBackColor = true;
            this.cbCompleteQuests.CheckedChanged += new System.EventHandler(this.cbCompleteQuests_CheckedChanged);
            // 
            // cbGetQuests
            // 
            this.cbGetQuests.AutoSize = true;
            this.cbGetQuests.Location = new System.Drawing.Point(16, 22);
            this.cbGetQuests.Name = "cbGetQuests";
            this.cbGetQuests.Size = new System.Drawing.Size(96, 17);
            this.cbGetQuests.TabIndex = 25;
            this.cbGetQuests.Text = "Взять квесты";
            this.cbGetQuests.UseVisualStyleBackColor = true;
            this.cbGetQuests.CheckedChanged += new System.EventHandler(this.cbGetQuests_CheckedChanged);
            // 
            // actionsCheckBox
            // 
            this.actionsCheckBox.AutoSize = true;
            this.actionsCheckBox.Location = new System.Drawing.Point(6, 498);
            this.actionsCheckBox.Name = "actionsCheckBox";
            this.actionsCheckBox.Size = new System.Drawing.Size(76, 17);
            this.actionsCheckBox.TabIndex = 16;
            this.actionsCheckBox.Text = "Действия";
            this.actionsCheckBox.UseVisualStyleBackColor = true;
            this.actionsCheckBox.CheckedChanged += new System.EventHandler(this.actionsCheckBox_CheckedChanged);
            // 
            // tReactionNPC
            // 
            this.tReactionNPC.Location = new System.Drawing.Point(103, 103);
            this.tReactionNPC.Multiline = true;
            this.tReactionNPC.Name = "tReactionNPC";
            this.tReactionNPC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tReactionNPC.Size = new System.Drawing.Size(479, 88);
            this.tReactionNPC.TabIndex = 1;
            // 
            // tNodes
            // 
            this.tNodes.Location = new System.Drawing.Point(103, 197);
            this.tNodes.Name = "tNodes";
            this.tNodes.Size = new System.Drawing.Size(479, 20);
            this.tNodes.TabIndex = 2;
            // 
            // lNodes
            // 
            this.lNodes.AutoSize = true;
            this.lNodes.Location = new System.Drawing.Point(3, 204);
            this.lNodes.Name = "lNodes";
            this.lNodes.Size = new System.Drawing.Size(68, 13);
            this.lNodes.TabIndex = 9;
            this.lNodes.Text = "Поддиалоги";
            // 
            // gbPrecondition
            // 
            this.gbPrecondition.Controls.Add(this.tabQuestsCircs);
            this.gbPrecondition.Controls.Add(this.mtbPlayerLevel);
            this.gbPrecondition.Controls.Add(this.lPlayerLevel);
            this.gbPrecondition.Controls.Add(this.bKarma);
            this.gbPrecondition.Controls.Add(this.bReputation);
            this.gbPrecondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPrecondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbPrecondition.Location = new System.Drawing.Point(0, 227);
            this.gbPrecondition.Name = "gbPrecondition";
            this.gbPrecondition.Size = new System.Drawing.Size(590, 273);
            this.gbPrecondition.TabIndex = 4;
            this.gbPrecondition.TabStop = false;
            this.gbPrecondition.Text = "Условия активности узла";
            // 
            // tabQuestsCircs
            // 
            this.tabQuestsCircs.Controls.Add(this.tabQuests);
            this.tabQuestsCircs.Controls.Add(this.tabReputation);
            this.tabQuestsCircs.Controls.Add(this.tabKarma);
            this.tabQuestsCircs.Controls.Add(this.tabClan);
            this.tabQuestsCircs.Location = new System.Drawing.Point(8, 19);
            this.tabQuestsCircs.Name = "tabQuestsCircs";
            this.tabQuestsCircs.SelectedIndex = 0;
            this.tabQuestsCircs.Size = new System.Drawing.Size(574, 219);
            this.tabQuestsCircs.TabIndex = 52;
            // 
            // tabQuests
            // 
            this.tabQuests.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabQuests.Controls.Add(this.gbQuestCondition);
            this.tabQuests.Location = new System.Drawing.Point(4, 22);
            this.tabQuests.Name = "tabQuests";
            this.tabQuests.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuests.Size = new System.Drawing.Size(566, 193);
            this.tabQuests.TabIndex = 0;
            this.tabQuests.Text = "Квесты";
            // 
            // gbQuestCondition
            // 
            this.gbQuestCondition.Controls.Add(this.lMassQuests);
            this.gbQuestCondition.Controls.Add(this.tShouldntHaveMassQuests);
            this.gbQuestCondition.Controls.Add(this.tMustHaveMassQuests);
            this.gbQuestCondition.Controls.Add(this.lFailedQuests);
            this.gbQuestCondition.Controls.Add(this.tShouldntHaveFailedQuests);
            this.gbQuestCondition.Controls.Add(this.tMustHaveFailedQuests);
            this.gbQuestCondition.Controls.Add(this.lShouldntHaveQuests);
            this.gbQuestCondition.Controls.Add(this.lNecessaryQuests);
            this.gbQuestCondition.Controls.Add(this.lCompletedQuests);
            this.gbQuestCondition.Controls.Add(this.lOnTestQuests);
            this.gbQuestCondition.Controls.Add(this.lOpenedQuests);
            this.gbQuestCondition.Controls.Add(this.tMustHaveQuestsOnTest);
            this.gbQuestCondition.Controls.Add(this.tShouldntHaveCompletedQuests);
            this.gbQuestCondition.Controls.Add(this.tMustHaveOpenQuests);
            this.gbQuestCondition.Controls.Add(this.tShouldntHaveQuestsOnTest);
            this.gbQuestCondition.Controls.Add(this.tShouldntHaveOpenQuests);
            this.gbQuestCondition.Controls.Add(this.tMustHaveCompletedQuests);
            this.gbQuestCondition.Location = new System.Drawing.Point(6, 6);
            this.gbQuestCondition.Name = "gbQuestCondition";
            this.gbQuestCondition.Size = new System.Drawing.Size(469, 181);
            this.gbQuestCondition.TabIndex = 6;
            this.gbQuestCondition.TabStop = false;
            this.gbQuestCondition.Text = "Состояния квестов";
            // 
            // lMassQuests
            // 
            this.lMassQuests.AutoSize = true;
            this.lMassQuests.Location = new System.Drawing.Point(6, 149);
            this.lMassQuests.Name = "lMassQuests";
            this.lMassQuests.Size = new System.Drawing.Size(100, 13);
            this.lMassQuests.TabIndex = 16;
            this.lMassQuests.Text = "Массовые квесты";
            // 
            // tShouldntHaveMassQuests
            // 
            this.tShouldntHaveMassQuests.Location = new System.Drawing.Point(234, 145);
            this.tShouldntHaveMassQuests.Name = "tShouldntHaveMassQuests";
            this.tShouldntHaveMassQuests.Size = new System.Drawing.Size(100, 20);
            this.tShouldntHaveMassQuests.TabIndex = 15;
            // 
            // tMustHaveMassQuests
            // 
            this.tMustHaveMassQuests.Location = new System.Drawing.Point(128, 145);
            this.tMustHaveMassQuests.Name = "tMustHaveMassQuests";
            this.tMustHaveMassQuests.Size = new System.Drawing.Size(100, 20);
            this.tMustHaveMassQuests.TabIndex = 14;
            // 
            // lFailedQuests
            // 
            this.lFailedQuests.AutoSize = true;
            this.lFailedQuests.Location = new System.Drawing.Point(6, 123);
            this.lFailedQuests.Name = "lFailedQuests";
            this.lFailedQuests.Size = new System.Drawing.Size(117, 13);
            this.lFailedQuests.TabIndex = 13;
            this.lFailedQuests.Text = "Проваленные квесты";
            // 
            // tShouldntHaveFailedQuests
            // 
            this.tShouldntHaveFailedQuests.Location = new System.Drawing.Point(234, 119);
            this.tShouldntHaveFailedQuests.Name = "tShouldntHaveFailedQuests";
            this.tShouldntHaveFailedQuests.Size = new System.Drawing.Size(100, 20);
            this.tShouldntHaveFailedQuests.TabIndex = 10;
            // 
            // tMustHaveFailedQuests
            // 
            this.tMustHaveFailedQuests.Location = new System.Drawing.Point(128, 119);
            this.tMustHaveFailedQuests.Name = "tMustHaveFailedQuests";
            this.tMustHaveFailedQuests.Size = new System.Drawing.Size(100, 20);
            this.tMustHaveFailedQuests.TabIndex = 6;
            // 
            // lShouldntHaveQuests
            // 
            this.lShouldntHaveQuests.AutoSize = true;
            this.lShouldntHaveQuests.Location = new System.Drawing.Point(227, 16);
            this.lShouldntHaveQuests.Name = "lShouldntHaveQuests";
            this.lShouldntHaveQuests.Size = new System.Drawing.Size(90, 13);
            this.lShouldntHaveQuests.TabIndex = 10;
            this.lShouldntHaveQuests.Text = "Не должно быть";
            // 
            // lNecessaryQuests
            // 
            this.lNecessaryQuests.AutoSize = true;
            this.lNecessaryQuests.Location = new System.Drawing.Point(140, 16);
            this.lNecessaryQuests.Name = "lNecessaryQuests";
            this.lNecessaryQuests.Size = new System.Drawing.Size(76, 13);
            this.lNecessaryQuests.TabIndex = 9;
            this.lNecessaryQuests.Text = "Должно быть";
            // 
            // lCompletedQuests
            // 
            this.lCompletedQuests.AutoSize = true;
            this.lCompletedQuests.Location = new System.Drawing.Point(6, 97);
            this.lCompletedQuests.Name = "lCompletedQuests";
            this.lCompletedQuests.Size = new System.Drawing.Size(99, 13);
            this.lCompletedQuests.TabIndex = 8;
            this.lCompletedQuests.Text = "Закрытые квесты";
            // 
            // lOnTestQuests
            // 
            this.lOnTestQuests.AutoSize = true;
            this.lOnTestQuests.Location = new System.Drawing.Point(6, 72);
            this.lOnTestQuests.Name = "lOnTestQuests";
            this.lOnTestQuests.Size = new System.Drawing.Size(115, 13);
            this.lOnTestQuests.TabIndex = 7;
            this.lOnTestQuests.Text = "Квесты \"к проверке\"";
            // 
            // lOpenedQuests
            // 
            this.lOpenedQuests.AutoSize = true;
            this.lOpenedQuests.Location = new System.Drawing.Point(6, 44);
            this.lOpenedQuests.Name = "lOpenedQuests";
            this.lOpenedQuests.Size = new System.Drawing.Size(99, 13);
            this.lOpenedQuests.TabIndex = 6;
            this.lOpenedQuests.Text = "Открытые квесты";
            // 
            // tMustHaveQuestsOnTest
            // 
            this.tMustHaveQuestsOnTest.Location = new System.Drawing.Point(128, 66);
            this.tMustHaveQuestsOnTest.Name = "tMustHaveQuestsOnTest";
            this.tMustHaveQuestsOnTest.Size = new System.Drawing.Size(100, 20);
            this.tMustHaveQuestsOnTest.TabIndex = 4;
            // 
            // tShouldntHaveCompletedQuests
            // 
            this.tShouldntHaveCompletedQuests.Location = new System.Drawing.Point(234, 92);
            this.tShouldntHaveCompletedQuests.Name = "tShouldntHaveCompletedQuests";
            this.tShouldntHaveCompletedQuests.Size = new System.Drawing.Size(100, 20);
            this.tShouldntHaveCompletedQuests.TabIndex = 9;
            // 
            // tMustHaveOpenQuests
            // 
            this.tMustHaveOpenQuests.Location = new System.Drawing.Point(128, 40);
            this.tMustHaveOpenQuests.Name = "tMustHaveOpenQuests";
            this.tMustHaveOpenQuests.Size = new System.Drawing.Size(100, 20);
            this.tMustHaveOpenQuests.TabIndex = 3;
            // 
            // tShouldntHaveQuestsOnTest
            // 
            this.tShouldntHaveQuestsOnTest.Location = new System.Drawing.Point(234, 66);
            this.tShouldntHaveQuestsOnTest.Name = "tShouldntHaveQuestsOnTest";
            this.tShouldntHaveQuestsOnTest.Size = new System.Drawing.Size(100, 20);
            this.tShouldntHaveQuestsOnTest.TabIndex = 8;
            // 
            // tShouldntHaveOpenQuests
            // 
            this.tShouldntHaveOpenQuests.Location = new System.Drawing.Point(234, 40);
            this.tShouldntHaveOpenQuests.Name = "tShouldntHaveOpenQuests";
            this.tShouldntHaveOpenQuests.Size = new System.Drawing.Size(100, 20);
            this.tShouldntHaveOpenQuests.TabIndex = 7;
            // 
            // tMustHaveCompletedQuests
            // 
            this.tMustHaveCompletedQuests.Location = new System.Drawing.Point(128, 92);
            this.tMustHaveCompletedQuests.Name = "tMustHaveCompletedQuests";
            this.tMustHaveCompletedQuests.Size = new System.Drawing.Size(100, 20);
            this.tMustHaveCompletedQuests.TabIndex = 5;
            // 
            // tabReputation
            // 
            this.tabReputation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabReputation.Controls.Add(this.lInfo);
            this.tabReputation.Controls.Add(this.dataReputation);
            this.tabReputation.Location = new System.Drawing.Point(4, 22);
            this.tabReputation.Name = "tabReputation";
            this.tabReputation.Padding = new System.Windows.Forms.Padding(3);
            this.tabReputation.Size = new System.Drawing.Size(566, 193);
            this.tabReputation.TabIndex = 1;
            this.tabReputation.Text = "Репутация";
            // 
            // lInfo
            // 
            this.lInfo.Location = new System.Drawing.Point(400, 6);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(146, 182);
            this.lInfo.TabIndex = 4;
            this.lInfo.Text = "Диалог будет доступен, если значение репутации игрока больше А и меньше В. Можно " +
    "задать только одно из значений А или В, или оба сразу.";
            // 
            // dataReputation
            // 
            this.dataReputation.AllowUserToAddRows = false;
            this.dataReputation.AllowUserToDeleteRows = false;
            this.dataReputation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataReputation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.dataGridViewTextBoxColumn1,
            this.b});
            this.dataReputation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataReputation.Location = new System.Drawing.Point(3, 3);
            this.dataReputation.Name = "dataReputation";
            this.dataReputation.Size = new System.Drawing.Size(560, 187);
            this.dataReputation.TabIndex = 3;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // name
            // 
            this.name.HeaderText = "Имя фракции";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "A (A < X < B)";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // b
            // 
            this.b.HeaderText = "B (A < X < B)";
            this.b.Name = "b";
            // 
            // tabKarma
            // 
            this.tabKarma.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabKarma.Controls.Add(this.panel2);
            this.tabKarma.Controls.Add(this.bTextBox);
            this.tabKarma.Controls.Add(this.label1);
            this.tabKarma.Controls.Add(this.aTextBox);
            this.tabKarma.Controls.Add(this.a);
            this.tabKarma.Location = new System.Drawing.Point(4, 22);
            this.tabKarma.Name = "tabKarma";
            this.tabKarma.Padding = new System.Windows.Forms.Padding(3);
            this.tabKarma.Size = new System.Drawing.Size(566, 193);
            this.tabKarma.TabIndex = 2;
            this.tabKarma.Text = "Карма";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelDescription);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 141);
            this.panel2.TabIndex = 4;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(3, 10);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(90, 13);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "Описание здесь";
            // 
            // bTextBox
            // 
            this.bTextBox.Location = new System.Drawing.Point(139, 21);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(100, 20);
            this.bTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "B";
            // 
            // aTextBox
            // 
            this.aTextBox.Location = new System.Drawing.Point(33, 21);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(100, 20);
            this.aTextBox.TabIndex = 6;
            // 
            // a
            // 
            this.a.AutoSize = true;
            this.a.Location = new System.Drawing.Point(79, 5);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(14, 13);
            this.a.TabIndex = 3;
            this.a.Text = "A";
            // 
            // tabClan
            // 
            this.tabClan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabClan.Controls.Add(this.gbClanOptions);
            this.tabClan.Controls.Add(this.cbShowClanOptions);
            this.tabClan.Location = new System.Drawing.Point(4, 22);
            this.tabClan.Name = "tabClan";
            this.tabClan.Padding = new System.Windows.Forms.Padding(3);
            this.tabClan.Size = new System.Drawing.Size(566, 193);
            this.tabClan.TabIndex = 3;
            this.tabClan.Text = "Клановые";
            // 
            // gbClanOptions
            // 
            this.gbClanOptions.BackColor = System.Drawing.SystemColors.Control;
            this.gbClanOptions.Controls.Add(this.cbLonerOnly);
            this.gbClanOptions.Controls.Add(this.cbAnyClanOnly);
            this.gbClanOptions.Controls.Add(this.cbSameClanOnly);
            this.gbClanOptions.Location = new System.Drawing.Point(15, 35);
            this.gbClanOptions.Name = "gbClanOptions";
            this.gbClanOptions.Size = new System.Drawing.Size(200, 100);
            this.gbClanOptions.TabIndex = 50;
            this.gbClanOptions.TabStop = false;
            this.gbClanOptions.Text = "Клановые опции";
            // 
            // cbLonerOnly
            // 
            this.cbLonerOnly.AutoSize = true;
            this.cbLonerOnly.Location = new System.Drawing.Point(19, 72);
            this.cbLonerOnly.Name = "cbLonerOnly";
            this.cbLonerOnly.Size = new System.Drawing.Size(134, 17);
            this.cbLonerOnly.TabIndex = 18;
            this.cbLonerOnly.Text = "Только для одиночек";
            this.cbLonerOnly.UseVisualStyleBackColor = true;
            // 
            // cbAnyClanOnly
            // 
            this.cbAnyClanOnly.AutoSize = true;
            this.cbAnyClanOnly.Location = new System.Drawing.Point(19, 48);
            this.cbAnyClanOnly.Name = "cbAnyClanOnly";
            this.cbAnyClanOnly.Size = new System.Drawing.Size(162, 17);
            this.cbAnyClanOnly.TabIndex = 17;
            this.cbAnyClanOnly.Text = "Только для имеющих клан";
            this.cbAnyClanOnly.UseVisualStyleBackColor = true;
            // 
            // cbSameClanOnly
            // 
            this.cbSameClanOnly.AutoSize = true;
            this.cbSameClanOnly.Location = new System.Drawing.Point(19, 24);
            this.cbSameClanOnly.Name = "cbSameClanOnly";
            this.cbSameClanOnly.Size = new System.Drawing.Size(147, 17);
            this.cbSameClanOnly.TabIndex = 16;
            this.cbSameClanOnly.Text = "Только для соклановца";
            this.cbSameClanOnly.UseVisualStyleBackColor = true;
            // 
            // cbShowClanOptions
            // 
            this.cbShowClanOptions.AutoSize = true;
            this.cbShowClanOptions.Location = new System.Drawing.Point(15, 12);
            this.cbShowClanOptions.Name = "cbShowClanOptions";
            this.cbShowClanOptions.Size = new System.Drawing.Size(110, 17);
            this.cbShowClanOptions.TabIndex = 51;
            this.cbShowClanOptions.Text = "Клановые опции";
            this.cbShowClanOptions.UseVisualStyleBackColor = true;
            this.cbShowClanOptions.Click += new System.EventHandler(this.cbShowClanOptions_Click);
            // 
            // mtbPlayerLevel
            // 
            this.mtbPlayerLevel.Location = new System.Drawing.Point(117, 244);
            this.mtbPlayerLevel.Mask = "000";
            this.mtbPlayerLevel.Name = "mtbPlayerLevel";
            this.mtbPlayerLevel.PromptChar = ' ';
            this.mtbPlayerLevel.Size = new System.Drawing.Size(100, 20);
            this.mtbPlayerLevel.TabIndex = 49;
            // 
            // lPlayerLevel
            // 
            this.lPlayerLevel.AutoSize = true;
            this.lPlayerLevel.Location = new System.Drawing.Point(13, 247);
            this.lPlayerLevel.Name = "lPlayerLevel";
            this.lPlayerLevel.Size = new System.Drawing.Size(92, 13);
            this.lPlayerLevel.TabIndex = 48;
            this.lPlayerLevel.Text = "Уровень игрока:";
            // 
            // bKarma
            // 
            this.bKarma.Location = new System.Drawing.Point(223, 244);
            this.bKarma.Name = "bKarma";
            this.bKarma.Size = new System.Drawing.Size(81, 23);
            this.bKarma.TabIndex = 47;
            this.bKarma.Text = "Карма";
            this.bKarma.UseVisualStyleBackColor = true;
            this.bKarma.Click += new System.EventHandler(this.bKarma_Click);
            // 
            // bReputation
            // 
            this.bReputation.Location = new System.Drawing.Point(309, 244);
            this.bReputation.Name = "bReputation";
            this.bReputation.Size = new System.Drawing.Size(81, 23);
            this.bReputation.TabIndex = 46;
            this.bReputation.Text = "Репутация";
            this.bReputation.UseVisualStyleBackColor = true;
            this.bReputation.Click += new System.EventHandler(this.bReputation_Click);
            // 
            // bEditDialogOk
            // 
            this.bEditDialogOk.Location = new System.Drawing.Point(172, 5);
            this.bEditDialogOk.Name = "bEditDialogOk";
            this.bEditDialogOk.Size = new System.Drawing.Size(106, 30);
            this.bEditDialogOk.TabIndex = 11;
            this.bEditDialogOk.Text = "OK";
            this.bEditDialogOk.UseVisualStyleBackColor = true;
            this.bEditDialogOk.Click += new System.EventHandler(this.bEditDialogOk_Click);
            // 
            // bEditDialogCancel
            // 
            this.bEditDialogCancel.Location = new System.Drawing.Point(284, 5);
            this.bEditDialogCancel.Name = "bEditDialogCancel";
            this.bEditDialogCancel.Size = new System.Drawing.Size(106, 30);
            this.bEditDialogCancel.TabIndex = 12;
            this.bEditDialogCancel.Text = "Отмена";
            this.bEditDialogCancel.UseVisualStyleBackColor = true;
            this.bEditDialogCancel.Click += new System.EventHandler(this.bEditDialogCancel_Click);
            // 
            // lReactionNPC
            // 
            this.lReactionNPC.AutoSize = true;
            this.lReactionNPC.Location = new System.Drawing.Point(3, 105);
            this.lReactionNPC.Name = "lReactionNPC";
            this.lReactionNPC.Size = new System.Drawing.Size(75, 13);
            this.lReactionNPC.TabIndex = 6;
            this.lReactionNPC.Text = "Реакция NPC";
            // 
            // pCommands
            // 
            this.pCommands.Controls.Add(this.debuglabel);
            this.pCommands.Controls.Add(this.debugTextBox);
            this.pCommands.Controls.Add(this.bEditDialogCancel);
            this.pCommands.Controls.Add(this.bEditDialogOk);
            this.pCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCommands.Location = new System.Drawing.Point(0, 644);
            this.pCommands.Name = "pCommands";
            this.pCommands.Size = new System.Drawing.Size(590, 62);
            this.pCommands.TabIndex = 13;
            // 
            // debuglabel
            // 
            this.debuglabel.AutoSize = true;
            this.debuglabel.Location = new System.Drawing.Point(5, 14);
            this.debuglabel.Name = "debuglabel";
            this.debuglabel.Size = new System.Drawing.Size(161, 13);
            this.debuglabel.TabIndex = 41;
            this.debuglabel.Text = "Для дебага(Стасу не трогать):";
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(8, 30);
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(147, 20);
            this.debugTextBox.TabIndex = 40;
            // 
            // gbTexts
            // 
            this.gbTexts.Controls.Add(this.tNodes);
            this.gbTexts.Controls.Add(this.lNodes);
            this.gbTexts.Controls.Add(this.tReactionNPC);
            this.gbTexts.Controls.Add(this.tPlayerText);
            this.gbTexts.Controls.Add(this.lAnswerText);
            this.gbTexts.Controls.Add(this.NPCSaidIs);
            this.gbTexts.Controls.Add(this.lGreetNPC);
            this.gbTexts.Controls.Add(this.lAttention);
            this.gbTexts.Controls.Add(this.lReactionNPC);
            this.gbTexts.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTexts.Location = new System.Drawing.Point(0, 0);
            this.gbTexts.Name = "gbTexts";
            this.gbTexts.Size = new System.Drawing.Size(590, 227);
            this.gbTexts.TabIndex = 15;
            this.gbTexts.TabStop = false;
            // 
            // EditDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 706);
            this.Controls.Add(this.actionsCheckBox);
            this.Controls.Add(this.pCommands);
            this.Controls.Add(this.gbActions);
            this.Controls.Add(this.gbPrecondition);
            this.Controls.Add(this.gbTexts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditDialogForm";
            this.Text = "Редактирование диалога";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditDialogForm_FormClosed);
            this.gbActions.ResumeLayout(false);
            this.gbActions.PerformLayout();
            this.gbPrecondition.ResumeLayout(false);
            this.gbPrecondition.PerformLayout();
            this.tabQuestsCircs.ResumeLayout(false);
            this.tabQuests.ResumeLayout(false);
            this.gbQuestCondition.ResumeLayout(false);
            this.gbQuestCondition.PerformLayout();
            this.tabReputation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataReputation)).EndInit();
            this.tabKarma.ResumeLayout(false);
            this.tabKarma.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabClan.ResumeLayout(false);
            this.tabClan.PerformLayout();
            this.gbClanOptions.ResumeLayout(false);
            this.gbClanOptions.PerformLayout();
            this.pCommands.ResumeLayout(false);
            this.pCommands.PerformLayout();
            this.gbTexts.ResumeLayout(false);
            this.gbTexts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tPlayerText;
        private System.Windows.Forms.Label lAnswerText;
        private System.Windows.Forms.Label NPCSaidIs;
        private System.Windows.Forms.Label lGreetNPC;
        private System.Windows.Forms.Label lAttention;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.ComboBox teleportComboBox;
        private System.Windows.Forms.MaskedTextBox tbCompleteQuests;
        private System.Windows.Forms.MaskedTextBox tbGetQuests;
        private System.Windows.Forms.ComboBox ToDialogComboBox;
        private System.Windows.Forms.CheckBox cbExit;
        private System.Windows.Forms.CheckBox cbCompleteQuests;
        private System.Windows.Forms.CheckBox cbGetQuests;
        private System.Windows.Forms.TextBox tReactionNPC;
        private System.Windows.Forms.MaskedTextBox tNodes;
        private System.Windows.Forms.Label lNodes;
        private System.Windows.Forms.CheckBox actionsCheckBox;
        private System.Windows.Forms.GroupBox gbQuestCondition;
        private System.Windows.Forms.Label lShouldntHaveQuests;
        private System.Windows.Forms.Label lNecessaryQuests;
        private System.Windows.Forms.Label lCompletedQuests;
        private System.Windows.Forms.Label lOnTestQuests;
        private System.Windows.Forms.Label lOpenedQuests;
        private System.Windows.Forms.MaskedTextBox tMustHaveQuestsOnTest;
        private System.Windows.Forms.MaskedTextBox tShouldntHaveCompletedQuests;
        private System.Windows.Forms.MaskedTextBox tMustHaveOpenQuests;
        private System.Windows.Forms.MaskedTextBox tShouldntHaveQuestsOnTest;
        private System.Windows.Forms.MaskedTextBox tShouldntHaveOpenQuests;
        private System.Windows.Forms.MaskedTextBox tMustHaveCompletedQuests;
        private System.Windows.Forms.Button bEditDialogOk;
        private System.Windows.Forms.Button bEditDialogCancel;
        private System.Windows.Forms.Label lReactionNPC;
        private System.Windows.Forms.Panel pCommands;
        private System.Windows.Forms.GroupBox gbTexts;
        private System.Windows.Forms.Button bReputation;
        private System.Windows.Forms.Button bKarma;
        private System.Windows.Forms.Label lFailedQuests;
        private System.Windows.Forms.MaskedTextBox tShouldntHaveFailedQuests;
        private System.Windows.Forms.MaskedTextBox tMustHaveFailedQuests;
        private System.Windows.Forms.MaskedTextBox mtbPlayerLevel;
        private System.Windows.Forms.Label lPlayerLevel;
        private System.Windows.Forms.CheckBox cbShowClanOptions;
        private System.Windows.Forms.GroupBox gbClanOptions;
        private System.Windows.Forms.CheckBox cbLonerOnly;
        private System.Windows.Forms.CheckBox cbAnyClanOnly;
        private System.Windows.Forms.CheckBox cbSameClanOnly;
        private System.Windows.Forms.ComboBox ActionsComboBox;
        private System.Windows.Forms.GroupBox gbPrecondition;
        private System.Windows.Forms.CheckBox cbFailQuests;
        private System.Windows.Forms.CheckBox cbCancelQuests;
        private System.Windows.Forms.MaskedTextBox tbFailQuests;
        private System.Windows.Forms.MaskedTextBox tbCancelQuests;
        private System.Windows.Forms.MaskedTextBox debugTextBox;
        private System.Windows.Forms.Label debuglabel;
        private System.Windows.Forms.Label lMassQuests;
        private System.Windows.Forms.MaskedTextBox tShouldntHaveMassQuests;
        private System.Windows.Forms.MaskedTextBox tMustHaveMassQuests;
        private System.Windows.Forms.TabControl tabQuestsCircs;
        private System.Windows.Forms.TabPage tabQuests;
        private System.Windows.Forms.TabPage tabReputation;
        private System.Windows.Forms.TabPage tabKarma;
        private System.Windows.Forms.TabPage tabClan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.MaskedTextBox bTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox aTextBox;
        private System.Windows.Forms.Label a;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.DataGridView dataReputation;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn b;
    }
}