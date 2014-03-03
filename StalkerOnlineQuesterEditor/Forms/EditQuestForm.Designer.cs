namespace StalkerOnlineQuesterEditor
{
    public partial class EditQuestForm
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
            this.QuestPanel = new System.Windows.Forms.Panel();
            this.groupQuestBox = new System.Windows.Forms.GroupBox();
            this.filedIFtContentBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.rewardGroupBox = new System.Windows.Forms.GroupBox();
            this.lSupportSkills = new System.Windows.Forms.Label();
            this.lSurvivalSkills = new System.Windows.Forms.Label();
            this.tSupport = new System.Windows.Forms.TextBox();
            this.tSurvival = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.difficultyComboBox = new System.Windows.Forms.ComboBox();
            this.bRewardEffects = new System.Windows.Forms.Button();
            this.bRewardFractions = new System.Windows.Forms.Button();
            this.bItemReward = new System.Windows.Forms.Button();
            this.textBoxKarmaPK = new System.Windows.Forms.TextBox();
            this.lKarmaPK = new System.Windows.Forms.Label();
            this.bHideReward = new System.Windows.Forms.Button();
            this.creditsTextBox = new System.Windows.Forms.TextBox();
            this.lCredits = new System.Windows.Forms.Label();
            this.tExperience = new System.Windows.Forms.TextBox();
            this.lCombatSkills = new System.Windows.Forms.Label();
            this.lQuestRules = new System.Windows.Forms.GroupBox();
            this.bItemQuestRules = new System.Windows.Forms.Button();
            this.bHideRules = new System.Windows.Forms.Button();
            this.scenariosTextBox = new System.Windows.Forms.TextBox();
            this.labelScenarios = new System.Windows.Forms.Label();
            this.groupQuestRulesBox = new System.Windows.Forms.GroupBox();
            this.instanceComboBox = new System.Windows.Forms.ComboBox();
            this.labelTeleportTo = new System.Windows.Forms.Label();
            this.numericMinGroup = new System.Windows.Forms.NumericUpDown();
            this.numericMaxGroup = new System.Windows.Forms.NumericUpDown();
            this.labelMinGroup = new System.Windows.Forms.Label();
            this.labelMaxGroup = new System.Windows.Forms.Label();
            this.preconditionGroupBox = new System.Windows.Forms.GroupBox();
            this.lH = new System.Windows.Forms.Label();
            this.takenPeriodTextBox = new System.Windows.Forms.TextBox();
            this.lDaily = new System.Windows.Forms.Label();
            this.repeatComboBox = new System.Windows.Forms.ComboBox();
            this.lRepeat = new System.Windows.Forms.Label();
            this.targetBox = new System.Windows.Forms.GroupBox();
            this.bItemQID = new System.Windows.Forms.Button();
            this.bHideTarget = new System.Windows.Forms.Button();
            this.targetAttributeComboBox2 = new System.Windows.Forms.ComboBox();
            this.lTargetAttr1 = new System.Windows.Forms.Label();
            this.bTargetClearDynamic = new System.Windows.Forms.Button();
            this.bTargetAddDynamic = new System.Windows.Forms.Button();
            this.dynamicCheckBox = new System.Windows.Forms.CheckBox();
            this.ltargetResult = new System.Windows.Forms.Label();
            this.resultextBox = new System.Windows.Forms.MaskedTextBox();
            this.targetAttributeComboBox = new System.Windows.Forms.ComboBox();
            this.labelTargetAttr = new System.Windows.Forms.Label();
            this.isClanCheckBox = new System.Windows.Forms.CheckBox();
            this.IsGroupCheckBox = new System.Windows.Forms.CheckBox();
            this.lQuantity = new System.Windows.Forms.Label();
            this.quantityUpDown = new System.Windows.Forms.NumericUpDown();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.lNameObject = new System.Windows.Forms.Label();
            this.questInformationBox = new System.Windows.Forms.GroupBox();
            this.tutorialCheckBox = new System.Windows.Forms.CheckBox();
            this.loseRButton = new System.Windows.Forms.RadioButton();
            this.winRButton = new System.Windows.Forms.RadioButton();
            this.lFailed = new System.Windows.Forms.Label();
            this.lWin = new System.Windows.Forms.Label();
            this.bHideInformation = new System.Windows.Forms.Button();
            this.showJournalCheckBox = new System.Windows.Forms.CheckBox();
            this.showCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.showFailedCheckBox = new System.Windows.Forms.CheckBox();
            this.showTakeCheckBox = new System.Windows.Forms.CheckBox();
            this.showWinCheckBox = new System.Windows.Forms.CheckBox();
            this.showProgressCheckBox = new System.Windows.Forms.CheckBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.onFailedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.onWonTextBox = new System.Windows.Forms.MaskedTextBox();
            this.titleTextBox = new System.Windows.Forms.MaskedTextBox();
            this.lTitle = new System.Windows.Forms.Label();
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.eventLabel = new System.Windows.Forms.Label();
            this.radMarkupDialog = new Telerik.WinControls.UI.RadMarkupDialog();
            this.QuestPanel.SuspendLayout();
            this.groupQuestBox.SuspendLayout();
            this.filedIFtContentBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.rewardGroupBox.SuspendLayout();
            this.lQuestRules.SuspendLayout();
            this.groupQuestRulesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxGroup)).BeginInit();
            this.preconditionGroupBox.SuspendLayout();
            this.targetBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityUpDown)).BeginInit();
            this.questInformationBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuestPanel
            // 
            this.QuestPanel.Controls.Add(this.groupQuestBox);
            this.QuestPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuestPanel.Location = new System.Drawing.Point(0, 0);
            this.QuestPanel.Name = "QuestPanel";
            this.QuestPanel.Size = new System.Drawing.Size(626, 863);
            this.QuestPanel.TabIndex = 0;
            // 
            // groupQuestBox
            // 
            this.groupQuestBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupQuestBox.Controls.Add(this.filedIFtContentBox);
            this.groupQuestBox.Controls.Add(this.eventComboBox);
            this.groupQuestBox.Controls.Add(this.eventLabel);
            this.groupQuestBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupQuestBox.Location = new System.Drawing.Point(0, 0);
            this.groupQuestBox.Name = "groupQuestBox";
            this.groupQuestBox.Size = new System.Drawing.Size(626, 863);
            this.groupQuestBox.TabIndex = 0;
            this.groupQuestBox.TabStop = false;
            this.groupQuestBox.Text = "Событие ID ";
            // 
            // filedIFtContentBox
            // 
            this.filedIFtContentBox.AutoSize = true;
            this.filedIFtContentBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filedIFtContentBox.Controls.Add(this.panel1);
            this.filedIFtContentBox.Controls.Add(this.rewardGroupBox);
            this.filedIFtContentBox.Controls.Add(this.lQuestRules);
            this.filedIFtContentBox.Controls.Add(this.preconditionGroupBox);
            this.filedIFtContentBox.Controls.Add(this.targetBox);
            this.filedIFtContentBox.Controls.Add(this.questInformationBox);
            this.filedIFtContentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filedIFtContentBox.Location = new System.Drawing.Point(3, 50);
            this.filedIFtContentBox.Name = "filedIFtContentBox";
            this.filedIFtContentBox.Size = new System.Drawing.Size(620, 810);
            this.filedIFtContentBox.TabIndex = 2;
            this.filedIFtContentBox.TabStop = false;
            this.filedIFtContentBox.Text = "Содержание";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 745);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 62);
            this.panel1.TabIndex = 7;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(445, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(526, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // rewardGroupBox
            // 
            this.rewardGroupBox.AutoSize = true;
            this.rewardGroupBox.Controls.Add(this.lSupportSkills);
            this.rewardGroupBox.Controls.Add(this.lSurvivalSkills);
            this.rewardGroupBox.Controls.Add(this.tSupport);
            this.rewardGroupBox.Controls.Add(this.tSurvival);
            this.rewardGroupBox.Controls.Add(this.label1);
            this.rewardGroupBox.Controls.Add(this.difficultyComboBox);
            this.rewardGroupBox.Controls.Add(this.bRewardEffects);
            this.rewardGroupBox.Controls.Add(this.bRewardFractions);
            this.rewardGroupBox.Controls.Add(this.bItemReward);
            this.rewardGroupBox.Controls.Add(this.textBoxKarmaPK);
            this.rewardGroupBox.Controls.Add(this.lKarmaPK);
            this.rewardGroupBox.Controls.Add(this.bHideReward);
            this.rewardGroupBox.Controls.Add(this.creditsTextBox);
            this.rewardGroupBox.Controls.Add(this.lCredits);
            this.rewardGroupBox.Controls.Add(this.tExperience);
            this.rewardGroupBox.Controls.Add(this.lCombatSkills);
            this.rewardGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.rewardGroupBox.Location = new System.Drawing.Point(3, 611);
            this.rewardGroupBox.Name = "rewardGroupBox";
            this.rewardGroupBox.Size = new System.Drawing.Size(614, 134);
            this.rewardGroupBox.TabIndex = 4;
            this.rewardGroupBox.TabStop = false;
            this.rewardGroupBox.Text = "Награда";
            // 
            // lSupportSkills
            // 
            this.lSupportSkills.AutoSize = true;
            this.lSupportSkills.Location = new System.Drawing.Point(295, 67);
            this.lSupportSkills.Name = "lSupportSkills";
            this.lSupportSkills.Size = new System.Drawing.Size(68, 13);
            this.lSupportSkills.TabIndex = 40;
            this.lSupportSkills.Text = "Поддержки:";
            // 
            // lSurvivalSkills
            // 
            this.lSurvivalSkills.AutoSize = true;
            this.lSurvivalSkills.Location = new System.Drawing.Point(172, 67);
            this.lSurvivalSkills.Name = "lSurvivalSkills";
            this.lSurvivalSkills.Size = new System.Drawing.Size(69, 13);
            this.lSurvivalSkills.TabIndex = 39;
            this.lSurvivalSkills.Text = "Выживания:";
            // 
            // tSupport
            // 
            this.tSupport.Location = new System.Drawing.Point(369, 67);
            this.tSupport.Name = "tSupport";
            this.tSupport.Size = new System.Drawing.Size(54, 20);
            this.tSupport.TabIndex = 38;
            // 
            // tSurvival
            // 
            this.tSurvival.Location = new System.Drawing.Point(247, 67);
            this.tSurvival.Name = "tSurvival";
            this.tSurvival.Size = new System.Drawing.Size(42, 20);
            this.tSurvival.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(458, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Сложность";
            // 
            // difficultyComboBox
            // 
            this.difficultyComboBox.FormattingEnabled = true;
            this.difficultyComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.difficultyComboBox.Location = new System.Drawing.Point(527, 64);
            this.difficultyComboBox.Name = "difficultyComboBox";
            this.difficultyComboBox.Size = new System.Drawing.Size(75, 21);
            this.difficultyComboBox.TabIndex = 35;
            // 
            // bRewardEffects
            // 
            this.bRewardEffects.Location = new System.Drawing.Point(233, 19);
            this.bRewardEffects.Name = "bRewardEffects";
            this.bRewardEffects.Size = new System.Drawing.Size(94, 23);
            this.bRewardEffects.TabIndex = 34;
            this.bRewardEffects.Text = "Эффекты";
            this.bRewardEffects.UseVisualStyleBackColor = true;
            this.bRewardEffects.Click += new System.EventHandler(this.bRewardEffects_Click);
            // 
            // bRewardFractions
            // 
            this.bRewardFractions.Location = new System.Drawing.Point(119, 19);
            this.bRewardFractions.Name = "bRewardFractions";
            this.bRewardFractions.Size = new System.Drawing.Size(105, 23);
            this.bRewardFractions.TabIndex = 33;
            this.bRewardFractions.Text = "Фракции";
            this.bRewardFractions.UseVisualStyleBackColor = true;
            this.bRewardFractions.Click += new System.EventHandler(this.bRewardFractions_Click);
            // 
            // bItemReward
            // 
            this.bItemReward.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bItemReward.Location = new System.Drawing.Point(8, 19);
            this.bItemReward.Name = "bItemReward";
            this.bItemReward.Size = new System.Drawing.Size(104, 23);
            this.bItemReward.TabIndex = 32;
            this.bItemReward.Text = "Предметы";
            this.bItemReward.UseVisualStyleBackColor = true;
            this.bItemReward.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxKarmaPK
            // 
            this.textBoxKarmaPK.Location = new System.Drawing.Point(164, 95);
            this.textBoxKarmaPK.Name = "textBoxKarmaPK";
            this.textBoxKarmaPK.Size = new System.Drawing.Size(100, 20);
            this.textBoxKarmaPK.TabIndex = 31;
            // 
            // lKarmaPK
            // 
            this.lKarmaPK.AutoSize = true;
            this.lKarmaPK.Location = new System.Drawing.Point(7, 98);
            this.lKarmaPK.Name = "lKarmaPK";
            this.lKarmaPK.Size = new System.Drawing.Size(146, 13);
            this.lKarmaPK.TabIndex = 30;
            this.lKarmaPK.Text = "Карма ПК (\"-\" -  вычитание)";
            // 
            // bHideReward
            // 
            this.bHideReward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bHideReward.Location = new System.Drawing.Point(527, 11);
            this.bHideReward.Name = "bHideReward";
            this.bHideReward.Size = new System.Drawing.Size(75, 23);
            this.bHideReward.TabIndex = 19;
            this.bHideReward.Text = "Скрыть";
            this.bHideReward.UseVisualStyleBackColor = true;
            this.bHideReward.Click += new System.EventHandler(this.bHideReward_Click);
            // 
            // creditsTextBox
            // 
            this.creditsTextBox.Location = new System.Drawing.Point(119, 45);
            this.creditsTextBox.Name = "creditsTextBox";
            this.creditsTextBox.Size = new System.Drawing.Size(47, 20);
            this.creditsTextBox.TabIndex = 15;
            // 
            // lCredits
            // 
            this.lCredits.AutoSize = true;
            this.lCredits.Location = new System.Drawing.Point(6, 47);
            this.lCredits.Name = "lCredits";
            this.lCredits.Size = new System.Drawing.Size(51, 13);
            this.lCredits.TabIndex = 14;
            this.lCredits.Text = "Кредиты";
            // 
            // tExperience
            // 
            this.tExperience.Location = new System.Drawing.Point(119, 67);
            this.tExperience.Name = "tExperience";
            this.tExperience.Size = new System.Drawing.Size(47, 20);
            this.tExperience.TabIndex = 9;
            // 
            // lCombatSkills
            // 
            this.lCombatSkills.AutoSize = true;
            this.lCombatSkills.Location = new System.Drawing.Point(7, 67);
            this.lCombatSkills.Name = "lCombatSkills";
            this.lCombatSkills.Size = new System.Drawing.Size(76, 13);
            this.lCombatSkills.TabIndex = 8;
            this.lCombatSkills.Text = "Опыт боевой:";
            // 
            // lQuestRules
            // 
            this.lQuestRules.Controls.Add(this.bItemQuestRules);
            this.lQuestRules.Controls.Add(this.bHideRules);
            this.lQuestRules.Controls.Add(this.scenariosTextBox);
            this.lQuestRules.Controls.Add(this.labelScenarios);
            this.lQuestRules.Controls.Add(this.groupQuestRulesBox);
            this.lQuestRules.Dock = System.Windows.Forms.DockStyle.Top;
            this.lQuestRules.Location = new System.Drawing.Point(3, 454);
            this.lQuestRules.Name = "lQuestRules";
            this.lQuestRules.Size = new System.Drawing.Size(614, 157);
            this.lQuestRules.TabIndex = 3;
            this.lQuestRules.TabStop = false;
            this.lQuestRules.Text = "Правила квеста";
            // 
            // bItemQuestRules
            // 
            this.bItemQuestRules.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bItemQuestRules.ImageKey = "(none)";
            this.bItemQuestRules.Location = new System.Drawing.Point(10, 19);
            this.bItemQuestRules.Name = "bItemQuestRules";
            this.bItemQuestRules.Size = new System.Drawing.Size(102, 23);
            this.bItemQuestRules.TabIndex = 11;
            this.bItemQuestRules.Text = "Предметы";
            this.bItemQuestRules.UseVisualStyleBackColor = true;
            this.bItemQuestRules.Click += new System.EventHandler(this.bItemQuestRules_Click);
            // 
            // bHideRules
            // 
            this.bHideRules.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bHideRules.Location = new System.Drawing.Point(527, 9);
            this.bHideRules.Name = "bHideRules";
            this.bHideRules.Size = new System.Drawing.Size(75, 23);
            this.bHideRules.TabIndex = 10;
            this.bHideRules.Text = "Скрыть";
            this.bHideRules.UseVisualStyleBackColor = true;
            this.bHideRules.Click += new System.EventHandler(this.bHideRules_Click);
            // 
            // scenariosTextBox
            // 
            this.scenariosTextBox.Location = new System.Drawing.Point(90, 49);
            this.scenariosTextBox.Name = "scenariosTextBox";
            this.scenariosTextBox.Size = new System.Drawing.Size(134, 20);
            this.scenariosTextBox.TabIndex = 9;
            // 
            // labelScenarios
            // 
            this.labelScenarios.AutoSize = true;
            this.labelScenarios.Location = new System.Drawing.Point(11, 49);
            this.labelScenarios.Name = "labelScenarios";
            this.labelScenarios.Size = new System.Drawing.Size(56, 13);
            this.labelScenarios.TabIndex = 2;
            this.labelScenarios.Text = "Сценарий";
            // 
            // groupQuestRulesBox
            // 
            this.groupQuestRulesBox.Controls.Add(this.instanceComboBox);
            this.groupQuestRulesBox.Controls.Add(this.labelTeleportTo);
            this.groupQuestRulesBox.Controls.Add(this.numericMinGroup);
            this.groupQuestRulesBox.Controls.Add(this.numericMaxGroup);
            this.groupQuestRulesBox.Controls.Add(this.labelMinGroup);
            this.groupQuestRulesBox.Controls.Add(this.labelMaxGroup);
            this.groupQuestRulesBox.Enabled = false;
            this.groupQuestRulesBox.Location = new System.Drawing.Point(339, 37);
            this.groupQuestRulesBox.Name = "groupQuestRulesBox";
            this.groupQuestRulesBox.Size = new System.Drawing.Size(262, 99);
            this.groupQuestRulesBox.TabIndex = 8;
            this.groupQuestRulesBox.TabStop = false;
            this.groupQuestRulesBox.Text = "Для групп";
            // 
            // instanceComboBox
            // 
            this.instanceComboBox.FormattingEnabled = true;
            this.instanceComboBox.Location = new System.Drawing.Point(107, 63);
            this.instanceComboBox.Name = "instanceComboBox";
            this.instanceComboBox.Size = new System.Drawing.Size(121, 21);
            this.instanceComboBox.TabIndex = 5;
            // 
            // labelTeleportTo
            // 
            this.labelTeleportTo.AutoSize = true;
            this.labelTeleportTo.Location = new System.Drawing.Point(8, 68);
            this.labelTeleportTo.Name = "labelTeleportTo";
            this.labelTeleportTo.Size = new System.Drawing.Size(53, 13);
            this.labelTeleportTo.TabIndex = 4;
            this.labelTeleportTo.Text = "Инстанс:";
            // 
            // numericMinGroup
            // 
            this.numericMinGroup.Location = new System.Drawing.Point(162, 37);
            this.numericMinGroup.Name = "numericMinGroup";
            this.numericMinGroup.Size = new System.Drawing.Size(66, 20);
            this.numericMinGroup.TabIndex = 3;
            // 
            // numericMaxGroup
            // 
            this.numericMaxGroup.Location = new System.Drawing.Point(162, 13);
            this.numericMaxGroup.Name = "numericMaxGroup";
            this.numericMaxGroup.Size = new System.Drawing.Size(66, 20);
            this.numericMaxGroup.TabIndex = 2;
            // 
            // labelMinGroup
            // 
            this.labelMinGroup.AutoSize = true;
            this.labelMinGroup.Location = new System.Drawing.Point(7, 37);
            this.labelMinGroup.Name = "labelMinGroup";
            this.labelMinGroup.Size = new System.Drawing.Size(133, 13);
            this.labelMinGroup.TabIndex = 1;
            this.labelMinGroup.Text = "Мин. участников группы:";
            // 
            // labelMaxGroup
            // 
            this.labelMaxGroup.AutoSize = true;
            this.labelMaxGroup.Location = new System.Drawing.Point(7, 20);
            this.labelMaxGroup.Name = "labelMaxGroup";
            this.labelMaxGroup.Size = new System.Drawing.Size(139, 13);
            this.labelMaxGroup.TabIndex = 0;
            this.labelMaxGroup.Text = "Макс. участников группы:";
            // 
            // preconditionGroupBox
            // 
            this.preconditionGroupBox.AutoSize = true;
            this.preconditionGroupBox.Controls.Add(this.lH);
            this.preconditionGroupBox.Controls.Add(this.takenPeriodTextBox);
            this.preconditionGroupBox.Controls.Add(this.lDaily);
            this.preconditionGroupBox.Controls.Add(this.repeatComboBox);
            this.preconditionGroupBox.Controls.Add(this.lRepeat);
            this.preconditionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.preconditionGroupBox.Location = new System.Drawing.Point(3, 401);
            this.preconditionGroupBox.Name = "preconditionGroupBox";
            this.preconditionGroupBox.Size = new System.Drawing.Size(614, 53);
            this.preconditionGroupBox.TabIndex = 2;
            this.preconditionGroupBox.TabStop = false;
            this.preconditionGroupBox.Text = "Условия";
            // 
            // lH
            // 
            this.lH.AutoSize = true;
            this.lH.Location = new System.Drawing.Point(421, 16);
            this.lH.Name = "lH";
            this.lH.Size = new System.Drawing.Size(36, 13);
            this.lH.TabIndex = 4;
            this.lH.Text = "часов";
            // 
            // takenPeriodTextBox
            // 
            this.takenPeriodTextBox.Location = new System.Drawing.Point(363, 13);
            this.takenPeriodTextBox.Name = "takenPeriodTextBox";
            this.takenPeriodTextBox.Size = new System.Drawing.Size(52, 20);
            this.takenPeriodTextBox.TabIndex = 3;
            // 
            // lDaily
            // 
            this.lDaily.AutoSize = true;
            this.lDaily.Location = new System.Drawing.Point(230, 16);
            this.lDaily.Name = "lDaily";
            this.lDaily.Size = new System.Drawing.Size(127, 13);
            this.lDaily.TabIndex = 2;
            this.lDaily.Text = "Период между взятием";
            // 
            // repeatComboBox
            // 
            this.repeatComboBox.FormattingEnabled = true;
            this.repeatComboBox.Location = new System.Drawing.Point(90, 13);
            this.repeatComboBox.Name = "repeatComboBox";
            this.repeatComboBox.Size = new System.Drawing.Size(134, 21);
            this.repeatComboBox.TabIndex = 1;
            // 
            // lRepeat
            // 
            this.lRepeat.AutoSize = true;
            this.lRepeat.Location = new System.Drawing.Point(6, 16);
            this.lRepeat.Name = "lRepeat";
            this.lRepeat.Size = new System.Drawing.Size(44, 13);
            this.lRepeat.TabIndex = 0;
            this.lRepeat.Text = "Повтор";
            // 
            // targetBox
            // 
            this.targetBox.AutoSize = true;
            this.targetBox.Controls.Add(this.bItemQID);
            this.targetBox.Controls.Add(this.bHideTarget);
            this.targetBox.Controls.Add(this.targetAttributeComboBox2);
            this.targetBox.Controls.Add(this.lTargetAttr1);
            this.targetBox.Controls.Add(this.bTargetClearDynamic);
            this.targetBox.Controls.Add(this.bTargetAddDynamic);
            this.targetBox.Controls.Add(this.dynamicCheckBox);
            this.targetBox.Controls.Add(this.ltargetResult);
            this.targetBox.Controls.Add(this.resultextBox);
            this.targetBox.Controls.Add(this.targetAttributeComboBox);
            this.targetBox.Controls.Add(this.labelTargetAttr);
            this.targetBox.Controls.Add(this.isClanCheckBox);
            this.targetBox.Controls.Add(this.IsGroupCheckBox);
            this.targetBox.Controls.Add(this.lQuantity);
            this.targetBox.Controls.Add(this.quantityUpDown);
            this.targetBox.Controls.Add(this.targetComboBox);
            this.targetBox.Controls.Add(this.lNameObject);
            this.targetBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.targetBox.Location = new System.Drawing.Point(3, 254);
            this.targetBox.Name = "targetBox";
            this.targetBox.Size = new System.Drawing.Size(614, 147);
            this.targetBox.TabIndex = 1;
            this.targetBox.TabStop = false;
            this.targetBox.Text = "Цель";
            // 
            // bItemQID
            // 
            this.bItemQID.Enabled = false;
            this.bItemQID.Location = new System.Drawing.Point(233, 67);
            this.bItemQID.Name = "bItemQID";
            this.bItemQID.Size = new System.Drawing.Size(75, 23);
            this.bItemQID.TabIndex = 16;
            this.bItemQID.Text = "Квест";
            this.bItemQID.UseVisualStyleBackColor = true;
            this.bItemQID.Click += new System.EventHandler(this.bItemQID_Click);
            // 
            // bHideTarget
            // 
            this.bHideTarget.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bHideTarget.Location = new System.Drawing.Point(527, 9);
            this.bHideTarget.Name = "bHideTarget";
            this.bHideTarget.Size = new System.Drawing.Size(75, 23);
            this.bHideTarget.TabIndex = 15;
            this.bHideTarget.Text = "Скрыть";
            this.bHideTarget.UseVisualStyleBackColor = true;
            this.bHideTarget.Click += new System.EventHandler(this.bHideTarget_Click);
            // 
            // targetAttributeComboBox2
            // 
            this.targetAttributeComboBox2.DropDownWidth = 200;
            this.targetAttributeComboBox2.Enabled = false;
            this.targetAttributeComboBox2.FormattingEnabled = true;
            this.targetAttributeComboBox2.Location = new System.Drawing.Point(103, 93);
            this.targetAttributeComboBox2.Name = "targetAttributeComboBox2";
            this.targetAttributeComboBox2.Size = new System.Drawing.Size(121, 21);
            this.targetAttributeComboBox2.TabIndex = 14;
            // 
            // lTargetAttr1
            // 
            this.lTargetAttr1.AutoSize = true;
            this.lTargetAttr1.Enabled = false;
            this.lTargetAttr1.Location = new System.Drawing.Point(14, 93);
            this.lTargetAttr1.Name = "lTargetAttr1";
            this.lTargetAttr1.Size = new System.Drawing.Size(58, 13);
            this.lTargetAttr1.TabIndex = 13;
            this.lTargetAttr1.Text = "Аттрибут2";
            // 
            // bTargetClearDynamic
            // 
            this.bTargetClearDynamic.Enabled = false;
            this.bTargetClearDynamic.Location = new System.Drawing.Point(314, 38);
            this.bTargetClearDynamic.Name = "bTargetClearDynamic";
            this.bTargetClearDynamic.Size = new System.Drawing.Size(75, 23);
            this.bTargetClearDynamic.TabIndex = 12;
            this.bTargetClearDynamic.Text = "Очистить";
            this.bTargetClearDynamic.UseVisualStyleBackColor = true;
            this.bTargetClearDynamic.Click += new System.EventHandler(this.bTargetClearDynamic_Click);
            // 
            // bTargetAddDynamic
            // 
            this.bTargetAddDynamic.Enabled = false;
            this.bTargetAddDynamic.Location = new System.Drawing.Point(233, 38);
            this.bTargetAddDynamic.Name = "bTargetAddDynamic";
            this.bTargetAddDynamic.Size = new System.Drawing.Size(75, 23);
            this.bTargetAddDynamic.TabIndex = 11;
            this.bTargetAddDynamic.Text = "Добавить";
            this.bTargetAddDynamic.UseVisualStyleBackColor = true;
            this.bTargetAddDynamic.Click += new System.EventHandler(this.bTargetAddDynamic_Click);
            // 
            // dynamicCheckBox
            // 
            this.dynamicCheckBox.AutoSize = true;
            this.dynamicCheckBox.Enabled = false;
            this.dynamicCheckBox.Location = new System.Drawing.Point(233, 13);
            this.dynamicCheckBox.Name = "dynamicCheckBox";
            this.dynamicCheckBox.Size = new System.Drawing.Size(102, 17);
            this.dynamicCheckBox.TabIndex = 10;
            this.dynamicCheckBox.Text = "Динамический";
            this.dynamicCheckBox.UseVisualStyleBackColor = true;
            this.dynamicCheckBox.CheckedChanged += new System.EventHandler(this.dynamicCheckBox_CheckedChanged);
            // 
            // ltargetResult
            // 
            this.ltargetResult.AutoSize = true;
            this.ltargetResult.Enabled = false;
            this.ltargetResult.Location = new System.Drawing.Point(14, 15);
            this.ltargetResult.Name = "ltargetResult";
            this.ltargetResult.Size = new System.Drawing.Size(59, 13);
            this.ltargetResult.TabIndex = 9;
            this.ltargetResult.Text = "Результат";
            // 
            // resultextBox
            // 
            this.resultextBox.Enabled = false;
            this.resultextBox.Location = new System.Drawing.Point(103, 12);
            this.resultextBox.Name = "resultextBox";
            this.resultextBox.Size = new System.Drawing.Size(121, 20);
            this.resultextBox.TabIndex = 8;
            // 
            // targetAttributeComboBox
            // 
            this.targetAttributeComboBox.DropDownWidth = 121;
            this.targetAttributeComboBox.Enabled = false;
            this.targetAttributeComboBox.FormattingEnabled = true;
            this.targetAttributeComboBox.Location = new System.Drawing.Point(103, 66);
            this.targetAttributeComboBox.Name = "targetAttributeComboBox";
            this.targetAttributeComboBox.Size = new System.Drawing.Size(121, 21);
            this.targetAttributeComboBox.TabIndex = 7;
            this.targetAttributeComboBox.SelectedIndexChanged += new System.EventHandler(this.targetAttributeComboBox_SelectedIndexChanged);
            // 
            // labelTargetAttr
            // 
            this.labelTargetAttr.AutoSize = true;
            this.labelTargetAttr.Enabled = false;
            this.labelTargetAttr.Location = new System.Drawing.Point(14, 66);
            this.labelTargetAttr.Name = "labelTargetAttr";
            this.labelTargetAttr.Size = new System.Drawing.Size(52, 13);
            this.labelTargetAttr.TabIndex = 6;
            this.labelTargetAttr.Text = "Аттрибут";
            // 
            // isClanCheckBox
            // 
            this.isClanCheckBox.AutoSize = true;
            this.isClanCheckBox.Enabled = false;
            this.isClanCheckBox.Location = new System.Drawing.Point(499, 111);
            this.isClanCheckBox.Name = "isClanCheckBox";
            this.isClanCheckBox.Size = new System.Drawing.Size(109, 17);
            this.isClanCheckBox.TabIndex = 5;
            this.isClanCheckBox.Text = "Клановый квест";
            this.isClanCheckBox.UseVisualStyleBackColor = true;
            this.isClanCheckBox.CheckedChanged += new System.EventHandler(this.isClanCheckBox_CheckedChanged);
            // 
            // IsGroupCheckBox
            // 
            this.IsGroupCheckBox.AutoSize = true;
            this.IsGroupCheckBox.Enabled = false;
            this.IsGroupCheckBox.Location = new System.Drawing.Point(499, 88);
            this.IsGroupCheckBox.Name = "IsGroupCheckBox";
            this.IsGroupCheckBox.Size = new System.Drawing.Size(111, 17);
            this.IsGroupCheckBox.TabIndex = 4;
            this.IsGroupCheckBox.Text = "Групповой квест";
            this.IsGroupCheckBox.UseVisualStyleBackColor = true;
            this.IsGroupCheckBox.CheckedChanged += new System.EventHandler(this.IsGroupCheckBox_CheckedChanged);
            // 
            // lQuantity
            // 
            this.lQuantity.AutoSize = true;
            this.lQuantity.Enabled = false;
            this.lQuantity.Location = new System.Drawing.Point(356, 16);
            this.lQuantity.Name = "lQuantity";
            this.lQuantity.Size = new System.Drawing.Size(69, 13);
            this.lQuantity.TabIndex = 3;
            this.lQuantity.Text = "Количество:";
            // 
            // quantityUpDown
            // 
            this.quantityUpDown.Enabled = false;
            this.quantityUpDown.Location = new System.Drawing.Point(431, 13);
            this.quantityUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.quantityUpDown.Name = "quantityUpDown";
            this.quantityUpDown.Size = new System.Drawing.Size(60, 20);
            this.quantityUpDown.TabIndex = 2;
            // 
            // targetComboBox
            // 
            this.targetComboBox.DropDownWidth = 250;
            this.targetComboBox.Enabled = false;
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(103, 39);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(121, 21);
            this.targetComboBox.Sorted = true;
            this.targetComboBox.TabIndex = 1;
            this.targetComboBox.SelectedIndexChanged += new System.EventHandler(this.targetComboBox_SelectedIndexChanged);
            // 
            // lNameObject
            // 
            this.lNameObject.AutoSize = true;
            this.lNameObject.Enabled = false;
            this.lNameObject.Location = new System.Drawing.Point(15, 43);
            this.lNameObject.Name = "lNameObject";
            this.lNameObject.Size = new System.Drawing.Size(42, 13);
            this.lNameObject.TabIndex = 0;
            this.lNameObject.Text = "Таргет";
            // 
            // questInformationBox
            // 
            this.questInformationBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.questInformationBox.Controls.Add(this.tutorialCheckBox);
            this.questInformationBox.Controls.Add(this.loseRButton);
            this.questInformationBox.Controls.Add(this.winRButton);
            this.questInformationBox.Controls.Add(this.lFailed);
            this.questInformationBox.Controls.Add(this.lWin);
            this.questInformationBox.Controls.Add(this.bHideInformation);
            this.questInformationBox.Controls.Add(this.showJournalCheckBox);
            this.questInformationBox.Controls.Add(this.showCloseCheckBox);
            this.questInformationBox.Controls.Add(this.showFailedCheckBox);
            this.questInformationBox.Controls.Add(this.showTakeCheckBox);
            this.questInformationBox.Controls.Add(this.showWinCheckBox);
            this.questInformationBox.Controls.Add(this.showProgressCheckBox);
            this.questInformationBox.Controls.Add(this.descriptionTextBox);
            this.questInformationBox.Controls.Add(this.lDescription);
            this.questInformationBox.Controls.Add(this.onFailedTextBox);
            this.questInformationBox.Controls.Add(this.onWonTextBox);
            this.questInformationBox.Controls.Add(this.titleTextBox);
            this.questInformationBox.Controls.Add(this.lTitle);
            this.questInformationBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.questInformationBox.Location = new System.Drawing.Point(3, 16);
            this.questInformationBox.Name = "questInformationBox";
            this.questInformationBox.Size = new System.Drawing.Size(614, 238);
            this.questInformationBox.TabIndex = 0;
            this.questInformationBox.TabStop = false;
            this.questInformationBox.Text = "Информация";
            // 
            // tutorialCheckBox
            // 
            this.tutorialCheckBox.AutoSize = true;
            this.tutorialCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tutorialCheckBox.Location = new System.Drawing.Point(261, 143);
            this.tutorialCheckBox.Name = "tutorialCheckBox";
            this.tutorialCheckBox.Size = new System.Drawing.Size(81, 17);
            this.tutorialCheckBox.TabIndex = 13;
            this.tutorialCheckBox.Text = "Туториал";
            this.tutorialCheckBox.UseVisualStyleBackColor = true;
            this.tutorialCheckBox.CheckedChanged += new System.EventHandler(this.tutorialCheckBox_CheckedChanged);
            // 
            // loseRButton
            // 
            this.loseRButton.AutoSize = true;
            this.loseRButton.BackColor = System.Drawing.Color.Red;
            this.loseRButton.Location = new System.Drawing.Point(501, 63);
            this.loseRButton.Name = "loseRButton";
            this.loseRButton.Size = new System.Drawing.Size(78, 17);
            this.loseRButton.TabIndex = 12;
            this.loseRButton.TabStop = true;
            this.loseRButton.Text = "Проигрыш";
            this.loseRButton.UseVisualStyleBackColor = false;
            // 
            // winRButton
            // 
            this.winRButton.AutoSize = true;
            this.winRButton.BackColor = System.Drawing.Color.LimeGreen;
            this.winRButton.Location = new System.Drawing.Point(501, 40);
            this.winRButton.Name = "winRButton";
            this.winRButton.Size = new System.Drawing.Size(73, 17);
            this.winRButton.TabIndex = 11;
            this.winRButton.TabStop = true;
            this.winRButton.Text = "Выигрыш";
            this.winRButton.UseVisualStyleBackColor = false;
            // 
            // lFailed
            // 
            this.lFailed.AutoSize = true;
            this.lFailed.Location = new System.Drawing.Point(5, 214);
            this.lFailed.Name = "lFailed";
            this.lFailed.Size = new System.Drawing.Size(60, 13);
            this.lFailed.TabIndex = 10;
            this.lFailed.Text = "Проигрыш";
            // 
            // lWin
            // 
            this.lWin.AutoSize = true;
            this.lWin.Location = new System.Drawing.Point(5, 192);
            this.lWin.Name = "lWin";
            this.lWin.Size = new System.Drawing.Size(55, 13);
            this.lWin.TabIndex = 9;
            this.lWin.Text = "Выигрыш";
            // 
            // bHideInformation
            // 
            this.bHideInformation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bHideInformation.Location = new System.Drawing.Point(527, 9);
            this.bHideInformation.Name = "bHideInformation";
            this.bHideInformation.Size = new System.Drawing.Size(75, 23);
            this.bHideInformation.TabIndex = 8;
            this.bHideInformation.Text = "Скрыть";
            this.bHideInformation.UseVisualStyleBackColor = true;
            this.bHideInformation.Click += new System.EventHandler(this.bHideInformation_Click);
            // 
            // showJournalCheckBox
            // 
            this.showJournalCheckBox.AutoSize = true;
            this.showJournalCheckBox.Location = new System.Drawing.Point(72, 162);
            this.showJournalCheckBox.Name = "showJournalCheckBox";
            this.showJournalCheckBox.Size = new System.Drawing.Size(144, 17);
            this.showJournalCheckBox.TabIndex = 7;
            this.showJournalCheckBox.Text = "Показывать в журнале";
            this.showJournalCheckBox.UseVisualStyleBackColor = true;
            // 
            // showCloseCheckBox
            // 
            this.showCloseCheckBox.AutoSize = true;
            this.showCloseCheckBox.Location = new System.Drawing.Point(72, 143);
            this.showCloseCheckBox.Name = "showCloseCheckBox";
            this.showCloseCheckBox.Size = new System.Drawing.Size(179, 17);
            this.showCloseCheckBox.TabIndex = 6;
            this.showCloseCheckBox.Text = "Показывать закрытие квеста";
            this.showCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // showFailedCheckBox
            // 
            this.showFailedCheckBox.AutoSize = true;
            this.showFailedCheckBox.Location = new System.Drawing.Point(261, 124);
            this.showFailedCheckBox.Name = "showFailedCheckBox";
            this.showFailedCheckBox.Size = new System.Drawing.Size(143, 17);
            this.showFailedCheckBox.TabIndex = 5;
            this.showFailedCheckBox.Text = "Показывать проигрыш";
            this.showFailedCheckBox.UseVisualStyleBackColor = true;
            // 
            // showTakeCheckBox
            // 
            this.showTakeCheckBox.AutoSize = true;
            this.showTakeCheckBox.Location = new System.Drawing.Point(72, 124);
            this.showTakeCheckBox.Name = "showTakeCheckBox";
            this.showTakeCheckBox.Size = new System.Drawing.Size(165, 17);
            this.showTakeCheckBox.TabIndex = 5;
            this.showTakeCheckBox.Text = "Показывать взятие квеста";
            this.showTakeCheckBox.UseVisualStyleBackColor = true;
            // 
            // showWinCheckBox
            // 
            this.showWinCheckBox.AutoSize = true;
            this.showWinCheckBox.Location = new System.Drawing.Point(261, 105);
            this.showWinCheckBox.Name = "showWinCheckBox";
            this.showWinCheckBox.Size = new System.Drawing.Size(178, 17);
            this.showWinCheckBox.TabIndex = 4;
            this.showWinCheckBox.Text = "Показывать сообщ. выигрыш";
            this.showWinCheckBox.UseVisualStyleBackColor = true;
            // 
            // showProgressCheckBox
            // 
            this.showProgressCheckBox.AutoSize = true;
            this.showProgressCheckBox.Location = new System.Drawing.Point(72, 105);
            this.showProgressCheckBox.Name = "showProgressCheckBox";
            this.showProgressCheckBox.Size = new System.Drawing.Size(139, 17);
            this.showProgressCheckBox.TabIndex = 4;
            this.showProgressCheckBox.Text = "Показывать прогресс";
            this.showProgressCheckBox.UseVisualStyleBackColor = true;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(72, 37);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(407, 62);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.DoubleClick += new System.EventHandler(this.descriptionTextBox_DoubleClick);
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(3, 40);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(60, 13);
            this.lDescription.TabIndex = 2;
            this.lDescription.Text = "Описание:";
            // 
            // onFailedTextBox
            // 
            this.onFailedTextBox.Location = new System.Drawing.Point(72, 211);
            this.onFailedTextBox.Name = "onFailedTextBox";
            this.onFailedTextBox.Size = new System.Drawing.Size(407, 20);
            this.onFailedTextBox.TabIndex = 1;
            // 
            // onWonTextBox
            // 
            this.onWonTextBox.Location = new System.Drawing.Point(72, 185);
            this.onWonTextBox.Name = "onWonTextBox";
            this.onWonTextBox.Size = new System.Drawing.Size(407, 20);
            this.onWonTextBox.TabIndex = 1;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(72, 13);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(407, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTitle.Location = new System.Drawing.Point(3, 16);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(64, 13);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Заголовок:";
            // 
            // eventComboBox
            // 
            this.eventComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventComboBox.Location = new System.Drawing.Point(3, 29);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(620, 21);
            this.eventComboBox.TabIndex = 1;
            this.eventComboBox.SelectedIndexChanged += new System.EventHandler(this.eventComboBox_SelectedIndexChanged);
            // 
            // eventLabel
            // 
            this.eventLabel.AutoSize = true;
            this.eventLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventLabel.Location = new System.Drawing.Point(3, 16);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(75, 13);
            this.eventLabel.TabIndex = 0;
            this.eventLabel.Text = "Тип события:";
            // 
            // EditQuestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(626, 863);
            this.Controls.Add(this.QuestPanel);
            this.MinimumSize = new System.Drawing.Size(636, 850);
            this.Name = "EditQuestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Редактирование события";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestEditForm_FormClosing);
            this.QuestPanel.ResumeLayout(false);
            this.groupQuestBox.ResumeLayout(false);
            this.groupQuestBox.PerformLayout();
            this.filedIFtContentBox.ResumeLayout(false);
            this.filedIFtContentBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.rewardGroupBox.ResumeLayout(false);
            this.rewardGroupBox.PerformLayout();
            this.lQuestRules.ResumeLayout(false);
            this.lQuestRules.PerformLayout();
            this.groupQuestRulesBox.ResumeLayout(false);
            this.groupQuestRulesBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxGroup)).EndInit();
            this.preconditionGroupBox.ResumeLayout(false);
            this.preconditionGroupBox.PerformLayout();
            this.targetBox.ResumeLayout(false);
            this.targetBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityUpDown)).EndInit();
            this.questInformationBox.ResumeLayout(false);
            this.questInformationBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel QuestPanel;
        private System.Windows.Forms.GroupBox groupQuestBox;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.ComboBox eventComboBox;
        private System.Windows.Forms.GroupBox filedIFtContentBox;
        private System.Windows.Forms.GroupBox questInformationBox;
        private System.Windows.Forms.MaskedTextBox titleTextBox;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.GroupBox targetBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.Label lNameObject;
        private System.Windows.Forms.Label lQuantity;
        private System.Windows.Forms.NumericUpDown quantityUpDown;
        private System.Windows.Forms.GroupBox preconditionGroupBox;
        private System.Windows.Forms.ComboBox repeatComboBox;
        private System.Windows.Forms.Label lRepeat;
        private System.Windows.Forms.GroupBox lQuestRules;
        private System.Windows.Forms.GroupBox rewardGroupBox;
        private System.Windows.Forms.Label lCombatSkills;
        private System.Windows.Forms.TextBox tExperience;
        private System.Windows.Forms.TextBox creditsTextBox;
        private System.Windows.Forms.Label lCredits;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox IsGroupCheckBox;
        private System.Windows.Forms.Label labelScenarios;
        private System.Windows.Forms.CheckBox isClanCheckBox;
        private System.Windows.Forms.GroupBox groupQuestRulesBox;
        private System.Windows.Forms.Label labelMinGroup;
        private System.Windows.Forms.Label labelMaxGroup;
        private System.Windows.Forms.NumericUpDown numericMinGroup;
        private System.Windows.Forms.NumericUpDown numericMaxGroup;
        private System.Windows.Forms.Label labelTeleportTo;
        private System.Windows.Forms.ComboBox instanceComboBox;
        private System.Windows.Forms.ComboBox targetAttributeComboBox;
        private System.Windows.Forms.Label labelTargetAttr;
        private System.Windows.Forms.TextBox scenariosTextBox;
        private System.Windows.Forms.CheckBox showProgressCheckBox;
        private System.Windows.Forms.Label ltargetResult;
        private System.Windows.Forms.MaskedTextBox resultextBox;
        private System.Windows.Forms.Button bTargetClearDynamic;
        private System.Windows.Forms.Button bTargetAddDynamic;
        private System.Windows.Forms.CheckBox dynamicCheckBox;
        private System.Windows.Forms.ComboBox targetAttributeComboBox2;
        private System.Windows.Forms.Label lTargetAttr1;
        private System.Windows.Forms.CheckBox showTakeCheckBox;
        private System.Windows.Forms.CheckBox showCloseCheckBox;
        private System.Windows.Forms.CheckBox showJournalCheckBox;
        private System.Windows.Forms.Label lDaily;
        private System.Windows.Forms.TextBox takenPeriodTextBox;
        private System.Windows.Forms.Label lH;
        private System.Windows.Forms.Button bHideInformation;
        private System.Windows.Forms.Button bHideTarget;
        private System.Windows.Forms.Button bHideRules;
        private System.Windows.Forms.Button bHideReward;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxKarmaPK;
        private System.Windows.Forms.Label lKarmaPK;
        private System.Windows.Forms.Button bItemReward;
        private System.Windows.Forms.Button bItemQuestRules;
        private System.Windows.Forms.Button bItemQID;
        private System.Windows.Forms.Button bRewardFractions;
        private System.Windows.Forms.Label lFailed;
        private System.Windows.Forms.Label lWin;
        private System.Windows.Forms.MaskedTextBox onFailedTextBox;
        private System.Windows.Forms.MaskedTextBox onWonTextBox;
        private System.Windows.Forms.CheckBox showFailedCheckBox;
        private System.Windows.Forms.CheckBox showWinCheckBox;
        private System.Windows.Forms.RadioButton loseRButton;
        private System.Windows.Forms.RadioButton winRButton;
        private System.Windows.Forms.CheckBox tutorialCheckBox;
        private System.Windows.Forms.Button bRewardEffects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox difficultyComboBox;
        private System.Windows.Forms.TextBox tSupport;
        private System.Windows.Forms.TextBox tSurvival;
        private System.Windows.Forms.Label lSupportSkills;
        private System.Windows.Forms.Label lSurvivalSkills;
        private Telerik.WinControls.UI.RadMarkupDialog radMarkupDialog;
    }
}