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
        Common common = new Common();
        public int SHOW_MESSAGE_TAKE = 1;
        public int SHOW_MESSAGE_CLOSE = 2;
        public int SHOW_MESSAGE_PROGRESS = 4;
        public int SHOW_JOURNAL = 8;

        //public Dictionary<string, NPCQuestDict> quest;
        //! Словарь <QuestID, CQuest> - основной, содержащий все квесты
        public NPCQuestDict quest;
        public QuestLocales locales = new QuestLocales();
        public NPCQuestDict m_Buffer = new NPCQuestDict();
        public NPCQuestDict m_EngBuffer = new NPCQuestDict();

        XDocument doc = new XDocument();
        MainForm parent;

        public int bufferTop = 0;
        public bool CutQuests = false;

        //! Конструктор, заполняет словарь quest, парсит файлы
        public CQuests(MainForm form)
        {
            parent = form;
            string QuestsXMLFile = parent.settings.getQuestsPath();

            this.quest = new NPCQuestDict();
            parseQuestsFile(QuestsXMLFile, quest);

            foreach (var locale in parent.settings.getListLocales())
            {
                //QuestsXMLFile = "locales\\" + locale + "\\" + parent.settings.questXML;
                QuestsXMLFile = parent.settings.getQuestLocalePath();
                if (!locales.Keys.Contains(locale))
                    locales.Add(locale, new NPCQuestDict());
                parseQuestsFile(QuestsXMLFile, this.locales[locale]);
            }
        }

        //! Парсит файл квестов и заносит результат в target
        void parseQuestsFile(string sPath, NPCQuestDict variable_target)
        {
            if (!File.Exists(sPath))
                return;
            doc = XDocument.Load(sPath);

            foreach (XElement item in doc.Root.Elements())
            {
                int QuestID = int.Parse(item.Element("QuestID").Value);
                int Version = new int();
                Version = int.Parse(item.Element("Version").Value);

                CQuestInformation information = new CQuestInformation();
                CQuestTarget target = new CQuestTarget();
                CQuestPrecondition precondition = new CQuestPrecondition();
                CQuestRules questRules = new CQuestRules();
                CQuestReward reward = new CQuestReward();
                CQuestAdditional additional = new CQuestAdditional();
                CQuestPenalty penalty = new CQuestPenalty();

                information.Description = common.decode(item.Element("QuestInformation").Element("Description").Value.Trim());
                information.Title = common.decode(item.Element("QuestInformation").Element("Title").Value.Trim());
                information.onWin = item.Element("QuestInformation").Element("onWin").Value.Trim();
                information.onFailed = item.Element("QuestInformation").Element("onFailed").Value.Trim();

                additional.Holder = item.Element("Additional").Element("Holder").Value.Trim();

                foreach (XElement qitem in item.Element("QuestInformation").Element("Items").Elements())
                {
                    string title = qitem.Element("title").Value;
                    string description = common.decode(qitem.Element("description").Value);
                    int itemID = int.Parse(qitem.Element("itemID").Value);
                    string activation = qitem.Element("activation").Value;
                    information.Items.Add(itemID, new QuestItemInfo(title, description, activation));
                }

                target.QuestType = int.Parse(item.Element("Target").Element("QuestType").Value);
                if (!item.Element("Target").Element("ObjectType").Value.Equals(""))
                    target.ObjectType = int.Parse(item.Element("Target").Element("ObjectType").Value);
                if (!item.Element("Target").Element("NumOfObjects").Value.Equals(""))
                    target.NumOfObjects = int.Parse(item.Element("Target").Element("NumOfObjects").Value);
                if (!item.Element("Target").Element("ObjectAttr").Value.Equals(""))
                {
                    int Target_ObjectAttr = int.Parse(item.Element("Target").Element("ObjectAttr").Value);
                    if (Target_ObjectAttr < 0)
                        Target_ObjectAttr = 0;
                    target.ObjectAttr = Target_ObjectAttr;
                }

                foreach (string at in item.Element("Target").Element("AObjectAttrs").Value.ToString().Split(','))
                    if (!at.Equals(""))
                        target.AObjectAttrs.Add(int.Parse(at));

                if (!item.Element("Target").Element("onFin").Value.Equals(""))
                    target.onFin = int.Parse(item.Element("Target").Element("onFin").Value);
                else
                    target.onFin = 1;

                if (!item.Element("Target").Element("AreaName").Value.Equals(""))
                    target.AreaName = item.Element("Target").Element("AreaName").Value;

                if (!item.Element("Target").Element("ObjectName").Value.Equals(""))
                    target.ObjectName = item.Element("Target").Element("ObjectName").Value;

                if (!item.Element("Target").Element("IsGroup").Value.Equals(""))
                    target.IsGroup = int.Parse(item.Element("Target").Element("IsGroup").Value);
                else
                    target.IsGroup = 0;

                if (item.Element("Target").Element("IsClan").Value.Equals("1"))
                    target.IsClan = true;
                else
                    target.IsClan = false;

                if (!item.Element("Target").Element("Time").Value.Equals(""))
                {
                    float Time = float.Parse(item.Element("Target").Element("Time").Value);
                    target.Time = Time;
                }
                else
                    target.Time = 0.0f;

                if (!item.Element("Precondition").Element("Repeat").Value.Equals(""))
                    precondition.Repeat = int.Parse(item.Element("Precondition").Element("Repeat").Value);

                if (!item.Element("Precondition").Element("TakenPeriod").Value.Equals(""))
                    precondition.TakenPeriod = double.Parse(item.Element("Precondition").Element("TakenPeriod").Value);

                if (!item.Element("QuestRules").Element("NumOfItems").Value.Equals(""))
                    foreach (string itemNum in item.Element("QuestRules").Element("NumOfItems").Value.Split(','))
                        questRules.NumOfItems.Add(int.Parse(itemNum));
                if (!item.Element("QuestRules").Element("TypeOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("QuestRules").Element("TypeOfItems").Value.Split(','))
                        questRules.TypeOfItems.Add(int.Parse(itemType));

                questRules.TeleportTo = item.Element("QuestRules").Element("TeleportTo").Value.ToString();
                if (!item.Element("QuestRules").Element("MaxGroup").Value.Equals(""))
                    questRules.MaxGroup = int.Parse(item.Element("QuestRules").Element("MaxGroup").Value);
                if (!item.Element("QuestRules").Element("MinGroup").Value.Equals(""))
                    questRules.MinGroup = int.Parse(item.Element("QuestRules").Element("MinGroup").Value);

                if (!item.Element("QuestRules").Element("MaxMember").Value.Equals(""))
                    questRules.MaxMember = int.Parse(item.Element("QuestRules").Element("MaxMember").Value);
                if (!item.Element("QuestRules").Element("MinMember").Value.Equals(""))
                    questRules.MinMember = int.Parse(item.Element("QuestRules").Element("MinMember").Value);

                if (!item.Element("QuestRules").Element("AttrOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("QuestRules").Element("AttrOfItems").Value.Split(','))
                        questRules.AttrOfItems.Add(int.Parse(itemType));
                if (!item.Element("QuestRules").Element("Scenarios").Value.Equals(""))
                    foreach (string itemType in item.Element("QuestRules").Element("Scenarios").Value.Split(','))
                        questRules.Scenarios.Add(int.Parse(itemType));

                reward.TeleportTo = item.Element("Reward").Element("TeleportTo").Value.ToString();

                if (!item.Element("Reward").Element("Expirience").Value.Equals(""))
                    foreach (string itemNum in item.Element("Reward").Element("Expirience").Value.Split(','))
                        reward.Experience.Add(int.Parse(itemNum));

                if (!item.Element("Reward").Element("NumOfItems").Value.Equals(""))
                    foreach (string itemNum in item.Element("Reward").Element("NumOfItems").Value.Split(','))
                        reward.NumOfItems.Add(int.Parse(itemNum));
                if (!item.Element("Reward").Element("TypeOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("Reward").Element("TypeOfItems").Value.Split(','))
                        reward.TypeOfItems.Add(int.Parse(itemType));

                if (!item.Element("Reward").Element("AttrOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("Reward").Element("AttrOfItems").Value.Split(','))
                        reward.AttrOfItems.Add(int.Parse(itemType));

                if (item.Element("Reward").Descendants().Any(itm2 => itm2.Name == "Probability"))
                    if (!item.Element("Reward").Element("Probability").Value.Equals(""))
                        foreach (string itemType in item.Element("Reward").Element("Probability").Value.Split(';'))
                            reward.Probability.Add(float.Parse(itemType, CultureInfo.InvariantCulture));

                if (!item.Element("Reward").Element("Credits").Value.Equals(""))
                    reward.Credits = float.Parse(item.Element("Reward").Element("Credits").Value);

                if (!item.Element("Reward").Element("EventCodes").Value.Equals(""))
                    foreach (string events in item.Element("Reward").Element("EventCodes").Value.Split(','))
                        reward.NumOfItems.Add(int.Parse(events));

                try
                {
                    foreach (string fraction in item.Element("Reward").Element("Fractions").Value.Split(','))
                        if (!fraction.Equals(""))
                            reward.Fractions.Add(int.Parse(fraction));
                }
                catch
                {
                }

                try
                {
                    reward.Unlimited = int.Parse(item.Element("Reward").Element("Unlimited").Value);
                }
                catch
                {
                }

                try
                {
                    reward.Difficulty = int.Parse(item.Element("Reward").Element("Difficulty").Value);
                }
                catch
                {
                    reward.Difficulty = 1;
                }
                //foreach (string fraction in item.Element("Reward").Element("Reputation").Value.Split(','))
                //    if (!fraction.Equals(""))
                //        reward.Reputation.Add(int.Parse(fraction.Split(':')[0]),int.Parse(fraction.Split(':')[1]));

                if (!item.Element("Reward").Element("KarmaPK").Value.Equals(""))
                    reward.KarmaPK = int.Parse(item.Element("Reward").Element("KarmaPK").Value);

                foreach (XElement effect in item.Element("Reward").Element("Effects").Elements())
                {
                    reward.Effects.Add(new CEffect(int.Parse(effect.Element("id").Value.ToString()),
                        int.Parse(effect.Element("stack").Value.ToString())));
                }
                
                if (!item.Element("Additional").Element("ShowProgress").Value.ToString().Equals(""))
                    additional.ShowProgress = int.Parse(item.Element("Additional").Element("ShowProgress").Value.ToString());
                else
                    additional.ShowProgress = this.SHOW_JOURNAL | this.SHOW_MESSAGE_CLOSE | this.SHOW_MESSAGE_TAKE | this.SHOW_MESSAGE_PROGRESS;

                if (!item.Element("Additional").Element("IsSubQuest").Value.Equals(""))
                    additional.IsSubQuest = int.Parse(item.Element("Additional").Element("IsSubQuest").Value);
                if (!item.Element("Additional").Element("ListOfSubQuest").Value.Equals(""))
                    foreach (string quest in item.Element("Additional").Element("ListOfSubQuest").Value.Split(','))
                        additional.ListOfSubQuest.Add(int.Parse(quest));

                if (item.Element("Additional").Descendants().Any(u => u.Name == "CantCancel"))
                {
                    if (item.Element("Additional").Element("CantCancel").Value.Equals(""))
                        additional.CantCancel = false;
                    else if (item.Element("Additional").Element("CantCancel").Value.Equals("1"))
                        additional.CantCancel = true;
                }
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Penalty
                string text_pen_exp = item.Element("Penalty").Element("Expirience").Value;
                penalty.Experience = 0;
                if (!text_pen_exp.Equals(""))
                {
                    if (text_pen_exp.Contains(','))
                    {
                        //After change from three skills role play
                        foreach (string element_pen_exp in text_pen_exp.Split(','))
                            penalty.Experience += int.Parse(element_pen_exp);
                    }
                    else
                        penalty.Experience = int.Parse(text_pen_exp);
                }

                if (!item.Element("Penalty").Element("NumOfItems").Value.Equals(""))
                    foreach (string itemNum in item.Element("Penalty").Element("NumOfItems").Value.Split(','))
                        penalty.NumOfItems.Add(int.Parse(itemNum));
                if (!item.Element("Penalty").Element("TypeOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("Penalty").Element("TypeOfItems").Value.Split(','))
                        penalty.TypeOfItems.Add(int.Parse(itemType));
                if (!item.Element("Penalty").Element("Credits").Value.Equals(""))
                    penalty.Credits = float.Parse(item.Element("Penalty").Element("Credits").Value);

                //foreach (string fraction in item.Element("Penalty").Element("Reputation").Value.Split(','))
                //    if (!fraction.Equals(""))
                //        reward.Reputation.Add(int.Parse(fraction.Split(':')[0]), int.Parse(fraction.Split(':')[1]));
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Penalty

                if (!variable_target.ContainsKey(QuestID))
                    variable_target.Add(QuestID, new CQuest(QuestID, Version, information, precondition, questRules, reward, additional, target, penalty));
            }
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
        public void saveQuests(string fileName)
        {
            //fileName = "RUS\\" + fileName;
            save(fileName, this.quest);
        }

        //! Сохраняет текущую локализацию квестов в файл
        public void saveLocales(string fileName)
        {
            //fileName = parent.settings.getCurrentLocalePath() + '\\' + fileName;
            this.save(fileName, this.locales[parent.settings.getCurrentLocale()]);
        }

        //! Сохраняет данные по квестам в xml файл
        public void save(string fileName, NPCQuestDict target)
        {
            XDocument resultDoc = new XDocument(
                new XDeclaration("1.0","utf-8",null),
                new XElement("root")
                );
            XElement element;
            //resultDoc.Root = new XElement("root");

            foreach (CQuest questValue in target.Values)
            {
                List<XElement> ItemsXE = getItemElements(questValue.QuestInformation.Items);
                List<XElement> EffectsXE = getEffectElements(questValue.Reward.Effects);
                XElement Items;
                XElement Effects;
                if (ItemsXE.Any())
                {
                    Items = new XElement("Items", ItemsXE);
                }
                else
                {
                    Items = new XElement("Items", "");
                }
                if (EffectsXE.Any())
                {
                    Effects = new XElement("Effects", EffectsXE);
                }
                else
                {
                    Effects = new XElement("Effects", "");
                }

                element = new XElement("quest",
                   new XElement("QuestID", questValue.QuestID),
                   new XElement("Version", questValue.Version),
                   new XElement("QuestInformation",
                       new XElement("Title", common.encode(questValue.QuestInformation.Title)),
                       new XElement("Description", common.encode(questValue.QuestInformation.Description)),
                       //new XElement("TypeOfHolder", questValue.QuestInformation.TypeOfHolder),
                       //new XElement("NameOfHolder", questValue.QuestInformation.NameOfHolder),
                       new XElement("onWin", questValue.QuestInformation.onWin),
                       new XElement("onFailed", questValue.QuestInformation.onFailed),
                       Items
                       ),
                    new XElement("Target",
                        new XElement("onFin", questValue.Target.onFin),
                        new XElement("QuestType", questValue.Target.QuestType),
                        new XElement("ObjectType", getIntAsString(questValue.Target.ObjectType)),
                        new XElement("ObjectAttr", questValue.Target.ObjectAttr),
                        new XElement("NumOfObjects", getIntAsString(questValue.Target.NumOfObjects)),
                        new XElement("ObjectName", questValue.Target.ObjectName),
                        new XElement("AObjectAttrs", getListAsString(questValue.Target.AObjectAttrs)),
                        new XElement("AreaName", questValue.Target.AreaName),
                        new XElement("IsGroup", questValue.Target.IsGroup.ToString()),
                        new XElement("IsClan", getBoolAsString(questValue.Target.IsClan)),
                        new XElement("Time", questValue.Target.Time.ToString())),
                    new XElement("Precondition",
                        new XElement("TakenPeriod", questValue.Precondition.TakenPeriod.ToString()),
                        new XElement("Repeat", getIntAsString(questValue.Precondition.Repeat))),
                    new XElement("QuestRules",
                        new XElement("Scenarios", getListAsString(questValue.QuestRules.Scenarios)),
                        new XElement("TeleportTo", questValue.QuestRules.TeleportTo),
                        new XElement("Reputation", ""),
                        new XElement("TypeOfItems", getListAsString(questValue.QuestRules.TypeOfItems)),
                        new XElement("NumOfItems", getListAsString(questValue.QuestRules.NumOfItems)),
                        new XElement("AttrOfItems", getListAsString(questValue.QuestRules.AttrOfItems)),
                        new XElement("MinGroup", getIntAsString(questValue.QuestRules.MinGroup)),
                        new XElement("MaxGroup", getIntAsString(questValue.QuestRules.MaxGroup)),
                        new XElement("MinMember", getIntAsString(questValue.QuestRules.MinMember)),
                        new XElement("MaxMember", getIntAsString(questValue.QuestRules.MaxMember))),
                    new XElement("Reward",
                        new XElement("TeleportTo", ""),
                        new XElement("Expirience", getListAsString(questValue.Reward.Experience)),
                        new XElement("TypeOfItems", getListAsString(questValue.Reward.TypeOfItems)),
                        new XElement("NumOfItems", getListAsString(questValue.Reward.NumOfItems)),
                        new XElement("AttrOfItems", getListAsString(questValue.Reward.AttrOfItems)),
                        new XElement("Probability", getListAsString(questValue.Reward.Probability)),
                        new XElement("Credits", questValue.Reward.Credits),
                        new XElement("EventCodes", getListAsString(questValue.Reward.EventCodes)),
                        new XElement("Reputation", ""),
                        new XElement("Fractions", getListAsString(questValue.Reward.Fractions)),
                        new XElement("KarmaPK", questValue.Reward.KarmaPK.ToString()),
                        new XElement("Unlimited", questValue.Reward.Unlimited.ToString()),
                        new XElement("Difficulty", questValue.Reward.Difficulty.ToString()),
                        Effects
                            ),
                    new XElement("Penalty",
                        new XElement("TeleportTo", ""),
                        new XElement("Expirience", getIntAsString(questValue.QuestPenalty.Experience)),
                        new XElement("TypeOfItems", getListAsString(questValue.QuestPenalty.TypeOfItems)),
                        new XElement("NumOfItems", getListAsString(questValue.QuestPenalty.NumOfItems)),
                        new XElement("Credits", questValue.QuestPenalty.Credits),
                        new XElement("Reputation", "")),
                    new XElement("Additional",
                        new XElement("IsSubQuest", getIntAsString(questValue.Additional.IsSubQuest)),
                        new XElement("ListOfSubQuest", getListAsString(questValue.Additional.ListOfSubQuest)),
                        new XElement("ShowProgress", questValue.Additional.ShowProgress.ToString()),
                        new XElement("CantCancel", getBoolAsString( questValue.Additional.CantCancel )),
                        new XElement("Holder",questValue.Additional.Holder))
                   );
                resultDoc.Root.Add(element);
            }

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
            //copyResultFile(fileName);
        }

        //! Копирование получившегося файла в директорию игры (она задается в настройках)
        void copyResultFile(string filename)
        {
            if (parent.settings.pathToCopyFiles == "")
                return;
            if (!parent.settings.pathToCopyFiles.EndsWith("\\"))
                parent.settings.pathToCopyFiles += "\\";
            string destPath = parent.settings.pathToCopyFiles + filename;
            string srcPath = filename;
            
            try
            {
                File.Copy(srcPath, destPath, true);
            }
            catch
            {
                MessageBox.Show("Невозможно скопировать файл результата! Проверьте путь в настройках программы.", "Ошибка",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    new XElement("description", common.encode(items[key].description)),
                    new XElement("activation", items[key].activation)));
            }
            return ret;
        }
        //! Возвращает список как строку аргументов через запятую
        string getListAsString(List<int> list)
        {
            string str = "";
            foreach (int element in list)
            {
                if (str.Equals(""))
                    str += element.ToString();
                else
                    str += "," + element.ToString();
            }
            return str;
        }
        //! Возвращает список как строку аргументов через запятую
        string getListAsString(List<float> list)
        {
            string str = "";
            foreach (float element in list)
            {
                if (str.Equals(""))
                    str += element.ToString("0.000", CultureInfo.InvariantCulture);
                else
                    str += ";" + element.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return str;
        }
        //! Возвращает bool строкой "1" или ""
        string getBoolAsString(bool bbool)
        {
            if (bbool)
                return "1";
            else
                return "";
        }
        //! Конвертит целое в строку, если целое = 0 -> вернет пустую строку
        string getIntAsString(int someInt)
        {
            if (someInt == 0)
                return "";
            else
                return someInt.ToString();
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
        public CQuest getQuestFromLocale(int questID, String locale)
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

        public void addQuest(CQuest iQuest)
        {
            quest.Add(iQuest.QuestID, iQuest);
        }

        public void removeQuest(int questID)
        {
            if (quest.Keys.Contains(questID))
                quest.Remove(questID);
            else if (m_Buffer.Keys.Contains(questID))
                m_Buffer.Remove(questID);
        }

        public void removeQuestWithSubs(int questID)
        {
            var quest = getQuest(questID);
            foreach (int qID in quest.Additional.ListOfSubQuest)
                removeQuestWithSubs(qID);
            removeQuest(questID);
        }

        //! Удаляет квест и  все его подквесты
        void removeQuestsWithLocals(int questID, bool recursiveCall)
        {
            List<int> temp = getQuest(questID).Additional.ListOfSubQuest;
            foreach (int subquest in temp) //getQuestOnQuestID(questID).Additional.ListOfSubQuest)
                removeQuestsWithLocals(subquest, true);

            if (!recursiveCall)
                if (getQuest(questID).Additional.IsSubQuest != 0)
                    quest[getQuest(questID).Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);

            //removeQuestWithSubs(questID);

            // удаляем квест из локализаций
            CQuest english = locales["ENG"][questID];
            foreach (int subquest in english.Additional.ListOfSubQuest)
                removeQuestsWithLocals(subquest, true);
            if (english.Additional.IsSubQuest != 0)
                locales["ENG"][english.Additional.IsSubQuest].Additional.ListOfSubQuest.Remove(questID);
            locales["ENG"].Remove(questID);

            //treeQuest.Nodes.Find(questID.ToString(), true)[0].Remove();
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
            System.Console.WriteLine("CQuests::setTutorial " + questID.ToString() + " " + isTutor.ToString());
            var quest = getQuest(questID);
            if (quest != null)
            {
                if (isTutor)

                    quest.Additional.ShowProgress |= 64;
                else
                {
                    System.Console.WriteLine(quest.Additional.ShowProgress.ToString() + " ^ " + "64 = " + (quest.Additional.ShowProgress ^ 64).ToString());
                    quest.Additional.ShowProgress &= 63;
                }

                System.Console.WriteLine("after set:" + quest.Additional.ShowProgress.ToString());
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

            //foreach (var locale_npc_name in sorted_locale.Keys)
            //{
            //    foreach (var locale_quest in sorted_locale[locale_npc_name].Values)
            //    {
            //        var locale_version = locale_quest.Version;
            //        var quest = this.quest[locale_quest.QuestID];
            //        if (quest.Version != locale_version)
            //        {
            //            if (sorted_locale[locale_npc_name].Any())
            //                ret.Add(locale_npc_name, new Dictionary<int, CDifference>());
            //            ret[locale_npc_name].Add(quest.QuestID, new CDifference(quest.Version, locale_version));
            //        }
            //    }
            //}
            /*
            foreach (var name in ret.Keys)
            {
                System.Console.WriteLine("npc:" + name);
                foreach (var quest_id in ret[name].Keys)
                    System.Console.WriteLine("id:" + quest_id.ToString());
            }
            */
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
        public List<CQuest> GetQuestWithSubs(int questID)
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
        public List<CQuest> GetLocalizedQuestWithSubs(int questID)
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
        public void ChangeQuestsIDs(List<CQuest> quests, List<CQuest> engQuests)
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

            //return quests;
        }

        //! Возвращает ID для нового квеста без учета списка excepts 
        private int getQuestNewID(List<int> exceptsList)
        {
            int iFirstQuestID = 1;
            for (int questi = iFirstQuestID; ; questi++)
                if (!quest.Keys.Contains(questi) && !m_Buffer.Keys.Contains(questi) && !exceptsList.Contains(questi))
                    return questi;
        }
        //! Сохраняет квесты quests в буфер обмена destination
        public void setBuffer(List<CQuest> quests, NPCQuestDict destination)
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

        public void ReplaceBuffer(int CurrentQuestID)
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
            int index = parentQuest.Additional.ListOfSubQuest.IndexOf(questID);
            int engIndex = parentEngQuest.Additional.ListOfSubQuest.IndexOf(questID);
            //removeQuestWithSubs(questID);
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
            }
            else
            {
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
            }
            else
            {
                parentEngQuest.Additional.ListOfSubQuest.Remove(questID);
                parentEngQuest.Additional.ListOfSubQuest.Insert(engIndex, engBuffer[0].QuestID);
                engBuffer[0].Additional.IsSubQuest = parentID;
                foreach (var subQuest in engBuffer)
                {
                    subQuest.Additional.Holder = name;
                    locales["ENG"].Add(subQuest.QuestID, subQuest);
                }
            }

            //if (cut_quest_mode)
             //   clearQuestsBuffer();
        }
    }
}
