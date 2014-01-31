using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StalkerOnlineQuesterEditor.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class ItemDialog : Form
    {
        int ITEM_REWARD = 0;
        int ITEM_QUESTRULES = 1;
        int ITEM_LOCALIZATION_RULES = 2;
        int ITEM_LOCALIZATION_REWARD = 3;

        int questID;
        public MainForm parent;
        EditQuestForm parentForm;
        LocaleQuestForm parentForm2;

        //List<string> locale_title;
        //List<string> locale_description;

        Dictionary<int, QuestItemInfo> locale;

        int translate = 0;

        int formType;

        //public void click_change_qids(object sender, EventArgs e)
        //{
        //    this.Enabled = false;
        //    SelectQuestItem sitem = new SelectQuestItem(this, questID);
        //}

        void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (formType == this.ITEM_QUESTRULES)
            {
                if (e.ColumnIndex == 1) // your combo column index 
                {

                    e.Value = "Квестовый";

                }
            }

        }

        public ItemDialog(MainForm parent, EditQuestForm parentForm, LocaleQuestForm parentForm2, int questID, int type)
        {
            InitializeComponent();
            itemGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(CellFormatting);
            //itemGridView.CellBeginEdit += CellFormatting;
            this.Enabled = false;
            this.parent = parent;
            this.parentForm2 = parentForm2;
            this.parentForm = parentForm;
            this.formType = type;
            this.questID = questID;

            locale = new Dictionary<int, QuestItemInfo>();

            foreach (CItem item in parent.itemConst.getAllItems().Values)
                ((DataGridViewComboBoxColumn)itemGridView.Columns[0]).Items.Add(item.getDescription());
            ((DataGridViewComboBoxColumn)itemGridView.Columns[0]).Sorted = true;
            if (formType == this.ITEM_LOCALIZATION_REWARD || formType == this.ITEM_LOCALIZATION_RULES)
            {
                bTranslate.Enabled = true;
                if (formType == this.ITEM_LOCALIZATION_REWARD)
                {
                    this.locale = parentForm2.cur_locale_quest.QuestInformation.Items;
                }
                else
                {
                    this.locale = parentForm2.cur_locale_quest.QuestInformation.Items;
                }
            }
            fillItemGrid();
        }
        
        
        
        private void fillItemGrid()
       {
            System.Console.WriteLine("ItemDialog::fillItemGrid");

           itemGridView.Rows.Clear();


            if (formType == this.ITEM_REWARD)
            {
                System.Console.WriteLine("formType::ITEM_REWARD");

                this.Text += " Награда";


                for (int i = 0; i< parentForm.editQuestReward.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm.editQuestReward.TypeOfItems[i];
                    int quantity = parentForm.editQuestReward.NumOfItems[i];
                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr;
                    try
                    {
                        if (parentForm.editQuestReward.AttrOfItems[i].Equals(1))
                        {
                            attr = "Квестовый";
                        }
                        else
                        {
                            attr = "Обычный";
                        }
                    }
                    catch
                    {
                        attr = "Обычный";
                    }
                    string title = "";
                    string description = "";
                    string activation = "";
                    if (parentForm.quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        title = parentForm.quest.QuestInformation.Items[typeID].title;
                        description = parentForm.quest.QuestInformation.Items[typeID].description;
                        activation = parentForm.quest.QuestInformation.Items[typeID].activation;
                    }


                    object[] row = { name, attr, quantity.ToString(), title, description, activation };
                    itemGridView.Rows.Add(row);

                }   

            }

            else if (formType == this.ITEM_QUESTRULES)
            {

                System.Console.WriteLine("formType::ITEM_QUESTRULES");
                this.Text += " Правила квеста";
                for (int i = 0; i < parentForm.editQuestRules.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm.editQuestRules.TypeOfItems[i];
                    int quantity = parentForm.editQuestRules.NumOfItems[i];


                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr = "Квестовый";

                    string title = "";
                    string description = "";
                    string activation = "";
                    if (parentForm.quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        title = parentForm.quest.QuestInformation.Items[typeID].title;
                        description = parentForm.quest.QuestInformation.Items[typeID].description;
                        activation = parentForm.quest.QuestInformation.Items[typeID].activation;
                    }
                    object[] row = { name, attr, quantity.ToString(), title, description, activation };
                    itemGridView.Rows.Add(row);

               }
                itemGridView.Columns[1].ReadOnly = true;
            }
            else if (formType == this.ITEM_LOCALIZATION_REWARD)
            {
                System.Console.WriteLine("formType::ITEM_LOCALIZATION_REWARD");
                this.Text = "Локализация награды";
                for (int i = 0; i < parentForm2.pub_quest.Reward.TypeOfItems.Count; ++i)
                {

                    int typeID = parentForm2.pub_quest.Reward.TypeOfItems[i];
                    int quantity = parentForm2.pub_quest.Reward.NumOfItems[i];
                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr;
                    try
                    {
                        if (parentForm2.pub_quest.Reward.AttrOfItems[i].Equals(1))
                        {
                            attr = "Квестовый";
                        }
                        else
                        {
                            attr = "Обычный";
                        }
                    }
                    catch
                    {
                        attr = "Обычный";
                    }
                    string title = "";
                    string description = "";
                    string activation = "";
                    if (parentForm2.pub_quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        if (this.translate == 0)
                        {
                            title = parentForm2.pub_quest.QuestInformation.Items[typeID].title;
                            description = parentForm2.pub_quest.QuestInformation.Items[typeID].description;
                        }
                        else
                        {
                            title = this.locale[typeID].title;
                            description = this.locale[typeID].description;
                            activation = this.locale[typeID].activation;
                        }
                    }
                    object[] row = { name, attr, quantity.ToString(), title, description, activation };
                    itemGridView.Rows.Add(row);

                    for (int row_index = 0; row_index < itemGridView.Rows.Count; row_index ++ )
                    {
                        if (translate == 1)
                            itemGridView.Rows[row_index].ReadOnly = false;
                        else
                        {
                            if (row_index != 4 && row_index != 5)
                                itemGridView.Rows[row_index].ReadOnly = false;
                            else
                                itemGridView.Rows[row_index].ReadOnly = true;
                        }
                    }
                }
            }
            else if (formType == this.ITEM_LOCALIZATION_RULES)
            {
                System.Console.WriteLine("formType::ITEM_LOCALIZATION_RULES");
                this.Text = "Локализация правил квеста";
                for (int i = 0; i < parentForm2.pub_quest.QuestRules.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm2.pub_quest.QuestRules.TypeOfItems[i];
                    int quantity = parentForm2.pub_quest.QuestRules.NumOfItems[i];


                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr = "Квестовый";

                    string title = "";
                    string description = "";
                    string activation = "";
                    if (parentForm2.pub_quest.QuestInformation.Items.Keys.Contains(typeID))
                    {

                        if (this.translate == 0)
                        {
                            title = parentForm2.pub_quest.QuestInformation.Items[typeID].title;
                            description = parentForm2.pub_quest.QuestInformation.Items[typeID].description;
                        }
                        else
                        {
                            title = this.locale[typeID].title;
                            description = this.locale[typeID].description;
                            activation = this.locale[typeID].activation;
                        }
                    }
                    object[] row = { name, attr, quantity.ToString(), title, description, activation };
                    itemGridView.Rows.Add(row);

                }
                for (int row_index = 0; row_index < itemGridView.Rows.Count; row_index++)
                {
                    if (translate == 1)
                        itemGridView.Rows[row_index].ReadOnly = false;
                    else
                    {
                        if (row_index != 4 && row_index != 5)
                            itemGridView.Rows[row_index].ReadOnly = false;
                        else
                            itemGridView.Rows[row_index].ReadOnly = true;
                    }
                }
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> typeOfItems = new List<int>();
            List<int> numOfItems = new List<int>();
            List<int> attrOfItems = new List<int>();

            Dictionary<int, QuestItemInfo> itemsInfo = new Dictionary<int, QuestItemInfo>();

                
            foreach (DataGridViewRow row in itemGridView.Rows)
            {
                string typeName = row.Cells[0].FormattedValue.ToString();
                
                if (!typeName.Equals(""))
                {
                    int quantity = int.Parse(row.Cells[2].FormattedValue.ToString());
                    if (quantity >= 1)
                    {
                        int typeID = parent.itemConst.getIDOnDescription(typeName);

                        string attrName = row.Cells[1].FormattedValue.ToString();
                        int attr;
                        string title = row.Cells[3].FormattedValue.ToString();
                        string description = row.Cells[4].FormattedValue.ToString();
                        string activation = row.Cells[5].FormattedValue.ToString();
                        if (attrName.Equals("Квестовый"))
                        {
                            attr = 1;
                            try
                            {
                                itemsInfo.Add(typeID, new QuestItemInfo(title, description, activation));
                            }
                            catch
                            {
                                MessageBox.Show("Есть идентичные квестовые предметы.", "Ошибка.");
                                return;
                            }
                        }
                        else
                            attr = 0;

                        typeOfItems.Add(typeID);
                        numOfItems.Add(quantity);
                        attrOfItems.Add(attr);
                    }
                }
            }

            if (formType == this.ITEM_QUESTRULES)
            {
                parentForm.editQuestRules.TypeOfItems.Clear();
                parentForm.editQuestRules.TypeOfItems = typeOfItems;
                parentForm.editQuestRules.NumOfItems.Clear();
                parentForm.editQuestRules.NumOfItems = numOfItems;
                parentForm.editQuestRules.AttrOfItems.Clear();
                parentForm.editQuestRules.AttrOfItems = attrOfItems;
                parentForm.checkQuestRulesIndicates();
            }
            else if (formType == this.ITEM_REWARD)
            {
                parentForm.editQuestReward.TypeOfItems.Clear();
                parentForm.editQuestReward.TypeOfItems = typeOfItems;
                parentForm.editQuestReward.NumOfItems.Clear();
                parentForm.editQuestReward.NumOfItems = numOfItems;
                parentForm.editQuestReward.AttrOfItems.Clear();
                parentForm.editQuestReward.AttrOfItems = attrOfItems;
                parentForm.checkRewardIndicates();
            }
            else if (formType == this.ITEM_LOCALIZATION_RULES || formType == this.ITEM_LOCALIZATION_REWARD)
            {
                if (this.translate == 1)
                    parseLocale();
                parentForm2.cur_locale_quest.QuestInformation.Items = this.locale;
            }
            if (parentForm != null)
            {
                parentForm.editInformation.Items.Clear();
                parentForm.editInformation.Items = itemsInfo;
            }

            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ItemDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (parentForm != null)
                parentForm.Enabled = true;
            else
                parentForm2.Enabled = true;
        }

        void parseLocale()
        {
            foreach (DataGridViewRow row in itemGridView.Rows)
            {
                string title = row.Cells[3].FormattedValue.ToString();
                string description = row.Cells[4].FormattedValue.ToString();
                string activation = row.Cells[5].FormattedValue.ToString();
                string typeName = row.Cells[0].FormattedValue.ToString();
                int typeID = parent.itemConst.getIDOnDescription(typeName);
                if (!this.locale.Keys.Contains(typeID))
                    this.locale.Add(typeID, new QuestItemInfo());
                this.locale[typeID].description = description;
                this.locale[typeID].title = title;
                this.locale[typeID].activation = activation;

            }
        }

        private void bTranslate_Click(object sender, EventArgs e)
        {
            if (this.translate == 0)
            {
                this.translate = 1;
                bTranslate.Text = "Оригинал";
                fillItemGrid();
            }
            else
            {
                this.translate = 0;
                bTranslate.Text = "Перевод";
                parseLocale();
                fillItemGrid();
            }
        }
    }
}