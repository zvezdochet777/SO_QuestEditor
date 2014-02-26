using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;


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
        public Dictionary<int, CQuest> quest;
        public QuestLocales locales = new QuestLocales();
        public Dictionary<int, CQuest> buffer = new Dictionary<int, CQuest>();

        XDocument doc = new XDocument();
        XDocument startQuestsDoc = new XDocument();
        public List<int> startQuests = new List<int>();
        MainForm parent;

        public int bufferTop = 0;

        //public CQuests(string QuestsXMLFile, string StartQuestsXMLFile)

        //! Конструктор, заполняет словарь quest, startQuests, парсит файлы
        public CQuests(MainForm form)
        {
            parent = form;
            string QuestsXMLFile = parent.settings.getQuestsPath();
            string StartQuestsXMLFile = parent.settings.getStartQuestsPath();

            startQuestsDoc = XDocument.Load(StartQuestsXMLFile);
            foreach (string sQuest in startQuestsDoc.Root.Element("quests").Value.ToString().Split(','))
                if (sQuest!="")
                    startQuests.Add(int.Parse(sQuest));
            this.quest = new Dictionary<int, CQuest>();
            parseQuestsFile(QuestsXMLFile, quest);

            foreach (var locale in parent.settings.getListLocales())
            {
                QuestsXMLFile = "locales\\" + locale + "\\" + parent.settings.questXML;
                if (!locales.Keys.Contains(locale))
                    locales.Add(locale, new Dictionary<int, CQuest>());
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

                if (!item.Element("Target").Element("IsClan").Value.Equals(""))
                    target.IsClan = false;
                else if (!item.Element("Target").Element("IsClan").Value.Equals("0"))
                    target.IsClan = false;
                else if (!item.Element("Target").Element("IsClan").Value.Equals("1"))
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

                //string text_rew_exp = item.Element("Reward").Element("Expirience").Value;
                /*
                reward.Expirience = 0;
                if (!text_rew_exp.Equals(""))
                {
                    if (text_rew_exp.Contains(','))
                    {
                        //After change from three skills role play
                        foreach (string element_rew_exp in text_rew_exp.Split(','))
                            reward.Expirience += int.Parse(element_rew_exp);
                    }
                    else
                        reward.Expirience = int.Parse(text_rew_exp);
                }
                 */

                if (!item.Element("Reward").Element("Expirience").Value.Equals(""))
                    foreach (string itemNum in item.Element("Reward").Element("Expirience").Value.Split(','))
                        reward.Expirience.Add(int.Parse(itemNum));

                if (!item.Element("Reward").Element("NumOfItems").Value.Equals(""))
                    foreach (string itemNum in item.Element("Reward").Element("NumOfItems").Value.Split(','))
                        reward.NumOfItems.Add(int.Parse(itemNum));
                if (!item.Element("Reward").Element("TypeOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("Reward").Element("TypeOfItems").Value.Split(','))
                        reward.TypeOfItems.Add(int.Parse(itemType));

                if (!item.Element("Reward").Element("AttrOfItems").Value.Equals(""))
                    foreach (string itemType in item.Element("Reward").Element("AttrOfItems").Value.Split(','))
                        reward.AttrOfItems.Add(int.Parse(itemType));

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

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Penalty
                string text_pen_exp = item.Element("Penalty").Element("Expirience").Value;

                    penalty.Expirience = 0;
                    if (!text_pen_exp.Equals(""))
                    {
                        if (text_pen_exp.Contains(','))
                        {

                            //After change from three skills role play
                            foreach (string element_pen_exp in text_pen_exp.Split(','))
                                penalty.Expirience += int.Parse(element_pen_exp);
                        }
                        else
                            penalty.Expirience = int.Parse(text_pen_exp);
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
            //! @todo костыль - вычитаем 1 StartQuest. Выкурить нахер
            return (quests.Count - 1);
        }

        //! @todo выжигать каленым железом
        public void saveStartQuests(string fileName)
        {
            XDocument resultDoc2 = new XDocument(new XElement("root"));
            XElement element = new XElement("quests", getListAsString(startQuests));
            resultDoc2.Root.Add(element);

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc2.Save(w);
            }
            copyResultFile(fileName);
        }

        //! Сохраняет данные по квестам в xml файл
        public void saveQuests(string fileName)
        {
            save(fileName, this.quest);
        }

        //! Сохраняет данные по квестам в xml файл
        public void save(string fileName, Dictionary<int, CQuest> target)
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
                        new XElement("MaxMember", getIntAsString(questValue.QuestRules.MaxMember))
                        ),

                    new XElement("Reward",
                        new XElement("TeleportTo", ""),
                        new XElement("Expirience", getListAsString(questValue.Reward.Expirience)),
                        new XElement("TypeOfItems", getListAsString(questValue.Reward.TypeOfItems)),
                        new XElement("NumOfItems", getListAsString(questValue.Reward.NumOfItems)),
                        new XElement("AttrOfItems", getListAsString(questValue.Reward.AttrOfItems)),
                        new XElement("Credits", questValue.Reward.Credits),
                        new XElement("EventCodes", getListAsString(questValue.Reward.EventCodes)),
                        new XElement("Reputation", ""),
                        new XElement ("Fractions", getListAsString(questValue.Reward.Fractions)),
                        new XElement("KarmaPK", questValue.Reward.KarmaPK.ToString()),
                        new XElement("Unlimited", questValue.Reward.Unlimited.ToString()),
                        new XElement("Difficulty", questValue.Reward.Difficulty.ToString()),
                        Effects
                            ),
                    new XElement("Penalty",
                        new XElement("TeleportTo", ""),
                        new XElement("Expirience", getIntAsString(questValue.QuestPenalty.Expirience)),
                        new XElement("TypeOfItems", getListAsString(questValue.QuestPenalty.TypeOfItems)),
                        new XElement("NumOfItems", getListAsString(questValue.QuestPenalty.NumOfItems)),
                        new XElement("Credits", questValue.QuestPenalty.Credits),
                        new XElement("Reputation", "")
                        ),
                    new XElement("Additional",
                        new XElement("IsSubQuest", getIntAsString(questValue.Additional.IsSubQuest)),
                        new XElement("ListOfSubQuest", getListAsString(questValue.Additional.ListOfSubQuest)),
                        new XElement("ShowProgress", questValue.Additional.ShowProgress.ToString()),
                        new XElement("Holder",questValue.Additional.Holder)

                        )
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
            copyResultFile(fileName);
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

        //void replaceOldStartQuestsFile(int start, string name)
        //{
        //    if (File.Exists(name + start.ToString()))
        //        replaceOldQuestsFile((start + 1), (name + "_" + start));
        //    else
        //    {
        //        File.Move(parent.settings.startQuestXML, name);
        //    }
        //}

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

        public CQuest getQuest(int questID)
        {
            if (quest.Keys.Contains(questID))
                return quest[questID];
            else if (buffer.Keys.Contains(questID))
                return buffer[questID];
            else return null;
        }

        public void setClan(int questID, bool IsClan)
        {
            CQuest curQuest = getQuest(questID);
            curQuest.Target.IsClan = IsClan;

            if (curQuest.Additional.ListOfSubQuest.Any())
                foreach (int subQuestID in curQuest.Additional.ListOfSubQuest)
                    setClan(subQuestID, IsClan);
        }

        public string getRootTitle(int questID)
        {
            string ret = "";
            CQuest quest = getQuest(questID);
            
            if (quest != null)
            {
                if (!quest.Additional.IsSubQuest.Equals(0))
                    ret += getRootTitle(quest.Additional.IsSubQuest);
                else
                    ret += quest.QuestInformation.Title;
            }
            return ret;
        }

        public void setBuffer(List<CQuest> quests)
        {
            buffer.Clear();
            foreach (CQuest quest in quests)
                buffer.Add(quest.QuestID, quest);
            
        }

        public void replaceQuest(CQuest iQuest)
        {
            var questID = iQuest.QuestID;
            if (quest.Keys.Contains(questID))
                quest[questID] = iQuest;
            else if (buffer.Keys.Contains(questID))
                buffer[questID] = iQuest;
        }

        public void addQuest(CQuest iQuest)
        {
            quest.Add(iQuest.QuestID, iQuest);
        }

        public void remQuest(int questID)
        {
            if (quest.Keys.Contains(questID))
                quest.Remove(questID);
            else if (buffer.Keys.Contains(questID))
                buffer.Remove(questID);
        }

        public void removeQuest(int questID)
        {
            var quest = getQuest(questID);
            foreach (int qID in quest.Additional.ListOfSubQuest)
            {
                removeQuest(qID);
            }
            remQuest(questID);
        }

        public int getRoot(int questID)
        {
            var quest = getQuest(questID);
            if (quest.Additional.IsSubQuest == 0)
                return questID;
            else return getRoot(quest.Additional.IsSubQuest);
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

        public void createExamples()
        {
            List<int> rem = new List<int>();
            foreach (var q in this.quest.Keys)
                if (!startQuests.Contains(q))
                    rem.Add(q);
            foreach (var qId in rem)
            {
                quest[qId].Target = new CQuestTarget();
                quest[qId].Precondition = new CQuestPrecondition();
                quest[qId].QuestRules = new CQuestRules();
                quest[qId].Reward = new CQuestReward();
                quest[qId].QuestPenalty = new CQuestPenalty();
            }
            saveQuests(parent.settings.questXML);
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
                   this.locales.Add(locale, new Dictionary<int, CQuest>());
               }
               if (this.locales[locale].Keys.Contains(quest.QuestID))
                    this.locales[locale].Remove(quest.QuestID);
               this.locales[locale].Add(quest.QuestID, quest);
        }

        public void saveLocales(string fileName)
        {
            fileName = parent.settings.getCurrentLocalePath() + '\\' + fileName;
            this.save(fileName, this.locales[parent.settings.getCurrentLocale()]);
        }

        public void createResults()
        {
            if (!this.locales.Keys.Contains(parent.settings.getCurrentLocale()))
                return;
            Dictionary<int, CQuest> results = new Dictionary<int, CQuest>();
            CQuest cur_quest;
            foreach (var loc_quest in this.locales[parent.settings.getCurrentLocale()].Values)
            {
                //System.Console.WriteLine("questID:" + loc_quest.QuestID.ToString());
                CQuest quest = getQuest(loc_quest.QuestID);

                if (quest != null && loc_quest.Version == quest.Version)
                {
                    cur_quest = (CQuest)quest.Clone();
                    cur_quest.QuestInformation = loc_quest.QuestInformation;
                    results.Add(cur_quest.QuestID, cur_quest);
                }
            }

            foreach (var start_quest_id in this.startQuests)
            {
                CQuest start_quest = getQuest(start_quest_id);
                if (start_quest != null && !results.Keys.Contains(start_quest_id))
                    results.Add(start_quest.QuestID, start_quest);

            }
            this.save(parent.settings.questXML, results);
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
                        sorted_locale.Add(locale_quest.Additional.Holder, new Dictionary<int, CQuest>());
                    sorted_locale[locale_quest.Additional.Holder].Add(locale_quest.QuestID, locale_quest);
                }
            }

            DifferenceDict ret = new DifferenceDict();
            foreach (var cur_quest in quest.Values)
            {
                if (sorted_locale.Keys.Contains(cur_quest.Additional.Holder))
                {
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
    }
}
