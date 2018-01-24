using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Globalization;


namespace StalkerOnlineQuesterEditor
{
    //! Словарь <QuestID, CQuest>
    using NPCQuestDict = Dictionary<int, CQuest>;
    //! Словарь локализаций квестов <LocaleName, <QuestID, CQuest>>
    using QuestLocales = Dictionary<string, Dictionary<int, CQuest>>;
    //! Словарь из разностей версий диалогов русской версии и локализации
    using DifferenceDict = Dictionary<string, Dictionary<int, CDifference>>;

    //! Класс обработки квестов
    public class CQuests
    {
        public int SHOW_MESSAGE_TAKE = 1;
        public int SHOW_MESSAGE_CLOSE = 2;
        public int SHOW_MESSAGE_PROGRESS = 4;
        public int SHOW_JOURNAL = 8;

        public int last_quest_id = 0;
        //public Dictionary<string, NPCQuestDict> quest;
        //! Словарь <QuestID, CQuest> - основной, содержащий все квесты
        public NPCQuestDict quest;
        public QuestLocales locales = new QuestLocales();
        public NPCQuestDict m_Buffer = new NPCQuestDict();
        public NPCQuestDict m_EngBuffer = new NPCQuestDict();
        public List<int> deletedQuests = new List<int>();
        XDocument doc = new XDocument();
        MainForm parent;

        public int bufferTop = 0;
        public bool CutQuests = false;

        //! Конструктор, заполняет словарь quest, парсит файлы
        public CQuests(MainForm form)
        {
            parent = form;
            this.quest = new NPCQuestDict();
            ParseLastQuestID(parent.settings.GetLastQuestIDPath());
            ParseQuestsData(parent.settings.GetQuestDataPath(), quest);
            ParseQuestsTexts(parent.settings.GetQuestTextPath(), quest);
            ParseDeletedQuest(parent.settings.GetDeletedQuestsPath(), deletedQuests);

            foreach (var locale in parent.settings.getListLocales())
            {
                if (!locales.Keys.Contains(locale))
                    locales.Add(locale, new NPCQuestDict());
                ParseQuestsData(parent.settings.GetQuestDataPath(), this.locales[locale]);
                ParseQuestsTexts(parent.settings.GetQuestLocaleTextPath(), this.locales[locale]);
            }
        }

        void ParseDeletedQuest(string sPath, List<int> list_target)
        {
            if (!File.Exists(sPath))
                return;
            System.IO.StreamReader fileReader = new StreamReader(sPath);
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                if (!line.Any())
                    continue;
                list_target.Add(Convert.ToInt32(line));
            }
            fileReader.Close();
        }

        public void addDeletedQuests(int questID)
        {
            deletedQuests.Add(questID);
            string path = parent.settings.GetDeletedQuestsPath();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true);
            writer.WriteLine(questID.ToString());
            writer.Close();
        }

        void ParseLastQuestID(string sPath)
        {
            if (!File.Exists(sPath))
                return;

            System.IO.StreamReader fileReader = new StreamReader(sPath);
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                if (!line.Any())
                    continue;
                last_quest_id = Convert.ToInt32(line);
            }
            fileReader.Close();
        }

        void ParseQuestsData(string sPath, NPCQuestDict dict_target)
        {
            doc = XDocument.Load(sPath);

            foreach (XElement item in doc.Root.Elements())
            {
                int QuestID = int.Parse(item.Element("ID").Value);
                if (QuestID >= last_quest_id)
                    last_quest_id = QuestID + 1;
                CQuestInformation information = new CQuestInformation();
                CQuestTarget target = new CQuestTarget();
                CQuestPrecondition precondition = new CQuestPrecondition();
                CQuestRules questRules = new CQuestRules();
                CQuestReward reward = new CQuestReward();
                CQuestAdditional additional = new CQuestAdditional();
                CQuestReward penalty = new CQuestReward();
                bool hidden = false;
                if (item.Element("hidden") != null)
                    hidden = true;

                additional.Holder = item.Element("Additional").Element("Holder").Value.Trim();

                if (item.Element("Target") != null)
                {
                    if (item.Element("Target").Element("QuestType") != null)
                        target.QuestType = int.Parse(item.Element("Target").Element("QuestType").Value);
                    if ((item.Element("Target").Element("ObjectType") != null) && (!item.Element("Target").Element("ObjectType").Value.Equals("")))
                        target.ObjectType = int.Parse(item.Element("Target").Element("ObjectType").Value);
                    if ((item.Element("Target").Element("NumOfObjects") != null) &&(!item.Element("Target").Element("NumOfObjects").Value.Equals("")))
                        target.NumOfObjects = int.Parse(item.Element("Target").Element("NumOfObjects").Value);
                    if((item.Element("Target").Element("ObjectAttr") != null) && (!item.Element("Target").Element("ObjectAttr").Value.Equals("")))
                    {
                        int Target_ObjectAttr = int.Parse(item.Element("Target").Element("ObjectAttr").Value);
                        if (Target_ObjectAttr < 0)
                            Target_ObjectAttr = 0;
                        target.ObjectAttr = Target_ObjectAttr;
                    }
                    if (item.Element("Target").Element("AObjectAttrs") != null)
                        foreach (string at in item.Element("Target").Element("AObjectAttrs").Value.ToString().Split(','))
                            if (!at.Equals(""))
                                target.AObjectAttrs.Add(int.Parse(at));
                    if (item.Element("Target").Element("onFin") != null)
                    {                
                        if (!item.Element("Target").Element("onFin").Value.Equals(""))
                            target.onFin = int.Parse(item.Element("Target").Element("onFin").Value);
                        else
                            target.onFin = 1;
                    }

                    if ((item.Element("Target").Element("AreaName") != null) && (!item.Element("Target").Element("AreaName").Value.Equals("")))
                        target.AreaName = item.Element("Target").Element("AreaName").Value;

                    if ((item.Element("Target").Element("ObjectName") != null) && (!item.Element("Target").Element("ObjectName").Value.Equals("")))
                        target.ObjectName = item.Element("Target").Element("ObjectName").Value;

                    ParseIntIfNotEmpty(item, "Target", "IsGroup", out target.IsGroup, 0);
                    if (item.Element("Target").Element("IsClan") != null)
                        target.IsClan = item.Element("Target").Element("IsClan").Value.Trim().Equals("1");

                    if (item.Element("Target").Element("percent") != null)
                    {
                        target.usePercent = true;
                        string str = item.Element("Target").Element("percent").Value;
                        target.percent = float.Parse(item.Element("Target").Element("percent").Value, CultureInfo.InvariantCulture);
                    }
                    if (item.Element("Target").Element("additional") != null)
                    {
                        target.additional = "1";
                    }
                    if ((item.Element("Target").Element("Time") != null) && (!item.Element("Target").Element("Time").Value.Equals("")))
                    {
                        float Time = float.Parse(item.Element("Target").Element("Time").Value);
                        target.Time = Time;
                    }
                    else
                        target.Time = 0.0f;
                }

                if (item.Element("Precondition") != null)
                {
                    if ((item.Element("Precondition").Element("Repeat") != null) && (!item.Element("Precondition").Element("Repeat").Value.Equals("")))
                        precondition.Repeat = int.Parse(item.Element("Precondition").Element("Repeat").Value);

                    if ((item.Element("Precondition").Element("TakenPeriod") != null) && (!item.Element("Precondition").Element("TakenPeriod").Value.Equals("")))
                        precondition.TakenPeriod = double.Parse(item.Element("Precondition").Element("TakenPeriod").Value, CultureInfo.InvariantCulture);

                    if (item.Element("Precondition").Element("OmniCounter") != null)
                        precondition.omniCounter = item.Element("Precondition").Element("OmniCounter").Value.Trim().Equals("1");
                }
                if (item.Element("QuestRules") != null)
                {
                
                    if (item.Element("QuestRules").Element("baseToCapturePercent") != null)
                        questRules.basePercent = float.Parse(item.Element("QuestRules").Element("baseToCapturePercent").Value, CultureInfo.InvariantCulture);
                    CQuests.AddDataToList(item, "QuestRules", "NumOfItems", questRules.NumOfItems);
                    CQuests.AddDataToList(item, "QuestRules", "TypeOfItems", questRules.TypeOfItems);
                    CQuests.AddDataToList(item, "QuestRules", "AttrOfItems", questRules.AttrOfItems);
                    CQuests.AddDataToList(item, "QuestRules", "Scenarios", questRules.Scenarios);
                    CQuests.AddDataToList(item, "QuestRules", "MassQuests", questRules.MassQuests);

                    ParseIntIfNotEmpty(item, "QuestRules", "MaxGroup", out questRules.MaxGroup, 0);
                    ParseIntIfNotEmpty(item, "QuestRules", "MinGroup", out questRules.MinGroup, 0);
                    ParseIntIfNotEmpty(item, "QuestRules", "MaxMember", out questRules.MaxMember, 0);
                    ParseIntIfNotEmpty(item, "QuestRules", "MinMember", out questRules.MinMember, 0);
                    if (item.Element("QuestRules").Element("TeleportTo") != null)
                        questRules.TeleportTo = item.Element("QuestRules").Element("TeleportTo").Value.ToString();

                    if (item.Element("QuestRules").Element("npc") != null)
                    {
                        questRules.npc.setXML(item.Element("QuestRules").Element("npc"));
                    }
                    if (item.Element("QuestRules").Element("mobs") != null)
                    {
                        questRules.mobs.setXML(item.Element("QuestRules").Element("mobs"));
                    }
                }

                if (item.Element("Reward") != null)
                {
                    CQuests.AddDataToList(item, "Reward", "Experience", reward.Experience);
                    CQuests.AddDataToList(item, "Reward", "NumOfItems", reward.NumOfItems);
                    CQuests.AddDataToList(item, "Reward", "TypeOfItems", reward.TypeOfItems);
                    CQuests.AddDataToList(item, "Reward", "AttrOfItems", reward.AttrOfItems);

                    if (item.Element("Reward").Descendants().Any(itm2 => itm2.Name == "Probability"))
                        if (!item.Element("Reward").Element("Probability").Value.Equals(""))
                            foreach (string itemType in item.Element("Reward").Element("Probability").Value.Split(';'))
                                reward.Probability.Add(float.Parse(itemType, CultureInfo.InvariantCulture));

                    if ((item.Element("Reward").Element("Credits") != null) &&(!item.Element("Reward").Element("Credits").Value.Equals("")))
                        reward.Credits = float.Parse(item.Element("Reward").Element("Credits").Value, System.Globalization.CultureInfo.InvariantCulture);

                    ParseIntIfNotEmpty(item, "Reward", "KarmaPK", out reward.KarmaPK, 0);

                    if (item.Element("Reward").Element("Reputation") != null)
                        foreach (string fraction in item.Element("Reward").Element("Reputation").Value.Split(';'))
                            if (!fraction.Equals(""))
                                reward.Reputation.Add(int.Parse(fraction.Split(':')[0]), int.Parse(fraction.Split(':')[1]));
                    if (item.Element("Reward").Element("ChangeQuests") != null)
                    {
                        if (item.Element("Reward").Element("randomQuest") != null)
                            reward.randomQuest = true;
                        foreach (string quest in item.Element("Reward").Element("ChangeQuests").Value.Split(';'))
                            if (!quest.Equals(""))
                                reward.ChangeQuests.Add(int.Parse(quest.Split(':')[0]), int.Parse(quest.Split(':')[1]));
                    }
                    if (item.Element("Reward").Element("Effects") != null)
                        foreach (XElement effect in item.Element("Reward").Element("Effects").Elements())
                        {
                            reward.Effects.Add(new CEffect(int.Parse(effect.Element("id").Value.ToString()),
                            int.Parse(effect.Element("stack").Value.ToString())));
                        }

                    if (item.Element("Reward").Element("RewardWindow") != null)
                        reward.RewardWindow = item.Element("Reward").Element("RewardWindow").Value.Trim().Equals("1");
                }


                if (item.Element("Penalty") != null)
                {
                    CQuests.AddDataToList(item, "Penalty", "Experience", penalty.Experience);
                    CQuests.AddDataToList(item, "Penalty", "NumOfItems", penalty.NumOfItems);
                    CQuests.AddDataToList(item, "Penalty", "TypeOfItems", penalty.TypeOfItems);
                    CQuests.AddDataToList(item, "Penalty", "AttrOfItems", penalty.AttrOfItems);

                    if (item.Element("Penalty").Descendants().Any(itm2 => itm2.Name == "Probability"))
                        if (!item.Element("Penalty").Element("Probability").Value.Equals(""))
                            foreach (string itemType in item.Element("Penalty").Element("Probability").Value.Split(';'))
                                penalty.Probability.Add(float.Parse(itemType, CultureInfo.InvariantCulture));

                    if ((item.Element("Penalty").Element("Credits") != null) && (!item.Element("Penalty").Element("Credits").Value.Equals("")))
                        penalty.Credits = float.Parse(item.Element("Penalty").Element("Credits").Value, System.Globalization.CultureInfo.InvariantCulture);

                    ParseIntIfNotEmpty(item, "Penalty", "KarmaPK", out penalty.KarmaPK, 0);

                    if (item.Element("Penalty").Element("Reputation") != null)
                        foreach (string fraction in item.Element("Penalty").Element("Reputation").Value.Split(';'))
                            if (!fraction.Equals(""))
                                penalty.Reputation.Add(int.Parse(fraction.Split(':')[0]), int.Parse(fraction.Split(':')[1]));
                    if (item.Element("Penalty").Element("ChangeQuests") != null)
                    {
                        if (item.Element("Penalty").Element("randomQuest") != null)
                            reward.randomQuest = true;
                        foreach (string quest in item.Element("Penalty").Element("ChangeQuests").Value.Split(';'))
                            if (!quest.Equals(""))
                                penalty.ChangeQuests.Add(int.Parse(quest.Split(':')[0]), int.Parse(quest.Split(':')[1]));
                    }
                    if (item.Element("Penalty").Element("Effects") != null)
                        foreach (XElement effect in item.Element("Penalty").Element("Effects").Elements())
                        {
                            penalty.Effects.Add(new CEffect(int.Parse(effect.Element("id").Value.ToString()),
                            int.Parse(effect.Element("stack").Value.ToString())));
                        }
                }

                if (item.Element("Additional") != null)
                {
                    if (item.Element("Additional").Element("ShowProgress") != null)
                    {
                        if (!item.Element("Additional").Element("ShowProgress").Value.ToString().Equals(""))
                            additional.ShowProgress = int.Parse(item.Element("Additional").Element("ShowProgress").Value.ToString());
                        else
                            additional.ShowProgress = this.SHOW_JOURNAL | this.SHOW_MESSAGE_CLOSE | this.SHOW_MESSAGE_TAKE | this.SHOW_MESSAGE_PROGRESS;
                    }

                    ParseIntIfNotEmpty(item, "Additional", "IsSubQuest", out additional.IsSubQuest, 0);
                    CQuests.AddDataToList(item, "Additional", "ListOfSubQuest", additional.ListOfSubQuest);

                    if (item.Element("Additional").Descendants().Any(u => u.Name == "CantCancel"))
                        additional.CantCancel = item.Element("Additional").Element("CantCancel").Value.Trim().Equals("1");
                    if (item.Element("Additional").Descendants().Any(u => u.Name == "CantFail"))
                        additional.CantFail = item.Element("Additional").Element("CantFail").Value.Trim().Equals("1");
                    if (item.Element("Additional").Element("DebugData") != null)
                    {
                        additional.DebugData = item.Element("Additional").Element("DebugData").Value.ToString();
                    }
                }

                if (!dict_target.ContainsKey(QuestID))
                    dict_target.Add(QuestID, new CQuest(QuestID, 0, information, precondition, questRules, reward, additional, target, penalty, hidden));
            }
        }

        void ParseQuestsTexts(string sPath, NPCQuestDict target)
        { 
            doc = XDocument.Load(sPath);

            foreach (XElement quest in doc.Root.Elements())
            {
                int QuestID = int.Parse(quest.Element("ID").Value);
                String npc_name = quest.Element("NPC").Value;
                if (!target.ContainsKey(QuestID))
                {
                    MessageBox.Show("Ошибка, квест с ID:" + QuestID.ToString() + " был удалён, по причине, что есть текст, но нет квеста", "Удалён квест");
                    continue;
                }
                if (quest.Element("Title") != null)
                    target[QuestID].QuestInformation.Title = quest.Element("Title").Value;
                if (quest.Element("Description") != null)
                    target[QuestID].QuestInformation.Description = quest.Element("Description").Value;
                if (quest.Element("onWin") != null)
                    target[QuestID].QuestInformation.onWin = quest.Element("onWin").Value;
                if (quest.Element("onFailed") != null)
                    target[QuestID].QuestInformation.onFailed = quest.Element("onFailed").Value;
                int Version = 0;
                if (!quest.Element("Version").Value.Equals(""))
                    Version = int.Parse(quest.Element("Version").Value);
                target[QuestID].Version = Version;

                if (quest.Element("Items") != null)
                    foreach (XElement qitem in quest.Element("Items").Elements())
                    {
                        string title = qitem.Element("title").Value;
                        string description = qitem.Element("description").Value;
                        int itemID = int.Parse(qitem.Element("itemID").Value);
                        string activation = qitem.Element("activation").Value;
                        target[QuestID].QuestInformation.Items.Add(itemID, new QuestItemInfo(title, description, activation));
                    }
            }
        }
        
        public static void AddDataToList(XElement Element, String Name1, String Name2, List<int> list)
        {

            if((Element == null) || (Element.Element(Name1).Element(Name2) == null) )
                return;
            if (Element.Element(Name1).Element(Name2).Value != "")
                foreach (string quest in Element.Element(Name1).Element(Name2).Value.Split(','))
                    list.Add(int.Parse(quest));
        }

        public static void AddDataToList(XElement Element, String Name1, List<int> list)
        {
            if ((Element == null) || (Element.Element(Name1) == null))
                return;
            if (Element.Element(Name1).Value != "")
                foreach (string quest in Element.Element(Name1).Value.Split(','))
                    list.Add(int.Parse(quest));
        }

        private void ParseIntIfNotEmpty(XElement Element, String Name1, String Name2, out int value, int defaultValue)
        {
            if ((Element == null) || (Element.Element(Name1).Element(Name2) == null))
            {
                value = defaultValue;
                return;
            }
            if (!Element.Element(Name1).Element(Name2).Value.Equals(""))
                value = int.Parse(Element.Element(Name1).Element(Name2).Value);
            else
                value = defaultValue;
        }

        //! Возращает список квестов (без вложенных Subquest) для конкретного NPC
        public List<CQuest> getQuestAndTitleOnNPCName(string NPCName)
        {
            List<CQuest> retQuests = new List<CQuest>();
            foreach (CQuest quest in this.quest.Values)
            {
                 //System.Console.WriteLine(NPCName + "vs" + quest.QuestInformation.NameOfHolder);
                 if (quest.Additional.Holder.Equals(NPCName) && (quest.Additional.IsSubQuest==0))
                     retQuests.Add(quest);
            }
            return retQuests;
        }
        //! Возвращает число квестов у заданного NPC 
        public int getCountOfQuests(string NPCName)
        { 
            List<CQuest> quests = getQuestAndTitleOnNPCName(NPCName);            
            return (quests.Count);
        }
        //! Возвращает число квестов верхнего уровня (без субквестов)
        public int getCountTopLevelQuests()
        { 
            int total = 0;
            foreach (int questID in quest.Keys)
                if (quest[questID].Additional.IsSubQuest == 0)
                    total++;
            return total;
        }

        //! Возвращает список ID квестов (для комбобокса)
        public string[] getQuestsIDasString()
        {
            int count = quest.Count + getCountTopLevelQuests();
            int i = 0;
            string[] array = new string[count];
            foreach (int questID in quest.Keys)
            {
                array[i++] = questID.ToString();
                if (quest[questID].Additional.IsSubQuest == 0)
                {
                    if (quest[questID].QuestInformation.Title != "")
                        array[i++] = quest[questID].QuestInformation.Title;
                    else
                        array[i++] = "fuck";                    
                }
            }
            return array;
        }

        //! Сохраняет данные по квестам в xml файл
        public void SaveQuests()
        {
            string path = parent.settings.GetLastQuestIDPath();
            FileStream fcreate = File.Open(path, FileMode.Create);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fcreate);
            writer.WriteLine(last_quest_id.ToString());
            writer.Close();

            SaveQuestsTexts(parent.settings.GetQuestTextPath(), this.quest);
            SaveQuestsData(parent.settings.GetQuestDataPath(), this.quest);
        }

        //! Сохраняет текущую локализацию квестов в файл
        public void SaveLocales()
        {
            SaveQuestsTexts(parent.settings.GetQuestLocaleTextPath(), this.locales[parent.settings.getCurrentLocale()]);
        }

        private void SaveQuestsTexts(string fileName, NPCQuestDict target)
        { 
            XDocument resultDoc = new XDocument(new XDeclaration("1.0","utf-8",null), new XElement("root"));
            XElement element;

            foreach (CQuest questValue in target.Values)
            {
                
                element = new XElement("Quest",
                   new XElement("ID", questValue.QuestID),
                   new XElement("NPC", questValue.Additional.Holder),
                   new XElement("Version", questValue.Version));
                if (questValue.QuestInformation.Title != "")
                    element.Add(new XElement("Title", questValue.QuestInformation.Title));
                if (questValue.QuestInformation.Description != "")
                    element.Add(new XElement("Description", questValue.QuestInformation.Description));
                if (questValue.QuestInformation.onWin != "")
                    element.Add(new XElement("onWin", questValue.QuestInformation.onWin));
                if (questValue.QuestInformation.onFailed != "")
                    element.Add(new XElement("onFailed", questValue.QuestInformation.onFailed));

                List<XElement> ItemsXE = getItemElements(questValue.QuestInformation.Items);
                if (ItemsXE.Any())
                    element.Add(new XElement("Items", ItemsXE));
                resultDoc.Root.Add(element);
            }

            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
        }

        private void SaveQuestsData(string fileName, NPCQuestDict target)
        {
            XDocument resultDoc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("root"));
            XElement element;

            foreach (CQuest questValue in target.Values)
            {
                element = new XElement("Quest",
                   new XElement("ID", questValue.QuestID));
                if (questValue.hidden)
                    element.Add(new XElement("hidden", "1"));

                if (questValue.Target.Any())
                {
                    element.Add(new XElement("Target"));
                    if (questValue.Target.onFin != 0)
                        element.Element("Target").Add(new XElement("onFin", questValue.Target.onFin));
                    if (questValue.Target.QuestType != 0)
                        element.Element("Target").Add(new XElement("QuestType", questValue.Target.QuestType));
                    if (questValue.Target.ObjectType != 0)
                        element.Element("Target").Add(new XElement("ObjectType", Global.GetIntAsString(questValue.Target.ObjectType)));
                    if (questValue.Target.ObjectAttr != 0)
                        element.Element("Target").Add(new XElement("ObjectAttr", questValue.Target.ObjectAttr));
                    if (questValue.Target.NumOfObjects != 0)
                        element.Element("Target").Add(new XElement("NumOfObjects", Global.GetIntAsString(questValue.Target.NumOfObjects)));
                    if (questValue.Target.ObjectName != "")
                        element.Element("Target").Add( new XElement("ObjectName", questValue.Target.ObjectName));
                    if (questValue.Target.AObjectAttrs.Any())
                        element.Element("Target").Add(new XElement("AObjectAttrs", Global.GetListAsString(questValue.Target.AObjectAttrs)));
                    if (questValue.Target.AreaName != "")
                        element.Element("Target").Add(new XElement("AreaName", questValue.Target.AreaName));
                    if (questValue.Target.IsGroup != 0)
                        element.Element("Target").Add(new XElement("IsGroup", questValue.Target.IsGroup.ToString()));
                    if (questValue.Target.IsClan)
                        element.Element("Target").Add(new XElement("IsClan", questValue.Target.IsClan));
                    if (questValue.Target.Time != 0)
                        element.Element("Target").Add(new XElement("Time", questValue.Target.Time.ToString()));
                    if (questValue.Target.usePercent)
                        element.Element("Target").Add(new XElement("percent", questValue.Target.percent.ToString("G6", CultureInfo.InvariantCulture)));
                    if (questValue.Target.additional.Any())
                        element.Element("Target").Add(new XElement("additional", "1"));
                }

                if (questValue.Precondition.Any())
                {
                    element.Add(new XElement("Precondition"));
                    if (questValue.Precondition.TakenPeriod != 0)
                        element.Element("Precondition").Add(new XElement("TakenPeriod", questValue.Precondition.TakenPeriod.ToString("G6", CultureInfo.InvariantCulture)));
                    if (questValue.Precondition.Repeat != 0)
                        element.Element("Precondition").Add(new XElement("Repeat", questValue.Precondition.Repeat));
                    if (questValue.Precondition.omniCounter)
                        element.Element("Precondition").Add(new XElement("OmniCounter", Global.GetBoolAsString(questValue.Precondition.omniCounter)));
                }

                if (questValue.QuestRules.Any())
                {
                    element.Add(new XElement("QuestRules"));
                    if (questValue.QuestRules.Scenarios.Any())
                         element.Element("QuestRules").Add(new XElement("Scenarios", Global.GetListAsString(questValue.QuestRules.Scenarios)));
                    if (questValue.QuestRules.TeleportTo != "")
                        element.Element("QuestRules").Add(new XElement("TeleportTo", questValue.QuestRules.TeleportTo));
                    if (questValue.QuestRules.TypeOfItems.Any())
                        element.Element("QuestRules").Add(new XElement("TypeOfItems", Global.GetListAsString(questValue.QuestRules.TypeOfItems)));
                    if (questValue.QuestRules.NumOfItems.Any())
                        element.Element("QuestRules").Add(new XElement("NumOfItems", Global.GetListAsString(questValue.QuestRules.NumOfItems)));
                    if (questValue.QuestRules.AttrOfItems.Any())
                        element.Element("QuestRules").Add(new XElement("AttrOfItems", Global.GetListAsString(questValue.QuestRules.AttrOfItems)));
                    if (questValue.QuestRules.MinGroup != 0)
                        element.Element("QuestRules").Add(new XElement("MinGroup", Global.GetIntAsString(questValue.QuestRules.MinGroup)));
                    if (questValue.QuestRules.MaxGroup != 0)
                        element.Element("QuestRules").Add(new XElement("MaxGroup", Global.GetIntAsString(questValue.QuestRules.MaxGroup)));
                    if (questValue.QuestRules.MaxMember != 0)
                        element.Element("QuestRules").Add(new XElement("MaxMember", Global.GetIntAsString(questValue.QuestRules.MaxMember)));
                    if (questValue.QuestRules.MassQuests.Any())
                        element.Element("QuestRules").Add(new XElement("MassQuests", Global.GetListAsString(questValue.QuestRules.MassQuests)));
                    if (questValue.QuestRules.basePercent != 0)
                        element.Element("QuestRules").Add(new XElement("baseToCapturePercent", questValue.QuestRules.basePercent.ToString("G6", CultureInfo.InvariantCulture)));
                    if (questValue.QuestRules.npc.Any())
                        element.Element("QuestRules").Add(questValue.QuestRules.npc.getXML());
                    if (questValue.QuestRules.mobs.Any())
                        element.Element("QuestRules").Add(questValue.QuestRules.mobs.getXML());
                }

                if (questValue.Reward.Any())
                {
                    element.Add(new XElement("Reward"));
                    if (questValue.Reward.Experience.Any())
                        element.Element("Reward").Add(new XElement("Experience", Global.GetListAsString(questValue.Reward.Experience)));
                    if (questValue.Reward.TypeOfItems.Any())
                        element.Element("Reward").Add(new XElement("TypeOfItems", Global.GetListAsString(questValue.Reward.TypeOfItems)));
                    if (questValue.Reward.NumOfItems.Any())
                        element.Element("Reward").Add(new XElement("NumOfItems", Global.GetListAsString(questValue.Reward.NumOfItems)));
                    if (questValue.Reward.AttrOfItems.Any())
                        element.Element("Reward").Add(new XElement("AttrOfItems", Global.GetListAsString(questValue.Reward.AttrOfItems)));
                    if (questValue.Reward.Probability.Any())
                        element.Element("Reward").Add(new XElement("Probability", getListAsString(questValue.Reward.Probability)));
                    if (questValue.Reward.Credits != 0)
                        element.Element("Reward").Add( new XElement("Credits", questValue.Reward.Credits));
                    if (questValue.Reward.ReputationNotEmpty())
                        element.Element("Reward").Add(new XElement("Reputation", questValue.Reward.getReputation()));
                    if (questValue.Reward.ChangeQuests.Any())
                        element.Element("Reward").Add(new XElement("ChangeQuests", questValue.Reward.getChangeQuests()));
                    if (questValue.Reward.randomQuest)
                        element.Element("Reward").Add(new XElement("randomQuest", "1"));
                    if (questValue.Reward.KarmaPK != 0)
                        element.Element("Reward").Add(new XElement("KarmaPK", questValue.Reward.KarmaPK.ToString()));
                    if (questValue.Reward.RewardWindow)
                        element.Element("Reward").Add(new XElement("RewardWindow", Global.GetBoolAsString(questValue.Reward.RewardWindow)));
                    List<XElement> EffectsXE = getEffectElements(questValue.Reward.Effects);
                    if (EffectsXE.Any())
                        element.Element("Reward").Add(new XElement("Effects", EffectsXE));
                }              

                if (questValue.QuestPenalty.Any())
                {
                     element.Add(new XElement("Penalty"));
                     if (questValue.QuestPenalty.Experience.Any())
                         element.Element("Penalty").Add(new XElement("Experience", Global.GetListAsString(questValue.QuestPenalty.Experience)));
                     if (questValue.QuestPenalty.TypeOfItems.Any())
                         element.Element("Penalty").Add(new XElement("TypeOfItems", Global.GetListAsString(questValue.QuestPenalty.TypeOfItems)));
                     if (questValue.QuestPenalty.NumOfItems.Any())
                         element.Element("Penalty").Add(new XElement("NumOfItems", Global.GetListAsString(questValue.QuestPenalty.NumOfItems)));
                     if (questValue.QuestPenalty.AttrOfItems.Any())
                         element.Element("Penalty").Add(new XElement("AttrOfItems", Global.GetListAsString(questValue.QuestPenalty.AttrOfItems)));
                     if (questValue.QuestPenalty.Probability.Any())
                         element.Element("Penalty").Add(new XElement("Probability", getListAsString(questValue.QuestPenalty.Probability)));
                     if (questValue.QuestPenalty.Credits != 0)
                         element.Element("Penalty").Add(new XElement("Credits", questValue.QuestPenalty.Credits.ToString("G6", CultureInfo.InvariantCulture)));
                     if (questValue.QuestPenalty.ReputationNotEmpty())
                         element.Element("Penalty").Add(new XElement("Reputation", questValue.QuestPenalty.getReputation()));
                    if (questValue.QuestPenalty.ChangeQuests.Any())
                        element.Element("Penalty").Add(new XElement("ChangeQuests", questValue.QuestPenalty.getChangeQuests()));
                    if (questValue.QuestPenalty.randomQuest)
                        element.Element("Penalty").Add(new XElement("randomQuest", "1"));
                    if (questValue.QuestPenalty.KarmaPK != 0)
                         element.Element("Penalty").Add(new XElement("KarmaPK", questValue.QuestPenalty.KarmaPK.ToString()));
                     List<XElement> EffectsXE = getEffectElements(questValue.QuestPenalty.Effects);
                     if (EffectsXE.Any())
                         element.Element("Penalty").Add(new XElement("Effects", EffectsXE));
                }

                if (questValue.Additional.Any())
                {
                    element.Add(new XElement("Additional"));
                    if (questValue.Additional.IsSubQuest != 0)
                        element.Element("Additional").Add(new XElement("IsSubQuest", Global.GetIntAsString(questValue.Additional.IsSubQuest)));
                    if (questValue.Additional.ListOfSubQuest.Any())
                        element.Element("Additional").Add(new XElement("ListOfSubQuest", Global.GetListAsString(questValue.Additional.ListOfSubQuest)));
                    if (questValue.Additional.ShowProgress != 0)
                        element.Element("Additional").Add(new XElement("ShowProgress", questValue.Additional.ShowProgress.ToString()));
                    if (questValue.Additional.CantCancel)
                        element.Element("Additional").Add(new XElement("CantCancel", Global.GetBoolAsString(questValue.Additional.CantCancel)));
                    if (questValue.Additional.CantFail)
                        element.Element("Additional").Add(new XElement("CantFail", Global.GetBoolAsString(questValue.Additional.CantFail)));
                    if (questValue.Additional.Holder != "")
                        element.Element("Additional").Add(new XElement("Holder", questValue.Additional.Holder));
                    if (questValue.Additional.DebugData != "")
                        element.Element("Additional").Add(new XElement("DebugData", questValue.Additional.DebugData));
                    if (!questValue.QuestInformation.Title.Any() && !questValue.QuestInformation.Description.Any())
                        element.Element("Additional").Add(new XElement("isEmpty", "1")); ;

                }

                resultDoc.Root.Add(element);
            }

            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
        }

        List<XElement> getEffectElements(List<CEffect> effects)
        {
            List<XElement> ret = new List<XElement>();
            foreach (CEffect effect in effects)
            {
                ret.Add(new XElement("effect",
                    new XElement("id", effect.getID()),
                    new XElement("stack", effect.getStack())));
            }
            return ret;
        }
        List<XElement> getItemElements(Dictionary<int, QuestItemInfo> items)
        {
            List<XElement> ret = new List<XElement>();
            foreach (int key in items.Keys)
            {
                ret.Add(new XElement("item",
                    new XElement("itemID", key.ToString()),
                    new XElement("title", items[key].title),
                    new XElement("description", items[key].description),
                    new XElement("activation", items[key].activation)));
            }
            return ret;
        }

        //! Возвращает список как строку аргументов через запятую
        string getListAsString(List<float> list)
        {
            string str = "";
            foreach (float element in list)
            {
                if (str.Equals(""))
                    str += element.ToString("0.00000", CultureInfo.InvariantCulture);
                else
                    str += ";" + element.ToString("0.00000", CultureInfo.InvariantCulture);
            }
            return str;
        }

        //! Возвращает экземпляр класса CQuest с указанным ID
        public CQuest getQuest(int questID)
        {
            if (quest.Keys.Contains(questID))
                return quest[questID];
            else if (m_Buffer.Keys.Contains(questID))
                return m_Buffer[questID];
            else return null;
        }

        //! Возвращает экземпляр класса CQuest с указанным ID
        private CQuest getQuestFromLocale(int questID, String locale)
        {
            if (locales.ContainsKey(locale) && locales[locale].ContainsKey(questID))
                return locales[locale][questID];
            else if (m_EngBuffer.Keys.Contains(questID))
                return m_EngBuffer[questID];
            else return null;
        }

        //! Возвращает экземпляр CQuest  в зависимости от языка перевода
        public CQuest getQuestLocalized(int questID)
        {
            if (parent.settings.getMode() == parent.settings.MODE_EDITOR)
                return getQuest(questID);
            else
            {
                string locale = parent.settings.getCurrentLocale();
                if (locales[locale].ContainsKey(questID))
                    return locales[locale][questID];
                else
                {
                    CQuest quest = getQuest(questID);
                    quest.Version = 0;
                    return quest;
                }
            }
        }

        //! Возвращает строку с описанием цели квеста в удобочитаемом виде
        public string getTargetString(CQuest quest)
        {
            string result;
            switch (quest.Target.QuestType)
            { 
                case 1:
                    result = quest.Target.ObjectName;
                    break;
                case 2: case 3:
                    result = "Тип моба " + parent.mobConst.getDescriptionOnType(quest.Target.ObjectType).getName();
                    result += ", " + quest.Target.NumOfObjects + " штук";
                    break;
                case 4: case 8:
                    result = "Зона " + parent.zoneConst.getDescriptionOnKey(quest.Target.ObjectName).getName();
                    break;
                case 5:
                    result = quest.Target.NumOfObjects + " рублей";
                    break;
                case 6:
                    result = "Триггер " + parent.triggerConst.getDescriptionOnId(quest.Target.ObjectType);
                    break;
                case 7: case 19: case 20:
                    result = "Предмет " + parent.itemConst.getDescriptionOnID(quest.Target.ObjectType);
                    break;
                case 0: case 16:
                    result = "Предмет " + parent.itemConst.getDescriptionOnID(quest.Target.ObjectType);
                    result += ", " + quest.Target.NumOfObjects + " штук";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        public void setClan(int questID, bool IsClan)
        {
            CQuest curQuest = getQuest(questID);
            curQuest.Target.IsClan = IsClan;

            if (curQuest.Additional.ListOfSubQuest.Any())
                foreach (int subQuestID in curQuest.Additional.ListOfSubQuest)
                    setClan(subQuestID, IsClan);
        }

        public void replaceQuest(CQuest iQuest)
        {
            var questID = iQuest.QuestID;
            if (quest.Keys.Contains(questID))
                quest[questID] = iQuest;
            else if (m_Buffer.Keys.Contains(questID))
                m_Buffer[questID] = iQuest;
        }

        private void addQuest(CQuest iQuest)
        {
            quest.Add(iQuest.QuestID, iQuest);
        }

        //! Удаляет квест и  все его подквесты
        private void removeQuestsWithLocals(int questID, bool recursiveCall)
        {
            List<int> temp = getQuest(questID).Additional.ListOfSubQuest;
            foreach (int subquest in temp)
                removeQuestsWithLocals(subquest, true);

            if (!recursiveCall)
                if (getQuest(questID).Additional.IsSubQuest != 0)
                    quest[getQuest(questID).Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);

            // удаляем квест из локализаций
            CQuest english = locales["ENG"][questID];
            foreach (int subquest in english.Additional.ListOfSubQuest)
                removeQuestsWithLocals(subquest, true);
            if (english.Additional.IsSubQuest != 0)
                locales["ENG"][english.Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);
            locales["ENG"].Remove(questID);
            quest.Remove(questID);
        }

        public int getRoot(int questID)
        {
            CQuest quest = getQuest(questID);
            if (quest.Additional.IsSubQuest == 0)
                return questID;
            else 
                return getRoot(quest.Additional.IsSubQuest);
        }

        public void setTutorial(int questID, bool isTutor)
        {
            var quest = getQuest(questID);
            if (quest != null)
            {
                if (isTutor)
                    quest.Additional.ShowProgress |= 64;
                else
                    quest.Additional.ShowProgress &= 63;

                foreach (int qID in quest.Additional.ListOfSubQuest)
                    setTutorial(qID, isTutor);
            }
        }

        public CQuest getLocaleQuest(int questID, string locale)
        {
            if (this.locales.Keys.Contains(locale) && this.locales[locale].Keys.Contains(questID))
                return this.locales[locale][questID];
            return null;
        }

        public void addLocaleQuest(CQuest quest, string locale)
        {
               if (!this.locales.Keys.Contains(locale))
               {
                   this.locales.Add(locale, new NPCQuestDict());
               }
               if (this.locales[locale].Keys.Contains(quest.QuestID))
                    this.locales[locale].Remove(quest.QuestID);
               this.locales[locale].Add(quest.QuestID, quest);
        }

        //! Возвращает словарь из квестов для локализации (устаревшие, актуальные или все)
        public DifferenceDict getQuestDifference(string locale, FindType findType)
        {
            //System.Console.WriteLine("CQuests::getQuestDifference");
            QuestLocales sorted_locale = new QuestLocales();
            if (this.locales.Keys.Contains(locale))
            {
                foreach (var locale_quest in this.locales[locale].Values)
                {
                    if (!sorted_locale.Keys.Contains(locale_quest.Additional.Holder))
                        sorted_locale.Add(locale_quest.Additional.Holder, new NPCQuestDict());
                    sorted_locale[locale_quest.Additional.Holder].Add(locale_quest.QuestID, locale_quest);
                }
            }
            DifferenceDict ret = new DifferenceDict();
            foreach (var cur_quest in quest.Values)
            {
                if (!sorted_locale.Keys.Contains(cur_quest.Additional.Holder))
                { 
                    NPCQuestDict dict = new NPCQuestDict();
                    dict.Add(cur_quest.QuestID, cur_quest);
                    sorted_locale.Add(cur_quest.Additional.Holder,dict);
                }
                CQuest locale_quest = new CQuest();
                locale_quest.Version = 0;

                if (sorted_locale[cur_quest.Additional.Holder].Keys.Contains(cur_quest.QuestID))
                    locale_quest.Version = sorted_locale[cur_quest.Additional.Holder][cur_quest.QuestID].Version;

                if (!ret.Keys.Contains(cur_quest.Additional.Holder))
                    ret.Add(cur_quest.Additional.Holder, new Dictionary<int, CDifference>());
                switch (findType)
                { 
                    case FindType.all:
                        ret[cur_quest.Additional.Holder].Add(cur_quest.QuestID, new CDifference(cur_quest.Version, locale_quest.Version));
                        break;
                    case FindType.outdatedOnly:
                        if (cur_quest.Version != locale_quest.Version)
                            ret[cur_quest.Additional.Holder].Add(cur_quest.QuestID, new CDifference(cur_quest.Version, locale_quest.Version)); 
                        break;
                    case FindType.actualOnly:
                        if (cur_quest.Version == locale_quest.Version)
                            ret[cur_quest.Additional.Holder].Add(cur_quest.QuestID, new CDifference(cur_quest.Version, locale_quest.Version));
                        break;
                }
            }
            return ret;
        }

        public void PutQuestsToBuffer(int questID, bool cutQuests)
        {
            List<CQuest> result = GetQuestWithSubs(questID);
            List<CQuest> engResult = GetLocalizedQuestWithSubs(questID);
            CutQuests = cutQuests;
            if(cutQuests)
                removeQuestsWithLocals(questID, false);

            result[0].Additional.IsSubQuest = 0;
            engResult[0].Additional.IsSubQuest = 0; 
            foreach (CQuest quest in result)
                quest.Additional.Holder = "";
            foreach (CQuest quest in engResult)
                quest.Additional.Holder = "";

            if (!cutQuests)
                ChangeQuestsIDs(result, engResult);

            setBuffer(result, m_Buffer);
            setBuffer(engResult, m_EngBuffer); 
            bufferTop = result[0].QuestID;
        }
        //! Возвращает список всех подквестов для квеста questID
        private List<CQuest> GetQuestWithSubs(int questID)
        {
            List<CQuest> result = new List<CQuest>();
            CQuest quest = getQuest(questID);
            if (quest != null)
            {
                result.Add((CQuest)quest.Clone());
                if (quest.Additional.ListOfSubQuest.Any())
                    foreach (int subquestID in quest.Additional.ListOfSubQuest)
                        result.AddRange(GetQuestWithSubs(subquestID));
            }
            return result;
        }

        //! Возвращает список всех переведенных подквестов для квеста questID
        private List<CQuest> GetLocalizedQuestWithSubs(int questID)
        {
            List<CQuest> result = new List<CQuest>();
            CQuest quest = locales["ENG"][questID];
            if (quest != null)
            {
                result.Add((CQuest)quest.Clone());
                if (quest.Additional.ListOfSubQuest.Any())
                    foreach (int subquestID in quest.Additional.ListOfSubQuest)
                        result.AddRange(GetLocalizedQuestWithSubs(subquestID));
            }
            return result;
        }

        //! Заменяет ID квестов в буфере на новые
        private void ChangeQuestsIDs(List<CQuest> quests, List<CQuest> engQuests)
        {
            Dictionary<int, int> replace = new Dictionary<int, int>();
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
                    newSubQuestIDs.Add(replace[subqID]);

                quest.Additional.ListOfSubQuest = newSubQuestIDs;
            }

            foreach (CQuest engQuest in engQuests)
            {
                engQuest.QuestID = replace[engQuest.QuestID];
                if (replace.Keys.Contains(engQuest.Additional.IsSubQuest))
                    engQuest.Additional.IsSubQuest = replace[engQuest.Additional.IsSubQuest];

                List<int> newSubQuestIDs = new List<int>();
                foreach (int subqID in engQuest.Additional.ListOfSubQuest)
                    newSubQuestIDs.Add(replace[subqID]);

                engQuest.Additional.ListOfSubQuest = newSubQuestIDs;
            }
        }

        //! Возвращает ID для нового квеста без учета списка excepts 
        private int getQuestNewID(List<int> exceptsList)
        {
            int iFirstQuestID = 1;
            if (last_quest_id != 0)
            {
                last_quest_id++;
                return last_quest_id - 1;
            }
            for (int questi = iFirstQuestID; ; questi++)

                if (!quest.Keys.Contains(questi) && !m_Buffer.Keys.Contains(questi) && !exceptsList.Contains(questi))
                    return questi;
        }
        //! Сохраняет квесты quests в буфер обмена destination
        private void setBuffer(List<CQuest> quests, NPCQuestDict destination)
        {
            destination.Clear();
            foreach (CQuest quest in quests)
                destination.Add(quest.QuestID, quest);
        }

        //! Вставляет квесты из буфера обмена как подквест 
        public void PasteBuffer(int CurrentQuestID)
        {
            CQuest HeadQuest = getQuest(CurrentQuestID);
            List<CQuest> buffer = new List<CQuest>();
            foreach (CQuest q in m_Buffer.Values.ToList())
                buffer.Add((CQuest)q.Clone());

            List<CQuest> engBuffer = new List<CQuest>();
            foreach (CQuest q in m_EngBuffer.Values.ToList())
                engBuffer.Add((CQuest)q.Clone());

            if (!CutQuests)
                ChangeQuestsIDs(buffer, engBuffer);
            
            HeadQuest.Additional.ListOfSubQuest.Add(buffer[0].QuestID);
            buffer[0].Additional.IsSubQuest = HeadQuest.QuestID;
            foreach (CQuest bufQuest in buffer)
            {
                bufQuest.Additional.Holder = HeadQuest.Additional.Holder;
                addQuest(bufQuest);
            }

            CQuest HeadEnglish = locales["ENG"][CurrentQuestID];
            HeadEnglish.Additional.ListOfSubQuest.Add(engBuffer[0].QuestID);
            engBuffer[0].Additional.IsSubQuest = HeadEnglish.QuestID;
            foreach (CQuest engQuest in engBuffer)
            {
                engQuest.Additional.Holder = HeadEnglish.Additional.Holder;
                locales["ENG"].Add(engQuest.QuestID, engQuest);
            }
            //if (cut_quest_mode)
             //   clearQuestsBuffer();
        }

        public int ReplaceBuffer(int CurrentQuestID)
        {
            CQuest HeadQuest = getQuest(CurrentQuestID);
            CQuest HeadEnglish = getQuestFromLocale(CurrentQuestID, "ENG");
            List<CQuest> buffer = new List<CQuest>();
            foreach (CQuest q in m_Buffer.Values.ToList())
                buffer.Add((CQuest)q.Clone());
            List<CQuest> engBuffer = new List<CQuest>();
            foreach (CQuest q in m_EngBuffer.Values.ToList())
                engBuffer.Add((CQuest)q.Clone());

            if (!CutQuests)
                ChangeQuestsIDs(buffer, engBuffer);

            String name = HeadQuest.Additional.Holder;
            int questID = HeadQuest.QuestID;
            int parentID = HeadQuest.Additional.IsSubQuest;
            CQuest parentQuest = getQuest(parentID);
            CQuest parentEngQuest = getQuestFromLocale(parentID, "ENG");
            removeQuestsWithLocals(questID, false);

            if (HeadQuest.Additional.IsSubQuest == 0)
            {
                buffer[0].QuestID = questID;
                foreach (var subQuest in buffer)
                {
                    if (buffer[0].Additional.ListOfSubQuest.Contains(subQuest.QuestID))
                        subQuest.Additional.IsSubQuest = questID;
                    subQuest.Additional.Holder = name;
                    addQuest(subQuest);
                }
                parentID = questID;
            }
            else
            {
                int index = parentQuest.Additional.ListOfSubQuest.IndexOf(questID);
                parentQuest.Additional.ListOfSubQuest.Remove(questID);
                parentQuest.Additional.ListOfSubQuest.Insert(index, buffer[0].QuestID);
                buffer[0].Additional.IsSubQuest = parentID;
                foreach (var subQuest in buffer)
                {
                    subQuest.Additional.Holder = name;
                    addQuest(subQuest);
                }
            }

            // копируем и вставляем локализации
            if (HeadEnglish.Additional.IsSubQuest == 0)
            {
                engBuffer[0].QuestID = questID;
                foreach (var subQuest in engBuffer)
                {
                    if (engBuffer[0].Additional.ListOfSubQuest.Contains(subQuest.QuestID))
                        subQuest.Additional.IsSubQuest = questID;
                    subQuest.Additional.Holder = name;
                    locales["ENG"].Add(subQuest.QuestID, subQuest);                    
                }
                parentID = questID;
            }
            else
            {
                int engIndex = parentEngQuest.Additional.ListOfSubQuest.IndexOf(questID);
                parentEngQuest.Additional.ListOfSubQuest.Remove(questID);
                parentEngQuest.Additional.ListOfSubQuest.Insert(engIndex, engBuffer[0].QuestID);
                engBuffer[0].Additional.IsSubQuest = parentID;
                foreach (var subQuest in engBuffer)
                {
                    subQuest.Additional.Holder = name;
                    locales["ENG"].Add(subQuest.QuestID, subQuest);
                }
            }

            return getRoot(parentID);

            //if (cut_quest_mode)
             //   clearQuestsBuffer();
        }
    }
}
