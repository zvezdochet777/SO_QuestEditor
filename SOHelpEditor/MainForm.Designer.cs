namespace SOHelpEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtbText = new System.Windows.Forms.RichTextBox();
            this.treeChapters = new System.Windows.Forms.TreeView();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.changeLittleCard = new System.Windows.Forms.Button();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.btnLink = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnAddParagraph = new System.Windows.Forms.Button();
            this.btnAddSubTitle = new System.Windows.Forms.Button();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStripItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbText
            // 
            this.rtbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbText.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbText.Location = new System.Drawing.Point(221, -1);
            this.rtbText.Name = "rtbText";
            this.rtbText.Size = new System.Drawing.Size(813, 537);
            this.rtbText.TabIndex = 0;
            this.rtbText.Text = "";
            this.rtbText.Enter += new System.EventHandler(this.rtbText_Enter);
            this.rtbText.Leave += new System.EventHandler(this.rtbText_Leave);
            // 
            // treeChapters
            // 
            this.treeChapters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeChapters.HideSelection = false;
            this.treeChapters.Location = new System.Drawing.Point(1, -1);
            this.treeChapters.Name = "treeChapters";
            this.treeChapters.Size = new System.Drawing.Size(214, 537);
            this.treeChapters.TabIndex = 1;
            this.treeChapters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeChapters_AfterSelect);
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.SystemColors.HighlightText;
            // 
            // changeLittleCard
            // 
            this.changeLittleCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeLittleCard.Enabled = false;
            this.changeLittleCard.Location = new System.Drawing.Point(1036, 490);
            this.changeLittleCard.Name = "changeLittleCard";
            this.changeLittleCard.Size = new System.Drawing.Size(40, 40);
            this.changeLittleCard.TabIndex = 12;
            this.changeLittleCard.Text = "Little Card";
            this.toolTip.SetToolTip(this.changeLittleCard, "Редактировать текст всплывающей подсказки");
            this.changeLittleCard.UseVisualStyleBackColor = true;
            this.changeLittleCard.Click += new System.EventHandler(this.changeLittleCard_Click);
            // 
            // btnAddComment
            // 
            this.btnAddComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComment.CausesValidation = false;
            this.btnAddComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddComment.Location = new System.Drawing.Point(1037, 310);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(40, 40);
            this.btnAddComment.TabIndex = 15;
            this.btnAddComment.Text = "</>";
            this.toolTip.SetToolTip(this.btnAddComment, "Вставить комментарий");
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // btnLink
            // 
            this.btnLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLink.CausesValidation = false;
            this.btnLink.Image = global::SOHelpEditor.Properties.Resources.link;
            this.btnLink.Location = new System.Drawing.Point(1037, 430);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(40, 40);
            this.btnLink.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnLink, "Вставить ссылку на статью");
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Image = global::SOHelpEditor.Properties.Resources.rename;
            this.btnRename.Location = new System.Drawing.Point(1037, 257);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(40, 40);
            this.btnRename.TabIndex = 13;
            this.toolTip.SetToolTip(this.btnRename, "Переименовать параграф/главу");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnAddParagraph
            // 
            this.btnAddParagraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParagraph.Image = global::SOHelpEditor.Properties.Resources.plus1;
            this.btnAddParagraph.Location = new System.Drawing.Point(1037, 85);
            this.btnAddParagraph.Name = "btnAddParagraph";
            this.btnAddParagraph.Size = new System.Drawing.Size(40, 40);
            this.btnAddParagraph.TabIndex = 11;
            this.toolTip.SetToolTip(this.btnAddParagraph, "Добавить параграф");
            this.btnAddParagraph.UseVisualStyleBackColor = true;
            this.btnAddParagraph.Click += new System.EventHandler(this.btnAddParagraph_Click);
            // 
            // btnAddSubTitle
            // 
            this.btnAddSubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSubTitle.CausesValidation = false;
            this.btnAddSubTitle.Image = global::SOHelpEditor.Properties.Resources.subtitle;
            this.btnAddSubTitle.Location = new System.Drawing.Point(1037, 390);
            this.btnAddSubTitle.Name = "btnAddSubTitle";
            this.btnAddSubTitle.Size = new System.Drawing.Size(40, 40);
            this.btnAddSubTitle.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnAddSubTitle, "Вставить подзаголовок");
            this.btnAddSubTitle.UseVisualStyleBackColor = true;
            this.btnAddSubTitle.Click += new System.EventHandler(this.btnAddSubTitle_Click);
            // 
            // btnAddImage
            // 
            this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddImage.CausesValidation = false;
            this.btnAddImage.Image = global::SOHelpEditor.Properties.Resources.addImage;
            this.btnAddImage.Location = new System.Drawing.Point(1037, 350);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(40, 40);
            this.btnAddImage.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnAddImage, "Вставить изображение");
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Enabled = false;
            this.btnDown.Image = global::SOHelpEditor.Properties.Resources.down;
            this.btnDown.Location = new System.Drawing.Point(1037, 211);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(40, 40);
            this.btnDown.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnDown, "Опустить вниз");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Enabled = false;
            this.btnUp.Image = global::SOHelpEditor.Properties.Resources.up;
            this.btnUp.Location = new System.Drawing.Point(1037, 171);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(40, 40);
            this.btnUp.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnUp, "Поднять вверх");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Image = global::SOHelpEditor.Properties.Resources.minus;
            this.btnRemove.Location = new System.Drawing.Point(1037, 125);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(40, 40);
            this.btnRemove.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnRemove, "Убрать главу");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::SOHelpEditor.Properties.Resources.plus;
            this.btnAdd.Location = new System.Drawing.Point(1037, 44);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 40);
            this.btnAdd.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnAdd, "Добавить главу");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::SOHelpEditor.Properties.Resources.save1;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSave.Location = new System.Drawing.Point(1037, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(1368, 523);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripItemCut,
            this.menuStripItemCopy,
            this.menuStripItemPaste});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // menuStripItemCut
            // 
            this.menuStripItemCut.Name = "menuStripItemCut";
            this.menuStripItemCut.Size = new System.Drawing.Size(180, 22);
            this.menuStripItemCut.Text = "Вырезать";
            this.menuStripItemCut.Click += new System.EventHandler(this.menuStripItemCut_Click);
            // 
            // menuStripItemCopy
            // 
            this.menuStripItemCopy.Name = "menuStripItemCopy";
            this.menuStripItemCopy.Size = new System.Drawing.Size(180, 22);
            this.menuStripItemCopy.Text = "Копировать";
            this.menuStripItemCopy.Click += new System.EventHandler(this.menuStripItemCopy_Click);
            // 
            // menuStripItemPaste
            // 
            this.menuStripItemPaste.Name = "menuStripItemPaste";
            this.menuStripItemPaste.Size = new System.Drawing.Size(180, 22);
            this.menuStripItemPaste.Text = "Вставить";
            this.menuStripItemPaste.Click += new System.EventHandler(this.menuStripItemPaste_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 533);
            this.Controls.Add(this.btnAddComment);
            this.Controls.Add(this.btnLink);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.changeLittleCard);
            this.Controls.Add(this.btnAddParagraph);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.btnAddSubTitle);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.treeChapters);
            this.Controls.Add(this.rtbText);
            this.Name = "MainForm";
            this.Text = "HelpEditor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbText;
        private System.Windows.Forms.TreeView treeChapters;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnAddSubTitle;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button btnAddParagraph;
        private System.Windows.Forms.Button changeLittleCard;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemCut;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemCopy;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemPaste;
    }
}

