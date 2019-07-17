using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using StalkerOnlineQuesterEditor.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class ItemDialog : Form
    {
        int ITEM_REWARD = 0;
        int ITEM_QUESTRULES = 1;
        int ITEM_LOCALIZATION_RULES = 2;
        int ITEM_LOCALIZATION_REWARD = 3;
        int ITEM_PENALTY = 4;

        int questID;
        public MainForm parent;
        EditQuestForm parentForm;
        LocaleQuestForm parentForm2;

        //List<string> locale_title;
        //List<string> locale_description;

        Dictionary<int, QuestItemInfo> locale;
        int translate = 0;
        int formType;

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
            itemGridView.Rows.Clear();
            if (formType == this.ITEM_REWARD)
            {
//                System.Console.WriteLine("formType::ITEM_REWARD");
                this.Text += " Награда";
                itemGridView.Columns["itemProbability"].Visible = true;
                for (int i = 0; i < parentForm.editQuestReward.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm.editQuestReward.TypeOfItems[i];
                    int quantity = parentForm.editQuestReward.NumOfItems[i];
                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr;
                    try
                    {
                        switch (parentForm.editQuestReward.AttrOfItems[i])
                        {
                            case 1:
                                attr = "Квестовый";
                                break;
                            case 2:
                                attr = "Авто";
                                break;
                            default:
                                attr = "Обычный";
                                break;
                        }
                    }
                    catch
                    {
                        attr = "Обычный";
                    }
                    string title = "";
                    string description = "";
                    string activation = "";
                    string probability = "1";
                    if (parentForm.quest.Reward.Probability.Count > 0)
                        probability = parentForm.quest.Reward.Probability[i].ToString();
                    if (parentForm.quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        title = parentForm.quest.QuestInformation.Items[typeID].title;
                        description = parentForm.quest.QuestInformation.Items[typeID].description;
                        activation = parentForm.quest.QuestInformation.Items[typeID].activation;
                    }
                    object[] row = { name, attr, quantity.ToString(), probability, title, description, activation };
                    itemGridView.Rows.Add(row);
                }
            }

            else if (formType == this.ITEM_PENALTY)
            {
                //                System.Console.WriteLine("formType::ITEM_REWARD");
                this.Text += " Награда";
                itemGridView.Columns["itemProbability"].Visible = true;
                for (int i = 0; i < parentForm.editQuestReward.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm.editQuestPenalty.TypeOfItems[i];
                    int quantity = parentForm.editQuestPenalty.NumOfItems[i];
                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr;
                    try
                    {
                        switch (parentForm.editQuestPenalty.AttrOfItems[i])
                        {
                            case 1:
                                attr = "Квестовый";
                                break;
                            case 2:
                                attr = "Авто";
                                break;
                            default:
                                attr = "Обычный";
                                break;
                        }
                    }
                    catch
                    {
                        attr = "Обычный";
                    }
                    string title = "";
                    string description = "";
                    string content = "";
                    string activation = "";
                    string probability = "1";

                    if (parentForm.quest.QuestPenalty.Probability.Count > 0)
                        probability = parentForm.quest.QuestPenalty.Probability[i].ToString();
                    if (parentForm.quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        title = parentForm.quest.QuestInformation.Items[typeID].title;
                        description = parentForm.quest.QuestInformation.Items[typeID].description;
                        content = parentForm.quest.QuestInformation.Items[typeID].content;
                        activation = parentForm.quest.QuestInformation.Items[typeID].activation;
                    }
                    object[] row = { name, attr, quantity.ToString(), probability, title, description, content, activation };
                    itemGridView.Rows.Add(row);
                }
            }

            else if (formType == this.ITEM_QUESTRULES)
            {
//                System.Console.WriteLine("formType::ITEM_QUESTRULES");
                this.Text += " Правила квеста";
                itemGridView.Columns["itemProbability"].Visible = false;
                for (int i = 0; i < parentForm.editQuestRules.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm.editQuestRules.TypeOfItems[i];
                    int quantity = parentForm.editQuestRules.NumOfItems[i];

                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr = "Квестовый";

                    string title = "";
                    string description = "";
                    string content = "";
                    string activation = "";
                    if (parentForm.quest.QuestInformation.Items.Keys.Contains(typeID))
                    {
                        title = parentForm.quest.QuestInformation.Items[typeID].title;
                        description = parentForm.quest.QuestInformation.Items[typeID].description;
                        content = parentForm.quest.QuestInformation.Items[typeID].content;
                        activation = parentForm.quest.QuestInformation.Items[typeID].activation;
                    }
                    object[] row = { name, attr, quantity.ToString(), null, title, description, content, activation };
                    itemGridView.Rows.Add(row);
               }
                itemGridView.Columns[1].ReadOnly = true;
            }
            else if (formType == this.ITEM_LOCALIZATION_REWARD)
            {
//                System.Console.WriteLine("formType::ITEM_LOCALIZATION_REWARD");
                this.Text = "Локализация награды";
                for (int i = 0; i < parentForm2.pub_quest.Reward.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm2.pub_quest.Reward.TypeOfItems[i];
                    int quantity = parentForm2.pub_quest.Reward.NumOfItems[i];
                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr;
                    try
                    {
                        switch (parentForm2.pub_quest.Reward.AttrOfItems[i])
                        {
                            case 1:
                                attr = "Квестовый";
                                break;
                            case 2:
                                attr = "Авто";
                                break;
                            default:
                                attr = "Обычный";
                                break;
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
                    object[] row = { name, attr, quantity.ToString(), null, title, description, activation };
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
                //System.Console.WriteLine("formType::ITEM_LOCALIZATION_RULES");
                this.Text = "Локализация правил квеста";
                for (int i = 0; i < parentForm2.pub_quest.QuestRules.TypeOfItems.Count; ++i)
                {
                    int typeID = parentForm2.pub_quest.QuestRules.TypeOfItems[i];
                    int quantity = parentForm2.pub_quest.QuestRules.NumOfItems[i];

                    string name = parent.itemConst.getDescriptionOnID(typeID);
                    string attr = "Квестовый";

                    string title = "";
                    string description = "";
                    string content = "";
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
                            if (!this.locale.ContainsKey(typeID))
                            {
                                this.locale.Add(typeID, new QuestItemInfo());
                            }
                            title = this.locale[typeID].title;
                            description = this.locale[typeID].description;
                            content = this.locale[typeID].content;
                            activation = this.locale[typeID].activation;
                        }
                    }
                    object[] row = { name, attr, quantity.ToString(), null, title, description, content, activation };
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
                string title = row.Cells["itemTitle"].FormattedValue.ToString();
                string description = row.Cells["itemDescription"].FormattedValue.ToString();
                string activation = row.Cells["itemActivation"].FormattedValue.ToString();
                string typeName = row.Cells["itemType"].FormattedValue.ToString();
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
        //! Нажатие ОК - проверка и сохранение данных, выход в форму редактирования квеста
        private void bOk_Click(object sender, EventArgs e)
        {
            List<int> typeOfItems = new List<int>();
            List<int> numOfItems = new List<int>();
            List<int> attrOfItems = new List<int>();
            List<float> probabilityOfItems = new List<float>();
            Dictionary<int, QuestItemInfo> itemsInfo = new Dictionary<int, QuestItemInfo>();

            foreach (DataGridViewRow row in itemGridView.Rows)
            {
                string typeName = row.Cells["itemType"].FormattedValue.ToString();
                if (!typeName.Equals(""))
                {
                    int quantity = int.Parse(row.Cells["itemQuantity"].FormattedValue.ToString());
                    if (quantity >= 1)
                    {
                        int typeID = parent.itemConst.getIDOnDescription(typeName);
                        string attrName = row.Cells["itemAttr"].FormattedValue.ToString();
                        int attr;
                        string title = row.Cells["itemTitle"].FormattedValue.ToString();
                        string description = row.Cells["itemDescription"].FormattedValue.ToString();
                        string activation = row.Cells["itemActivation"].FormattedValue.ToString();
                        string content = row.Cells["itemContent"].FormattedValue.ToString();
                        float probability = checkFloatValue(row.Cells["itemProbability"].FormattedValue.ToString());
                        if (probability < 0)
                            return;
                        switch (attrName)
                        {
                            case "Квестовый":
                                attr = 1;
                                try
                                {
                                    itemsInfo.Add(typeID, new QuestItemInfo(title, description, activation, content));
                                }
                                catch
                                {
                                    MessageBox.Show("Есть идентичные квестовые предметы.", "Ошибка.");
                                    return;
                                }
                                break;
                            case "Авто":
                                attr = 2;
                                break;
                            default:
                                attr = 0;
                                break;
                        }

                        probabilityOfItems.Add(probability);
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
                parentForm.editQuestReward.Probability.Clear();
                parentForm.editQuestReward.Probability = probabilityOfItems;
                parentForm.checkRewardIndicates();
            }
            else if (formType == this.ITEM_PENALTY)
            {
                parentForm.editQuestPenalty.TypeOfItems.Clear();
                parentForm.editQuestPenalty.TypeOfItems = typeOfItems;
                parentForm.editQuestPenalty.NumOfItems.Clear();
                parentForm.editQuestPenalty.NumOfItems = numOfItems;
                parentForm.editQuestPenalty.AttrOfItems.Clear();
                parentForm.editQuestPenalty.AttrOfItems = attrOfItems;
                parentForm.editQuestPenalty.Probability.Clear();
                parentForm.editQuestPenalty.Probability = probabilityOfItems;
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
        //! Проверка, что в поле Вероятность ввели корректное число
        float checkFloatValue(string value)
        {            
            float result;
            value = value.Replace(",", ".");
            if (!float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                MessageBox.Show("Поле вероятность содержит недопустимые символы","Ошибка!");
                return -1.0f;
            }

            if (result > 1)
            {
                MessageBox.Show("Вероятность не может быть больше 1", "Ошибка!");
                return -1.0f;
            }
            if (result < 0)
            {
                MessageBox.Show("Вероятность не может быть меньше 0", "Ошибка!");
                return -1.0f;
            }

            return result;
        }
    }
}