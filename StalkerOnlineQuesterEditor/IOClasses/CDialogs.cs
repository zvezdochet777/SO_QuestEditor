using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using UMD.HCIL.Piccolo;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    using ListOfQuests = List<int>;

    //! Словарь <DialogID, CDialog>
    using NPCDialogDict = Dictionary<int, CDialog>;
    //! Словарь <NPCName, <DialogID, CDialog>>
    using NPCDicts = Dictionary<string, Dictionary<int, CDialog>>;
    //! Словарь <LocaleName, <NPCName, <DialogID, CDialog>>>
    using NPCLocales = Dictionary<string, Dictionary<string, Dictionary<int, CDialog>>>;
    //! Словарь <NPCName, <DialogID, CDifference>>
    using DifferenceDict = Dictionary<string, Dictionary<int, CDifference>>;
    //! Словарь местонахождения NPC
    using NPCDataDict = Dictionary<string,string>;

    //! Класс обработки диалогов
    public class CDialogs
    {
        Common common = new Common();
        MainForm parent;
        //! XML файл диалогов для хранения информации
        XDocument doc = new XDocument();
        //! Словарь диалогов: < Имя NPC, <DialogID,  CDialog> >
        public NPCDicts dialogs = new NPCDicts();
        //! Словарь локалей
        public NPCLocales locales = new NPCLocales();
        //! Словарь местонахождения NPC
        public NPCDataDict location = new NPCDataDict();
        //! Словарь имен NPC по-русски
        public NPCDataDict rusName = new NPCDataDict();

        //! Конструктор - парсит текущий файл диалогов, ищет локализации и парсит их тоже
        public CDialogs(MainForm parent)
        {
            this.parent = parent;
            String DialogsXMLFile = parent.settings.getDialogsPath();
            parseDialogsFile(DialogsXMLFile, this.dialogs);
            foreach (var locale in parent.settings.getListLocales())
            {
                //DialogsXMLFile = "locales\\" + locale + "\\" + parent.settings.dialogXML;
                DialogsXMLFile = parent.settings.getDialogLocalePath();
                if (!locales.Keys.Contains(locale))
                    locales.Add(locale, new NPCDicts());
                parseDialogsFile(DialogsXMLFile, this.locales[locale]);
            }
            parseNpcLocationFile("npc_stat.xml");
        }

        //! Парсер xml - файла диалогов, записывает результат в target
        private void parseDialogsFile(String DialogsXMLFile, NPCDicts target)
        {
            if (!File.Exists(DialogsXMLFile))
                return;

            doc = XDocument.Load(DialogsXMLFile);
            foreach (XElement item in doc.Root.Elements())
            {
                int DialogID = int.Parse(item.Element("DialogID").Value);
                string Title = item.Element("Title").Value.Trim();
                string Text = item.Element("Text").Value.Trim();
                List<string> Holder = new List<string>();
                List<int> Nodes = new List<int>();
                Actions Actions = new Actions();
                CDialogPrecondition Precondition = new CDialogPrecondition();

                foreach (string holder in item.Element("Holder").Value.Split(','))
                    Holder.Add(holder.Trim());

                if (item.Element("Nodes").Value != "")
                    foreach (string node in item.Element("Nodes").Value.Split(','))
                        if (node != "")
                            Nodes.Add(int.Parse(node));

                if (item.Element("Actions").Element("Exit").Value == "1")
                    Actions.Exit = true;
                else
                    Actions.Exit = false;

                if (item.Element("Actions").Element("ToDialog").Value != "")
                    Actions.ToDialog = int.Parse(item.Element("Actions").Element("ToDialog").Value);

                if (!item.Element("Actions").Element("Event").Value.Equals(""))
                    Actions.Event = int.Parse(item.Element("Actions").Element("Event").Value);
                else
                    Actions.Event = 0;

                Actions.Data = item.Element("Actions").Element("Data").Value;

                if (item.Element("Actions").Element("GetQuest").Value != "")
                    foreach (string quest in item.Element("Actions").Element("GetQuest").Value.Split(','))
                        Actions.GetQuests.Add(int.Parse(quest));

                if (item.Element("Actions").Element("CompleteQuest").Value != "")
                    foreach (string quest in item.Element("Actions").Element("CompleteQuest").Value.Split(','))
                        Actions.CompleteQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfCompletedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfCompletedQuests").Value.Split(','))
                        Precondition.ListOfNecessaryQuests.ListOfCompletedQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfOpenedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfOpenedQuests").Value.Split(','))
                        Precondition.ListOfNecessaryQuests.ListOfOpenedQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfOnTestQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfOnTestQuests").Value.Split(','))
                        Precondition.ListOfNecessaryQuests.ListOfOnTestQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfCompletedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfCompletedQuests").Value.Split(','))
                        Precondition.ListOfMustNoQuests.ListOfCompletedQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfOpenedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfOpenedQuests").Value.Split(','))
                        Precondition.ListOfMustNoQuests.ListOfOpenedQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfOnTestQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfOnTestQuests").Value.Split(','))
                        Precondition.ListOfMustNoQuests.ListOfOnTestQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfFailedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfFailedQuests").Value.Split(','))
                        Precondition.ListOfMustNoQuests.ListOfFailedQuests.Add(int.Parse(quest));

                if (item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfFailedQuests").Value != "")
                    foreach (string quest in item.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfFailedQuests").Value.Split(','))
                        Precondition.ListOfNecessaryQuests.ListOfOnTestQuests.Add(int.Parse(quest));

                //if (item.Element("Precondition").Element("CheckClan").Value == "1")
                //    Precondition.tests.Add(0);
                //if (item.Element("Precondition").Element("CheckClanID").Value == "1")
                //    Precondition.tests.Add(1);

                Precondition.KarmaPK = new List<int>();
                if (item.Element("Precondition").Element("KarmaPK").Value != "")
                    foreach (string karme_el in item.Element("Precondition").Element("KarmaPK").Value.Split(','))
                        Precondition.KarmaPK.Add(int.Parse(karme_el));
                if (item.Element("Precondition").Descendants().Any(itm2 => itm2.Name == "PlayerLevel"))
                    if (!item.Element("Precondition").Element("PlayerLevel").Value.Equals(""))
                        Precondition.PlayerLevel = int.Parse(item.Element("Precondition").Element("PlayerLevel").Value);

                if (item.Element("Precondition").Element("tests").Value != "")
                    foreach (string test in item.Element("Precondition").Element("tests").Value.Split(','))
                        Precondition.tests.Add(int.Parse(test));

                int Version = 0;
                if (!item.Element("Version").Value.Equals(""))
                    Version = int.Parse(item.Element("Version").Value);

                NodeCoordinates nodeCoord = new NodeCoordinates();
                if ( item.Descendants("NodeCoordinates").ToList().Count > 0 )
                {
                    if (!item.Element("NodeCoordinates").Element("X").Value.Equals(""))
                        nodeCoord.X = int.Parse(item.Element("NodeCoordinates").Element("X").Value);
                    if (!item.Element("NodeCoordinates").Element("Y").Value.Equals(""))
                        nodeCoord.Y = int.Parse(item.Element("NodeCoordinates").Element("Y").Value);

                    if (item.Element("NodeCoordinates").Element("RootDialog").Value.Equals("1"))
                        nodeCoord.RootDialog = true;
                    else
                        nodeCoord.RootDialog = false;
                    
                    if (item.Element("NodeCoordinates").Element("Active").Value.Equals("1"))
                        nodeCoord.Active = true;
                    else
                        nodeCoord.Active = false;
                }

                foreach (string el in item.Element("Precondition").Element("Reputation").Value.Split(';'))
                {
                    string[] fr = el.Split(':');
                    if (fr.Count() > 1)
                    {
                        Precondition.Reputation.Add(int.Parse(fr[0]), new List<double>());
                        int type = int.Parse(fr[1]);
                        Precondition.Reputation[int.Parse(fr[0])].Add(type);
                        double a = 0;
                        if (fr[2] != "")
                            a = double.Parse(fr[2]);
                        Precondition.Reputation[int.Parse(fr[0])].Add(a);
                        double b = 0;
                        if (fr[3] != "")
                            b = double.Parse(fr[3]);
                        Precondition.Reputation[int.Parse(fr[0])].Add(b);
                    }
                }

                foreach (string holder in Holder)
                {
                    if (target.Keys.Contains(holder))
                    {
                        if (!target[holder].Keys.Contains(DialogID))
                            target[holder].Add(DialogID, new CDialog(holder, Title, Text, Precondition, Actions, Nodes, DialogID, Version, nodeCoord));
                    }
                    else
                    {
                        target.Add(holder, new Dictionary<int, CDialog>());
                        target[holder].Add(DialogID, new CDialog(holder, Title, Text, Precondition, Actions, Nodes, DialogID, Version, nodeCoord));
                    }
                }
            }
        }

        //! Сохранить все диалоги в xml файл
        public void saveDialogs(string fileName)
        {
            //fileName = "RUS\\" + fileName; 
            save(fileName, this.dialogs);
        }

        //! Сохраняет текущую локализацию диалогов в файл
        public void saveLocales(string fileName)
        {
            //fileName = parent.settings.getCurrentLocalePath() + '\\' + fileName;
            this.save(fileName, this.locales[parent.settings.getCurrentLocale()]);
        }

        //! Сохранение всех диалогов в xml файл
        private void save(string fileName, NPCDicts target)
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement element;

            foreach (NPCDialogDict Dictdialog in target.Values)
                foreach (CDialog dialog in Dictdialog.Values)
                {
                    element = new XElement("dialog",
                       new XElement("DialogID", dialog.DialogID.ToString()),
                       new XElement("Version", dialog.version.ToString()),
                       new XElement("Holder", dialog.Holder.ToString()),
                       new XElement("Title", dialog.Title.ToString()),
                       new XElement("Precondition",
                           new XElement("ListOfNecessaryQuests",
                               new XElement("listOfCompletedQuests",
                                       getListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)),
                               new XElement("listOfOpenedQuests",
                                       getListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)),
                               new XElement("listOfOnTestQuests",
                                       getListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)),
                                new XElement("listOfFailedQuests",
                                       getListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests))),
                           new XElement("ListOfMustNoQuests",
                               new XElement("listOfCompletedQuests",
                                       getListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests)),
                               new XElement("listOfOpenedQuests",
                                       getListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests)),
                               new XElement("listOfOnTestQuests",
                                       getListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests)),
                               new XElement("listOfFailedQuests",
                                       getListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfFailedQuests))),
                           new XElement("tests", getListAsString(dialog.Precondition.tests)),
                           new XElement("Reputation", dialog.Precondition.getReputation()),
                           new XElement("PlayerLevel", getIntAsString(dialog.Precondition.PlayerLevel)),
                           new XElement("KarmaPK", getListAsString(dialog.Precondition.KarmaPK))),
                       new XElement("Text", dialog.Text),
                       new XElement("Actions",
                           new XElement("Exit", getBoolAsString(dialog.Actions.Exit)),
                           new XElement("ToDialog", getIntAsString(dialog.Actions.ToDialog)),
                           new XElement("Data", dialog.Actions.Data),
                           new XElement("Event", dialog.Actions.Event.ToString()),
                           new XElement("GetQuest", getListAsString(dialog.Actions.GetQuests)),
                           new XElement("CompleteQuest", getListAsString(dialog.Actions.CompleteQuests))),
                       new XElement("Nodes", getListAsString(dialog.Nodes)),
                       new XElement("NodeCoordinates",
                           new XElement("X", getIntAsString(dialog.coordinates.X)),
                           new XElement("Y", getIntAsString(dialog.coordinates.Y)),
                           new XElement("RootDialog", getBoolAsString(dialog.coordinates.RootDialog)),
                           new XElement("Active", getBoolAsString(dialog.coordinates.Active)))
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

        //! Парсит файл с местонахождением NPC
        void parseNpcLocationFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            doc = XDocument.Load(fileName);
            foreach (XElement item in doc.Root.Elements())
            {
                string name = item.Element("Name").Value.ToString();
                string map = item.Element("map").Value.ToString();
                string localName = item.Element("npcLocal").Value.ToString();
                if ( !location.ContainsKey(name) )
                    location.Add(name, map);
                if (!rusName.ContainsKey(name))
                    rusName.Add(name, localName);
            }
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

        //! Возвращает список как строку из элементов через запятую
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

        //! Возвращает булевское значение строкой: "1" или ""
        string getBoolAsString(bool bbool)
        {
            if (bbool)
                return "1";
            else
                return "";
        }

        //! Возвращает целое как строку: "123" или "" в случае нуля
        string getIntAsString(int someInt)
        {
            if (someInt == 0)
                return "";
            else
                return someInt.ToString();

        }

        //! Возвращает список всех NPC
        public List<string> getListOfNPC()
        {
            List<string> npc = new List<string>();
            foreach (string key in dialogs.Keys)
                npc.Add(key);
            return npc;
        }

        //--------------------------locale dialogs-------------------------------------------------------

        //! Возвращает CDialog по заданной локали, имени NPC и ID диалога
        public CDialog getLocaleDialog(int dialogID, string locale, string npcName)
        {
            if (this.locales.Keys.Contains(locale))
                if (this.locales[locale].Keys.Contains(npcName))
                    if (this.locales[locale][npcName].Keys.Contains(dialogID))
                        return locales[locale][npcName][dialogID];
            return null;
        }

        //! Добавить диалог к локали
        public void addLocaleDialog(CDialog dialog, string locale)
        {
            if (!this.locales.Keys.Contains(locale))
            {
                this.locales.Add(locale, new NPCDicts());
            }
            if (!this.locales[locale].Keys.Contains(dialog.Holder))
            {
                this.locales[locale].Add(dialog.Holder, new Dictionary<int, CDialog>());
            }

            if (this.locales[locale][dialog.Holder].Keys.Contains(dialog.DialogID))
                this.locales[locale][dialog.Holder].Remove(dialog.DialogID);
            this.locales[locale][dialog.Holder].Add(dialog.DialogID, dialog);
        }

        //! @todo исправлять это говнище, все затирает
        public void createExamples()
        {
            foreach (var npc in dialogs.Values)
                foreach (var dialog in npc.Values)
                {
                    dialog.Actions = new Actions();
                    dialog.Precondition = new CDialogPrecondition();
                }
            saveDialogs(parent.settings.dialogXML);
        }

        //! Возвращает словарь из диалогов для локализации (устаревшие, актуальные или все)
        public DifferenceDict getDialogDifference(string locale, FindType findType)
        {
            //System.Console.WriteLine("CDialogs::getDialogDifference");
            DifferenceDict ret = new DifferenceDict();
            if (this.locales.Keys.Contains(locale))
            {
                var cur_locale_info = this.locales[locale];
                foreach (var npc_name in dialogs.Keys)
                {
                    if (!cur_locale_info.Keys.Contains(npc_name))
                    {
                        //NPCDialogDict dict = parent.getDialogDictionary(npc_name);
                        NPCDialogDict dict = new NPCDialogDict();
                        dict.Add(dialogs[npc_name].First().Key, new CDialog() );
                        cur_locale_info.Add(npc_name, dict);
                    }
                    var locale_dialogs = cur_locale_info[npc_name];
                    foreach (var dialog in dialogs[npc_name].Values)
                    {
                        if (!dialog.coordinates.Active)
                            continue;
                        if (!location.ContainsKey(npc_name) || location[npc_name] == "notfound")
                            continue;

                        var locale_version = 0;
                        if (locale_dialogs.Keys.Contains(dialog.DialogID))
                            locale_version = locale_dialogs[dialog.DialogID].version;
                        
                        if (!ret.Keys.Contains(npc_name))
                            ret.Add(npc_name, new Dictionary<int, CDifference>());
                        switch (findType)
                        {
                            case FindType.all:
                                ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                            case FindType.outdatedOnly:
                                if (dialog.version != locale_version)
                                    ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                            case FindType.actualOnly:
                                if (dialog.version == locale_version)
                                    ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                        }
                    }
                }
            }
            //foreach (var name in ret.Keys)
            //    {
            //        System.Console.WriteLine(name + ":");
            //        foreach (var id in ret[name])
            //            System.Console.WriteLine("id:"+id.ToString());
            //    }
            return ret;
        }

        //! @todo исправлять это говнище!!!
        public void createResults()
        {
            NPCDicts results = new NPCDicts();
            foreach (var npc_name in this.locales[parent.settings.getCurrentLocale()].Keys)
                foreach (var locale_dialog in this.locales[parent.settings.getCurrentLocale()][npc_name].Values)
                {
                    var dialog = parent.getDialogOnDialogID(npc_name, locale_dialog.DialogID);
                    if (dialog != null && dialog.version == locale_dialog.version)
                    {
                        if (!results.Keys.Contains(npc_name))
                            results.Add(npc_name, new Dictionary<int, CDialog>());
                        results[npc_name].Add(dialog.DialogID, new CDialog(dialog.Holder, locale_dialog.Title,
                            locale_dialog.Text, dialog.Precondition, dialog.Actions, dialog.Nodes, 
                            dialog.DialogID, dialog.version, dialog.coordinates));
                    }
                }
            this.save(parent.settings.dialogXML, results);
        }
    }
}