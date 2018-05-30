using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class CheckErrorForm : Form
    {
        protected CDialogs dialogs;
        protected CQuests quests;
        private MainForm parent;
        private List<int> list_quest_id = new List<int>();
        private List<int> deleted_quests_ids = new List<int>();
        string JSON_PATH = "settings/ignore_ids.json";
        string DELETED_PATH = "../../../res/scripts/common/QuestsToRemove.py";
        private Dictionary<string, List<int>> ignores_id = new Dictionary<string, List<int>>();

        public CheckErrorForm(CDialogs dialogs, CQuests quests, MainForm parent)
        {
            InitializeComponent();
            this.dialogs = dialogs;
            this.quests = quests;
            this.parent = parent;
            readIgnoresIDs();
            readDeletedQuests();
        }


        private void readDeletedQuests()
        {
             if (!File.Exists(DELETED_PATH))
                return;

            string line;
            StreamReader reader = new StreamReader(DELETED_PATH);
            while((line = reader.ReadLine()) != null)
            {

                if((!line.Any()) || (line[0] == '#')) continue;
                string[] lines;
                lines = line.Split(',');
                foreach (string _num in lines)
                {
                    int i = 0;
                    int.TryParse(_num, out i);
                    if (i != 0) deleted_quests_ids.Add(i);
                }
            }
            reader.Close();
        }

        private void readIgnoresIDs()
        {
            JsonTextReader reader;
            if (!File.Exists(JSON_PATH))
                return;

            bool dialogs = false;
 
            reader = new JsonTextReader(new StreamReader(JSON_PATH, Encoding.UTF8));
            while (reader.Read())
            {
                if ((reader.TokenType == JsonToken.PropertyName) && (reader.Value.ToString() != "quests"))
                    dialogs = true;
                else if (reader.TokenType == JsonToken.Integer)
                {
                    if (dialogs)
                    {
                        if (!ignores_id.ContainsKey("dialogs"))
                            ignores_id.Add("dialogs", new List<int>());
                        ignores_id["dialogs"].Add(Convert.ToInt32(reader.ReadAsInt32()));
                    }
                    else
                    {
                        if (!ignores_id.ContainsKey("quests"))
                            ignores_id.Add("quests", new List<int>());
                        ignores_id["quests"].Add(Convert.ToInt32(reader.Value));
                    }
                }

            }
        }

        private void saveIgnoresIDs()
        {
            using (JsonWriter writer = new JsonTextWriter(new StreamWriter(JSON_PATH)))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                foreach(KeyValuePair<string, List<int>> val in ignores_id)
                {
                    writer.WritePropertyName(val.Key);
                    writer.WriteStartArray();
                    foreach (int id in val.Value)
                    {
                        writer.WriteValue(id);
                    }
                    writer.WriteEnd();
                }
                writer.WriteEndObject();
            }
        }

        private bool wasIgnoredQuestID(int quest_id)
        {
            if (!ignores_id.ContainsKey("quests"))
                return false;

            return ignores_id["quests"].Contains(quest_id);
        }

        private bool checkQuestIDOnGet(int quest_id, string npc_name, string dialog_id)
        {
            CQuest quest = quests.getQuest(quest_id);
            if (quest == null)
            {
                string line = "NPC:" + npc_name + "\t\tДиалогID: " + dialog_id + "\t\tКвест №" + quest_id.ToString() + " не существует, а проверяется";
                //Thread.Sleep(100);
                this.writeToLog(line);
                return true;
            }
            if (quest.Additional.IsSubQuest != 0) return true;

            foreach (KeyValuePair<string, Dictionary<int, CDialog>> npc in dialogs.dialogs)
            {
                foreach (CDialog dia in npc.Value.Values)
                {
                    if (dia.Actions.GetQuests.Contains(quest_id))
                        return true;
                }
            }

            if (parent != null)
                return parent.zoneConst.checkAreaGiveQuestByID(quest_id);

            return false; 
        }


        delegate void WriteToLogDelegate(string message, int quest_id = 0);

        private void writeToLog(string text, int quest_id = 0)
        {
            if (lbLog.InvokeRequired)
            {
                var _writeToLog = new WriteToLogDelegate(writeToLog);
                lbLog.Invoke(_writeToLog, text, quest_id);
            }
            else
            {
                if (!lbLog.Items.Contains(text))
                {
                    lbLog.Items.Add(text);
                    list_quest_id.Add(quest_id);
                }    
            }
        }

        delegate void doProgressDelegate(int value);
        private void doProgress(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                var _writeToLog = new doProgressDelegate(doProgress);
                progressBar1.Invoke(_writeToLog, value);
            }
            else
            {
                progressBar1.Value = value; ;
            }
        }

        delegate void setLabelDelegate(string value);
        private void setLabel(string value)
        {
            if (lCurrentCheck.InvokeRequired)
            {
                var _writeToLog = new setLabelDelegate(setLabel);
                lCurrentCheck.Invoke(_writeToLog, value);
            }
            else
            {
                lCurrentCheck.Text = value;
            }
        }

        private void checkErrors()
        {
            int[] quest_types = new int[] { 0, 13, 14, 22, 2, 5 };
            int count = dialogs.dialogs.Count;
            int i = 0;
            
            setLabel("Проверяем квесты в диалогах");
            foreach (KeyValuePair<string, Dictionary<int, CDialog>> npc in dialogs.dialogs)
            {
                i++;
                doProgress(50 * Convert.ToInt32(Convert.ToDouble(i) / count));
                foreach (KeyValuePair<int, CDialog> dia in npc.Value)
                {
                    List<int> check_list = new List<int>();
                    check_list.AddRange(dia.Value.Precondition.ListOfMustNoQuests.ListOfCompletedQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfMustNoQuests.ListOfOpenedQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfMustNoQuests.ListOfFailQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfMustNoQuests.ListOfOnTestQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfMustNoQuests.ListOfCompletedQuests);

                    check_list.AddRange(dia.Value.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfNecessaryQuests.ListOfFailQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests);
                    check_list.AddRange(dia.Value.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests);

                    foreach (int quest_id in check_list)
                    {
                        if (wasIgnoredQuestID(quest_id))
                            continue;
                        if (deleted_quests_ids.Contains(quest_id))
                        {
                            string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tКвест №" + quest_id.ToString() + " находится в списке автозавершения";
                            this.writeToLog(line, quest_id);
                        }
                        if (!checkQuestIDOnGet(quest_id, npc.Key, dia.Key.ToString()))
                        {
                            string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tКвест №" + quest_id.ToString() + " не выдаётся";
                            //Thread.Sleep(100);
                            this.writeToLog(line, quest_id);
                        }
                    }


                    //dia.Value.Actions.GetQuests

                    foreach(int complete_quest_id in dia.Value.Actions.CompleteQuests)
                    {
                        CQuest complete_quest = parent.getQuestOnQuestID(complete_quest_id);
                        if (complete_quest == null)
                        {
                            string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tКвест №" + complete_quest_id + " выдаётся и не существует";
                            this.writeToLog(line);
                            continue;
                        }
                        if (complete_quest.Reward.Any())
                        {
                            if (dia.Value.Actions.GetQuests.Contains(complete_quest_id))
                            {
                                string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tКвест №" + complete_quest_id + " даётся и завершается в одном ноде диалога";
                                this.writeToLog(line);
                            }

                            if (complete_quest.Additional.IsSubQuest != 0)
                            {
                                int parent_id = complete_quest.Additional.IsSubQuest;
                                CQuest parent_complete_quest = parent.getQuestOnQuestID(parent_id);
                                if (parent_complete_quest.Additional.ListOfSubQuest.Count == 1)
                                {
                                    string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tКвест №" + parent_id + " даётся, а его единственное событие " +complete_quest_id +" завершается";
                                    this.writeToLog(line);
                                }
                            }
                        }
                    }
                    List<DialogEffect> check_eff_list = new List<DialogEffect>();
                    check_eff_list.AddRange(dia.Value.Precondition.NecessaryEffects);
                    check_eff_list.AddRange(dia.Value.Precondition.MustNoEffects);

                    foreach (DialogEffect effect in check_eff_list)
                    {
                        if (!this.parent.effects.hasEffectById(effect.getID()))
                        {
                            string line = "NPC:" + npc.Key + "\t\tДиалогID: " + dia.Key.ToString() + "\t\tЭффекта №" + effect.getID() + " нет, а проверяется";
                            //Thread.Sleep(100);
                            this.writeToLog(line);
                        }
                    }

                }

            }

            count = quests.quest.Count;
            i = 0;
            setLabel("Проверяем квесты");
            Dictionary<int, CItem> items = this.parent.itemConst.getAllItems();
            foreach (KeyValuePair<int, CQuest> quest in quests.quest)
            {
                i++;
                doProgress(50 + 50 * Convert.ToInt32(Convert.ToDouble(i) / count));
                if (wasIgnoredQuestID(quest.Key))
                    continue;
                if ((quest.Value.Target.QuestType == 0) || (quest.Value.Target.QuestType == 16) || (quest.Value.Target.QuestType == 7))
                {
                    int item_id = quest.Value.Target.ObjectType;
                    if ((item_id != 0) && (!items.ContainsKey(item_id)))
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\t\t\tпредмета цели type:" + item_id + " не существует";
                       this.writeToLog(line, quest.Key);
                        continue;
                    }
                }
                else if ((quest.Value.Target.QuestType == 4) || (quest.Value.Target.QuestType == 8))
                {
                    string zone = quest.Value.Target.ObjectName;
                    if (zone.Any())
                        if (!this.parent.zoneConst.checkHaveArea(zone))
                        {
                            string line = "Квест №:" + quest.Key.ToString() + "\tимеет в целях зону: \"" + quest.Value.Target.ObjectName + "\", которой нигде нет";
                           this.writeToLog(line, quest.Key);
                        }
                }
                else if (quest.Value.Target.QuestType == 6)
                {
                    if (parent.triggerConst.getDescriptionOnId(quest.Value.Target.ObjectType) == "")
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tимеет в целях триггер: \"" + quest.Value.Target.ObjectType.ToString() + "\", которого нет";
                       this.writeToLog(line, quest.Key);
                    }
                }
                else if ((quest.Value.Target.QuestType == 2) || (quest.Value.Target.QuestType == 3))
                {
                    if (quest.Value.Target.AreaName.Any())
                        if (!this.parent.zoneConst.checkHaveArea(quest.Value.Target.AreaName))
                        {
                            string line = "Квест №:" + quest.Key.ToString() + "\tимеет зону: \"" + quest.Value.Target.AreaName + "\", которой нигде нет";
                           this.writeToLog(line, quest.Key);
                        }
                }
                foreach (int item_id in quest.Value.QuestRules.TypeOfItems)
                {
                    if (!items.ContainsKey(item_id))
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмета условия type:" + item_id + " не существует";
                       this.writeToLog(line, quest.Key);
                        continue;
                    }

                    if (items[item_id].deleted)
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмет условия type:" + item_id + " помечен на замену";
                       this.writeToLog(line, quest.Key);
                    }

                    if (items[item_id].converted)
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмета условия type:" + item_id + " помечен на удаление";
                       this.writeToLog(line, quest.Key);
                    }
                }

                foreach (int item_id in quest.Value.Reward.TypeOfItems)
                {
                    if (!items.ContainsKey(item_id))
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмета награды type:" + item_id + " не существует";
                       this.writeToLog(line, quest.Key);
                        continue;
                    }

                    if (items[item_id].deleted)
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмет награды type:" + item_id + " помечен на замену";
                       this.writeToLog(line, quest.Key);
                        continue;
                    }

                    if (items[item_id].converted)
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tпредмета награды type:" + item_id + " помечен на удаление";
                       this.writeToLog(line, quest.Key);
                        continue;
                    }
                }

                foreach (CEffect effect in quest.Value.Reward.Effects)
                {
                    if (!this.parent.effects.hasEffectById(effect.getID()))
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tЭффекта №" + effect.getID() + " нет, а выдаётся как награда";
                       this.writeToLog(line, quest.Key);
                    }
                }

                if (quest_types.Contains(quest.Value.Target.QuestType)) 
                {
                    if (!checkQuestIDinDialogOnTest(quest.Key))
                    {
                        string line = "Квест №:" + quest.Key.ToString() + "\tимеет тип: \"" + this.parent.questConst.getDescription(quest.Value.Target.QuestType) + "\" и нигде не проверяется";
                       this.writeToLog(line, quest.Key);
                    }
                }
            }
            setLabel("Записываю в файл");
            saveLogToFile();
            setLabel("Готово");

        }
        delegate void saveLogDelegate();
        private void saveLogToFile()
        {
            if (lbLog.InvokeRequired)
            {
                var _writeToLog = new saveLogDelegate(saveLogToFile);
                lbLog.Invoke(_writeToLog);
            }
            else
            {
                if (lbLog.Items.Count == 0)
                {
                    bpNoErrors.Visible = true;
                    MessageBox.Show("Ты молодец, ошибок нет, Стас");  
                }
                StreamWriter writer = new StreamWriter("CheckErrorLog.txt");
                foreach (string line in lbLog.Items)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }
        private bool checkQuestIDinDialogOnTest(int questID)
        {
            foreach (KeyValuePair<string, Dictionary<int, CDialog>> npc in dialogs.dialogs)
            {
                foreach (KeyValuePair<int, CDialog> dia in npc.Value)
                {
                    foreach (int quest_id in dia.Value.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)
                    {
                        if (quest_id == questID)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            //step 1:
            bpNoErrors.Visible = false;
            progressBar1.Visible = true;
            lbLog.Items.Clear();
            var thread = new Thread(checkErrors);
            thread.IsBackground = true;
            thread.Start();
           // MessageBox.Show(DateTime.Now.ToString() + " " + lbLog.Items.Count.ToString());
           // progressBar1.Visible = false;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbLog.SelectedIndex == -1)
            {
                MessageBox.Show("Для удаления выбери элемент, Стас", "Ты допустил ошибку, не надо больше так делать");
                return;
            }
            int index = list_quest_id[lbLog.SelectedIndex];
            if (index > 0)
            {
                if (!ignores_id.ContainsKey("quests"))
                    ignores_id.Add("quests", new List<int>());
                ignores_id["quests"].Add(index);
                this.saveIgnoresIDs();
            }
            list_quest_id.RemoveRange(lbLog.SelectedIndex, 1);
            lbLog.Items.RemoveAt(lbLog.SelectedIndex);
        }
    }
}
